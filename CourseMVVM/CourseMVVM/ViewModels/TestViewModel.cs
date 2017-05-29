using CourseMVVM.Commands;
using CourseMVVM.Model;
using CourseMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseMVVM.ViewModels
{
    public class TestViewModel: ObservableObject
    {
        private List<CatQuestions> _catqt;
        private List<CatQuestions> _skipedqt;
        private int _questionNumb;
        private int _curQuestion;
        private string _login;
        private string _desc;
        private string _aA;
        private string _aB;
        private string _aC;
        private string _aD;
        private int _bank;
        private int _pocket;
        private bool _isA;
        private bool _isB;
        private bool _isC;
        private bool _isD;
        private string _selected;
        private string _nextContent;
        private bool _skiped;
        private int _cat_id;

        public string NextContent
        {
            get { return _nextContent; }
            set
            {
                _nextContent = value;
                OnPropertyChanged("NextContent");
            }
        }
        
        public TestViewModel(int cat_id, string login)
        {
            _login = login;
            _cat_id = cat_id;
            _catqt = DbContex.GetDbListQuestions(cat_id);
            _questionNumb = _catqt.Count;
            CurrentQuestion = 1;
            NextContent = "Следующий";
            _skiped = false;
            _skipedqt = new List<CatQuestions>();
            NextCommand = new RelayCommand(NextExecute, NextCanExecute);
            SkipCommand = new RelayCommand(SkipExecute, SkipCanExecute);
            FinishCommand = new RelayCommand(FinishExecute);
            Description = _catqt[CurrentQuestion - 1].QDesc;
            List<CatAnswers> list = DbContex.GetDbCatAnswers(_catqt[CurrentQuestion-1].CatQuestionID);
            AnswerA = list[0].AnswerA;
            AnswerB = list[0].AnswerB;
            AnswerC = list[0].AnswerC;
            AnswerD = list[0].AnswerD;
            _bank = 0;
            CA = "A";
            CB = "B";
            CC = "C";
            CD = "D";
            foreach(var s in _catqt)
            {
                _bank += s.QWeight;
            }
            _pocket = 0;
        }

        public bool IsA
        {
            get { return _isA; }
            set
            {
                _isA = value;
                if(_isA)
                {
                    IsB = false;
                    IsC = false;
                    IsD = false;
                    _selected = "A";
                }
                OnPropertyChanged("IsA");
            }
        }

        public bool IsB
        {
            get { return _isB; }
            set
            {
                _isB = value;
                if (_isB)
                {
                    IsA = false;
                    IsC = false;
                    IsD = false;
                    _selected = "B";
                }
                OnPropertyChanged("IsB");
            }
        }

        public bool IsC
        {
            get { return _isC; }
            set
            {
                _isC = value;
                if (_isC)
                {
                    IsB = false;
                    IsA = false;
                    IsD = false;
                    _selected = "C";
                }
                OnPropertyChanged("IsC");
            }
        }

        public bool IsD
        {
            get { return _isD; }
            set
            {
                _isD = value;
                if (_isD)
                {
                    IsB = false;
                    IsC = false;
                    IsA = false;
                    _selected = "D";
                }
                OnPropertyChanged("IsD");
            }
        }

        public int NumberOfQuestions
        {
            get { return _questionNumb; }
            set
            {
                _questionNumb = value;
                OnPropertyChanged("NumberOfQuestions");
            }
        }

        public int CurrentQuestion
        {
            get { return _curQuestion; }
            set
            {
                _curQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        public string CA { get; private set; }

        public string CB { get; private set; }

        public string CC { get; private set; }

        public string CD { get; private set; }

        public string Description
        {
            get { return _desc; }
            set
            {
                _desc = value;
                OnPropertyChanged("Description");
            }
        }

        public string AnswerA
        {
            get { return _aA; }
            set
            {
                _aA = value;
                OnPropertyChanged("AnswerA");
            }
        }

        public string AnswerB
        {
            get { return _aB; }
            set
            {
                _aB = value;
                OnPropertyChanged("AnswerB");
            }
        }

        public string AnswerC
        {
            get { return _aC; }
            set
            {
                _aC = value;
                OnPropertyChanged("AnswerC");
            }
        }

        public string AnswerD
        {
            get { return _aD; }
            set
            {
                _aD = value;
                OnPropertyChanged("AnswerD");
            }
        }

        public RelayCommand NextCommand { get; set; }

        private void NextExecute()
        {
            if (_catqt[CurrentQuestion - 1].QAnswer == _selected)
            {
                _pocket += _catqt[CurrentQuestion - 1].QWeight;
            }
            if(CurrentQuestion == NumberOfQuestions)
            {
                if (_skiped)
                {
                    ToSkipedExecute();
                }
                else
                {
                    FinishExecute();
                }
                return;
            }
            CurrentQuestion++;
            if (CurrentQuestion == NumberOfQuestions && !_skiped)
            {
                NextContent = "Завершить";
            }
            Description = _catqt[CurrentQuestion - 1].QDesc;
            List<CatAnswers> list = DbContex.GetDbCatAnswers(_catqt[CurrentQuestion - 1].CatQuestionID);
            AnswerA = list[0].AnswerA;
            AnswerB = list[0].AnswerB;
            AnswerC = list[0].AnswerC;
            AnswerD = list[0].AnswerD;
            IsA = false;
            IsB = false;
            IsC = false;
            IsD = false;
        }

        private bool NextCanExecute()
        {
            return (IsA || IsB || IsC || IsD);
        }

        public RelayCommand SkipCommand { get; set; }

        private void SkipExecute()
        {
            _skiped = true;
            _skipedqt.Add(_catqt[CurrentQuestion - 1]);
            if (CurrentQuestion == NumberOfQuestions)
            {
                ToSkipedExecute();
                return;
            }
            CurrentQuestion++;
            Description = _catqt[CurrentQuestion - 1].QDesc;
            List<CatAnswers> list = DbContex.GetDbCatAnswers(_catqt[CurrentQuestion - 1].CatQuestionID);
            AnswerA = list[0].AnswerA;
            AnswerB = list[0].AnswerB;
            AnswerC = list[0].AnswerC;
            AnswerD = list[0].AnswerD;
            IsA = false;
            IsB = false;
            IsC = false;
            IsD = false;
        }

        private bool SkipCanExecute()
        {
            return (CurrentQuestion <= NumberOfQuestions);
        }

        private void ToSkipedExecute()
        {
            CatQuestions[] s = new CatQuestions[_skipedqt.Count];
            _skipedqt.CopyTo(s);
            _skipedqt.Clear();
            _catqt = null;
            _catqt = new List<CatQuestions>(s);
            CurrentQuestion = 1;
            NumberOfQuestions = _catqt.Count;
            Description = _catqt[CurrentQuestion - 1].QDesc;
            List<CatAnswers> list = DbContex.GetDbCatAnswers(_catqt[CurrentQuestion - 1].CatQuestionID);
            AnswerA = list[0].AnswerA;
            AnswerB = list[0].AnswerB;
            AnswerC = list[0].AnswerC;
            AnswerD = list[0].AnswerD;
            IsA = false;
            IsB = false;
            IsC = false;
            IsD = false;
            _skiped = false;
        }

        public RelayCommand FinishCommand { get; set; }

        private void FinishExecute()
        {
            int percent = (_pocket * 100)/ _bank;
            int mark = (int)(percent/10);
            ViewsContainer.Show("Ваши баллы: " + _pocket + "\nОбщее количество: " + _bank + "\nПроцент: " + percent + "\nОценка: " + mark);
            Results result = Model.DbContex.GetDbContex().Results.Add(new Results { Owner = _login, Points = _pocket, TotalPoints = _bank, Mark = mark, RCat_id = _cat_id });
            Model.DbContex.GetDbContex().SaveChanges();
            Model.DbContex.Refresh();
            ViewsContainer.TestWin.Close();
        }
    }
}
