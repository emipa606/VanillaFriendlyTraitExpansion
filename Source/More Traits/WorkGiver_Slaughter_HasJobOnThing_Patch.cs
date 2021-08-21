using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits
{
    // Token: 0x0200000B RID: 11
    [HarmonyPatch(typeof(WorkGiver_Slaughter), "HasJobOnThing")]
    public static class WorkGiver_Slaughter_HasJobOnThing_Patch
    {
        // Token: 0x0600000F RID: 15 RVA: 0x0000245C File Offset: 0x0000065C
        private static bool Prefix(Pawn pawn, Thing t)
        {
            bool result;
            if (GMT_Animal_Friend_Helper.ShouldBeFriendly(pawn, t))
            {
                JobFailReason.Is("Garthor_CannotHarmAnimals".Translate());
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}