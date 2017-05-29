using CourseMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;

namespace CourseMVVM.Views
{
    public class MBViewModel : ObservableObject
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                OnPropertyChanged("Message");
            }
        }
        
        public MBViewModel(string message)
        {
            Message = message;
            CancelCommand = new RelayCommand(CancelExecute);
        }

        public RelayCommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            ViewsContainer.MessageWin.Close();
        }
    }
}
