using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

[HarmonyPatch(typeof(TraitSet), "GainTrait")]
public static class TraitSet_GainTrait_Patch
{
    private static FieldInfo fieldinfo_pawn;

    public static bool Prefix(TraitSet __instance, Trait trait)
    {
        if (__instance.HasTrait(trait.def))
        {
            return true;
        }

        var modExtension = trait.def.GetModExtension<GMT_Trait_ModExtension>();
        var list = modExtension?.appliedHediffs;
        if (list == null)
        {
            return true;
        }

        foreach (var def in list)
        {
            if (fieldinfo_pawn == null)
            {
                fieldinfo_pawn =
                    typeof(TraitSet).GetField("pawn", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            (fieldinfo_pawn?.GetValue(__instance) as Pawn)?.health.AddHediff(def);
        }

        return true;
    }
}