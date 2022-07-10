
using DAL;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;


namespace WebServiceManager
{
    public class WebReadJson<TJson> where TJson : class // read json to class 

    {

        private string _url;
        private int _waittime;

        public WebReadJson(string url, int waitTime = 5000)
        {
            _url = url;
            _waittime = waitTime;
        }

        // نیست و باید  Null حاوی خطا مالک وب سرویس باشد خروجی  Json بر می گرداند در صورتی که فایل  NULL در صورت عدم توانای در خواند فایل مقدار
        //خارج از کلاس هندل شود ERR پس از دریافت  فایل حاوی
        public object WebReadjsonResult()
        {
            lock (StaticDictionary.ThreadLockToken)
            {
                if (TestInternetConnection())
                {
                    using (HttpClient client = new HttpClient())
                    {
                        DateTime reqDateTime = DateTime.Now;
                        string _name = typeof(TJson).FullName;
                        string stauts = "";
                        bool success = true;

                        try
                        {
                            Thread.Sleep(_waittime);
                            var res = client.GetStringAsync(_url).Result;
                            if (res == null)
                            {
                                stauts = "خروجی فایل جیسون خالی است";

                                return null;
                            }

                            var settings = new JsonSerializerSettings
                            {
                                Error = (obj, args) =>
                                {
                                    var contextErrors = args.ErrorContext;
                                    contextErrors.Handled = true;
                                }
                            };
                            success = true;
                            stauts = "فایل جیسون بدرستی دریافت شد";

                            return JsonConvert.DeserializeObject<TJson>(res, settings);

                        }
                        catch (Exception)
                        {
                            success = false;
                            stauts = "فایل جیسون بدرستی  دی سریال نشده است";

                            return null;
                        }
                        finally
                        {
                            Logger logger = new Logger();
                            logger.Name = _name;
                            logger.ReqTime = reqDateTime;
                            logger.Status = stauts;
                            logger.Success = success;

                            //Program.LogbBox.Text += string.Format("{0} name = {1}*****reqTime ={2}*****status= {3}", Environment.NewLine, _name,reqDateTime.ToString(@"T"),success);
                            using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                            {
                                db.LoggerdDapperRepository.AddData(logger);
                            }

                        }

                    }
                }
                else
                {
                    return null;
                }

            }
        }

        private bool TestInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                Logger logger = new Logger();
                logger.ReqTime = DateTime.Now;
                logger.Name = typeof(TJson).FullName;
                logger.Status = "اتصال اینترنت بر قرار نیست ";
                logger.Success = false;
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.LoggerdDapperRepository.AddData(logger);
                }
                return false;
            }
        }
    }
}


