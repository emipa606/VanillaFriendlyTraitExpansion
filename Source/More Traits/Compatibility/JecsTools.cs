using AbilityUser;
using HarmonyLib;
using Verse;

namespace Garthor_More_Traits.Compatibility
{
    // Token: 0x0200002D RID: 45
    [StaticConstructorOnStartup]
    internal static class JecsTools
    {
        // Token: 0x04000025 RID: 37
        internal static readonly bool active;

        // Token: 0x0600004F RID: 79 RVA: 0x00003F70 File Offset: 0x00002170
        static JecsTools()
        {
            if (AccessTools.TypeByName("AbilityUser.Verb_UseAbility") != null)
            {
                active = true;
            }
        }

        // Token: 0x06000050 RID: 80 RVA: 0x00003FA0 File Offset: 0x000021A0
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
}