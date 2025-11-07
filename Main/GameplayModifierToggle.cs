using System;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000393 RID: 915
public class GameplayModifierToggle : MonoBehaviour
{
	// Token: 0x1700036D RID: 877
	// (get) Token: 0x0600109E RID: 4254 RVA: 0x0000CA4A File Offset: 0x0000AC4A
	public Toggle toggle
	{
		get
		{
			return this._toggle;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x0600109F RID: 4255 RVA: 0x0000CA52 File Offset: 0x0000AC52
	public GameplayModifierParamsSO gameplayModifier
	{
		get
		{
			return this._gameplayModifier;
		}
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x000410B0 File Offset: 0x0003F2B0
	protected void Start()
	{
		if (this._gameplayModifier.multiplier >= 0f)
		{
			this._nameText.text = string.Format("{0}\n<color=#ffffff88><size=80%>+{1:F2}</size></color>", Localization.Get(this._gameplayModifier.modifierNameLocalizationKey), this._gameplayModifier.multiplier);
		}
		else
		{
			this._nameText.text = string.Format("{0}\n<color=#ffffff88><size=80%>{1:F2}</size></color>", Localization.Get(this._gameplayModifier.modifierNameLocalizationKey), this._gameplayModifier.multiplier);
		}
		this._hoverHint.text = Localization.Get(this._gameplayModifier.descriptionLocalizationKey);
		this._icon.sprite = this._gameplayModifier.icon;
	}

	// Token: 0x040010BA RID: 4282
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private GameplayModifierParamsSO _gameplayModifier;

	// Token: 0x040010BB RID: 4283
	[SerializeField]
	private TextMeshProUGUI _nameText;

	// Token: 0x040010BC RID: 4284
	[SerializeField]
	private HoverHint _hoverHint;

	// Token: 0x040010BD RID: 4285
	[SerializeField]
	private Image _icon;

	// Token: 0x040010BE RID: 4286
	[SerializeField]
	private Toggle _toggle;
}
