using System;
using UnityEngine;
using Zenject;

// Token: 0x020002F6 RID: 758
public class PlayerHeightDetector : MonoBehaviour
{
	// Token: 0x14000066 RID: 102
	// (add) Token: 0x06000CFF RID: 3327 RVA: 0x000377F8 File Offset: 0x000359F8
	// (remove) Token: 0x06000D00 RID: 3328 RVA: 0x00037830 File Offset: 0x00035A30
	public event Action<float> playerHeightDidChangeEvent;

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0000A1C1 File Offset: 0x000083C1
	public float playerHeight
	{
		get
		{
			return this._lastReportedHeight;
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x00037868 File Offset: 0x00035A68
	protected void Start()
	{
		if (this._beatmapObjectCallbackController != null)
		{
			if (this._beatmapObjectCallbackData != null)
			{
				this._beatmapObjectCallbackController.RemoveBeatmapObjectCallback(this._beatmapObjectCallbackData);
			}
			this._beatmapObjectCallbackData = this._beatmapObjectCallbackController.AddBeatmapObjectCallback(new BeatmapObjectCallbackController.BeatmapObjectCallback(this.BeatmapObjectSpawnCallback), 0.5f);
		}
		Action<float> action = this.playerHeightDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this._lastReportedHeight);
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0000A1C9 File Offset: 0x000083C9
	protected void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.RemoveBeatmapObjectCallback(this._beatmapObjectCallbackData);
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x000378D4 File Offset: 0x00035AD4
	protected void LateUpdate()
	{
		float songTime = this._audioTimeSyncController.songTime;
		if (songTime < this._noTopObstaclesStartTime)
		{
			return;
		}
		float y = this._playerController.headPos.y;
		float num = y - this._computedPlayerHeight;
		this._changeWeight += ((num > 0f) ? (num * Time.deltaTime * 16f) : (num * Time.deltaTime * 2f));
		this._computedPlayerHeight = Mathf.Lerp(this._computedPlayerHeight, y, Time.deltaTime * Mathf.Abs(this._changeWeight) * 4f / (songTime * 0.1f + 1f));
		this._changeWeight *= 0.9f;
		float num2 = this._computedPlayerHeight * 1.2f;
		if (Mathf.Abs(this._lastReportedHeight - num2) > 0.01f)
		{
			this._lastReportedHeight = num2;
			Action<float> action = this.playerHeightDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action(num2);
		}
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x000379C4 File Offset: 0x00035BC4
	private void BeatmapObjectSpawnCallback(BeatmapObjectData beatmapObjectData)
	{
		if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
		{
			ObstacleData obstacleData = (ObstacleData)beatmapObjectData;
			if (obstacleData.obstacleType == ObstacleType.Top)
			{
				this._noTopObstaclesStartTime = Mathf.Max(this._noTopObstaclesStartTime, beatmapObjectData.time + obstacleData.duration + 0.5f);
			}
		}
	}

	// Token: 0x04000D63 RID: 3427
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000D64 RID: 3428
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000D65 RID: 3429
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000D67 RID: 3431
	private const float kHeightScaleAdjust = 1.2f;

	// Token: 0x04000D68 RID: 3432
	private BeatmapObjectCallbackController.BeatmapObjectCallbackData _beatmapObjectCallbackData;

	// Token: 0x04000D69 RID: 3433
	private float _noTopObstaclesStartTime;

	// Token: 0x04000D6A RID: 3434
	private float _computedPlayerHeight = 1.4999999f;

	// Token: 0x04000D6B RID: 3435
	private float _changeWeight;

	// Token: 0x04000D6C RID: 3436
	private float _lastReportedHeight = 1.8f;
}
