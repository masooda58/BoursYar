using System;

namespace BoursYar.Authorization.Utilities.MvcNameUtilities
{
    /// <summary>
    ///   MVC در Rout مدل یک
    /// </summary>
    // می کند OverrRide را Equal یک اینتر فیس است که متد IEquatable
    public class MvcNamesModel : IEquatable<MvcNamesModel>
    {
   
        /// <summary>
        /// Area نام
        /// </summary>
        public string AreaName { get; }
        /// <summary>
        /// Controller نام
        /// </summary>
        public string ControllerName { get; }
        /// <summary>
        /// Actions نام
        /// </summary>
        public string ActionName { get; }
        /// <summary>
        /// مورد نیاز برای دسترسی Claim
        /// </summary>
        public string ClaimToAuthoriz { get; }
        /// <summary>
        ///  مورد نظر را دارد یا خیر Attribute آیا یک جز
        /// </summary>
        public bool IsClaimBaseAuthoraztionRequired { get; }
        /// <summary>
        /// سازنده اول کلاس
        /// </summary>
        /// <param name="claimToAuthoriz"> claimToAuthoriz</param>
        /// <param name="actionName">actionName</param>
        /// <param name="controllerName">controllerName</param>
        /// <param name="areaName">areaName اگر نباشد مقدار خالی را نگه می دارد</param>
        public MvcNamesModel(string claimToAuthoriz, string actionName, string controllerName, string areaName)
        {
            IsClaimBaseAuthoraztionRequired = !string.IsNullOrWhiteSpace(claimToAuthoriz);
            ClaimToAuthoriz = claimToAuthoriz;
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
        }
        /// <summary>
        /// سازنده دوم کلاس
        /// </summary>
        /// <remarks>
        /// این سازنده جهت جستجو استفاده می شود
        /// </remarks>
        /// <param name="actionName">actionName</param>
        /// <param name="controllerName">controllerName</param>
        /// <param name="areaName">areaName</param>
        public MvcNamesModel(string actionName, string controllerName, string areaName)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
            IsClaimBaseAuthoraztionRequired = false;
        }
        // GetTry استفاده می شود برای Hasset برای گرفتن اطلاعات داخل Region این
        #region GetHashset
        /// <summary>
        ///  GetTry استفاده می شود برای Hasset برای گرفتن اطلاعات داخل Region این
        /// </summary>
        /// <param name="other"> ورودی از جنس <c>MvcNamesModel</c></param>
        /// <returns></returns>
        public bool Equals(MvcNamesModel other)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(other, null)) return false;

            // Optimization for a common success case.
            if (ReferenceEquals(this, other)) return true;

            // If run-time types are not exactly the same, return false.
            if (GetType() != other.GetType()) return false;

            return AreaName == other.AreaName
                   && ControllerName == other.ControllerName
                   && ActionName == other.ActionName;
        }
        /// <summary>
        /// تبدیل آبجکت
        /// </summary>
        /// <param name="obj"> یک آبجکت است</param>
        /// <returns>bool Equals</returns>

        public override bool Equals(object obj)
        {
            return Equals(obj as MvcNamesModel);
        }
        /// <summary>
        /// // با پارمترهای آن اجرا می شود Equals وقتی این تابع فراخوان می شود
        /// </summary>
        /// 
        /// <returns> function Equals(obj) </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(AreaName, ControllerName, ActionName);
        }

        #endregion

    }
}
