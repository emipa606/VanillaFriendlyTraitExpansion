using System.Reflection;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits
{
    // Token: 0x02000017 RID: 23
    public class GMT_JobGiver_FightDownedEnemies : JobGiver_AIFightEnemies
    {
        // Token: 0x0400001D RID: 29
        private static readonly IntRange My_ExpiryInterval_Melee = new IntRange(360, 480);

        // Token: 0x0400001E RID: 30
        private static FieldInfo fieldinfo_targetAcquireRadius;

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000025 RID: 37 RVA: 0x00002CB4 File Offset: 0x00000EB4
        protected float targetAcquireRadius
        {
            get
            {
                if (fieldinfo_targetAcquireRadius == null)
                {
                    fieldinfo_targetAcquireRadius = typeof(JobGiver_AIFightEnemy).GetField("targetAcquireRadius",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                }

                if (fieldinfo_targetAcquireRadius is not null)
                {
                    return (float)fieldinfo_targetAcquireRadius.GetValue(this);
                }

                return 0;
            }
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00002D00 File Offset: 0x00000F00
        protected override Job TryGiveJob(Pawn pawn)
        {
            UpdateEnemyTarget(pawn);
            var enemyTarget = pawn.mindState.enemyTarget;
            Job result;
            if (enemyTarget == null)
            {
                result = null;
            }
            else
            {
                if (enemyTarget is Pawn pawn2 && pawn2.IsInvisible())
                {
                    result = null;
                }
                else
                {
                    var verb = pawn.meleeVerbs.TryGetMeleeVerb(enemyTarget);
                    result = verb != null ? MeleeAttackJob(enemyTarget) : null;
                }
            }

            return result;
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00002D7C File Offset: 0x00000F7C
        protected override Thing FindAttackTarget(Pawn pawn)
        {
            return (Thing)AttackTargetFinder.BestAttackTarget(pawn,
                TargetScanFlags.NeedLOSToPawns | TargetScanFlags.NeedReachableIfCantHitFromMyPos,
                x => ExtraTargetValidator(pawn, x),
                0f, targetAcquireRadius, GetFlagPosition(pawn), GetFlagRadius(pawn));
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00002DE8 File Offset: 0x00000FE8
        protected override Job MeleeAttackJob(Thing enemyTarget)
        {
            var job = JobMaker.MakeJob(GMT_DefOf.GMT_Job_AttackMeleeDowned, enemyTarget);
            job.expiryInterval = My_ExpiryInterval_Melee.RandomInRange;
            job.checkOverrideOnExpire = true;
            job.expireRequiresEnemiesNearby = true;
            return job;
        }
    }
}