using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseMVVM.Commands;
using System.Threading.Tasks;
using CourseMVVM.Views;
using CourseMVVM.Model;
using System.Windows;

namespace CourseMVVM.ViewModels
{
    public class AddQuestViewModel: ObservableObject
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
        
        public AddQuestViewModel(int category)
        {
            RightAnswer = new string[4] { "A", "B", "C", "D" };
            Weights = new int[7] { 4, 5, 6, 7, 8, 9, 10 };
            AddQuestionCommand = new RelayCommand(AddQuestionExecute, AddQuestionCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            SelectedAnswer = RightAnswer[0];
            SelectedWeight = Weights[0];
            _category = category;
        }

        public RelayCommand AddQuestionCommand { get; set; }

        private void AddQuestionExecute()
        {
            try
            {
                CatQuestions quest = DbContex.GetDbContex().CatQuestions.Add(new CatQuestions { QDesc = Description, QCat_id = _category, QAnswer = SelectedAnswer, QWeight = SelectedWeight });
                CatAnswers answer = DbContex.GetDbContex().CatAnswers.Add(new CatAnswers { AnswerA = this.AnswerA, AnswerB = this.AnswerB, AnswerC = this.AnswerC, AnswerD = this.AnswerD, ACat_id = quest.CatQuestionID });
                DbContex.GetDbContex().SaveChanges();
                MessageBox.Show("Вопрос успешно добавлен!", "Успех");
                Description = "";
                AnswerA = "";
                AnswerB = "";
                AnswerC = "";
                AnswerD = "";
                SelectedAnswer = RightAnswer[0];
                SelectedWeight = Weights[0];
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении вопроса в базу данных", "Ошибка");
            }
        }

        private bool AddQuestionCanExecute()
        {
            return (!String.IsNullOrWhiteSpace(Description) && !String.IsNullOrWhiteSpace(AnswerA)
                && !String.IsNullOrWhiteSpace(AnswerB) && !String.IsNullOrWhiteSpace(AnswerC)
                && !String.IsNullOrWhiteSpace(AnswerD));
        }

        public RelayCommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            ViewsContainer.AddQuestWin.Close();
        }
    }
}