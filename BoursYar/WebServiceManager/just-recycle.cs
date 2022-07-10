using PersianTools.Core;

namespace WDbManager
{
    class JustRecycle
    {
        public void test()
        {
            var pdt = new PersianDateTime(1400, 01, 17);
            var x = pdt.GetDateInformation();
        }
        // دیتای روزانه اولیه
        //UnitOfWork db = new UnitOfWork();
        //var pdt = new PersianDateTime(1400, 01, 17);
        //var panShanbe = new PersianDateTime(1400, 02, 9);

        //    for (int i = 1; i <= 100; i++)
        //{

        //    pdt = pdt.AddDays(-1);
        //    if (pdt.IsHoliDay || pdt.DayOfWeek==panShanbe.DayOfWeek || (pdt.Day==30 && pdt.Month==12))
        //    {
        //        continue;
        //        // MessageBox.Show($"تعطیل است" + pdt.ToString());
        //    }
        //    else
        //    {

        //        NamadInfoVariable urlInfoVariable = new NamadInfoVariable(pdt.ShamsiDate);

        //        using (var client = new HttpClient())
        //        {
        //            var res = client.GetStringAsync(urlInfoVariable.Url).Result;
        //            var l = JsonConvert.DeserializeObject<List<AllNamadInfo_Daily>>(res);
        //            db.AllNamadInfo_DailyRepository.AddList(l, pdt.ShamsiDate);
        //            db.AllNamadInfo_DailyRepository.save();

        //        }
        //        Thread.Sleep(15000);
        //    }
    }
}
//var x = db.AllNamadInfoDailies.Where(c => c.Name == "وسپهر").OrderByDescending(s => s.MiladiDate).Take(6).OrderBy(s => s.MiladiDate).Select(c => new MyCustomQuote()
//{
//Date = c.MiladiDate,
//// Open =(decimal)c.FinalPrice,
//Close = (decimal)c.TradeVolume,
//// High = (decimal)c.HighestPrice,
//// Low =(decimal)c.LowestPrice,
//// Volume = (decimal)c.TradeVolume


//}).ToList();

//List<MyCustomQuote> history = x;

//IEnumerable<SmaResult> results = Indicator.GetSma(history, 5);



//var cc = (List < AllNamadInfo >) gl;
//var tt = cc.Where(c => c.Name.StartsWith("ض"));
//var op = JsonConvert.DeserializeObject<List<AllNamadOption>>(JsonConvert.SerializeObject(tt));
//    //.Select(d => new AllNamadOption());

//AppSetting app = new AppSetting();
//;
//using (IDbConnection dbc = new SqlConnection(db.Database.Connection.ConnectionString))
//{

//    dbc.Open();
//    //var xcc = dbc.GetAll<AllNamadInfo>().Where(c => c.Name == "اپال").ToList();
//    //var xc = dbc.Delete(xcc);
//    dbc.Insert(op);

//}
//string gg = string.Empty;
//    gg = File.ReadAllText(@"C:\Stock-Project\WinForm\WDbManager\WDbManager\new.json");
//  gl = JsonConvert.DeserializeObject<List<AllNamadInfo>>(gg);
// IndexesVariable index= new IndexesVariable(IndexesVariable.Bazar.fav_namad_bourse);
//  WebReadJson<List<Codal>> st= new WebReadJson<List<Codal>>(index.Url);
// PayamNazerVariable payamNazer= new PayamNazerVariable();
//CandelPatternVariable ii =new CandelPatternVariable();
//WebReadJson<CandelPattern> cp= new WebReadJson<CandelPattern>(ii.Url);
//var ff = (CandelPattern)cp.WebReadjsonResult();


//UnitOfWorkDapper db=new UnitOfWorkDapper();

// db.BourseDapperRepository.AddData((BourseIndex)xx);
//https://www.codeproject.com/Articles/1186566/Dapper-Generic-Repository
//.......................................................................
//string connectionString = ConfigurationManager.ConnectionStrings["WDbContext"].ConnectionString;

//IDbConnection dbc = new SqlConnection(connectionString);

//var x = dbc.Query<Arz>(@"with tt as
//            (
//            select * , max(ReqdateTime) over(partition by name) as maxdate
//            from[WDb_Repository].[dbo].[Arz]
//            )
//            select * from tt where ReqDateTime = maxdate "
//).ToList();

//var y = x.Average(f => f.Price);

//............................................................................................

//if (setting.Name.ToLower() == "allnamadinfo")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["allnamadinfo"].AllNamadInfoRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}

