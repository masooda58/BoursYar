using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace WebServiceManager
{
    public class Utilities
    {
        //را مقدار دهی می کند  ShamsiDate وREqDateTime, MiladiDate  این تابع فیلد های

        public object AddExtraData<TClass>(object tClass, string market = "بورس", string shamsiDate = null, DateTime? cryptoAllDate = null) where TClass : class
        {
            if (typeof(TClass) == typeof(List<AllNamadInfo>))
            {
                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }

                foreach (var item in (List<AllNamadInfo>)tClass)
                {
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(List<AllNamadInfo_Daily>))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }

                foreach (var item in (List<AllNamadInfo_Daily>)tClass)
                {
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(List<AllNamadOption>))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }

                foreach (var item in (List<AllNamadOption>)tClass)
                {
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(List<FavNamad>))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }

                foreach (var item in (List<FavNamad>)tClass)
                {
                    item.Market = market;
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(List<IndNamad>))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }

                foreach (var item in (List<IndNamad>)tClass)
                {
                    item.Market = market;
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(List<IndusteryIndex>))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }

                foreach (var item in (List<IndusteryIndex>)tClass)
                {
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(BourseIndex))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi
                if (shamsiDate != null)
                {
                    ds = shamsiDate;
                    dm = shamsiDate.ToMiladi();
                }
                var item = (BourseIndex)tClass;
                item.Market = market;
                if (item != null)
                {
                    item.ReqDateTime = dd;
                    item.MiladiDate = dm;
                    item.ShamsiDate = ds;

                }

                return tClass;
            } // فرابورس و بورس 
            else if (typeof(TClass) == typeof(List<Crypto>))//add date
            {



                foreach (var item in (List<Crypto>)tClass)
                {
                    if (!cryptoAllDate.HasValue)
                    {
                        cryptoAllDate = DateTime.Now;
                    }
                    if (item != null)
                    {

                        item.Date = cryptoAllDate.Value;

                    }
                }
                return tClass;
            }
            else if (typeof(TClass) == typeof(List<Arz>))
            {

                DateTime dd = DateTime.Now;
                string ds = dd.ToShamsi(); // date shamsi
                DateTime dm = ds.ToMiladi(); //date miladi


                foreach (var item in (List<Arz>)tClass)
                {
                    if (item != null)
                    {
                        item.ReqDateTime = dd;
                        item.MiladiDate = dm;
                        item.ShamsiDate = ds;

                    }
                }
                return tClass;
            }
            return null;
        }
        public bool TestInternetConnection()
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
                logger.Name = "main";
                logger.Status = "اتصال اینترنت بر قرار نیست ";
                logger.Success = false;
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.LoggerdDapperRepository.AddData(logger);
                }
                return false;
            }
        }

        public Task<bool> CheckDbConnection(string dbconnectionstring = "WDbContext")
        {

            return Task.Run(() =>
             {

                 try
                 {
                     string connectionString = dbconnectionstring;
                     //ConfigurationManager.ConnectionStrings["WDbContext"].ConnectionString;

                     IDbConnection db = new SqlConnection(connectionString);
                     db.Open();
                     db.Close();
                     return true;
                 }
                 catch (Exception e)
                 {
                     return false;
                 }
             });

        }

        public string GetConnectionString(string StringName)
        {
            string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();

            string connectionStringIs = configuration.GetConnectionString("StringName");
            return connectionStringIs;
        }
    }

}
