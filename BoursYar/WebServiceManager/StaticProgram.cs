using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;


using Timer = System.Threading.Timer;


namespace WebServiceManager
{
   public static class StaticProgram

    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
   
        public static object ThreadLockToken = new object();
        // threading timer for call web service
        public static IDictionary<string, Timer> ReqTimers = new Dictionary<string, Timer>();
        // تنطیمات برای زمان تایمر 
        public static IDictionary<string, ScheduleRun> ScheduleRuns = new Dictionary<string, ScheduleRun>();
        // تنظیمات موجود در بانک برای درخواست اطلاعات از وبسرویس
        public static IDictionary<string, CallWebServiceSetting> WebSettings = new Dictionary<string, CallWebServiceSetting>();
        // تابعی که باید برای تایمر ارسال شود
        public static IDictionary<string, ScheduleCallBack> ScheduleCallBacks = new Dictionary<string, ScheduleCallBack>();
        public static IDictionary<string, string> NextRuns = new Dictionary<string, string>();
       
    }
}
