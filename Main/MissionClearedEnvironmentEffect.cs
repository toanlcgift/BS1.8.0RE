using System;
using UnityEngine;
using Zenject;

// Token: 0x0200028D RID: 653
public class MissionClearedEnvironmentEffect : MonoBehaviour
{
	// Token: 0x06000AF8 RID: 2808 RVA: 0x0000890E File Offset: 0x00006B0E
	protected void Awake()
	{
		this._missionObjectiveCheckersManager.objectiveWasClearedEvent += this.HandleMissionObjectiveCheckersManagerObjectiveWasCleared;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00008927 File Offset: 0x00006B27
	protected void OnDestroy()
	{
		this._missionObjectiveCheckersManager.objectiveWasClearedEvent -= this.HandleMissionObjectiveCheckersManagerObjectiveWasCleared;
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x00033220 File Offset: 0x00031420
	private void HandleMissionObjectiveCheckersManagerObjectiveWasCleared()
	{
		BeatmapEventData beatmapEventData = new BeatmapEventData(0f, BeatmapEventType.Event8, 0);
		this._beatmapObjectCallbackController.SendBeatmapEventDidTriggerEvent(beatmapEventData);
	}

	// Token: 0x04000B8C RID: 2956
	[SerializeField]
	private MissionObjectiveCheckersManager _missionObjectiveCheckersManager;

	// Token: 0x04000B8D RID: 2957
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;
}
