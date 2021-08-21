using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000008 RID: 8
    [HarmonyPatch(typeof(FloatMenuUtility), "GetMeleeAttackAction")]
    public static class FloatMenuUtility_GetMeleeAttackAction_Patch
    {
        // Token: 0x0600000C RID: 12 RVA: 0x00002398 File Offset: 0x00000598
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
}