using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class SaberMovementData
{
	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000871 RID: 2161 RVA: 0x00006ECB File Offset: 0x000050CB
	public float bladeSpeed
	{
		get
		{
			return this._bladeSpeed;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002A278 File Offset: 0x00028478
	public SaberMovementData.Data lastAddedData
	{
		get
		{
			int num = this._nextAddIndex - 1;
			if (num < 0)
			{
				num += this._data.Length;
			}
			return this._data[num];
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00006ED3 File Offset: 0x000050D3
	public SaberMovementData()
	{
		this._data = new SaberMovementData.Data[500];
		this._dataProcessors = new List<ISaberMovementDataProcessor>(32);
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00006EF8 File Offset: 0x000050F8
	public void AddDataProcessor(ISaberMovementDataProcessor dataProcessor)
	{
		if (!this._dataProcessors.Contains(dataProcessor))
		{
			this._dataProcessors.Add(dataProcessor);
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00006F14 File Offset: 0x00005114
	public void RemoveDataProcessor(ISaberMovementDataProcessor dataProcessor)
	{
		this._dataProcessors.Remove(dataProcessor);
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0002A2AC File Offset: 0x000284AC
	public void RequestLastDataProcessing(ISaberMovementDataProcessor dataProcessor)
	{
		if (this._validCount > 0)
		{
			int num = this._data.Length;
			int num2 = this._nextAddIndex - 1;
			if (num2 < 0)
			{
				num2 += num;
			}
			int num3 = num2 - 1;
			if (num3 < 0)
			{
				num3 += num;
			}
			dataProcessor.ProcessNewData(this._data[num2], this._data[num3], this._validCount > 1);
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0002A310 File Offset: 0x00028510
	public void AddNewData(Vector3 topPos, Vector3 bottomPos, float time)
	{
		int num = this._data.Length;
		int nextAddIndex = this._nextAddIndex;
		int num2 = nextAddIndex - 1;
		if (num2 < 0)
		{
			num2 += num;
		}
		if (this._validCount > 0)
		{
			if (Time.deltaTime > 0.0001f)
			{
				float num3 = ((topPos - this._data[num2].topPos) / (time - this._data[num2].time)).magnitude;
				if (num3 > 100f)
				{
					num3 = 100f;
				}
				this._bladeSpeed = Mathf.Lerp(this._bladeSpeed, num3, Time.deltaTime * ((num3 < this._bladeSpeed) ? 2f : 24f));
			}
			if (this._data[num2].topPos == topPos && this._data[num2].bottomPos == bottomPos)
			{
				return;
			}
		}
		this._data[nextAddIndex].topPos = topPos;
		this._data[nextAddIndex].bottomPos = bottomPos;
		this._data[nextAddIndex].time = time;
		this.ComputeAdditionalData(this._data[nextAddIndex].topPos, this._data[nextAddIndex].bottomPos, 0, out this._data[nextAddIndex].segmentNormal, out this._data[nextAddIndex].segmentAngle);
		this._nextAddIndex = (this._nextAddIndex + 1) % num;
		if (this._validCount < this._data.Length)
		{
			this._validCount++;
		}
		foreach (ISaberMovementDataProcessor saberMovementDataProcessor in this._dataProcessors)
		{
			saberMovementDataProcessor.ProcessNewData(this._data[nextAddIndex], this._data[num2], this._validCount > 1);
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0002A510 File Offset: 0x00028710
	public void ComputeAdditionalData(Vector3 topPos, Vector3 bottomPos, int idxOffset, out Vector3 segmentNormal, out float segmentAngle)
	{
		int num = this._data.Length;
		int num2 = this._nextAddIndex + idxOffset;
		int num3 = num2 - 1;
		if (num3 < 0)
		{
			num3 += num;
		}
		if (this._validCount > 0)
		{
			Vector3 topPos2 = this._data[num2].topPos;
			Vector3 bottomPos2 = this._data[num2].bottomPos;
			Vector3 topPos3 = this._data[num3].topPos;
			Vector3 bottomPos3 = this._data[num3].bottomPos;
			segmentNormal = this.ComputePlaneNormal(topPos2, bottomPos2, topPos3, bottomPos3);
			segmentAngle = Vector3.Angle(topPos3 - bottomPos3, topPos2 - bottomPos2);
			return;
		}
		segmentNormal = Vector3.zero;
		segmentAngle = 0f;
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0002A5D4 File Offset: 0x000287D4
	public Vector3 ComputePlaneNormal(Vector3 tp0, Vector3 bp0, Vector3 tp1, Vector3 bp1)
	{
		return Vector3.Cross(tp0 - bp0, (tp1 + bp1) * 0.5f - bp0).normalized;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0002A610 File Offset: 0x00028810
	public Vector3 ComputeCutPlaneNormal()
	{
		int num = this._data.Length;
		int num2 = this._nextAddIndex - 1;
		if (num2 < 0)
		{
			num2 += num;
		}
		int num3 = num2 - 1;
		if (num3 < 0)
		{
			num3 += num;
		}
		return this.ComputePlaneNormal(this._data[num2].topPos, this._data[num2].bottomPos, this._data[num3].topPos, this._data[num3].bottomPos);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00006F23 File Offset: 0x00005123
	public float ComputeSwingRating(float overrideSegmentAngle)
	{
		return this.ComputeSwingRating(true, overrideSegmentAngle);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00006F2D File Offset: 0x0000512D
	public float ComputeSwingRating()
	{
		return this.ComputeSwingRating(false, 0f);
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0002A690 File Offset: 0x00028890
	private float ComputeSwingRating(bool overrideSegmenAngle, float overrideValue)
	{
		if (this._validCount < 2)
		{
			return 0f;
		}
		int num = this._data.Length;
		int num2 = this._nextAddIndex - 1;
		if (num2 < 0)
		{
			num2 += num;
		}
		float time = this._data[num2].time;
		float num3 = time;
		float num4 = 0f;
		Vector3 segmentNormal = this._data[num2].segmentNormal;
		float angleDiff = overrideSegmenAngle ? overrideValue : this._data[num2].segmentAngle;
		int num5 = 2;
		num4 += SaberSwingRating.BeforeCutStepRating(angleDiff, 0f);
		while (time - num3 < 0.4f && num5 < this._validCount)
		{
			num2--;
			if (num2 < 0)
			{
				num2 += num;
			}
			Vector3 segmentNormal2 = this._data[num2].segmentNormal;
			angleDiff = this._data[num2].segmentAngle;
			float num6 = Vector3.Angle(segmentNormal2, segmentNormal);
			if (num6 > 90f)
			{
				break;
			}
			num4 += SaberSwingRating.BeforeCutStepRating(angleDiff, num6);
			num3 = this._data[num2].time;
			num5++;
		}
		if (num4 > 1f)
		{
			num4 = 1f;
		}
		return num4;
	}

	// Token: 0x040008E5 RID: 2277
	private const float kOutOfRangeBladeSpeed = 100f;

	// Token: 0x040008E6 RID: 2278
	private const float kSmoothUpBladeSpeedCoef = 24f;

	// Token: 0x040008E7 RID: 2279
	private const float kSmoothDownBladeSpeedCoef = 2f;

	// Token: 0x040008E8 RID: 2280
	private SaberMovementData.Data[] _data;

	// Token: 0x040008E9 RID: 2281
	private List<ISaberMovementDataProcessor> _dataProcessors;

	// Token: 0x040008EA RID: 2282
	private int _nextAddIndex;

	// Token: 0x040008EB RID: 2283
	private int _validCount;

	// Token: 0x040008EC RID: 2284
	private float _bladeSpeed;

	// Token: 0x0200021D RID: 541
	public struct Data
	{
		// Token: 0x040008ED RID: 2285
		public float time;

		// Token: 0x040008EE RID: 2286
		public float segmentAngle;

		// Token: 0x040008EF RID: 2287
		public Vector3 topPos;

		// Token: 0x040008F0 RID: 2288
		public Vector3 bottomPos;

		// Token: 0x040008F1 RID: 2289
		public Vector3 segmentNormal;
	}
}
