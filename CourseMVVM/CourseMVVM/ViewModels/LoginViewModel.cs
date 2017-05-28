using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using System.Windows;
using CourseMVVM.Model;
using CourseMVVM.Views;

namespace CourseMVVM.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        string _name;
        string _password;
        List<Account> _acc;

        public string Login
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public LoginViewModel()
        {
            VerifyCommand = new RelayCommand(VerifyExecute, CanVerifyExecute);
            RegistrationCommand = new RelayCommand(Register);
            _acc = DbContex.GetDbListAccount();
            RegistrationViewModel.ContexChange += ListAccountsRefresh;
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
        }

        private void ListAccountsRefresh()
        {
            _acc = DbContex.GetDbListAccount();
        }

        public RelayCommand VerifyCommand { get; private set; }

        public void VerifyExecute()
        {
            bool Exist = false;
            bool isLector = false;
            string login ="";
            string password = "";
            foreach (var s in _acc)
            {
                if (s.Login == Login && s.Password == Password)
                {
                    Exist = true;
                    login = s.Login;
                    password = s.Password;
                    if (s.Lecturer.Count != 0)
                        isLector = true;
                }
            }
            if (Exist)
            {
                if(isLector)
                {
                    ViewsContainer.LectorMainView = new LectorMain();
                    ViewsContainer.LectorMainView.Show();
                    ViewsContainer.LoginView.Close();
                }
                else
                {
                    ViewsContainer.StudentMainView = new StudentMain(login, password);
                    ViewsContainer.StudentMainView.Show();
                    ViewsContainer.LoginView.Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка в логине или пароле. Пожалуйста, проверьте введенные данные!", "Доступ запрещен");
                Login = "";
            }
        }

        public bool CanVerifyExecute()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        public RelayCommand RegistrationCommand { get; private set; }

        public void Register()
        {
            ViewsContainer.RegistrationView = new Registration();
            ViewsContainer.RegistrationView.ShowDialog();
        }

        public RelayCommand ForgotPasswordCommand { get; private set; }

        public void ForgotPassword()
        {
            ViewsContainer.PasswordRestoreView = new PasswordRestore();
            ViewsContainer.PasswordRestoreView.ShowDialog();
        }
    }
}
