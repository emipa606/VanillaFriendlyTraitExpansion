using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200001E RID: 30
    [HarmonyPatch(typeof(Building_Door), "Notify_PawnApproaching")]
    public static class Building_Door_Notify_PawnApproaching_Patch
    {
        // Token: 0x0600003B RID: 59 RVA: 0x00003580 File Offset: 0x00001780
        private static void Postfix(Building_Door __instance, Pawn p, ref int ___ticksUntilClose)
        {
            if (p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true)
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