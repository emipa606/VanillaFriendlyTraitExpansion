using HarmonyLib;
using SyrDoorMats;
using Verse;

namespace Garthor_More_Traits.Compatibility;

[PatchIfMod("syrchalis.doormats")]
public static class Syr_Doormats
{
    [HarmonyPatch(typeof(Building_DoorMat), nameof(Building_DoorMat.Notify_PawnApproaching))]
    [HarmonyPrefix]
    public static bool Prefix_Notify_PawnApproaching(Pawn pawn)
    {
        return pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Slob) != true;
    }

    [HarmonyPatch(typeof(CostToMoveIntoCellPatch), nameof(CostToMoveIntoCellPatch.CostToMoveIntoCell_Postfix))]
    [HarmonyPrefix]
    public static bool Prefix_CostToMoveIntoCell_Postfix(Pawn pawn)
    {
        return pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Slob) != true;
    }
}