using System;
using UnityEngine;

// Token: 0x020003A0 RID: 928
[SelectionBase]
public class MissionNode : MonoBehaviour
{
	// Token: 0x1700037F RID: 895
	// (get) Token: 0x060010DC RID: 4316 RVA: 0x0000CC82 File Offset: 0x0000AE82
	public MissionDataSO missionData
	{
		get
		{
			return this._missionDataSO;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x060010DD RID: 4317 RVA: 0x0000CC8A File Offset: 0x0000AE8A
	public MissionNode[] childNodes
	{
		get
		{
			return this._childNodes;
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x060010DE RID: 4318 RVA: 0x0000CC92 File Offset: 0x0000AE92
	public MissionNodeVisualController missionNodeVisualController
	{
		get
		{
			return this._missionNodeVisualController;
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x060010DF RID: 4319 RVA: 0x0000CC9A File Offset: 0x0000AE9A
	public string letterPartName
	{
		get
		{
			return this._letterPartName;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0000CCA2 File Offset: 0x0000AEA2
	public int numberPartName
	{
		get
		{
			return this._numberPartName;
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0000CCAA File Offset: 0x0000AEAA
	public string missionId
	{
		get
		{
			return this._numberPartName.ToString() + this._letterPartName;
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0000CCC2 File Offset: 0x0000AEC2
	public string formattedMissionNodeName
	{
		get
		{
			return string.Format("{0}<size=66%>{1}</size>", this.numberPartName, this.letterPartName);
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0000CCDF File Offset: 0x0000AEDF
	public Vector2 position
	{
		get
		{
			return this._rectTransform.localPosition;
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00041724 File Offset: 0x0003F924
	public float radius
	{
		get
		{
			return this._rectTransform.rect.width;
		}
	}

	// Token: 0x040010EA RID: 4330
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private MissionDataSO _missionDataSO;

	// Token: 0x040010EB RID: 4331
	[SerializeField]
	private string _letterPartName;

	// Token: 0x040010EC RID: 4332
	[SerializeField]
	private int _numberPartName;

	// Token: 0x040010ED RID: 4333
	[SerializeField]
	private RectTransform _rectTransform;

	// Token: 0x040010EE RID: 4334
	[SerializeField]
	private MissionNodeVisualController _missionNodeVisualController;

	// Token: 0x040010EF RID: 4335
	[Space]
	[SerializeField]
	private MissionNode[] _childNodes;
}
