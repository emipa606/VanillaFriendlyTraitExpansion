using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits.Traits;

[HarmonyPatch(typeof(StunHandler), nameof(StunHandler.StunFor))]
public class StunHandler_StunFor_Patch
{
    private static void Prefix(StunHandler __instance, ref int ticks, ref bool addBattleLog)
    {
        if (__instance.parent is not Pawn pawn)
        {
            return;
        }

        if (pawn.story?.traits?.HasTrait(GMT_DefOf.GMT_Juggernaut) != true)
        {
            return;
        }

        ticks = 0;
        addBattleLog = false;
    }
}