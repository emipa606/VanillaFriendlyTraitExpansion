using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Building_Door), nameof(Building_Door.Notify_PawnApproaching))]
public static class Building_Door_Notify_PawnApproaching
{
    private static void Postfix(Building_Door __instance, Pawn p, ref int ___ticksUntilClose)
    {
        if (p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true)
        {
            return;
        }

        var open = __instance.Open;
        if (open)
        {
            ___ticksUntilClose = int.MaxValue;
        }
    }
}