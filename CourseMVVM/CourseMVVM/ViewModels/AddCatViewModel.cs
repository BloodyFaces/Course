using CourseMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseMVVM.Commands;
using System.Windows;
using CourseMVVM.Views;

namespace CourseMVVM.ViewModels
{
    public class AddCatViewModel: ObservableObject
    {
        private string _catName;

        public string CategoryName
        {
            get { return _catName; }
            set 
            { 
                _catName = value;
                OnPropertyChanged("CategoryName");
            }
        }
        
        public AddCatViewModel()
        {
            CategoryName = "";
            AddCatCommand = new RelayCommand(AddCatExecute, AddCatCanExecute);
            CancelCommand = new RelayCommand(CancelExecute);
        }

        public RelayCommand AddCatCommand { get; set; }

        private void AddCatExecute()
        {
            Categories category = DbContex.GetDbContex().Categories.Add(new Categories { Cat_name = CategoryName });
            DbContex.GetDbContex().SaveChanges();
            ViewsContainer.Show("Категория успешно создана!");
            CancelExecute();
        }

        private bool AddCatCanExecute()
        {
            return !String.IsNullOrWhiteSpace(CategoryName);
        }

        public RelayCommand CancelCommand { get; set; }

        private void CancelExecute()
        {
            ViewsContainer.AddViewWin.Close();
        }
    }
}
