using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Model;
using System.Collections.ObjectModel;
using CourseMVVM.Commands;
using CourseMVVM.Views;
using System.Windows;

namespace CourseMVVM.ViewModels
{
    public class LectorMainViewModel: ObservableObject
    {
        private ObservableCollection<AccountData> _studList;

        public ObservableCollection<AccountData> StudentList
        {
            get { return _studList; }
            set
            {
                _studList = value;
                OnPropertyChanged("AccountList");
            }
        }

        public LectorMainViewModel()
        {
            ToCategoriesCommand = new RelayCommand(ToCategoiesExecute);
            LogoutCommand = new RelayCommand(LogoutExecute);
            ViewResultCommand = new RelayCommand<object>(ViewResultExecute);
            List<Student> list = DbContex.GetDbStudentList();
            AccountData[] acc = new AccountData[list.Count];
            for(int i=0; i<list.Count; i++)
            {
                acc[i] = new AccountData();
                acc[i].Name = "Имя: " + list[i].Name;
                acc[i].Surname = "Фамилия: " + list[i].Surname;
                acc[i].Patronymic = "Отчество: " + list[i].Patronymic;
                acc[i].Login = "Логин: " + list[i].Login;
                acc[i].Group = "Группа: " + list[i].Group.Value.ToString();
            }
            _studList = new ObservableCollection<AccountData>(acc);
        }

        public RelayCommand LogoutCommand { get; set; }

        private void LogoutExecute()
        {
            ViewsContainer.LoginView = new MainWindow();
            ViewsContainer.LoginView.Show();
            ViewsContainer.LectorMainView.Close();
        }

        public RelayCommand<object> ViewResultCommand { get; set; }

        private void ViewResultExecute(object selected)
        {
            if(selected is int)
            {
                try
                {
                    int Selected = (int)selected;
                    AccountData tmp = _studList[Selected];
                    string login = tmp.Login.Substring(7);
                    List<Results> results = DbContex.FindResults(login);
                    ResultsData[] res = new ResultsData[results.Count];
                    for (int i = 0; i < results.Count; i++)
                    {
                        res[i] = new ResultsData();
                        res[i].Number = "Написанный тест: " + (i + 1).ToString();
                        res[i].Category = "Категория: " + results[i].CategoryRs.Cat_name;
                        res[i].TotalPoints = "Макс. баллов: " + results[i].TotalPoints.Value.ToString();
                        res[i].Points = "Набрано: " + results[i].Points.Value.ToString();
                        res[i].Mark = "Оценка: " + results[i].Mark.Value.ToString();
                    }
                    List<ResultsData> list = new List<ResultsData>(res);
                    ViewsContainer.ResultsWin = new ResultsView(list);
                    ViewsContainer.ResultsWin.ShowDialog();
                }
                catch
                {
                    ViewsContainer.Show("Выберите студента");
                }
            }
        }

        public RelayCommand ToCategoriesCommand { get; set; }

        private void ToCategoiesExecute()
        {
            ViewsContainer.CategoryWin = new CategoryView();
            ViewsContainer.CategoryWin.ShowDialog();
        }
    }
}
