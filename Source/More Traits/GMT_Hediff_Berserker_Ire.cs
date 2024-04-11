using System.Reflection;
using RimWorld;
using Verse;

namespace Garthor_More_Traits;

public class GMT_Hediff_Berserker_Ire : HediffWithComps
{
    public const float ire_trickle = 0.06f;

    public const float rage_chance_factor = 0.2f;

    private static FieldInfo fieldinfo_visible;

    protected static readonly float scaleFactor = 0.5f;

    protected bool visible
    {
        get
        {
            if (fieldinfo_visible == null)
            {
                fieldinfo_visible =
                    typeof(Hediff).GetField("visible", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            return fieldinfo_visible is not null && (bool)fieldinfo_visible.GetValue(this);
        }
        set
        {
            if (fieldinfo_visible == null)
            {
                fieldinfo_visible =
                    typeof(Hediff).GetField("visible", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            fieldinfo_visible?.SetValue(this, value);
        }
    }

    public override void Tick()
    {
        base.Tick();
        if (!pawn.IsHashIntervalTick(60) || !(Severity > def.minSeverity))
        {
            return;
        }

        Severity -= 0.06f;
        if (Severity == def.minSeverity)
        {
            Exit_Rage();
        }
    }

    public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
    {
        var pawn1 = pawn;
        if (pawn1?.story?.traits?.HasTrait(GMT_DefOf.GMT_Berserker) == true)
        {
            pawn.health.RemoveHediff(this);
            var firstHediffOfDef =
                pawn.health.hediffSet.GetFirstHediffOfDef(GMT_DefOf.GMT_Hediff_Berserker_Rage);
            if (firstHediffOfDef != null)
            {
                pawn.health.RemoveHediff(firstHediffOfDef);
            }
        }

        if (dinfo.Instigator is not Pawn instigator || !pawn.HostileTo(instigator))
        {
            return;
        }

        if (Rand.Chance(0.2f * Severity))
        {
            Enter_Rage();
        }
        else
        {
            Severity += totalDamageDealt / pawn.RaceProps.body.corePart.def.GetMaxHealth(pawn) /
                        scaleFactor;
        }
    }

    protected virtual void Enter_Rage()
    {
        if (pawn.mindState.mentalStateHandler.TryStartMentalState(GMT_DefOf.GMT_MentalState_Berserking))
        {
            Severity = 1f;
        }
    }

    protected virtual void Exit_Rage()
    {
        if (pawn.MentalStateDef != GMT_DefOf.GMT_MentalState_Berserking)
        {
            return;
        }

        pawn.MentalState.RecoverFromState();
        if (pawn.IsColonist && !pawn.health.Downed)
        {
            pawn.drafter.Drafted = true;
        }
    }
}