using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x020002C9 RID: 713
public class StandardLevelFailedController : MonoBehaviour
{
	// Token: 0x06000C0F RID: 3087 RVA: 0x0000973A File Offset: 0x0000793A
	protected void Start()
	{
		this._gameplayManager.levelFailedEvent += this.HandleLevelFailed;
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x00009753 File Offset: 0x00007953
	protected void OnDestroy()
	{
		if (this._gameplayManager != null)
		{
			this._gameplayManager.levelFailedEvent -= this.HandleLevelFailed;
		}
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00009774 File Offset: 0x00007974
	private void HandleLevelFailed()
	{
		base.StartCoroutine(this.LevelFailedCoroutine());
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00009783 File Offset: 0x00007983
	private IEnumerator LevelFailedCoroutine()
	{
		base.transform.eulerAngles = new Vector3(0f, this._environmentSpawnRotation.targetRotation, 0f);
		LevelCompletionResults.LevelEndAction levelEndAction = this._initData.autoRestart ? LevelCompletionResults.LevelEndAction.Restart : LevelCompletionResults.LevelEndAction.None;
		LevelCompletionResults levelCompletionResults = this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.Failed, levelEndAction);
		this._gameSongController.FailStopSong();
		this._beatmapObjectSpawnController.StopSpawning();
		this._beatmapObjectManager.DissolveAllObjects();
		this._levelFailedTextEffect.ShowEffect();
		yield return new WaitForSeconds(2f);
		this._standardLevelSceneSetupData.Finish(levelCompletionResults);
		yield break;
	}

	// Token: 0x04000CB8 RID: 3256
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;

	// Token: 0x04000CB9 RID: 3257
	[SerializeField]
	private LevelFailedTextEffect _levelFailedTextEffect;

	// Token: 0x04000CBA RID: 3258
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _standardLevelSceneSetupData;

	// Token: 0x04000CBB RID: 3259
	[Inject]
	private StandardLevelFailedController.InitData _initData;

	// Token: 0x04000CBC RID: 3260
	[Inject]
	private ILevelEndActions _gameplayManager;

	// Token: 0x04000CBD RID: 3261
	[Inject]
	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	// Token: 0x04000CBE RID: 3262
	[Inject]
	private GameSongController _gameSongController;

	// Token: 0x04000CBF RID: 3263
	[Inject]
	private EnvironmentSpawnRotation _environmentSpawnRotation;

	// Token: 0x04000CC0 RID: 3264
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x020002CA RID: 714
	public class InitData
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x00009792 File Offset: 0x00007992
		public InitData(bool autoRestart)
		{
			this.autoRestart = autoRestart;
		}

		// Token: 0x04000CC1 RID: 3265
		public readonly bool autoRestart;
	}
}
