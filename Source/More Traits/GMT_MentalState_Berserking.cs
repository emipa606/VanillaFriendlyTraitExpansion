using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits;

public class GMT_MentalState_Berserking : MentalState
{
    public Pawn insult_target;

    public int lastInsultTicks = int.MinValue;

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

    public override void PreStart()
    {
        base.PreStart();
        pawn.health.AddHediff(GMT_DefOf.GMT_Hediff_Berserker_Rage);
    }

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