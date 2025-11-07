using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class TestDeeplinkManager : IDeeplinkManager
{
	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060002BA RID: 698 RVA: 0x0001E410 File Offset: 0x0001C610
	// (remove) Token: 0x060002BB RID: 699 RVA: 0x0001E448 File Offset: 0x0001C648
	public event Action<Deeplink> didReceiveDeeplinkEvent;

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060002BC RID: 700 RVA: 0x00003C82 File Offset: 0x00001E82
	// (set) Token: 0x060002BD RID: 701 RVA: 0x00003C8A File Offset: 0x00001E8A
	public Deeplink currentDeeplink
	{
		get
		{
			return this._currentDeeplink;
		}
		set
		{
			this._currentDeeplink = value;
			Debug.Log(JsonUtility.ToJson(this._currentDeeplink));
			Action<Deeplink> action = this.didReceiveDeeplinkEvent;
			if (action == null)
			{
				return;
			}
			action(this._currentDeeplink);
		}
	}

	// Token: 0x0400034C RID: 844
	[DoesNotRequireDomainReloadInit]
	public static TestDeeplinkManager instance = new TestDeeplinkManager();

	// Token: 0x0400034D RID: 845
	private Deeplink _currentDeeplink;
}
