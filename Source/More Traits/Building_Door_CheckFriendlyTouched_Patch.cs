﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000021 RID: 33
    [HarmonyPatch(typeof(Building_Door), "CheckFriendlyTouched")]
    public static class Building_Door_CheckFriendlyTouched_Patch
    {
        // Token: 0x0600003E RID: 62 RVA: 0x00003714 File Offset: 0x00001914
        private static void Postfix(Building_Door __instance, Pawn p, ref int ___lastFriendlyTouchTick)
        {
            if (p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Satan_Spawn) != true)
            {
                return;
            }

            if (!p.HostileTo(__instance) && __instance.PawnCanOpen(p))
            {
                ___lastFriendlyTouchTick = int.MaxValue;
            }
        }
    }
}