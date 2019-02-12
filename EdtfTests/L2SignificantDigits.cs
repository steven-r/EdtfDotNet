using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture]
    public class L2SignificantDigits
    {
        [Test]
        public void TestL2SignificantDigitsYearWithoutY()
        {
            const string dateString = "1960S2";
            var date = Edtf.DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Normal, date.StartValue.Status);
            Assert.IsFalse(date.StartValue.Year.Invalid);
            Assert.IsTrue(date.StartValue.Year.HasValue);
            Assert.AreEqual(1960, date.StartValue.Year.Value);
            Assert.AreEqual(2, date.StartValue.Year.SignificantDigits);
            Assert.AreEqual(dateString, date.ToString());
        }

        [Test]
        public void TestL2SignificantDigitsYearWithYWithoutE()
        {
            const string dateString = "Y196000002S2";
            var date = Edtf.DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Normal, date.StartValue.Status);
            Assert.IsFalse(date.StartValue.Year.Invalid);
            Assert.IsTrue(date.StartValue.Year.HasValue);
            Assert.AreEqual(196000002, date.StartValue.Year.Value);
            Assert.AreEqual(2, date.StartValue.Year.SignificantDigits);
            Assert.AreEqual(dateString, date.ToString());
        }

        [Test]
        public void TestL2SignificantDigitsYearWithYWithE()
        {
            const string dateString = "Y1960E2S2";
            var date = Edtf.DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Normal, date.StartValue.Status);
            Assert.IsFalse(date.StartValue.Year.Invalid);
            Assert.IsTrue(date.StartValue.Year.HasValue);
            Assert.AreEqual(196000, date.StartValue.Year.Value);
            Assert.AreEqual(2, date.StartValue.Year.SignificantDigits);
            Assert.AreEqual("Y196000S2", date.ToString());
        }


        [Test]
        public void TestL2SignificantDigitsYearUnspecific()
        {
            const string dateString = "Y196XXXS2";
            var date = Edtf.DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Normal, date.StartValue.Status);
            Assert.IsFalse(date.StartValue.Year.Invalid);
            Assert.IsTrue(date.StartValue.Year.HasValue);
            Assert.AreEqual(196000, date.StartValue.Year.Value);
            Assert.AreEqual(111, date.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(2, date.StartValue.Year.SignificantDigits);
            Assert.AreEqual(dateString, date.ToString());
        }

        [Test]
        public void TestL2SignificantDigitsErrorTooLarge()
        {
            const string dateString = "Y1960S5";
            var date = Edtf.DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Normal, date.StartValue.Status);
            Assert.IsTrue(date.StartValue.Year.Invalid);
        }
    }
}