using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x02000038 RID: 56
public class MainSystemInit : MonoBehaviour
{
	// Token: 0x060000E7 RID: 231 RVA: 0x000169A8 File Offset: 0x00014BA8
	public void Init()
	{
		this._mainSettingsModel.Load(false);
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		this._audioManager.Init();
		this._audioManager.mainVolume = AudioHelpers.NormalizedVolumeToDB(this._mainSettingsModel.volume);
		Vector2Int vector2Int = this._mainSettingsModel.windowResolution;
		if (Screen.width != vector2Int.x || Screen.height != vector2Int.y)
		{
			Screen.SetResolution(vector2Int.x, vector2Int.y, false);
		}
		Screen.fullScreen = this._mainSettingsModel.fullscreen;
		XRSettings.renderViewportScale = 1f;
		XRSettings.eyeTextureResolutionScale = this._mainSettingsModel.vrResolutionScale * this._mainSettingsModel.menuVRResolutionScaleMultiplier;
		XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
		if (this._mainSettingsModel.mirrorGraphicsSettings >= this._mirrorRendererGraphicsSettingsPresets.presets.Length)
		{
			this._mainSettingsModel.mirrorGraphicsSettings.value = this._mirrorRendererGraphicsSettingsPresets.presets.Length - 1;
		}
		if (this._mainSettingsModel.mainEffectGraphicsSettings >= this._mainEffectGraphicsSettingsPresets.presets.Length)
		{
			this._mainSettingsModel.mainEffectGraphicsSettings.value = this._mainEffectGraphicsSettingsPresets.presets.Length - 1;
		}
		if (this._mainSettingsModel.bloomPrePassGraphicsSettings >= this._bloomPrePassGraphicsSettingsPresets.presets.Length)
		{
			this._mainSettingsModel.bloomPrePassGraphicsSettings.value = this._bloomPrePassGraphicsSettingsPresets.presets.Length - 1;
		}
		MirrorRendererGraphicsSettingsPresets.Preset preset = this._mirrorRendererGraphicsSettingsPresets.presets[this._mainSettingsModel.mirrorGraphicsSettings];
		MainEffectGraphicsSettingsPresetsSO.Preset preset2 = this._mainEffectGraphicsSettingsPresets.presets[this._mainSettingsModel.mainEffectGraphicsSettings];
		BloomPrePassGraphicsSettingsPresetsSO.Preset preset3 = this._bloomPrePassGraphicsSettingsPresets.presets[this._mainSettingsModel.bloomPrePassGraphicsSettings];
		BoolSO smokeGraphicsSettings = this._mainSettingsModel.smokeGraphicsSettings;
		this._mirrorRenderer.Init(preset.reflectLayers, preset.stereoTextureWidth, preset.stereoTextureHeight, preset.monoTextureWidth, preset.monoTextureHeight, preset.maxAntiAliasing, preset.enableBloomPrePassFog);
		this._mainEffectContainer.Init(preset2.mainEffect);
		this._bloomPrePassEffectContainer.Init(preset3.bloomPrePassEffect);
		Application.targetFrameRate = -1;
		Application.runInBackground = true;
		QualitySettings.maxQueuedFrames = -1;
		QualitySettings.vSyncCount = 0;
		QualitySettings.antiAliasing = this._mainSettingsModel.antiAliasingLevel;
		this._platformLeaderboardsModel.Init();
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00002A87 File Offset: 0x00000C87
	public void InstallBindings(DiContainer container)
	{
		container.Bind<ExternalCamerasManager.InitData>().FromInstance(new ExternalCamerasManager.InitData(this._mainSettingsModel.oculusMRCEnabled)).AsSingle();
	}

	// Token: 0x040000B8 RID: 184
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040000B9 RID: 185
	[SerializeField]
	private AudioManagerSO _audioManager;

	// Token: 0x040000BA RID: 186
	[SerializeField]
	private MirrorRendererGraphicsSettingsPresets _mirrorRendererGraphicsSettingsPresets;

	// Token: 0x040000BB RID: 187
	[SerializeField]
	private MainEffectGraphicsSettingsPresetsSO _mainEffectGraphicsSettingsPresets;

	// Token: 0x040000BC RID: 188
	[SerializeField]
	private BloomPrePassGraphicsSettingsPresetsSO _bloomPrePassGraphicsSettingsPresets;

	// Token: 0x040000BD RID: 189
	[SerializeField]
	private MirrorRendererSO _mirrorRenderer;

	// Token: 0x040000BE RID: 190
	[SerializeField]
	private MainEffectContainerSO _mainEffectContainer;

	// Token: 0x040000BF RID: 191
	[SerializeField]
	private BloomPrePassEffectContainerSO _bloomPrePassEffectContainer;

	// Token: 0x040000C0 RID: 192
	[Inject]
	private PlatformLeaderboardsModel _platformLeaderboardsModel;

	// Token: 0x040000C1 RID: 193
	[SerializeField]
	private BeatmapLevelSO _anyBeatmapLevelSO;
}
