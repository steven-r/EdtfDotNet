using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L0DateAndTime {

        [Test] public void TestL0DateTime1() {
            const string DateString = "2001-02-03T09:30:01";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2001, TestDate.StartValue.Year.Value);
            Assert.AreEqual(2, TestDate.StartValue.Month.Value);
            Assert.AreEqual(3, TestDate.StartValue.Day.Value);
            Assert.AreEqual(9, TestDate.StartValue.Hour);
            Assert.AreEqual(30, TestDate.StartValue.Minute);
            Assert.AreEqual(1, TestDate.StartValue.Second);
            Assert.AreEqual(0, TestDate.StartValue.TimeZoneOffset);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0DateTime2() {
            const string DateString = "2004-01-01T10:10:10Z";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(1, TestDate.StartValue.Month.Value);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);
            Assert.AreEqual(10, TestDate.StartValue.Hour);
            Assert.AreEqual(10, TestDate.StartValue.Minute);
            Assert.AreEqual(10, TestDate.StartValue.Second);
            Assert.AreEqual(0, TestDate.StartValue.TimeZoneOffset);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0DateTime3() {
            const string DateString = "2004-01-01T10:10:10+05:00";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(1, TestDate.StartValue.Month.Value);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);
            Assert.AreEqual(10, TestDate.StartValue.Hour);
            Assert.AreEqual(10, TestDate.StartValue.Minute);
            Assert.AreEqual(10, TestDate.StartValue.Second);
            Assert.AreEqual(300, TestDate.StartValue.TimeZoneOffset);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }
    }
}