using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using CourseMVVM.Model;
using CourseMVVM.Views;
using System.Windows;

namespace CourseMVVM.ViewModels
{
    public class RenameCategoryViewModel : ObservableObject
    {
        private int _catID;
        private string _newName;

        public string NewName
        {
            get { return _newName; }
            set 
            { 
                _newName = value;
                OnPropertyChanged("NewName");
            }
        }
        

        public RenameCategoryViewModel(int catID)
        {
            _catID = catID;
            ConfirmCommand = new RelayCommand(ConfirmExecute, ConfirmCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
        }

        public RelayCommand ConfirmCommand { get; set; }

        private void ConfirmExecute()
        {
            var cat = DbContex.GetDbContex().Categories.Where(s => s.Cat_id == _catID).First();
            cat.Cat_name = NewName;
            DbContex.GetDbContex().SaveChanges();
            DbContex.Refresh();
            ViewsContainer.Show("Категория переименована");
            CancelExecute();
        }

        private bool ConfirmCanExecute()
        {
            return !String.IsNullOrWhiteSpace(NewName);
        }

        public RelayCommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            ViewsContainer.RenameCategoryWin.Close();
        }
    }
}
