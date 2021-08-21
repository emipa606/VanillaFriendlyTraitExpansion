using Garthor_More_Traits.Compatibility;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000005 RID: 5
    public static class GMT_Animal_Friend_Helper
    {
        // Token: 0x06000005 RID: 5 RVA: 0x00002164 File Offset: 0x00000364
        internal static bool ShouldBeFriendly(Thing t1, Thing t2)
        {
            var pawn2 = t2 as Pawn;
            bool result;
            if (t1 is Pawn pawn && isAnimalFriend(pawn) && isAnimalOrHive(t2))
            {
                result = true;
            }
            else
            {
                result = pawn2 != null && isAnimalFriend(pawn2) && isAnimalOrHive(t1);
            }

            return result;
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000021C4 File Offset: 0x000003C4
        internal static bool isAnimalFriend(Pawn p)
        {
            return p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Animal_Friend) == true;
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002224 File Offset: 0x00000424
        internal static bool isAnimalOrHive(Thing t)
        {
            return t != null && t.def == ThingDefOf.Hive || isAnimalOrHive(t as Pawn);
        }

        // Token: 0x06000008 RID: 8 RVA: 0x0000225C File Offset: 0x0000045C
        internal static bool isAnimalOrHive(Pawn p)
        {
            return p?.RaceProps?.Animal == true;
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000022A4 File Offset: 0x000004A4
        internal static bool isHarmfulVerb(Verb verb)
        {
            if (JecsTools.active && JecsTools.isHarmfulVerb(verb, out var harmful))
            {
                return harmful;
            }

            if (verb.HarmsHealth())
            {
                return true;
            }

            if (verb is not Verb_CastAbility verb_CastAbility)
            {
                return false;
            }

            return verb_CastAbility.ability?.EffectComps?.Exists(e => e.Props.goodwillImpact < 0) == true;
        }
    }
}