using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004EE RID: 1262
	public class UserInputInstaller : MonoInstaller
	{
		// Token: 0x0600178E RID: 6030 RVA: 0x00054DC8 File Offset: 0x00052FC8
		public override void InstallBindings()
		{
			base.Container.Bind<UserInputManager>().FromInstance(this._userInputManager);
			base.Container.Bind<KeyboardShortcutManager>().FromInstance(this._keyboardShortcutManager);
			base.Container.Bind<UserInputConfigSO>().FromInstance(this._userInputConfigSO);
			base.Container.Bind<UserInputConfig>().FromInstance(this._userInputConfigSO.userInputConfig);
			base.Container.Bind<KeyboardController>().FromInstance(this._keyboardController);
			base.Container.Bind<MouseController>().FromInstance(this._mouseController);
			base.Container.Bind<MouseScrollController>().FromInstance(this._mouseScrollController);
			base.Container.Bind<Raycaster>().FromInstance(this._raycaster);
		}

		// Token: 0x04001755 RID: 5973
		[SerializeField]
		private UserInputManager _userInputManager;

		// Token: 0x04001756 RID: 5974
		[SerializeField]
		private UserInputConfigSO _userInputConfigSO;

		// Token: 0x04001757 RID: 5975
		[SerializeField]
		private KeyboardShortcutManager _keyboardShortcutManager;

		// Token: 0x04001758 RID: 5976
		[SerializeField]
		private KeyboardController _keyboardController;

		// Token: 0x04001759 RID: 5977
		[SerializeField]
		private MouseController _mouseController;

		// Token: 0x0400175A RID: 5978
		[SerializeField]
		private MouseScrollController _mouseScrollController;

		// Token: 0x0400175B RID: 5979
		private Raycaster _raycaster = new Raycaster();
	}
}
