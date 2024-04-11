using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(Building_Door), nameof(Building_Door.Tick))]
public static class Building_Door_Tick_Patch
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var i = 0;
        var instrs = instructions.ToList();
        var cellContains = AccessTools.Method(typeof(ThingGrid), "CellContains", [
            typeof(IntVec3),
            typeof(ThingCategory)
        ]);
        var found = false;
        while (i < instrs.Count)
        {
            var instr = instrs[i];
            if (instr.Is(OpCodes.Callvirt, cellContains))
            {
                found = true;
                yield return instr;
                break;
            }

            yield return instr;
            var num = i + 1;
            i = num;
        }

        if (found)
        {
            yield return new CodeInstruction(OpCodes.Ldarg_0);
            yield return new CodeInstruction(OpCodes.Ldfld,
                AccessTools.Field(typeof(Building_Door), "ticksUntilClose"));
            yield return new CodeInstruction(OpCodes.Ldc_I4, 10000);
            yield return new CodeInstruction(OpCodes.Clt);
            yield return new CodeInstruction(OpCodes.And);
            var num = i + 1;
            for (i = num; i < instrs.Count; i = num)
            {
                yield return instrs[i];
                num = i + 1;
            }
        }
        else
        {
            Log.Error("(GMT) Error patching Building_Door.Tick()");
        }
    }
}