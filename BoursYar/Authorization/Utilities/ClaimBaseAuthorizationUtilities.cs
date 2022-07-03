using BoursYarAuthorization.Utilities.MvcNameUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BoursYarAuthorization.Utilities
{
    public class ClaimBaseAuthorizationUtilities:IClaimBaseAuthorizationUtilities
    {
        private readonly IMvcUtilities _mvcUtilities;
        // می خواهد دسترسی داشته باشد action کاربر به کدام
        public ClaimBaseAuthorizationUtilities(IMvcUtilities mvcUtilities)
        {
            _mvcUtilities = mvcUtilities;
        }

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
