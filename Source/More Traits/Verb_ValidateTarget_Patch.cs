using HarmonyLib;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200000A RID: 10
    [HarmonyPatch(typeof(Verb), "ValidateTarget")]
    public static class Verb_ValidateTarget_Patch
    {
        // Token: 0x0600000E RID: 14 RVA: 0x00002420 File Offset: 0x00000620
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
}