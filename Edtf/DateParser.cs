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

using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Edtf {

	/// <summary>
	/// This static class contains a single public method for parsing strings into Dates. It is handled
	/// separately because it is rather long. This could be done using a partial class instead.
	/// </summary>
	public static class DateParser {

		/// <summary>
		/// This class is merely a convenient bucket for some logic and variables used
		/// by DateParser. It could be inlined for a small performance gain.
		/// </summary>
		private class ParenthesisTracker {
			public int YearsOpen { get; set; }
			public int YearsClosed { get; private set; }
			public int MonthsOpen { get; set; }
			public int MonthsClosed { get; private set; }
			public int DaysOpen { get; set; }
			public int DaysClosed { get; private set; }
			public bool JustClosedDayParen { get; set; }
			public bool JustClosedMonthParen { get; set; }
			public bool JustClosedYearParen { get; set; }
			public void Close() {
				JustClosedDayParen = false;
				JustClosedMonthParen = false;
				JustClosedYearParen = false;
				if(DaysOpen > DaysClosed) {
					DaysClosed++;
					JustClosedDayParen = true;
				} else if (MonthsOpen > MonthsClosed) {
					MonthsClosed++;
					JustClosedMonthParen = true;
				} else if (YearsOpen > YearsClosed) {
					YearsClosed++;
					JustClosedYearParen = true;
				}
			}
		}


		private static Regex _matcher;
		private static readonly object MatchLoadLocker = new object();

		private static Regex Matcher {
			get {
				if (_matcher == null) {
					lock (MatchLoadLocker) {
						if (_matcher == null) {
							var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Edtf.EdtfRegexPattern.txt");
                            if (stream == null) throw new InvalidOperationException("stream is null");
							string pattern;
							using (var reader = new System.IO.StreamReader (stream)) {
								pattern = reader.ReadToEnd ();
							}
							_matcher = new Regex(pattern, RegexOptions.Compiled);
						}
					}
				}
				return _matcher;
			}
		}

		public static Date Parse(string rawValue) {
			var result = new Date();

			if (String.IsNullOrEmpty(rawValue)) {
				result.Status = DateStatus.Unused;
				return result;
			}

			if (rawValue == SpecialValues.Open) {
				result.Status = DateStatus.Open;
				return result;
			}

			if (rawValue == SpecialValues.Unknown) {
				result.Status = DateStatus.Unknown;
				return result;
			}

			var re = Matcher;
			var m = re.Match(rawValue);
			if (!m.Success) {
				result.Status = DateStatus.Invalid;
				return result;
			}

			result.Status = DateStatus.Normal;
			var g = m.Groups;

			// Take the returned regular expression match and parse it into the various date/time bits,
			// validating as needed.

			result = ParseYear(g, result, out var yearFlagsVal);

            var monthVal = g["monthnum"].Value;
			if (string.IsNullOrEmpty(monthVal))
            {
                return result;
            }
			result.Month = DatePart.Parse(monthVal);
            if (result.Month.Invalid)
            {
                result.Status = DateStatus.Invalid;
            }
			// Keep a stack of open parenthesis and where they occurred. Also
			// keep a count of accounted-for ones (where the closing paren
			// has been reached).
			var parens = new ParenthesisTracker() { YearsOpen = g["yearopenparens"].Value.Length };
			{
				var yearsClosed = yearFlagsVal.Count(t => t == ')');
				for (int i = 0; i < yearsClosed; i++)
					parens.Close();
			}

			bool yearIsProtected = parens.JustClosedYearParen;
			bool yearGetsFlags = !yearIsProtected;

			parens.MonthsOpen = g["monthopenparens"].Value.Length;
			var monthFlagsVal = g["monthend"].Value;
			if (!string.IsNullOrEmpty(monthFlagsVal)) {
				parens.JustClosedYearParen = false;
				parens.JustClosedMonthParen = false;
				foreach (var c in monthFlagsVal) {
					switch (c) {
						case ')':
							parens.Close();
							yearGetsFlags = (!yearIsProtected) && (!parens.JustClosedMonthParen);
							break;
						case '~':
							result.Month.IsApproximate = true;
							result.Year.IsApproximate = result.Year.IsApproximate || yearGetsFlags;
							break;
						case '?':
							result.Month.IsUncertain = true;
							result.Year.IsUncertain = result.Year.IsUncertain || yearGetsFlags;
							break;
					}
				}
			}

			if (result.Month.Value >= 20) {
				result.SeasonQualifier = g["seasonqualifier"].Value;
				// There won't be a day or time, or if there is, it should be ignored
				return result;
			}

            if (result.Month.Value > 12)
            {
                result.Month.Invalid = true;
                result.Status = DateStatus.Invalid;
                return result;
            }

			var dayVal = g["daynum"].Value;
            if (string.IsNullOrEmpty(dayVal))
            {
                return result;
            }

			result.Day = DatePart.Parse(dayVal);
            if (result.Day.Invalid)
            {
                result.Status = DateStatus.Invalid;
                return result;
            }

			var dayFlagsVal = g["dayend"].Value;
			if (!string.IsNullOrEmpty(dayFlagsVal)) {
				parens.JustClosedYearParen = false;
				parens.JustClosedMonthParen = false;
				parens.DaysOpen = g["dayopenparens"].Value.Length;
				yearGetsFlags = !yearIsProtected;
				bool monthIsProtected = (parens.MonthsClosed > 0);
				bool monthGetsFlags = !monthIsProtected;
				foreach (var c in dayFlagsVal) {
					switch (c) {
						case ')':
							parens.Close();
							monthGetsFlags = (!monthIsProtected) && !parens.JustClosedDayParen;
							yearGetsFlags = (!yearIsProtected) && (!parens.JustClosedDayParen) && (!parens.JustClosedMonthParen); 
							break;
						case '~':
							result.Day.IsApproximate = true;
							result.Year.IsApproximate = result.Year.IsApproximate || yearGetsFlags;
							result.Month.IsApproximate = result.Month.IsApproximate || monthGetsFlags;
							break;
						case '?':
							result.Day.IsUncertain = true;
							result.Year.IsUncertain = result.Year.IsUncertain || yearGetsFlags;
							result.Month.IsUncertain = result.Month.IsUncertain || monthGetsFlags;
							break;
					}
				}
			}

			// TIME

			var hourVal = g["hour"].Value;
            if (string.IsNullOrEmpty(hourVal))
            {
                return result;
            }
			result.Hour = int.Parse(hourVal);
			result.Minute = int.Parse(g["minute"].Value);
			result.Second = int.Parse(g["second"].Value);

			// Time zone offset
			var tzSignValue = g["tzsign"].Value;
			if(!string.IsNullOrEmpty(g["tzutc"].Value)) {
				result.HasTimeZoneOffset = true;
			} else {
				if (!String.IsNullOrEmpty(tzSignValue)) {
					result.HasTimeZoneOffset = true;
					var tzSign = (tzSignValue == "-") ? -1 : 1;
					var tzHour = int.Parse(g["tzhour"].Value);
					var tzMinute = int.Parse(g["tzminute"].Value);
					result.TimeZoneOffset = tzSign * (tzHour * 60) + tzMinute;
				}
			}

			return result;

		}

        private static Date ParseYear(GroupCollection g, Date result, out string yearFlagsVal)
        {
            var yearVal = g["yearnum"].Value;
            yearFlagsVal = "";

            // A Year is required.
            if (string.IsNullOrEmpty(yearVal))
            {
                result.Year.Invalid = true;
                result.Status = DateStatus.Invalid;
                return result;
            }

            // Convert the year, this handles both normal and scientific notation
            // Only parse using a Double first if there is an exponent.
            result.Year = DatePart.Parse(yearVal);
            if (result.Year.Invalid)
            {
                result.Status = DateStatus.Invalid;
                return result;
            }

            var yearPrecision = g["yearprecision"].Value;
            if (!string.IsNullOrEmpty(yearPrecision))
            {
                // The part after "p" are the number of *significant* digits, so
                // to find the insignificant count, convert the value to a temp
                // string to get its length.
                // http://stackoverflow.com/questions/4483886/how-can-i-get-a-count-of-the-total-number-of-digits-in-a-number
                var totalDigits = Math.Floor(Math.Log10(result.Year.Value) + 1);
                var insigDigits = totalDigits - int.Parse(yearPrecision);
                result.Year.InsignificantDigits = (insigDigits < 0) ? (byte) 0 : (byte) insigDigits;
            }

            yearFlagsVal = g["yearend"].Value;
            if (!string.IsNullOrEmpty(yearFlagsVal))
            {
                result.Year.IsApproximate = yearFlagsVal.Contains('~');
                result.Year.IsUncertain = yearFlagsVal.Contains('?');
            }

            return result;
        }
    }
}