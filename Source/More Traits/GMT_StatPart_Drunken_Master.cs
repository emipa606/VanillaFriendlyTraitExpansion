using RimWorld;
using Verse;

namespace Garthor_More_Traits
{
    // Token: 0x0200001A RID: 26
    public class GMT_StatPart_Drunken_Master : StatPart
    {
        // Token: 0x04000020 RID: 32
        private SimpleCurve curve;

        // Token: 0x06000030 RID: 48 RVA: 0x00002F68 File Offset: 0x00001168
        public override void TransformValue(StatRequest req, ref float val)
        {
            var num = Calculate_Drunk_Bonus(req, out _);
            if (num > 0f)
            {
                val += num;
            }
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00002F94 File Offset: 0x00001194
        public override string ExplanationPart(StatRequest req)
        {
            var num = Calculate_Drunk_Bonus(req, out var arg);
            string result;
            if (num != -2.14748365E+09f)
            {
                result = num != 0f
                    ? $"{GMT_DefOf.GMT_Drunken_Master.degreeDatas[0].label} ({arg}): {num:+0.0;0.0}"
                    : null;
            }
            else
            {
                result = null;
            }

            return result;
        }

        // Token: 0x06000032 RID: 50 RVA: 0x00003004 File Offset: 0x00001204
        private float Calculate_Drunk_Bonus(StatRequest req, out string label)
        {
            var pawn = req.Thing as Pawn;
            float result;
            if (pawn?.story?.traits?.HasTrait(GMT_DefOf.GMT_Drunken_Master) == true)
            {
                var firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.AlcoholHigh);
                if (firstHediffOfDef != null)
                {
                    var curStageIndex = firstHediffOfDef.CurStageIndex;
                    label = firstHediffOfDef.CurStage.label;
                    result = curve.Evaluate(curStageIndex);
                }
                else
                {
                    label = null;
                    result = 0f;
                }
            }
            else
            {
                label = null;
                result = -2.14748365E+09f;
            }

            return result;
        }
    }
}