using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000014 RID: 20
    public class GMT_Hediff_Berserker_Rage : HediffWithComps
    {
        // Token: 0x0400001A RID: 26
        public const float rage_ire_trickle_factor = 0.4f;

        // Token: 0x0600001E RID: 30 RVA: 0x00002ABC File Offset: 0x00000CBC
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
}