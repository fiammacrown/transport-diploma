﻿using System.Collections.Generic;
using System.Windows;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.ViewModels;

namespace Abeslamidze_Kursovaya7
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {

            InitializeComponent();

            DataContext = ViewModel = new RegisterWindowViewModel()
            {
                CloseDelegate = (order) =>
                {
                    DataResult = order;
                    DialogResult = true;
                    Close();
                }
            };
        }

        public RegisterWindowViewModel ViewModel { get; }

        public Order? DataResult { get; private set; }
    }
}
