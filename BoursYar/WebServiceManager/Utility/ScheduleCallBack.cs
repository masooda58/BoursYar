using System;
using DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Index = DAL.Index;

namespace WebServiceManager
{
    public class ScheduleCallBack
    {
   
        private readonly string _url;
        private  string _name; // for test
       
       
        public ScheduleCallBack(string url)
        {
            _url = url;
            
        }

        private void AllNamadInfoRead()
        {
            try
            {
                WebReadJson<List<AllNamadInfo>> webReadJson = new WebReadJson<List<AllNamadInfo>>(_url);
                Utilities utl = new Utilities();
                var allnamadinfo =
                    (List<AllNamadInfo>) utl.AddExtraData<List<AllNamadInfo>>(webReadJson.WebReadjsonResult());
               //CacheManager.CacheData("allnamadinfo",allnamadinfo);
               
                //var ops = allnamadinfo.Where(c => c.Name.StartsWith("ض") || c.Name.StartsWith("ط")).ToList();
                //// ops= همه نماده های شامل  اختیار
                //foreach (var namad in ops)
                //{
                //    allnamadinfo.Remove(namad);
                //}

                //var allnamadoption = JsonConvert.DeserializeObject<List<AllNamadOption>>(JsonConvert.SerializeObject(ops));
                // تبدیل کلاس به کلاس OPtion
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    // db.AllnamadOptionDapperRepository.AddDataList(allnamadoption);
                    db.AllnamadDapperRepository.AddDataList(allnamadinfo);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void AllNamadInfo_DailyRead()
        {
            try
            {
                WebReadJson<List<AllNamadInfo_Daily>> webReadJson = new WebReadJson<List<AllNamadInfo_Daily>>(_url);
                var allnamadobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");
                Utilities utl = new Utilities();
                var allnamadinfo =
                    (List<AllNamadInfo_Daily>) utl.AddExtraData<List<AllNamadInfo_Daily>>(allnamadobject);

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {

                    db.AllnamadDailydDapperRepository.AddDataList(allnamadinfo);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void AllNamadOptionRead()
        {
            try
            {
                WebReadJson<List<AllNamadOption>> webReadJson = new WebReadJson<List<AllNamadOption>>(_url);
                var allnamadoptionobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");
                Utilities utl = new Utilities();
                var allnamadinfo =
                    (List<AllNamadOption>) utl.AddExtraData<List<AllNamadOption>>(allnamadoptionobject);
                var allnamadoption = allnamadinfo.Where(c => c.Name.StartsWith("ض") || c.Name.StartsWith("ط")).ToList();
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {

                    db.AllnamadOptionDapperRepository.AddDataList(allnamadoption);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void market_bourseRead()
        {
            try
            {
                WebReadJson<Index> webReadJson = new WebReadJson<Index>(_url);
                var index = (Index) webReadJson.WebReadjsonResult(); // index 
                if (index == null) throw new ArgumentNullException("webReadJson.WebReadjsonResult()");


                Utilities utl = new Utilities();
                var bourse = (BourseIndex) utl.AddExtraData<BourseIndex>(index.Bourse);
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.BourseDapperRepository.AddData(bourse);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void market_farabourseRead()
        {
            try
            {
                WebReadJson<Index> webReadJson = new WebReadJson<Index>(_url);
                var index = (Index) webReadJson.WebReadjsonResult(); // index 
                if (index == null) throw new ArgumentNullException(" webReadJson.WebReadjsonResult()");


                Utilities utl = new Utilities();
                var farabourse = (BourseIndex) utl.AddExtraData<BourseIndex>(index.FaraBourse, "فرابورس");
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.BourseDapperRepository.AddData(farabourse);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void IndusteryIndexRead()
        {
            try
            {
                WebReadJson<List<IndusteryIndex>> webReadJson = new WebReadJson<List<IndusteryIndex>>(_url);

                var jsonobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");

                Utilities utl = new Utilities();
                var indextype =
                    (List<IndusteryIndex>) utl.AddExtraData<List<IndusteryIndex>>(jsonobject);
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.IndusteryDapperRepository.AddDataList(indextype);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void fav_namad_bourseRead()
        {
            try
            {
                WebReadJson<List<FavNamad>> webReadJson = new WebReadJson<List<FavNamad>>(_url);
                var jsonobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");



                Utilities utl = new Utilities();
                var favnamads = (List<FavNamad>) utl.AddExtraData<List<FavNamad>>(jsonobject);

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.FavdDapperRepository.AddDataList(favnamads);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void fav_namad_farabourseRead()
        {
            try
            {
                WebReadJson<List<FavNamad>> webReadJson = new WebReadJson<List<FavNamad>>(_url);
                var jsonobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");



                Utilities utl = new Utilities();
                var favnamads =
                    (List<FavNamad>) utl.AddExtraData<List<FavNamad>>(jsonobject, "فرابورس");

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.FavdDapperRepository.AddDataList(favnamads);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void ind_namad_bourseRead()
        {
            try
            {
                WebReadJson<List<IndNamad>> webReadJson = new WebReadJson<List<IndNamad>>(_url);
                var jsonobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");

                Utilities utl = new Utilities();
                var indnamads = (List<IndNamad>) utl.AddExtraData<List<IndNamad>>(jsonobject);

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.InDapperRepository.AddDataList(indnamads);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void ind_namad_farabourseRead()
        {
            try
            {
                WebReadJson<List<IndNamad>> webReadJson = new WebReadJson<List<IndNamad>>(_url);

                var jsonobject = webReadJson.WebReadjsonResult() ?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");


                Utilities utl = new Utilities();
                var indnamads =
                    (List<IndNamad>) utl.AddExtraData<List<IndNamad>>(jsonobject, "فرابورس");

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.InDapperRepository.AddDataList(indnamads);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void CodalRead()
        {
            try
            {
                WebReadJson<List<Codal>> webReadJson = new WebReadJson<List<Codal>>(_url);

                var codals = (List<Codal>) webReadJson.WebReadjsonResult();
                if (codals == null) throw new ArgumentNullException(" webReadJson.WebReadjsonResult()");

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.CodalDapperRepository.DeleteAllData();
                    db.CodalDapperRepository.AddDataList(codals);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void PayamnazerRead()
        {
            try
            {
                WebReadJson<List<PayamNazer>> webReadJson = new WebReadJson<List<PayamNazer>>(_url);

                var payamNazers = (List<PayamNazer>)webReadJson.WebReadjsonResult();
                if (payamNazers == null) throw new ArgumentNullException("webReadJson.WebReadjsonResult()");

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.PayamnazeRepository.DeleteAllData();
                    db.PayamnazeRepository.AddDataList(payamNazers);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
     
        }

        private void KhodroRead()
        {
            try
            {
                WebReadJson<List<Khodro>> webReadJson = new WebReadJson<List<Khodro>>(_url);

                var khodros = (List<Khodro>)webReadJson.WebReadjsonResult();
                if (khodros == null) throw new ArgumentNullException("webReadJson.WebReadjsonResult()");

                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.KhodrodDapperRepository.DeleteAllData();
                    db.KhodrodDapperRepository.AddDataList(khodros);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        

        }

        private void CryptoRead()
        {
            try
            {
                WebReadJson<CryptoAll> webReadJson = new WebReadJson<CryptoAll>(_url);
                var index = (CryptoAll) webReadJson.WebReadjsonResult();
                if (index == null) throw new ArgumentNullException("webReadJson.WebReadjsonResult()");


                Utilities utl = new Utilities();
                var cryptoes = (List<Crypto>) utl.AddExtraData<List<Crypto>>(index.Data);
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {
                    db.CryptoDapperRepository.AddDataList(cryptoes);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void ArzRead()
        {
            try
            {
                WebReadJson<LArz> webReadJson = new WebReadJson<LArz>(_url);
                var jArz = (LArz) webReadJson.WebReadjsonResult();
                if (jArz == null) throw new ArgumentNullException("webReadJson.WebReadjsonResult()");
                Utilities utl = new Utilities();
                var arzes = (List<Arz>) utl.AddExtraData<List<Arz>>(jArz.data);
                using (UnitOfWorkDapper db = new UnitOfWorkDapper())
                {

                    db.ArzdDapperRepository.AddDataList(arzes);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
        }

        private void testaction()// for test
        {
            var f = DateTime.Now.ToString();
           
            //MessageBox.Show("next"+Program.ScheduleRuns[_name].MilisecondWaitForNewStart().ToString());
            Thread.Sleep(2000);
        }

        public Action CallActionBack(string nameOfSetting)
        {
            switch (nameOfSetting)
            {
                case "allnamadinfo":
                    return AllNamadInfoRead;
                   
                case "market_bourse":
                    return market_bourseRead;
                     
                case "market_farabourse":
                    return market_farabourseRead;
                     
                case "industeryindex":
                    return IndusteryIndexRead;
                     
                case "fav_namad_bourse":
                    return fav_namad_bourseRead;
                     
                case "fav_namad_farabourse":
                    return fav_namad_farabourseRead;
                     
                case "ind_namad_bourse":
                    return ind_namad_bourseRead;
                     
                case "ind_namad_farabourse":
                    return ind_namad_farabourseRead;
                     
                case "codal":
                    return CodalRead;
                     
                case "payamnazer":
                    return PayamnazerRead;
                     
                case "khodro":
                    return KhodroRead;
                     
                case "crypto":
                    return CryptoRead;
                     
                case "arz":
                    return ArzRead;
                     
                case "allnamadinfo_daily":
                    return AllNamadInfo_DailyRead;
                     
                case "allnamadoption":
                    return AllNamadOptionRead;
                     

            }
            _name = nameOfSetting;
            return testaction;
        }
        private void RefreshData<T>(T input, string cachename )
        {
            var cachedata = (T) input;
            // our expiry handler
           // onRemove = new CacheItemRemovedCallback(this.RemovedCallback);
    
            // connect to the webservice and get the data
         
    
            // add the data to the cache, setting an expiry time of two mins
            //Program.HttpRuntime.Cache.Add(cachename, cachedata, 
            //    null, DateTime.Now.AddMinutes(20), TimeSpan.Zero, 
            //    CacheItemPriority.High, null);
        }
    }
}
