using System;
using Zenject;

// Token: 0x020002D4 RID: 724
public class FullComboMissionObjectiveChecker : MissionObjectiveChecker
{
	// Token: 0x06000C35 RID: 3125 RVA: 0x000098C8 File Offset: 0x00007AC8
	protected override void Init()
	{
		base.status = MissionObjectiveChecker.Status.NotFailedYet;
		this._scoreController.comboBreakingEventHappenedEvent -= this.HandleComboBreakingEventHappened;
		this._scoreController.comboBreakingEventHappenedEvent += this.HandleComboBreakingEventHappened;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x000098FF File Offset: 0x00007AFF
	protected void OnDestroy()
	{
		if (this._scoreController != null)
		{
			this._scoreController.comboBreakingEventHappenedEvent -= this.HandleComboBreakingEventHappened;
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00009926 File Offset: 0x00007B26
	private void HandleComboBreakingEventHappened()
	{
		base.status = MissionObjectiveChecker.Status.Failed;
	}

	// Token: 0x04000CD2 RID: 3282
	[Inject]
	private ScoreController _scoreController;
}
