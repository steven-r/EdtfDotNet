using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L0Interval {

        [Test] public void TestL0Interval1() {
            const string DateString = "1964/2008";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(1964, TestDate.StartValue.Year.Value);	
            Assert.AreEqual(2008, TestDate.EndValue.Year.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(false, TestDate.IsRange);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0Interval2() {
            const string DateString = "2004-06/2006-08";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);	
            Assert.AreEqual(2006, TestDate.EndValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);	
            Assert.AreEqual(8, TestDate.EndValue.Month.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(false, TestDate.IsRange);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0Interval3() {
            const string DateString = "2004-02-01/2005-02-08";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);	
            Assert.AreEqual(2005, TestDate.EndValue.Year.Value);
            Assert.AreEqual(2, TestDate.StartValue.Month.Value);	
            Assert.AreEqual(2, TestDate.EndValue.Month.Value);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);	
            Assert.AreEqual(8, TestDate.EndValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(false, TestDate.IsRange);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0Interval4() {
            const string DateString = "2004-02-01/2005-02";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);	
            Assert.AreEqual(2005, TestDate.EndValue.Year.Value);
            Assert.AreEqual(2, TestDate.StartValue.Month.Value);	
            Assert.AreEqual(2, TestDate.EndValue.Month.Value);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);	
            Assert.AreEqual(false, TestDate.EndValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(false, TestDate.IsRange);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0Interval5() {
            const string DateString = "2004-02-01/2005";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);	
            Assert.AreEqual(2005, TestDate.EndValue.Year.Value);
            Assert.AreEqual(2, TestDate.StartValue.Month.Value);	
            Assert.AreEqual(false, TestDate.EndValue.Month.HasValue);
            Assert.AreEqual(1, TestDate.StartValue.Day.Value);	
            Assert.AreEqual(false, TestDate.EndValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(false, TestDate.IsRange);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0Interval6() {
            const string DateString = "2005/2006-02";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2005, TestDate.StartValue.Year.Value);	
            Assert.AreEqual(2006, TestDate.EndValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);	
            Assert.AreEqual(2, TestDate.EndValue.Month.Value);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);	
            Assert.AreEqual(false, TestDate.EndValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Normal, TestDate.EndValue.Status);
            Assert.AreEqual(false, TestDate.IsRange);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

    }
}