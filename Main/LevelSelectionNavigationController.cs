using System;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x020003E7 RID: 999
public class LevelSelectionNavigationController : NavigationController
{
	// Token: 0x140000A1 RID: 161
	// (add) Token: 0x0600128C RID: 4748 RVA: 0x00045CB8 File Offset: 0x00043EB8
	// (remove) Token: 0x0600128D RID: 4749 RVA: 0x00045CF0 File Offset: 0x00043EF0
	public event Action<LevelSelectionNavigationController, StandardLevelDetailViewController.ContentType> didPresentDetailContentEvent;

	// Token: 0x140000A2 RID: 162
	// (add) Token: 0x0600128E RID: 4750 RVA: 0x00045D28 File Offset: 0x00043F28
	// (remove) Token: 0x0600128F RID: 4751 RVA: 0x00045D60 File Offset: 0x00043F60
	public event Action<LevelSelectionNavigationController, IBeatmapLevelPack> didSelectLevelPackEvent;

	// Token: 0x140000A3 RID: 163
	// (add) Token: 0x06001290 RID: 4752 RVA: 0x00045D98 File Offset: 0x00043F98
	// (remove) Token: 0x06001291 RID: 4753 RVA: 0x00045DD0 File Offset: 0x00043FD0
	public event Action<LevelSelectionNavigationController> didPressPlayButtonEvent;

	// Token: 0x140000A4 RID: 164
	// (add) Token: 0x06001292 RID: 4754 RVA: 0x00045E08 File Offset: 0x00044008
	// (remove) Token: 0x06001293 RID: 4755 RVA: 0x00045E40 File Offset: 0x00044040
	public event Action<LevelSelectionNavigationController, IBeatmapLevelPack> didPressOpenPackButtonEvent;

	// Token: 0x140000A5 RID: 165
	// (add) Token: 0x06001294 RID: 4756 RVA: 0x00045E78 File Offset: 0x00044078
	// (remove) Token: 0x06001295 RID: 4757 RVA: 0x00045EB0 File Offset: 0x000440B0
	public event Action<LevelSelectionNavigationController, IBeatmapLevel> didPressPracticeButtonEvent;

