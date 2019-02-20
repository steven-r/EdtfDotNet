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

        private static readonly int[] SmallMonths = {4, 6, 9, 11};

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

		public static Date Parse(string rawValue, bool hasInterval) {
			var result = new Date();

			if (string.IsNullOrEmpty(rawValue)) {
				result.Status = hasInterval ? DateStatus.Unknown : DateStatus.Unused;
				return result;
			}

			if (rawValue == SpecialValues.Open) {
				result.Status = DateStatus.Open;
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

            if (!ParseYear(g, ref result) || !ParseMonth(g, ref result))
            {
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

            if (!ValidateDay(result))
            {
                result.Status = DateStatus.Invalid;
                return result;
            }

            var openFlags = g["dayopenflags"].Value;
            var closeFlags = g["daycloseflags"].Value;

            SetDatePartFlags(openFlags, ref result.Day);
            SetDatePartFlags(closeFlags, ref result.Day);
            SetDatePartFlags(closeFlags, ref result.Month);
            SetDatePartFlags(closeFlags, ref result.Year);

			// TIME

			var hourVal = g["hour"].Value;
            if (string.IsNullOrEmpty(hourVal))
            {
                return result;
            }
			result.Hour = int.Parse(hourVal);
            ValidateRange(ref result, result.Hour, 0, 23);
			result.Minute = int.Parse(g["minute"].Value);
            ValidateRange(ref result, result.Minute, 0, 59);
			result.Second = int.Parse(g["second"].Value);
            ValidateRange(ref result, result.Second, 0, 59);

            if (DateStatus.Invalid == result.Status)
            {
                return result;
            }
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

        private static bool ParseMonth(GroupCollection g, ref Date result)
        {
            var monthVal = g["monthnum"].Value;
            if (string.IsNullOrEmpty(monthVal))
            {
                return false;
            }

            result.Month = DatePart.Parse(monthVal);
            if (result.Month.Invalid)
            {
                result.Status = DateStatus.Invalid;
            }

            var openFlags = g["monthopenflags"].Value;
            var closeFlags = g["monthcloseflags"].Value;

            SetDatePartFlags(openFlags, ref result.Month);
            SetDatePartFlags(closeFlags, ref result.Month);
            SetDatePartFlags(closeFlags, ref result.Year);

            if (result.Month.Value >= 20)
            {
                result.SeasonQualifier = g["seasonqualifier"].Value;
                // There won't be a day or time, or if there is, it should be ignored
                return false;
            }

            if (result.Month.Value > 12 || (result.Month.Value < 1 && result.Month.UnspecifiedMask == 0))
            {
                result.Month.Invalid = true;
                result.Status = DateStatus.Invalid;
                return false;
            }

            return true;
        }

        private static void ValidateRange(ref Date result, int value, int min, int max)
        {
            if (value < min || value > max)
            {
                result.Status = DateStatus.Invalid;
            }
        }

        private static bool ValidateDay(Date dateValue)
        {
            if (!dateValue.Day.HasValue || dateValue.Month.UnspecifiedMask != 0 || dateValue.Day.UnspecifiedMask != 0)
            {
                return true; // no date is a good date
            }

            if (dateValue.Day.Value < 1)
            {
                return false;
            }

            if (dateValue.Day.Value > 30 && SmallMonths.Contains(dateValue.Month.Value))
            {
                return false;
            }

            if (dateValue.Day.Value > 29 && dateValue.Month.Value == 2)
            {
                return false;
            }

            if (dateValue.Year.UnspecifiedMask != 0)
            {
                return true; // ignored on 'X'
            }

            var isLeap = dateValue.Year.Value % 4 == 0 && (dateValue.Year.Value % 100 != 0 || dateValue.Year.Value % 1000 == 0);
            if (!isLeap && dateValue.Day.Value > 28 && dateValue.Month.Value == 2)
            {
                return false;
            }

            return true;
        }

        private static bool ParseYear(GroupCollection g, ref Date result)
        {
            var yearVal = g["yearnum"].Value;

            // A Year is required.  as of regex this cannot happen
            //if (string.IsNullOrEmpty(yearVal))
            //{
            //    result.Year.Invalid = true;
            //    result.Status = DateStatus.Invalid;
            //    return false;
            //}

            // Convert the year, this handles both normal and scientific notation
            // Only parse using a Double first if there is an exponent.
            result.Year = DatePart.Parse(yearVal);
            if (result.Year.Invalid)
            {
                result.Status = DateStatus.Invalid;
                return false;
            }

            var yearPrecision = g["yearprecision"].Value;
            if (!string.IsNullOrEmpty(yearPrecision))
            {
                // The part after "p" are the number of *significant* digits, so
                // to find the insignificant count, convert the value to a temp
                // string to get its length.
                // http://stackoverflow.com/questions/4483886/how-can-i-get-a-count-of-the-total-number-of-digits-in-a-number
                var totalDigits = Math.Floor(Math.Log10(result.Year.Value) + 1);
                var sigDigits = int.Parse(yearPrecision);
                result.Year.SignificantDigits = sigDigits;
                var insignificantDigits = totalDigits - sigDigits;
                result.Year.InsignificantDigits = (insignificantDigits < 0) ? (byte)0 : (byte)insignificantDigits;
                if (totalDigits - sigDigits < 0)
                {
                    // sig digits larger then year digit count
                    result.Year.Invalid = true;
                    return false;
                }
            }

            var yearFlags = g["yearcloseflags"].Value + g["yearopenflags"].Value;
            SetDatePartFlags(yearFlags, ref result.Year);

            return true;
        }

        private static void SetDatePartFlags(string flags, ref DatePart datePart)
        {
            if (!string.IsNullOrEmpty(flags))
            {
                datePart.IsApproximate |= flags.Contains('~') || flags.Contains('%');
                datePart.IsUncertain |= flags.Contains('?') || flags.Contains('%');
            }
        }
    }
}