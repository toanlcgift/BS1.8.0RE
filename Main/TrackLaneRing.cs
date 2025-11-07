using System;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class TrackLaneRing : MonoBehaviour
{
	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00008E72 File Offset: 0x00007072
	public float destRotZ
	{
		get
		{
			return this._destRotZ;
		}
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00008E7A File Offset: 0x0000707A
	public void Init(Vector3 position, Vector3 positionOffset)
	{
		this._transform = base.transform;
		this._positionOffset = positionOffset;
		this._transform.localPosition = position + positionOffset;
		this._posZ = position.z + positionOffset.z;
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00034044 File Offset: 0x00032244
	public void FixedUpdateRing(float fixedDeltaTime)
	{
		this._prevRotZ = this._rotZ;
		this._rotZ = Mathf.Lerp(this._rotZ, this._destRotZ, fixedDeltaTime * this._rotationSpeed);
		this._prevPosZ = this._posZ;
		this._posZ = Mathf.Lerp(this._posZ, this._positionOffset.z + this._destPosZ, fixedDeltaTime * this._moveSpeed);
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x000340B4 File Offset: 0x000322B4
	public void LateUpdateRing(float interpolationFactor)
	{
		this._transform.localEulerAngles = new Vector3(0f, 0f, this._prevRotZ + (this._rotZ - this._prevRotZ) * interpolationFactor);
		this._transform.localPosition = new Vector3(this._positionOffset.x, this._positionOffset.y, this._prevPosZ + (this._posZ - this._prevPosZ) * interpolationFactor);
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00008EB4 File Offset: 0x000070B4
	public void SetDestRotation(float destRotZ, float rotateSpeed)
	{
		this._destRotZ = destRotZ;
		this._rotationSpeed = rotateSpeed;
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00008EC4 File Offset: 0x000070C4
	public float GetRotation()
	{
		return this._rotZ;
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00008E72 File Offset: 0x00007072
	public float GetDestinationRotation()
	{
		return this._destRotZ;
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00008ECC File Offset: 0x000070CC
	public void SetPosition(float destPosZ, float moveSpeed)
	{
		this._destPosZ = destPosZ;
		this._moveSpeed = moveSpeed;
	}

	// Token: 0x04000BF2 RID: 3058
	private float _prevRotZ;

	// Token: 0x04000BF3 RID: 3059
	private float _rotZ;

	// Token: 0x04000BF4 RID: 3060
	private float _destRotZ;

	// Token: 0x04000BF5 RID: 3061
	private float _rotationSpeed;

	// Token: 0x04000BF6 RID: 3062
	private float _prevPosZ;

	// Token: 0x04000BF7 RID: 3063
	private float _posZ;

	// Token: 0x04000BF8 RID: 3064
	private float _destPosZ;

	// Token: 0x04000BF9 RID: 3065
	private float _moveSpeed;

	// Token: 0x04000BFA RID: 3066
	private Vector3 _positionOffset;

	// Token: 0x04000BFB RID: 3067
	private Transform _transform;
}
