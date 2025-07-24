using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(FoodUtility), nameof(FoodUtility.IsAcceptablePreyFor), typeof(Pawn), typeof(Pawn))]
public static class FoodUtility_IsAcceptablePreyFor
{
    private static bool Prefix(ref bool __result, Pawn predator, Pawn prey)
    {
        bool result;
        if (GMT_Animal_Friend_Helper.ShouldBeFriendly(predator, prey))
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