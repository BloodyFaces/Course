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
using CourseMVVM.ViewModels;

namespace CourseMVVM.Views
{
    /// <summary>
    /// Interaction logic for EditQuestionView.xaml
    /// </summary>
    public partial class EditQuestionView : Window
    {
        public EditQuestionView(int category, CatQuestions catQuest, CatAnswers catAnswer)
        {
            InitializeComponent();
            DataContext = new EditQuestViewModel(category, catQuest, catAnswer);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
