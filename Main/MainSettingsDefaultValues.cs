using System;

// Token: 0x0200017B RID: 379
public class MainSettingsDefaultValues
{
	// Token: 0x060005F6 RID: 1526 RVA: 0x00005748 File Offset: 0x00003948
	public static void SetFixedDefaultValues(MainSettingsModelSO mainSettingsModel)
	{
		mainSettingsModel.burnMarkTrailsEnabled.value = true;
		if (!mainSettingsModel.overrideAudioLatency)
		{
			mainSettingsModel.audioLatency.value = 0f;
		}
	}

	// Token: 0x0400060F RID: 1551
	public const float kDefaultRoomCenterX = 0f;

	// Token: 0x04000610 RID: 1552
	public const float kDefaultRoomCenterY = 0f;

	// Token: 0x04000611 RID: 1553
	public const float kDefaultRoomCenterZ = 0f;

	// Token: 0x04000612 RID: 1554
	public const float kDetaultControllerPositionX = 0f;

	// Token: 0x04000613 RID: 1555
	public const float kDetaultControllerPositionY = 0f;

	// Token: 0x04000614 RID: 1556
	public const float kDetaultControllerPositionZ = 0f;

	// Token: 0x04000615 RID: 1557
	public const float kDetaultControllerRotationX = 0f;

	// Token: 0x04000616 RID: 1558
	public const float kDetaultControllerRotationY = 0f;

	// Token: 0x04000617 RID: 1559
	public const float kDetaultControllerRotationZ = 0f;

	// Token: 0x04000618 RID: 1560
	public const int kDefaultWindowResolutionWidth = 1280;

	// Token: 0x04000619 RID: 1561
	public const int kDefaultWindowResolutionHeight = 720;

	// Token: 0x0400061A RID: 1562
	public const int kDefaultMirrorGraphicsSettings = 2;

	// Token: 0x0400061B RID: 1563
	public const int kDefaultMainEffectGraphicsSettings = 1;

	// Token: 0x0400061C RID: 1564
	public const int kDefaultBloomGraphicsSettings = 0;

	// Token: 0x0400061D RID: 1565
	public const bool kDefaultSmokeGraphicsSettings = true;

	// Token: 0x0400061E RID: 1566
	public const int kDefaultAntiAliasingLevel = 2;

	// Token: 0x0400061F RID: 1567
	public const float kDefaultVrResolutionScale = 1f;

	// Token: 0x04000620 RID: 1568
	public const float kDefaultMenuVRResolutionScaleMultiplier = 1f;

	// Token: 0x04000621 RID: 1569
	public const bool kDefaultUseFixedFoveatedRenderingDuringGameplay = false;

	// Token: 0x04000622 RID: 1570
	public const bool kDefaultBurnMarkTrailsEnabled = true;

	// Token: 0x04000623 RID: 1571
	public const bool kDefaultScreenDisplacementEffectsEnabled = true;

	// Token: 0x04000624 RID: 1572
	public const float kDefaultAudioLatency = 0f;

	// Token: 0x04000625 RID: 1573
	public const int kMaxShockwaveParticles = 1;

	// Token: 0x04000626 RID: 1574
	public const int kMaxNumberOfCutSoundEffects = 24;

	// Token: 0x04000627 RID: 1575
	public const bool kCreateScreenshotDuringTheGame = false;

	// Token: 0x04000628 RID: 1576
	public const int kDefaultPauseButtonPressDurationLevel = 0;
}
