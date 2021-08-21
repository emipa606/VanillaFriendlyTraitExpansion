using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200001B RID: 27
    [HarmonyPatch(typeof(JobGiver_TakeCombatEnhancingDrug), "FindCombatEnhancingDrug")]
    public class JobGiver_TakeCombatEnhancingDrug_FindCombatEnhancingDrug_Patch
    {
        // Token: 0x06000034 RID: 52 RVA: 0x000030E0 File Offset: 0x000012E0
        private static bool Prepare()
        {
            var method = typeof(JobGiver_TakeCombatEnhancingDrug).GetMethod("FindCombatEnhancingDrug",
                BindingFlags.Instance | BindingFlags.NonPublic);
            return !(method == null || method.HasAttribute<ObsoleteAttribute>());
        }

        // Token: 0x06000035 RID: 53 RVA: 0x00003128 File Offset: 0x00001328
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
                            Log.Message("result");
                            __result = thing;
                            break;
                        }
                    }
                }

                i++;
            }
        }
    }
}