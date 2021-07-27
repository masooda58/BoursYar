using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using DAL;
using Newtonsoft.Json;
using PersianTools.Core;

namespace WebServiceManager
{
    public class InitialDailyHistoryData
    {
        // دیتای روزانه اولیه
        public static void DailyNamdeInfo()
        {

            var pdt = new PersianDateTime(1399, 12, 08);
            var panShanbe = new PersianDateTime(1400, 02, 9);
           
            for (int i = 1; i <= 50; i++)
            {

                pdt = pdt.AddDays(-1);
                if (pdt.IsHoliDay || pdt.DayOfWeek == panShanbe.DayOfWeek || (pdt.Day == 30 && pdt.Month == 12))
                {
                    continue;
                    // MessageBox.Show($"تعطیل است" + pdt.ToString());
                }
                else
                {
                    try
                    {
                        string Url = "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9" +
                                     "&all&type=0&time=" + pdt.ShamsiDate;
                        WebReadJson<List<AllNamadInfo_Daily>> webReadJson = new WebReadJson<List<AllNamadInfo_Daily>>(Url);
                        var allnamadobject = webReadJson.WebReadjsonResult()?? throw new ArgumentNullException("webReadJson.WebReadjsonResult()");
                        Utilities utl = new Utilities();
                        var allnamadinfo =
                            (List<AllNamadInfo_Daily>) utl.AddExtraData<List<AllNamadInfo_Daily>>(allnamadobject,shamsiDate:pdt.ShamsiDate);

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
            }
        }
    }
}