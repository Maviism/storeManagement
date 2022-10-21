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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for ManageStockView.xaml
    /// </summary>
    public partial class ManageStockView : UserControl
    {
        public ManageStockView()
        {
            InitializeComponent();
        }

        private void input_modal(object sender, RoutedEventArgs e)
        {
            InputModalView inputModal = new InputModalView();
            inputModal.ShowDialog();
        }


    }
}