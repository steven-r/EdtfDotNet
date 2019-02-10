using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L2PartialUnspecified {

        [Test] public void TestL2PartialUnspecified1() {
            const string DateString = "156X-12-25";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1560, TestDate.StartValue.Year.Value);
            Assert.AreEqual(12, TestDate.StartValue.Month.Value);
            Assert.AreEqual(25, TestDate.StartValue.Day.Value);
            Assert.AreEqual(0001, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(0, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(0, TestDate.StartValue.Day.UnspecifiedMask);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUnspecified2() {
            const string DateString = "15XX-12-25";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1500, TestDate.StartValue.Year.Value);
            Assert.AreEqual(12, TestDate.StartValue.Month.Value);
            Assert.AreEqual(25, TestDate.StartValue.Day.Value);
            Assert.AreEqual(0011, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(0, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(0, TestDate.StartValue.Day.UnspecifiedMask);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUnspecified3() {
            const string DateString = "15XX-12-XX";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1500, TestDate.StartValue.Year.Value);
            Assert.AreEqual(12, TestDate.StartValue.Month.Value);
            Assert.AreEqual(0, TestDate.StartValue.Day.Value);
            Assert.AreEqual(0011, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(0, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(11, TestDate.StartValue.Day.UnspecifiedMask);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUnspecified4() {
            const string DateString = "1560-XX-25";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1560, TestDate.StartValue.Year.Value);
            Assert.AreEqual(0, TestDate.StartValue.Month.Value);
            Assert.AreEqual(25, TestDate.StartValue.Day.Value);
            Assert.AreEqual(0000, TestDate.StartValue.Year.UnspecifiedMask);
            Assert.AreEqual(11, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(00, TestDate.StartValue.Day.UnspecifiedMask);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}