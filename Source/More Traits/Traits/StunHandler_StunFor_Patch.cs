using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits.Traits
{
    // Token: 0x0200002A RID: 42
    [HarmonyPatch(typeof(StunHandler), "StunFor")]
    public class StunHandler_StunFor_Patch
    {
        // Token: 0x06000047 RID: 71 RVA: 0x00003DA4 File Offset: 0x00001FA4
        private static void Prefix(StunHandler __instance, ref int ticks, ref bool addBattleLog)
        {
            if (__instance.parent is not Pawn pawn)
            {
                return;
            }

            if (pawn.story?.traits?.HasTrait(GMT_DefOf.GMT_Juggernaut) != true)
            {
                return;
            }

            ticks = 0;
            addBattleLog = false;
        }
    }
}