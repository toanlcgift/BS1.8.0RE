using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

// Token: 0x02000373 RID: 883
[Serializable]
public class EnvironmentsTableViewDataSource : TableView.IDataSource
{
	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0000BF3B File Offset: 0x0000A13B
	public List<EnvironmentInfoSO> environmentInfos
	{
		get
		{
			return this._environmentInfos;
		}
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0003E784 File Offset: 0x0003C984
	public EnvironmentsTableViewDataSource(EnvironmentsTableViewDataSource environmentsTableViewDataSource)
	{
		this._environmentCellPrefab = environmentsTableViewDataSource._environmentCellPrefab;
		this._environmentCellsReuseIdentifier = environmentsTableViewDataSource._environmentCellsReuseIdentifier;
		this._cellHeight = environmentsTableViewDataSource._cellHeight;
		this._environmentInfos = environmentsTableViewDataSource._environmentInfos;
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x0000BF43 File Offset: 0x0000A143
	public void SetData(List<EnvironmentInfoSO> environmentInfos)
	{
		this._environmentInfos = environmentInfos;
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0000BF4C File Offset: 0x0000A14C
	public float CellSize()
	{
		return this._cellHeight;
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0000BF54 File Offset: 0x0000A154
	public int NumberOfCells()
	{
		if (this._environmentInfos == null)
		{
			return 0;
		}
		return this._environmentInfos.Count;
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x0003E7E0 File Offset: 0x0003C9E0
	public TableCell CellForIdx(TableView tableView, int row)
	{
		EnvironmentTableCell environmentTableCell = tableView.DequeueReusableCellForIdentifier(this._environmentCellsReuseIdentifier) as EnvironmentTableCell;
		if (environmentTableCell == null)
		{
			environmentTableCell = UnityEngine.Object.Instantiate<EnvironmentTableCell>(this._environmentCellPrefab);
			environmentTableCell.reuseIdentifier = this._environmentCellsReuseIdentifier;
		}
		environmentTableCell.text = this._environmentInfos[row].environmentName;
		return environmentTableCell;
	}

	// Token: 0x04000FFF RID: 4095
	[SerializeField]
	private EnvironmentTableCell _environmentCellPrefab;

	// Token: 0x04001000 RID: 4096
	[SerializeField]
	private string _environmentCellsReuseIdentifier = "Cell";

	// Token: 0x04001001 RID: 4097
	[SerializeField]
	private float _cellHeight = 7f;

	// Token: 0x04001002 RID: 4098
	private List<EnvironmentInfoSO> _environmentInfos;
}
