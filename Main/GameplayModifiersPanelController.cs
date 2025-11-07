using System;
using HMUI;
using TMPro;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public class GameplayModifiersPanelController : MonoBehaviour, IRefreshable
{
	// Token: 0x170003AB RID: 939
	// (get) Token: 0x0600120C RID: 4620 RVA: 0x0000DC30 File Offset: 0x0000BE30
	public GameplayModifiers gameplayModifiers
	{
		get
		{
			return this._gameplayModifiers;
		}
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0000DC38 File Offset: 0x0000BE38
	public void SetData(GameplayModifiers gameplayModifiers)
	{
		this._gameplayModifiers = gameplayModifiers;
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x00043EE4 File Offset: 0x000420E4
	protected void Awake()
	{
		this._toggleBinder = new ToggleBinder();
		this._gameplayModifierToggles = base.GetComponentsInChildren<GameplayModifierToggle>();
		GameplayModifierToggle[] gameplayModifierToggles = this._gameplayModifierToggles;
		for (int i = 0; i < gameplayModifierToggles.Length; i++)
		{
			GameplayModifierToggle gameplayModifierToggle = gameplayModifierToggles[i];
			this._toggleBinder.AddBinding(gameplayModifierToggle.toggle, delegate(bool on)
			{
				if (this._changingGameplayModifierToggles)
				{
					return;
				}
				this._changingGameplayModifierToggles = true;
				this._gameplayModifiersModel.SetModifierBoolValue(this._gameplayModifiers, gameplayModifierToggle.gameplayModifier, on);
				if (on)
				{
					foreach (GameplayModifierParamsSO gameplayModifier in gameplayModifierToggle.gameplayModifier.mutuallyExclusives)
					{
						this.DisableTogglesWithGameplayModifier(gameplayModifier);
					}
				}
				this.RefreshTotalMultiplierAndRankUI();
				this._changingGameplayModifierToggles = false;
			});
		}
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x0000DC41 File Offset: 0x0000BE41
	private void OnDestroy()
	{
		ToggleBinder toggleBinder = this._toggleBinder;
		if (toggleBinder == null)
		{
			return;
		}
		toggleBinder.ClearBindings();
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x00043F58 File Offset: 0x00042158
	private void DisableTogglesWithGameplayModifier(GameplayModifierParamsSO gameplayModifier)
	{
		foreach (GameplayModifierToggle gameplayModifierToggle in this._gameplayModifierToggles)
		{
			if (gameplayModifierToggle.gameplayModifier == gameplayModifier)
			{
				gameplayModifierToggle.toggle.isOn = false;
			}
		}
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x00043F98 File Offset: 0x00042198
	public void Refresh()
	{
		foreach (GameplayModifierToggle gameplayModifierToggle in this._gameplayModifierToggles)
		{
			gameplayModifierToggle.toggle.isOn = this._gameplayModifiersModel.GetModifierBoolValue(this._gameplayModifiers, gameplayModifierToggle.gameplayModifier);
		}
		this.RefreshTotalMultiplierAndRankUI();
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x00043FE8 File Offset: 0x000421E8
	private void RefreshTotalMultiplierAndRankUI()
	{
		float totalMultiplier = this._gameplayModifiersModel.GetTotalMultiplier(this._gameplayModifiers);
		this._totalMultiplierValueText.text = string.Format("{0:F2}x", totalMultiplier);
		RankModel.Rank rank = RankModel.MaxRankForGameplayModifiers(this.gameplayModifiers, this._gameplayModifiersModel);
		this._maxRankValueText.text = RankModel.GetRankName(rank);
	}

	// Token: 0x040011CF RID: 4559
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModel;

	// Token: 0x040011D0 RID: 4560
	[SerializeField]
	private TextMeshProUGUI _totalMultiplierValueText;

	// Token: 0x040011D1 RID: 4561
	[SerializeField]
	private TextMeshProUGUI _maxRankValueText;

	// Token: 0x040011D2 RID: 4562
	private GameplayModifiers _gameplayModifiers;

	// Token: 0x040011D3 RID: 4563
	private ToggleBinder _toggleBinder;

	// Token: 0x040011D4 RID: 4564
	private GameplayModifierToggle[] _gameplayModifierToggles;

	// Token: 0x040011D5 RID: 4565
	private bool _changingGameplayModifierToggles;
}
