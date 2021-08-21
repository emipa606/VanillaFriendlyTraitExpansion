using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x02000004 RID: 4
    [DefOf]
    public static class GMT_DefOf
    {
        // Token: 0x04000003 RID: 3
        public static TraitDef GMT_Animal_Friend;

        // Token: 0x04000004 RID: 4
        public static TraitDef GMT_Boring;

        // Token: 0x04000005 RID: 5
        public static TraitDef GMT_Caravaneer;

        // Token: 0x04000006 RID: 6
        public static TraitDef GMT_Satan_Spawn;

        // Token: 0x04000007 RID: 7
        public static TraitDef GMT_Teacher;

        // Token: 0x04000008 RID: 8
        public static TraitDef GMT_Slob;

        // Token: 0x04000009 RID: 9
        public static TraitDef GMT_Drunken_Master;

        // Token: 0x0400000A RID: 10
        public static TraitDef GMT_Berserker;

        // Token: 0x0400000B RID: 11
        public static TraitDef GMT_Juggernaut;

        // Token: 0x0400000C RID: 12
        public static ThoughtDef GMT_Animal_Friend_Hurt_Animal;

        // Token: 0x0400000D RID: 13
        public static HediffDef GMT_Hediff_Bored;

        // Token: 0x0400000E RID: 14
        public static HediffDef GMT_Hediff_Berserker_Ire;

        // Token: 0x0400000F RID: 15
        public static HediffDef GMT_Hediff_Berserker_Rage;

        // Token: 0x04000010 RID: 16
        public static MentalStateDef GMT_MentalState_Berserking;

        // Token: 0x04000011 RID: 17
        public static JobDef GMT_Job_AttackMeleeDowned;

        // Token: 0x04000012 RID: 18
        public static InteractionDef GMT_Interaction_InsultEnemy;

        // Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
        static GMT_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GMT_DefOf));
        }
    }
}