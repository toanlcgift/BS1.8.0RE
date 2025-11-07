using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class FlyingCar : MonoBehaviour
{
	// Token: 0x06000B2C RID: 2860 RVA: 0x00033C04 File Offset: 0x00031E04
	protected void Start()
	{
		this._pos = base.transform.localPosition;
		this._progress = (this._pos.z - this._startZ) / Mathf.Abs(this._endZ - this._startZ);
		this.UpdatePos();
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00033C54 File Offset: 0x00031E54
	protected void Update()
	{
		this._progress += Time.deltaTime * this._speed / Mathf.Abs(this._endZ - this._startZ);
		if (this._progress > 1f)
		{
			this._progress = -UnityEngine.Random.value;
		}
		this.UpdatePos();
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00008BD6 File Offset: 0x00006DD6
	protected void UpdatePos()
	{
		this._pos.z = Mathf.LerpUnclamped(this._startZ, this._endZ, this._progress);
		base.transform.localPosition = this._pos;
	}

	// Token: 0x04000BCE RID: 3022
	[SerializeField]
	private float _startZ = -30f;

	// Token: 0x04000BCF RID: 3023
	[SerializeField]
	private float _endZ = 100f;

	// Token: 0x04000BD0 RID: 3024
	[SerializeField]
	private float _speed = 1f;

	// Token: 0x04000BD1 RID: 3025
	private float _progress;

	// Token: 0x04000BD2 RID: 3026
	private Vector3 _pos;
}
