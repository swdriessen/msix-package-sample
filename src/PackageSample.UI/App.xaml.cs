﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PackageSample.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Any())
            {
                //MessageBox.Show(string.Join(", ", e.Args), "Arguments Provided");
            }



            this.MainWindow = new MainWindow(e.Args);
            this.MainWindow.Show();
        }
    }
}
