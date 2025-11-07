using System;
using System.Collections;
using System.Linq;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x0200039A RID: 922
public class MissionMapAnimationController : MonoBehaviour
{
	// Token: 0x17000376 RID: 886
	// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0000CB54 File Offset: 0x0000AD54
	public bool animatedUpdateIsRequired
	{
		get
		{
			return this._missionNodesManager.GetMissionNodeWithModelClearedStateInconsistency() != null;
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00041304 File Offset: 0x0003F504
	public void ScrollToTopMostNotClearedMission()
	{
		MissionNode topMostNotClearedMissionNode = this._missionNodesManager.GetTopMostNotClearedMissionNode();
		if (topMostNotClearedMissionNode != null)
		{
			this._mapScrollView.ScrollToWorldPosition(topMostNotClearedMissionNode.transform.position, 0.5f, false);
			return;
		}
		this._mapScrollView.ScrollDown(false);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x00041350 File Offset: 0x0003F550
	public void UpdateMissionMapAfterMissionWasCleared(bool animated, Action finishCallback)
	{
		if (!animated)
		{
			this._missionNodesManager.SetupNodeMap();
			if (finishCallback != null)
			{
				finishCallback();
			}
			return;
		}
		MissionNode missionNodeWithModelClearedStateInconsistency = this._missionNodesManager.GetMissionNodeWithModelClearedStateInconsistency();
		if (missionNodeWithModelClearedStateInconsistency != null)
		{
			base.StartCoroutine(this.UpdateMissionMapCoroutine(missionNodeWithModelClearedStateInconsistency, finishCallback));
			return;
		}
		this._missionNodesManager.SetupNodeMap();
		if (finishCallback != null)
		{
			finishCallback();
		}
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0000CB67 File Offset: 0x0000AD67
	private IEnumerator UpdateMissionMapCoroutine(MissionNode lastClearedMissionNode, Action finishCallback)
	{
		this._mapScrollView.ScrollToWorldPosition(lastClearedMissionNode.transform.position, 0.7f, true);
		yield return new WaitForSeconds(this._startDelay);
		yield return this.UpdateClearedNodeStateCoroutine(lastClearedMissionNode);
		yield return new WaitForSeconds(this._stageAnimationStartDelay);
		yield return this.UpdateStageCoroutine();
		yield return new WaitForSeconds(this._missionConnectionAnimationStartDelay);
		yield return this.UpdateNodesAndConnectionCoroutine();
		if (finishCallback != null)
		{
			finishCallback();
		}
		yield break;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0000CB84 File Offset: 0x0000AD84
	private IEnumerator UpdateClearedNodeStateCoroutine(MissionNode lastClearedMissionNode)
	{
		if (lastClearedMissionNode != null)
		{
			lastClearedMissionNode.missionNodeVisualController.SetMissionCleared();
			this._shockwaveEffect.SpawnShockwave(lastClearedMissionNode.transform.position);
		}
		yield return null;
		yield break;
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0000CB9A File Offset: 0x0000AD9A
	private IEnumerator UpdateStageCoroutine()
	{
		this._missionNodesManager.UpdateStageLockText();
		if (this._missionNodesManager.DidFirstLockedMissionStageChange())
		{
			this._missionNodesManager.missionStagesManager.UpdateStageLockPositionAnimated(true, this._stageAnimationDuration);
			this._missionNodesManager.UpdateStageLockText();
		}
		yield return null;
		yield break;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0000CBA9 File Offset: 0x0000ADA9
	private IEnumerator UpdateNodesAndConnectionCoroutine()
	{
		MissionNodeConnection[] newEnabledConnection = this._missionNodesManager.GetNewEnabledConnection();
		if (newEnabledConnection.Count<MissionNodeConnection>() == 0)
		{
			yield return null;
		}
		newEnabledConnection = (from connection in newEnabledConnection
		orderby connection.childMissionNode.missionNode.position.x, connection.parentMissionNode.missionNode.position.x
		select connection).ToArray<MissionNodeConnection>();
		foreach (MissionNodeConnection missionNodeConnection in newEnabledConnection)
		{
			missionNodeConnection.SetActive(true);
			missionNodeConnection.childMissionNode.SetInteractable();
			yield return new WaitForSeconds(this._missionConnectionAnimationSeparationTime);
		}
		MissionNodeConnection[] array = null;
		yield break;
	}

	// Token: 0x040010CD RID: 4301
	[SerializeField]
	private MissionNodesManager _missionNodesManager;

	// Token: 0x040010CE RID: 4302
	[SerializeField]
	private ScrollView _mapScrollView;

	// Token: 0x040010CF RID: 4303
	[Space]
	[SerializeField]
	private float _startDelay;

	// Token: 0x040010D0 RID: 4304
	[SerializeField]
	private float _stageAnimationStartDelay = 0.3f;

	// Token: 0x040010D1 RID: 4305
	[SerializeField]
	private float _missionConnectionAnimationStartDelay = 0.1f;

	// Token: 0x040010D2 RID: 4306
	[SerializeField]
	private float _missionConnectionAnimationSeparationTime = 0.1f;

	// Token: 0x040010D3 RID: 4307
	[SerializeField]
	private float _stageAnimationDuration = 1f;

	// Token: 0x040010D4 RID: 4308
	[Inject]
	private MenuShockwave _shockwaveEffect;
}
