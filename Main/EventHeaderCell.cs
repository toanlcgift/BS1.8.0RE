using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x02000007 RID: 7
public class EventHeaderCell : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000021B0 File Offset: 0x000003B0
	public HoverHint hoverHint
	{
		get
		{
			return this._hoverHint;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000011 RID: 17 RVA: 0x000021B8 File Offset: 0x000003B8
	public Image image
	{
		get
		{
			return this._image;
		}
	}

	// Token: 0x0400000F RID: 15
	[SerializeField]
	private HoverHint _hoverHint;

	// Token: 0x04000010 RID: 16
	[SerializeField]
	private Image _image;

	// Token: 0x02000008 RID: 8
	public class Factory : PlaceholderFactory<EventHeaderCell>
	{
	}
}
