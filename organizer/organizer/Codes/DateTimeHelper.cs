using System;
using System.Text.RegularExpressions;

// Date Time functions
namespace organizer.Codes {
    public static class DateTimeHelper {

        // to write to the database
        public static string ToString(DateTime dateTime) {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); // case sensitive
        }

        // to restore from database
        public static DateTime FromString(string str) {
            string pattern = @"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})\.(\d{3})";
            if (Regex.IsMatch(str, pattern)) {
                Match match = Regex.Match(str, pattern);
                int year = Convert.ToInt32(match.Groups[1].Value);
                int month = Convert.ToInt32(match.Groups[2].Value);
                int day = Convert.ToInt32(match.Groups[3].Value);
                int hour = Convert.ToInt32(match.Groups[4].Value);
                int minute = Convert.ToInt32(match.Groups[5].Value);
                int second = Convert.ToInt32(match.Groups[6].Value);
                int millisecond = Convert.ToInt32(match.Groups[7].Value);
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            } else {
                throw new Exception("Unable to parse.");
            }
        }
    }
}
