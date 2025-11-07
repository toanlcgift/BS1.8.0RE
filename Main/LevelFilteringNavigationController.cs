using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x020003DE RID: 990
public class LevelFilteringNavigationController : NavigationController
{
	// Token: 0x1400009F RID: 159
	// (add) Token: 0x0600125D RID: 4701 RVA: 0x00044EB0 File Offset: 0x000430B0
	// (remove) Token: 0x0600125E RID: 4702 RVA: 0x00044EE8 File Offset: 0x000430E8
	public event Action<LevelFilteringNavigationController, IAnnotatedBeatmapLevelCollection, GameObject, BeatmapCharacteristicSO> didSelectAnnotatedBeatmapLevelCollectionEvent;

	// Token: 0x140000A0 RID: 160
	// (add) Token: 0x0600125F RID: 4703 RVA: 0x00044F20 File Offset: 0x00043120
	// (remove) Token: 0x06001260 RID: 4704 RVA: 0x00044F58 File Offset: 0x00043158
	public event Action<LevelFilteringNavigationController> didStartLoadingEvent;

	// Token: 0x06001261 RID: 4705 RVA: 0x00044F90 File Offset: 0x00043190
	public void Setup(bool hideIfOneOrNoPacks, bool enableCustomLevels, IAnnotatedBeatmapLevelCollection selectedAnnotatedBeatmapLevelCollection)
	{
		this._hideIfOneOrNoPacks = hideIfOneOrNoPacks;
		this._enableCustomLevels = enableCustomLevels;
		this._customSongsListNeedsReload = true;
		this.InitializeIfNeeded();
		LevelFilteringNavigationController.TabBarData[] tabBarDatas = this._tabBarDatas;
		for (int i = 0; i < tabBarDatas.Length; i++)
		{
			tabBarDatas[i].selectedItem = 0;
		}
		if (selectedAnnotatedBeatmapLevelCollection == null)
		{
			this._tabBarViewController.SelectItem(0);
			this.TabBarDidSwitch();
			return;
		}
		this.SelectAnnotatedBeatmapLevelCollection(selectedAnnotatedBeatmapLevelCollection);
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x00044FF4 File Offset: 0x000431F4
	public void SelectAnnotatedBeatmapLevelCollection(IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection)
	{
		IBeatmapLevelPack beatmapLevelPack = annotatedBeatmapLevelCollection as IBeatmapLevelPack;
		IPlaylist playList = annotatedBeatmapLevelCollection as IPlaylist;
		this.SelectBeatmapLevelPackOrPlayList(beatmapLevelPack, playList);
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x00045018 File Offset: 0x00043218
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this.InitializeIfNeeded();
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._annotatedBeatmapLevelCollectionsViewController.didSelectAnnotatedBeatmapLevelCollectionEvent += this.HandleAnnotatedBeatmapLevelCollectionsViewControllerDidSelectAnnotatedBeatmapLevelCollection;
			this._playerDataModel.playerData.favoriteLevelsSetDidChangeEvent += this.HandlePlayerDataFavoriteLevelsSetDidChange;
			base.SetChildViewControllers(new ViewController[]
			{
				this._tabBarViewController,
				this._annotatedBeatmapLevelCollectionsViewController
			});
		}
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00045084 File Offset: 0x00043284
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this._cancellationTokenSource = null;
			this._annotatedBeatmapLevelCollectionsViewController.didSelectAnnotatedBeatmapLevelCollectionEvent -= this.HandleAnnotatedBeatmapLevelCollectionsViewControllerDidSelectAnnotatedBeatmapLevelCollection;
			this._playerDataModel.playerData.favoriteLevelsSetDidChangeEvent -= this.HandlePlayerDataFavoriteLevelsSetDidChange;
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x0000DF01 File Offset: 0x0000C101
	private void InitializeIfNeeded()
	{
		if (this._initialized)
		{
			return;
		}
		this.InitPlaylists();
		this.UpdatePlaylistsData();
		this.InitializeTabBarViewController();
		this._initialized = true;
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000450E0 File Offset: 0x000432E0
	private void InitializeTabBarViewController()
	{
		List<LevelFilteringNavigationController.TabBarData> list = new List<LevelFilteringNavigationController.TabBarData>();
		List<LevelFilteringNavigationController.TabBarData> list2 = list;
		LevelFilteringNavigationController.TabBarData tabBarData = new LevelFilteringNavigationController.TabBarData();
		tabBarData.tabBarTitle = Localization.Get("OST_&_EXTRA_TABBAR_TITLE");
		tabBarData.action = new Action(this.TabBarDidSwitch);
		tabBarData.selectedItem = 0;
		IAnnotatedBeatmapLevelCollection[] annotatedBeatmapLevelCollections = this._beatmapLevelsModel.ostAndExtrasPackCollection.beatmapLevelPacks;
		tabBarData.annotatedBeatmapLevelCollections = annotatedBeatmapLevelCollections;
		list2.Add(tabBarData);
		List<LevelFilteringNavigationController.TabBarData> list3 = list;
		LevelFilteringNavigationController.TabBarData tabBarData2 = new LevelFilteringNavigationController.TabBarData();
		tabBarData2.tabBarTitle = Localization.Get("MUSIC_PACKS_TABBAR_TITLE");
		tabBarData2.action = new Action(this.TabBarDidSwitch);
		tabBarData2.selectedItem = 0;
		annotatedBeatmapLevelCollections = this._beatmapLevelsModel.dlcBeatmapLevelPackCollection.beatmapLevelPacks;
		tabBarData2.annotatedBeatmapLevelCollections = annotatedBeatmapLevelCollections;
		list3.Add(tabBarData2);
		LevelFilteringNavigationController.TabBarData tabBarData3 = new LevelFilteringNavigationController.TabBarData();
		tabBarData3.tabBarTitle = Localization.Get("PLAYLISTS_TABBAR_TITLE");
		tabBarData3.action = new Action(this.SwitchToPlaylists);
		tabBarData3.selectedItem = 0;
		annotatedBeatmapLevelCollections = this._playlists;
		tabBarData3.annotatedBeatmapLevelCollections = annotatedBeatmapLevelCollections;
		this._playlistTabBarData = tabBarData3;
		list.Add(this._playlistTabBarData);
		if (this._enableCustomLevels)
		{
			this._customLevelsTabBarData = new LevelFilteringNavigationController.TabBarData
			{
				tabBarTitle = Localization.Get("TITLE_CUSTOM_LEVELS"),
				action = new Action(this.SwitchWithReloadIfNeeded),
				selectedItem = 0,
				annotatedBeatmapLevelCollections = null
			};
			list.Add(this._customLevelsTabBarData);
		}
		this._tabBarDatas = list.ToArray();
		this._tabBarViewController.sizeToFit = true;
		this._tabBarViewController.Setup((from x in this._tabBarDatas
		select new TabBarViewController.TabBarItem(x.tabBarTitle, x.action)).ToArray<TabBarViewController.TabBarItem>());
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x0004527C File Offset: 0x0004347C
	private void UpdatePlaylistsData()
	{
		this._userFavoritesPlaylist.SetupFromLevelPackCollection(this._playerDataModel.playerData.favoritesLevelIds, this._beatmapLevelsModel.allLoadedBeatmapLevelPackCollection);
		this._allSongsPlaylist.SetupFromLevelPackCollection(this._beatmapLevelsModel.allLoadedBeatmapLevelPackCollection);
		foreach (FilteredByBeatmapCharacteristicPlaylistSO filteredByBeatmapCharacteristicPlaylistSO in this._filteredByBeatmapCharacteristicPlaylists)
		{
			if (!filteredByBeatmapCharacteristicPlaylistSO.beatmapCharacteristic.requires360Movement || this._appStaticSettings.enable360DegreeLevels)
			{
				filteredByBeatmapCharacteristicPlaylistSO.SetupFromLevelPackCollection(this._beatmapLevelsModel.allLoadedBeatmapLevelPackCollection);
			}
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x0004530C File Offset: 0x0004350C
	private void InitPlaylists()
	{
		List<IPlaylist> list = new List<IPlaylist>();
		list.Add(this._userFavoritesPlaylist);
		list.Add(this._allSongsPlaylist);
		foreach (FilteredByBeatmapCharacteristicPlaylistSO filteredByBeatmapCharacteristicPlaylistSO in this._filteredByBeatmapCharacteristicPlaylists)
		{
			if (!filteredByBeatmapCharacteristicPlaylistSO.beatmapCharacteristic.requires360Movement || this._appStaticSettings.enable360DegreeLevels)
			{
				list.Add(filteredByBeatmapCharacteristicPlaylistSO);
			}
		}
		this._playlists = list.ToArray();
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x00045380 File Offset: 0x00043580
	private void HandlePlayerDataFavoriteLevelsSetDidChange()
	{
		this._userFavoritesPlaylist.SetupFromLevelPackCollection(this._playerDataModel.playerData.favoritesLevelIds, this._beatmapLevelsModel.allLoadedBeatmapLevelPackCollection);
		this._annotatedBeatmapLevelCollectionsViewController.RefreshAvailability();
		if (this._annotatedBeatmapLevelCollectionsViewController.isInViewControllerHierarchy && this._annotatedBeatmapLevelCollectionsViewController.selectedAnnotatedBeatmapLevelCollection is UserFavoritesPlaylistSO)
		{
			Action<LevelFilteringNavigationController, IAnnotatedBeatmapLevelCollection, GameObject, BeatmapCharacteristicSO> action = this.didSelectAnnotatedBeatmapLevelCollectionEvent;
			if (action == null)
			{
				return;
			}
			action(this, this._userFavoritesPlaylist, this._emptyFavoritesListInfoPrefab, null);
		}
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x0000DF25 File Offset: 0x0000C125
	private void HandleAnnotatedBeatmapLevelCollectionsViewControllerDidSelectAnnotatedBeatmapLevelCollection(IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection)
	{
		this._tabBarDatas[this._tabBarViewController.selectedCellNumber].selectedItem = this._annotatedBeatmapLevelCollectionsViewController.selectedItemIndex;
		this.SendEventIfNeeded(annotatedBeatmapLevelCollection);
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000453FC File Offset: 0x000435FC
	private void SelectBeatmapLevelPackOrPlayList(IBeatmapLevelPack beatmapLevelPack, IPlaylist playList)
	{
		for (int i = 0; i < this._tabBarDatas.Length; i++)
		{
			LevelFilteringNavigationController.TabBarData tabBarData = this._tabBarDatas[i];
			if (tabBarData.annotatedBeatmapLevelCollections != null)
			{
				for (int j = 0; j < tabBarData.annotatedBeatmapLevelCollections.Length; j++)
				{
					IBeatmapLevelPack beatmapLevelPack2 = tabBarData.annotatedBeatmapLevelCollections[j] as IBeatmapLevelPack;
					IPlaylist playlist = tabBarData.annotatedBeatmapLevelCollections[j] as IPlaylist;
					if (beatmapLevelPack2 != null && beatmapLevelPack != null && beatmapLevelPack2.packID == beatmapLevelPack.packID)
					{
						this._tabBarViewController.SelectItem(i);
						tabBarData.selectedItem = j;
						this.TabBarDidSwitch();
						return;
					}
					if (playlist != null && playlist == playList)
					{
						this._tabBarViewController.SelectItem(i);
						tabBarData.selectedItem = j;
						this.SwitchToPlaylists();
						return;
					}
				}
			}
		}
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000454C0 File Offset: 0x000436C0
	private void TabBarDidSwitch()
	{
		LevelFilteringNavigationController.TabBarData tabBarData = this._tabBarDatas[this._tabBarViewController.selectedCellNumber];
		this._annotatedBeatmapLevelCollectionsViewController.SetData(tabBarData.annotatedBeatmapLevelCollections, tabBarData.selectedItem, this._hideIfOneOrNoPacks);
		this.SendEventIfNeeded(tabBarData.selectedAnnotatedBeatmapLevelCollections);
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0004550C File Offset: 0x0004370C
	private void SendEventIfNeeded(IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection)
	{
		FilteredByBeatmapCharacteristicPlaylistSO filteredByBeatmapCharacteristicPlaylistSO = annotatedBeatmapLevelCollection as FilteredByBeatmapCharacteristicPlaylistSO;
		BeatmapCharacteristicSO arg = (filteredByBeatmapCharacteristicPlaylistSO != null) ? filteredByBeatmapCharacteristicPlaylistSO.beatmapCharacteristic : null;
		GameObject arg2 = (annotatedBeatmapLevelCollection == this._userFavoritesPlaylist) ? this._emptyFavoritesListInfoPrefab : null;
		if (this._customLevelsTabBarData == this._tabBarDatas[this._tabBarViewController.selectedCellNumber])
		{
			arg2 = this._emptyCustomSongListInfoPrefab;
		}
		Action<LevelFilteringNavigationController, IAnnotatedBeatmapLevelCollection, GameObject, BeatmapCharacteristicSO> action = this.didSelectAnnotatedBeatmapLevelCollectionEvent;
		if (action == null)
		{
			return;
		}
		action(this, annotatedBeatmapLevelCollection, arg2, arg);
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0000DF50 File Offset: 0x0000C150
	private void SwitchToPlaylists()
	{
		if (!this._enableCustomLevels)
		{
			this.TabBarDidSwitch();
			return;
		}
		this.SwitchWithReloadIfNeeded();
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x0000DF67 File Offset: 0x0000C167
	private void SwitchWithReloadIfNeeded()
	{
		this.ReloadSongListIfNeeded();
		if (this.IsLoading())
		{
			Action<LevelFilteringNavigationController> action = this.didStartLoadingEvent;
			if (action != null)
			{
				action(this);
			}
			this._annotatedBeatmapLevelCollectionsViewController.ShowLoading();
			return;
		}
		this.TabBarDidSwitch();
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x0000DF9B File Offset: 0x0000C19B
	private void ReloadSongListIfNeeded()
	{
		if (this._customSongsListNeedsReload)
		{
			this._customSongsListNeedsReload = false;
			this.UpdateCustomSongs();
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
	private bool IsLoading()
	{
		return this._cancellationTokenSource != null;
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x00045574 File Offset: 0x00043774
	private async void UpdateCustomSongs()
	{
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = new CancellationTokenSource();
		CancellationToken cancellationToken = this._cancellationTokenSource.Token;
		try
		{
			IBeatmapLevelPackCollection beatmapLevelPackCollection = await this._beatmapLevelsModel.GetCustomLevelPackCollectionAsync(cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
			this._customLevelsTabBarData.annotatedBeatmapLevelCollections = beatmapLevelPackCollection.beatmapLevelPacks;
			this.UpdatePlaylistsData();
			if (this._customLevelsTabBarData == this._tabBarDatas[this._tabBarViewController.selectedCellNumber])
			{
				this.TabBarDidSwitch();
			}
			else if (this._playlistTabBarData == this._tabBarDatas[this._tabBarViewController.selectedCellNumber])
			{
				this.TabBarDidSwitch();
			}
		}
		catch (OperationCanceledException)
		{
		}
		finally
		{
			this._cancellationTokenSource = null;
		}
	}

	// Token: 0x0400121A RID: 4634
	[SerializeField]
	private FilteredByBeatmapCharacteristicPlaylistSO[] _filteredByBeatmapCharacteristicPlaylists;

	// Token: 0x0400121B RID: 4635
	[SerializeField]
	private UserFavoritesPlaylistSO _userFavoritesPlaylist;

	// Token: 0x0400121C RID: 4636
	[SerializeField]
	private AllSongsPlaylistSO _allSongsPlaylist;

	// Token: 0x0400121D RID: 4637
	[Space]
	[SerializeField]
	private GameObject _emptyFavoritesListInfoPrefab;

	// Token: 0x0400121E RID: 4638
	[SerializeField]
	private GameObject _emptyCustomSongListInfoPrefab;

	// Token: 0x0400121F RID: 4639
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04001220 RID: 4640
	[Inject]
	private TabBarViewController _tabBarViewController;

	// Token: 0x04001221 RID: 4641
	[Inject]
	private AnnotatedBeatmapLevelCollectionsViewController _annotatedBeatmapLevelCollectionsViewController;

	// Token: 0x04001222 RID: 4642
	[Inject]
	private BeatmapLevelsModel _beatmapLevelsModel;

	// Token: 0x04001223 RID: 4643
	[Inject]
	private AppStaticSettingsSO _appStaticSettings;

	// Token: 0x04001226 RID: 4646
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x04001227 RID: 4647
	private bool _hideIfOneOrNoPacks;

	// Token: 0x04001228 RID: 4648
	private IPlaylist[] _playlists;

	// Token: 0x04001229 RID: 4649
	private bool _initialized;

	// Token: 0x0400122A RID: 4650
	private bool _enableCustomLevels;

	// Token: 0x0400122B RID: 4651
	private bool _customSongsListNeedsReload;

	// Token: 0x0400122C RID: 4652
	private LevelFilteringNavigationController.TabBarData[] _tabBarDatas;

	// Token: 0x0400122D RID: 4653
	private LevelFilteringNavigationController.TabBarData _playlistTabBarData;

	// Token: 0x0400122E RID: 4654
	private LevelFilteringNavigationController.TabBarData _customLevelsTabBarData;

	// Token: 0x020003DF RID: 991
	private class TabBarData
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		public IAnnotatedBeatmapLevelCollection selectedAnnotatedBeatmapLevelCollections
		{
			get
			{
				if (this.annotatedBeatmapLevelCollections == null || this.annotatedBeatmapLevelCollections.Length <= this.selectedItem)
				{
					return null;
				}
				return this.annotatedBeatmapLevelCollections[this.selectedItem];
			}
		}

		// Token: 0x0400122F RID: 4655
		public string tabBarTitle;

		// Token: 0x04001230 RID: 4656
		public Action action;

		// Token: 0x04001231 RID: 4657
		public int selectedItem;

		// Token: 0x04001232 RID: 4658
		public IAnnotatedBeatmapLevelCollection[] annotatedBeatmapLevelCollections;
	}
}
