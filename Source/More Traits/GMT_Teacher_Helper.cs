using System.Collections.Generic;
using RimWorld;

namespace Garthor_More_Traits;

public static class GMT_Teacher_Helper
{
    internal const float teacher_xp_per_level_difference = 200f;

    internal static readonly Dictionary<InteractionDef, float> teaching_factors =
        new()
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