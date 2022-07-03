using System.Collections.Immutable;

namespace BoursYarAuthorization.Utilities.MvcNameUtilities
{
    public interface IMvcUtilities
    {
        //hashset yek list bedon Index ba sorat ballast
        // Immutable bari in ast ke list(hasset) bad az sakht taghier nakonad
        // barai zakhireh hame action ha area ha va... estefadeh mishavad
        public ImmutableHashSet<MvcNamesModel> MvcInfo { get; }
        // list action hai keh bari dastresi be system dastrsi daynamic niyaz darand
        public ImmutableHashSet<MvcNamesModel> ActionThatRequireClaimBaseAuthorazition { get;}

    }
}
