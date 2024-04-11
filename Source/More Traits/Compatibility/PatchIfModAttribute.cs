using System;
using System.Linq;
using Verse;

namespace Garthor_More_Traits.Compatibility;

internal class PatchIfModAttribute(string modPackageID) : Attribute
{
    public bool IsModLoaded()
    {
        return ModLister.AllInstalledMods.Any(x => x.Active && x.SamePackageId(modPackageID));
    }
}