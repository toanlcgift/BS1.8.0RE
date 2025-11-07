using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x020002AF RID: 687
public class MissionObjectivesGameUIController : MonoBehaviour
{
	// Token: 0x06000B99 RID: 2969 RVA: 0x0000925D File Offset: 0x0000745D
	protected void Start()
	{
		this._missionObjectiveCheckersManager.objectivesListDidChangeEvent += this.HandleMissionObjectiveCheckersManagerObjectivesListDidChange;
		this.CreateUIElements();
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0000927C File Offset: 0x0000747C
	protected void OnDestroy()
	{
		if (this._missionObjectiveCheckersManager != null)
		{
			this._missionObjectiveCheckersManager.objectivesListDidChangeEvent -= this.HandleMissionObjectiveCheckersManagerObjectivesListDidChange;
		}
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x000092A3 File Offset: 0x000074A3
	private void HandleMissionObjectiveCheckersManagerObjectivesListDidChange()
	{
		this.CreateUIElements();
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00034CD0 File Offset: 0x00032ED0
	private void CreateUIElements()
	{
		if (this._missionObjectiveGameUIViews != null)
		{
			foreach (MissionObjectiveGameUIView missionObjectiveGameUIView in this._missionObjectiveGameUIViews)
			{
				UnityEngine.Object.Destroy(missionObjectiveGameUIView.gameObject);
			}
		}
		MissionObjectiveChecker[] activeMissionObjectiveCheckers = this._missionObjectiveCheckersManager.activeMissionObjectiveCheckers;
		this._backgroundGO.SetActive(activeMissionObjectiveCheckers.Length != 0);
		this._missionObjectiveGameUIViews = new List<MissionObjectiveGameUIView>(activeMissionObjectiveCheckers.Length);
		int num = activeMissionObjectiveCheckers.Length;
		float num2 = -(this._elementWidth * (float)num + this._separator * (float)(num - 1)) * 0.5f + this._elementWidth * 0.5f;
		for (int i = 0; i < activeMissionObjectiveCheckers.Length; i++)
		{
			MissionObjectiveChecker missionObjectiveChecker = this._missionObjectiveCheckersManager.activeMissionObjectiveCheckers[i];
			MissionObjectiveGameUIView missionObjectiveGameUIView2 = (MissionObjectiveGameUIView)UnityEngine.Object.Instantiate(this._missionObjectiveGameUIViewPrefab, base.transform, false);
			missionObjectiveGameUIView2.transform.localPosition = new Vector2(num2, 0f);
			missionObjectiveGameUIView2.SetMissionObjectiveChecker(missionObjectiveChecker);
			this._missionObjectiveGameUIViews.Add(missionObjectiveGameUIView2);
			num2 += this._separator + this._elementWidth;
		}
	}

	// Token: 0x04000C4C RID: 3148
	[SerializeField]
	private MissionObjectiveGameUIView _missionObjectiveGameUIViewPrefab;

	// Token: 0x04000C4D RID: 3149
	[SerializeField]
	private GameObject _backgroundGO;

	// Token: 0x04000C4E RID: 3150
	[SerializeField]
	private float _separator = 1f;

	// Token: 0x04000C4F RID: 3151
	[SerializeField]
	private float _elementWidth = 1f;

	// Token: 0x04000C50 RID: 3152
	[Inject]
	private MissionObjectiveCheckersManager _missionObjectiveCheckersManager;

	// Token: 0x04000C51 RID: 3153
	private List<MissionObjectiveGameUIView> _missionObjectiveGameUIViews;
}
