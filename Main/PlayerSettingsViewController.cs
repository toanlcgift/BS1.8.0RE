using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020003D8 RID: 984
public class PlayerSettingsViewController : ViewController
{
	// Token: 0x170003B1 RID: 945
	// (set) Token: 0x06001232 RID: 4658 RVA: 0x0000DDCA File Offset: 0x0000BFCA
	public bool hideBackButton
	{
		set
		{
			this._hideBackButton = value;
			this._backButtonContainer.SetActive(!value);
		}
	}

	// Token: 0x14000098 RID: 152
	// (add) Token: 0x06001233 RID: 4659 RVA: 0x000445C4 File Offset: 0x000427C4
	// (remove) Token: 0x06001234 RID: 4660 RVA: 0x000445FC File Offset: 0x000427FC
	public event Action<PlayerSettingsViewController> didFinishEvent;

	// Token: 0x06001235 RID: 4661 RVA: 0x00044634 File Offset: 0x00042834
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._backButton, delegate
			{
				Action<PlayerSettingsViewController> action = this.didFinishEvent;
				if (action == null)
				{
					return;
				}
				action(this);
			});
		}
		this._playerSettingsPanelController.SetData(this._playerDataModelSO.playerData.playerSpecificSettings);
		this._playerSettingsPanelController.Refresh();
		this._playerSettingsPanelController.SetActionCallback(this.didFinishEvent, this);
	}

	// Token: 0x040011F4 RID: 4596
	[SerializeField]
	private PlayerSettingsPanelController _playerSettingsPanelController;

	// Token: 0x040011F5 RID: 4597
	[SerializeField]
	private Button _backButton;

	// Token: 0x040011F6 RID: 4598
	[SerializeField]
	private GameObject _backButtonContainer;

	// Token: 0x040011F7 RID: 4599
	[Inject]
	private PlayerDataModel _playerDataModelSO;

	// Token: 0x040011F8 RID: 4600
	private bool _hideBackButton;
}
