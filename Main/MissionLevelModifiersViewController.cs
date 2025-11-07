using System;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public class MissionLevelModifiersViewController : ViewController
{
	// Token: 0x060012EC RID: 4844 RVA: 0x0000E4CC File Offset: 0x0000C6CC
	public void Setup(GameplayModifiers gameplayModifiers)
	{
		this._gameplayModifiers = gameplayModifiers;
		if (base.isInViewControllerHierarchy)
		{
			this.RefreshContent();
		}
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		this.RefreshContent();
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x00046BC0 File Offset: 0x00044DC0
	public void RefreshContent()
	{
		List<GameplayModifierParamsSO> modifierParamsList = this._gameplayModifiersModel.GetModifierParams(this._gameplayModifiers);
		this._gameplayModifierInfoListItemsList.SetData(modifierParamsList.Count, delegate(int idx, GameplayModifierInfoListItem gameplayModifierInfoListItem)
		{
			GameplayModifierParamsSO gameplayModifierParamsSO = modifierParamsList[idx];
			gameplayModifierInfoListItem.modifierIcon = gameplayModifierParamsSO.icon;
			gameplayModifierInfoListItem.modifierName = Localization.Get(gameplayModifierParamsSO.modifierNameLocalizationKey);
			gameplayModifierInfoListItem.modifierDescription = Localization.Get(gameplayModifierParamsSO.descriptionLocalizationKey);
			gameplayModifierInfoListItem.showSeparator = (idx != modifierParamsList.Count - 1);
		});
		if (modifierParamsList.Count == 0)
		{
			this._titleText.text = "No modifiers are active in this mission.";
			this._modifiersPanel.SetActive(false);
			return;
		}
		this._titleText.text = "These modifiers will affect the gemaplay of selected mission.";
		this._modifiersPanel.SetActive(true);
	}

	// Token: 0x040012A3 RID: 4771
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModel;

	// Token: 0x040012A4 RID: 4772
	[Space]
	[SerializeField]
	private GameplayModifierInfoListItemsList _gameplayModifierInfoListItemsList;

	// Token: 0x040012A5 RID: 4773
	[SerializeField]
	private GameObject _modifiersPanel;

	// Token: 0x040012A6 RID: 4774
	[SerializeField]
	private TextMeshProUGUI _titleText;

	// Token: 0x040012A7 RID: 4775
	private GameplayModifiers _gameplayModifiers;
}
