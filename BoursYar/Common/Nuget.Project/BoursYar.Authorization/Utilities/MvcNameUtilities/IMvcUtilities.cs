using System.Collections.Immutable;

namespace BoursYar.Authorization.Utilities.MvcNameUtilities
{
    /// <summary>
    ///  استفاده می شود ACtins , مثل Mvc جهت ذخیره اجزا 
    /// </summary>
    public interface IMvcUtilities
    {
        //hashset yek list bedon Index ba sorat ballast
        // Immutable bari in ast ke list(hasset) bad az sakht taghier nakonad
        // barai zakhireh hame action ha area ha va... estefadeh mishavad
        /// <summary>
        /// // های یک پروژه Area،Controller,Actions اطلاعات کل
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }
        // list action hai keh bari dastresi be system dastrsi daynamic niyaz darand
        /// <summary>
        /// را دارد Auttribut ,ClaimBase که MVC اطلاعات
        /// </summary>
        public ImmutableHashSet<MvcNamesModel> ActionThatRequireClaimBaseAuthorazition { get; }

    }
}
