using System;
using System.Linq;
using Verse;

namespace Garthor_More_Traits.Compatibility;

internal class PatchIfModAttribute : Attribute
{
    private readonly string modPackageID;

    public PatchIfModAttribute(string modPackageID)
    {
        this.modPackageID = modPackageID;
    }

    public bool IsModLoaded()
    {
        return ModLister.AllInstalledMods.Any(x => x.Active && x.SamePackageId(modPackageID));
    }
}