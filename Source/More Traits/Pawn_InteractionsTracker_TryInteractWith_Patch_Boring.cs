using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200000E RID: 14
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "TryInteractWith")]
    public static class Pawn_InteractionsTracker_TryInteractWith_Patch_Boring
    {
        // Token: 0x06000012 RID: 18 RVA: 0x00002574 File Offset: 0x00000774
        private static void Postfix(Pawn recipient, InteractionDef intDef,
            Pawn ___pawn)
        {
            if (___pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Boring) != true)
            {
                return;
            }

            if (recipient == null || recipient.story?.traits?.HasTrait(GMT_DefOf.GMT_Boring) == true)
            {
                return;
            }

            var hediff = HediffMaker.MakeHediff(GMT_DefOf.GMT_Hediff_Bored, recipient);
            hediff.Severity = 0.15f * GMT_Boring_Helper.bored_factors.TryGetValue(intDef, 1f);
            recipient.health.AddHediff(hediff);
        }
    }
}