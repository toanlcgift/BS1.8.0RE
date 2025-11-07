using System;
using System.IO;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class MainSettingsModelSO : PersistentScriptableObject
{
	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00005773 File Offset: 0x00003973
	// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0000577B File Offset: 0x0000397B
	public bool createScreenshotDuringTheGame { get; private set; }

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x060005FA RID: 1530 RVA: 0x00005784 File Offset: 0x00003984
	// (set) Token: 0x060005FB RID: 1531 RVA: 0x0000578C File Offset: 0x0000398C
	public bool playingForTheFirstTime { get; private set; }

	// Token: 0x060005FC RID: 1532 RVA: 0x00024098 File Offset: 0x00022298
	public void Save()
	{
		MainSettingsModelSO.Config config = new MainSettingsModelSO.Config();
		config.vrResolutionScale = this.vrResolutionScale;
		config.menuVRResolutionScaleMultiplier = this.menuVRResolutionScaleMultiplier;
		config.useFixedFoveatedRenderingDuringGameplay = this.useFixedFoveatedRenderingDuringGameplay;
		config.windowResolutionWidth = this.windowResolution.value.x;
		config.windowResolutionHeight = this.windowResolution.value.y;
		config.windowMode = (this.fullscreen ? MainSettingsModelSO.WindowMode.Fullscreen : MainSettingsModelSO.WindowMode.Windowed);
		config.antiAliasingLevel = this.antiAliasingLevel;
		config.volume = this.volume;
		config.controllersRumbleEnabled = this.controllersRumbleEnabled;
		config.roomCenterX = this.roomCenter.value.x;
		config.roomCenterY = this.roomCenter.value.y;
		config.roomCenterZ = this.roomCenter.value.z;
		config.roomRotation = this.roomRotation;
		config.controllerPositionX = this.controllerPosition.value.x;
		config.controllerPositionY = this.controllerPosition.value.y;
		config.controllerPositionZ = this.controllerPosition.value.z;
		config.controllerRotationX = this.controllerRotation.value.x;
		config.controllerRotationY = this.controllerRotation.value.y;
		config.controllerRotationZ = this.controllerRotation.value.z;
		config.mirrorGraphicsSettings = this.mirrorGraphicsSettings;
		config.mainEffectGraphicsSettings = this.mainEffectGraphicsSettings;
		config.bloomGraphicsSettings = this.bloomPrePassGraphicsSettings;
		config.smokeGraphicsSettings = (this.smokeGraphicsSettings.value ? 1 : 0);
		config.enableAlphaFeatures = (this.enableAlphaFeatures.value ? 1 : 0);
		config.pauseButtonPressDurationLevel = this.pauseButtonPressDurationLevel;
		config.burnMarkTrailsEnabled = this.burnMarkTrailsEnabled;
		config.screenDisplacementEffectsEnabled = this.screenDisplacementEffectsEnabled;
		config.smoothCameraEnabled = (this.smoothCameraEnabled.value ? 1 : 0);
		config.smoothCameraFieldOfView = this.smoothCameraFieldOfView;
		config.smoothCameraThirdPersonPositionX = this.smoothCameraThirdPersonPosition.value.x;
		config.smoothCameraThirdPersonPositionY = this.smoothCameraThirdPersonPosition.value.y;
		config.smoothCameraThirdPersonPositionZ = this.smoothCameraThirdPersonPosition.value.z;
		config.smoothCameraThirdPersonEulerAnglesX = this.smoothCameraThirdPersonEulerAngles.value.x;
		config.smoothCameraThirdPersonEulerAnglesY = this.smoothCameraThirdPersonEulerAngles.value.y;
		config.smoothCameraThirdPersonEulerAnglesZ = this.smoothCameraThirdPersonEulerAngles.value.z;
		config.smoothCameraThirdPersonEnabled = (this.smoothCameraThirdPersonEnabled.value ? 1 : 0);
		config.smoothCameraRotationSmooth = this.smoothCameraRotationSmooth;
		config.smoothCameraPositionSmooth = this.smoothCameraPositionSmooth;
		config.overrideAudioLatency = this.overrideAudioLatency;
		config.audioLatency = this.audioLatency;
		config.maxShockwaveParticles = this.maxShockwaveParticles;
		config.maxNumberOfCutSoundEffects = this.maxNumberOfCutSoundEffects;
		config.onlineServicesEnabled = this.onlineServicesEnabled;
		config.oculusMRCEnabled = this.oculusMRCEnabled;
		string filePath = Application.persistentDataPath + "/settings.cfg";
		string tempFilePath = Application.persistentDataPath + "/settings.cfg.tmp";
		string backupFilePath = Application.persistentDataPath + "/settings.cfg.bak";
		FileHelpers.SaveToJSONFile(config, filePath, tempFilePath, backupFilePath);
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00024448 File Offset: 0x00022648
	public void Load(bool forced)
	{
		if (this._isLoaded && !forced)
		{
			return;
		}
		string filePath = Application.persistentDataPath + "/settings.cfg";
		string backupFilePath = Application.persistentDataPath + "/settings.cfg.bak";
		MainSettingsModelSO.Config config = FileHelpers.LoadFromJSONFile<MainSettingsModelSO.Config>(filePath, backupFilePath);
		if (config == null)
		{
			config = new MainSettingsModelSO.Config();
		}
		else if (config.version != "1.5.0")
		{
			config.version = "1.5.0";
		}
		this.vrResolutionScale.value = config.vrResolutionScale;
		this.menuVRResolutionScaleMultiplier.value = config.menuVRResolutionScaleMultiplier;
		this.useFixedFoveatedRenderingDuringGameplay.value = config.useFixedFoveatedRenderingDuringGameplay;
		this.windowResolution.value = new Vector2Int(config.windowResolutionWidth, config.windowResolutionHeight);
		this.fullscreen.value = (config.windowMode == MainSettingsModelSO.WindowMode.Fullscreen);
		this.antiAliasingLevel.value = config.antiAliasingLevel;
		this.roomCenter.value = new Vector3(config.roomCenterX, config.roomCenterY, config.roomCenterZ);
		this.roomRotation.value = config.roomRotation;
		this.controllersRumbleEnabled.value = config.controllersRumbleEnabled;
		this.controllerPosition.value = new Vector3(Mathf.Clamp(config.controllerPositionX, -0.1f, 0.1f), Mathf.Clamp(config.controllerPositionY, -0.1f, 0.1f), Mathf.Clamp(config.controllerPositionZ, -0.1f, 0.1f));
		this.controllerRotation.value = new Vector3(Mathf.Clamp(config.controllerRotationX, -180f, 180f), Mathf.Clamp(config.controllerRotationY, -180f, 180f), Mathf.Clamp(config.controllerRotationZ, -180f, 180f));
		this.mirrorGraphicsSettings.value = config.mirrorGraphicsSettings;
		this.mainEffectGraphicsSettings.value = config.mainEffectGraphicsSettings;
		this.bloomPrePassGraphicsSettings.value = config.bloomGraphicsSettings;
		this.smokeGraphicsSettings.value = (config.smokeGraphicsSettings == 1);
		this.enableAlphaFeatures.value = (config.enableAlphaFeatures == 1);
		this.burnMarkTrailsEnabled.value = config.burnMarkTrailsEnabled;
		this.screenDisplacementEffectsEnabled.value = config.screenDisplacementEffectsEnabled;
		this.maxShockwaveParticles.value = config.maxShockwaveParticles;
		this.smoothCameraEnabled.value = (config.smoothCameraEnabled > 0);
		this.smoothCameraFieldOfView.value = config.smoothCameraFieldOfView;
		this.smoothCameraThirdPersonPosition.value = new Vector3(config.smoothCameraThirdPersonPositionX, config.smoothCameraThirdPersonPositionY, config.smoothCameraThirdPersonPositionZ);
		this.smoothCameraThirdPersonEulerAngles.value = new Vector3(config.smoothCameraThirdPersonEulerAnglesX, config.smoothCameraThirdPersonEulerAnglesY, config.smoothCameraThirdPersonEulerAnglesZ);
		this.smoothCameraThirdPersonEnabled.value = (config.smoothCameraThirdPersonEnabled > 0);
		this.smoothCameraRotationSmooth.value = config.smoothCameraRotationSmooth;
		this.smoothCameraPositionSmooth.value = config.smoothCameraPositionSmooth;
		this.volume.value = config.volume;
		this.overrideAudioLatency.value = config.overrideAudioLatency;
		this.audioLatency.value = config.audioLatency;
		this.maxNumberOfCutSoundEffects.value = config.maxNumberOfCutSoundEffects;
		this.pauseButtonPressDurationLevel.value = config.pauseButtonPressDurationLevel;
		this.onlineServicesEnabled.value = config.onlineServicesEnabled;
		this.oculusMRCEnabled.value = config.oculusMRCEnabled;
		this.depthTextureEnabled.value = this.smokeGraphicsSettings;
		this.createScreenshotDuringTheGame = false;
		this._isLoaded = true;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x000247D0 File Offset: 0x000229D0
	public void __DeleteSettingsFiles()
	{
		string path = Path.Combine(Application.persistentDataPath, "settings.cfg");
		string path2 = Path.Combine(Application.persistentDataPath, "settings.cfg.bak");
		try
		{
			File.Delete(path);
			File.Delete(path2);
		}
		catch
		{
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00024820 File Offset: 0x00022A20
	protected override void OnEnable()
	{
		base.OnEnable();
		if (!this._playingForTheFirstTimeChecked)
		{
			this._playingForTheFirstTimeChecked = true;
			string path = Application.persistentDataPath + "/settings.cfg";
			this.playingForTheFirstTime = !File.Exists(path);
		}
		this.Load(true);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00005795 File Offset: 0x00003995
	protected void OnDisable()
	{
		this.Save();
	}

	// Token: 0x04000629 RID: 1577
	[SOVariable]
	public FloatSO vrResolutionScale;

	// Token: 0x0400062A RID: 1578
	[SOVariable]
	public FloatSO menuVRResolutionScaleMultiplier;

	// Token: 0x0400062B RID: 1579
	[SOVariable]
	public BoolSO useFixedFoveatedRenderingDuringGameplay;

	// Token: 0x0400062C RID: 1580
	[SOVariable]
	public Vector2IntSO windowResolution;

	// Token: 0x0400062D RID: 1581
	[SOVariable]
	public BoolSO fullscreen;

	// Token: 0x0400062E RID: 1582
	[SOVariable]
	public IntSO antiAliasingLevel;

	// Token: 0x0400062F RID: 1583
	[SOVariable]
	public FloatSO volume;

	// Token: 0x04000630 RID: 1584
	[SOVariable]
	public BoolSO controllersRumbleEnabled;

	// Token: 0x04000631 RID: 1585
	[SOVariable]
	public Vector3SO roomCenter;

	// Token: 0x04000632 RID: 1586
	[SOVariable]
	public FloatSO roomRotation;

	// Token: 0x04000633 RID: 1587
	[SOVariable]
	public Vector3SO controllerPosition;

	// Token: 0x04000634 RID: 1588
	[SOVariable]
	public Vector3SO controllerRotation;

	// Token: 0x04000635 RID: 1589
	[SOVariable]
	public IntSO mirrorGraphicsSettings;

	// Token: 0x04000636 RID: 1590
	[SOVariable]
	public IntSO mainEffectGraphicsSettings;

	// Token: 0x04000637 RID: 1591
	[SOVariable]
	public IntSO bloomPrePassGraphicsSettings;

	// Token: 0x04000638 RID: 1592
	[SOVariable]
	public BoolSO smokeGraphicsSettings;

	// Token: 0x04000639 RID: 1593
	[SOVariable]
	public BoolSO enableAlphaFeatures;

	// Token: 0x0400063A RID: 1594
	[SOVariable]
	public IntSO pauseButtonPressDurationLevel;

	// Token: 0x0400063B RID: 1595
	[SOVariable]
	public BoolSO burnMarkTrailsEnabled;

	// Token: 0x0400063C RID: 1596
	[SOVariable]
	public BoolSO screenDisplacementEffectsEnabled;

	// Token: 0x0400063D RID: 1597
	[SOVariable]
	public BoolSO smoothCameraEnabled;

	// Token: 0x0400063E RID: 1598
	[SOVariable]
	public FloatSO smoothCameraFieldOfView;

	// Token: 0x0400063F RID: 1599
	[SOVariable]
	public Vector3SO smoothCameraThirdPersonPosition;

	// Token: 0x04000640 RID: 1600
	[SOVariable]
	public Vector3SO smoothCameraThirdPersonEulerAngles;

	// Token: 0x04000641 RID: 1601
	[SOVariable]
	public BoolSO smoothCameraThirdPersonEnabled;

	// Token: 0x04000642 RID: 1602
	[SOVariable]
	public FloatSO smoothCameraRotationSmooth;

	// Token: 0x04000643 RID: 1603
	[SOVariable]
	public FloatSO smoothCameraPositionSmooth;

	// Token: 0x04000644 RID: 1604
	[SOVariable]
	public BoolSO overrideAudioLatency;

	// Token: 0x04000645 RID: 1605
	[SOVariable]
	public FloatSO audioLatency;

	// Token: 0x04000646 RID: 1606
	[SOVariable]
	public IntSO maxShockwaveParticles;

	// Token: 0x04000647 RID: 1607
	[SOVariable]
	public IntSO maxNumberOfCutSoundEffects;

	// Token: 0x04000648 RID: 1608
	[SOVariable]
	public BoolSO onlineServicesEnabled;

	// Token: 0x04000649 RID: 1609
	[SOVariable]
	public BoolSO oculusMRCEnabled;

	// Token: 0x0400064A RID: 1610
	[SOVariable]
	public BoolSO depthTextureEnabled;

	// Token: 0x0400064C RID: 1612
	public const float kDefaultPlayerHeight = 1.8f;

	// Token: 0x0400064D RID: 1613
	public const float kHeadPosToPlayerHeightOffset = 0.1f;

	// Token: 0x0400064E RID: 1614
	private const string kFileName = "settings.cfg";

	// Token: 0x0400064F RID: 1615
	private const string kTempFileName = "settings.cfg.tmp";

	// Token: 0x04000650 RID: 1616
	private const string kBackupFileName = "settings.cfg.bak";

	// Token: 0x04000651 RID: 1617
	private const string kCurrentVersion = "1.5.0";

	// Token: 0x04000652 RID: 1618
	public const float kControllersPositionOffsetLimit = 0.1f;

	// Token: 0x04000653 RID: 1619
	public const float kControllersRotationOffsetLimit = 180f;

	// Token: 0x04000655 RID: 1621
	private bool _playingForTheFirstTimeChecked;

	// Token: 0x04000656 RID: 1622
	private bool _isLoaded;

	// Token: 0x0200017D RID: 381
	public enum WindowMode
	{
		// Token: 0x04000658 RID: 1624
		Windowed,
		// Token: 0x04000659 RID: 1625
		Fullscreen
	}

	// Token: 0x0200017E RID: 382
	private class Config
	{
		// Token: 0x0400065A RID: 1626
		public string version = "1.5.0";

		// Token: 0x0400065B RID: 1627
		public int windowResolutionWidth = 1280;

		// Token: 0x0400065C RID: 1628
		public int windowResolutionHeight = 720;

		// Token: 0x0400065D RID: 1629
		public MainSettingsModelSO.WindowMode windowMode = MainSettingsModelSO.WindowMode.Fullscreen;

		// Token: 0x0400065E RID: 1630
		public float vrResolutionScale = 1f;

		// Token: 0x0400065F RID: 1631
		public float menuVRResolutionScaleMultiplier = 1f;

		// Token: 0x04000660 RID: 1632
		public bool useFixedFoveatedRenderingDuringGameplay;

		// Token: 0x04000661 RID: 1633
		public int antiAliasingLevel = 2;

		// Token: 0x04000662 RID: 1634
		public int mirrorGraphicsSettings = 2;

		// Token: 0x04000663 RID: 1635
		public int mainEffectGraphicsSettings = 1;

		// Token: 0x04000664 RID: 1636
		public int bloomGraphicsSettings;

		// Token: 0x04000665 RID: 1637
		public int smokeGraphicsSettings = 1;

		// Token: 0x04000666 RID: 1638
		public bool burnMarkTrailsEnabled = true;

		// Token: 0x04000667 RID: 1639
		public bool screenDisplacementEffectsEnabled = true;

		// Token: 0x04000668 RID: 1640
		public float roomCenterX;

		// Token: 0x04000669 RID: 1641
		public float roomCenterY;

		// Token: 0x0400066A RID: 1642
		public float roomCenterZ;

		// Token: 0x0400066B RID: 1643
		public float roomRotation;

		// Token: 0x0400066C RID: 1644
		public float controllerPositionX;

		// Token: 0x0400066D RID: 1645
		public float controllerPositionY;

		// Token: 0x0400066E RID: 1646
		public float controllerPositionZ;

		// Token: 0x0400066F RID: 1647
		public float controllerRotationX;

		// Token: 0x04000670 RID: 1648
		public float controllerRotationY;

		// Token: 0x04000671 RID: 1649
		public float controllerRotationZ;

		// Token: 0x04000672 RID: 1650
		public int smoothCameraEnabled;

		// Token: 0x04000673 RID: 1651
		public float smoothCameraFieldOfView = 70f;

		// Token: 0x04000674 RID: 1652
		public float smoothCameraThirdPersonPositionX;

		// Token: 0x04000675 RID: 1653
		public float smoothCameraThirdPersonPositionY = 1.5f;

		// Token: 0x04000676 RID: 1654
		public float smoothCameraThirdPersonPositionZ = -1.5f;

		// Token: 0x04000677 RID: 1655
		public float smoothCameraThirdPersonEulerAnglesX;

		// Token: 0x04000678 RID: 1656
		public float smoothCameraThirdPersonEulerAnglesY;

		// Token: 0x04000679 RID: 1657
		public float smoothCameraThirdPersonEulerAnglesZ;

		// Token: 0x0400067A RID: 1658
		public int smoothCameraThirdPersonEnabled;

		// Token: 0x0400067B RID: 1659
		public float smoothCameraRotationSmooth = 4f;

		// Token: 0x0400067C RID: 1660
		public float smoothCameraPositionSmooth = 4f;

		// Token: 0x0400067D RID: 1661
		public float volume = 1f;

		// Token: 0x0400067E RID: 1662
		public bool controllersRumbleEnabled = true;

		// Token: 0x0400067F RID: 1663
		public int enableAlphaFeatures;

		// Token: 0x04000680 RID: 1664
		public int pauseButtonPressDurationLevel;

		// Token: 0x04000681 RID: 1665
		public int maxShockwaveParticles = 1;

		// Token: 0x04000682 RID: 1666
		public bool overrideAudioLatency;

		// Token: 0x04000683 RID: 1667
		public float audioLatency;

		// Token: 0x04000684 RID: 1668
		public int maxNumberOfCutSoundEffects = 24;

		// Token: 0x04000685 RID: 1669
		public bool onlineServicesEnabled;

		// Token: 0x04000686 RID: 1670
		public bool oculusMRCEnabled;
	}
}
