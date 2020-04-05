using System;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace PackageSample.Library
{
    public static class LibraryHelper
    {
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static PackageInfo GetPackageInfo()
        {
            try
            {
                return new PackageInfo()
                {
                    IsPackaged = true,
                    Version = Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor + "." + Package.Current.Id.Version.Build + "." + Package.Current.Id.Version.Revision,
                    Name = Package.Current.DisplayName,
                    AppInstallerUri = Package.Current.GetAppInstallerInfo()?.Uri.ToString()
                };
            }
            catch (InvalidOperationException)
            {
                // the app is not running from the package, return and empty info
                return new PackageInfo();
            }
        }

        public async static Task<PackageUpdateAvailabilityResult> Check()
        {
            return await Package.Current.CheckUpdateAvailabilityAsync();
        }
    }

    public class PackageInfo
    {
        public bool IsPackaged { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string AppInstallerUri { get; set; }
    }
}
