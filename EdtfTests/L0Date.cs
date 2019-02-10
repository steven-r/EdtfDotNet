using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    /// <summary>
    /// This test suite uses the examples in the Features table of the EDTF 1.0 Draft Submission, available here:
    /// http://www.loc.gov/standards/datetime/pre-submission.html#table
    /// </summary>

    // Level 0. ISO 8601 Features

    [TestFixture()] public class L0Date {

        [Test] public void TestL0DateComplete() {
            const string DateString = "2001-02-03";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2001, TestDate.StartValue.Year.Value);
            Assert.AreEqual(2, TestDate.StartValue.Month.Value);
            Assert.AreEqual(3, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0DateMonth() {
            const string DateString = "2008-12";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2008, TestDate.StartValue.Year.Value);
            Assert.AreEqual(12, TestDate.StartValue.Month.Value);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0DateYear() {
            const string DateString = "2008";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2008, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0DateNegativeYear() {
            const string DateString = "-0999";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(-999, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL0DateYear0() {
            const string DateString = "0000";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(0, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }
    }
}