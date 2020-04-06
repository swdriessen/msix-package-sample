using System;
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
using Windows.ApplicationModel;
using PackageSample.Library;

namespace PackageSample.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string[] args)
        {
            InitializeComponent();



            this.Title = $"{LibraryHelper.GetPackageInfo().Name ?? "Package Demo"} - v{LibraryHelper.GetPackageInfo().Version ?? "1.0.0"}";

            txtUIVersion.Text = $"{Assembly.GetExecutingAssembly().GetName().Version}";

            txtLibraryVersion.Text = $"{LibraryHelper.GetVersion()}";

            txtPackageVersion.Text = LibraryHelper.GetPackageInfo().Version ?? "N/A";

            if (args.Any())
                txtArgs.Text = string.Join(Environment.NewLine, args);
            else
                txtArgs.Text = "N/A";
        }

        private void BrowsePackageClick(object sender, RoutedEventArgs e)
        {
            var url = "https://downloads.swdriessen.nl/msix";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        private void BrowseRepositoryClick(object sender, RoutedEventArgs e)
        {
            var url = "https://github.com/swdriessen/msix-package-sample";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await LibraryHelper.Check();

                if (result.Availability == PackageUpdateAvailability.Available)
                    MessageBox.Show("Restart the application to apply the updates.", "Update Available!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
