using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WINConnect.Libs.Extensions
{
    /// <summary>
    /// StringExtension
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// EqualsOrdinalIgnoreCase
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool EqualsOrdinalIgnoreCase(this string value1, string value2)
        {
            return string.Equals(value1, value2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Return original value or empty if no value specified.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToValueOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
        }

        /// <summary>
        /// Return original value replacement value provided if no value specified.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToValueOrDefault(this string value, string defaultValue)
        {
            if (value.IsEmpty())
            {
                return defaultValue;
            }
            return value;
        }

        /// <summary>
        /// ToLowerValue
        ///     {string}.ToValueOrEmpty().ToLower()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerValue(this string value)
        {
            return value.ToValueOrEmpty().ToLower();
        }

        /// <summary>
        /// IsEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Get a substring of the first N characters.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int length)
        {
            value = value.ToValueOrEmpty();
            if (value.Length > length)
            {
                return value.Substring(0, length);
            }
            return value;
        }

        /// <summary>
        /// ToInt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            int i = 0;
            int.TryParse(value, out i);

            return i;
        }

        /// <summary>
        /// ToBoolean
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            bool result = false;
            Boolean.TryParse(value, out result);

            return result;
        }

        /// <summary>
        /// ToDecimalFormat
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digitformat"></param>
        /// <returns></returns>
        public static string ToDecimalFormat(this string value, string digitformat = "{0:N}")
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            return string.Format(digitformat, Convert.ToDouble(value));
        }

        /// <summary>
        /// IsStartsWith
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsStartsWith(this string str, string value)
        {
            if (str.IsEmpty())
            {
                return false;
            }

            if (value.IsEmpty())
            {
                return false;
            }

            return str.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// ReplaceString
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static string ReplaceString(this string str, string oldValue)
        {
            return str.ReplaceString(oldValue, string.Empty);
        }

        /// <summary>
        /// ReplaceString
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceString(this string str, string oldValue, string newValue)
        {
            if (str.IsEmpty())
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(oldValue))
            {
                return str;
            }

            if (newValue == null)
            {
                newValue = newValue.ToValueOrEmpty();
            }

            return str.Replace(oldValue, newValue);
        }

        /// <summary>
        /// RemoveSpecialCharacters
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            str = str.ToValueOrEmpty();

            if (str.IsEmpty())
            {
                return string.Empty;
            }

            string[] charsToRemove = new string[] { 
                "+", ".", "#", "!", "'", 
                "$", "%", "^", "&", "*", 
                "(", ")", "-", "<", ">", 
                "?", "/", ";", ",", ":", " ",
                "{", "}", "[", "]", "\\", "|" };

            foreach (string c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }

            return str;
        }

        /// <summary>
        /// ToTemperatureDigit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTemperatureDigit(this string value)
        {
            if (value.IsEmpty())
            {
                return "000";

            }

            decimal outvalue;
            if (!decimal.TryParse(value, out outvalue))
            {
                return "000";
            }

            string[] splits = value.Split('.');
            if (splits.Length > 1)
            {
                if (splits[0].Length >= 3 && splits[1].Length > 0)
                {
                    return string.Format("{0}", splits[0].ToInt());
                }

                return string.Format("{0:00}.{1:0}", splits[0].ToInt(), splits[1].ToInt());
            }

            return string.Format("{0:000}", value.ToInt());
        }

        /// <summary>
        /// GetSubstring
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string GetSubstring(this string str, int startIndex)
        {
            if (str.IsEmpty())
            {
                return string.Empty;
            }

            return str.Length <= startIndex ? string.Empty : str.Substring(startIndex);
        }

        /// <summary>
        /// GetSubstring
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubstring(this string str, int startIndex, int length)
        {
            return str.GetSubstring(startIndex, length, false);
        }

        /// <summary>
        /// GetSubstring
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="ignoreOutOfRange"></param>
        /// <returns></returns>
        public static string GetSubstring(this string str, int startIndex, int length, bool ignoreOutOfRange)
        {
            if (str.IsEmpty())
            {
                return string.Empty;
            }

            if (startIndex < 0)
            {
                return string.Empty;
            }

            if (startIndex > str.Length)
            {
                return string.Empty;
            }

            if (length <= 0)
            {
                return string.Empty;
            }

            if (startIndex > (str.Length - length))
            {
                return ignoreOutOfRange ? str.Substring(startIndex) : string.Empty;
            }

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// SplitOrEmpty
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] SplitOrEmpty(this string str, params char[] separator)
        {
            if (str.IsEmpty())
            {
                return new string[] { };
            }

            return str.Split(separator);
        }

        /// <summary>
        /// Split with trim
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] SplitWithTrim(this string str, params char[] separator)
        {
            string[] values = str.SplitOrEmpty(separator)
                .Select(x => x.Trim())
                .ToArray();

            return values;
        }

        /// <summary>
        /// Convert string to type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T GetValue<T>(this string str)
        {
            try
            {
                return (T)Convert.ChangeType(str, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Remove Invalid filename characters from string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Valid filename</returns>
        public static string GetValidFileName(this string fileName)
        {
            return Path.GetInvalidFileNameChars()
                .Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty))
                .ReplaceString(" ", "_");
        }

        /// <summary>
        /// ContainsString
        /// </summary>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string source, string comparer)
        {
            if (comparer.IsEmpty())
            {
                return false;
            }

            return source.IndexOf(comparer, StringComparison.OrdinalIgnoreCase) > -1;
        }

        /// <summary>
        /// Chop
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Chop(this string str)
        {
            str = str.Trim();
            if (str.Length > 35)
            {
                return str.GetSubstring(1, 35, true) + "...";
            }
            return str;
        }
    }
}