//if (setting.Name.ToLower() == "market_bourse")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["market_bourse"].market_bourseRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "market_farabourse")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["market_farabourse"].market_farabourseRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "industeryindex")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["industeryindex"].IndusteryIndexRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "fav_namad_bourse")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["fav_namad_bourse"].fav_namad_bourseRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "fav_namad_farabourse")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["fav_namad_farabourse"].fav_namad_farabourseRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "ind_namad_bourse")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["ind_namad_bourse"].ind_namad_bourseRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "ind_namad_farabourse")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["ind_namad_farabourse"].ind_namad_farabourseRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "codal")
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["codal"].CodalRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "Payamnazer".ToLower())
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["payamnazer"].PayamnazerRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "Khodro".ToLower())
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["khodro"].KhodroRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "Crypto".ToLower())
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["crypto"].CryptoRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "arz".ToLower())
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["arz"].ArzRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "AllNamadInfo_Daily".ToLower())
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["allnamadinfo_daily"].AllNamadInfo_DailyRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}
//if (setting.Name.ToLower() == "AllNamadOption".ToLower())
//{
//    Program.ScheduleRuns.Add(setting.Name.ToLower(), new ScheduleRun(Program.ScheduleCallBacks["AllNamadOption".ToLower()].AllNamadOptionRead, setting.StartTime, setting.FinishTime, setting.Interval, setting.Name.ToLower()));
//}

//....................................................................
//var ss = Program.ScheduleCallBacks["arz"].CallActionBack("arz");



//foreach (var item in Program.ScheduleRuns)
//{
//if (Program.WebSettings[item.Key].Faal)
//{

//    Program.ReqTimers.Add(item.Key, new System.Threading.Timer(item.Value.TimerCallBack, null,
//        (int) item.Value.MilisecondWaitForNewStart().TotalMilliseconds, item.Value.Interval*1000));
//}

//}

///
///....................................................................... connection test
//       AppSetting app = new AppSetting();

//var yx = app.GetConnectionString("WDbContext");
//using (UnitOfWorkDapper db = new UnitOfWorkDapper())
//{
//var x = db.CallwebservicesettingDapperRepository.GetAllData();
//}

//using (WDbContext db = new WDbContext() )
//{
//var dd = db.Database.Connection.ConnectionString;
//var t = db.CallWebServiceSetting.ToList();
//}
//..................................................................




//string connectionString2 = ConfigurationManager.ConnectionStrings["WDbContext"].ConnectionString;
//int g = 0;
//using (IDbConnection db = new SqlConnection(connectionString2))
//{
//    var y = db.GetAll<lastnamad>().ToList();
//    g = y.Count;
//}
//using (IDbConnection db = new SqlConnection(connectionString2))
//{
//    var y = db.GetAll<lastnamad>().ToList();
//    g = y.Count;
//}

//string connectionString1 = ConfigurationManager.ConnectionStrings["WDbContext"].ConnectionString;
//int t = 0;
//using (IDbConnection db = new SqlConnection(connectionString1))
//{
//    var x = db.Query<lastnamad>(@"with tt as
// (
// select * , max(ReqdateTime) over(partition by name) as maxdate
//from[WDb_Repository].[dbo].[AllNamadInfo]
// )
//select * from tt where ReqDateTime = maxdate ").ToList();
//t = x.Count;
//}
//using (IDbConnection db = new SqlConnection(connectionString1))
//{
//var x = db.Query<lastnamad>(@"with ss as
//(
//select * from [WDb_Repository].[dbo].[AllNamadInfo]  where MiladiDate='2021-06-23 00:00:00.000' 
//),
//tt as
//(
//select * , max (ReqdateTime ) over (partition by name ) as maxdate
//from ss


//)


//select * from tt where ReqDateTime=maxdate ").ToList();
//t = x.Count;
//}

// string input ="اختيارخ تاپيكو-9000-00/07/18";


//input= input.Replace("ي", "ی").Replace("ك", "ک");
//var x = input.IndexOf("ی");

/*  getlastdata
 * public object getlastdata()
        {
            var qureylastdataallnamad = @"with ss as
(
select * from [WDb_1].[dbo].[AllNamadInfo] where ReqDateTime =
(select max(ReqDateTime) from [WDb_1].[dbo].[AllNamadInfo])
),
tt as
(
select *, max (ReqdateTime ) over (partition by name ) as maxdate
from ss
)
select * from tt where ReqDateTime = maxdate ";

            using (var db = new UnitOfWorkDapper())
            {
                var d = db.AllnamadDapperRepository.GetQureyData(qureylastdataallnamad).ToList();
                return d;
            }
        
        }
*/