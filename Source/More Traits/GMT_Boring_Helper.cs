using System.Collections.Generic;
using RimWorld;

namespace Garthor_More_Traits;

public static class GMT_Boring_Helper
{
    private const float boring_severity = 0.15f;

    internal static readonly Dictionary<InteractionDef, float> bored_factors = new()
    {
        {
            InteractionDefOf.BuildRapport,
            boring_severity
        },
        {
            InteractionDefOf.AnimalChat,
            0.05f
        },
        {
            InteractionDefOf.Insult,
            0.4f
        }
    };
}