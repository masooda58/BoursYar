using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Authoriz.ClaimBaseAuthoriz.MvcUserAccessClaims
{
    public static class HomeControllerClaimValues
    {
        public const string Test = nameof(Test);
        public const string TestPersian = "دسترسی به  تست";
        public const string AddClaimToRole = nameof(AddClaimToRole);
        public const string AddClaimToRolePersian = " دسترسی به اضافه کردن  claim  به  Role";

        public static readonly ReadOnlyCollection<(string ClaimValueEnglish, string ClaimValuePersian)> AllHomeClaimValues;
         
        static HomeControllerClaimValues()
        {
            AllHomeClaimValues = MvcUserClaimValueUtilities
                .GetPersianEnglishClaimVAlue(typeof(HomeControllerClaimValues)).
                ToList()
                .AsReadOnly();
        }
    }
}
