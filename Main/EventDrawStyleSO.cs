using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class EventDrawStyleSO : PersistentScriptableObject
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002148 File Offset: 0x00000348
	public string eventID
	{
		get
		{
			return this._eventID;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002150 File Offset: 0x00000350
	public Sprite image
	{
		get
		{
			return this._image;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000003 RID: 3 RVA: 0x00002158 File Offset: 0x00000358
	public string hintText
	{
		get
		{
			return this._hintText;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002160 File Offset: 0x00000360
	public EventDrawStyleSO.SubEventDrawStyle[] subEvents
	{
		get
		{
			return this._subEvents;
		}
	}

	// Token: 0x04000001 RID: 1
	[SerializeField]
	private string _eventID;

	// Token: 0x04000002 RID: 2
	[SerializeField]
	private Sprite _image;

	// Token: 0x04000003 RID: 3
	[SerializeField]
	private string _hintText;

	// Token: 0x04000004 RID: 4
	[SerializeField]
	private EventDrawStyleSO.SubEventDrawStyle[] _subEvents;

	// Token: 0x02000003 RID: 3
	[Serializable]
	public class SubEventDrawStyle
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002170 File Offset: 0x00000370
		public Sprite image
		{
			get
			{
				return this._image;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002178 File Offset: 0x00000378
		public Color color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
		public Color eventActiveColor
		{
			get
			{
				return this._eventActiveColor;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002188 File Offset: 0x00000388
		public int eventValue
		{
			get
			{
				return this._eventValue;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002190 File Offset: 0x00000390
		public string hintText
		{
			get
			{
				return this._hintText;
			}
		}

		// Token: 0x04000005 RID: 5
		[SerializeField]
		[NullAllowed]
		private Sprite _image;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private Color _color;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private Color _eventActiveColor;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private int _eventValue;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private string _hintText;
	}
}
