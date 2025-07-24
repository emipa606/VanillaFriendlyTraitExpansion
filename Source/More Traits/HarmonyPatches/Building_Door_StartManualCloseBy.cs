using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Building_Door), nameof(Building_Door.StartManualCloseBy))]
public static class Building_Door_StartManualCloseBy
{
    private static bool Prefix(Pawn closer)
    {
        return closer?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true;
    }
}