using System;
using System.Collections;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x02000406 RID: 1030
public class ResultsViewController : ViewController
{
	// Token: 0x140000B2 RID: 178
	// (add) Token: 0x06001361 RID: 4961 RVA: 0x000480A4 File Offset: 0x000462A4
	// (remove) Token: 0x06001362 RID: 4962 RVA: 0x000480DC File Offset: 0x000462DC
	public event Action<ResultsViewController> continueButtonPressedEvent;

	// Token: 0x140000B3 RID: 179
	// (add) Token: 0x06001363 RID: 4963 RVA: 0x00048114 File Offset: 0x00046314
	// (remove) Token: 0x06001364 RID: 4964 RVA: 0x0004814C File Offset: 0x0004634C
	public event Action<ResultsViewController> restartButtonPressedEvent;

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x06001365 RID: 4965 RVA: 0x0000E9BF File Offset: 0x0000CBBF
	public bool practice
	{
		get
		{
			return this._practice;
		}
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x0000E9C7 File Offset: 0x0000CBC7
	public void Init(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, bool practice, bool newHighScore)
	{
		this._levelCompletionResults = levelCompletionResults;
		this._difficultyBeatmap = difficultyBeatmap;
		this._newHighScore = newHighScore;
		this._practice = practice;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00048184 File Offset: 0x00046384
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._restartButton, new Action(this.RestartButtonPressed));
			base.buttonBinder.AddBinding(this._continueButton, new Action(this.ContinueButtonPressed));
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.SetDataToUI();
			if (this._levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared && this._newHighScore)
			{
				this._startFireworksAfterDelayCoroutine = base.StartCoroutine(this.StartFireworksAfterDelay(1.95f));
				this._songPreviewPlayer.CrossfadeTo(this._levelClearedAudioClip, 0f, this._levelClearedAudioClip.length, 1f);
			}
		}
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x0000E9E6 File Offset: 0x0000CBE6
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (this._startFireworksAfterDelayCoroutine != null)
		{
			base.StopCoroutine(this._startFireworksAfterDelayCoroutine);
			this._startFireworksAfterDelayCoroutine = null;
		}
		if (this._fireworksController != null)
		{
			this._fireworksController.enabled = false;
		}
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x0000EA1D File Offset: 0x0000CC1D
	private IEnumerator StartFireworksAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		this._fireworksController.enabled = true;
		yield break;
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x0004822C File Offset: 0x0004642C
	private void SetDataToUI()
	{
		this._failedPanel.SetActive(this._levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed);
		this._clearedPanel.SetActive(this._levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared);
		IBeatmapLevel level = this._difficultyBeatmap.level;
		BeatmapDifficulty difficulty = this._difficultyBeatmap.difficulty;
		int notesCount = this._difficultyBeatmap.beatmapData.notesCount;
		if (this._levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
		{
			this._failedSongNameText.text = level.songName + " <size=80%>" + level.songSubName;
			this._failedDifficultyText.text = difficulty.Name();
		}
		else if (this._levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
		{
			this._clearedSongNameText.text = level.songName + " <size=80%>" + level.songSubName;
			this._clearedDifficultyText.text = difficulty.Name();
		}
		this._scoreText.text = ScoreFormatter.Format(this._levelCompletionResults.modifiedScore);
		this._rankText.text = RankModel.GetRankName(this._levelCompletionResults.rank);
		this._goodCutsPercentageText.text = this._levelCompletionResults.goodCutsCount.ToString() + "<size=50%> / " + notesCount.ToString() + "</size>";
		this._comboText.text = (this._levelCompletionResults.fullCombo ? Localization.Get("STATS_FULL_COMBO") : (Localization.Get("STATS_MAX_COMBO") + " " + this._levelCompletionResults.maxCombo.ToString()));
		this._newHighScoreText.SetActive(this._newHighScore);
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x0000EA33 File Offset: 0x0000CC33
	private void ContinueButtonPressed()
	{
		Action<ResultsViewController> action = this.continueButtonPressedEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x0000EA46 File Offset: 0x0000CC46
	private void RestartButtonPressed()
	{
		Action<ResultsViewController> action = this.restartButtonPressedEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x04001317 RID: 4887
	[SerializeField]
	private Button _restartButton;

	// Token: 0x04001318 RID: 4888
	[SerializeField]
	private Button _continueButton;

	// Token: 0x04001319 RID: 4889
	[Space]
	[SerializeField]
	private GameObject _failedPanel;

	// Token: 0x0400131A RID: 4890
	[SerializeField]
	private GameObject _clearedPanel;

	// Token: 0x0400131B RID: 4891
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	// Token: 0x0400131C RID: 4892
	[SerializeField]
	private GameObject _newHighScoreText;

	// Token: 0x0400131D RID: 4893
	[SerializeField]
	private TextMeshProUGUI _rankText;

	// Token: 0x0400131E RID: 4894
	[SerializeField]
	private TextMeshProUGUI _goodCutsPercentageText;

	// Token: 0x0400131F RID: 4895
	[SerializeField]
	private TextMeshProUGUI _comboText;

	// Token: 0x04001320 RID: 4896
	[Space]
	[SerializeField]
	private TextMeshProUGUI _clearedSongNameText;

	// Token: 0x04001321 RID: 4897
	[SerializeField]
	private TextMeshProUGUI _clearedDifficultyText;

	// Token: 0x04001322 RID: 4898
	[SerializeField]
	private TextMeshProUGUI _failedSongNameText;

	// Token: 0x04001323 RID: 4899
	[SerializeField]
	private TextMeshProUGUI _failedDifficultyText;

	// Token: 0x04001324 RID: 4900
	[Space]
	[SerializeField]
	private AudioClip _levelClearedAudioClip;

	// Token: 0x04001325 RID: 4901
	[Inject]
	private FireworksController _fireworksController;

	// Token: 0x04001326 RID: 4902
	[Inject]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x04001329 RID: 4905
	private LevelCompletionResults _levelCompletionResults;

	// Token: 0x0400132A RID: 4906
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x0400132B RID: 4907
	private Coroutine _startFireworksAfterDelayCoroutine;

	// Token: 0x0400132C RID: 4908
	private bool _newHighScore;

	// Token: 0x0400132D RID: 4909
	private bool _practice;
}
