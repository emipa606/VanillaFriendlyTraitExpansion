using HarmonyLib;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Verb), "ValidateTarget")]
public static class Verb_ValidateTarget_Patch
{
    private static bool Prefix(Verb __instance, ref bool __result, LocalTargetInfo target)
    {
        bool result;
        if (GMT_Animal_Friend_Helper.isHarmfulVerb(__instance) &&
            GMT_Animal_Friend_Helper.ShouldBeFriendly(__instance.Caster, target.Thing))
        {
            __result = false;
            result = false;
        }
        else
        {
            result = true;
        }

        return result;
    }
}