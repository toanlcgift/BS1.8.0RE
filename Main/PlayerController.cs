using System;
using UnityEngine;
using Zenject;

// Token: 0x020002F3 RID: 755
public class PlayerController : MonoBehaviour
{
	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000CED RID: 3309 RVA: 0x0000A046 File Offset: 0x00008246
	public Saber leftSaber
	{
		get
		{
			return this._leftSaber;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0000A04E File Offset: 0x0000824E
	public Saber rightSaber
	{
		get
		{
			return this._rightSaber;
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0000A056 File Offset: 0x00008256
	public Vector3 headPos
	{
		get
		{
			return this._headPos;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0000A05E File Offset: 0x0000825E
	public Quaternion headRot
	{
		get
		{
			return this._headRot;
		}
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0000A066 File Offset: 0x00008266
	public Saber SaberForType(SaberType saberType)
	{
		if (this._leftSaber.saberType == saberType)
		{
			return this._leftSaber;
		}
		if (this._rightSaber.saberType == saberType)
		{
			return this._rightSaber;
		}
		return null;
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0000A0AB File Offset: 0x000082AB
	// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x0000A093 File Offset: 0x00008293
	public bool disableSabers
	{
		get
		{
			return this._disableSabers;
		}
		set
		{
			this._disableSabers = value;
			this._saberManager.enabled = !value;
		}
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0000A0B3 File Offset: 0x000082B3
	public void OverrideHeadPos(Vector3 pos)
	{
		this._headPos = pos;
		this._overrideHeadPos = true;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0000A0C3 File Offset: 0x000082C3
	protected void Start()
	{
		if (this._initData.oneSaberMode)
		{
			this._saberManager.AllowOnlyOneSaber(this._initData.oneSaberType);
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0000A0E8 File Offset: 0x000082E8
	protected void Update()
	{
		if (!this._overrideHeadPos)
		{
			this._headPos = this._headTransform.position;
			this._headRot = this._headTransform.rotation;
		}
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x000377B8 File Offset: 0x000359B8
	public float MoveTowardsHead(float start, float end, Quaternion noteInverseWorldRotation, float t)
	{
		float z = (noteInverseWorldRotation * this._headPos).z;
		return Mathf.LerpUnclamped(start + z * Mathf.Min(1f, t * 2f), end + z, t);
	}

	// Token: 0x04000D55 RID: 3413
	[SerializeField]
	private Saber _leftSaber;

	// Token: 0x04000D56 RID: 3414
	[SerializeField]
	private Saber _rightSaber;

	// Token: 0x04000D57 RID: 3415
	[SerializeField]
	private SaberManager _saberManager;

	// Token: 0x04000D58 RID: 3416
	[SerializeField]
	private Transform _headTransform;

	// Token: 0x04000D59 RID: 3417
	[InjectOptional]
	private PlayerController.InitData _initData = new PlayerController.InitData(false, SaberType.SaberA);

	// Token: 0x04000D5A RID: 3418
	private bool _overrideHeadPos;

	// Token: 0x04000D5B RID: 3419
	private Vector3 _overriddenHeadPos;

	// Token: 0x04000D5C RID: 3420
	private bool _disableSabers;

	// Token: 0x04000D5D RID: 3421
	private Vector3 _headPos;

	// Token: 0x04000D5E RID: 3422
	private Quaternion _headRot;

	// Token: 0x020002F4 RID: 756
	public class InitData
	{
		// Token: 0x06000CF9 RID: 3321 RVA: 0x0000A129 File Offset: 0x00008329
		public InitData(bool oneSaberMode, SaberType oneSaberType = SaberType.SaberA)
		{
			this.oneSaberMode = oneSaberMode;
			this.oneSaberType = oneSaberType;
		}

		// Token: 0x04000D5F RID: 3423
		public readonly bool oneSaberMode;

		// Token: 0x04000D60 RID: 3424
		public readonly SaberType oneSaberType;
	}
}
