using System;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x02000456 RID: 1110
public class VRCenterAdjust : MonoBehaviour
{
	// Token: 0x06001503 RID: 5379 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
	protected void Awake()
	{
		XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0004DB44 File Offset: 0x0004BD44
	protected void Start()
	{
		if (this._roomCenter.value.magnitude > 5f)
		{
			this.ResetRoom();
		}
		base.transform.position = this._roomCenter;
		base.transform.eulerAngles = new Vector3(0f, this._roomRotation, 0f);
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x0000FCB9 File Offset: 0x0000DEB9
	protected void OnEnable()
	{
		this._roomCenter.didChangeEvent += this.HandleRoomCenterDidChange;
		this._roomRotation.didChangeEvent += this.HandleRoomRotationDidChange;
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x0000FCE9 File Offset: 0x0000DEE9
	protected void OnDisable()
	{
		this._roomCenter.didChangeEvent -= this.HandleRoomCenterDidChange;
		this._roomRotation.didChangeEvent -= this.HandleRoomRotationDidChange;
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0000FD19 File Offset: 0x0000DF19
	private void HandleRoomCenterDidChange()
	{
		base.transform.position = this._roomCenter;
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0000FD31 File Offset: 0x0000DF31
	private void HandleRoomRotationDidChange()
	{
		base.transform.eulerAngles = new Vector3(0f, this._roomRotation, 0f);
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x0000FD58 File Offset: 0x0000DF58
	protected void Update()
	{
		if (Input.GetKey(KeyCode.Delete))
		{
			this.ResetRoom();
		}
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x0000FD69 File Offset: 0x0000DF69
	public void ResetRoom()
	{
		this._roomCenter.value = new Vector3(0f, 0f, 0f);
		this._roomRotation.value = 0f;
	}

	// Token: 0x040014F5 RID: 5365
	[SerializeField]
	private Vector3SO _roomCenter;

	// Token: 0x040014F6 RID: 5366
	[SerializeField]
	private FloatSO _roomRotation;

	// Token: 0x040014F7 RID: 5367
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;
}
