using System;

// Token: 0x02000190 RID: 400
public class ObjectiveValueFormatterSO : PersistentScriptableObject
{
	// Token: 0x06000643 RID: 1603 RVA: 0x00005A88 File Offset: 0x00003C88
	public virtual string FormatValue(int value)
	{
		return value.ToString();
	}
}
