using System;
using Zenject;

// Token: 0x020002D7 RID: 727
public class MissMissionObjectiveChecker : SimpleValueMissionObjectiveChecker
{
	// Token: 0x06000C41 RID: 3137 RVA: 0x00036070 File Offset: 0x00034270
	protected override void Init()
	{
		this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissed;
		this._beatmapObjectManager.noteWasMissedEvent += this.HandleNoteWasMissed;
		if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min || this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal)
		{
			base.status = MissionObjectiveChecker.Status.NotClearedYet;
			return;
		}
		base.status = MissionObjectiveChecker.Status.NotFailedYet;
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x0000998D File Offset: 0x00007B8D
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissed;
		}
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x000360D8 File Offset: 0x000342D8
	private void HandleNoteWasMissed(INoteController noteController)
	{
		NoteType noteType = noteController.noteData.noteType;
		if (noteType == NoteType.NoteA || noteType == NoteType.NoteB)
		{
			int checkedValue = base.checkedValue;
			base.checkedValue = checkedValue + 1;
		}
		base.CheckAndUpdateStatus();
	}

	// Token: 0x04000CD5 RID: 3285
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;
}
