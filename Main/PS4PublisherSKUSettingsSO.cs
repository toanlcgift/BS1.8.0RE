using System;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class PS4PublisherSKUSettingsSO : PersistentScriptableObject
{
	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000767 RID: 1895 RVA: 0x000063EE File Offset: 0x000045EE
	public string skuName
	{
		get
		{
			return this._skuName;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000768 RID: 1896 RVA: 0x000063F6 File Offset: 0x000045F6
	public string contentId
	{
		get
		{
			return this._contentId;
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000769 RID: 1897 RVA: 0x000063FE File Offset: 0x000045FE
	public int parentalLockLevel
	{
		get
		{
			return this._parentalLockLevel;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x0600076A RID: 1898 RVA: 0x00006406 File Offset: 0x00004606
	public string npTitleFilenamePath
	{
		get
		{
			return this._npTitleFilenamePath;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x0600076B RID: 1899 RVA: 0x0000640E File Offset: 0x0000460E
	public int defaultAgeRestriction
	{
		get
		{
			return this._defaultAgeRestriction;
		}
	}

	// Token: 0x040007CE RID: 1998
	[SerializeField]
	private string _skuName;

	// Token: 0x040007CF RID: 1999
	[SerializeField]
	private string _contentId;

	// Token: 0x040007D0 RID: 2000
	[SerializeField]
	private int _parentalLockLevel;

	// Token: 0x040007D1 RID: 2001
	[SerializeField]
	private string _npTitleFilenamePath;

	// Token: 0x040007D2 RID: 2002
	[SerializeField]
	private int _defaultAgeRestriction;
}
