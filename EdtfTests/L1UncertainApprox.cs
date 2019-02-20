using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L1UncertainApprox {

        [Test] public void TestL1Uncertain1() {
            const string DateString = "?1984";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Uncertain2() {
            const string DateString = "2004-06?";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(0, TestDate.StartValue.Month.UnspecifiedMask);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Uncertain3() {
            const string DateString = "2004-06-11?";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Approx1() {
            const string DateString = "~1984";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Approx2() {
            const string DateString = "%1984";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}