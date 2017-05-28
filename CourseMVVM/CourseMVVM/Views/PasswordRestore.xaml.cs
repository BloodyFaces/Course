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

namespace CourseMVVM.Views
{
    /// <summary>
    /// Interaction logic for PasswordRestore.xaml
    /// </summary>
    public partial class PasswordRestore : Window
    {
        public PasswordRestore()
        {
            InitializeComponent();
            DataContext = new PasswordRestoreViewModel();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
