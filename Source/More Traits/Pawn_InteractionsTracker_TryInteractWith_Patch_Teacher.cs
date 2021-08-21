using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000024 RID: 36
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "TryInteractWith")]
    public static class Pawn_InteractionsTracker_TryInteractWith_Patch_Teacher
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003800 File Offset: 0x00001A00
        private static void Postfix(Pawn recipient, InteractionDef intDef,
            Pawn ___pawn)
        {
            if (___pawn?.skills == null || recipient.skills == null)
            {
                return;
            }

            if (___pawn.story?.traits?.HasTrait(GMT_DefOf.GMT_Teacher) != true)
            {
                return;
            }

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
        }
    }
}