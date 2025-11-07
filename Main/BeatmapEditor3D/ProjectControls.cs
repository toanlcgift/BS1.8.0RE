using System;
using System.Collections.Generic;
using BeatmapEditor;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004F7 RID: 1271
	public class ProjectControls : MonoBehaviour
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x000559CC File Offset: 0x00053BCC
		protected void Start()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._openButton,
					delegate()
					{
						this.OpenLevel();
					}
				}
			});
			if (this._activeCharacteristicDifficulty != null)
			{
				this._activeCharacteristicDifficulty.didChangeEvent += this.HandleActiveBeatmapCharacteristicDifficultyDidChange;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0001186D File Offset: 0x0000FA6D
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			if (this._activeCharacteristicDifficulty != null)
			{
				this._activeCharacteristicDifficulty.didChangeEvent -= this.HandleActiveBeatmapCharacteristicDifficultyDidChange;
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000118A7 File Offset: 0x0000FAA7
		private void OpenLevel()
		{
			this._projectController.OpenLevel();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000118B4 File Offset: 0x0000FAB4
		private void HandleActiveBeatmapCharacteristicDifficultyDidChange(BeatmapCharacteristicDifficulty prevCharacteristicDifficulty, BeatmapCharacteristicDifficulty currentCharacteristicDifficulty)
		{
			if (prevCharacteristicDifficulty != currentCharacteristicDifficulty)
			{
				this._projectController.OpenBeatmapCharacteristicDifficulty(currentCharacteristicDifficulty);
			}
		}

		// Token: 0x04001796 RID: 6038
		[SerializeField]
		private Button _openButton;

		// Token: 0x04001797 RID: 6039
		[SerializeField]
		private ActiveCharacteristicDifficultySO _activeCharacteristicDifficulty;

		// Token: 0x04001798 RID: 6040
		[Inject]
		private ProjectController _projectController;

		// Token: 0x04001799 RID: 6041
		private ButtonBinder _buttonBinder;
	}
}
