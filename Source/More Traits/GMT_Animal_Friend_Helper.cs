using Garthor_More_Traits.Compatibility;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

public static class GMT_Animal_Friend_Helper
{
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

    internal static bool isAnimalFriend(Pawn p)
    {
        return p?.story?.traits?.HasTrait(GMT_DefOf.GMT_Animal_Friend) == true;
    }

    internal static bool isAnimalOrHive(Thing t)
    {
        return t != null && t.def == ThingDefOf.Hive || isAnimalOrHive(t as Pawn);
    }

    internal static bool isAnimalOrHive(Pawn p)
    {
        return p?.RaceProps?.Animal == true;
    }

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