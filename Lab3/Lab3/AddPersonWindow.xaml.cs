﻿using System;
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

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddPersonWindow: Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
        }

        private void ClickOK(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
