using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000006 RID: 6
    [HarmonyPatch(typeof(FoodUtility), "IsAcceptablePreyFor", typeof(Pawn), typeof(Pawn))]
    public static class FoodUtility_IsAcceptablePreyFor_Patch
    {
        // Token: 0x0600000A RID: 10 RVA: 0x0000234C File Offset: 0x0000054C
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
}