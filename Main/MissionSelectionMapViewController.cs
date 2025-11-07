using System;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x020003F7 RID: 1015
public class MissionSelectionMapViewController : ViewController
{
	// Token: 0x140000AD RID: 173
	// (add) Token: 0x06001306 RID: 4870 RVA: 0x000471E8 File Offset: 0x000453E8
	// (remove) Token: 0x06001307 RID: 4871 RVA: 0x00047220 File Offset: 0x00045420
	public event Action<MissionSelectionMapViewController, MissionNode> didSelectMissionLevelEvent;

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06001308 RID: 4872 RVA: 0x0000E577 File Offset: 0x0000C777
	public bool animatedUpdateIsRequired
	{
		get
		{
			return this._missionMapAnimationController.animatedUpdateIsRequired;
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x00047258 File Offset: 0x00045458
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._missionNodeSelectionManager.didSelectMissionNodeEvent += this.HandleMissionNodeSelectionManagerDidSelectMissionNode;
			this._selectedMissionNode = null;
		}
		if (firstActivation)
		{
			this._missionMapAnimationController.ScrollToTopMostNotClearedMission();
		}
		if (this._selectedMissionNode != null)
		{
			this._mapScrollView.ScrollToWorldPositionIfOutsideArea(this._selectedMissionNode.transform.position, 0.75f, 0.25f, 0.75f, false);
		}
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0000E584 File Offset: 0x0000C784
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._missionNodeSelectionManager.didSelectMissionNodeEvent -= this.HandleMissionNodeSelectionManagerDidSelectMissionNode;
			this._missionNodeSelectionManager.DeselectSelectedNode();
			this._songPreviewPlayer.CrossfadeToDefault();
		}
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000472D0 File Offset: 0x000454D0
	private void HandleMissionNodeSelectionManagerDidSelectMissionNode(MissionNodeVisualController missionNodeVisualController)
	{
		this._selectedMissionNode = missionNodeVisualController.missionNode;
		BeatmapLevelSO level = this._selectedMissionNode.missionData.level;
		this._songPreviewPlayer.CrossfadeTo(level.previewAudioClip, level.previewStartTime, level.previewDuration, 1f);
		Action<MissionSelectionMapViewController, MissionNode> action = this.didSelectMissionLevelEvent;
		if (action == null)
		{
			return;
		}
		action(this, missionNodeVisualController.missionNode);
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x0000E5B6 File Offset: 0x0000C7B6
	public void ShowMissionClearedAnimation(Action finishCallback)
	{
		this._missionMapAnimationController.UpdateMissionMapAfterMissionWasCleared(true, finishCallback);
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x0000E5C5 File Offset: 0x0000C7C5
	public void DeselectSelectedNode()
	{
		this._missionNodeSelectionManager.DeselectSelectedNode();
	}

	// Token: 0x040012C5 RID: 4805
	[SerializeField]
	private ScrollView _mapScrollView;

	// Token: 0x040012C6 RID: 4806
	[SerializeField]
	private MissionNodeSelectionManager _missionNodeSelectionManager;

	// Token: 0x040012C7 RID: 4807
	[SerializeField]
	private MissionMapAnimationController _missionMapAnimationController;

	// Token: 0x040012C8 RID: 4808
	[Inject]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x040012CA RID: 4810
	private MissionNode _selectedMissionNode;
}
