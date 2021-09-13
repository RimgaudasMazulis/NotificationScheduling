using System;

namespace NotificationScheduling.Services.Helpers
{
    public static class DateHelper
    {
        public static string FormatDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
