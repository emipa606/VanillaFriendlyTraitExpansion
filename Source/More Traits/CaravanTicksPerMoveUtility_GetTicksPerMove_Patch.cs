using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000012 RID: 18
    [HarmonyPatch(typeof(CaravanTicksPerMoveUtility), "GetTicksPerMove")]
    [HarmonyPatch(new[]
    {
        typeof(List<Pawn>),
        typeof(float),
        typeof(float),
        typeof(StringBuilder)
    })]
    public static class CaravanTicksPerMoveUtility_GetTicksPerMove_Patch
    {
        // Token: 0x06000015 RID: 21 RVA: 0x000026A0 File Offset: 0x000008A0
        private static void Postfix(ref int __result, List<Pawn> pawns, StringBuilder explanation)
        {
            foreach (var pawn in pawns)
            {
                if (pawn.Downed || pawn.IsPrisoner)
                {
                    continue;
                }

                if (pawn.story?.traits?.HasTrait(GMT_DefOf.GMT_Caravaneer) != true)
                {
                    continue;
                }

                __result = Mathf.RoundToInt(__result / 1.15f);
                if (explanation != null)
                {
                    explanation.AppendLine();
                    explanation.Append(
                        $"  {"GMT_CaravaneerPresent".Translate()} ({0.15f:P0}): {60000f / __result:0.#} {"TilesPerDay".Translate()}");
                }

                break;
            }
        }
    }
}