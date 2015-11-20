using System;

namespace WINConnect.Libs.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Check if DateTime is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidDateTime(this DateTime? dt)
        {
            if (dt == null)
            {
                return false;
            }

            if (!dt.HasValue)
            {
                return false;
            }

            if (dt.Value <= DateTime.MinValue)
            {
                return false;
            }

            if (dt.Value >= DateTime.MaxValue)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent short date string representation.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToWINDateString(this DateTime dt)
        {
            return string.Format("{0:yyyy-MM-dd}", dt);
        }

        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static Int64 DateDiff(this DateTime StartDate, String DatePart, DateTime EndDate)
        {
            Int64 DateDiffVal = 0;
            System.Globalization.Calendar cal = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart.ToLower().Trim())
            {
                #region year
                case "year":
                case "yy":
                case "yyyy":
                    DateDiffVal = (Int64)(cal.GetYear(EndDate) - cal.GetYear(StartDate));
                    break;
                #endregion

                #region quarter
                case "quarter":
                case "qq":
                case "q":
                    DateDiffVal = (Int64)((((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 4)
                                        + ((cal.GetMonth(EndDate) - 1) / 3))
                                        - ((cal.GetMonth(StartDate) - 1) / 3));
                    break;
                #endregion

                #region month
                case "month":
                case "mm":
                case "m":
                    DateDiffVal = (Int64)(((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 12
                                        + cal.GetMonth(EndDate))
                                        - cal.GetMonth(StartDate));
                    break;
                #endregion

                #region day
                case "day":
                case "d":
                case "dd":
                    DateDiffVal = (Int64)ts.TotalDays;
                    break;
                #endregion

                #region week
                case "week":
                case "wk":
                case "ww":
                    DateDiffVal = (Int64)(ts.TotalDays / 7);
                    break;
                #endregion

                #region hour
                case "hour":
                case "hh":
                    DateDiffVal = (Int64)ts.TotalHours;
                    break;
                #endregion

                #region minute
                case "minute":
                case "mi":
                case "n":
                    DateDiffVal = (Int64)ts.TotalMinutes;
                    break;
                #endregion

                #region second
                case "second":
                case "ss":
                case "s":
                    DateDiffVal = (Int64)ts.TotalSeconds;
                    break;
                #endregion

                #region millisecond
                case "millisecond":
                case "ms":
                    DateDiffVal = (Int64)ts.TotalMilliseconds;
                    break;
                #endregion

                default:
                    throw new Exception(String.Format("DatePart \"{0}\" is unknown", DatePart));
            }
            return DateDiffVal;
        }

        /* http://codereview.stackexchange.com/questions/2738/pretty-date-generator */
        /// <summary>
        /// Gets the relative time for a datetime.
        /// </summary>
        /// <param name="dateTime">The datetime to get the relative time.</param>
        /// <returns>A relative time in english.</returns>
        public static string GetTimeSpan(this DateTime dateTime)
        {
            if (!IsValidDateTime(dateTime))
            {
                return string.Empty;
            }

            TimeSpan diff = DateTime.UtcNow.Subtract(dateTime);

            if (diff.TotalMinutes < 1)
            {
                return string.Format("{0:D2} second{1} ago", diff.Seconds, PluralizeIfNeeded(diff.Seconds));
            }

            if (diff.TotalHours < 1)
            {
                return string.Format("{0:D2} minute{1} ago", diff.Minutes, PluralizeIfNeeded(diff.Minutes));
            }

            if (diff.TotalDays < 1)
            {
                return string.Format("{0:D2} hour{2} and {1:D2} minute{3} ago", diff.Hours, diff.Minutes, PluralizeIfNeeded(diff.Hours), PluralizeIfNeeded(diff.Minutes));
            }

            if (diff.TotalDays <= 2)
            {
                return string.Format(
                    "{0:D2} day{3}, {1:D2} hour{4} and {2:D2} minute{5} ago",
                    diff.Days,
                    diff.Hours,
                    diff.Minutes,
                    PluralizeIfNeeded(diff.Days),
                    PluralizeIfNeeded(diff.Hours),
                    PluralizeIfNeeded(diff.Minutes));
            }

            if (diff.TotalDays <= 30)
            {
                return string.Format("{0:D2} days ago", diff.Days);
            }

            int years = (int)(diff.TotalDays / 365);
            if (years >= 1)
            {
                return string.Format("{0} year{1} ago", years, PluralizeIfNeeded(years));
            }

            return string.Format("{0:g}", dateTime);
        }

        /// <summary>
        /// Gets a 's' if value is > 1.
        /// </summary>
        /// <param name="testValue">The value to test.</param>
        /// <returns>An 's' if value is > 1, otherwise an empty string.</returns>
        private static string PluralizeIfNeeded(int testValue)
        {
            return testValue > 1 ? "s" : string.Empty;
        }

    }
}