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
            EditCommand = new RelayCommand(EditExecute, EditCanExecute);
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
                if (Index < 0)
                {
                    SelectedIndexCategory = Index;
                    return;
                }
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
            return _wasSelected && SelectedIndexCategory >= 0;
        }

        public RelayCommand<object> RemoveCategoryCommand { get; set; }

        private void RemoveCategoryExecute(object index)
        {
            if(index is int)
            {
                int Index = (int)index;
                var results = DbContex.GetDbContex().Results.ToList();
                List<Results> res = new List<Results>();
                foreach(var s in results)
                {
                    if (s.RCat_id == _catlist[Index].Cat_id)
                    {
                        res.Add(s);
                    }
                }
                DbContex.GetDbContex().Results.RemoveRange(res);
                var catQ = DbContex.GetDbContex().CatQuestions.ToList();
                List<CatQuestions> cQ = new List<CatQuestions>();
                foreach(var s in catQ)
                {
                    if (s.QCat_id == _catlist[Index].Cat_id)
                    {
                        cQ.Add(s);
                        DbContex.GetDbContex().CatAnswers.Remove(DbContex.GetDbContex().CatAnswers.Single(c => c.ACat_id.Value == s.CatQuestionID));
                    }
                }
                DbContex.GetDbContex().CatQuestions.RemoveRange(cQ);
                var caties = DbContex.GetDbContex().Categories.ToList();
                Categories cats = new Categories();
                foreach(var s in caties)
                {
                    if(s.Cat_id == _catlist[Index].Cat_id)
                    {
                        cats = s;
                    }
                }
                DbContex.GetDbContex().Categories.Remove(cats);
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
            return _wasSelectedCategory && SelectedIndexCategory >= 0;
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
            return _wasSelectedCategory && SelectedIndexCategory >= 0;
        }

        public RelayCommand RemoveQuestionCommand { get; set; }

        private void RemoveQuestionExecute()
        {
            try
            {
                var answers = DbContex.GetDbContex().CatAnswers.ToList();
                CatAnswers answer = new CatAnswers();
                foreach (var s in answers)
                {
                    if (s.ACat_id.Value == _catQuest[SelectedIndexQuestion].CatQuestionID)
                    {
                        answer = s;
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
            catch
            {
                ViewsContainer.Show("Ошибка при удалении вопроса");
            }
        }

        private bool RemoveQuestionCanExecute()
        {
            return _wasSelectedQuestion && SelectedIndexQuestion >= 0;
        }

        public RelayCommand EditCommand { get; set; }

        private void EditExecute()
        {
            try
            {
                var answers = DbContex.GetDbContex().CatAnswers.ToList();
                CatAnswers answer = new CatAnswers();
                foreach (var s in answers)
                {
                    if (s.ACat_id.Value == _catQuest[SelectedIndexQuestion].CatQuestionID)
                    {
                        answer = s;
                    }
                }
                ViewsContainer.EditQuesWin = new EditQuestionView(_catlist[SelectedIndexCategory].Cat_id, _catQuest[SelectedIndexQuestion], answer);
                ViewsContainer.EditQuesWin.ShowDialog();
                ObservableCollection<CatQuestions> cat = new ObservableCollection<CatQuestions>(DbContex.GetDbListQuestions(_catlist[SelectedIndexCategory].Cat_id));
                _catQuest.Clear();
                foreach (var c in cat)
                {
                    _catQuest.Add(c);
                }
            }
            catch
            {
                ViewsContainer.Show("Программная ошибка!");
            }
        }

        private bool EditCanExecute()
        {
            return _wasSelectedQuestion && SelectedIndexQuestion >= 0;
        }
    }
}
