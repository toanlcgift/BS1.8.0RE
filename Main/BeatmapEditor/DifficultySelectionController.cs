using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000559 RID: 1369
	public class DifficultySelectionController : MonoBehaviour
	{
		// Token: 0x06001A9C RID: 6812 RVA: 0x0005CB78 File Offset: 0x0005AD78
		protected void Start()
		{
			this._difficultyDropdown.onValueChanged.AddListener(new UnityAction<int>(this.HandleDifficultyDropdownValueChanged));
			this._activeCharacteristicDifficulty.didChangeEvent += this.HandleActiveDifficultyDidChange;
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			this._difficulties = (BeatmapDifficulty[])Enum.GetValues(typeof(BeatmapDifficulty));
			foreach (BeatmapDifficulty difficulty in this._difficulties)
			{
				list.Add(new Dropdown.OptionData(difficulty.Name()));
			}
			this._difficultyDropdown.AddOptions(list);
			this.RefreshUI();
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0005CC14 File Offset: 0x0005AE14
		protected void OnDestroy()
		{
			if (this._difficultyDropdown != null)
			{
				this._difficultyDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.HandleDifficultyDropdownValueChanged));
			}
			if (this._activeCharacteristicDifficulty != null)
			{
				this._activeCharacteristicDifficulty.didChangeEvent -= this.HandleActiveDifficultyDidChange;
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0005CC70 File Offset: 0x0005AE70
		private void RefreshUI()
		{
			for (int i = 0; i < this._difficulties.Length; i++)
			{
				if (this._difficulties[i] == this._activeCharacteristicDifficulty.characteristicDifficulty.difficulty)
				{
					this._difficultyDropdown.value = i;
				}
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00013B0E File Offset: 0x00011D0E
		private void HandleActiveDifficultyDidChange(BeatmapCharacteristicDifficulty prevCharacteristicDifficulty, BeatmapCharacteristicDifficulty currentCharacteristicDifficulty)
		{
			this.RefreshUI();
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0005CCB8 File Offset: 0x0005AEB8
		private void HandleDifficultyDropdownValueChanged(int value)
		{
			this._activeCharacteristicDifficulty.characteristicDifficulty = new BeatmapCharacteristicDifficulty
			{
				characteristicSerializedName = this._activeCharacteristicDifficulty.characteristicDifficulty.characteristicSerializedName,
				difficulty = this._difficulties[value]
			};
		}

		// Token: 0x0400197C RID: 6524
		[SerializeField]
		private ActiveCharacteristicDifficultySO _activeCharacteristicDifficulty;

		// Token: 0x0400197D RID: 6525
		[SerializeField]
		private Dropdown _difficultyDropdown;

		// Token: 0x0400197E RID: 6526
		private BeatmapDifficulty[] _difficulties;
	}
}
