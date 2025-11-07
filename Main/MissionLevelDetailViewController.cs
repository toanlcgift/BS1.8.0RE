using System;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F0 RID: 1008
public class MissionLevelDetailViewController : ViewController
{
	// Token: 0x140000AA RID: 170
	// (add) Token: 0x060012E1 RID: 4833 RVA: 0x000468DC File Offset: 0x00044ADC
	// (remove) Token: 0x060012E2 RID: 4834 RVA: 0x00046914 File Offset: 0x00044B14
	public event Action<MissionLevelDetailViewController> didPressPlayButtonEvent;

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0000E472 File Offset: 0x0000C672
	public MissionNode missionNode
	{
		get
		{
			return this._missionNode;
		}
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x0000E47A File Offset: 0x0000C67A
	public void Setup(MissionNode missionNode)
	{
		this._missionNode = missionNode;
		if (base.isInViewControllerHierarchy)
		{
			this.RefreshContent();
		}
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x0000E491 File Offset: 0x0000C691
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._playButton, new Action(this.PlayButtonPressed));
		}
		this.RefreshContent();
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x0004694C File Offset: 0x00044B4C
	public void RefreshContent()
	{
		MissionDataSO missionData = this._missionNode.missionData;
		BeatmapLevelSO level = missionData.level;
		this._missionText.text = Localization.Get("CAMPAIGN_MISSION") + " " + this._missionNode.formattedMissionNodeName;
		this._songNameText.text = string.Format(Localization.Get("CAMPAIGN_SONG"), level.songName);
		this._difficultyText.text = string.Format(Localization.Get("CAMPAIGN_DIFFICULTY"), missionData.beatmapDifficulty.Name());
		this._characteristicsText.text = string.Format(Localization.Get("CAMPAIGN_TYPE"), Localization.Get(missionData.beatmapCharacteristic.characteristicNameLocalizationKey));
		MissionObjective[] missionObjectives = missionData.missionObjectives;
		this._objectiveListItems.SetData((missionObjectives.Length == 0) ? 1 : missionObjectives.Length, delegate(int idx, ObjectiveListItem objectiveListItem)
		{
			if (idx == 0 && missionObjectives.Length == 0)
			{
				objectiveListItem.title = Localization.Get("CAMPAIGN_FINISH_LEVEL");
				objectiveListItem.conditionText = "";
				objectiveListItem.hideCondition = true;
				return;
			}
			MissionObjective missionObjective = missionObjectives[idx];
			if (missionObjective.type.noConditionValue)
			{
				objectiveListItem.title = missionObjective.type.objectiveNameLocalized.Replace(" ", "\n");
				objectiveListItem.hideCondition = true;
				return;
			}
			objectiveListItem.title = missionObjective.type.objectiveNameLocalized;
			objectiveListItem.hideCondition = false;
			ObjectiveValueFormatterSO objectiveValueFormater = missionObjective.type.objectiveValueFormater;
			objectiveListItem.conditionText = string.Format("{0} {1}", missionObjective.referenceValueComparisonType.Name(), objectiveValueFormater.FormatValue(missionObjective.referenceValue));
		});
		List<GameplayModifierParamsSO> modifierParamsList = this._gameplayModifiersModel.GetModifierParams(missionData.gameplayModifiers);
		this._modifiersPanelGO.SetActive(modifierParamsList.Count > 0);
		this._gameplayModifierInfoListItemsList.SetData(modifierParamsList.Count, delegate(int idx, GameplayModifierInfoListItem gameplayModifierInfoListItem)
		{
			GameplayModifierParamsSO gameplayModifierParamsSO = modifierParamsList[idx];
			gameplayModifierInfoListItem.modifierIcon = gameplayModifierParamsSO.icon;
			gameplayModifierInfoListItem.modifierName = Localization.Get(gameplayModifierParamsSO.modifierNameLocalizationKey);
			gameplayModifierInfoListItem.modifierDescription = Localization.Get(gameplayModifierParamsSO.descriptionLocalizationKey);
			gameplayModifierInfoListItem.showSeparator = (idx != modifierParamsList.Count - 1);
		});
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x0000E4B9 File Offset: 0x0000C6B9
	private void PlayButtonPressed()
	{
		Action<MissionLevelDetailViewController> action = this.didPressPlayButtonEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x04001296 RID: 4758
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModel;

	// Token: 0x04001297 RID: 4759
	[Space]
	[SerializeField]
	private Button _playButton;

	// Token: 0x04001298 RID: 4760
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x04001299 RID: 4761
	[SerializeField]
	private TextMeshProUGUI _missionText;

	// Token: 0x0400129A RID: 4762
	[SerializeField]
	private TextMeshProUGUI _difficultyText;

	// Token: 0x0400129B RID: 4763
	[SerializeField]
	private TextMeshProUGUI _characteristicsText;

	// Token: 0x0400129C RID: 4764
	[SerializeField]
	private ObjectiveListItemsList _objectiveListItems;

	// Token: 0x0400129D RID: 4765
	[SerializeField]
	private GameplayModifierInfoListItemsList _gameplayModifierInfoListItemsList;

	// Token: 0x0400129E RID: 4766
	[SerializeField]
	private GameObject _modifiersPanelGO;

	// Token: 0x040012A0 RID: 4768
	private MissionNode _missionNode;
}
