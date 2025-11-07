using System;
using UnityEngine;
using Zenject;

// Token: 0x02000343 RID: 835
public class TutorialBeatmapObjectPoolsInstaller : MonoInstaller
{
	// Token: 0x06000E7E RID: 3710 RVA: 0x0003B600 File Offset: 0x00039800
	public override void InstallBindings()
	{
		base.Container.BindMemoryPool<NoteController, NoteController.Pool>().WithId(NoteType.NoteA).WithInitialSize(20).FromComponentInNewPrefab(this._basicNotePrefab);
		base.Container.BindMemoryPool<NoteController, NoteController.Pool>().WithId(NoteType.NoteB).WithInitialSize(20).FromComponentInNewPrefab(this._basicNotePrefab);
		base.Container.BindMemoryPool<NoteController, NoteController.Pool>().WithId(NoteType.Bomb).WithInitialSize(20).FromComponentInNewPrefab(this._bombNotePrefab);
		base.Container.BindMemoryPool<ObstacleController, ObstacleController.Pool>().WithInitialSize(4).FromComponentInNewPrefab(this._obstaclePrefab);
		base.Container.BindMemoryPool<NoteLineConnectionController, NoteLineConnectionController.Pool>().WithInitialSize(10).FromComponentInNewPrefab(this._noteLineConnectionControllerPrefab);
	}

	// Token: 0x04000EF2 RID: 3826
	[SerializeField]
	private NoteController _basicNotePrefab;

	// Token: 0x04000EF3 RID: 3827
	[SerializeField]
	private BombNoteController _bombNotePrefab;

	// Token: 0x04000EF4 RID: 3828
	[SerializeField]
	private ObstacleController _obstaclePrefab;

	// Token: 0x04000EF5 RID: 3829
	[SerializeField]
	private NoteLineConnectionController _noteLineConnectionControllerPrefab;
}
