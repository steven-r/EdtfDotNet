using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void TestStatusAndPartStatusSync()
        {
            const string dateString = "X199";
            var testDate = DatePair.Parse(dateString);
            Assert.IsTrue(testDate.StartValue.Year.Invalid);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

        [Test]
        public void TestStatusMonthInvalid()
        {
            const string dateString = "2199-13";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

        [Test]
        public void TestStatusDayInvalid()
        {
            const string dateString = "2199-02-30";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

        [Test]
        public void TestStatusDayInvalidLeap()
        {
            const string dateString = "2019-02-29";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

        [Test]
        public void TestStatusTimeInvalid()
        {
            const string dateString = "2199-01-01T25:61:60";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

        [Test]
        public void TestStatusHourInvalid()
        {
            const string dateString = "2199-01-01T25:00:00";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

        [Test]
        public void TestStatusMinuteInvalid()
        {
            const string dateString = "2199-01-01T23:61:00";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }


        [Test]
        public void TestStatusSecsInvalid()
        {
            const string dateString = "2199-01-01T23:00:60";
            var testDate = DatePair.Parse(dateString);
            Assert.AreEqual(DateStatus.Invalid, testDate.StartValue.Status);
        }

    }
}