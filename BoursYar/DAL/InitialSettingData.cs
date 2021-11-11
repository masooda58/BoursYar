﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;


namespace DAL
{
    public class InitialSettingData
    {
        public static  List<CallWebServiceSetting>  Seed()
        {
            List<CallWebServiceSetting> defaultCallWebServiceSettings = new List<CallWebServiceSetting>();

            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               

                Code = 1,
              Faal=false,
                Name = "AllNamadInfo",
                StartTime = "09:00:00",
                FinishTime = "12:30:00",
                Interval = 120,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&all&type=0",
                ClassType = typeof(List<AllNamadInfo>).FullName,
                ClassJsonType = typeof(List<AllNamadInfo>).FullName

            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 2,
              Faal=false,
                Name = "market_bourse",
                NeedAddDate = true,
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=market_bourse",
                ClassType = typeof(BourseIndex).FullName,
                ClassJsonType = typeof(Index).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 3,
              Faal=false,
                Name = "market_farabourse",
                NeedAddDate = true,
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=market_farabourse",
                ClassType = typeof(BourseIndex).FullName,
                ClassJsonType = typeof(Index).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 4,
              Faal=false,
                Name = "IndusteryIndex",
                NeedAddDate = true,
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=indices",
                ClassType = typeof(List<IndusteryIndex>).FullName,
                ClassJsonType = typeof(List<IndusteryIndex>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 5,
              Faal=false,
                Name = "fav_namad_bourse",
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=fav_namad_bourse",
                ClassType = typeof(List<FavNamad>).FullName,
                ClassJsonType = typeof(List<FavNamad>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 6,
              Faal=false,
                Name = "fav_namad_farabourse",
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=fav_namad_farabourse",
                ClassType = typeof(List<FavNamad>).FullName,
                ClassJsonType = typeof(List<FavNamad>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 7,
              Faal=false,
                Name = "ind_namad_bourse",
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=ind_namad_bourse",
                ClassType = typeof(List<IndNamad>).FullName,
                ClassJsonType = typeof(List<IndNamad>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 8,
              Faal=false,
                Name = "ind_namad_farabourse",
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&market=ind_namad_farabourse",
                ClassType = typeof(List<IndNamad>).FullName,
                ClassJsonType = typeof(List<IndNamad>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 9,
              Faal=false,
                Name = "Codal",
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                NeedAddDate = false,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&codal=all&p=1",
                ClassType = typeof(List<Codal>).FullName,
                ClassJsonType = typeof(List<Codal>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 10,
              Faal=false,
                Name = "Payamnazer",
                StartTime = "14:50:00",
                FinishTime = "16:55:00",
                Interval = 120,
                NeedAddDate = false,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&inspect=all",
                ClassType = typeof(List<PayamNazer>).FullName,
                ClassJsonType = typeof(List<PayamNazer>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 11,
              Faal=false,
                Name = "Khodro",
                StartTime = "14:50:00",
                FinishTime = "18:30:00",
                Interval = 3600,
                NeedAddDate = false,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&car=all",
                ClassType = typeof(List<Khodro>).FullName,
                ClassJsonType = typeof(List<Khodro>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 12,
              Faal=false,
                Name = "Crypto",
                StartTime = "14:50:00",
                FinishTime = "18:30:00",
                Interval = 3600,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&crypto_v2=all",
                ClassType = typeof(CryptoAll).FullName,
                ClassJsonType = typeof(List<Crypto>).FullName
            }); defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {
               
                Code = 13,
              Faal=false,
                Name = "Arz",
                StartTime = "14:50:00",
                FinishTime = "18:30:00",
                Interval = 3600,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&currency",
                ClassType = typeof(List<Arz>).FullName,
                ClassJsonType = typeof(List<Arz>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {

                Code = 14,
              Faal=false,
                Name = "AllNamadInfo_Daily",
                StartTime = "14:50:00",
                FinishTime = "18:30:00",
                Interval = 3600,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" +"&all&type=0",
                ClassType = typeof(List<AllNamadInfo_Daily>).FullName,
                ClassJsonType = typeof(List<AllNamadInfo_Daily>).FullName
            });
            defaultCallWebServiceSettings.Add(new CallWebServiceSetting()
            {

                Code = 15,
              Faal=false,
                Name = "AllNamadOption",
                StartTime = "14:50:00",
                FinishTime = "18:30:00",
                Interval = 3600,
                NeedAddDate = true,
                Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" + "&all&type=1",
                ClassType = typeof(List<AllNamadOption>).FullName,
                ClassJsonType = typeof(List<AllNamadOption>).FullName
            });
            return defaultCallWebServiceSettings;
        }

        public static string GetConnectionString(string StringName)
        {
            string c = Directory.GetCurrentDirectory();
            string MyFilePath = Path.Combine(c, "appsettings.json");
            //IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
            JObject data = JObject.Parse(File.ReadAllText(MyFilePath));
            string connectionStringIs = data["ConnectionStrings"][StringName].ToString();
            return connectionStringIs;
        }
    }
}
