using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using System.Windows;
using CourseMVVM.Views;
using CourseMVVM.Model;

namespace CourseMVVM.ViewModels
{
    public class RegistrationViewModel : ObservableObject
    {
        private string _newLogin;
        private string _newPassword;
        private string _newPasswordVerify;
        private string _newName;
        private string _newSurname;
        private string _newPatronomyc;
        private int _newSecretQuestion;
        private string _newAnswer;
        private bool _isStudent;
        private bool _isLector;


        public static event Action ContexChange;

        public void ContexChangeExecute()
        {
            if(ContexChange!=null)
                ContexChange();
        }

        public string NewLogin
        {
            get { return _newLogin; }
            set
            {
                _newLogin = value;
                OnPropertyChanged("NewLogin");
            }
        }
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        public string NewPasswordVerify
        {
            get { return _newPasswordVerify; }
            set
            {
                _newPasswordVerify = value;
                OnPropertyChanged("NewPasswordVerify");
            }
        }

        public string NewName
        {
            get { return _newName; }
            set
            {
                _newName = value;
                OnPropertyChanged("NewName");
            }
        }

        public string NewSurname
        {
            get { return _newSurname; }
            set
            {
                _newSurname = value;
                OnPropertyChanged("NewSurname");
            }
        }

        public string NewPatronomyc
        {
            get { return _newPatronomyc; }
            set
            {
                _newPatronomyc = value;
                OnPropertyChanged("NewPatronomyc");
            }
        }

        public int NewSecretQuestion
        {
            get { return _newSecretQuestion; }
            set
            {
                _newSecretQuestion = value;
                OnPropertyChanged("NewSecretQuestion");
            }
        }

        public string NewAnswer
        {
            get { return _newAnswer; }
            set
            {
                _newAnswer = value;
                OnPropertyChanged("NewAnswer");
            }
        }

        public bool IsStudent
        {
            get { return _isStudent; }
            set
            {
                _isStudent = value;
                if (_isStudent)
                    IsLector = false;
            }
        }

        public bool IsLector
        {
            get { return _isLector; }
            set
            {
                _isLector = value;
                if (_isLector)
                    IsStudent = false;
            }
        }
        public RegistrationViewModel()
        {
            NewSecretQuestion = 1;
            RegisterCommand = new RelayCommand(Register, CanRegister);
            BackCommand = new RelayCommand(Back);
        }

        public RelayCommand RegisterCommand { get; private set; }

        public bool CanRegister()
        {
            return (!String.IsNullOrWhiteSpace(NewLogin) && !String.IsNullOrWhiteSpace(NewPassword) && !String.IsNullOrWhiteSpace(NewPasswordVerify) &&
                   !String.IsNullOrWhiteSpace(NewName) && !String.IsNullOrWhiteSpace(NewSurname) && !String.IsNullOrWhiteSpace(NewPatronomyc) &&
                   !String.IsNullOrWhiteSpace(NewAnswer)) && (IsLector || IsStudent);
        }

        public void Register()
        {
            if(NewPassword != NewPasswordVerify)
            {
                MessageBox.Show("Введенные пароли не совпадают!", "Ошибка в подтверждении пароля");
                return;
            }
            foreach(var s in DbContex.GetDbListAccount())
            {
                if (s.Login == NewLogin)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует!", "Ошибка в выборе логина");

                    return;
                }
            }
            //try
            //{
                if (IsStudent)
                {
                    Account account = Model.DbContex.GetDbContex().Account.Add(new Account { Login = NewLogin, Password = NewPassword, SecretQuestionId = NewSecretQuestion, Answer = NewAnswer });
                    Student student = Model.DbContex.GetDbContex().Student.Add(new Student { Name = NewName, Surname = NewSurname, Patronymic = NewPatronomyc, Group = 1, Login = account.Login, AccountSt = account });
                    Model.DbContex.GetDbContex().SaveChanges();
                    MessageBox.Show("Аккаунт успешно создан!", "Создание аккаунта");
                    Back();
                    Model.DbContex.Refresh();
                    ContexChangeExecute();
                }
                if (IsLector)
                {
                    Account account = Model.DbContex.GetDbContex().Account.Add(new Account { Login = NewLogin, Password = NewPassword, SecretQuestionId = NewSecretQuestion, Answer = NewAnswer });
                    Lecturer lector = Model.DbContex.GetDbContex().Lecturer.Add(new Lecturer { Name = NewName, Surname = NewSurname, Patronymic = NewPatronomyc, Login = account.Login, AccountLr = account });
                    Model.DbContex.GetDbContex().SaveChanges();
                    MessageBox.Show("Аккаунт успешно создан!", "Создание аккаунта");
                    Back();
                    Model.DbContex.Refresh();
                    ContexChangeExecute();
                }
            //}
            //catch
           /// {
            //    MessageBox.Show("Ошибка при создании аккаунта");
            //}
        }

        public RelayCommand BackCommand { get; private set; }

        public void Back()
        {
            ViewsContainer.RegistrationView.Close();
        }
    }
}
