using System;
using UnityEngine;

// Token: 0x0200018A RID: 394
[Serializable]
public class MissionObjective
{
	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x00005953 File Offset: 0x00003B53
	public MissionObjectiveTypeSO type
	{
		get
		{
			return this._type;
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000595B File Offset: 0x00003B5B
	public MissionObjective.ReferenceValueComparisonType referenceValueComparisonType
	{
		get
		{
			return this._referenceValueComparisonType;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x0600062D RID: 1581 RVA: 0x00005963 File Offset: 0x00003B63
	public int referenceValue
	{
		get
		{
			return this._referenceValue;
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00024A68 File Offset: 0x00022C68
	public static bool operator ==(MissionObjective obj1, MissionObjective obj2)
	{
		return obj1 == obj2 || (obj1 != null && obj2 != null && (obj1.type == obj2.type && obj1.referenceValueComparisonType == obj2.referenceValueComparisonType) && obj1._referenceValue == obj2._referenceValue);
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0000596B File Offset: 0x00003B6B
	public static bool operator !=(MissionObjective obj1, MissionObjective obj2)
	{
		return !(obj1 == obj2);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00005977 File Offset: 0x00003B77
	public override bool Equals(object obj)
	{
		return obj != null && (this == obj || (obj.GetType() == base.GetType() && this == (MissionObjective)obj));
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x000059A5 File Offset: 0x00003BA5
	public override int GetHashCode()
	{
		return (this._type.GetHashCode() * 397 ^ this._referenceValueComparisonType.GetHashCode()) * 397 ^ this._referenceValue.GetHashCode();
	}

	// Token: 0x040006A4 RID: 1700
	[SerializeField]
	private MissionObjectiveTypeSO _type;

	// Token: 0x040006A5 RID: 1701
	[SerializeField]
	private MissionObjective.ReferenceValueComparisonType _referenceValueComparisonType;

	// Token: 0x040006A6 RID: 1702
	[SerializeField]
	private int _referenceValue;

	// Token: 0x0200018B RID: 395
	public enum ReferenceValueComparisonType
	{
		// Token: 0x040006A8 RID: 1704
		None,
		// Token: 0x040006A9 RID: 1705
		Equal,
		// Token: 0x040006AA RID: 1706
		Max,
		// Token: 0x040006AB RID: 1707
		Min
	}
}
