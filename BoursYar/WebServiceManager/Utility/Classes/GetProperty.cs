using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WebServiceManager
{
    public class GetProperty
    {
        /*
         * دریافت
         *properties
         * از کلاسهای مختلف
         * و نمایش نام یا
         * attribiuteDisplyName آن
         */
        public static List<string> GetFeildName<T>(List<T> listData) where T : class
        {
            IList<string> fieldNames = Array.ConvertAll<PropertyInfo, String>(typeof(T).GetProperties(),
                delegate (PropertyInfo fo)
                {


                    return fo.Name;

                });
            return fieldNames.ToList();
            // Do something with the fieldNames array....
        }
        public static List<string> GetFeildDisplayNameAttribute<T>(List<T> listData) where T : class
        {
            IList<string> fieldNames = Array.ConvertAll<PropertyInfo, String>(typeof(T).GetProperties(),
                delegate (PropertyInfo fo)
                {


                    return fo.GetCustomAttributes<DisplayNameAttribute>().ToList().Count == 0
                        ? fo.Name
                        : fo.GetCustomAttributes<DisplayNameAttribute>().ToList().FirstOrDefault().DisplayName;



                });
            return fieldNames.ToList();
            // Do something with the fieldNames array....
        }

        public static List<CBT> GetDictionaryOfName<T>(List<T> listData) where T : class
        {
            List<string> feildname = GetFeildName(listData);
            List<string> feildattribut = GetFeildDisplayNameAttribute(listData);
            List<CBT> returndictionary = new List<CBT>();
            for (int i = 0; i < feildname.Count - 1; i++)
            {
                returndictionary.Add(new CBT()
                {
                    key = feildname[i],
                    Value = feildattribut[i]
                });
            }

            return returndictionary;
        }
    }

    public class CBT
    {
        public string key { get; set; }
        public string Value { get; set; }
    }
}
