using System;
using HMUI;
using Zenject;

// Token: 0x020003F8 RID: 1016
public class MissionSelectionNavigationController : NavigationController
{
	// Token: 0x140000AE RID: 174
	// (add) Token: 0x0600130F RID: 4879 RVA: 0x00047334 File Offset: 0x00045534
	// (remove) Token: 0x06001310 RID: 4880 RVA: 0x0004736C File Offset: 0x0004556C
	public event Action<MissionSelectionNavigationController> didPressPlayButtonEvent;

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x06001311 RID: 4881 RVA: 0x0000E5D2 File Offset: 0x0000C7D2
	public MissionNode selectedMissionNode
	{
		get
		{
			return this._missionLevelDetailViewController.missionNode;
		}
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x000473A4 File Offset: 0x000455A4
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			base.SetChildViewControllers(new ViewController[]
			{
				this._missionSelectionMapViewController
			});
			this._missionSelectionMapViewController.didSelectMissionLevelEvent += this.HandleMissionSelectionMapViewControllerDidSelectMissionLevel;
			this._missionLevelDetailViewController.didPressPlayButtonEvent += this.HandleMissionLevelDetailViewControllerDidPressPlayButton;
		}
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x0000E5DF File Offset: 0x0000C7DF
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._missionSelectionMapViewController.didSelectMissionLevelEvent -= this.HandleMissionSelectionMapViewControllerDidSelectMissionLevel;
			this._missionLevelDetailViewController.didPressPlayButtonEvent -= this.HandleMissionLevelDetailViewControllerDidPressPlayButton;
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x0000E612 File Offset: 0x0000C812
	private void HandleMissionSelectionMapViewControllerDidSelectMissionLevel(MissionSelectionMapViewController viewController, MissionNode _missionNode)
	{
		this._missionLevelDetailViewController.Setup(_missionNode);
		if (!this._missionLevelDetailViewController.isInViewControllerHierarchy)
		{
			base.PushViewController(this._missionLevelDetailViewController, null, false);
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0000E63B File Offset: 0x0000C83B
	private void HandleMissionLevelDetailViewControllerDidPressPlayButton(MissionLevelDetailViewController viewController)
	{
		Action<MissionSelectionNavigationController> action = this.didPressPlayButtonEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000473F8 File Offset: 0x000455F8
	public void PresentMissionClearedIfNeeded(Action<bool> finishedCallback)
	{
		if (this._missionSelectionMapViewController.animatedUpdateIsRequired)
		{
			Action action = null;
			base.PopViewController(delegate
			{
				this._missionSelectionMapViewController.DeselectSelectedNode();
				MissionSelectionMapViewController missionSelectionMapViewController = this._missionSelectionMapViewController;
				Action finishCallback;
				if ((finishCallback = action) == null)
				{
					finishCallback = (action = delegate()
					{
						Action<bool> finishedCallback3 = finishedCallback;
						if (finishedCallback3 == null)
						{
							return;
						}
						finishedCallback3(true);
					});
				}
				missionSelectionMapViewController.ShowMissionClearedAnimation(finishCallback);
			}, false);
			return;
		}
		Action<bool> finishedCallback2 = finishedCallback;
		if (finishedCallback2 == null)
		{
			return;
		}
		finishedCallback2(false);
	}

	// Token: 0x040012CB RID: 4811
	[Inject]
	private MissionSelectionMapViewController _missionSelectionMapViewController;

	// Token: 0x040012CC RID: 4812
	[Inject]
	private MissionLevelDetailViewController _missionLevelDetailViewController;
}
