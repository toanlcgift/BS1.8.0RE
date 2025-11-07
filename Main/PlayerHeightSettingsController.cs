using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Zenject;

// Token: 0x020003B9 RID: 953
public class PlayerHeightSettingsController : MonoBehaviour
{
	// Token: 0x170003A0 RID: 928
	// (set) Token: 0x06001184 RID: 4484 RVA: 0x000426F8 File Offset: 0x000408F8
	public bool interactable
	{
		set
		{
			this._canvasGroup.interactable = value;
			this._canvasGroup.alpha = (value ? 1f : 0.05f);
			this._text.gameObject.SetActive(value);
			this._setButton.gameObject.SetActive(value);
		}
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0000D53D File Offset: 0x0000B73D
	protected void Awake()
	{
		this._buttonBinder = new ButtonBinder();
		this._buttonBinder.AddBinding(this._setButton, new Action(this.AutoSetHeight));
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x0000D567 File Offset: 0x0000B767
	public void Init(PlayerSpecificSettings playerSettings)
	{
		this._playerSettings = playerSettings;
		this.RefreshUI();
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00042750 File Offset: 0x00040950
	private void AutoSetHeight()
	{
		Vector3 vector;
		Quaternion quaternion;
		this._vrPlatformHelper.GetNodePose(XRNode.Head, 0, out vector, out quaternion);
		this._playerSettings.playerHeight = vector.y + this._roomCenter.value.y + 0.1f;
		this.RefreshUI();
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0000D576 File Offset: 0x0000B776
	private void RefreshUI()
	{
		this._text.text = string.Format("{0:0.0}m", this._playerSettings.playerHeight);
	}

	// Token: 0x04001156 RID: 4438
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04001157 RID: 4439
	[SerializeField]
	private Button _setButton;

	// Token: 0x04001158 RID: 4440
	[SerializeField]
	private Vector3SO _roomCenter;

	// Token: 0x04001159 RID: 4441
	[SerializeField]
	private CanvasGroup _canvasGroup;

	// Token: 0x0400115A RID: 4442
	[Inject]
	private VRPlatformHelper _vrPlatformHelper;

	// Token: 0x0400115B RID: 4443
	private PlayerSpecificSettings _playerSettings;

	// Token: 0x0400115C RID: 4444
	private ButtonBinder _buttonBinder;
}
