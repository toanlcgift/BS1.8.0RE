using System;
using UnityEngine;
using Zenject;

// Token: 0x02000363 RID: 867
public class SafeAreaRectChecker : MonoBehaviour
{
	// Token: 0x06000F4B RID: 3915 RVA: 0x0000BBFB File Offset: 0x00009DFB
	public void Start()
	{
		base.enabled = this._initData.checkingEnabled;
		if (base.enabled)
		{
			this._activeObjectWhenInsideSafeArea.SetActive(false);
			this._activeObjectWhenNotInsideSafeArea.SetActive(true);
		}
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x0003D858 File Offset: 0x0003BA58
	protected void Update()
	{
		this._rectTransformToCheck.GetWorldCorners(this._corners);
		Matrix4x4 worldToLocalMatrix = this._cameraTransform.worldToLocalMatrix;
		bool flag = true;
		foreach (Vector3 point in this._corners)
		{
			Vector3 vector = worldToLocalMatrix.MultiplyPoint3x4(point);
			if (vector.z < 0f || Mathf.Atan(vector.x / vector.z) < this._minAngleX * 0.017453292f || Mathf.Atan(vector.x / vector.z) > this._maxAngleX * 0.017453292f || Mathf.Atan(vector.y / vector.z) < this._minAngleY * 0.017453292f || Mathf.Atan(vector.y / vector.z) > this._maxAngleY * 0.017453292f)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this._activeObjectWhenInsideSafeArea.SetActive(true);
			this._activeObjectWhenNotInsideSafeArea.SetActive(false);
			base.enabled = false;
		}
	}

	// Token: 0x04000FB1 RID: 4017
	[SerializeField]
	private float _minAngleX = -27f;

	// Token: 0x04000FB2 RID: 4018
	[SerializeField]
	private float _maxAngleX = 27f;

	// Token: 0x04000FB3 RID: 4019
	[SerializeField]
	private float _minAngleY = -22f;

	// Token: 0x04000FB4 RID: 4020
	[SerializeField]
	private float _maxAngleY = 17f;

	// Token: 0x04000FB5 RID: 4021
	[NullAllowed(NullAllowed.Context.Prefab)]
	[SerializeField]
	private Transform _cameraTransform;

	// Token: 0x04000FB6 RID: 4022
	[SerializeField]
	private GameObject _activeObjectWhenInsideSafeArea;

	// Token: 0x04000FB7 RID: 4023
	[SerializeField]
	private GameObject _activeObjectWhenNotInsideSafeArea;

	// Token: 0x04000FB8 RID: 4024
	[SerializeField]
	private RectTransform _rectTransformToCheck;

	// Token: 0x04000FB9 RID: 4025
	private Vector3[] _corners = new Vector3[4];

	// Token: 0x04000FBA RID: 4026
	[Inject]
	private SafeAreaRectChecker.InitData _initData;

	// Token: 0x02000364 RID: 868
	public class InitData
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x0000BC6E File Offset: 0x00009E6E
		public InitData(bool checkingEnabled)
		{
			this.checkingEnabled = checkingEnabled;
		}

		// Token: 0x04000FBB RID: 4027
		public readonly bool checkingEnabled;
	}
}
