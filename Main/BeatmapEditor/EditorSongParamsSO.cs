using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000536 RID: 1334
	public class EditorSongParamsSO : PersistentScriptableObject
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00012F4A File Offset: 0x0001114A
		public FloatSO beatsPerMinute
		{
			get
			{
				return this._beatsPerMinute;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00012F52 File Offset: 0x00011152
		public FloatSO songTimeOffset
		{
			get
			{
				return this._songTimeOffset;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00012F5A File Offset: 0x0001115A
		public FloatSO shuffleStrength
		{
			get
			{
				return this._shuffleStrength;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00012F62 File Offset: 0x00011162
		public FloatSO shufflePeriod
		{
			get
			{
				return this._shufflePeriod;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x00012F6A File Offset: 0x0001116A
		public FloatSO previewStartTime
		{
			get
			{
				return this._previewStartTime;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00012F72 File Offset: 0x00011172
		public FloatSO previewDuration
		{
			get
			{
				return this._previewDuration;
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00059A34 File Offset: 0x00057C34
		public void SetDefaults()
		{
			this._beatsPerMinute.value = 120f;
			this._songTimeOffset.value = 0f;
			this._shuffleStrength.value = 0f;
			this._shufflePeriod.value = 0.5f;
			this._previewStartTime.value = 10f;
			this._previewDuration.value = 10f;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00059AA4 File Offset: 0x00057CA4
		public void SetValues(float beatsPerMinute, float songTimeOffset, float shuffleStrength, float shufflePeriod, float previewStartTime, float previewDuration)
		{
			this._beatsPerMinute.value = beatsPerMinute;
			this._songTimeOffset.value = songTimeOffset;
			this._shuffleStrength.value = shuffleStrength;
			this._shufflePeriod.value = shufflePeriod;
			this._previewStartTime.value = previewStartTime;
			this._previewDuration.value = previewDuration;
		}

		// Token: 0x040018A3 RID: 6307
		[SerializeField]
		private FloatSO _beatsPerMinute;

		// Token: 0x040018A4 RID: 6308
		[SerializeField]
		private FloatSO _songTimeOffset;

		// Token: 0x040018A5 RID: 6309
		[SerializeField]
		private FloatSO _shuffleStrength;

		// Token: 0x040018A6 RID: 6310
		[SerializeField]
		private FloatSO _shufflePeriod;

		// Token: 0x040018A7 RID: 6311
		[SerializeField]
		private FloatSO _previewStartTime;

		// Token: 0x040018A8 RID: 6312
		[SerializeField]
		private FloatSO _previewDuration;
	}
}
