using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class MissionNodeSelectionManager : MonoBehaviour
{
	// Token: 0x1400008C RID: 140
	// (add) Token: 0x060010F0 RID: 4336 RVA: 0x000418D4 File Offset: 0x0003FAD4
	// (remove) Token: 0x060010F1 RID: 4337 RVA: 0x0004190C File Offset: 0x0003FB0C
	public event Action<MissionNodeVisualController> didSelectMissionNodeEvent;

	// Token: 0x060010F2 RID: 4338 RVA: 0x0000CD4B File Offset: 0x0000AF4B
	public void DeselectSelectedNode()
	{
		if (this._selectedNode != null)
		{
			this._selectedNode.SetSelected(false);
			this._selectedNode = null;
		}
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x00041944 File Offset: 0x0003FB44
	protected void Start()
	{
		this._missionNodes = this._missionNodesManager.allMissionNodes;
		foreach (MissionNode missionNode in this._missionNodes)
		{
			missionNode.missionNodeVisualController.nodeWasSelectEvent += this.HandleNodeWasSelect;
			missionNode.missionNodeVisualController.nodeWasDisplayedEvent += this.HandleNodeWasDisplayed;
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000419A8 File Offset: 0x0003FBA8
	protected void OnDestroy()
	{
		foreach (MissionNode missionNode in this._missionNodes)
		{
			if (missionNode != null)
			{
				missionNode.missionNodeVisualController.nodeWasSelectEvent -= this.HandleNodeWasSelect;
				missionNode.missionNodeVisualController.nodeWasDisplayedEvent -= this.HandleNodeWasDisplayed;
			}
		}
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x0000CD6E File Offset: 0x0000AF6E
	private void HandleNodeWasSelect(MissionNodeVisualController missionNode)
	{
		if (this._selectedNode != null)
		{
			this._selectedNode.SetSelected(false);
		}
		this._selectedNode = missionNode;
		Action<MissionNodeVisualController> action = this.didSelectMissionNodeEvent;
		if (action == null)
		{
			return;
		}
		action(missionNode);
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
	private void HandleNodeWasDisplayed(MissionNodeVisualController missionNode)
	{
		missionNode.SetSelected(missionNode == this._selectedNode);
	}

	// Token: 0x040010FE RID: 4350
	[SerializeField]
	private MissionNodesManager _missionNodesManager;

	// Token: 0x04001100 RID: 4352
	private MissionNode[] _missionNodes;

	// Token: 0x04001101 RID: 4353
	private MissionNodeVisualController _selectedNode;
}
