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
        var verbPropertiesAbility =
            verb is Verb_UseAbility verbUseAbility ? verbUseAbility.UseAbilityProps : null;
        bool result;
        if (verbPropertiesAbility != null)
        {
            harmful = verbPropertiesAbility.isViolent;
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