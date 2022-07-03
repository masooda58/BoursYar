using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityApi.Authoriz.ClaimBaseAuthoriz.MvcUserAccessClaims
{
    public static class MvcUserClaimValueUtilities
    {
        public static IEnumerable<(string ClaimValueEnglish, string ClaimVAluePersian)> GetPersianEnglishClaimVAlue(
            Type type)
        {
            var allConstentInTheType = type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(feild => feild.IsLiteral || !feild.IsInitOnly).ToList();
            foreach(var englishClaimValue in allConstentInTheType.Where(m=>!m.Name.Contains("Persian")))
            {
                var persianClaimValue = allConstentInTheType.Single(m => m.Name == englishClaimValue.Name + "Persian");
                yield return (englishClaimValue.GetValue(null)!.ToString(),
                    persianClaimValue.GetValue(null)!.ToString());
            }
        }
    }
}
