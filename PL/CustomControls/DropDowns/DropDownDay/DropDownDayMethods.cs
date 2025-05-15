using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVBC_CLCB_Notifier.PL.CustomControls.DropDowns.DropDownDay
{
    public static class DropDownDayMethods
    {
        public static List<int> GetDaysInMonth(DateTime date)
        {
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return Enumerable.Range(1, daysInMonth).ToList();
        }

    }
}
