using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x02000020 RID: 32
public class VRsenalScoreLogger : MonoBehaviour
{
	// Token: 0x0600008B RID: 139 RVA: 0x00002706 File Offset: 0x00000906
	protected IEnumerator Start()
	{
		this._levelEndActions.levelFinishedEvent += this.HandleLevelFinishedEvent;
		Debug.Log(string.Format("VRsenalLogger: Level started. Song={0}, Difficulty={1}, Characteristic={2}, Duration={3}", new object[]
		{
			this._difficultyBeatmap.level.songName,
			this._difficultyBeatmap.difficulty,
			this._difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.characteristicNameLocalizationKey,
			this._difficultyBeatmap.level.beatmapLevelData.audioClip.length
		}));
		yield return null;
		YieldInstruction yieldInstruction = new WaitForSeconds(10f);
		for (;;)
		{
			this.LogScore();
			yield return yieldInstruction;
		}
		yield break;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00002715 File Offset: 0x00000915
	protected void OnDestroy()
	{
		if (this._levelEndActions != null)
		{
			this._levelEndActions.levelFinishedEvent -= this.HandleLevelFinishedEvent;
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00002736 File Offset: 0x00000936
	private void HandleLevelFinishedEvent()
	{
		this.LogScore();
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000273E File Offset: 0x0000093E
	private void LogScore()
	{
		if (this._scoreController != null)
		{
			Debug.Log(string.Format("VRsenalLogger: Score={0}", this._scoreController.prevFrameModifiedScore));
		}
	}

	// Token: 0x0400007B RID: 123
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x0400007C RID: 124
	[Inject]
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x0400007D RID: 125
	[Inject]
	private ILevelEndActions _levelEndActions;
}
