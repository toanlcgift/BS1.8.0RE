using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004EC RID: 1260
	public class Editor3DInstaller : MonoInstaller
	{
		// Token: 0x0600178A RID: 6026 RVA: 0x00054CF4 File Offset: 0x00052EF4
		public override void InstallBindings()
		{
			base.Container.Bind<ProjectController>().FromInstance(this._projectController);
			base.Container.Bind<SongAudioController>().FromInstance(this._songAudioController);
			base.Container.Bind<GridController>().FromInstance(this._gridController);
			base.Container.Bind<GridConfig>().FromInstance(this._gridConfig.gridConfig);
			base.Container.Bind<GridColorScheme>().FromInstance(this._gridColorScheme.colorScheme);
			base.Container.Bind<BeatmapObjectsConfig>().FromInstance(this._beatmapObjectsConfigSO.beatmapObjectsConfig);
			base.Container.Bind<IUISnapToBorder>().To<UISnapToBorder>().AsSingle();
			base.Container.Bind<IUISnapToGrid>().To<UISnapToGrid>().AsSingle();
		}

		// Token: 0x0400174E RID: 5966
		[SerializeField]
		private ProjectController _projectController;

		// Token: 0x0400174F RID: 5967
		[SerializeField]
		private SongAudioController _songAudioController;

		// Token: 0x04001750 RID: 5968
		[SerializeField]
		private GridController _gridController;

		// Token: 0x04001751 RID: 5969
		[SerializeField]
		private GridConfigSO _gridConfig;

		// Token: 0x04001752 RID: 5970
		[SerializeField]
		private GridColorSchemeSO _gridColorScheme;

		// Token: 0x04001753 RID: 5971
		[SerializeField]
		private BeatmapObjectsConfigSO _beatmapObjectsConfigSO;
	}
}
