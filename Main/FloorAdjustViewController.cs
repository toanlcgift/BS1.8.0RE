using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Zenject;

// Token: 0x020003D2 RID: 978
public class FloorAdjustViewController : ViewController
{
	// Token: 0x06001207 RID: 4615 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._yIncButton, delegate
			{
				Vector3 value = this._roomCenter;
				value.y -= 0.05f;
				this._roomCenter.value = value;
			});
			base.buttonBinder.AddBinding(this._yDecButton, delegate
			{
				Vector3 value = this._roomCenter;
				value.y += 0.05f;
				this._roomCenter.value = value;
			});
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x00043D6C File Offset: 0x00041F6C
	protected void Update()
	{
		Vector3 vector;
		Quaternion quaternion;
		this._vrPlatformHelper.GetNodePose(XRNode.Head, 0, out vector, out quaternion);
		this._playerHeight = vector.y + this._roomCenter.value.y + 0.1f;
		if (this._playerHeight < 0.5f)
		{
			Vector3 value = this._roomCenter;
			value.y += 0.5f - this._playerHeight;
			this._roomCenter.value = value;
			this._playerHeight = 0.5f;
		}
		else if (this._playerHeight > 3f)
		{
			Vector3 value2 = this._roomCenter;
			value2.y += 3f - this._playerHeight;
			this._roomCenter.value = value2;
			this._playerHeight = 3f;
		}
		this._playerHeightText.text = Mathf.FloorToInt(this._playerHeight * 100f + 0.5f).ToString() + "cm";
	}

	// Token: 0x040011C6 RID: 4550
	[SerializeField]
	private Vector3SO _roomCenter;

	// Token: 0x040011C7 RID: 4551
	[Space]
	[SerializeField]
	private Button _yIncButton;

	// Token: 0x040011C8 RID: 4552
	[SerializeField]
	private Button _yDecButton;

	// Token: 0x040011C9 RID: 4553
	[SerializeField]
	private TextMeshProUGUI _playerHeightText;

	// Token: 0x040011CA RID: 4554
	[Inject]
	private VRPlatformHelper _vrPlatformHelper;

	// Token: 0x040011CB RID: 4555
	private const float kMoveStep = 0.05f;

	// Token: 0x040011CC RID: 4556
	private const float kMinPlayerHeight = 0.5f;

	// Token: 0x040011CD RID: 4557
	private const float kMaxPlayerHeight = 3f;

	// Token: 0x040011CE RID: 4558
	private float _playerHeight;
}
