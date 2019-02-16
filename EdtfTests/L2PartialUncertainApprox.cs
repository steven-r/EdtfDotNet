using Edtf;
using NUnit.Framework;

namespace EdtfTests
{
    [TestFixture()] public class L2PartialUncertainApprox {

        [Test] public void TestL2PartialUncertainApprox1() {
            const string DateString = "?2004-06-11";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUncertainApprox2() {
            const string DateString = "2004-06~-11";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUncertainApprox3() {
            const string DateString = "2004-?06-11";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(TestDate, Edtf.DatePair.Parse(TestDate.ToString()));	// Grouping comes out differently on output, compare the results of reparsing
        }

        [Test] public void TestL2PartialUncertainApprox4() {
            const string DateString = "2004-06-~11";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUncertainApprox5() {
            const string DateString = "2004-?~06";
            const string expected = "2004-%06";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(expected, TestDate.ToString());	
        }

        [Test] public void TestL2PartialUncertainApprox6() {
            const string DateString = "2004-?06-?11";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(TestDate, Edtf.DatePair.Parse(TestDate.ToString()));	// Grouping comes out differently on output, compare the results of reparsing
        }

        [Test] public void TestL2PartialUncertainApprox7() {
            const string DateString = "?2004-06-~11";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(11, TestDate.StartValue.Day.Value);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUncertainApprox8() {
            const string DateString = "2004-~06?";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(TestDate, Edtf.DatePair.Parse(TestDate.ToString()));	// Grouping comes out differently on output, compare the results of reparsing
        }

        [Test] public void TestL2PartialUncertainApprox9() {
            const string DateString = "?2004?-%06";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(TestDate, Edtf.DatePair.Parse(TestDate.ToString()));	// Grouping comes out differently on output, compare the results of reparsing
        }

        [Test] public void TestL2PartialUncertainApprox10() {
            const string DateString = "?2004-~06-~04";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2004, TestDate.StartValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(4, TestDate.StartValue.Day.Value);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Month.IsUncertain);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsUncertain);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUncertainApprox11() {
            const string DateString = "2011-~06-~04";
            var TestDate = Edtf.DatePair.Parse(DateString);
            // values
            Assert.AreEqual(2011, TestDate.StartValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(4, TestDate.StartValue.Day.Value);
            // flags
            Assert.AreEqual(true, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            // status and ToString()
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        [Test] public void TestL2PartialUncertainApprox12() {
            const string DateString = "2011-~06-~04";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2011, TestDate.StartValue.Year.Value);
            Assert.AreEqual(6, TestDate.StartValue.Month.Value);
            Assert.AreEqual(4, TestDate.StartValue.Day.Value);
            Assert.AreEqual(true, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(TestDate, Edtf.DatePair.Parse(TestDate.ToString()));	// Grouping comes out differently on output, compare the results of reparsing
        }

        [Test] public void TestL2PartialUncertainApprox13() {
            const string DateString = "2011-23~";
            var TestDate = Edtf.DatePair.Parse(DateString);
            Assert.AreEqual(2011, TestDate.StartValue.Year.Value);
            Assert.AreEqual(Seasons.Autumn, TestDate.StartValue.Month.Value);
            Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
            Assert.AreEqual(true, TestDate.StartValue.Month.IsApproximate);
            Assert.AreEqual(true, TestDate.StartValue.Year.IsApproximate);
            Assert.AreEqual(false, TestDate.StartValue.Day.IsApproximate);
            Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
            Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
            Assert.AreEqual(DateString, TestDate.ToString());
        }

        // TODO
    }
}