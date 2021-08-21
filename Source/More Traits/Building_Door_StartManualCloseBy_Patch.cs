using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000020 RID: 32
    [HarmonyPatch(typeof(Building_Door), "StartManualCloseBy")]
    public static class Building_Door_StartManualCloseBy_Patch
    {
        // Token: 0x0600003D RID: 61 RVA: 0x000036A8 File Offset: 0x000018A8
        private static bool Prefix(Pawn closer)
        {
            return closer?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true;
        }
    }
}