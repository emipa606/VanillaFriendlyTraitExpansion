using System;
using System.Linq;
using Verse;

namespace Garthor_More_Traits.Compatibility
{
    // Token: 0x0200002B RID: 43
    internal class PatchIfModAttribute : Attribute
    {
        // Token: 0x04000024 RID: 36
        private readonly string modPackageID;

        // Token: 0x06000049 RID: 73 RVA: 0x00003E20 File Offset: 0x00002020
        public PatchIfModAttribute(string modPackageID)
        {
            this.modPackageID = modPackageID;
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00003E34 File Offset: 0x00002034
        public bool IsModLoaded()
        {
            return ModLister.AllInstalledMods.Any(x => x.Active && x.SamePackageId(modPackageID));
        }
    }
}