	// Token: 0x140000A6 RID: 166
	// (add) Token: 0x06001296 RID: 4758 RVA: 0x00045EE8 File Offset: 0x000440E8
	// (remove) Token: 0x06001297 RID: 4759 RVA: 0x00045F20 File Offset: 0x00044120
	public event Action<LevelSelectionNavigationController, IDifficultyBeatmap> didChangeDifficultyBeatmapEvent;

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06001298 RID: 4760 RVA: 0x0000E0AD File Offset: 0x0000C2AD
	public IDifficultyBeatmap selectedDifficultyBeatmap
	{
		get
		{
			return this._levelDetailViewController.selectedDifficultyBeatmap;
		}
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x0000E0BA File Offset: 0x0000C2BA
	public void SetData(IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection, bool showPackHeader, bool showPlayerStats, bool showPracticeButton, GameObject noDataInfoPrefab)
	{
		if (annotatedBeatmapLevelCollection is IBeatmapLevelPack)
		{
			this.SetData((IBeatmapLevelPack)annotatedBeatmapLevelCollection, showPackHeader, showPlayerStats, showPracticeButton);
			return;
		}
		this.SetData((annotatedBeatmapLevelCollection != null) ? annotatedBeatmapLevelCollection.beatmapLevelCollection : null, showPlayerStats, showPracticeButton, noDataInfoPrefab);
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x0000E0EC File Offset: 0x0000C2EC
	public void SelectLevel(IPreviewBeatmapLevel beatmapLevel)
	{
		this._levelCollectionViewController.SelectLevel(beatmapLevel);
		this._beatmapLevelToBeSelectedAfterPresent = beatmapLevel;
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x00045F58 File Offset: 0x00044158
	private void SetData(IBeatmapLevelPack levelPack, bool showPackHeader, bool showPlayerStats, bool showPracticeButton)
	{
		this._levelPack = levelPack;
		this._showPlayerStatsInDetailView = showPlayerStats;
		this._showPracticeButtonInDetailView = showPracticeButton;
		this._levelCollectionViewController.SetData(levelPack.beatmapLevelCollection, showPackHeader ? levelPack.packName : null, showPackHeader ? levelPack.coverImage : null, false, null);
		this._levelPackDetailViewController.SetData(levelPack);
		this.PresentViewControllersForPack();
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x0000E101 File Offset: 0x0000C301
	private void SetData(IBeatmapLevelCollection beatmapLevelCollection, bool showPlayerStats, bool showPracticeButton, GameObject noDataInfoPrefab)
	{
		this._levelPack = null;
		this._showPlayerStatsInDetailView = showPlayerStats;
		this._showPracticeButtonInDetailView = showPracticeButton;
		this._levelCollectionViewController.SetData(beatmapLevelCollection, null, null, true, noDataInfoPrefab);
		this.PresentViewControllersForLevelCollection();
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00045FB8 File Offset: 0x000441B8
	public void ShowLoading()
	{
		this._loadingControl.ShowLoading();
		if (!base.isInViewControllerHierarchy)
		{
			base.SetChildViewControllers(Array.Empty<ViewController>());
			return;
		}
		if (this._levelPackDetailViewController.isInViewControllerHierarchy || this._levelDetailViewController.isInViewControllerHierarchy)
		{
			base.PopViewController(null, true);
		}
		if (this._levelCollectionViewController.isInViewControllerHierarchy)
		{
			base.PopViewController(null, true);
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x0004601C File Offset: 0x0004421C
	private void PresentViewControllersForPack()
	{
		this.HideLoading();
		if (!base.isInViewControllerHierarchy)
		{
			base.SetChildViewControllers(new ViewController[]
			{
				this._levelCollectionViewController,
				this._levelPackDetailViewController
			});
			return;
		}
		if (!this._levelCollectionViewController.isInViewControllerHierarchy)
		{
			this.PresentDetailViewController(this._levelCollectionViewController, true);
		}
		if (!this._levelPackDetailViewController.isInViewControllerHierarchy)
		{
			this.PresentDetailViewController(this._levelPackDetailViewController, true);
		}
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x0004608C File Offset: 0x0004428C
	private void PresentViewControllersForLevelCollection()
	{
		this.HideLoading();
		if (!base.isInViewControllerHierarchy)
		{
			base.SetChildViewControllers(new ViewController[]
			{
				this._levelCollectionViewController
			});
			return;
		}
		if (!this._levelCollectionViewController.isInViewControllerHierarchy)
		{
			this.PresentDetailViewController(this._levelCollectionViewController, true);
		}
		this.HideDetailViewController();
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x0000E12F File Offset: 0x0000C32F
	private void HideLoading()
	{
		this._loadingControl.Hide();
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x0000E13C File Offset: 0x0000C33C
	private void HideDetailViewController()
	{
		if (this._levelPackDetailViewController.isInViewControllerHierarchy || this._levelDetailViewController.isInViewControllerHierarchy)
		{
			base.PopViewController(null, true);
		}
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x000460E0 File Offset: 0x000442E0
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._levelCollectionViewController.didSelectLevelEvent += this.HandleLevelCollectionViewControllerDidSelectLevel;
			this._levelCollectionViewController.didSelectHeaderEvent += this.HandleLevelCollectionViewControllerDidSelectPack;
			this._levelDetailViewController.didPressPlayButtonEvent += this.HandleLevelDetailViewControllerDidPressPlayButton;
			this._levelDetailViewController.didPressPracticeButtonEvent += this.HandleLevelDetailViewControllerDidPressPracticeButton;
			this._levelDetailViewController.didChangeDifficultyBeatmapEvent += this.HandleLevelDetailViewControllerDidChangeDifficultyBeatmap;
			this._levelDetailViewController.didPresentContentEvent += this.HandleLevelDetailViewControllerDidPresentContent;
			this._levelDetailViewController.didPressOpenLevelPackButtonEvent += this.HandleLevelDetailViewControllerDidPressOpenLevelPackButton;
			this._levelDetailViewController.levelFavoriteStatusDidChangeEvent += this.HandleLevelDetailViewControllerLevelFavoriteStatusDidChange;
			if (this._beatmapLevelToBeSelectedAfterPresent != null)
			{
				this._levelCollectionViewController.SelectLevel(this._beatmapLevelToBeSelectedAfterPresent);
				this._beatmapLevelToBeSelectedAfterPresent = null;
			}
		}
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x000461CC File Offset: 0x000443CC
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._levelCollectionViewController.didSelectLevelEvent -= this.HandleLevelCollectionViewControllerDidSelectLevel;
			this._levelCollectionViewController.didSelectHeaderEvent -= this.HandleLevelCollectionViewControllerDidSelectPack;
			this._levelDetailViewController.didPressPlayButtonEvent -= this.HandleLevelDetailViewControllerDidPressPlayButton;
			this._levelDetailViewController.didPressPracticeButtonEvent -= this.HandleLevelDetailViewControllerDidPressPracticeButton;
			this._levelDetailViewController.didChangeDifficultyBeatmapEvent -= this.HandleLevelDetailViewControllerDidChangeDifficultyBeatmap;
			this._levelDetailViewController.didPresentContentEvent -= this.HandleLevelDetailViewControllerDidPresentContent;
			this._levelDetailViewController.didPressOpenLevelPackButtonEvent -= this.HandleLevelDetailViewControllerDidPressOpenLevelPackButton;
			this._levelDetailViewController.levelFavoriteStatusDidChangeEvent -= this.HandleLevelDetailViewControllerLevelFavoriteStatusDidChange;
		}
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x00046298 File Offset: 0x00044498
	private void HandleLevelCollectionViewControllerDidSelectLevel(LevelCollectionViewController viewController, IPreviewBeatmapLevel level)
	{
		if (this._levelPack == null)
		{
			this._levelDetailViewController.SetData(level, this._showPlayerStatsInDetailView, !this._showPracticeButtonInDetailView, !this._appStaticSettings.enable360DegreeLevels);
		}
		else
		{
			this._levelDetailViewController.SetData(this._levelPack, level, this._showPlayerStatsInDetailView, !this._showPracticeButtonInDetailView, !this._appStaticSettings.enable360DegreeLevels, true);
		}
		this.PresentDetailViewController(this._levelDetailViewController, false);
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x0000E160 File Offset: 0x0000C360
	private void HandleLevelCollectionViewControllerDidSelectPack(LevelCollectionViewController viewController)
	{
		if (this._levelPack == null)
		{
			return;
		}
		this._levelPackDetailViewController.SetData(this._levelPack);
		this.PresentDetailViewController(this._levelPackDetailViewController, false);
		Action<LevelSelectionNavigationController, IBeatmapLevelPack> action = this.didSelectLevelPackEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._levelPack);
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00046318 File Offset: 0x00044518
	private void PresentDetailViewController(ViewController viewController, bool immediately)
	{
		if ((viewController == this._levelPackDetailViewController && this._levelDetailViewController.isInViewControllerHierarchy) || (viewController == this._levelDetailViewController && this._levelPackDetailViewController.isInViewControllerHierarchy))
		{
			base.PopViewController(delegate
			{
				this.PushViewController(viewController, null, true);
			}, true);
			return;
		}
		if (!viewController.isInViewControllerHierarchy)
		{
			base.PushViewController(viewController, null, immediately);
		}
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
	private void HandleLevelDetailViewControllerDidPressPlayButton(StandardLevelDetailViewController viewController)
	{
		Action<LevelSelectionNavigationController> action = this.didPressPlayButtonEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x0000E1B3 File Offset: 0x0000C3B3
	private void HandleLevelDetailViewControllerDidPressPracticeButton(StandardLevelDetailViewController viewController, IBeatmapLevel level)
	{
		Action<LevelSelectionNavigationController, IBeatmapLevel> action = this.didPressPracticeButtonEvent;
		if (action == null)
		{
			return;
		}
		action(this, level);
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x0000E1C7 File Offset: 0x0000C3C7
	private void HandleLevelDetailViewControllerDidChangeDifficultyBeatmap(StandardLevelDetailViewController viewController, IDifficultyBeatmap beatmap)
	{
		Action<LevelSelectionNavigationController, IDifficultyBeatmap> action = this.didChangeDifficultyBeatmapEvent;
		if (action == null)
		{
			return;
		}
		action(this, beatmap);
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x0000E1DB File Offset: 0x0000C3DB
	private void HandleLevelDetailViewControllerDidPresentContent(StandardLevelDetailViewController viewController, StandardLevelDetailViewController.ContentType contentType)
	{
		Action<LevelSelectionNavigationController, StandardLevelDetailViewController.ContentType> action = this.didPresentDetailContentEvent;
		if (action == null)
		{
			return;
		}
		action(this, contentType);
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x0000E1EF File Offset: 0x0000C3EF
	private void HandleLevelDetailViewControllerDidPressOpenLevelPackButton(StandardLevelDetailViewController viewController, IBeatmapLevelPack levelPack)
	{
		Action<LevelSelectionNavigationController, IBeatmapLevelPack> action = this.didPressOpenPackButtonEvent;
		if (action == null)
		{
			return;
		}
		action(this, levelPack);
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0000E203 File Offset: 0x0000C403
	private void HandleLevelDetailViewControllerLevelFavoriteStatusDidChange(StandardLevelDetailViewController viewController, bool favoriteStatus)
	{
		this._levelCollectionViewController.RefreshFavorites();
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x0000E210 File Offset: 0x0000C410
	public void RefreshDetail()
	{
		this._levelDetailViewController.RefreshContentLevelDetailView();
	}

	// Token: 0x04001255 RID: 4693
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x04001256 RID: 4694
	[Inject]
	private LevelCollectionViewController _levelCollectionViewController;

	// Token: 0x04001257 RID: 4695
	[Inject]
	private LevelPackDetailViewController _levelPackDetailViewController;

	// Token: 0x04001258 RID: 4696
	[Inject]
	private StandardLevelDetailViewController _levelDetailViewController;

	// Token: 0x04001259 RID: 4697
	[Inject]
	private AppStaticSettingsSO _appStaticSettings;

	// Token: 0x04001260 RID: 4704
	private bool _showPlayerStatsInDetailView;

	// Token: 0x04001261 RID: 4705
	private bool _showPracticeButtonInDetailView;

	// Token: 0x04001262 RID: 4706
	private IBeatmapLevelPack _levelPack;

	// Token: 0x04001263 RID: 4707
	private IPreviewBeatmapLevel _beatmapLevelToBeSelectedAfterPresent;
}
