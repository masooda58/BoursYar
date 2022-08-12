using BoursYar.Authorization.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BoursYar.Authorization.Utilities
{
 
    /// <summary>
    /// پیاده سازی <see cref="IClaimBaseAuthorizationUtilities"/>
    /// </summary>
    public class ClaimBaseAuthorizationUtilities : IClaimBaseAuthorizationUtilities
    {
        private readonly IMvcUtilities _mvcUtilities;
        // می خواهد دسترسی داشته باشد action کاربر به کدام
        /// <summary>
        /// سازنده جهت مقدار دهی اولیه
        /// </summary>
        /// <param name="mvcUtilities">IMvcUtilities نمونه ای از </param>
        public ClaimBaseAuthorizationUtilities(IMvcUtilities mvcUtilities)
        {
            _mvcUtilities = mvcUtilities;
        }
        /// <summary>
        /// <inheritdoc cref="IClaimBaseAuthorizationUtilities"/>
        /// </summary>
        /// <remarks>
        /// ابتدا اطلاعات مسیر با استفاده از کد زیر می گیرد
        /// <code>
        /// var areaName = httpContext.GetRouteValue("area")?.ToString();
        /// var controllerName = httpContext.GetRouteValue("controller")?.ToString();
        /// var actionName = httpContext.GetRouteValue("action")?.ToString();
        /// </code>
        /// سپس با استفاده از این کد
        /// <code>
        /// var mvcName = _mvcUtilities.ActionThatRequireClaimBaseAuthorazition.TryGetValue(
        /// new MvcNamesModel(actionName, controllerName, areaName),
        /// out var actualMvc);
        /// return actualMvc?.ClaimToAuthoriz;
        /// </code>
        /// </remarks>
        /// <example>
        /// <code>
        /// [BoursYarAuthoriz(ClaimToAuthoriz="Action name")]
        /// Public IActionResult test(){....}
        ///  </code>
        ///  را بر می گرداند "Action name" برای مسیر فوق
        /// </example>
        /// <param name="httpContext">HttpContext</param>
        /// <returns>
        ///  یک مسیر مشخص ذکر شده است attribute که در Claim مقدار
        /// </returns>
        public string GetClaimToAuthorize(HttpContext httpContext)
        {
            var areaName = httpContext.GetRouteValue("area")?.ToString();
            var controllerName = httpContext.GetRouteValue("controller")?.ToString();
            var actionName = httpContext.GetRouteValue("action")?.ToString();

            // chon dastorat linq roi Ienumerable Ha ka mikonad va roi Hashset kar nemikonad khat zir comment shode

            /* IEnumerable
            var mvcName= _mvcUtilities.ActionThatRequireClaimBaseAuthorazition
               .SingleOrDefault(action =>
              action.AreaName == areaName && action.ActionName == actionName &&
                  action.ControllerName == controllerName);
            
            //return mvcName?.ClaimToAuthoriz;
           */

            #region HashSet

            var mvcName = _mvcUtilities.ActionThatRequireClaimBaseAuthorazition.TryGetValue(
                new MvcNamesModel(actionName, controllerName, areaName),
                out var actualMvc);

            #endregion




            return actualMvc?.ClaimToAuthoriz;

        }
    }
}
