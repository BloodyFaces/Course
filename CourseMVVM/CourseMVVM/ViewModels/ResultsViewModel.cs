using CourseMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CourseMVVM.Commands;
using CourseMVVM.Views;

namespace CourseMVVM.ViewModels
{
    public class ResultsViewModel: ObservableObject
    {
        private ObservableCollection<ResultsData> _list;

        public ObservableCollection<ResultsData> List
        {
            get { return _list; }
            set 
            { 
                _list = value;
                OnPropertyChanged("List");
            }
        }
        
        public ResultsViewModel(List<ResultsData> list)
        {
            _list = new ObservableCollection<ResultsData>(list);
            CloseCommand = new RelayCommand(CloseExecute);
        }
        public RelayCommand CloseCommand { get; set; }

        private void CloseExecute()
        {
            ViewsContainer.ResultsWin.Close();
        }
    }
}
