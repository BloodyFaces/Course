using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using System.Windows;
using CourseMVVM.Views;
using CourseMVVM.Model;
using System.Collections.ObjectModel;

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
        private string _newSecretQuestion;
        private string _newAnswer;
        private bool _isStudent;
        private bool _isLector;
        private string _adminField;
        ObservableCollection<Question> _source;
        private bool _isAdmin;
        private string[] _questions;
        private int _group;
        private bool _hasGroup;

        public bool HasGroup
        {
            get { return _hasGroup; }
            set 
            { 
                _hasGroup = value;
                OnPropertyChanged("HasGroup");
            }
        }
        

        public int Group
        {
            get { return _group; }
            set 
            { 
                _group = value;
                OnPropertyChanged("Group");
            }
        }
        

        public string[] Questions
        {
            get { return _questions; }
            set 
            { 
                _questions = value;
                OnPropertyChanged("Questions");
            }
        }
        

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set 
            { 
                _isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }
        

        public string AdminField
        {
            get { return _adminField; }
            set 
            { 
                _adminField = value;
                OnPropertyChanged("AdminField");
            }
        }

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

        public string NewSecretQuestion
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
                {
                    IsLector = false;
                    IsAdmin = false;
                    HasGroup = true;
                }
            }
        }

        public bool IsLector
        {
            get { return _isLector; }
            set
            {
                _isLector = value;
                if (_isLector)
                {
                    IsStudent = false;
                    IsAdmin = true;
                    HasGroup = false;
                }
            }
        }
        public RegistrationViewModel()
        {
            _source = new ObservableCollection<Question>(DbContex.GetDbContex().Question.ToList());
            _questions = new string[_source.Count];
            for (int i = 0; i < _source.Count; i++)
            {
                _questions[i] = new string(_source[i].SecretQuestion.ToCharArray());
            }
            NewSecretQuestion = Questions[0];
            Group = 1;
            RegisterCommand = new RelayCommand(Register, CanRegister);
            BackCommand = new RelayCommand(Back);

        }

        public RelayCommand RegisterCommand { get; private set; }

        public bool CanRegister()
        {
            return (!String.IsNullOrWhiteSpace(NewLogin) && !String.IsNullOrWhiteSpace(NewPassword) && !String.IsNullOrWhiteSpace(NewPasswordVerify) &&
                   !String.IsNullOrWhiteSpace(NewName) && !String.IsNullOrWhiteSpace(NewSurname) && !String.IsNullOrWhiteSpace(NewPatronomyc) &&
                   !String.IsNullOrWhiteSpace(NewAnswer)) && (IsLector || IsStudent) && (!NewLogin.Contains(" ") && !NewPassword.Contains(" "));
        }

        public void Register()
        {
            if(NewPassword != NewPasswordVerify)
            {
                MessageBox.Show("Введенные пароли не совпадают!", "Ошибка в подтверждении пароля");
                NewPassword = "";
                NewPasswordVerify = "";
                AdminField = "";
                NewSecretQuestion = "";
                return;
            }
            foreach(var s in DbContex.GetDbListAccount())
            {
                if (s.Login == NewLogin)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует!", "Ошибка в выборе логина");
                    NewLogin = "";
                    AdminField = "";
                    NewSecretQuestion = "";
                    return;
                }
            }
            try
            {
                int questID = 1;
                foreach( var s in _source)
                {
                    if (s.SecretQuestion == NewSecretQuestion)
                        questID = s.QuestionId;
                }
                if (IsStudent)
                {
                    Account account = Model.DbContex.GetDbContex().Account.Add(new Account { Login = NewLogin, Password = NewPassword, SecretQuestionId = questID, Answer = NewAnswer });
                    Student student = Model.DbContex.GetDbContex().Student.Add(new Student { Name = NewName, Surname = NewSurname, Patronymic = NewPatronomyc, Group = this.Group, Login = account.Login, AccountSt = account });
                    Model.DbContex.GetDbContex().SaveChanges();
                    MessageBox.Show("Аккаунт успешно создан!", "Создание аккаунта");
                    Back();
                    Model.DbContex.Refresh();
                    ContexChangeExecute();
                }
                if (IsLector)
                {
                    if(!CheckAdmin())
                    {
                        MessageBox.Show("Неверно введено секретное слово администратора!", "Ошибка");
                        NewLogin = "";
                        AdminField = "";
                        NewSecretQuestion = "";
                        NewPassword = "";
                        NewPasswordVerify = "";
                        return;
                    }
                    Account account = Model.DbContex.GetDbContex().Account.Add(new Account { Login = NewLogin, Password = NewPassword, SecretQuestionId = questID, Answer = NewAnswer });
                    Lecturer lector = Model.DbContex.GetDbContex().Lecturer.Add(new Lecturer { Name = NewName, Surname = NewSurname, Patronymic = NewPatronomyc, Login = account.Login, AccountLr = account });
                    Model.DbContex.GetDbContex().SaveChanges();
                    MessageBox.Show("Аккаунт успешно создан!", "Создание аккаунта");
                    Back();
                    Model.DbContex.Refresh();
                    ContexChangeExecute();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при создании аккаунта");
            }
        }

        public RelayCommand BackCommand { get; private set; }

        public void Back()
        {
            ViewsContainer.RegistrationView.Close();
        }

        private bool CheckAdmin()
        {
            return AdminField == "Нарцей";
        }
    }
}
