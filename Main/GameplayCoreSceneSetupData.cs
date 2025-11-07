using System;

// Token: 0x02000466 RID: 1126
public class GameplayCoreSceneSetupData : SceneSetupData
{
	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06001535 RID: 5429 RVA: 0x0000FE90 File Offset: 0x0000E090
	// (set) Token: 0x06001536 RID: 5430 RVA: 0x0000FE98 File Offset: 0x0000E098
	public IDifficultyBeatmap difficultyBeatmap { get; private set; }

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06001537 RID: 5431 RVA: 0x0000FEA1 File Offset: 0x0000E0A1
	// (set) Token: 0x06001538 RID: 5432 RVA: 0x0000FEA9 File Offset: 0x0000E0A9
	public GameplayModifiers gameplayModifiers { get; private set; }

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06001539 RID: 5433 RVA: 0x0000FEB2 File Offset: 0x0000E0B2
	// (set) Token: 0x0600153A RID: 5434 RVA: 0x0000FEBA File Offset: 0x0000E0BA
	public PlayerSpecificSettings playerSpecificSettings { get; private set; }

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x0600153B RID: 5435 RVA: 0x0000FEC3 File Offset: 0x0000E0C3
	// (set) Token: 0x0600153C RID: 5436 RVA: 0x0000FECB File Offset: 0x0000E0CB
	public PracticeSettings practiceSettings { get; private set; }

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x0600153D RID: 5437 RVA: 0x0000FED4 File Offset: 0x0000E0D4
	// (set) Token: 0x0600153E RID: 5438 RVA: 0x0000FEDC File Offset: 0x0000E0DC
	public bool useTestNoteCutSoundEffects { get; private set; }

	// Token: 0x0600153F RID: 5439 RVA: 0x0000FEE5 File Offset: 0x0000E0E5
	public GameplayCoreSceneSetupData(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, PracticeSettings practiceSettings, bool useTestNoteCutSoundEffects)
	{
		this.difficultyBeatmap = difficultyBeatmap;
		this.gameplayModifiers = gameplayModifiers;
		this.playerSpecificSettings = playerSpecificSettings;
		this.practiceSettings = practiceSettings;
		this.useTestNoteCutSoundEffects = useTestNoteCutSoundEffects;
	}
}
