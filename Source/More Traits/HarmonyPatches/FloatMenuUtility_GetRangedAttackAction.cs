using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(FloatMenuUtility), nameof(FloatMenuUtility.GetRangedAttackAction))]
public static class FloatMenuUtility_GetRangedAttackAction
{
    private static bool Prefix(ref Action __result, Pawn pawn, LocalTargetInfo target, out string failStr)
    {
        bool result;
        if (GMT_Animal_Friend_Helper.ShouldBeFriendly(pawn, target.Thing))
        {
            failStr = "GMT_CannotHarmAnimals".Translate();
            __result = null;
            result = false;
        }
        else
        {
            failStr = "";
            result = true;
        }

        return result;
    }
}