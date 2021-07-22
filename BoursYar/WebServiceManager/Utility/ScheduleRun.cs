using PersianTools.Core;
using System;

namespace WebServiceManager
{

    public class ScheduleRun
    {

        public static EventHandler ChangedNextTim;
        public int Interval;
        private string _startTime;
        private string _endTime;
        private Action _callByTimer;
        private string _name;

        public ScheduleRun(Action callByTimer, string startTime, string endTime, int interval, string name)
        {

            _startTime = startTime;
            _endTime = endTime;
            Interval = interval;
            _callByTimer = callByTimer;
            _name = name;


        }

        public void TimerCallBack(object o)
        {

            DateTime now = DateTime.Now;
            var ft = _endTime.Split(':');
            int ftHour = int.Parse(ft[0]);
            int ftMin = int.Parse(ft[1]);
            int ftSec = int.Parse(ft[2]);
            DateTime endRun = new DateTime(now.Year, now.Month, now.Day, ftHour, ftMin, ftSec, 0);
            // _lblNext.SafeInvoke(d => d.Text = (now.ToString()));
            if (now > endRun)
            {
                StaticDictionary.NextRuns[_name] = (DateTime.Now + MilisecondWaitForNewStart()).ToString();
                StaticDictionary.ReqTimers[_name].Change((int)MilisecondWaitForNewStart().TotalMilliseconds, Interval);
                OnChangedNextTim(EventArgs.Empty);
            }
            // call do method
            // check now time > _endtime  if yes call milisecandwaitfornextstart and timer chang and lblnext = now+milisecandwaitfornextstart
            // else lblnext = now+_interval
            else
            {

                StaticDictionary.NextRuns[_name] = (DateTime.Now.AddMilliseconds(Convert.ToDouble(Interval))).ToString();
                OnChangedNextTim(EventArgs.Empty);
                _callByTimer();

            }

        }





        public TimeSpan MilisecondWaitForNewStart()
        {

            DateTime now = DateTime.Now;

            var st = _startTime.Split(':');
            var ft = _endTime.Split(':');
            int stHour = int.Parse(st[0]);

            int stMin = int.Parse(st[1]);
            int stSec = int.Parse(st[2]);
            int ftHour = int.Parse(ft[0]);

            int ftMin = int.Parse(ft[1]);
            int ftSec = int.Parse(ft[2]);

            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, stHour, stMin, stSec, 0);
            DateTime endRun = new DateTime(now.Year, now.Month, now.Day, ftHour, ftMin, ftSec, 0);
            if (now > endRun)
            {
                firstRun = firstRun.AddDays(1);
                endRun = endRun.AddDays(1);
            }
            var firstRunShamsi = new PersianDateTime(firstRun);
            var panShanbe = new PersianDateTime(1400, 02, 9);// یک روز پنجشنبه است
            while (firstRunShamsi.IsHoliDay || firstRunShamsi.DayOfWeek == panShanbe.DayOfWeek || (firstRunShamsi.Day == 30 && firstRunShamsi.Month == 12))
            {
                firstRunShamsi.AddDays(1);
                endRun.AddDays(1);
                firstRun = firstRun.AddDays(1);
            }
            TimeSpan timeToGo = firstRun - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }
            //_lblNext.SafeInvoke(d => d.Text =( now+timeToGo).ToString());
            return timeToGo;

        }


        protected virtual void OnChangedNextTim(EventArgs e)
        {
            ChangedNextTim?.Invoke(this, e);
        }
    }

}
