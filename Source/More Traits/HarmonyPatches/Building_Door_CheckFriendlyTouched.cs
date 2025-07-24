using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Building_Door), nameof(Building_Door.CheckFriendlyTouched))]
public static class Building_Door_CheckFriendlyTouched
{
    private static void Postfix(Building_Door __instance, Pawn p, ref int ___lastFriendlyTouchTick)
    {
        if (p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true)
        {
            return;
        }

        if (!p.HostileTo(__instance) && __instance.PawnCanOpen(p))
        {
            ___lastFriendlyTouchTick = int.MaxValue;
        }
    }
}