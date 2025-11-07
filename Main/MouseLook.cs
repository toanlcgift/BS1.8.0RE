using System;
using UnityEngine;

// Token: 0x02000434 RID: 1076
[Serializable]
public class MouseLook
{
	// Token: 0x06001495 RID: 5269 RVA: 0x0000F847 File Offset: 0x0000DA47
	public void Init(Transform character, Transform camera)
	{
		this._characterTargetRot = character.localRotation;
		this._cameraTargetRot = Quaternion.identity;
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x0004B51C File Offset: 0x0004971C
	public void LookRotation(Transform character, Transform camera)
	{
		float y = Input.GetAxis("MouseX") * this._xSensitivity;
		float num = Input.GetAxis("MouseY") * this._ySensitivity;
		this._characterTargetRot *= Quaternion.Euler(0f, y, 0f);
		this._cameraTargetRot *= Quaternion.Euler(-num, 0f, 0f);
		if (this._clampVerticalRotation)
		{
			this._cameraTargetRot = this.ClampRotationAroundXAxis(this._cameraTargetRot);
		}
		if (this._smooth)
		{
			character.localRotation = Quaternion.Slerp(character.localRotation, this._characterTargetRot, this._smoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp(camera.localRotation, this._cameraTargetRot, this._smoothTime * Time.deltaTime);
		}
		else
		{
			character.localRotation = this._characterTargetRot;
			camera.localRotation = this._cameraTargetRot;
		}
		this.UpdateCursorLock();
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x0000F860 File Offset: 0x0000DA60
	public void SetCursorLock(bool value)
	{
		this._lockCursor = value;
		if (!this._lockCursor)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x0000F87D File Offset: 0x0000DA7D
	public void UpdateCursorLock()
	{
		if (this._lockCursor)
		{
			this.InternalLockUpdate();
		}
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x0004B618 File Offset: 0x00049818
	private void InternalLockUpdate()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			this._cursorIsLocked = false;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this._cursorIsLocked = true;
		}
		if (this._cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			return;
		}
		if (!this._cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x0004B670 File Offset: 0x00049870
	private Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1f;
		float num = 114.59156f * Mathf.Atan(q.x);
		num = Mathf.Clamp(num, this._minimumX, this._maximumX);
		q.x = Mathf.Tan(0.008726646f * num);
		return q;
	}

	// Token: 0x04001445 RID: 5189
	[SerializeField]
	private float _xSensitivity = 2f;

	// Token: 0x04001446 RID: 5190
	[SerializeField]
	private float _ySensitivity = 2f;

	// Token: 0x04001447 RID: 5191
	[SerializeField]
	private bool _clampVerticalRotation = true;

	// Token: 0x04001448 RID: 5192
	[SerializeField]
	private float _minimumX = -90f;

	// Token: 0x04001449 RID: 5193
	[SerializeField]
	private float _maximumX = 90f;

	// Token: 0x0400144A RID: 5194
	[SerializeField]
	private bool _smooth;

	// Token: 0x0400144B RID: 5195
	[SerializeField]
	private float _smoothTime = 5f;

	// Token: 0x0400144C RID: 5196
	[SerializeField]
	private bool _lockCursor = true;

	// Token: 0x0400144D RID: 5197
	private Quaternion _characterTargetRot;

	// Token: 0x0400144E RID: 5198
	private Quaternion _cameraTargetRot;

	// Token: 0x0400144F RID: 5199
	private bool _cursorIsLocked = true;
}
