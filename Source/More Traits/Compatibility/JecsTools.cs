using AbilityUser;
using HarmonyLib;
using Verse;

namespace Garthor_More_Traits.Compatibility;

[StaticConstructorOnStartup]
internal static class JecsTools
{
    internal static readonly bool active;

    static JecsTools()
    {
        if (AccessTools.TypeByName("AbilityUser.Verb_UseAbility") != null)
        {
            active = true;
        }
    }

    internal static bool isHarmfulVerb(Verb verb, out bool harmful)
    {
        var verbProperties_Ability =
            verb is Verb_UseAbility verb_UseAbility ? verb_UseAbility.UseAbilityProps : null;
        bool result;
        if (verbProperties_Ability != null)
        {
            harmful = verbProperties_Ability.isViolent;
            result = true;
        }
        else
        {
            harmful = false;
            result = false;
        }

        return result;
    }
}