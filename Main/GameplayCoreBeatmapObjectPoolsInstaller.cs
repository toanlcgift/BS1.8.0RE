using System;
using UnityEngine;
using Zenject;

// Token: 0x0200033F RID: 831
public class GameplayCoreBeatmapObjectPoolsInstaller : MonoInstaller
{
	// Token: 0x06000E76 RID: 3702 RVA: 0x0003B51C File Offset: 0x0003971C
	public override void InstallBindings()
	{
		base.Container.BindMemoryPool<NoteController, NoteController.Pool>().WithId(NoteType.NoteA).WithInitialSize(25).FromComponentInNewPrefab(this._normalBasicNotePrefab);
		base.Container.BindMemoryPool<NoteController, NoteController.Pool>().WithId(NoteType.NoteB).WithInitialSize(25).FromComponentInNewPrefab(this._normalBasicNotePrefab);
		base.Container.BindMemoryPool<NoteController, NoteController.Pool>().WithId(NoteType.Bomb).WithInitialSize(25).FromComponentInNewPrefab(this._bombNotePrefab);
		base.Container.BindMemoryPool<ObstacleController, ObstacleController.Pool>().WithInitialSize(25).FromComponentInNewPrefab(this._obstaclePrefab);
		base.Container.BindMemoryPool<NoteLineConnectionController, NoteLineConnectionController.Pool>().WithInitialSize(10).FromComponentInNewPrefab(this._noteLineConnectionControllerPrefab);
		base.Container.BindMemoryPool<BeatLine, BeatLine.Pool>().WithInitialSize(16).FromComponentInNewPrefab(this._beatLinePrefab);
	}

	// Token: 0x04000EE5 RID: 3813
	[SerializeField]
	private NoteController _normalBasicNotePrefab;

	// Token: 0x04000EE6 RID: 3814
	[SerializeField]
	private BombNoteController _bombNotePrefab;

	// Token: 0x04000EE7 RID: 3815
	[SerializeField]
	private ObstacleController _obstaclePrefab;

	// Token: 0x04000EE8 RID: 3816
	[SerializeField]
	private NoteLineConnectionController _noteLineConnectionControllerPrefab;

	// Token: 0x04000EE9 RID: 3817
	[SerializeField]
	private BeatLine _beatLinePrefab;
}
