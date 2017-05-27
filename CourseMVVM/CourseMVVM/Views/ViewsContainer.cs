using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMVVM.Views
{
    public class ViewsContainer
    {
        public static MainWindow LoginView { get; set; }
        public static Registration RegistrationView { get; set; }
        public static PasswordRestore PasswordRestoreView { get; set; }
        public static StudentMain StudentMainView { get; set; }
        public static LectorMain LectorMainView { get; set; }
        public static Test TestWin { get; set; }
        public static ResultsView ResultsWin { get; set; }
        public static CategoryView CategoryWin { get; set; }
        public static AddCatView AddViewWin { get; set; }
        public static AddQuestView AddQuestWin { get; set; }
        public static RenameCategory RenameCategoryWin { get; set; }

        public ViewsContainer()
        {
            LoginView = new MainWindow();
        }
    }
}
