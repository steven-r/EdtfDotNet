using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L1Unspecified {

        [Test] public void TestL1Unspecified1() {
            const string DateString = "199X";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(0001, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(1990, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Unspecified2() {
            const string DateString = "19XX";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(0011, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(1900, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Unspecified3() {
            const string DateString = "1999-XX";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1999, TestDate.StartValue.Year.Value);
            Assert.AreEqual(0, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(11, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(0, TestDate.StartValue.Month.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Unspecified4() {
            const string DateString = "1999-01-XX";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1999, TestDate.StartValue.Year.Value);
            Assert.AreEqual(0, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(0, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(11, TestDate.StartValue.Day.UnspecifiedMask);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(1, TestDate.StartValue.Month.Value);
            Assert.AreEqual(0, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Unspecified5() {
            const string DateString = "1999-XX-XX";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1999, TestDate.StartValue.Year.Value);
            Assert.AreEqual(0, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(11, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(11, TestDate.StartValue.Day.UnspecifiedMask);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(0, TestDate.StartValue.Month.Value);
            Assert.AreEqual(0, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test]
        public void TestL1YearUnspecifiedMiddleError()
        {
            const string dateString = "1X99";
            var testDate = Edtf.DatePair.Parse(dateString);
            Assert.IsTrue(testDate.StartValue.Year.Invalid);
        }

        [Test]
        public void TestL1YearUnspecifiedError()
        {
            const string dateString = "X199";
            var testDate = Edtf.DatePair.Parse(dateString);
            Assert.IsTrue(testDate.StartValue.Year.Invalid);
        }

        [Test]
        public void TestL1MonthUnspecifiedError()
        {
            const string dateString = "2199-X0";
            var testDate = Edtf.DatePair.Parse(dateString);
            Assert.IsFalse(testDate.StartValue.Year.Invalid);
            Assert.IsTrue(testDate.StartValue.Month.Invalid);
            Assert.IsFalse(testDate.StartValue.Day.Invalid);
        }

        [Test]
        public void TestL1DayUnspecifiedError()
        {
            const string dateString = "2199-10-X0";
            var testDate = Edtf.DatePair.Parse(dateString);
            Assert.IsFalse(testDate.StartValue.Year.Invalid);
            Assert.IsFalse(testDate.StartValue.Month.Invalid);
            Assert.IsTrue(testDate.StartValue.Day.Invalid);
        }

    }
}