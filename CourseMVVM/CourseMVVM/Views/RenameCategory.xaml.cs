﻿using CourseMVVM.ViewModels;
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
    /// Interaction logic for RenameCategory.xaml
    /// </summary>
    public partial class RenameCategory : Window
    {
        public RenameCategory(int catID)
        {
            InitializeComponent();
            DataContext = new RenameCategoryViewModel(catID);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
