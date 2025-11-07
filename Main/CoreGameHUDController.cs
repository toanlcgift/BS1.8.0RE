using System;
using UnityEngine;
using Zenject;

// Token: 0x020002A9 RID: 681
public class CoreGameHUDController : MonoBehaviour
{
	// Token: 0x06000B7E RID: 2942 RVA: 0x00034628 File Offset: 0x00032828
	protected void Start()
	{
		this._songProgressPanelGO.SetActive(this._initData.advancedHUD);
		this._relativeScoreGO.SetActive(this._initData.advancedHUD);
		this._immediateRankGO.SetActive(this._initData.advancedHUD);
		this._energyPanelGO.SetActive(this._initData.showEnergyPanel);
	}

	// Token: 0x04000C27 RID: 3111
	[SerializeField]
	private GameObject _songProgressPanelGO;

	// Token: 0x04000C28 RID: 3112
	[SerializeField]
	private GameObject _relativeScoreGO;

	// Token: 0x04000C29 RID: 3113
	[SerializeField]
	private GameObject _immediateRankGO;

	// Token: 0x04000C2A RID: 3114
	[SerializeField]
	private GameObject _energyPanelGO;

	// Token: 0x04000C2B RID: 3115
	[Inject]
	private CoreGameHUDController.InitData _initData;

	// Token: 0x020002AA RID: 682
	public class InitData
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00009126 File Offset: 0x00007326
		public InitData(bool showEnergyPanel, bool advancedHUD)
		{
			this.showEnergyPanel = showEnergyPanel;
			this.advancedHUD = advancedHUD;
		}

		// Token: 0x04000C2C RID: 3116
		public readonly bool showEnergyPanel;

		// Token: 0x04000C2D RID: 3117
		public readonly bool advancedHUD;
	}
}
