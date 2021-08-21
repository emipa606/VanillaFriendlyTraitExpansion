using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits
{
    // Token: 0x02000015 RID: 21
    public class GMT_MentalState_Berserking : MentalState
    {
        // Token: 0x0400001B RID: 27
        public Pawn insult_target;

        // Token: 0x0400001C RID: 28
        public int lastInsultTicks = int.MinValue;

        // Token: 0x06000020 RID: 32 RVA: 0x00002B54 File Offset: 0x00000D54
        public override void MentalStateTick()
        {
            if (pawn.IsHashIntervalTick(180))
            {
                var source = from thing in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, 5f, false)
                    where thing is Pawn pawn1 && InteractionUtility.CanReceiveRandomInteraction(pawn1)
                    select thing as Pawn;
                if (source.TryRandomElement(out var recipient))
                {
                    pawn.interactions.TryInteractWith(recipient, GMT_DefOf.GMT_Interaction_InsultEnemy);
                }
            }

            base.MentalStateTick();
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00002C14 File Offset: 0x00000E14
        public override void PreStart()
        {
            base.PreStart();
            pawn.health.AddHediff(GMT_DefOf.GMT_Hediff_Berserker_Rage);
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002C4C File Offset: 0x00000E4C
        public override void PostEnd()
        {
            base.PostEnd();
            var firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(GMT_DefOf.GMT_Hediff_Berserker_Rage);
            if (firstHediffOfDef != null)
            {
                pawn.health.RemoveHediff(firstHediffOfDef);
            }
        }
    }
}