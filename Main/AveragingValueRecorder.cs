using System;
using System.Collections.Generic;

// Token: 0x02000436 RID: 1078
public class AveragingValueRecorder
{
	// Token: 0x060014A5 RID: 5285 RVA: 0x0004B7D0 File Offset: 0x000499D0
	public AveragingValueRecorder(float averageWindowDuration, float historyWindowDuration, float historyValuesPerSecond)
	{
		this._averageWindowDuration = averageWindowDuration;
		this._historyValuesPerSecond = historyValuesPerSecond;
		this._historyValuesCount = (int)(historyWindowDuration * historyValuesPerSecond);
		this._averageWindowValues = new Queue<AveragingValueRecorder.AverageValueData>(200);
		this._historyValues = new Queue<float>((this._historyValuesCount > 0) ? this._historyValuesCount : ((int)(historyValuesPerSecond * 300f)));
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x0004B850 File Offset: 0x00049A50
	public void Update(float value, float deltaTime)
	{
		this._lastValue = value;
		this._averageWindowValues.Enqueue(new AveragingValueRecorder.AverageValueData(value, deltaTime));
		this._averageWindowValuesDuration += deltaTime;
		while (this._averageWindowValuesDuration > this._averageWindowDuration)
		{
			this._averageWindowValuesDuration -= this._averageWindowValues.Peek().time;
			this._averageWindowValues.Dequeue();
		}
		this._averageValue = 0f;
		foreach (AveragingValueRecorder.AverageValueData averageValueData in this._averageWindowValues)
		{
			this._averageValue += averageValueData.value * averageValueData.time / this._averageWindowValuesDuration;
		}
		this._time += deltaTime;
		this._historyTime += deltaTime;
		if (this._historyTime > 1f / this._historyValuesPerSecond)
		{
			if (this._historyValues.Count == this._historyValuesCount && this._historyValuesCount > 0)
			{
				this._historyValues.Dequeue();
			}
			this._historyValues.Enqueue(this._averageValue);
			this._historyTime = 0f;
		}
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x0000F90E File Offset: 0x0000DB0E
	public float GetAverageValue()
	{
		return this._averageValue;
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x0000F916 File Offset: 0x0000DB16
	public float GetLastValue()
	{
		return this._lastValue;
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x0000F91E File Offset: 0x0000DB1E
	public Queue<float> GetHistoryValues()
	{
		return this._historyValues;
	}

	// Token: 0x04001453 RID: 5203
	private float _averageWindowDuration = 1f;

	// Token: 0x04001454 RID: 5204
	private float _historyValuesPerSecond = 1f;

	// Token: 0x04001455 RID: 5205
	private int _historyValuesCount = 10;

	// Token: 0x04001456 RID: 5206
	private Queue<AveragingValueRecorder.AverageValueData> _averageWindowValues;

	// Token: 0x04001457 RID: 5207
	private Queue<float> _historyValues;

	// Token: 0x04001458 RID: 5208
	private float _time;

	// Token: 0x04001459 RID: 5209
	private float _historyTime;

	// Token: 0x0400145A RID: 5210
	private float _averageValue;

	// Token: 0x0400145B RID: 5211
	private float _averageWindowValuesDuration;

	// Token: 0x0400145C RID: 5212
	private float _lastValue;

	// Token: 0x02000437 RID: 1079
	public struct AverageValueData
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0000F926 File Offset: 0x0000DB26
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0000F92E File Offset: 0x0000DB2E
		public float value { get; private set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0000F937 File Offset: 0x0000DB37
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0000F93F File Offset: 0x0000DB3F
		public float time { get; private set; }

		// Token: 0x060014AE RID: 5294 RVA: 0x0000F948 File Offset: 0x0000DB48
		public AverageValueData(float value, float time)
		{
			this = default(AveragingValueRecorder.AverageValueData);
			this.value = value;
			this.time = time;
		}
	}
}
