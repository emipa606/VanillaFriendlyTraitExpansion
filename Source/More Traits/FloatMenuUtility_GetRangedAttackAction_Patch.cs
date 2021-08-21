using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000009 RID: 9
    [HarmonyPatch(typeof(FloatMenuUtility), "GetRangedAttackAction")]
    public static class FloatMenuUtility_GetRangedAttackAction_Patch
    {
        // Token: 0x0600000D RID: 13 RVA: 0x000023DC File Offset: 0x000005DC
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