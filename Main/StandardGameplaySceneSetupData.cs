using System;
using Zenject;

// Token: 0x0200046B RID: 1131
[ZenjectAllowDuringValidation]
public class StandardGameplaySceneSetupData : SceneSetupData
{
	// Token: 0x06001548 RID: 5448 RVA: 0x0000FF95 File Offset: 0x0000E195
	public StandardGameplaySceneSetupData(bool autoRestart, string songName, string songSubName, string difficultyName, string backButtonText)
	{
		this.autoRestart = autoRestart;
		this.songName = songName;
		this.songSubName = songSubName;
		this.difficultyName = difficultyName;
		this.backButtonText = backButtonText;
	}

	// Token: 0x0400153A RID: 5434
	public readonly bool autoRestart;

	// Token: 0x0400153B RID: 5435
	public readonly string songName;

	// Token: 0x0400153C RID: 5436
	public readonly string songSubName;

	// Token: 0x0400153D RID: 5437
	public readonly string difficultyName;

	// Token: 0x0400153E RID: 5438
	public readonly string backButtonText;
}
