using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Pawn_FilthTracker), nameof(Pawn_FilthTracker.Notify_EnteredNewCell))]
public static class Pawn_FilthTracker_Notify_EnteredNewCell_Patch
{
    private static void Postfix(Pawn_FilthTracker __instance, Pawn ___pawn)
    {
        if (___pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Slob) != true)
        {
            return;
        }

        if (!(Rand.Value < ___pawn.GetStatValue(StatDefOf.FilthRate) * 0.005f))
        {
            return;
        }

        ThingDef filthDef;
        if (AccessTools.Field(typeof(Pawn_FilthTracker), "lastTerrainFilthDef")
                .GetValue(__instance) is ThingDef thingDef && Rand.Chance(0.66f))
        {
            filthDef = thingDef;
        }
        else
        {
            filthDef = ThingDefOf.Filth_Trash;
        }

        if (FilthMaker.TryMakeFilth(___pawn.Position, ___pawn.Map, filthDef, 1,
                (FilthSourceFlags)AccessTools
                    .Method(typeof(Pawn_FilthTracker), "get_AdditionalFilthSourceFlags")
                    .Invoke(__instance, null)))
        {
            AccessTools.TypeByName("RimWorld.FilthMonitor").GetMethod("Notify_FilthHumanGenerated")
                ?.Invoke(null, null);
        }
    }
}