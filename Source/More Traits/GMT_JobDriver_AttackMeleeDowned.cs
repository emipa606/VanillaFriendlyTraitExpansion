using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits
{
    // Token: 0x02000018 RID: 24
    public class GMT_JobDriver_AttackMeleeDowned : JobDriver_AttackMelee
    {
        // Token: 0x0600002B RID: 43 RVA: 0x00002E4C File Offset: 0x0000104C
        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_General.DoAtomic(delegate
            {
                if (job.targetA.Thing is Pawn { Downed: true })
                {
                    job.killIncappedTarget = true;
                }
            });
            yield return Toils_Misc.ThrowColonistAttackingMote(TargetIndex.A);
            yield return Toils_Combat.FollowAndMeleeAttack(TargetIndex.A, delegate
            {
                var thing = job.GetTarget(TargetIndex.A).Thing;
                if (!pawn.meleeVerbs.TryMeleeAttack(thing, job.verbToUse))
                {
                    return;
                }

                if (pawn.CurJob == null || pawn.jobs.curDriver != this)
                {
                }
            }).FailOnDespawnedOrNull(TargetIndex.A);
        }
    }
}