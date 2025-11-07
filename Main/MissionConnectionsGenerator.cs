using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000399 RID: 921
[ExecuteInEditMode]
public class MissionConnectionsGenerator : MonoBehaviour
{
	// Token: 0x17000375 RID: 885
	// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0000CAF7 File Offset: 0x0000ACF7
	private MissionNode _rootMissionNode
	{
		get
		{
			return this._missionNodesManager.rootMissionNode;
		}
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0000CB04 File Offset: 0x0000AD04
	private void CreateNodeConnections()
	{
		this.RemoveOldConnections();
		this._missionNodes = new List<MissionNode>();
		this.CreateConnections(this._rootMissionNode, this._missionNodes);
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x000411EC File Offset: 0x0003F3EC
	private void RemoveOldConnections()
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in this._connectionsCanvas.transform)
		{
			Transform item = (Transform)obj;
			list.Add(item);
		}
		foreach (Transform transform in list)
		{
			UnityEngine.Object.DestroyImmediate(transform.gameObject);
		}
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x00041290 File Offset: 0x0003F490
	private void CreateConnections(MissionNode missionNode, List<MissionNode> visitedNodes)
	{
		if (visitedNodes.Contains(missionNode))
		{
			return;
		}
		visitedNodes.Add(missionNode);
		if (missionNode.childNodes != null)
		{
			foreach (MissionNode missionNode2 in missionNode.childNodes)
			{
				this.CreateConnectionBetweenNodes(missionNode, missionNode2).gameObject.name = "NodeConnection_" + missionNode.missionId + "-" + missionNode2.missionId;
				this.CreateConnections(missionNode2, visitedNodes);
			}
		}
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0000CB29 File Offset: 0x0000AD29
	private MissionNodeConnection CreateConnectionBetweenNodes(MissionNode parentMissionNode, MissionNode childMissionNode)
	{
		MissionNodeConnection missionNodeConnection = UnityEngine.Object.Instantiate<MissionNodeConnection>(this._nodeConnectionPref, this._connectionsCanvas.transform, false);
		missionNodeConnection.Setup(parentMissionNode.missionNodeVisualController, childMissionNode.missionNodeVisualController);
		return missionNodeConnection;
	}

	// Token: 0x040010C9 RID: 4297
	[SerializeField]
	private MissionNodesManager _missionNodesManager;

	// Token: 0x040010CA RID: 4298
	[SerializeField]
	private MissionNodeConnection _nodeConnectionPref;

	// Token: 0x040010CB RID: 4299
	[SerializeField]
	private GameObject _connectionsCanvas;

	// Token: 0x040010CC RID: 4300
	private List<MissionNode> _missionNodes;
}
