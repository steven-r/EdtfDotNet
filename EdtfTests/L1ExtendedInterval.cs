using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L1ExtendedInterval {

        [Test] public void TestL1ExtendedInterval1() {
            const string DateString = "unknown/2006";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2006, TestDate.EndValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.EndValue.Month.HasValue);
            Assert.AreEqual(DateStatus.Unknown, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval2() {
            const string DateString = "2004-06-01/unknown";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.EndValue.Month.HasValue);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unknown, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval3() {
            const string DateString = "2004-01-01/open";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.EndValue.Month.HasValue);
            Assert.AreEqual(1, TestDate.StartValue.Month.Value);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Open, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval4() {
            const string DateString = "1984~/2004-06";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(2004, TestDate.EndValue.Year.Value);
            Assert.AreEqual(true, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.EndValue.Month.HasValue);
            Assert.AreEqual(6, TestDate.EndValue.Month.Value);
            Assert.AreEqual(0, TestDate.EndValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval5() {
            const string DateString = "1984/2004-06~";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(2004, TestDate.EndValue.Year.Value);
            Assert.AreEqual(true, TestDate.EndValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.EndValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.EndValue.Month.HasValue);
            Assert.AreEqual(6, TestDate.EndValue.Month.Value);
            Assert.AreEqual(0, TestDate.EndValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval6() {
            const string DateString = "1984~/2004~";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(2004, TestDate.EndValue.Year.Value);
            Assert.AreEqual(true, TestDate.EndValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.EndValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval7() {
            const string DateString = "1984?/2004?~";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(2004, TestDate.EndValue.Year.Value);
            Assert.AreEqual(true, TestDate.EndValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.EndValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.EndValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval8() {
            const string DateString = "1984-06?/2004-08?";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(2004, TestDate.EndValue.Year.Value);
            Assert.AreEqual(true, TestDate.EndValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.EndValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.EndValue.Month.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(true, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval9() {
            const string DateString = "1984-06-02?/2004-08-08~";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(2004, TestDate.EndValue.Year.Value);
            Assert.AreEqual(8, TestDate.EndValue.Month.Value);
            Assert.AreEqual(8, TestDate.EndValue.Day.Value);
            Assert.AreEqual(true, TestDate.EndValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.EndValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.EndValue.Day.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1ExtendedInterval10() {
            const string DateString = "1984-06-02?/unknown";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1984, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(0, TestDate.EndValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(2, TestDate.StartValue.Day.Value);
            Assert.AreEqual(false, TestDate.EndValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.EndValue.Year.IsUncertain);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unknown, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}