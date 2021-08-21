using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000019 RID: 25
    [StaticConstructorOnStartup]
    public static class GMT_Drunken_Master_Helper
    {
        // Token: 0x0400001F RID: 31
        internal static List<ThingDef> alcoholic_items;

        // Token: 0x0600002F RID: 47 RVA: 0x00002F20 File Offset: 0x00001120
        static GMT_Drunken_Master_Helper()
        {
            alcoholic_items = DefDatabase<ThingDef>.AllDefsListForReading.Where(delegate(ThingDef x)
            {
                bool result;
                if (x.category != ThingCategory.Item)
                {
                    result = false;
                }
                else
                {
                    var compProperties = x.GetCompProperties<CompProperties_Drug>();
                    result = compProperties != null && compProperties.chemical == ChemicalDefOf.Alcohol;
                }

                return result;
            }).ToList();
        }
    }
}