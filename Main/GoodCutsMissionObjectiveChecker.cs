using System;
using Zenject;

// Token: 0x020002D5 RID: 725
public class GoodCutsMissionObjectiveChecker : SimpleValueMissionObjectiveChecker
{
	// Token: 0x06000C39 RID: 3129 RVA: 0x00035F78 File Offset: 0x00034178
	protected override void Init()
	{
		this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCut;
		if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min || this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal)
		{
			base.status = MissionObjectiveChecker.Status.NotClearedYet;
			return;
		}
		base.status = MissionObjectiveChecker.Status.NotFailedYet;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x0000992F File Offset: 0x00007B2F
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00035FE0 File Offset: 0x000341E0
	private void HandleNoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		NoteType noteType = noteController.noteData.noteType;
		if ((noteType == NoteType.NoteA || noteType == NoteType.NoteB) && noteCutInfo.allIsOK)
		{
			int checkedValue = base.checkedValue;
			base.checkedValue = checkedValue + 1;
		}
		base.CheckAndUpdateStatus();
	}

	// Token: 0x04000CD3 RID: 3283
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;
}
