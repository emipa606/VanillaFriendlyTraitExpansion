using HarmonyLib;
using SyrDoorMats;
using Verse;

namespace Garthor_More_Traits.Compatibility
{
    // Token: 0x0200002C RID: 44
    [PatchIfMod("syrchalis.doormats")]
    public static class Syr_Doormats
    {
        // Token: 0x0600004D RID: 77 RVA: 0x00003EA8 File Offset: 0x000020A8
        [HarmonyPatch(typeof(Building_DoorMat), "Notify_PawnApproaching")]
        [HarmonyPrefix]
        public static bool Prefix_Notify_PawnApproaching(Pawn pawn)
        {
            return pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Slob) != true;
        }

        // Token: 0x0600004E RID: 78 RVA: 0x00003F0C File Offset: 0x0000210C
        [HarmonyPatch(typeof(CostToMoveIntoCellPatch), "CostToMoveIntoCell_Postfix")]
        [HarmonyPrefix]
        public static bool Prefix_CostToMoveIntoCell_Postfix(Pawn pawn)
        {
            return pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Slob) != true;
        }
    }
}