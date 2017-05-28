using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using CourseMVVM.Model;
using System.Windows;
using CourseMVVM.Views;

namespace CourseMVVM.ViewModels
{
    public class PasswordRestoreViewModel: ObservableObject
    {
        private string _userLogin;
        private string _quest;
        private string _answerQuest;
        private string _userPass;
        private Question _questLocal;
        private Account _acc;

        public string UserPass
        {
            get { return _userPass; }
            set 
            { 
                _userPass = value;
                OnPropertyChanged("UserPass");
            }
        }
        

        public string AnswerQuest
        {
            get { return _answerQuest; }
            set 
            { 
                _answerQuest = value;
                OnPropertyChanged("AnswerQuest");
            }
        }
        

        public string Quest
        {
            get { return _quest; }
            set 
            { 
                _quest = value;
                OnPropertyChanged("Quest");
            }
        }
        
        public string UserLogin
        {
            get { return _userLogin; }
            set 
            { 
                _userLogin = value;
                OnPropertyChanged("UserLogin");
            }
        }
        
        public PasswordRestoreViewModel()
        {
            GetQuestCommand = new RelayCommand(GetQuestExecute, GetQuestCanExecute);
            GetPassCommand = new RelayCommand(GetPassExecute, GetQuestCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
        }

        public RelayCommand GetQuestCommand { get; set; }
        
        private void GetQuestExecute()
        {
            var acc = DbContex.GetDbContex().Account.Where(s => s.Login == UserLogin).ToList();
            if(acc.Count == 0)
            {
                MessageBox.Show("Неверное имя аккаунта!", "Ошибка");
                UserLogin = "";
                return;
            }
            _acc = acc[0];
            var quests = DbContex.GetDbContex().Question.ToList();
            foreach(var s in quests)
            {
                if(s.QuestionId == acc[0].SecretQuestionId)
                {
                    Quest = s.SecretQuestion;
                    _questLocal = s;
                }
            }
        }

        private bool GetQuestCanExecute()
        {
            return !String.IsNullOrWhiteSpace(UserLogin);
        }

        public RelayCommand GetPassCommand { get; set; }

        private void GetPassExecute()
        {
            if(AnswerQuest == _acc.Answer)
            {
                UserPass = _acc.Password;
            }
            else
            {
                MessageBox.Show("Неправильный ответ!", "Ошибка");
                AnswerQuest = "";
            }
        }

        private bool GetPassCanExecute()
        {
            return !String.IsNullOrWhiteSpace(AnswerQuest);
        }

        public RelayCommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            ViewsContainer.PasswordRestoreView.Close();
        }
    }
}
