using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

// Token: 0x020003A8 RID: 936
public class MissionNodesManager : MonoBehaviour
{
	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06001117 RID: 4375 RVA: 0x0000CF37 File Offset: 0x0000B137
	public MissionNode rootMissionNode
	{
		get
		{
			return this._rootMissionNode;
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06001118 RID: 4376 RVA: 0x0000CF3F File Offset: 0x0000B13F
	public MissionNode finalMissionNode
	{
		get
		{
			return this._finalMissionNode;
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06001119 RID: 4377 RVA: 0x0000CF47 File Offset: 0x0000B147
	public MissionStagesManager missionStagesManager
	{
		get
		{
			return this._missionStagesManager;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x0600111A RID: 4378 RVA: 0x0000CF4F File Offset: 0x0000B14F
	public CampaignProgressModel missionProgressModel
	{
		get
		{
			return this._missionProgressModel;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x0600111B RID: 4379 RVA: 0x0000CF57 File Offset: 0x0000B157
	public MissionNode[] allMissionNodes
	{
		get
		{
			return this._allMissionNodes;
		}
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x0600111C RID: 4380 RVA: 0x0000CF5F File Offset: 0x0000B15F
	public bool IsInitialized
	{
		get
		{
			return this._isInitialized;
		}
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0000CF67 File Offset: 0x0000B167
	protected void Awake()
	{
		this.GetAllMissionNodes();
		this.RegisterAllNodes();
		this.SetupNodeMap();
		this._isInitialized = true;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00041C00 File Offset: 0x0003FE00
	public void SetupNodeMap()
	{
		this.ResetAllNodes();
		this.SetupStages();
		MissionNodeVisualController missionNodeVisualController = this.rootMissionNode.missionNodeVisualController;
		this.SetupNodeTree(missionNodeVisualController, true);
		this.SetupNodeConnections();
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0000CF82 File Offset: 0x0000B182
	public bool MissionWasCleared(MissionNode missionNode)
	{
		return !missionNode.missionNodeVisualController.cleared && this.missionProgressModel.IsMissionCleared(missionNode.missionId);
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x00041C34 File Offset: 0x0003FE34
	public MissionNode GetMissionNodeWithModelClearedStateInconsistency()
	{
		foreach (MissionNode missionNode in this._allMissionNodes)
		{
			if (!missionNode.missionNodeVisualController.cleared && this.missionProgressModel.IsMissionCleared(missionNode.missionId))
			{
				return missionNode;
			}
		}
		return null;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x00041C80 File Offset: 0x0003FE80
	public bool DidFirstLockedMissionStageChange()
	{
		UnityEngine.Object firstLockedMissionStage = this.missionStagesManager.firstLockedMissionStage;
		int numberOfClearedMissions = this.missionProgressModel.numberOfClearedMissions;
		this.missionStagesManager.UpdateFirtsLockedMissionStage(numberOfClearedMissions);
		return firstLockedMissionStage != this.missionStagesManager.firstLockedMissionStage;
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x00041CC0 File Offset: 0x0003FEC0
	public void UpdateStageLockText()
	{
		int numberOfClearedMissions = this.missionProgressModel.numberOfClearedMissions;
		this.missionStagesManager.UpdateStageLockText(numberOfClearedMissions);
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x00041CE8 File Offset: 0x0003FEE8
	public MissionNode GetTopMostNotClearedMissionNode()
	{
		MissionNode missionNode = null;
		foreach (MissionNode missionNode2 in this._allMissionNodes)
		{
			MissionNodeVisualController missionNodeVisualController = missionNode2.missionNodeVisualController;
			if (missionNodeVisualController.isInitialized && !missionNode2.missionNodeVisualController.cleared && missionNodeVisualController.interactable && (missionNode == null || missionNode.transform.transform.position.y < missionNodeVisualController.transform.transform.position.y))
			{
				missionNode = missionNode2;
			}
		}
		return missionNode;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
	private void GetAllMissionNodes()
	{
		this._allMissionNodes = this.GetAllMissionNodes(this.rootMissionNode, new HashSet<MissionNode>()).ToArray<MissionNode>();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x00041D70 File Offset: 0x0003FF70
	private HashSet<MissionNode> GetAllMissionNodes(MissionNode node, HashSet<MissionNode> visited)
	{
		if (visited.Contains(node))
		{
			return visited;
		}
		visited.Add(node);
		foreach (MissionNode node2 in node.childNodes)
		{
			this.GetAllMissionNodes(node2, visited);
		}
		return visited;
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00041DB4 File Offset: 0x0003FFB4
	public MissionNodeConnection[] GetNewEnabledConnection()
	{
		List<MissionNodeConnection> list = new List<MissionNodeConnection>();
		foreach (MissionNodeConnection missionNodeConnection in this._allMissionNodeConnections)
		{
			bool cleared = missionNodeConnection.parentMissionNode.cleared;
			bool flag = this.IsNodeInteractable(missionNodeConnection.childMissionNode, cleared);
			bool flag2 = cleared && flag;
			if (!missionNodeConnection.isActive && flag2)
			{
				list.Add(missionNodeConnection);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x00041E20 File Offset: 0x00040020
	private void ResetAllNodes()
	{
		MissionNode[] allMissionNodes = this._allMissionNodes;
		for (int i = 0; i < allMissionNodes.Length; i++)
		{
			allMissionNodes[i].missionNodeVisualController.Reset();
		}
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x00041E50 File Offset: 0x00040050
	private void SetupStages()
	{
		int numberOfClearedMissions = this.missionProgressModel.numberOfClearedMissions;
		this.missionStagesManager.UpdateFirtsLockedMissionStage(numberOfClearedMissions);
		this.missionStagesManager.UpdateStageLockPosition();
		this.missionStagesManager.UpdateStageLockText(numberOfClearedMissions);
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x00041E8C File Offset: 0x0004008C
	private void RegisterAllNodes()
	{
		foreach (MissionNode missionNode in this._allMissionNodes)
		{
			this.missionProgressModel.RegisterMissionId(missionNode.missionId);
		}
		this.missionProgressModel.SetFinalMissionId(this._finalMissionNode.missionId);
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x00041EDC File Offset: 0x000400DC
	private void SetupNodeTree(MissionNodeVisualController node, bool parentCleared)
	{
		if (node.isInitialized)
		{
			if (this.IsNodeInteractable(node, parentCleared))
			{
				node.SetInteractable();
			}
			return;
		}
		bool interactable = this.IsNodeInteractable(node, parentCleared);
		bool flag = this.missionProgressModel.IsMissionCleared(node.missionNode.missionId);
		node.Setup(flag, interactable);
		MissionNode[] childNodes = node.missionNode.childNodes;
		for (int i = 0; i < childNodes.Length; i++)
		{
			MissionNodeVisualController missionNodeVisualController = childNodes[i].missionNodeVisualController;
			this.SetupNodeTree(missionNodeVisualController, flag);
		}
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x00041F58 File Offset: 0x00040158
	private bool IsNodeInteractable(MissionNodeVisualController node, bool parentCleared)
	{
		if (!parentCleared)
		{
			return false;
		}
		MissionStage firstLockedMissionStage = this.missionStagesManager.firstLockedMissionStage;
		return node.missionNode.position.y < firstLockedMissionStage.position.y;
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x00041F94 File Offset: 0x00040194
	private void SetupNodeConnections()
	{
		this._allMissionNodeConnections = this._connectionsParentObject.GetComponentsInChildren<MissionNodeConnection>();
		foreach (MissionNodeConnection missionNodeConnection in this._allMissionNodeConnections)
		{
			if (missionNodeConnection.parentMissionNode.cleared && missionNodeConnection.childMissionNode.interactable)
			{
				missionNodeConnection.SetActive(false);
			}
		}
	}

	// Token: 0x0400110B RID: 4363
	[SerializeField]
	private MissionNode _rootMissionNode;

	// Token: 0x0400110C RID: 4364
	[SerializeField]
	private MissionNode _finalMissionNode;

	// Token: 0x0400110D RID: 4365
	[SerializeField]
	private MissionStagesManager _missionStagesManager;

	// Token: 0x0400110E RID: 4366
	[SerializeField]
	private GameObject _connectionsParentObject;

	// Token: 0x0400110F RID: 4367
	[SerializeField]
	private GameObject _missionNodesParentObject;

	// Token: 0x04001110 RID: 4368
	[Inject]
	private CampaignProgressModel _missionProgressModel;

	// Token: 0x04001111 RID: 4369
	private MissionNodeConnection[] _allMissionNodeConnections;

	// Token: 0x04001112 RID: 4370
	private MissionNode[] _allMissionNodes;

	// Token: 0x04001113 RID: 4371
	private bool _isInitialized;
}
