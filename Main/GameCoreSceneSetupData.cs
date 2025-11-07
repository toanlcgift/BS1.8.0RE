using System;

// Token: 0x02000465 RID: 1125
public class GameCoreSceneSetupData : SceneSetupData
{
	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06001532 RID: 5426 RVA: 0x0000FE70 File Offset: 0x0000E070
	// (set) Token: 0x06001533 RID: 5427 RVA: 0x0000FE78 File Offset: 0x0000E078
	public ColorScheme colorScheme { get; private set; }

	// Token: 0x06001534 RID: 5428 RVA: 0x0000FE81 File Offset: 0x0000E081
	public GameCoreSceneSetupData(ColorScheme colorScheme)
	{
		this.colorScheme = colorScheme;
	}
}
