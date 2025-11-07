using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x02000231 RID: 561
public class BeatmapObjectCallbackController : MonoBehaviour
{
	// Token: 0x1400001E RID: 30
	// (add) Token: 0x060008CA RID: 2250 RVA: 0x0002B9D0 File Offset: 0x00029BD0
	// (remove) Token: 0x060008CB RID: 2251 RVA: 0x0002BA08 File Offset: 0x00029C08
	public event Action<BeatmapEventData> beatmapEventDidTriggerEvent;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x060008CC RID: 2252 RVA: 0x0002BA40 File Offset: 0x00029C40
	// (remove) Token: 0x060008CD RID: 2253 RVA: 0x0002BA78 File Offset: 0x00029C78
	public event Action callbacksForThisFrameWereProcessedEvent;

	// Token: 0x060008CE RID: 2254 RVA: 0x0000707F File Offset: 0x0000527F
	protected void Start()
	{
		this._spawningStartTime = this._initData.spawningStartTime;
		this.SetNewBeatmapData(this._initData.beatmapData);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0002BAB0 File Offset: 0x00029CB0
	protected void LateUpdate()
	{
		if (this._beatmapData == null)
		{
			return;
		}
		for (int i = 0; i < this._beatmapObjectCallbackData.Count; i++)
		{
			this._beatmapObjectDataCallbackCacheList.Clear();
			BeatmapObjectCallbackController.BeatmapObjectCallbackData beatmapObjectCallbackData = this._beatmapObjectCallbackData[i];
			for (int j = 0; j < this._beatmapData.beatmapLinesData.Length; j++)
			{
				while (beatmapObjectCallbackData.nextObjectIndexInLine[j] < this._beatmapData.beatmapLinesData[j].beatmapObjectsData.Length)
				{
					BeatmapObjectData beatmapObjectData = this._beatmapData.beatmapLinesData[j].beatmapObjectsData[beatmapObjectCallbackData.nextObjectIndexInLine[j]];
					if (beatmapObjectData.time - beatmapObjectCallbackData.aheadTime >= this._audioTimeSource.songTime)
					{
						break;
					}
					if (beatmapObjectData.time >= this._spawningStartTime)
					{
						for (int k = this._beatmapObjectDataCallbackCacheList.Count; k >= 0; k--)
						{
							if (k == 0 || this._beatmapObjectDataCallbackCacheList[k - 1].time <= beatmapObjectData.time)
							{
								this._beatmapObjectDataCallbackCacheList.Insert(k, beatmapObjectData);
								break;
							}
						}
					}
					beatmapObjectCallbackData.nextObjectIndexInLine[j]++;
				}
			}
			foreach (BeatmapObjectData noteData in this._beatmapObjectDataCallbackCacheList)
			{
				beatmapObjectCallbackData.callback(noteData);
			}
		}
		for (int l = 0; l < this._beatmapEventCallbackData.Count; l++)
		{
			BeatmapObjectCallbackController.BeatmapEventCallbackData beatmapEventCallbackData = this._beatmapEventCallbackData[l];
			while (beatmapEventCallbackData.nextEventIndex < this._beatmapData.beatmapEventData.Length)
			{
				BeatmapEventData beatmapEventData = this._beatmapData.beatmapEventData[beatmapEventCallbackData.nextEventIndex];
				if (beatmapEventData.time - beatmapEventCallbackData.aheadTime >= this._audioTimeSource.songTime)
				{
					break;
				}
				beatmapEventCallbackData.callback(beatmapEventData);
				beatmapEventCallbackData.nextEventIndex++;
			}
		}
		while (this._nextEventIndex < this._beatmapData.beatmapEventData.Length)
		{
			BeatmapEventData beatmapEventData2 = this._beatmapData.beatmapEventData[this._nextEventIndex];
			if (beatmapEventData2.time >= this._audioTimeSource.songTime)
			{
				break;
			}
			this.SendBeatmapEventDidTriggerEvent(beatmapEventData2);
			this._nextEventIndex++;
		}
		Action action = this.callbacksForThisFrameWereProcessedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0002BD20 File Offset: 0x00029F20
	public BeatmapObjectCallbackController.BeatmapObjectCallbackData AddBeatmapObjectCallback(BeatmapObjectCallbackController.BeatmapObjectCallback callback, float aheadTime)
	{
		int numberOfLines = 4;
		if (this._beatmapData != null)
		{
			numberOfLines = this._beatmapData.beatmapLinesData.Length;
		}
		BeatmapObjectCallbackController.BeatmapObjectCallbackData beatmapObjectCallbackData = new BeatmapObjectCallbackController.BeatmapObjectCallbackData(callback, aheadTime, numberOfLines);
		this._beatmapObjectCallbackData.Add(beatmapObjectCallbackData);
		return beatmapObjectCallbackData;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x000070A3 File Offset: 0x000052A3
	public void RemoveBeatmapObjectCallback(BeatmapObjectCallbackController.BeatmapObjectCallbackData callbackData)
	{
		if (this._beatmapObjectCallbackData == null)
		{
			return;
		}
		this._beatmapObjectCallbackData.Remove(callbackData);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0002BD5C File Offset: 0x00029F5C
	public BeatmapObjectCallbackController.BeatmapEventCallbackData AddBeatmapEventCallback(BeatmapObjectCallbackController.BeatmapEventCallback callback, float aheadTime)
	{
		BeatmapObjectCallbackController.BeatmapEventCallbackData beatmapEventCallbackData = new BeatmapObjectCallbackController.BeatmapEventCallbackData(callback, aheadTime);
		this._beatmapEventCallbackData.Add(beatmapEventCallbackData);
		return beatmapEventCallbackData;
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x000070BB File Offset: 0x000052BB
	public void RemoveBeatmapEventCallback(BeatmapObjectCallbackController.BeatmapEventCallbackData callbackData)
	{
		if (this._beatmapEventCallbackData == null)
		{
			return;
		}
		this._beatmapEventCallbackData.Remove(callbackData);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x000070D3 File Offset: 0x000052D3
	public void SendBeatmapEventDidTriggerEvent(BeatmapEventData beatmapEventData)
	{
		Action<BeatmapEventData> action = this.beatmapEventDidTriggerEvent;
		if (action == null)
		{
			return;
		}
		action(beatmapEventData);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0002BD80 File Offset: 0x00029F80
	public void SetNewBeatmapData(BeatmapData beatmapData)
	{
		this._beatmapData = beatmapData;
		int num = 0;
		if (this._beatmapData != null)
		{
			num = this._beatmapData.beatmapLinesData.Length;
		}
		foreach (BeatmapObjectCallbackController.BeatmapObjectCallbackData beatmapObjectCallbackData in this._beatmapObjectCallbackData)
		{
			if (beatmapObjectCallbackData.nextObjectIndexInLine.Length < num)
			{
				beatmapObjectCallbackData.nextObjectIndexInLine = new int[num];
			}
			for (int i = 0; i < beatmapObjectCallbackData.nextObjectIndexInLine.Length; i++)
			{
				beatmapObjectCallbackData.nextObjectIndexInLine[i] = 0;
			}
		}
		this._nextEventIndex = 0;
	}

	// Token: 0x0400093E RID: 2366
	[Inject]
	private BeatmapObjectCallbackController.InitData _initData;

	// Token: 0x0400093F RID: 2367
	[Inject]
	private IAudioTimeSource _audioTimeSource;

	// Token: 0x04000940 RID: 2368
	private List<BeatmapObjectData> _beatmapObjectDataCallbackCacheList = new List<BeatmapObjectData>(10);

	// Token: 0x04000943 RID: 2371
	private List<BeatmapObjectCallbackController.BeatmapObjectCallbackData> _beatmapObjectCallbackData = new List<BeatmapObjectCallbackController.BeatmapObjectCallbackData>();

	// Token: 0x04000944 RID: 2372
	private List<BeatmapObjectCallbackController.BeatmapEventCallbackData> _beatmapEventCallbackData = new List<BeatmapObjectCallbackController.BeatmapEventCallbackData>();

	// Token: 0x04000945 RID: 2373
	private int _nextEventIndex;

	// Token: 0x04000946 RID: 2374
	private float _spawningStartTime;

	// Token: 0x04000947 RID: 2375
	private BeatmapData _beatmapData;

	// Token: 0x02000232 RID: 562
	public class InitData
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x00007111 File Offset: 0x00005311
		public InitData(BeatmapData beatmapData, float spawningStartTime)
		{
			this.beatmapData = beatmapData;
			this.spawningStartTime = spawningStartTime;
		}

		// Token: 0x04000948 RID: 2376
		public readonly BeatmapData beatmapData;

		// Token: 0x04000949 RID: 2377
		public readonly float spawningStartTime;
	}

	// Token: 0x02000233 RID: 563
	public class BeatmapObjectCallbackData
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0002BE28 File Offset: 0x0002A028
		public BeatmapObjectCallbackData(BeatmapObjectCallbackController.BeatmapObjectCallback callback, float aheadTime, int numberOfLines)
		{
			this.callback = callback;
			this.aheadTime = aheadTime;
			this.nextObjectIndexInLine = new int[numberOfLines];
			for (int i = 0; i < this.nextObjectIndexInLine.Length; i++)
			{
				this.nextObjectIndexInLine[i] = 0;
			}
		}

		// Token: 0x0400094A RID: 2378
		public float aheadTime;

		// Token: 0x0400094B RID: 2379
		public int[] nextObjectIndexInLine;

		// Token: 0x0400094C RID: 2380
		public readonly BeatmapObjectCallbackController.BeatmapObjectCallback callback;
	}

	// Token: 0x02000234 RID: 564
	public class BeatmapEventCallbackData
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00007127 File Offset: 0x00005327
		public BeatmapEventCallbackData(BeatmapObjectCallbackController.BeatmapEventCallback callback, float aheadTime)
		{
			this.callback = callback;
			this.aheadTime = aheadTime;
			this.nextEventIndex = 0;
		}

		// Token: 0x0400094D RID: 2381
		public BeatmapObjectCallbackController.BeatmapEventCallback callback;

		// Token: 0x0400094E RID: 2382
		public float aheadTime;

		// Token: 0x0400094F RID: 2383
		public int nextEventIndex;
	}

	// Token: 0x02000235 RID: 565
	// (Invoke) Token: 0x060008DB RID: 2267
	public delegate void BeatmapObjectCallback(BeatmapObjectData noteData);

	// Token: 0x02000236 RID: 566
	// (Invoke) Token: 0x060008DF RID: 2271
	public delegate void BeatmapEventCallback(BeatmapEventData eventData);
}
