using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PackageSample.UI
{
    /// <summary>
    /// Interaction logic for IntroWindow.xaml
    /// </summary>
    public partial class IntroWindow : Window
    {
        public IntroWindow()
        {
            InitializeComponent();
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            txtSetting2.Text = localSettings.Values.ContainsKey("setting2") ? localSettings.Values["setting2"].ToString() : "";


            if (!localSettings.Values.ContainsKey("setting1"))
            {
                localSettings.Values["setting1"] = 0;
            }
            else
            {
                btnAction1.Content = $"{localSettings.Values["setting1"]}++";
            }


            if (!localSettings.Values.ContainsKey("setting2"))
            {
                localSettings.Values["setting2"] = "";
            }
        }

        private void Action1Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("setting1"))
            {
                var value = int.Parse(localSettings.Values["setting1"].ToString());
                value++;
                localSettings.Values["setting1"] = value;
            }
            else
            {
                localSettings.Values["setting1"] = 1;
            }

            
        }

        private void Action2Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Add("intro", 1);
            this.Close();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["setting2"] = (sender as TextBox).Text;
        }
    }
}
