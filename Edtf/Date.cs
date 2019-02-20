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

namespace Edtf {

	public struct Date {

		public DateStatus Status { get; set; }

		public DatePart Year;
		public DatePart Month;
		public string SeasonQualifier { get; set; }
		public DatePart Day;
		public int Hour { get; set; }
		public int Minute { get; set; }
		public int Second { get; set; }
		public int TimeZoneOffset { get; set; }	// In minutes, positive or negative
		public bool HasTimeZoneOffset { get; set; }		// Useful to know if "0" means UTC or just undefined

		public override string ToString() {
			if (Status == DateStatus.Unused) return string.Empty;
			if (Status == DateStatus.Open) return SpecialValues.Open;
			if (Status == DateStatus.Unknown) return string.Empty;
			if (!Year.HasValue) return string.Empty;

			// FLAG GROUPINGS.

            var isSingleUncertain = Year.IsUncertain && !Month.IsUncertain;
            var isSingleApprox = Year.IsApproximate && !Month.IsApproximate;

            string result = string.Empty;
            AddFlagsToString(ref result, isSingleApprox, isSingleUncertain);

			result += Year.ToString(4);

			if (Month.HasValue) {
				result += "-";

                var isLeftUncertain = !Year.IsUncertain && Month.IsUncertain;
                var isLeftApprox = !Year.IsApproximate && Month.IsApproximate;

                AddFlagsToString(ref result, isLeftApprox, isLeftUncertain);

                result += Month.ToString(2).PadLeft(2, '0');

                var isRightUncertain = Year.IsUncertain && Month.IsUncertain && !Day.IsUncertain;
                var isRightApprox = Year.IsApproximate && Month.IsApproximate && !Day.IsApproximate;

                AddFlagsToString(ref result, isRightApprox, isRightUncertain);

                if (Day.HasValue) {
					result += "-";
                    isLeftUncertain = !(Year.IsUncertain && Month.IsUncertain) && Day.IsUncertain;
                    isLeftApprox = !(Year.IsApproximate && Month.IsApproximate) && Day.IsApproximate;

                    AddFlagsToString(ref result, isLeftApprox, isLeftUncertain);

                    result += Day.ToString(2).PadLeft(2, '0');

                    isRightUncertain = Year.IsUncertain && Month.IsUncertain && Day.IsUncertain;
                    isRightApprox = Year.IsApproximate && Month.IsApproximate && Day.IsApproximate;

                    AddFlagsToString(ref result, isRightApprox, isRightUncertain);

                    if ( (Hour > 0) || (Minute > 0) || (Second > 0) ) {
						result += "T" + Hour.ToString("00") + ":" + Minute.ToString("00") + ":" + Second.ToString("00");
						if (TimeZoneOffset == 0) {
							// The standard is somewhat unclear, but suggests that if there is no "Z" and no TZ offset,
							// the date does not define a time zone and should not be serialized to use UTC.
							if (!HasTimeZoneOffset)	return result;
							return result + "Z";
						} else {
							var tzHour = TimeZoneOffset / 60;
							var tzMinute = TimeZoneOffset % 60;
							result += 
								(TimeZoneOffset < 0 ? "-" : "+")
								+ tzHour.ToString("00")
								+ ":" + tzMinute.ToString("00");
						}
					}
				}
			}
			return result;
		}

        private void AddFlagsToString(ref string result, bool approx, bool uncertain)
        {
            if (approx || uncertain)
            {
                result += approx && uncertain ? "%" : approx? "~" : "?";
            }
        }

        internal static Date Parse(string s, bool hasInterval)
        {
            return DateParser.Parse(s, hasInterval);
        }

        public static Date Parse(string s)
        {
            return DateParser.Parse(s, false);
        }

	}

}