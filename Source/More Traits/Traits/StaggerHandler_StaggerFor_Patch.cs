using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits.Traits;

[HarmonyPatch(typeof(StaggerHandler), "StaggerFor", typeof(int), typeof(float))]
public static class StaggerHandler_StaggerFor_Patch
{
    private static void Prefix(Pawn_StanceTracker __instance, ref int ticks)
    {
        if (__instance?.pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Juggernaut) == true)
        {
            ticks = 0;
        }
    }
}