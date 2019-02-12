using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L2MaskedPrecision {

        [Test] public void TestL2MaskedPrecisionRemoved() {
            const string dateString = "196x";
            var date = Edtf.DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, date.StartValue.Status);
        }
    }
}