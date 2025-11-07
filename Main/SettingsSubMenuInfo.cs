using System;
using HMUI;
using Polyglot;
using UnityEngine;

// Token: 0x020003EC RID: 1004
[Serializable]
public class SettingsSubMenuInfo
{
	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x060012CE RID: 4814 RVA: 0x0000E3AF File Offset: 0x0000C5AF
	public ViewController viewController
	{
		get
		{
			return this._viewController;
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x060012CF RID: 4815 RVA: 0x0000E3B7 File Offset: 0x0000C5B7
	public string localizedMenuName
	{
		get
		{
			return Localization.Get(this._menuName);
		}
	}

	// Token: 0x04001289 RID: 4745
	[SerializeField]
	private ViewController _viewController;

	// Token: 0x0400128A RID: 4746
	[SerializeField]
	private string _menuName;
}
