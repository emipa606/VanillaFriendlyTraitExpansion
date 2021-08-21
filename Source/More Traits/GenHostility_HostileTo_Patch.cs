using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000007 RID: 7
    [HarmonyPatch(typeof(GenHostility), "HostileTo", typeof(Thing), typeof(Thing))]
    public static class GenHostility_HostileTo_Patch
    {
        // Token: 0x0600000B RID: 11 RVA: 0x00002374 File Offset: 0x00000574
        private static void Postfix(ref bool __result, Thing a, Thing b)
        {
            if (__result)
            {
                __result = !GMT_Animal_Friend_Helper.ShouldBeFriendly(a, b);
            }
        }
    }
}