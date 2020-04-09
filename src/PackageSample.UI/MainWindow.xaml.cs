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
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
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


            try
            {
                var test = Package.Current;
            }
            catch (InvalidOperationException e)
            {

            }


            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (!localSettings.Values.ContainsKey("intro"))
            {
                var intro = new IntroWindow();
                intro.ShowDialog();
            }
            

            txtSetting1.Text = localSettings.Values.ContainsKey("setting1") ? localSettings.Values["setting1"].ToString() : "";
            txtSetting2.Text = localSettings.Values.ContainsKey("setting2") ? localSettings.Values["setting2"].ToString() : "";

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
                {
                    var loc = Package.Current.InstalledLocation;

                    Toast("Restart the application to apply the updates.", "Update Available!");
                }
                else
                {
                    var loc = Package.Current.InstalledLocation;
                    //var package = Package.Current;
                    var packageStatus = Package.Current.Status;

                    var effLoc = Package.Current.EffectiveLocation.Path;


                    Toast("No updates available.");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Toast(string message, string title = "Aurora")
        {
            try
            {
                string timeString = $"{DateTime.Now:HH:mm:ss}";
                string image = Package.Current.Logo.ToString();//"https://avatars3.githubusercontent.com/u/34661065?s=200&v=4";


                //<image src='{image}'/>
                string toastXmlString =
                    $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>{title}</text>
            <text>{message}</text>
            
            </binding>
        </visual></toast>";


                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(toastXmlString);

                var toastNotification = new ToastNotification(xmlDoc);

                var toastNotifier = ToastNotificationManager.CreateToastNotifier();
                toastNotifier.Show(toastNotification);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SampleToastClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var title = "Unearthed Arcana: Subclasses, Part 3";
                var message = "Three new subclasses for you to playtest: the Armorer for the artificer, the Circle of the Stars for the druid, and the Fey Wanderer for the ranger.";
                var message2 = "Aurora";
                var productUrl = "https://aurorabuilder.com/wp-content/uploads/2019/07/unearthed-arcana-banner-1024x267.jpg";

                string timeString = $"{DateTime.Now:HH:mm:ss}";
                string image = productUrl; //Package.Current.Logo.ToString();//"https://avatars3.githubusercontent.com/u/34661065?s=200&v=4";


                

                string toastXmlString =
                    $@"<toast launch='action=toastAction&amp;id=1&amp;id2=2'><visual>
                               <binding template='ToastGeneric'>
                            <text>{title}</text>
                            <text>{message}</text>
                            <text placement='attribution'>{message2}</text>
                            <image src='{productUrl}' placement='hero'/>                        
                        </binding>
                       </visual></toast>";

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(toastXmlString);

                var toastNotification = new ToastNotification(xmlDoc);

                var toastNotifier = ToastNotificationManager.CreateToastNotifier();
                toastNotifier.Show(toastNotification);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static XmlDocument CreateToast()
        {
            var xDoc = new XDocument(
                new XElement("toast",
                    new XElement("visual",
                        new XElement("binding", new XAttribute("template", "ToastGeneric"),
                            new XElement("text", "C# Corner"),
                            new XElement("text", "Do you got MVP award?")
                        )
                    ),// actions    
                    new XElement("actions",
                        new XElement("action", new XAttribute("activationType", "background"),
                            new XAttribute("content", "Yes"), new XAttribute("arguments", "yes")),
                        new XElement("action", new XAttribute("activationType", "background"),
                            new XAttribute("content", "No"), new XAttribute("arguments", "no"))
                    )
                )
            );

            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            return xmlDoc;
        }

        private void Action1Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["setting1"] = null;

            txtSetting1.Text = "";
        }
        private void Action2Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["setting2"] = null;


            
            txtSetting2.Text = "";
        }

        private void ClearSettingsClick(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values.Clear();

        }

        private void CefClick(object sender, RoutedEventArgs e)
        {
            var cef = new CEFWindow();
            cef.Show();
        }
        private void IslandsClick(object sender, RoutedEventArgs e)
        {
            var window = new Islands();
            window.Show();
        }
    }
}
