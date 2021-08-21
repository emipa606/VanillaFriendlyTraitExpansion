using HarmonyLib;
using Verse;

namespace Garthor_More_Traits.Traits
{
    // Token: 0x02000029 RID: 41
    [HarmonyPatch(typeof(Pawn_StanceTracker), "StaggerFor", typeof(int))]
    public static class Pawn_StanceTracker_StaggerFor_Patch
    {
        // Token: 0x06000045 RID: 69 RVA: 0x00003CE8 File Offset: 0x00001EE8
        private static void Prefix(Pawn_StanceTracker __instance, ref int ticks)
        {
            if (__instance?.pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Juggernaut) == true)
            {
                ticks = 0;
            }
        }
    }
}