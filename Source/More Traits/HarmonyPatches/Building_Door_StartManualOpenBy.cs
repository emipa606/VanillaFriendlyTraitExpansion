using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Building_Door), nameof(Building_Door.StartManualOpenBy))]
public static class Building_Door_StartManualOpenBy
{
    private static void Postfix(Building_Door __instance, Pawn opener, ref int ___ticksUntilClose)
    {
        if (opener?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true)
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