using System;
using System.Collections.Generic;
using Polyglot;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000556 RID: 1366
	public class CharacteristicSelectionController : MonoBehaviour
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x0005C588 File Offset: 0x0005A788
		protected void Start()
		{
			this._characteristicDropdown.onValueChanged.AddListener(new UnityAction<int>(this.HandleCharacteristicDropdownValueChanged));
			this._activeCharacteristicDifficulty.didChangeEvent += this.HandleActiveBeatmapCharacteristicDifficultyDidChange;
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			foreach (BeatmapCharacteristicSO beatmapCharacteristicSO in this._allBeatmapCharacteristicCollection.beatmapCharacteristics)
			{
				list.Add(new Dropdown.OptionData(Localization.Get(beatmapCharacteristicSO.characteristicNameLocalizationKey)));
			}
			this._characteristicDropdown.AddOptions(list);
			this.RefreshUI();
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0005C614 File Offset: 0x0005A814
		protected void OnDestroy()
		{
			if (this._characteristicDropdown != null)
			{
				this._characteristicDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.HandleCharacteristicDropdownValueChanged));
			}
			if (this._activeCharacteristicDifficulty != null)
			{
				this._activeCharacteristicDifficulty.didChangeEvent -= this.HandleActiveBeatmapCharacteristicDifficultyDidChange;
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0005C670 File Offset: 0x0005A870
		private void RefreshUI()
		{
			for (int i = 0; i < this._allBeatmapCharacteristicCollection.beatmapCharacteristics.Length; i++)
			{
				if (this._allBeatmapCharacteristicCollection.beatmapCharacteristics[i].serializedName == this._activeCharacteristicDifficulty.characteristicDifficulty.characteristicSerializedName)
				{
					this._characteristicDropdown.value = i;
				}
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000139C1 File Offset: 0x00011BC1
		private void HandleActiveBeatmapCharacteristicDifficultyDidChange(BeatmapCharacteristicDifficulty prevCharacteristicDifficulty, BeatmapCharacteristicDifficulty currentCharacteristicDifficulty)
		{
			this.RefreshUI();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0005C6CC File Offset: 0x0005A8CC
		private void HandleCharacteristicDropdownValueChanged(int value)
		{
			this._activeCharacteristicDifficulty.characteristicDifficulty = new BeatmapCharacteristicDifficulty
			{
				characteristicSerializedName = this._allBeatmapCharacteristicCollection.beatmapCharacteristics[value].serializedName,
				difficulty = this._activeCharacteristicDifficulty.characteristicDifficulty.difficulty
			};
		}

		// Token: 0x04001965 RID: 6501
		[SerializeField]
		private ActiveCharacteristicDifficultySO _activeCharacteristicDifficulty;

		// Token: 0x04001966 RID: 6502
		[SerializeField]
		private Dropdown _characteristicDropdown;

		// Token: 0x04001967 RID: 6503
		[SerializeField]
		private BeatmapCharacteristicCollectionSO _allBeatmapCharacteristicCollection;
	}
}
