using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(JobGiver_TakeCombatEnhancingDrug), "FindCombatEnhancingDrug")]
public class JobGiver_TakeCombatEnhancingDrug_FindCombatEnhancingDrug
{
    private static bool Prepare()
    {
        var method = typeof(JobGiver_TakeCombatEnhancingDrug).GetMethod("FindCombatEnhancingDrug",
            BindingFlags.Instance | BindingFlags.NonPublic);
        return !(method == null || method.HasAttribute<ObsoleteAttribute>());
    }

    private static void Postfix(ref Thing __result, Pawn pawn)
    {
        if (pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Drunken_Master) != true)
        {
            return;
        }

        var i = 0;
        while (i < pawn.inventory.innerContainer.Count)
        {
            var thing = pawn.inventory.innerContainer[i];
            var compDrug = thing.TryGetComp<CompDrug>();
            if (compDrug != null && compDrug.Props.chemical == ChemicalDefOf.Alcohol)
            {
                if (thing.def.ingestible.outcomeDoers.Find(x =>
                        (x is IngestionOutcomeDoer_GiveHediff ingestionOutcomeDoer_GiveHediff2
                            ? ingestionOutcomeDoer_GiveHediff2.hediffDef
                            : null) == HediffDefOf.AlcoholHigh) is IngestionOutcomeDoer_GiveHediff
                    ingestionOutcomeDoerGiveHediff)
                {
                    var hediff = HediffMaker.MakeHediff(ingestionOutcomeDoerGiveHediff.hediffDef, pawn);
                    hediff.Severity = ingestionOutcomeDoerGiveHediff.severity;
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