using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test, Ignore("Test for #12")]
        public void TestStatusAndPartStatusSync()
        {
            const string dateString = "X199";
            var testDate = Edtf.DatePair.Parse(dateString);
            Assert.IsTrue(testDate.StartValue.Year.Invalid);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }
    }
}