using System.Collections.Generic;
using RimWorld;

namespace Garthor_More_Traits
{
    // Token: 0x02000023 RID: 35
    public static class GMT_Teacher_Helper
    {
        // Token: 0x04000021 RID: 33
        internal const float teacher_xp_per_level_difference = 200f;

        // Token: 0x04000022 RID: 34
        internal static readonly Dictionary<InteractionDef, float> teaching_factors =
            new Dictionary<InteractionDef, float>
            {
                {
                    InteractionDefOf.BuildRapport,
                    0.1f
                },
                {
                    InteractionDefOf.Insult,
                    0.4f
                }
            };
    }
}