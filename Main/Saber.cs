using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class Saber : MonoBehaviour
{
	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0000A397 File Offset: 0x00008597
	public SaberType saberType
	{
		get
		{
			return this._saberType.saberType;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0000A3A4 File Offset: 0x000085A4
	public Vector3 saberBladeTopPos
	{
		get
		{
			return this._topPos.position;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0000A3B1 File Offset: 0x000085B1
	public Vector3 saberBladeBottomPos
	{
		get
		{
			return this._bottomPos.position;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0000A3BE File Offset: 0x000085BE
	public Vector3 handlePos
	{
		get
		{
			return this._handlePos.position;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0000A3CB File Offset: 0x000085CB
	public Quaternion handleRot
	{
		get
		{
			return this._handlePos.rotation;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0000A3D8 File Offset: 0x000085D8
	public float bladeSpeed
	{
		get
		{
			return this._movementData.bladeSpeed;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0000A3E5 File Offset: 0x000085E5
	// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0000A3ED File Offset: 0x000085ED
	public bool disableCutting { get; set; }

	// Token: 0x06000D3B RID: 3387 RVA: 0x00038280 File Offset: 0x00036480
	protected void Start()
	{
		this._time = 0f;
		for (int i = 0; i < 20; i++)
		{
			this._unusedSwingRatingCounters.Add(new SaberSwingRatingCounter());
		}
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x000382B8 File Offset: 0x000364B8
	public void ManualUpdate()
	{
		this._time += Time.deltaTime;
		if (!this._vrController.active || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		Vector3 position = this._topPos.position;
		Vector3 position2 = this._bottomPos.position;
		int i = 0;
		while (i < this._swingRatingCounters.Count)
		{
			SaberSwingRatingCounter saberSwingRatingCounter = this._swingRatingCounters[i];
			if (saberSwingRatingCounter.didFinish)
			{
				saberSwingRatingCounter.Deinit();
				this._swingRatingCounters.RemoveAt(i);
				this._unusedSwingRatingCounters.Add(saberSwingRatingCounter);
			}
			else
			{
				i++;
			}
		}
		SaberMovementData.Data lastAddedData = this._movementData.lastAddedData;
		this._movementData.AddNewData(position, position2, this._time);
		if (!this.disableCutting)
		{
			this._cutter.Cut(this, position, position2, lastAddedData.topPos, lastAddedData.bottomPos);
		}
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0003839C File Offset: 0x0003659C
	public SaberSwingRatingCounter CreateSwingRatingCounter(Transform noteTransform)
	{
		SaberSwingRatingCounter saberSwingRatingCounter;
		if (this._unusedSwingRatingCounters.Count > 0)
		{
			saberSwingRatingCounter = this._unusedSwingRatingCounters[0];
			this._unusedSwingRatingCounters.RemoveAt(0);
		}
		else
		{
			saberSwingRatingCounter = new SaberSwingRatingCounter();
		}
		saberSwingRatingCounter.Init(this._movementData, noteTransform);
		this._swingRatingCounters.Add(saberSwingRatingCounter);
		return saberSwingRatingCounter;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x000383F4 File Offset: 0x000365F4
	protected void OnDrawGizmos()
	{
		foreach (SaberSwingRatingCounter saberSwingRatingCounter in this._swingRatingCounters)
		{
			saberSwingRatingCounter.DrawGizmos();
		}
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0000A3F6 File Offset: 0x000085F6
	public void OverridePositionAndRotation(Vector3 pos, Quaternion rot)
	{
		this._vrController.enabled = false;
		this._vrController.transform.SetPositionAndRotation(pos, rot);
	}

	// Token: 0x04000D9B RID: 3483
	[SerializeField]
	private Transform _topPos;

	// Token: 0x04000D9C RID: 3484
	[SerializeField]
	private Transform _bottomPos;

	// Token: 0x04000D9D RID: 3485
	[SerializeField]
	private Transform _handlePos;

	// Token: 0x04000D9E RID: 3486
	[SerializeField]
	private VRController _vrController;

	// Token: 0x04000D9F RID: 3487
	[SerializeField]
	private SaberTypeObject _saberType;

	// Token: 0x04000DA1 RID: 3489
	private const int kNumberOfPrealocatedSwingRatingCounters = 20;

	// Token: 0x04000DA2 RID: 3490
	private SaberMovementData _movementData = new SaberMovementData();

	// Token: 0x04000DA3 RID: 3491
	private List<SaberSwingRatingCounter> _swingRatingCounters = new List<SaberSwingRatingCounter>(20);

	// Token: 0x04000DA4 RID: 3492
	private List<SaberSwingRatingCounter> _unusedSwingRatingCounters = new List<SaberSwingRatingCounter>(20);

	// Token: 0x04000DA5 RID: 3493
	private Cutter _cutter = new Cutter();

	// Token: 0x04000DA6 RID: 3494
	private float _time;
}
