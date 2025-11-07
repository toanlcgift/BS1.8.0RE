using System;

// Token: 0x02000468 RID: 1128
public class MissionGameplaySceneSetupData : SceneSetupData
{
	// Token: 0x06001542 RID: 5442 RVA: 0x0000FF29 File Offset: 0x0000E129
	public MissionGameplaySceneSetupData(MissionObjective[] missionObjectives, bool autoRestart, string songName, string songSubName, string difficultyName, string backButtonText)
	{
		this.missionObjectives = missionObjectives;
		this.autoRestart = autoRestart;
		this.songName = songName;
		this.songSubName = songSubName;
		this.difficultyName = difficultyName;
		this.backButtonText = backButtonText;
	}

	// Token: 0x04001532 RID: 5426
	public readonly MissionObjective[] missionObjectives;

	// Token: 0x04001533 RID: 5427
	public readonly bool autoRestart;

	// Token: 0x04001534 RID: 5428
	public readonly string songName;

	// Token: 0x04001535 RID: 5429
	public readonly string songSubName;

	// Token: 0x04001536 RID: 5430
	public readonly string difficultyName;

	// Token: 0x04001537 RID: 5431
	public readonly string backButtonText;
}
