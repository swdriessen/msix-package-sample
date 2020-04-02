using System;
using System.Reflection;

namespace PackageSample.Library
{
    public static class LibraryHelper
    {
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
