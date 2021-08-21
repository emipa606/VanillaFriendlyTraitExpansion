using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200001D RID: 29
    [HarmonyPatch(typeof(PawnInventoryGenerator), "GiveDrugsIfAddicted")]
    public static class PawnInventoryGenerator_GiveDrugsIfAddicted_Patch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x00003434 File Offset: 0x00001634
        private static void Postfix(Pawn p)
        {
            if (p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Drunken_Master) != true)
            {
                return;
            }

            if (p.health.hediffSet.GetHediffs<Hediff_Addiction>().Any(x => x.Chemical == ChemicalDefOf.Alcohol))
            {
                return;
            }

            var alcoholicItems = DefDatabase<ThingDef>.AllDefsListForReading.Where(delegate(ThingDef x)
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
            if (!(from x in alcoholicItems
                where p.Faction == null || x.techLevel <= p.Faction.def.techLevel
                select x).TryRandomElement(out var def))
            {
                return;
            }

            var stackCount = Rand.RangeInclusive(Mathf.RoundToInt(0f * p.BodySize),
                Mathf.RoundToInt(2f * p.BodySize));
            var thing = ThingMaker.MakeThing(def);
            thing.stackCount = stackCount;
            p.inventory.TryAddItemNotForSale(thing);
        }
    }
}