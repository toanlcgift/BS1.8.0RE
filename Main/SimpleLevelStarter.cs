using System;
using HMUI;
using Polyglot;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x0200047F RID: 1151
public class SimpleLevelStarter : MonoBehaviour
{
	// Token: 0x0600158A RID: 5514 RVA: 0x000101DF File Offset: 0x0000E3DF
	protected void Awake()
	{
		this._buttonBinder = new ButtonBinder();
		this._buttonBinder.AddBinding(this._button, new Action(this.ButtonPressed));
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x00010209 File Offset: 0x0000E409
	protected void OnDestroy()
	{
		this._buttonBinder.ClearBindings();
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x0004ED1C File Offset: 0x0004CF1C
	private void StartLevel()
	{
		IDifficultyBeatmap difficultyBeatmap = this._level.beatmapLevelData.GetDifficultyBeatmap(this._beatmapCharacteristic, this._beatmapDifficulty);
		GameplayModifiers defaultModifiers = GameplayModifiers.defaultModifiers;
		defaultModifiers.noFail = true;
		Action afterSceneSwitchCallback = delegate()
		{
			if (this._recordingTextAsset != null)
			{
				VRControllersRecorder vrcontrollersRecorder = UnityEngine.Object.FindObjectOfType<VRControllersRecorder>();
				vrcontrollersRecorder.SetInGamePlaybackDefaultSettings();
				vrcontrollersRecorder.recordingTextAsset = this._recordingTextAsset;
				vrcontrollersRecorder.recordingFileName = "";
				vrcontrollersRecorder.enabled = true;
			}
			this._gameScenesManager.installEarlyBindingsEvent -= this.InstallEarlyBindings;
		};
		this._gameScenesManager.installEarlyBindingsEvent += this.InstallEarlyBindings;
		MenuTransitionsHelper menuTransitionsHelper = this._menuTransitionsHelper;
		IDifficultyBeatmap difficultyBeatmap2 = difficultyBeatmap;
		PlayerDataModel playerDataModel = this._playerDataModel;
		OverrideEnvironmentSettings overrideEnvironmentSettings;
		if (playerDataModel == null)
		{
			overrideEnvironmentSettings = null;
		}
		else
		{
			PlayerData playerData = playerDataModel.playerData;
			overrideEnvironmentSettings = ((playerData != null) ? playerData.overrideEnvironmentSettings : null);
		}
		ColorScheme overrideColorScheme = null;
		GameplayModifiers gameplayModifiers = defaultModifiers;
		PlayerDataModel playerDataModel2 = this._playerDataModel;
		PlayerSpecificSettings playerSpecificSettings;
		if (playerDataModel2 == null)
		{
			playerSpecificSettings = null;
		}
		else
		{
			PlayerData playerData2 = playerDataModel2.playerData;
			playerSpecificSettings = ((playerData2 != null) ? playerData2.playerSpecificSettings : null);
		}
		menuTransitionsHelper.StartStandardLevel(difficultyBeatmap2, overrideEnvironmentSettings, overrideColorScheme, gameplayModifiers, playerSpecificSettings ?? PlayerSpecificSettings.defaultSettings, null, Localization.Get("BUTTON_MENU"), this.useTestNoteCutSoundEffects, null, afterSceneSwitchCallback, new Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults>(this.HandleLevelDidFinish));
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x0004EDEC File Offset: 0x0004CFEC
	private void InstallEarlyBindings(ScenesTransitionSetupDataSO scenesTransitionSetupData, DiContainer container)
	{
		foreach (Component component in this._prefabBindings)
		{
			container.Bind(new Type[]
			{
				component.GetType()
			}).FromComponentInNewPrefab(component).AsSingle().NonLazy();
		}
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00010216 File Offset: 0x0000E416
	private void ButtonPressed()
	{
		this.StartLevel();
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x0001021E File Offset: 0x0000E41E
	private void HandleLevelDidFinish(StandardLevelScenesTransitionSetupDataSO standardLevelSceneSetupData, LevelCompletionResults levelCompletionResults)
	{
		if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Restart)
		{
			this.StartLevel();
		}
	}

	// Token: 0x04001573 RID: 5491
	[SerializeField]
	[NullAllowed]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04001574 RID: 5492
	[SerializeField]
	private BeatmapLevelSO _level;

	// Token: 0x04001575 RID: 5493
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x04001576 RID: 5494
	[SerializeField]
	private BeatmapDifficulty _beatmapDifficulty;

	// Token: 0x04001577 RID: 5495
	[SerializeField]
	private bool useTestNoteCutSoundEffects;

	// Token: 0x04001578 RID: 5496
	[SerializeField]
	[NullAllowed]
	private TextAsset _recordingTextAsset;

	// Token: 0x04001579 RID: 5497
	[SerializeField]
	[NullAllowed]
	private Component[] _prefabBindings;

	// Token: 0x0400157A RID: 5498
	[Space]
	[SerializeField]
	private Button _button;

	// Token: 0x0400157B RID: 5499
	[Inject]
	private MenuTransitionsHelper _menuTransitionsHelper;

	// Token: 0x0400157C RID: 5500
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x0400157D RID: 5501
	private ButtonBinder _buttonBinder;
}
