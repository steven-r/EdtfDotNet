using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L1LongYear {

        [Test] public void TestL1LongYear1() {
            const string DateString = "y170000002";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(170000002, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1LongYear2() {
            const string DateString = "y-170000002";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(-170000002, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}