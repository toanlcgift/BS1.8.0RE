using System;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public abstract class MissionObjectiveChecker : MonoBehaviour
{
	// Token: 0x14000050 RID: 80
	// (add) Token: 0x06000C45 RID: 3141 RVA: 0x00036110 File Offset: 0x00034310
	// (remove) Token: 0x06000C46 RID: 3142 RVA: 0x00036148 File Offset: 0x00034348
	public event Action<MissionObjectiveChecker> statusDidChangeEvent;

	// Token: 0x14000051 RID: 81
	// (add) Token: 0x06000C47 RID: 3143 RVA: 0x00036180 File Offset: 0x00034380
	// (remove) Token: 0x06000C48 RID: 3144 RVA: 0x000361B8 File Offset: 0x000343B8
	public event Action<MissionObjectiveChecker> checkedValueDidChangeEvent;

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000099B4 File Offset: 0x00007BB4
	public MissionObjectiveTypeSO missionObjectiveType
	{
		get
		{
			return this._missionObjectiveType;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000099BC File Offset: 0x00007BBC
	public MissionObjective missionObjective
	{
		get
		{
			return this._missionObjective;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000099C4 File Offset: 0x00007BC4
	// (set) Token: 0x06000C4C RID: 3148 RVA: 0x000099CC File Offset: 0x00007BCC
	public bool disableChecking
	{
		get
		{
			return this._disableChecking;
		}
		set
		{
			this._disableChecking = value;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000099D5 File Offset: 0x00007BD5
	// (set) Token: 0x06000C4E RID: 3150 RVA: 0x000099DD File Offset: 0x00007BDD
	public MissionObjectiveChecker.Status status
	{
		get
		{
			return this._status;
		}
		protected set
		{
			if (this._disableChecking)
			{
				return;
			}
			if (value != this._status)
			{
				this._status = value;
				Action<MissionObjectiveChecker> action = this.statusDidChangeEvent;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00009A09 File Offset: 0x00007C09
	// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00009A11 File Offset: 0x00007C11
	public int checkedValue
	{
		get
		{
			return this._checkedValue;
		}
		protected set
		{
			if (this._disableChecking)
			{
				return;
			}
			if (value != this._checkedValue)
			{
				this._checkedValue = value;
				Action<MissionObjectiveChecker> action = this.checkedValueDidChangeEvent;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00009A3D File Offset: 0x00007C3D
	public void SetCheckedMissionObjective(MissionObjective missionObjective)
	{
		this._missionObjective = missionObjective;
		this.Init();
	}

	// Token: 0x06000C52 RID: 3154
	protected abstract void Init();

	// Token: 0x04000CD6 RID: 3286
	[SerializeField]
	private MissionObjectiveTypeSO _missionObjectiveType;

	// Token: 0x04000CD9 RID: 3289
	private MissionObjectiveChecker.Status _status;

	// Token: 0x04000CDA RID: 3290
	private int _checkedValue;

	// Token: 0x04000CDB RID: 3291
	protected MissionObjective _missionObjective;

	// Token: 0x04000CDC RID: 3292
	private bool _disableChecking;

	// Token: 0x020002D9 RID: 729
	public enum Status
	{
		// Token: 0x04000CDE RID: 3294
		None,
		// Token: 0x04000CDF RID: 3295
		NotClearedYet,
		// Token: 0x04000CE0 RID: 3296
		NotFailedYet,
		// Token: 0x04000CE1 RID: 3297
		Cleared,
		// Token: 0x04000CE2 RID: 3298
		Failed
	}
}
