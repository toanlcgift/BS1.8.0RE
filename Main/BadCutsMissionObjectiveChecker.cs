using System;
using Zenject;

// Token: 0x020002D1 RID: 721
public class BadCutsMissionObjectiveChecker : SimpleValueMissionObjectiveChecker
{
	// Token: 0x06000C28 RID: 3112 RVA: 0x00035D8C File Offset: 0x00033F8C
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

	// Token: 0x06000C29 RID: 3113 RVA: 0x00009816 File Offset: 0x00007A16
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00035DF4 File Offset: 0x00033FF4
	private void HandleNoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		NoteType noteType = noteController.noteData.noteType;
		if ((noteType == NoteType.NoteA || noteType == NoteType.NoteB) && !noteCutInfo.allIsOK)
		{
			int checkedValue = base.checkedValue;
			base.checkedValue = checkedValue + 1;
		}
		base.CheckAndUpdateStatus();
	}

	// Token: 0x04000CCF RID: 3279
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;
}
