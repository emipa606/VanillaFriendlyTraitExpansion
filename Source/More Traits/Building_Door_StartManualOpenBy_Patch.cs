using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200001F RID: 31
    [HarmonyPatch(typeof(Building_Door), "StartManualOpenBy")]
    public static class Building_Door_StartManualOpenBy_Patch
    {
        // Token: 0x0600003C RID: 60 RVA: 0x00003614 File Offset: 0x00001814
        private static void Postfix(Building_Door __instance, Pawn opener, ref int ___ticksUntilClose)
        {
            if (opener?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true)
            {
                return;
            }

            var open = __instance.Open;
            if (open)
            {
                ___ticksUntilClose = int.MaxValue;
            }
        }
    }
}