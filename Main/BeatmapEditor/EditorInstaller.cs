using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x02000518 RID: 1304
	public class EditorInstaller : MonoInstaller
	{
		// Token: 0x060018BB RID: 6331 RVA: 0x00057420 File Offset: 0x00055620
		public override void InstallBindings()
		{
			base.Container.Bind<IPlayheadBeatIndex>().FromInstance(this._beatmapEditorScrollView);
			base.Container.Bind<IBeatmapEditorObstacleLength>().FromInstance(this._obstacleTypePanelController);
			base.Container.Bind<IBeatmapEditorObstacleType>().FromInstance(this._obstacleTypePanelController);
			base.Container.BindFactory<EventsPanelButton, EventsPanelButton.Factory>().FromComponentInNewPrefab(this._eventsPanelButtonPrefab);
			base.Container.BindFactory<EventHeaderCell, EventHeaderCell.Factory>().FromComponentInNewPrefab(this._eventHeaderCellPrefab);
		}

		// Token: 0x0400184A RID: 6218
		[SerializeField]
		private BeatmapEditorScrollView _beatmapEditorScrollView;

		// Token: 0x0400184B RID: 6219
		[SerializeField]
		private ObstacleTypePanelController _obstacleTypePanelController;

		// Token: 0x0400184C RID: 6220
		[SerializeField]
		private EventsPanelButton _eventsPanelButtonPrefab;

		// Token: 0x0400184D RID: 6221
		[SerializeField]
		private EventHeaderCell _eventHeaderCellPrefab;
	}
}
