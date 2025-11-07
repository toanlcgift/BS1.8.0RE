using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x020002DA RID: 730
public class MissionObjectiveCheckersManager : MonoBehaviour
{
	// Token: 0x14000052 RID: 82
	// (add) Token: 0x06000C54 RID: 3156 RVA: 0x000361F0 File Offset: 0x000343F0
	// (remove) Token: 0x06000C55 RID: 3157 RVA: 0x00036228 File Offset: 0x00034428
	public event Action objectiveDidFailEvent;

	// Token: 0x14000053 RID: 83
	// (add) Token: 0x06000C56 RID: 3158 RVA: 0x00036260 File Offset: 0x00034460
	// (remove) Token: 0x06000C57 RID: 3159 RVA: 0x00036298 File Offset: 0x00034498
	public event Action objectiveWasClearedEvent;

	// Token: 0x14000054 RID: 84
	// (add) Token: 0x06000C58 RID: 3160 RVA: 0x000362D0 File Offset: 0x000344D0
	// (remove) Token: 0x06000C59 RID: 3161 RVA: 0x00036308 File Offset: 0x00034508
	public event Action objectivesListDidChangeEvent;

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00009A4C File Offset: 0x00007C4C
	public MissionObjectiveChecker[] activeMissionObjectiveCheckers
	{
		get
		{
			return this._activeMissionObjectiveCheckers;
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00036340 File Offset: 0x00034540
	protected void Start()
	{
		this._gameplayManager.levelFailedEvent += this.HandleLevelFailed;
		this._gameplayManager.levelFinishedEvent += this.HandleLevelFinished;
		MissionObjective[] missionObjectives = this._initData.missionObjectives;
		List<MissionObjectiveChecker> list = new List<MissionObjectiveChecker>(this._missionObjectiveCheckers.Length);
		List<MissionObjectiveChecker> list2 = new List<MissionObjectiveChecker>(this._missionObjectiveCheckers);
		foreach (MissionObjective missionObjective in missionObjectives)
		{
			bool flag = false;
			foreach (MissionObjectiveChecker missionObjectiveChecker in list2)
			{
				if (missionObjectiveChecker.missionObjectiveType == missionObjective.type)
				{
					list.Add(missionObjectiveChecker);
					missionObjectiveChecker.SetCheckedMissionObjective(missionObjective);
					list2.Remove(missionObjectiveChecker);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Debug.LogError("No missionObjectiveCheckers for missionOjective");
			}
		}
		this._activeMissionObjectiveCheckers = list.ToArray();
		MissionObjectiveChecker[] activeMissionObjectiveCheckers = this._activeMissionObjectiveCheckers;
		for (int i = 0; i < activeMissionObjectiveCheckers.Length; i++)
		{
			activeMissionObjectiveCheckers[i].statusDidChangeEvent += this.HandleMissionObjectiveCheckerStatusDidChange;
		}
		Action action = this.objectivesListDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00009A54 File Offset: 0x00007C54
	protected void OnDestroy()
	{
		if (this._gameplayManager != null)
		{
			this._gameplayManager.levelFailedEvent -= this.HandleLevelFailed;
			this._gameplayManager.levelFinishedEvent -= this.HandleLevelFinished;
		}
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00009A8C File Offset: 0x00007C8C
	private void HandleMissionObjectiveCheckerStatusDidChange(MissionObjectiveChecker missionObjectiveChecker)
	{
		if (missionObjectiveChecker.status != MissionObjectiveChecker.Status.Failed)
		{
			if (missionObjectiveChecker.status == MissionObjectiveChecker.Status.Cleared)
			{
				Action action = this.objectiveWasClearedEvent;
				if (action == null)
				{
					return;
				}
				action();
			}
			return;
		}
		Action action2 = this.objectiveDidFailEvent;
		if (action2 == null)
		{
			return;
		}
		action2();
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00036480 File Offset: 0x00034680
	public MissionObjectiveChecker GetMissionObjectiveChecker(MissionObjectiveTypeSO missionObjectiveType)
	{
		foreach (MissionObjectiveChecker missionObjectiveChecker in this._missionObjectiveCheckers)
		{
			if (missionObjectiveChecker.missionObjectiveType == missionObjectiveType)
			{
				return missionObjectiveChecker;
			}
		}
		return null;
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000364B8 File Offset: 0x000346B8
	public MissionObjectiveResult[] GetResults()
	{
		MissionObjectiveResult[] array = new MissionObjectiveResult[this._activeMissionObjectiveCheckers.Length];
		for (int i = 0; i < this._activeMissionObjectiveCheckers.Length; i++)
		{
			MissionObjectiveChecker missionObjectiveChecker = this._activeMissionObjectiveCheckers[i];
			missionObjectiveChecker.disableChecking = true;
			missionObjectiveChecker.statusDidChangeEvent -= this.HandleMissionObjectiveCheckerStatusDidChange;
			bool cleared = missionObjectiveChecker.status == MissionObjectiveChecker.Status.Cleared || missionObjectiveChecker.status == MissionObjectiveChecker.Status.NotFailedYet;
			int checkedValue = missionObjectiveChecker.checkedValue;
			MissionObjectiveResult missionObjectiveResult = new MissionObjectiveResult(missionObjectiveChecker.missionObjective, cleared, checkedValue);
			array[i] = missionObjectiveResult;
		}
		return array;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x00009AC1 File Offset: 0x00007CC1
	private void HandleLevelFailed()
	{
		this.StopChecking();
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00009AC1 File Offset: 0x00007CC1
	private void HandleLevelFinished()
	{
		this.StopChecking();
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0003653C File Offset: 0x0003473C
	private void StopChecking()
	{
		for (int i = 0; i < this._activeMissionObjectiveCheckers.Length; i++)
		{
			MissionObjectiveChecker missionObjectiveChecker = this._activeMissionObjectiveCheckers[i];
			missionObjectiveChecker.disableChecking = true;
			missionObjectiveChecker.statusDidChangeEvent -= this.HandleMissionObjectiveCheckerStatusDidChange;
		}
	}

	// Token: 0x04000CE3 RID: 3299
	[SerializeField]
	private MissionObjectiveChecker[] _missionObjectiveCheckers;

	// Token: 0x04000CE4 RID: 3300
	[Inject]
	private MissionObjectiveCheckersManager.InitData _initData;

	// Token: 0x04000CE5 RID: 3301
	[Inject]
	private ILevelEndActions _gameplayManager;

	// Token: 0x04000CE9 RID: 3305
	private MissionObjectiveChecker[] _activeMissionObjectiveCheckers = new MissionObjectiveChecker[0];

	// Token: 0x020002DB RID: 731
	public class InitData
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x00009ADD File Offset: 0x00007CDD
		public InitData(MissionObjective[] missionObjectives)
		{
			this.missionObjectives = missionObjectives;
		}

		// Token: 0x04000CEA RID: 3306
		public readonly MissionObjective[] missionObjectives;
	}
}
