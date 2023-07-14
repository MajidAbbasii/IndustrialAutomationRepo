using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            int y = calendar.GetYear(dateTime);
            int m = calendar.GetMonth(dateTime);
            int d = calendar.GetDayOfMonth(dateTime);
            return string.Format("{0}/{1}/{2}", y, m.ToString().PadLeft(2, '0'), d.ToString().PadLeft(2, '0'));
        }

        public static string ToPersianDateTime(this DateTime dateTime)
        {
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            int y = calendar.GetYear(dateTime);
            int m = calendar.GetMonth(dateTime);
            int d = calendar.GetDayOfMonth(dateTime);
            int h = calendar.GetHour(dateTime);
            int M = calendar.GetMinute(dateTime);
            return string.Format("{0}/{1}/{2}-{3}:{4}", y, m.ToString().PadLeft(2, '0'), d.ToString().PadLeft(2, '0'), h.ToString().PadLeft(2, '0'), M.ToString().PadLeft(2, '0'));
        }

        public static string HijriToPersianDate(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            string dt = value;
            if (dt.Length == 10) dt = dt + "-00:00";
            if (dt.Length != 16) return null;
            dt = dt.Replace("/", "");
            dt = dt.Replace("-", "");
            dt = dt.Replace(":", "");
            if (dt.IsDigit(12))
            {
                int y = System.Convert.ToInt32(dt.Substring(0, 4));
                int m = System.Convert.ToInt32(dt.Substring(4, 2));
                int d = System.Convert.ToInt32(dt.Substring(6, 2));
                int h = System.Convert.ToInt32(dt.Substring(8, 2));
                int mi = System.Convert.ToInt32(dt.Substring(10, 2));
                System.Globalization.HijriCalendar calendar = new System.Globalization.HijriCalendar();
                DateTime dt1 = calendar.ToDateTime(y, m, d, h, mi, 0, 0);
                if (value.Length == 10)
                {
                    return dt1.ToPersianDate();
                }
                else
                {
                    return dt1.ToPersianDateTime();
                }
            }
            return null;
        }

        public static DateTime? ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            string dt = value;
            if (dt.Length == 10) dt = dt + "-00:00";
            if (dt.IsValidDateTime())
            {

                dt = dt.Replace("/", "");
                dt = dt.Replace("-", "");
                dt = dt.Replace(":", "");
                if (dt.IsDigit(12))
                {
                    int y = System.Convert.ToInt32(dt.Substring(0, 4));
                    int m = System.Convert.ToInt32(dt.Substring(4, 2));
                    int d = System.Convert.ToInt32(dt.Substring(6, 2));
                    int h = System.Convert.ToInt32(dt.Substring(8, 2));
                    int mi = System.Convert.ToInt32(dt.Substring(10, 2));
                    System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
                    return calendar.ToDateTime(y, m, d, h, mi, 0, 0);
                }
            }
            return null;
        }

        public static bool IsValidDateTime(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (value.Length != 16) return false;
            var sdt = value.Split('-');
            if (sdt.Length == 2)
            {
                if (sdt[1].IsValidTime())
                {
                    if (sdt[0].IsValidDate())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool IsValidDate(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (value.Length != 10) return false;
            string pattern = @"^(1[234])\d\d/((0[1-6]/((0[1-9])|([12]\d)|(3[01])))|(((0[7-9])|(1[012]))/((0[1-9])|([12]\d)|(30))))$";
            bool r = System.Text.RegularExpressions.Regex.IsMatch(value, pattern);
            if (r)
            {
                string d = value.Substring(8, 2);
                if (d == "30")
                {
                    string m = value.Substring(5, 2);
                    if (m == "12")
                    {
                        string y = value.Substring(0, 4);
                        System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
                        if (!calendar.IsLeapYear(System.Convert.ToInt32(y)))
                        {
                            return false;
                        }
                    }
                }
            }
            return r;
        }
        public static bool IsValidTime(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (value.Length != 5) return false;
            string pattern = @"^(([01]\d)|(2[0-3])):[0-5]\d$";
            bool r = System.Text.RegularExpressions.Regex.IsMatch(value, pattern);
            return r;
        }

        public static string AddMinutes(this string value, int minutes)
        {
            DateTime? dt = value.ToDateTime();
            if (dt == null) return null;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            if (value.Length == 10) return calendar.AddMinutes(dt.Value, minutes).ToPersianDate();
            return calendar.AddMinutes(dt.Value, minutes).ToPersianDateTime();
        }
        public static string AddHours(this string value, int hours)
        {
            DateTime? dt = value.ToDateTime();
            if (dt == null) return null;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            if (value.Length == 10) return calendar.AddHours(dt.Value, hours).ToPersianDate();
            return calendar.AddHours(dt.Value, hours).ToPersianDateTime();
        }

        public static string AddDays(this string value, int days)
        {
            DateTime? dt = value.ToDateTime();
            if (dt == null) return null;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            if (value.Length == 10) return calendar.AddDays(dt.Value, days).ToPersianDate();
            return calendar.AddDays(dt.Value, days).ToPersianDateTime();
        }

        public static string AddMonths(this string value, int months)
        {
            DateTime? dt = value.ToDateTime();
            if (dt == null) return null;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            if (value.Length == 10) return calendar.AddMonths(dt.Value, months).ToPersianDate();
            return calendar.AddMonths(dt.Value, months).ToPersianDateTime();
        }

        public static string AddYears(this string value, int years)
        {
            DateTime? dt = value.ToDateTime();
            if (dt == null) return null;
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
            if (value.Length == 10) return calendar.AddYears(dt.Value, years).ToPersianDate();
            return calendar.AddYears(dt.Value, years).ToPersianDateTime();
        }

        public static string ReverseDate(this string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 10) return value;
            string[] sv = value.Split('/');
            if (sv.Length != 3) return value;
            return sv[2] + "/" + sv[1] + "/" + sv[0];
        }

        public static string PersianWeekDayName(this string value)
        {
            DateTime? dt = value.ToDateTime();
            if (dt == null) return null;
            switch (dt.Value.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Monday:
                    return "دو شنبه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
            }
            return null;
        }

        public static TimeSpan GetDateTimeDistance(this string self, string value)
        {
            DateTime? dt1 = self.ToDateTime();
            if (dt1 == null) return new TimeSpan();
            DateTime? dt2 = value.ToDateTime();
            if (dt2 == null) return new TimeSpan();
            return (dt1.Value - dt2.Value);
        }

        public static string SetDateOfDateTime(this string dateTime, string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                dateTime = "9999/99/99-99:99";
            else if (string.IsNullOrWhiteSpace(dateTime))
                dateTime = date + "-" + "08:00";
            else if (date.Length == 10 && dateTime.Length > 11 && dateTime.Substring(11) == "99:99")
                dateTime = date + "-" + "08:00";
            else if (date.Length == 10)
                dateTime = dateTime.Replace(dateTime.Substring(0, 10), date);
            return (dateTime);
        }

        public static string SetTimeOfDateTime(this string dateTime, string time)
        {
            if (string.IsNullOrWhiteSpace(time) && !string.IsNullOrWhiteSpace(dateTime))
                dateTime = dateTime.Substring(0, 10) + "-" + "99:99";
            else if (string.IsNullOrWhiteSpace(dateTime))
                dateTime = "1390/01/01" + "-" + time;
            else
                dateTime = dateTime.Replace(dateTime.Substring(10), "-" + time);
            return (dateTime);
        }

        public static string GetTimeOfDateTime(this string dateTime)
        {
            if (dateTime != null && dateTime.Length > 11 && dateTime.Substring(11) == "99:99")
                return null;
            if (dateTime != null && dateTime != "9999/99/99-99:99" && dateTime.Length > 11)
                return dateTime.Substring(11);
            return null;
        }

        public static string GetDateOfDateTime(this string dateTime)
        {
            if (dateTime != null && dateTime.Length > 9 && dateTime.Substring(0, 10) == "9999/99/99")
                return null;
            if (dateTime != null && dateTime != "9999/99/99-99:99" && dateTime.Length > 9)
                return dateTime.Substring(0, 10);
            return null;
        }


        public static void Rethrow(this System.Exception ex)
        {
            if (ex == null) return;
            throw new System.Exception(ex.Message, ex);
        }

    }
}
