using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Pawn_InteractionsTracker), nameof(Pawn_InteractionsTracker.TryInteractWith))]
public static class Pawn_InteractionsTracker_TryInteractWith
{
    private static void Postfix(Pawn recipient, InteractionDef intDef, Pawn ___pawn)
    {
        if (___pawn.story?.traits?.HasTrait(GMT_DefOf.GMT_Teacher) == true)
        {
            for (var i = 0; i < 2; i++)
            {
                var skillDef = DefDatabase<SkillDef>.AllDefsListForReading.RandomElement();
                var num = ___pawn.skills.GetSkill(skillDef).Level - recipient.skills.GetSkill(skillDef).Level;
                if (!(num > 0))
                {
                    continue;
                }

                recipient.skills.GetSkill(skillDef)
                    .Learn(num * 200f * GMT_Teacher_Helper.teaching_factors.TryGetValue(intDef, 1f));
                break;
            }

            return;
        }

        if (___pawn.story?.traits?.HasTrait(GMT_DefOf.GMT_Boring) != true)
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