using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[StaticConstructorOnStartup]
public static class GMT_Drunken_Master_Helper
{
    internal static List<ThingDef> alcoholic_items;

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