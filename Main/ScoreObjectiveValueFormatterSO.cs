using System;

// Token: 0x02000191 RID: 401
public class ScoreObjectiveValueFormatterSO : ObjectiveValueFormatterSO
{
	// Token: 0x06000645 RID: 1605 RVA: 0x00005A91 File Offset: 0x00003C91
	public override string FormatValue(int value)
	{
		return ScoreFormatter.Format(value);
	}
}
