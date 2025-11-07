using System;

// Token: 0x02000470 RID: 1136
public class BeatmapEditorScenesTransitionSetupDataSO : SingleFixedSceneScenesTransitionSetupDataSO
{
	// Token: 0x140000C8 RID: 200
	// (add) Token: 0x06001550 RID: 5456 RVA: 0x0004E4FC File Offset: 0x0004C6FC
	// (remove) Token: 0x06001551 RID: 5457 RVA: 0x0004E534 File Offset: 0x0004C734
	public event Action<BeatmapEditorScenesTransitionSetupDataSO> didFinishEvent;

	// Token: 0x06001552 RID: 5458 RVA: 0x00010006 File Offset: 0x0000E206
	public void Init()
	{
		base.Init(new BeatmapEditorSceneSetupData(null, null));
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x00010015 File Offset: 0x0000E215
	public void Finish()
	{
		Action<BeatmapEditorScenesTransitionSetupDataSO> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}
}
