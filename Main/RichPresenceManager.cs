using System;
using UnityEngine;
using Zenject;

// Token: 0x02000217 RID: 535
public class RichPresenceManager : MonoBehaviour
{
	// Token: 0x06000864 RID: 2148 RVA: 0x00006E19 File Offset: 0x00005019
	protected void Awake()
	{
		this._gameScenesManager.transitionDidFinishEvent += this.HandleGameScenesManagerTransitionDidFinish;
		this._playingCampaignRichPresenceData = new PlayingCampaignRichPresenceData();
		this._playingTutorialPresenceData = new PlayingTutorialPresenceData();
		this._browsingMenusRichPresenceData = new BrowsingMenusRichPresenceData();
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00006E53 File Offset: 0x00005053
	protected void OnDestroy()
	{
		if (this._gameScenesManager != null)
		{
			this._gameScenesManager.transitionDidFinishEvent -= this.HandleGameScenesManagerTransitionDidFinish;
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0002A1AC File Offset: 0x000283AC
	private void HandleGameScenesManagerTransitionDidFinish(ScenesTransitionSetupDataSO scenesTransitionSetupData, DiContainer diContainer)
	{
		if (scenesTransitionSetupData == this._menuScenesTransitionSetupData)
		{
			this._richPresencePlatformHandler.SetPresence(this._browsingMenusRichPresenceData);
			this._menuWasLoaded = true;
			return;
		}
		if (scenesTransitionSetupData == this._standardLevelScenesTransitionSetupData)
		{
			PlayingDifficultyBeatmapRichPresenceData presence = new PlayingDifficultyBeatmapRichPresenceData(diContainer.Resolve<IDifficultyBeatmap>());
			this._richPresencePlatformHandler.SetPresence(presence);
			return;
		}
		if (scenesTransitionSetupData == this._missionLevelScenesTransitionSetupData)
		{
			this._richPresencePlatformHandler.SetPresence(this._playingCampaignRichPresenceData);
			return;
		}
		if (scenesTransitionSetupData == this._tutorialScenesTransitionSetupData)
		{
			this._richPresencePlatformHandler.SetPresence(this._playingTutorialPresenceData);
			return;
		}
		if (scenesTransitionSetupData == null && this._menuWasLoaded)
		{
			this._richPresencePlatformHandler.SetPresence(this._browsingMenusRichPresenceData);
			return;
		}
		this._richPresencePlatformHandler.Clear();
	}

	// Token: 0x040008DB RID: 2267
	[SerializeField]
	private ScenesTransitionSetupDataSO _standardLevelScenesTransitionSetupData;

	// Token: 0x040008DC RID: 2268
	[SerializeField]
	private ScenesTransitionSetupDataSO _tutorialScenesTransitionSetupData;

	// Token: 0x040008DD RID: 2269
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _missionLevelScenesTransitionSetupData;

	// Token: 0x040008DE RID: 2270
	[Inject]
	private MenuScenesTransitionSetupDataSO _menuScenesTransitionSetupData;

	// Token: 0x040008DF RID: 2271
	[Inject]
	private IRichPresencePlatformHandler _richPresencePlatformHandler;

	// Token: 0x040008E0 RID: 2272
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x040008E1 RID: 2273
	private bool _menuWasLoaded;

	// Token: 0x040008E2 RID: 2274
	private BrowsingMenusRichPresenceData _browsingMenusRichPresenceData;

	// Token: 0x040008E3 RID: 2275
	private PlayingCampaignRichPresenceData _playingCampaignRichPresenceData;

	// Token: 0x040008E4 RID: 2276
	private PlayingTutorialPresenceData _playingTutorialPresenceData;
}
