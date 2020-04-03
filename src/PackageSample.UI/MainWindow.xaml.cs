﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using PackageSample.Library;

namespace PackageSample.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



            this.Title = $"{LibraryHelper.GetPackageInfo().Name ?? "Package Demo"} - v{LibraryHelper.GetPackageInfo().Version ?? "1.0.0"}";

            txtUIVersion.Text = $"{Assembly.GetExecutingAssembly().GetName().Version}";

            txtLibraryVersion.Text = $"{LibraryHelper.GetVersion()}";

            txtPackageVersion.Text = LibraryHelper.GetPackageInfo().Version ?? "N/A";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://downloads.swdriessen.nl/msix");
        }
    }
}
