using System;

// Token: 0x0200018C RID: 396
public static class MissionDataExtensions
{
	// Token: 0x06000633 RID: 1587 RVA: 0x000059DC File Offset: 0x00003BDC
	public static string Name(this MissionObjective.ReferenceValueComparisonType comparisonType)
	{
		if (comparisonType == MissionObjective.ReferenceValueComparisonType.Max)
		{
			return "Max";
		}
		if (comparisonType != MissionObjective.ReferenceValueComparisonType.Min)
		{
			return "";
		}
		return "Min";
	}
}
