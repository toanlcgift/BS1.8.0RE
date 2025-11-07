using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D7 RID: 983
public class PlayerSettingsPanelController : MonoBehaviour, IRefreshable
{
	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06001223 RID: 4643 RVA: 0x0000DD20 File Offset: 0x0000BF20
	public PlayerSpecificSettings playerSpecificSettings
	{
		get
		{
			return this._playerSpecificSettings;
		}
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x0000DD28 File Offset: 0x0000BF28
	public void SetData(PlayerSpecificSettings playerSpecificSettings)
	{
		this._playerSpecificSettings = playerSpecificSettings;
		this._playerHeightSettingsController.Init(playerSpecificSettings);
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x0004436C File Offset: 0x0004256C
	protected void Awake()
	{
		this._toggleBinder = new ToggleBinder();
		this._toggleBinder.AddBinding(this._leftHandedToggle, delegate (bool on)
		{
			this._playerSpecificSettings.leftHanded = on;
		});
		this._toggleBinder.AddBinding(this._staticLightsToggle, delegate (bool on)
		{
			this._playerSpecificSettings.staticLights = on;
		});
		this._toggleBinder.AddBinding(this._reduceDebrisToggle, delegate (bool on)
		{
			this._playerSpecificSettings.reduceDebris = on;
		});
		this._toggleBinder.AddBinding(this._noTextsAndHudsToggle, delegate (bool on)
		{
			this._playerSpecificSettings.noTextsAndHuds = on;
		});
		this._toggleBinder.AddBinding(this._advanceHudToggle, delegate (bool on)
		{
			this._playerSpecificSettings.advancedHud = on;
		});
		this._toggleBinder.AddBinding(this._autoRestartToggle, delegate (bool on)
		{
			this._playerSpecificSettings.autoRestart = on;
		});
		this._toggleBinder.AddBinding(this._automaticPlayerHeightToggle, delegate (bool on)
		{
			this._playerSpecificSettings.automaticPlayerHeight = on;
			this._playerHeightSettingsController.interactable = !on;
		});
		this._sfxVolumeSettingsController.valueDidChangeEvent += this.HandleSFXVolumeSettingsControllerValueDidChange;
		this._saberTrailIntensitySettingsController.valueDidChangeEvent += this.HandleSaberTrailIntensitySettingsControllerValueDidChange;
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00044480 File Offset: 0x00042680
	private void OnDestroy()
	{
		if (this._toggleBinder != null)
		{
			this._toggleBinder.ClearBindings();
		}
		if (this._sfxVolumeSettingsController != null)
		{
			this._sfxVolumeSettingsController.valueDidChangeEvent -= this.HandleSFXVolumeSettingsControllerValueDidChange;
		}
		if (this._saberTrailIntensitySettingsController != null)
		{
			this._saberTrailIntensitySettingsController.valueDidChangeEvent -= this.HandleSaberTrailIntensitySettingsControllerValueDidChange;
		}
	}

	Action<PlayerSettingsViewController> _didFinishEvent;
	PlayerSettingsViewController _playerSettingsViewController;

	internal void SetActionCallback(Action<PlayerSettingsViewController> didFinishEvent, PlayerSettingsViewController playerSettingsViewController)
	{
		_didFinishEvent = didFinishEvent;
		_playerSettingsViewController = playerSettingsViewController;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			_didFinishEvent.Invoke(_playerSettingsViewController);
		}
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000444EC File Offset: 0x000426EC
	public void Refresh()
	{
		this._leftHandedToggle.isOn = this._playerSpecificSettings.leftHanded;
		this._staticLightsToggle.isOn = this._playerSpecificSettings.staticLights;
		this._reduceDebrisToggle.isOn = this.playerSpecificSettings.reduceDebris;
		this._noTextsAndHudsToggle.isOn = this.playerSpecificSettings.noTextsAndHuds;
		this._advanceHudToggle.isOn = this.playerSpecificSettings.advancedHud;
		this._autoRestartToggle.isOn = this.playerSpecificSettings.autoRestart;
		this._automaticPlayerHeightToggle.isOn = this.playerSpecificSettings.automaticPlayerHeight;
		this._sfxVolumeSettingsController.SetValue(this.playerSpecificSettings.sfxVolume, false);
		this._saberTrailIntensitySettingsController.SetValue(this.playerSpecificSettings.saberTrailIntensity, false);
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x0000DD3D File Offset: 0x0000BF3D
	private void HandleSFXVolumeSettingsControllerValueDidChange(FormattedFloatListSettingsController settingsController, float value)
	{
		this._playerSpecificSettings.sfxVolume = value;
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x0000DD4B File Offset: 0x0000BF4B
	private void HandleSaberTrailIntensitySettingsControllerValueDidChange(FormattedFloatListSettingsController settingsController, float value)
	{
		this._playerSpecificSettings.saberTrailIntensity = value;
	}

	// Token: 0x040011E8 RID: 4584
	[SerializeField]
	private Toggle _leftHandedToggle;

	// Token: 0x040011E9 RID: 4585
	[SerializeField]
	private Toggle _staticLightsToggle;

	// Token: 0x040011EA RID: 4586
	[SerializeField]
	private Toggle _reduceDebrisToggle;

	// Token: 0x040011EB RID: 4587
	[SerializeField]
	private Toggle _noTextsAndHudsToggle;

	// Token: 0x040011EC RID: 4588
	[SerializeField]
	private Toggle _advanceHudToggle;

	// Token: 0x040011ED RID: 4589
	[SerializeField]
	private Toggle _autoRestartToggle;

	// Token: 0x040011EE RID: 4590
	[SerializeField]
	private PlayerHeightSettingsController _playerHeightSettingsController;

	// Token: 0x040011EF RID: 4591
	[SerializeField]
	private Toggle _automaticPlayerHeightToggle;

	// Token: 0x040011F0 RID: 4592
	[SerializeField]
	private FormattedFloatListSettingsController _sfxVolumeSettingsController;

	// Token: 0x040011F1 RID: 4593
	[SerializeField]
	private FormattedFloatListSettingsController _saberTrailIntensitySettingsController;

	// Token: 0x040011F2 RID: 4594
	private PlayerSpecificSettings _playerSpecificSettings;

	// Token: 0x040011F3 RID: 4595
	private ToggleBinder _toggleBinder;
}
