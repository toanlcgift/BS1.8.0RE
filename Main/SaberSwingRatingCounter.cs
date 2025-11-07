using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class SaberSwingRatingCounter : HMAutoincrementedRequestId, ISaberMovementDataProcessor
{
	// Token: 0x1400001C RID: 28
	// (add) Token: 0x06000882 RID: 2178 RVA: 0x0002A7B4 File Offset: 0x000289B4
	// (remove) Token: 0x06000883 RID: 2179 RVA: 0x0002A7EC File Offset: 0x000289EC
	public event SaberSwingRatingCounter.DidChangeDelegate didChangeEvent;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06000884 RID: 2180 RVA: 0x0002A824 File Offset: 0x00028A24
	// (remove) Token: 0x06000885 RID: 2181 RVA: 0x0002A85C File Offset: 0x00028A5C
	public event SaberSwingRatingCounter.DidFinishDelegate didFinishEvent;

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000886 RID: 2182 RVA: 0x00006F7F File Offset: 0x0000517F
	public bool didFinish
	{
		get
		{
			return this._didFinish;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000887 RID: 2183 RVA: 0x00006F87 File Offset: 0x00005187
	public float beforeCutRating
	{
		get
		{
			return this._beforeCutRating;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000888 RID: 2184 RVA: 0x00006F8F File Offset: 0x0000518F
	public float afterCutRating
	{
		get
		{
			return this._afterCutRating;
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0002A894 File Offset: 0x00028A94
	public void Init(SaberMovementData saberMovementData, Transform noteTransform)
	{
		this._initialized = true;
		this._didFinish = false;
		this._notePlaneWasCut = false;
		SaberMovementData.Data lastAddedData = saberMovementData.lastAddedData;
		this._cutPlaneNormal = lastAddedData.segmentNormal;
		this._cutTime = lastAddedData.time;
		this._beforeCutRating = saberMovementData.ComputeSwingRating();
		this._afterCutRating = 0f;
		this._notePlaneCenter = noteTransform.position;
		this._notePlane = new Plane(noteTransform.up, this._notePlaneCenter);
		this._noteForward = noteTransform.forward;
		this._saberMovementData = saberMovementData;
		this._saberMovementData.AddDataProcessor(this);
		this._saberMovementData.RequestLastDataProcessing(this);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00006F97 File Offset: 0x00005197
	public void Deinit()
	{
		this._initialized = false;
		this._saberMovementData.RemoveDataProcessor(this);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0002A93C File Offset: 0x00028B3C
	public void ProcessNewData(SaberMovementData.Data newData, SaberMovementData.Data prevData, bool prevDataAreValid)
	{
		if (newData.time - this._cutTime > 0.4f)
		{
			this._didFinish = true;
			SaberSwingRatingCounter.DidFinishDelegate didFinishDelegate = this.didFinishEvent;
			if (didFinishDelegate == null)
			{
				return;
			}
			didFinishDelegate(this);
			return;
		}
		else
		{
			if (!prevDataAreValid)
			{
				return;
			}
			if (!this._notePlaneWasCut)
			{
				this._newPlaneNormal = Vector3.Cross(this._cutPlaneNormal, this._noteForward);
				this._notePlane = new Plane(this._newPlaneNormal, this._notePlaneCenter);
			}
			if (!this._notePlane.SameSide(newData.topPos, prevData.topPos) && !this._notePlaneWasCut)
			{
				this._beforeCutTopPos = prevData.topPos;
				this._beforeCutBottomPos = prevData.bottomPos;
				this._afterCutTopPos = newData.topPos;
				this._afterCutBottomPos = newData.bottomPos;
				float distance = 0f;
				Ray ray = new Ray(prevData.topPos, newData.topPos - prevData.topPos);
				this._notePlane.Raycast(ray, out distance);
				this._cutTopPos = ray.GetPoint(distance);
				this._cutBottomPos = (prevData.bottomPos + newData.bottomPos) * 0.5f;
				float overrideSegmentAngle = Vector3.Angle(this._cutTopPos - this._cutBottomPos, this._beforeCutTopPos - this._beforeCutBottomPos);
				float angleDiff = Vector3.Angle(this._cutTopPos - this._cutBottomPos, this._afterCutTopPos - this._afterCutBottomPos);
				this._cutTime = newData.time;
				this._beforeCutRating = this._saberMovementData.ComputeSwingRating(overrideSegmentAngle);
				this._afterCutRating = SaberSwingRating.AfterCutStepRating(angleDiff, 0f);
				this._notePlaneWasCut = true;
			}
			else
			{
				float num = Vector3.Angle(newData.segmentNormal, this._cutPlaneNormal);
				if (num > 90f)
				{
					this._didFinish = true;
					SaberSwingRatingCounter.DidFinishDelegate didFinishDelegate2 = this.didFinishEvent;
					if (didFinishDelegate2 == null)
					{
						return;
					}
					didFinishDelegate2(this);
					return;
				}
				else
				{
					this._afterCutRating += SaberSwingRating.AfterCutStepRating(newData.segmentAngle, num);
					if (this._afterCutRating > 1f)
					{
						this._afterCutRating = 1f;
					}
				}
			}
			SaberSwingRatingCounter.DidChangeDelegate didChangeDelegate = this.didChangeEvent;
			if (didChangeDelegate == null)
			{
				return;
			}
			didChangeDelegate(this, this._afterCutRating);
			return;
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0002AB70 File Offset: 0x00028D70
	public void DrawGizmos()
	{
		Quaternion q = Quaternion.LookRotation(this._notePlane.normal);
		Gizmos.matrix = Matrix4x4.TRS(this._notePlaneCenter, q, Vector3.one);
		Color32 c = Color.blue;
		c.a = 125;
		Gizmos.color = c;
		Gizmos.DrawCube(Vector3.zero, new Vector3(1f, 1f, 0.0001f));
		Gizmos.matrix = Matrix4x4.identity;
		Gizmos.color = Color.white;
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(this._beforeCutBottomPos, this._beforeCutTopPos);
		Gizmos.color = Color.white;
		Gizmos.DrawLine(this._afterCutBottomPos, this._afterCutTopPos);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(this._notePlaneCenter, this._notePlaneCenter + this._newPlaneNormal);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(this._cutTopPos, this._cutBottomPos);
	}

	// Token: 0x040008FA RID: 2298
	private SaberMovementData _saberMovementData;

	// Token: 0x040008FB RID: 2299
	private Vector3 _cutPlaneNormal;

	// Token: 0x040008FC RID: 2300
	private float _cutTime;

	// Token: 0x040008FD RID: 2301
	private float _afterCutRating;

	// Token: 0x040008FE RID: 2302
	private float _beforeCutRating;

	// Token: 0x040008FF RID: 2303
	private bool _didFinish;

	// Token: 0x04000900 RID: 2304
	private bool _initialized;

	// Token: 0x04000901 RID: 2305
	private Plane _notePlane;

	// Token: 0x04000902 RID: 2306
	private bool _notePlaneWasCut;

	// Token: 0x04000903 RID: 2307
	private Vector3 _noteForward;

	// Token: 0x04000904 RID: 2308
	private Vector3 _notePlaneCenter;

	// Token: 0x04000905 RID: 2309
	private Vector3 _beforeCutTopPos;

	// Token: 0x04000906 RID: 2310
	private Vector3 _beforeCutBottomPos;

	// Token: 0x04000907 RID: 2311
	private Vector3 _afterCutTopPos;

	// Token: 0x04000908 RID: 2312
	private Vector3 _afterCutBottomPos;

	// Token: 0x04000909 RID: 2313
	private Vector3 _newPlaneNormal;

	// Token: 0x0400090A RID: 2314
	private Vector3 _cutTopPos;

	// Token: 0x0400090B RID: 2315
	private Vector3 _cutBottomPos;

	// Token: 0x02000220 RID: 544
	// (Invoke) Token: 0x0600088F RID: 2191
	public delegate void DidChangeDelegate(SaberSwingRatingCounter afterCutRating, float rating);

	// Token: 0x02000221 RID: 545
	// (Invoke) Token: 0x06000893 RID: 2195
	public delegate void DidFinishDelegate(SaberSwingRatingCounter afterCutRating);
}
