using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackageSample.Library;

namespace PackageSample.Tests
{
    [TestClass]
    public class PackageSampleUnitTest
    {
        [TestMethod]
        public void LibraryHelperTest()
        {
            var version = LibraryHelper.GetVersion();

            Assert.IsFalse(string.IsNullOrWhiteSpace(version));
        }
    }
}
