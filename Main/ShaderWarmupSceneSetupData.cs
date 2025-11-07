using System;

// Token: 0x0200046A RID: 1130
public class ShaderWarmupSceneSetupData : SceneSetupData
{
	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06001545 RID: 5445 RVA: 0x0000FF75 File Offset: 0x0000E175
	// (set) Token: 0x06001546 RID: 5446 RVA: 0x0000FF7D File Offset: 0x0000E17D
	public ScenesTransitionSetupDataSO nextScenesTransitionSetupData { get; private set; }

	// Token: 0x06001547 RID: 5447 RVA: 0x0000FF86 File Offset: 0x0000E186
	public ShaderWarmupSceneSetupData(ScenesTransitionSetupDataSO nextScenesTransitionSetupData)
	{
		this.nextScenesTransitionSetupData = nextScenesTransitionSetupData;
	}
}
