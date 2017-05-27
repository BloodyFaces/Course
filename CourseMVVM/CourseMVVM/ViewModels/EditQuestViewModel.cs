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
    public class EditQuestViewModel: ObservableObject
    {
        private string _description;
        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;
        private string[] _rightAnswer;
        private int[] _weights;
        private string _selectedAnswer;
        private int _selectedWeight;
        private int _category;
        private CatQuestions _catQuest;
        private CatAnswers _catAnswer;

        public int SelectedWeight
        {
            get { return _selectedWeight; }
            set
            {
                _selectedWeight = value;
                OnPropertyChanged("SelectedWeight");
            }
        }


        public string SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                OnPropertyChanged("SelectedAnswer");
            }
        }

        public int[] Weights
        {
            get { return _weights; }
            set
            {
                _weights = value;
                OnPropertyChanged("Weights");
            }
        }

        public string[] RightAnswer
        {
            get { return _rightAnswer; }
            set
            {
                _rightAnswer = value;
                OnPropertyChanged("RightAnswer");
            }
        }


        public string AnswerD
        {
            get { return _answerD; }
            set
            {
                _answerD = value;
                OnPropertyChanged("AnswerD");
            }
        }


        public string AnswerC
        {
            get { return _answerC; }
            set
            {
                _answerC = value;
                OnPropertyChanged("AnswerC");
            }
        }


        public string AnswerB
        {
            get { return _answerB; }
            set
            {
                _answerB = value;
                OnPropertyChanged("AnswerB");
            }
        }


        public string AnswerA
        {
            get { return _answerA; }
            set
            {
                _answerA = value;
                OnPropertyChanged("AnswerA");
            }
        }


        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public EditQuestViewModel(int category, CatQuestions catQuest, CatAnswers catAnswer)
        {
            RightAnswer = new string[4] { "A", "B", "C", "D" };
            Weights = new int[7] { 4, 5, 6, 7, 8, 9, 10 };
            EditQuestionCommand = new RelayCommand(EditQuestionExecute, EditQuestionCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            _catQuest = catQuest;
            _catAnswer = catAnswer;
            Description = _catQuest.QDesc;
            _selectedAnswer = _catQuest.QAnswer;
            _selectedWeight = _catQuest.QWeight;
            AnswerA = _catAnswer.AnswerA;
            AnswerB = _catAnswer.AnswerB;
            AnswerC = _catAnswer.AnswerC;
            AnswerD = _catAnswer.AnswerD;
            _category = category;
        }

        public RelayCommand EditQuestionCommand { get; set; }

        private void EditQuestionExecute()
        {
            try
            {
                CatQuestions quest = DbContex.GetDbContex().CatQuestions.Where(s => s.CatQuestionID == _catQuest.CatQuestionID).First();
                quest.QDesc = Description;
                quest.QWeight = SelectedWeight;
                quest.QAnswer = SelectedAnswer;
                CatAnswers answer = DbContex.GetDbContex().CatAnswers.Where(s => s.Answers_id == _catAnswer.Answers_id).First();
                answer.AnswerA = AnswerA;
                answer.AnswerB = AnswerB;
                answer.AnswerC = AnswerC;
                answer.AnswerD = AnswerD;
                DbContex.GetDbContex().SaveChanges();
                DbContex.Refresh();
                MessageBox.Show("Вопрос успешно изменен", "Успех");
                CancelExecute();
            }
            catch
            {
                MessageBox.Show("Ошибка при измении данных", "Ошибка");
            }
        }

        private bool EditQuestionCanExecute()
        {
            return (!String.IsNullOrWhiteSpace(Description) && !String.IsNullOrWhiteSpace(AnswerA)
                && !String.IsNullOrWhiteSpace(AnswerB) && !String.IsNullOrWhiteSpace(AnswerC)
                && !String.IsNullOrWhiteSpace(AnswerD));
        }

        public RelayCommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            ViewsContainer.EditQuesWin.Close();
        }
    }
}
