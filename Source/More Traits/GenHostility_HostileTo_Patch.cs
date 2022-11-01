using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(GenHostility), "HostileTo", typeof(Thing), typeof(Thing))]
public static class GenHostility_HostileTo_Patch
{
    private static void Postfix(ref bool __result, Thing a, Thing b)
    {
        if (__result)
        {
            __result = !GMT_Animal_Friend_Helper.ShouldBeFriendly(a, b);
        }
    }
}