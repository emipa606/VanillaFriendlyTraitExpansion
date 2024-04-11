using HarmonyLib;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Thing), nameof(Thing.PostApplyDamage))]
public static class Thing_PostApplyDamage_Patch
{
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

        var mood = needs?.mood;

        var thoughts = mood?.thoughts;

        var memories = thoughts?.memories;
        memories?.TryGainMemory(GMT_DefOf.GMT_Animal_Friend_Hurt_Animal,
            __instance as Pawn);

        return true;
    }
}