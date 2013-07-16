using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Utility
{
    public class DateTimeHelper
    {
        public static DateTime GetFirstDayOfMonth(int Year, int Month)
        {
            return Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-1");
        }

        public static DateTime GetLastDayOfMonth(int Year, int Month)
        {
            //这里的关键就是 DateTime.DaysInMonth 获得一个月中的天数
            int Days = DateTime.DaysInMonth(Year, Month);
            return Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Days.ToString());

        }
    }
}
