using HarmonyLib;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200000C RID: 12
    [HarmonyPatch(typeof(Thing), "PostApplyDamage")]
    public static class Thing_PostApplyDamage_Patch
    {
        // Token: 0x06000010 RID: 16 RVA: 0x00002498 File Offset: 0x00000698
        private static bool Prefix(Thing __instance, DamageInfo dinfo, float totalDamageDealt)
        {
            if (!(totalDamageDealt > 0f) || !GMT_Animal_Friend_Helper.ShouldBeFriendly(__instance, dinfo.Instigator))
            {
                return true;
            }

            if (dinfo.Def.defName == "ExecutionCut")
            {
                return true;
            }

            if (dinfo.Instigator is not Pawn pawn)
            {
                return true;
            }

            var needs = pawn.needs;
            if (needs == null)
            {
                return true;
            }

            var mood = needs.mood;
            if (mood == null)
            {
                return true;
            }

            var thoughts = mood.thoughts;
            if (thoughts == null)
            {
                return true;
            }

            var memories = thoughts.memories;
            if (memories != null)
            {
                memories.TryGainMemory(GMT_DefOf.GMT_Animal_Friend_Hurt_Animal,
                    __instance as Pawn);
            }

            return true;
        }
    }
}