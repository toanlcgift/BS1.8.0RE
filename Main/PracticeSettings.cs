using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
[Serializable]
public class PracticeSettings
{
	// Token: 0x17000248 RID: 584
	// (get) Token: 0x0600083C RID: 2108 RVA: 0x00006C37 File Offset: 0x00004E37
	// (set) Token: 0x0600083D RID: 2109 RVA: 0x00006C3F File Offset: 0x00004E3F
	public float startSongTime
	{
		get
		{
			return this._startSongTime;
		}
		set
		{
			this._startSongTime = value;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x0600083E RID: 2110 RVA: 0x00006C48 File Offset: 0x00004E48
	// (set) Token: 0x0600083F RID: 2111 RVA: 0x00006C50 File Offset: 0x00004E50
	public float songSpeedMul
	{
		get
		{
			return this._songSpeedMul;
		}
		set
		{
			this._songSpeedMul = value;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000840 RID: 2112 RVA: 0x00006C59 File Offset: 0x00004E59
	// (set) Token: 0x06000841 RID: 2113 RVA: 0x00006C61 File Offset: 0x00004E61
	public bool startInAdvanceAndClearNotes
	{
		get
		{
			return this._startInAdvanceAndClearNotes;
		}
		set
		{
			this._startInAdvanceAndClearNotes = value;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000842 RID: 2114 RVA: 0x00006C6A File Offset: 0x00004E6A
	public static PracticeSettings defaultPracticeSettings
	{
		get
		{
			return new PracticeSettings();
		}
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00006C71 File Offset: 0x00004E71
	public PracticeSettings()
	{
		this.ResetToDefault();
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00006C86 File Offset: 0x00004E86
	public PracticeSettings(PracticeSettings practiceSettings)
	{
		this._startSongTime = practiceSettings._startSongTime;
		this._songSpeedMul = practiceSettings._songSpeedMul;
		this._startInAdvanceAndClearNotes = true;
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00006CB4 File Offset: 0x00004EB4
	public PracticeSettings(float startSongTime, float songSpeedMul)
	{
		this._startSongTime = startSongTime;
		this._songSpeedMul = songSpeedMul;
		this._startInAdvanceAndClearNotes = true;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00006CD8 File Offset: 0x00004ED8
	public void ResetToDefault()
	{
		this._startSongTime = 0f;
		this._songSpeedMul = 1f;
		this._startInAdvanceAndClearNotes = true;
	}

	// Token: 0x040008C4 RID: 2244
	public const float kDelayBeforeStart = 1f;

	// Token: 0x040008C5 RID: 2245
	[SerializeField]
	private float _startSongTime;

	// Token: 0x040008C6 RID: 2246
	[SerializeField]
	private float _songSpeedMul;

	// Token: 0x040008C7 RID: 2247
	private bool _startInAdvanceAndClearNotes = true;
}
