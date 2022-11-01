using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(WorkGiver_Slaughter), "HasJobOnThing")]
public static class WorkGiver_Slaughter_HasJobOnThing_Patch
{
    private static bool Prefix(Pawn pawn, Thing t)
    {
        bool result;
        if (GMT_Animal_Friend_Helper.ShouldBeFriendly(pawn, t))
        {
            JobFailReason.Is("Garthor_CannotHarmAnimals".Translate());
            result = false;
        }
        else
        {
            result = true;
        }

        return result;
    }
}