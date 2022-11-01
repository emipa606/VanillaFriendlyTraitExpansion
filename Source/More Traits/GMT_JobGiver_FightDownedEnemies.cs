using System.Reflection;
using RimWorld;
using Verse;
using Verse.AI;

namespace Garthor_More_Traits;

public class GMT_JobGiver_FightDownedEnemies : JobGiver_AIFightEnemies
{
    private static readonly IntRange My_ExpiryInterval_Melee = new IntRange(360, 480);

    private static FieldInfo fieldinfo_targetAcquireRadius;

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

    protected override Job TryGiveJob(Pawn pawn)
    {
        UpdateEnemyTarget(pawn);
        var enemyTarget = pawn.mindState.enemyTarget;
        Job result;
        switch (enemyTarget)
        {
            case null:
            case Pawn pawn2 when pawn2.IsInvisible():
                result = null;
                break;
            default:
            {
                var verb = pawn.meleeVerbs.TryGetMeleeVerb(enemyTarget);
                result = verb != null ? MeleeAttackJob(enemyTarget) : null;
                break;
            }
        }

        return result;
    }

    protected override Thing FindAttackTarget(Pawn pawn)
    {
        return (Thing)AttackTargetFinder.BestAttackTarget(pawn,
            TargetScanFlags.NeedLOSToPawns | TargetScanFlags.NeedReachableIfCantHitFromMyPos,
            x => ExtraTargetValidator(pawn, x),
            0f, targetAcquireRadius, GetFlagPosition(pawn), GetFlagRadius(pawn));
    }

    protected override Job MeleeAttackJob(Thing enemyTarget)
    {
        var job = JobMaker.MakeJob(GMT_DefOf.GMT_Job_AttackMeleeDowned, enemyTarget);
        job.expiryInterval = My_ExpiryInterval_Melee.RandomInRange;
        job.checkOverrideOnExpire = true;
        job.expireRequiresEnemiesNearby = true;
        return job;
    }
}