using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x020002C3 RID: 707
public class MissionLevelFailedController : MonoBehaviour
{
	// Token: 0x06000BFA RID: 3066 RVA: 0x0000967A File Offset: 0x0000787A
	protected void Start()
	{
		this._gameplayManager.levelFailedEvent += this.HandleLevelFailed;
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00009693 File Offset: 0x00007893
	protected void OnDestroy()
	{
		if (this._gameplayManager != null)
		{
			this._gameplayManager.levelFailedEvent -= this.HandleLevelFailed;
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x000096B4 File Offset: 0x000078B4
	private void HandleLevelFailed()
	{
		base.StartCoroutine(this.LevelFailedCoroutine());
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x000096C3 File Offset: 0x000078C3
	private IEnumerator LevelFailedCoroutine()
	{
		LevelCompletionResults.LevelEndAction levelEndAction = this._initData.autoRestart ? LevelCompletionResults.LevelEndAction.Restart : LevelCompletionResults.LevelEndAction.None;
		LevelCompletionResults levelCompletionResults = this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.Failed, levelEndAction);
		MissionObjectiveResult[] results = this._missionObjectiveCheckersManager.GetResults();
		MissionCompletionResults missionCompletionResults = new MissionCompletionResults(levelCompletionResults, results);
		this._gameSongController.FailStopSong();
		this._beatmapObjectSpawnController.StopSpawning();
		this._beatmapObjectManager.DissolveAllObjects();
		this._levelFailedTextEffect.ShowEffect();
		yield return new WaitForSeconds(2f);
		this._missionLevelSceneSetupData.Finish(missionCompletionResults);
		yield break;
	}

	// Token: 0x04000CA2 RID: 3234
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;

	// Token: 0x04000CA3 RID: 3235
	[SerializeField]
	private LevelFailedTextEffect _levelFailedTextEffect;

	// Token: 0x04000CA4 RID: 3236
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _missionLevelSceneSetupData;

	// Token: 0x04000CA5 RID: 3237
	[SerializeField]
	private MissionObjectiveCheckersManager _missionObjectiveCheckersManager;

	// Token: 0x04000CA6 RID: 3238
	[Inject]
	private MissionLevelFailedController.InitData _initData;

	// Token: 0x04000CA7 RID: 3239
	[Inject]
	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	// Token: 0x04000CA8 RID: 3240
	[Inject]
	private GameSongController _gameSongController;

	// Token: 0x04000CA9 RID: 3241
	[Inject]
	private ILevelEndActions _gameplayManager;

	// Token: 0x04000CAA RID: 3242
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x020002C4 RID: 708
	public class InitData
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x000096D2 File Offset: 0x000078D2
		public InitData(bool autoRestart)
		{
			this.autoRestart = autoRestart;
		}

		// Token: 0x04000CAB RID: 3243
		public readonly bool autoRestart;
	}
}
