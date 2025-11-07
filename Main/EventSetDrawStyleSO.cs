using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class EventSetDrawStyleSO : PersistentScriptableObject
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600000C RID: 12 RVA: 0x000021A0 File Offset: 0x000003A0
	public EventSetDrawStyleSO.SpecificEventDrawStyle[] specificEvents
	{
		get
		{
			return this._specificEvents;
		}
	}

	// Token: 0x0400000A RID: 10
	[SerializeField]
	private EventSetDrawStyleSO.SpecificEventDrawStyle[] _specificEvents;

	// Token: 0x02000005 RID: 5
	[Serializable]
	public class SpecificEventDrawStyle
	{
		// Token: 0x0400000B RID: 11
		[NullAllowed]
		public EventDrawStyleSO eventDrawStyle;

		// Token: 0x0400000C RID: 12
		[NullAllowed]
		public Sprite overrideImage;

		// Token: 0x0400000D RID: 13
		[NullAllowed]
		public string hintText;
	}
}
