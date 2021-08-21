using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000003 RID: 3
    [HarmonyPatch(typeof(TraitSet), "GainTrait")]
    public static class TraitSet_GainTrait_Patch
    {
        // Token: 0x04000002 RID: 2
        private static FieldInfo fieldinfo_pawn;

        // Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
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

                if (fieldinfo_pawn is not null)
                {
                    (fieldinfo_pawn.GetValue(__instance) as Pawn)?.health.AddHediff(def);
                }
            }

            return true;
        }
    }
}