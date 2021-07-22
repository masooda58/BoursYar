using DAL;
using System;

namespace WebServiceManager

{
    public class CreatJob
    {
        private static void IntialJob() // public just for test
        {
            StaticDictionary.WebSettings.Clear();
            StaticDictionary.ScheduleCallBacks.Clear();
            StaticDictionary.ScheduleRuns.Clear();
            StaticDictionary.NextRuns.Clear();
            using (UnitOfWorkDapper db = new UnitOfWorkDapper())
            {
                var settings = db.CallwebservicesettingDapperRepository.GetAllData();

                foreach (var setting in settings)
                {
                    StaticDictionary.WebSettings.Add(setting.Name.ToLower(), setting);

                    StaticDictionary.ScheduleCallBacks.Add(setting.Name.ToLower(), new ScheduleCallBack(setting.Url));
                    StaticDictionary.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(StaticDictionary.ScheduleCallBacks[setting.Name.ToLower()].CallActionBack(setting.Name.ToLower()), setting.StartTime, setting.FinishTime, setting.Interval * 1000, setting.Name.ToLower()));
                    StaticDictionary.NextRuns.Add(setting.Name.ToLower(), "");

                }

            }
        }

        public static void DefaultSetting()
        {



        }

        public static void RunAllTimer()
        {
            foreach (var timer in StaticDictionary.ReqTimers)
            {
                timer.Value.Dispose();
            }

            StaticDictionary.ReqTimers.Clear();
            IntialJob();
            foreach (var item in StaticDictionary.ScheduleRuns)
            {
                if (StaticDictionary.WebSettings[item.Key].Faal)
                {

                    StaticDictionary.ReqTimers.Add(item.Key, new System.Threading.Timer(item.Value.TimerCallBack, null,
                        (int)item.Value.MilisecondWaitForNewStart().TotalMilliseconds, item.Value.Interval));
                    StaticDictionary.NextRuns[item.Key] = (DateTime.Now + item.Value.MilisecondWaitForNewStart()).ToString("G");

                }
                else
                {
                    StaticDictionary.NextRuns[item.Key] = "Stop";
                }

            }
        }

    }
}
