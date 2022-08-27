using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Jwt.Identity.BoursYarServer.Helpers.Extensions
{

    public static class TempDataExtensions
    {
        /// <summary>
        /// set class to TempData
        /// </summary>
        /// <typeparam name="T"> class name</typeparam>
        /// <param name="tempData">temptData Name</param>
        /// <param name="key">key name</param>
        /// <param name="value"> class value</param>
        /// <remarks>
        /// <example>
        /// <code>
        /// var contact = new Contact { FirstName = "Masoud", Domain = "BoursYar.com" };
        /// TempData.Set("MM", contact);
        /// var mike = TempData.Get&lt;Contact&gt;("MM");
        /// </code>
        /// </example>
        /// </remarks>
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
