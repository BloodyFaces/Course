using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using CourseMVVM.Views;
using System.Collections.ObjectModel;
using CourseMVVM.Model;
using System.Windows;
using System.Windows.Interactivity;

namespace CourseMVVM.ViewModels
{
    public class CategoryViewModel: ObservableObject
    {
        private ObservableCollection<Categories> _catlist;
        private ObservableCollection<CatQuestions> _catQuest;
        private bool _wasSelected;
        private bool _wasSelectedCategory;
        private bool _wasSelectedQuestion;

        public int SelectedIndexCategory { get; set; }
        public int SelectedIndexQuestion { get; set; }

        public ObservableCollection<CatQuestions> CatQuestions
        {
            get { return _catQuest; }
            set
            {
                _catQuest = value;
                OnPropertyChanged("CatQuestions");
            }
        }

        public ObservableCollection<Categories> CatList
        {
            get { return _catlist; }
            set
            {
                _catlist = value;
                OnPropertyChanged("CatList");
            }
        }

        public CategoryViewModel()
        {
            _catlist = new ObservableCollection<Categories>(DbContex.GetDbListCategories());
            LogoutCommand = new RelayCommand(LogoutExecute);
            ToResultsCommand = new RelayCommand(ToResultsExecute);
            RenameCommand = new RelayCommand(RenameExecute, RenameCanExecute);
            SelectionChangedCommand = new RelayCommand<object>(SelectionChangedExecute);
            SelectionChangedQuestionCommand = new RelayCommand<object>(SelectionChangedQuestionExecute);
            AddQuestCommand = new RelayCommand(AddQuestExecute, AddQuestCanExecute);
            AddCatCommand = new RelayCommand(AddCatExecute);
            RemoveQuestionCommand = new RelayCommand(RemoveQuestionExecute, RemoveQuestionCanExecute);
            RemoveCategoryCommand = new RelayCommand<object>(RemoveCategoryExecute, RemoveCategoryCanExecute);
            _wasSelected = false;
            _wasSelectedCategory = false;
            _wasSelectedQuestion = false;
        }

        public RelayCommand LogoutCommand { get; set; }

        private void LogoutExecute()
        {
            ViewsContainer.LoginView = new MainWindow();
            ViewsContainer.LoginView.Show();
            ViewsContainer.CategoryWin.Close();
            ViewsContainer.LectorMainView.Close();
        }

        public RelayCommand ToResultsCommand { get; set; }

        private void ToResultsExecute()
        {
            ViewsContainer.CategoryWin.Close();
        }

        public RelayCommand<object> SelectionChangedCommand { get; set; }

        private void SelectionChangedExecute(object index)
        {
            if(index is int)
            {
                int Index = (int)index;
                SelectedIndexCategory = Index;
                _wasSelected = true;
                _wasSelectedCategory = true;
                CatQuestions = new ObservableCollection<CatQuestions>(DbContex.GetDbListQuestions(_catlist[Index].Cat_id));
            }
        }

        public RelayCommand<object> SelectionChangedQuestionCommand { get; set; }

        private void SelectionChangedQuestionExecute(object index)
        {
            if (index is int)
            {
                int Index = (int)index;
                SelectedIndexQuestion = Index;
                _wasSelectedQuestion = true;
            }
        }

        public RelayCommand AddCatCommand { get; set; }

        private void AddCatExecute()
        {
            ViewsContainer.AddViewWin = new AddCatView();
            ViewsContainer.AddViewWin.ShowDialog();
            DbContex.Refresh();
            _catlist.Clear();
            ObservableCollection<Categories> cat = new ObservableCollection<Categories>(DbContex.GetDbListCategories());
            foreach(var c in cat)
            {
                _catlist.Add(c);
            }
        }

        public RelayCommand AddQuestCommand { get; set; }

        private void AddQuestExecute()
        {
            ViewsContainer.AddQuestWin = new AddQuestView(_catlist[SelectedIndexCategory].Cat_id);
            ViewsContainer.AddQuestWin.ShowDialog();
            ObservableCollection<CatQuestions> cat = new ObservableCollection<CatQuestions>(DbContex.GetDbListQuestions(_catlist[SelectedIndexCategory].Cat_id));
            _catQuest.Clear();
            foreach (var c in cat)
            {
                _catQuest.Add(c);
            }
        }

        private bool AddQuestCanExecute()
        {
            return _wasSelected;
        }

        public RelayCommand<object> RemoveCategoryCommand { get; set; }

        private void RemoveCategoryExecute(object index)
        {
            if(index is int)
            {
                int Index = (int)index;
                DbContex.GetDbContex().Results.RemoveRange(DbContex.GetDbContex().Results.Where(s => s.RCat_id == _catlist[Index].Cat_id));
                DbContex.GetDbContex().CatQuestions.RemoveRange(DbContex.GetDbContex().CatQuestions.Where(s => s.QCat_id == _catlist[Index].Cat_id));
                DbContex.GetDbContex().Categories.Remove(_catlist[Index]);
                DbContex.GetDbContex().SaveChanges();
                DbContex.Refresh();
                _catlist.Clear();
                ObservableCollection<Categories> cat = new ObservableCollection<Categories>(DbContex.GetDbListCategories());
                foreach (var c in cat)
                {
                    _catlist.Add(c);
                }
            }
        }

        private bool RemoveCategoryCanExecute(object obj)
        {
            return _wasSelectedCategory;
        }

        public RelayCommand RenameCommand { get; set; }

        private void RenameExecute()
        {
            ViewsContainer.RenameCategoryWin = new RenameCategory(_catlist[SelectedIndexCategory].Cat_id);
            ViewsContainer.RenameCategoryWin.ShowDialog();
            _catlist.Clear();
            ObservableCollection<Categories> cat = new ObservableCollection<Categories>(DbContex.GetDbListCategories());
            foreach (var c in cat)
            {
                _catlist.Add(c);
            }
        }

        private bool RenameCanExecute()
        {
            return _wasSelectedCategory;
        }

        public RelayCommand RemoveQuestionCommand { get; set; }

        private void RemoveQuestionExecute()
        {
            var answers = DbContex.GetDbContex().CatAnswers.ToList();
            CatAnswers answer = new CatAnswers();
            foreach(var s in answers)
            {
                if (s.ACat_id.Value == _catQuest[SelectedIndexQuestion].CatQuestionID)
                {
                    answer = s;
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении вопроса", "Ошибка");
                    return;
                }
            }
            DbContex.GetDbContex().CatAnswers.Remove(answer);
            DbContex.GetDbContex().CatQuestions.Remove(_catQuest[SelectedIndexQuestion]);
            DbContex.GetDbContex().SaveChanges();
            DbContex.Refresh();
            ObservableCollection<CatQuestions> cat = new ObservableCollection<CatQuestions>(DbContex.GetDbListQuestions(_catlist[SelectedIndexCategory].Cat_id));
            _catQuest.Clear();
            foreach (var c in cat)
            {
                _catQuest.Add(c);
            }
        }

        private bool RemoveQuestionCanExecute()
        {
            return _wasSelectedQuestion;
        }
    }
}
