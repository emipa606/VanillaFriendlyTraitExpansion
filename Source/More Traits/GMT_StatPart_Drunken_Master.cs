using RimWorld;
using Verse;

namespace Garthor_More_Traits;

public class GMT_StatPart_Drunken_Master : StatPart
{
    private SimpleCurve curve;

    public override void TransformValue(StatRequest req, ref float val)
    {
        var num = Calculate_Drunk_Bonus(req, out _);
        if (num > 0f)
        {
            val += num;
        }
    }

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