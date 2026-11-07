using HarmonyLib;
using RimWorld;

namespace Garthor_More_Traits.Traits;

[HarmonyPatch(typeof(StaggerHandler), nameof(StaggerHandler.StaggerFor), typeof(int), typeof(float))]
public static class StaggerHandler_StaggerFor
{
    public static void Prefix(StaggerHandler __instance, ref int ticks)
    {
        if (__instance?.parent?.story?.traits?.HasTrait(GMT_DefOf.GMT_Juggernaut) == true)
        {
            ticks = 0;
        }
    }
}