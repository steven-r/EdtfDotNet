using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L2MaskedPrecision {

        [Test] public void TestL2MaskedPrecision1() {
            const string DateString = "196x";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1960, TestDate.StartValue.Year.Value);
            Assert.AreEqual(0, TestDate.StartValue.Month.Value);
            Assert.AreEqual(0, TestDate.StartValue.Day.Value);
            Assert.AreEqual(0000, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(1, TestDate.StartValue.Year.InsignificantDigits);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2MaskedPrecision2() {
            const string DateString = "19xx";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1900, TestDate.StartValue.Year.Value);
            Assert.AreEqual(0, TestDate.StartValue.Month.Value);
            Assert.AreEqual(0, TestDate.StartValue.Day.Value);
            Assert.AreEqual(0000, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(2, TestDate.StartValue.Year.InsignificantDigits);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}