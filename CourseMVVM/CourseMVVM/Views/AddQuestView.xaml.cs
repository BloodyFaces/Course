using CourseMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseMVVM
{
    /// <summary>
    /// Interaction logic for AddQuestView.xaml
    /// </summary>
    public partial class AddQuestView : Window
    {
        public AddQuestView(int category)
        {
            InitializeComponent();
            DataContext = new AddQuestViewModel(category);
        }
    }
}
