using DAL;
using System;

namespace WebServiceManager

{
    public class CreatJob
    {
        private static void IntialJob() // public just for test
        {
            StaticProgram.WebSettings.Clear();
            StaticProgram.ScheduleCallBacks.Clear();
            StaticProgram.ScheduleRuns.Clear();
            StaticProgram.NextRuns.Clear();
            using (UnitOfWorkDapper db = new UnitOfWorkDapper())
            {
                var settings = db.CallwebservicesettingDapperRepository.GetAllData();

                foreach (var setting in settings)
                {
                    StaticProgram.WebSettings.Add(setting.Name.ToLower(), setting);

                    StaticProgram.ScheduleCallBacks.Add(setting.Name.ToLower(), new ScheduleCallBack(setting.Url));
                    StaticProgram.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(StaticProgram.ScheduleCallBacks[setting.Name.ToLower()].CallActionBack(setting.Name.ToLower()), setting.StartTime, setting.FinishTime, setting.Interval * 1000, setting.Name.ToLower()));
                    StaticProgram.NextRuns.Add(setting.Name.ToLower(), "");

                }

            }
        }

        public static void DefaultSetting()
        {



        }

        public static void RunAllTimer()
        {
            foreach (var timer in StaticProgram.ReqTimers)
            {
                timer.Value.Dispose();
            }

            StaticProgram.ReqTimers.Clear();
            IntialJob();
            foreach (var item in StaticProgram.ScheduleRuns)
            {
                if (StaticProgram.WebSettings[item.Key].Faal)
                {

                    StaticProgram.ReqTimers.Add(item.Key, new System.Threading.Timer(item.Value.TimerCallBack, null,
                        (int)item.Value.MilisecondWaitForNewStart().TotalMilliseconds, item.Value.Interval));
                    StaticProgram.NextRuns[item.Key] = (DateTime.Now + item.Value.MilisecondWaitForNewStart()).ToString("G");

                }
                else
                {
                    StaticProgram.NextRuns[item.Key] = "Stop";
                }

            }
        }

    }
}
