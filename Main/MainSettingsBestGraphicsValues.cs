using System;

// Token: 0x0200017A RID: 378
public class MainSettingsBestGraphicsValues
{
	// Token: 0x060005F4 RID: 1524 RVA: 0x00023FFC File Offset: 0x000221FC
	public static void ApplyValues(MainSettingsModelSO mainSettingsModel)
	{
		mainSettingsModel.mirrorGraphicsSettings.value = 2;
		mainSettingsModel.mainEffectGraphicsSettings.value = 1;
		mainSettingsModel.bloomPrePassGraphicsSettings.value = 1;
		mainSettingsModel.smokeGraphicsSettings.value = true;
		mainSettingsModel.antiAliasingLevel.value = 8;
		mainSettingsModel.vrResolutionScale.value = 1.4f;
		mainSettingsModel.menuVRResolutionScaleMultiplier.value = 1f;
		mainSettingsModel.useFixedFoveatedRenderingDuringGameplay.value = false;
		mainSettingsModel.burnMarkTrailsEnabled.value = true;
		mainSettingsModel.screenDisplacementEffectsEnabled.value = true;
		mainSettingsModel.maxShockwaveParticles.value = 2;
	}
}
