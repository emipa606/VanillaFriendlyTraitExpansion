using Verse;

namespace Garthor_More_Traits;

public class GMT_Hediff_Berserker_Rage : HediffWithComps
{
    public const float rage_ire_trickle_factor = 0.4f;

    public override void Tick()
    {
        base.Tick();
        if (!pawn.IsHashIntervalTick(60))
        {
            return;
        }

        var firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(GMT_DefOf.GMT_Hediff_Berserker_Ire);
        if (firstHediffOfDef != null)
        {
            firstHediffOfDef.Severity += 0.024f;
        }

        if (pawn.MentalStateDef != GMT_DefOf.GMT_MentalState_Berserking)
        {
            pawn.health.RemoveHediff(this);
        }
    }
}