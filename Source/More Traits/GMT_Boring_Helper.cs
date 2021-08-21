using System.Collections.Generic;
using RimWorld;

namespace Garthor_More_Traits
{
    // Token: 0x0200000D RID: 13
    public static class GMT_Boring_Helper
    {
        // Token: 0x04000013 RID: 19
        internal const float boring_severity = 0.15f;

        // Token: 0x04000014 RID: 20
        internal static readonly Dictionary<InteractionDef, float> bored_factors = new Dictionary<InteractionDef, float>
        {
            {
                InteractionDefOf.BuildRapport,
                0.15f
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
}