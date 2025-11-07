using System;

// Token: 0x02000187 RID: 391
public class DistanceObjectiveValueFormatterSO : ObjectiveValueFormatterSO
{
	// Token: 0x0600061F RID: 1567 RVA: 0x000058D4 File Offset: 0x00003AD4
	public override string FormatValue(int value)
	{
		return string.Format("{0} m", value);
	}
}
