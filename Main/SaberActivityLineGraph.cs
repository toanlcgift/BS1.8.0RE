using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class SaberActivityLineGraph : MonoBehaviour
{
	// Token: 0x060000A6 RID: 166 RVA: 0x00016118 File Offset: 0x00014318
	protected void Awake()
	{
		this._pointsValues = new Queue<float>(this._pointCount);
		this._pointsValues2 = new Queue<float>(this._pointCount);
		this._pointPositions = new Vector3[this._pointCount];
		for (int i = 0; i < this._pointPositions.Length; i++)
		{
			this._pointPositions[i] = new Vector3((float)i * this._pointDistance, 0f, 0f);
			this._pointsValues.Enqueue(0f);
			this._pointsValues2.Enqueue(0f);
		}
		this._lineRenderer.positionCount = this._pointCount;
		this._lineRenderer.SetPositions(this._pointPositions);
		this._lineRenderer2.positionCount = this._pointCount;
		this._lineRenderer2.SetPositions(this._pointPositions);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00002859 File Offset: 0x00000A59
	protected void Start()
	{
		base.StartCoroutine(this.UpdateGraphCoroutine());
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00002868 File Offset: 0x00000A68
	private IEnumerator UpdateGraphCoroutine()
	{
		YieldInstruction yieldInstruction = new WaitForSeconds(1f / this._updateFps);
		for (;;)
		{
			if (this._pointsValues.Count == this._pointPositions.Length)
			{
				this._pointsValues.Dequeue();
			}
			float item = this._saberActivityCounter.saberMovementAveragingValueRecorder.GetAverageValue();
			this._pointsValues.Enqueue(item);
			int num = 0;
			foreach (float num2 in this._pointsValues)
			{
				this._pointPositions[num] = new Vector3((float)num * this._pointDistance, num2 * this._scale);
				num++;
			}
			this._lineRenderer.SetPositions(this._pointPositions);
			if (this._pointsValues2.Count == this._pointPositions.Length)
			{
				this._pointsValues2.Dequeue();
			}
			item = this._saberActivityCounter.saberMovementAveragingValueRecorder.GetLastValue();
			this._pointsValues2.Enqueue(item);
			num = 0;
			foreach (float num3 in this._pointsValues2)
			{
				this._pointPositions[num] = new Vector3((float)num * this._pointDistance, num3 * this._scale);
				num++;
			}
			this._lineRenderer2.SetPositions(this._pointPositions);
			yield return yieldInstruction;
		}
		yield break;
	}

	// Token: 0x0400008B RID: 139
	[SerializeField]
	private SaberActivityCounter _saberActivityCounter;

	// Token: 0x0400008C RID: 140
	[SerializeField]
	private LineRenderer _lineRenderer;

	// Token: 0x0400008D RID: 141
	[SerializeField]
	private LineRenderer _lineRenderer2;

	// Token: 0x0400008E RID: 142
	[SerializeField]
	private int _pointCount = 100;

	// Token: 0x0400008F RID: 143
	[SerializeField]
	private float _pointDistance = 0.02f;

	// Token: 0x04000090 RID: 144
	[SerializeField]
	private float _scale = 0.01f;

	// Token: 0x04000091 RID: 145
	[SerializeField]
	private float _updateFps = 10f;

	// Token: 0x04000092 RID: 146
	private Vector3[] _pointPositions;

	// Token: 0x04000093 RID: 147
	private Queue<float> _pointsValues;

	// Token: 0x04000094 RID: 148
	private Queue<float> _pointsValues2;
}
