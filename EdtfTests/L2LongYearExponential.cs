//
// Enums.cs
//
// Author:
//       Richard S. Tallent, II <richard@tallent.us>
//
// Copyright (c) 2014 Richard S. Tallent, II
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using NUnit.Framework;
using Edtf;

namespace EdtfTests {
    [TestFixture()] public class L2LongYearExponential {

		[Test] public void TestL2LongYearExponential1() {
			const string DateString = "Y17E7";
			var TestDate = Edtf.DatePair.Parse(DateString);
			Assert.AreEqual(170000000, TestDate.StartValue.Year.Value);
			Assert.AreEqual(0, TestDate.StartValue.Month.Value);
			Assert.AreEqual(0, TestDate.StartValue.Day.Value);
			Assert.AreEqual(0, TestDate.StartValue.Year.UnspecifiedMask);
			Assert.AreEqual(0, TestDate.StartValue.Year.InsignificantDigits);
			Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
			Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
			Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
			Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
			Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
			//Assert.AreEqual(DateString, TestDate.ToString());
		}

		[Test] public void TestL2LongYearExponential2() {
			const string DateString = "Y-17E7";
			var TestDate = Edtf.DatePair.Parse(DateString);
			Assert.AreEqual(-170000000, TestDate.StartValue.Year.Value);
			Assert.AreEqual(0, TestDate.StartValue.Month.Value);
			Assert.AreEqual(0, TestDate.StartValue.Day.Value);
			Assert.AreEqual(0, TestDate.StartValue.Year.UnspecifiedMask);
			Assert.AreEqual(0, TestDate.StartValue.Year.InsignificantDigits);
			Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
			Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
			Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
			Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
			Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
			//Assert.AreEqual(DateString, TestDate.ToString());
		}

		[Test] public void TestL2LongYearExponential3() {
			const string DateString = "Y17101E4S3";
			var TestDate = Edtf.DatePair.Parse(DateString);
			Assert.AreEqual(171010000, TestDate.StartValue.Year.Value);
			Assert.AreEqual(0, TestDate.StartValue.Month.Value);
			Assert.AreEqual(0, TestDate.StartValue.Day.Value);
			Assert.AreEqual(0, TestDate.StartValue.Year.UnspecifiedMask);
			Assert.AreEqual(6, TestDate.StartValue.Year.InsignificantDigits);
			Assert.AreEqual(true, TestDate.StartValue.Year.HasValue);
			Assert.AreEqual(false, TestDate.StartValue.Month.HasValue);
			Assert.AreEqual(false, TestDate.StartValue.Day.HasValue);
			Assert.AreEqual(DateStatus.Normal, TestDate.StartValue.Status);
			Assert.AreEqual(DateStatus.Unused, TestDate.EndValue.Status);
			//Assert.AreEqual(DateString, TestDate.ToString());
		}

	}

}