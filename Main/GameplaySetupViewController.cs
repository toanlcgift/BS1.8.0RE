using System;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x020003D5 RID: 981
public class GameplaySetupViewController : ViewController
{
	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06001216 RID: 4630 RVA: 0x0000DC53 File Offset: 0x0000BE53
	public PlayerSpecificSettings playerSettings
	{
		get
		{
			return this._playerDataModel.playerData.playerSpecificSettings;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001217 RID: 4631 RVA: 0x0000DC65 File Offset: 0x0000BE65
	public GameplayModifiers gameplayModifiers
	{
		get
		{
			return this._playerDataModel.playerData.gameplayModifiers;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001218 RID: 4632 RVA: 0x0000DC77 File Offset: 0x0000BE77
	public OverrideEnvironmentSettings environmentOverrideSettings
	{
		get
		{
			return this._playerDataModel.playerData.overrideEnvironmentSettings;
		}
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06001219 RID: 4633 RVA: 0x0000DC89 File Offset: 0x0000BE89
	public ColorSchemesSettings colorSchemesSettings
	{
		get
		{
			return this._playerDataModel.playerData.colorSchemesSettings;
		}
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x0000DC9B File Offset: 0x0000BE9B
	public void Setup(bool showModifiers, bool showEnvironmentOverrideSettings, bool showColorSchemesSettings)
	{
		this.Init();
		this._showModifiers = showModifiers;
		this._showEnvironmentOverrideSettings = showEnvironmentOverrideSettings;
		this._showColorSchemesSettings = showColorSchemesSettings;
		if (base.isActiveAndEnabled)
		{
			this.RefreshContent();
			this._shouldRefreshContent = false;
			return;
		}
		this._shouldRefreshContent = true;
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000440E0 File Offset: 0x000422E0
	private void Init()
	{
		if (this._isInitialized)
		{
			return;
		}
		this._isInitialized = true;
		this._playerSettingsPanelController.SetData(this.playerSettings);
		this._gameplayModifiersPanelController.SetData(this.gameplayModifiers);
		this._environmentOverrideSettingsPanelController.SetData(this.environmentOverrideSettings);
		this._colorsOverrideSettingsPanelController.SetData(this.colorSchemesSettings);
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x00044144 File Offset: 0x00042344
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this.Init();
			this._selectionSegmentedControl.didSelectCellEvent += this.HandleSelectionSegmentedControlDidSelectCell;
		}
		if (this._shouldRefreshContent || firstActivation)
		{
			this._shouldRefreshContent = false;
			this.RefreshContent();
		}
		this.SetActivePanel(this._selectionSegmentedControl.selectedCellNumber);
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x0000DCD5 File Offset: 0x0000BED5
	private void HandleSelectionSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellIdx)
	{
		this.SetActivePanel(cellIdx);
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x0004419C File Offset: 0x0004239C
	private void SetActivePanel(int panelIdx)
	{
		for (int i = 0; i < this._panels.Count; i++)
		{
			this._panels[i].gameObject.SetActive(i == panelIdx);
		}
		this._activePanelIdx = panelIdx;
		this.RefreshActivePanel();
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000441E8 File Offset: 0x000423E8
	private void RefreshContent()
	{
		this._panels = new List<GameplaySetupViewController.Panel>(4);
		if (this._showModifiers)
		{
			this._panels.Add(new GameplaySetupViewController.Panel(Localization.Get("BUTTON_MODIFIERS"), this._gameplayModifiersPanelController, this._gameplayModifiersPanelController.gameObject));
		}
		else
		{
			this._gameplayModifiersPanelController.gameObject.SetActive(false);
		}
		this._panels.Add(new GameplaySetupViewController.Panel(Localization.Get("BUTTON_PLAYER_SETTINGS"), this._playerSettingsPanelController, this._playerSettingsPanelController.gameObject));
		if (this._showEnvironmentOverrideSettings)
		{
			this._panels.Add(new GameplaySetupViewController.Panel(Localization.Get("BUTTON_ENVIRONMENTS"), this._environmentOverrideSettingsPanelController, this._environmentOverrideSettingsPanelController.gameObject));
		}
		else
		{
			this._environmentOverrideSettingsPanelController.gameObject.SetActive(false);
		}
		if (this._showColorSchemesSettings)
		{
			this._panels.Add(new GameplaySetupViewController.Panel(Localization.Get("BUTTON_COLORS"), this._colorsOverrideSettingsPanelController, this._colorsOverrideSettingsPanelController.gameObject));
		}
		else
		{
			this._colorsOverrideSettingsPanelController.gameObject.SetActive(false);
		}
		List<string> list = new List<string>(this._panels.Count);
		foreach (GameplaySetupViewController.Panel panel in this._panels)
		{
			list.Add(panel.title);
		}
		this._selectionSegmentedControl.SetTexts(list.ToArray());
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x0000DCDE File Offset: 0x0000BEDE
	private void RefreshActivePanel()
	{
		if (this._panels != null)
		{
			this._panels[this._activePanelIdx].refreshable.Refresh();
		}
	}

	// Token: 0x040011D8 RID: 4568
	[SerializeField]
	private TextSegmentedControl _selectionSegmentedControl;

	// Token: 0x040011D9 RID: 4569
	[SerializeField]
	private PlayerSettingsPanelController _playerSettingsPanelController;

	// Token: 0x040011DA RID: 4570
	[SerializeField]
	private GameplayModifiersPanelController _gameplayModifiersPanelController;

	// Token: 0x040011DB RID: 4571
	[SerializeField]
	private EnvironmentOverrideSettingsPanelController _environmentOverrideSettingsPanelController;

	// Token: 0x040011DC RID: 4572
	[SerializeField]
	private ColorsOverrideSettingsPanelController _colorsOverrideSettingsPanelController;

	// Token: 0x040011DD RID: 4573
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x040011DE RID: 4574
	private List<GameplaySetupViewController.Panel> _panels;

	// Token: 0x040011DF RID: 4575
	private int _activePanelIdx;

	// Token: 0x040011E0 RID: 4576
	private bool _showModifiers;

	// Token: 0x040011E1 RID: 4577
	private bool _showEnvironmentOverrideSettings;

	// Token: 0x040011E2 RID: 4578
	private bool _showColorSchemesSettings;

	// Token: 0x040011E3 RID: 4579
	private bool _shouldRefreshContent;

	// Token: 0x040011E4 RID: 4580
	private bool _isInitialized;

	// Token: 0x020003D6 RID: 982
	private class Panel
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x0000DD03 File Offset: 0x0000BF03
		public Panel(string title, IRefreshable refreshable, GameObject gameObject)
		{
			this.title = title;
			this.refreshable = refreshable;
			this.gameObject = gameObject;
		}

		// Token: 0x040011E5 RID: 4581
		public readonly string title;

		// Token: 0x040011E6 RID: 4582
		public readonly IRefreshable refreshable;

		// Token: 0x040011E7 RID: 4583
		public readonly GameObject gameObject;
	}
}
