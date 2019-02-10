using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L1Season {

        [Test] public void TestL1Season1() {
            const string DateString = "2001-21";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2001, TestDate.StartValue.Year.Value);
            Assert.AreEqual(Edtf.Seasons.Spring, TestDate.StartValue.Month.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Season2() {
            const string DateString = "2003-22";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2003, TestDate.StartValue.Year.Value);
            Assert.AreEqual(Edtf.Seasons.Summer, TestDate.StartValue.Month.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Season3() {
            const string DateString = "2000-23";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2000, TestDate.StartValue.Year.Value);
            Assert.AreEqual(Edtf.Seasons.Autumn, TestDate.StartValue.Month.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL1Season4() {
            const string DateString = "2010-24";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2010, TestDate.StartValue.Year.Value);
            Assert.AreEqual(Edtf.Seasons.Winter, TestDate.StartValue.Month.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}