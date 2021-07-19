using System;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace DAL
{
    public class ParseStringConverter : JsonConverter
        /* برای فیلد های موجود در کلاس که خروجی آنها از نوع لانگ است 
         *  
         *  استفاده می شود درواقع اگر نوع اصلی دابل یا لانگ باشد که در 
        که در جیسان استرینگ ذخیره شده باشد این تابع کار تبدیل را انجام می دهد 
        */
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        //استفاده می شود Allnamadinfooption  به AllNamadinfo این بخش برای تبدیل کلاس
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }


        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
           
            long g = 0;
           // if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            int megdar = 1;
            long l;
            double x;
            if (string.IsNullOrEmpty(value))
            {
                return g;
            }
            if (value.Contains('B'))
            {
                value = value.Trim('B');
                megdar = 1000000000;
            }
            if (value.Contains('M'))
            {
                value = value = value.Trim('M');
                megdar = 1000000;
            }


            if (Int64.TryParse(value,NumberStyles.Number, CultureInfo.CurrentCulture,out l))
            {
                return l*megdar;
            }
            else if (double.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.CurrentCulture, out x) ||
                     //Then try in US english
                     double.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out x) ||
                     //Then in neutral language
                     double.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture, out x))
            {
                return x*megdar;
            }

            throw new Exception("Cannot unmarshal type long");
        }
    }
    public class ParseStringConverterDouble : JsonConverter
        /* برای فیلد های موجود در کلاس که خروجی آنها از نوع لانگ است 
         *  
         *  استفاده می شود درواقع اگر نوع اصلی دابل یا لانگ باشد که در 
        که در جیسان استرینگ ذخیره شده باشد این تابع کار تبدیل را انجام می دهد 
        */
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {

         double g = 0;
            // if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            int megdar = 1;
            double x;
            if (string.IsNullOrEmpty(value))
            {
                return g;
            }
            if (value.Contains('B'))
            {
                value = value.Trim('B');
                megdar = 1000000000;
            }

            if (value.Contains('M'))
            {
                value = value = value.Trim('M');
                megdar = 1000000;
            }

            if (double.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.CurrentCulture, out x) ||
                     //Then try in US english
                     double.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out x) ||
                     //Then in neutral language
                     double.TryParse(value, System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture, out x))
            {
                return x*megdar;
            }

            throw new Exception("Cannot unmarshal type long");
        }
    }
}
