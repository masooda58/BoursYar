using System;
using System.Globalization;
using PersianTools.Core;


namespace DAL
{
   public static class Extention
   {
       public static string ToShamsi(this DateTime value)
       {
            PersianCalendar pc= new PersianCalendar();

           return pc.GetYear(value).ToString("0000") + "/" + pc.GetMonth(value).ToString("00") + "/" +
                  pc.GetDayOfMonth(value).ToString("00");
       }

       public static DateTime ToMiladi(this string value)
       {
            
           var pdt = new PersianDateTime(value);
           var ff = pdt.ToGregorianDateTime();
            var dt1= new DateTime(ff.Year,ff.Month,ff.Day);
           return dt1;
       }
      
            
      
    }
}
