using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Pawn_InventoryTracker), "FindCombatEnhancingDrug")]
public class Pawn_InventoryTracker_FindCombatEnhancingDrug_Patch
{
    private static bool Prepare()
    {
        var method = typeof(Pawn_InventoryTracker).GetMethod("FindCombatEnhancingDrug",
            BindingFlags.Instance | BindingFlags.Public);
        return !(method == null || method.HasAttribute<ObsoleteAttribute>());
    }

    private static void Postfix(Pawn_InventoryTracker __instance, ref Thing __result)
    {
        var pawn = __instance.pawn;
        if (pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Drunken_Master) != true)
        {
            return;
        }

        var i = 0;
        while (i < __instance.innerContainer.Count)
        {
            var thing = __instance.innerContainer[i];
            var compDrug = thing.TryGetComp<CompDrug>();
            if (compDrug != null && compDrug.Props.chemical == ChemicalDefOf.Alcohol)
            {
                if (thing.def.ingestible.outcomeDoers.Find(
                        x => (x is IngestionOutcomeDoer_GiveHediff ingestionOutcomeDoer_GiveHediff2
                            ? ingestionOutcomeDoer_GiveHediff2.hediffDef
                            : null) == HediffDefOf.AlcoholHigh) is IngestionOutcomeDoer_GiveHediff
                    ingestionOutcomeDoer_GiveHediff)
                {
                    var hediff = HediffMaker.MakeHediff(ingestionOutcomeDoer_GiveHediff.hediffDef, pawn);
                    hediff.Severity = ingestionOutcomeDoer_GiveHediff.severity;
                    if (!pawn.health.WouldBeDownedAfterAddingHediff(hediff))
                    {
                        __result = thing;
                        break;
                    }
                }
            }

            i++;
        }
    }
}