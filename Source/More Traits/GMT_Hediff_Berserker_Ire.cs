using System.Reflection;
using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000013 RID: 19
    public class GMT_Hediff_Berserker_Ire : HediffWithComps
    {
        // Token: 0x04000018 RID: 24
        public const float ire_trickle = 0.06f;

        // Token: 0x04000019 RID: 25
        public const float rage_chance_factor = 0.2f;

        // Token: 0x04000016 RID: 22
        private static FieldInfo fieldinfo_visible;

        // Token: 0x04000017 RID: 23
        protected static float scaleFactor = 0.5f;

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000016 RID: 22 RVA: 0x000027C8 File Offset: 0x000009C8
        // (set) Token: 0x06000017 RID: 23 RVA: 0x00002814 File Offset: 0x00000A14
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

                if (fieldinfo_visible is not null)
                {
                    fieldinfo_visible.SetValue(this, value);
                }
            }
        }

        // Token: 0x06000018 RID: 24 RVA: 0x00002860 File Offset: 0x00000A60
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

        // Token: 0x06000019 RID: 25 RVA: 0x000028D4 File Offset: 0x00000AD4
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

        // Token: 0x0600001A RID: 26 RVA: 0x000029F4 File Offset: 0x00000BF4
        protected virtual void Enter_Rage()
        {
            if (pawn.mindState.mentalStateHandler.TryStartMentalState(GMT_DefOf.GMT_MentalState_Berserking))
            {
                Severity = 1f;
            }
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002A34 File Offset: 0x00000C34
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
}