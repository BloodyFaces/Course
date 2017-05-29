using CourseMVVM.Commands;
using CourseMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Model;
using System.Windows;

namespace CourseMVVM.ViewModels
{
    public class StudentMainViewModel: ObservableObject
    {
        private List<string> _catlist;
        private string _selected;
        private string _login;
        private string _password;
        private List<Results> _results;
        private int _points;
        private int _total;
        private int _mark;

        public StudentMainViewModel(string login, string password)
        {
            _login = login;
            _password = password;
            StartTestCommand = new RelayCommand(StartTest);
            _results = DbContex.FindResults(_login, _password);
            List<Categories> catlist = DbContex.GetDbListCategories();
            _catlist = new List<string>();
            foreach(var s in catlist)
            {
                _catlist.Add(s.Cat_name);
            }
            LogoutCommand = new RelayCommand(LogoutExecute);
        }

        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged("Points");
            }
        }

        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        public int Mark
        {
            get { return _mark; }
            set
            {
                _mark = value;
                OnPropertyChanged("Mark");
            }
        }

        public List<string> CatList
        {
            get { return _catlist; }
            set
            {
                _catlist = value;
                OnPropertyChanged("CatList");
            }
        }

        public string Selected
        {
            get 
            {
                return _selected; 
            }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
                ChangeResults(_selected);
            }
        }

        public RelayCommand LogoutCommand { get; set; }

        private void LogoutExecute()
        {
            ViewsContainer.LoginView = new MainWindow();
            ViewsContainer.LoginView.Show();
            ViewsContainer.StudentMainView.Close();
        }

        public RelayCommand StartTestCommand { get; set; }

        private void StartTest()
        {
            try
            {
                int cat = 0;
                List<Categories> catlist = DbContex.GetDbListCategories();
                foreach (var s in catlist)
                {
                    if (s.Cat_name == Selected)
                    {
                        cat = s.Cat_id;
                        if(s.CatQuestions.ToList().Count==0)
                        {
                            ViewsContainer.Show("Текущая категория не содержит вопросов!");
                            return;
                        }
                    }
                }
                ViewsContainer.TestWin = new Test(cat, _login);
                ViewsContainer.TestWin.ShowDialog();
                _results = DbContex.FindResults(_login, _password);
                ChangeResults(_selected);
            }
            catch
            {
                ViewsContainer.Show("Выберите тест!");
            }
        }

        private void ChangeResults(string selected)
        {
            bool isChanged = false;
            foreach(var s in _results)
            {
                if(s.CategoryRs.Cat_name == selected)
                {
                    Points = s.Points.Value;
                    Total = s.TotalPoints.Value;
                    Mark = s.Mark.Value;
                    isChanged = true;
                }
            }
            if(!isChanged)
            {
                Points = 0;
                Total = 0;
                Mark = 0;
            }
        }
    }
}
