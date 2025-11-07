using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class MovementHistoryRecorder
{
	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0000A33E File Offset: 0x0000853E
	public AveragingValueRecorder averagingValueRecorer
	{
		get
		{
			return this._averagingValueRecorer;
		}
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0000A346 File Offset: 0x00008546
	public MovementHistoryRecorder(float averageWindowDuration, float historyValuesPerSecond, float increaseSpeed, float decreaseSpeed)
	{
		this._averagingValueRecorer = new AveragingValueRecorder(averageWindowDuration, -1f, historyValuesPerSecond);
		this._increaseSpeed = increaseSpeed;
		this._decreaseSpeed = decreaseSpeed;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0000A36F File Offset: 0x0000856F
	public void AddMovement(float distance)
	{
		this._accum += distance * this._increaseSpeed / Mathf.Max(1f, this._accum);
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x00038234 File Offset: 0x00036434
	public void ManualUpdate(float deltaTime)
	{
		this._accum -= this._decreaseSpeed * deltaTime;
		if (this._accum < 0f)
		{
			this._accum = 0f;
		}
		this._averagingValueRecorer.Update(this._accum, deltaTime);
	}

	// Token: 0x04000D97 RID: 3479
	private AveragingValueRecorder _averagingValueRecorer;

	// Token: 0x04000D98 RID: 3480
	private float _increaseSpeed;

	// Token: 0x04000D99 RID: 3481
	private float _decreaseSpeed;

	// Token: 0x04000D9A RID: 3482
	private float _accum;
}
