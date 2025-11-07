using System;
using System.Threading;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200042E RID: 1070
public class StandardLevelDetailView : MonoBehaviour
{
	// Token: 0x140000C4 RID: 196
	// (add) Token: 0x0600146C RID: 5228 RVA: 0x0004AA28 File Offset: 0x00048C28
	// (remove) Token: 0x0600146D RID: 5229 RVA: 0x0004AA60 File Offset: 0x00048C60
	public event Action<StandardLevelDetailView, IDifficultyBeatmap> didChangeDifficultyBeatmapEvent;

	// Token: 0x140000C5 RID: 197
	// (add) Token: 0x0600146E RID: 5230 RVA: 0x0004AA98 File Offset: 0x00048C98
	// (remove) Token: 0x0600146F RID: 5231 RVA: 0x0004AAD0 File Offset: 0x00048CD0
	public event Action<StandardLevelDetailView, Toggle> didFavoriteToggleChangeEvent;

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06001470 RID: 5232 RVA: 0x0000F6DA File Offset: 0x0000D8DA
	public IDifficultyBeatmap selectedDifficultyBeatmap
	{
		get
		{
			return this._selectedDifficultyBeatmap;
		}
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06001471 RID: 5233 RVA: 0x0000F6E2 File Offset: 0x0000D8E2
	public Button playButton
	{
		get
		{
			return this._playButton;
		}
	}

	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x06001472 RID: 5234 RVA: 0x0000F6EA File Offset: 0x0000D8EA
	public Button practiceButton
	{
		get
		{
			return this._practiceButton;
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (set) Token: 0x06001473 RID: 5235 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
	public bool hidePracticeButton
	{
		set
		{
			this._practiceButton.gameObject.SetActive(!value);
		}
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x0004AB08 File Offset: 0x00048D08
	public void SetContent(IBeatmapLevel level, BeatmapDifficulty defaultDifficulty, BeatmapCharacteristicSO defaultBeatmapCharacteristic, PlayerData playerData, bool showPlayerStats)
	{
		this._level = level;
		this._playerData = playerData;
		this._showPlayerStats = showPlayerStats;
		if (this._level != null && this._level.beatmapLevelData != null)
		{
			this._beatmapCharacteristicSegmentedControlController.SetData(level.beatmapLevelData.difficultyBeatmapSets, defaultBeatmapCharacteristic ?? this._beatmapCharacteristicSegmentedControlController.selectedBeatmapCharacteristic);
			this._beatmapDifficultySegmentedControlController.SetData(level.beatmapLevelData.GetDifficultyBeatmapSet(this._beatmapCharacteristicSegmentedControlController.selectedBeatmapCharacteristic).difficultyBeatmaps, defaultDifficulty);
		}
		this.RefreshContent();
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0004AB94 File Offset: 0x00048D94
	protected void Awake()
	{
		this._beatmapDifficultySegmentedControlController.didSelectDifficultyEvent += this.HandleBeatmapDifficultySegmentedControlControllerDidSelectDifficulty;
		this._beatmapCharacteristicSegmentedControlController.didSelectBeatmapCharacteristicEvent += this.HandleBeatmapCharacteristicSegmentedControlControllerDidSelectBeatmapCharacteristic;
		this._toggleBinder = new ToggleBinder();
		this._toggleBinder.AddBinding(this._favoriteToggle, delegate(bool on)
		{
			Action<StandardLevelDetailView, Toggle> action = this.didFavoriteToggleChangeEvent;
			if (action == null)
			{
				return;
			}
			action(this, this._favoriteToggle);
		});
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x0004ABF8 File Offset: 0x00048DF8
	protected void OnDestroy()
	{
		if (this._beatmapDifficultySegmentedControlController != null)
		{
			this._beatmapDifficultySegmentedControlController.didSelectDifficultyEvent -= this.HandleBeatmapDifficultySegmentedControlControllerDidSelectDifficulty;
		}
		if (this._beatmapCharacteristicSegmentedControlController != null)
		{
			this._beatmapCharacteristicSegmentedControlController.didSelectBeatmapCharacteristicEvent -= this.HandleBeatmapCharacteristicSegmentedControlControllerDidSelectBeatmapCharacteristic;
		}
		if (this._blurredCoverTexture != null)
		{
			UnityEngine.Object.Destroy(this._blurredCoverTexture);
			this._blurredCoverTexture = null;
		}
		ToggleBinder toggleBinder = this._toggleBinder;
		if (toggleBinder != null)
		{
			toggleBinder.ClearBindings();
		}
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource == null)
		{
			return;
		}
		cancellationTokenSource.Cancel();
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x0000F708 File Offset: 0x0000D908
	private void HandleBeatmapDifficultySegmentedControlControllerDidSelectDifficulty(BeatmapDifficultySegmentedControlController controller, BeatmapDifficulty difficulty)
	{
		this.RefreshContent();
		Action<StandardLevelDetailView, IDifficultyBeatmap> action = this.didChangeDifficultyBeatmapEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._selectedDifficultyBeatmap);
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x0004AC90 File Offset: 0x00048E90
	private void HandleBeatmapCharacteristicSegmentedControlControllerDidSelectBeatmapCharacteristic(BeatmapCharacteristicSegmentedControlController controller, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		this._beatmapDifficultySegmentedControlController.SetData(this._level.beatmapLevelData.GetDifficultyBeatmapSet(this._beatmapCharacteristicSegmentedControlController.selectedBeatmapCharacteristic).difficultyBeatmaps, this._beatmapDifficultySegmentedControlController.selectedDifficulty);
		this.RefreshContent();
		Action<StandardLevelDetailView, IDifficultyBeatmap> action = this.didChangeDifficultyBeatmapEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._selectedDifficultyBeatmap);
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x0004ACF0 File Offset: 0x00048EF0
	public void RefreshContent()
	{
		if (this._level == null || this._level.beatmapLevelData == null)
		{
			return;
		}
		this._toggleBinder.Disable();
		this._favoriteToggle.isOn = this._playerData.IsLevelUserFavorite(this._level);
		this._toggleBinder.Enable();
		this._selectedDifficultyBeatmap = this._level.beatmapLevelData.GetDifficultyBeatmap(this._beatmapCharacteristicSegmentedControlController.selectedBeatmapCharacteristic, this._beatmapDifficultySegmentedControlController.selectedDifficulty);
		this._songNameText.text = this._level.songName;
		this.SetTextureAsync(this._level);
		this._levelParamsPanel.duration = this._level.beatmapLevelData.audioClip.length;
		this._levelParamsPanel.bpm = this._level.beatsPerMinute;
		this._levelParamsPanel.notesPerSecond = (float)this._selectedDifficultyBeatmap.beatmapData.notesCount / this._level.beatmapLevelData.audioClip.length;
		this._levelParamsPanel.notesCount = this._selectedDifficultyBeatmap.beatmapData.notesCount;
		this._levelParamsPanel.obstaclesCount = this._selectedDifficultyBeatmap.beatmapData.obstaclesCount;
		this._levelParamsPanel.bombsCount = this._selectedDifficultyBeatmap.beatmapData.bombsCount;
		if (this._playerStatsContainer)
		{
			if (this._showPlayerStats && this._playerData != null)
			{
				this._playerStatsContainer.SetActive(true);
				PlayerLevelStatsData playerLevelStatsData = this._playerData.GetPlayerLevelStatsData(this._level.levelID, this._selectedDifficultyBeatmap.difficulty, this._selectedDifficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic);
				this._highScoreText.text = (playerLevelStatsData.validScore ? playerLevelStatsData.highScore.ToString() : "-");
				string text = playerLevelStatsData.fullCombo ? Localization.Get("STATS_FULL_COMBO").ToUpper() : playerLevelStatsData.maxCombo.ToString();
				this._maxComboText.text = (playerLevelStatsData.validScore ? text : "-");
				this._maxRankText.text = (playerLevelStatsData.validScore ? RankModel.GetRankName(playerLevelStatsData.maxRank) : "-");
			}
			else
			{
				this._playerStatsContainer.SetActive(false);
			}
		}
		this._separator.SetActive(!this._playerStatsContainer.activeSelf);
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x0004AF68 File Offset: 0x00049168
	public async void SetTextureAsync(IPreviewBeatmapLevel level)
	{
		if (!(this._settingTextureForLevelId == level.levelID))
		{
			try
			{
				this._settingTextureForLevelId = level.levelID;
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._cancellationTokenSource = new CancellationTokenSource();
				this._coverImage.enabled = false;
				this._coverImage.texture = null;
				CancellationToken cancellationToken = this._cancellationTokenSource.Token;
				Texture2D src = await level.GetCoverImageTexture2DAsync(cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				if (this._blurredCoverTexture != null)
				{
					UnityEngine.Object.Destroy(this._blurredCoverTexture);
				}
				this._blurredCoverTexture = this._kawaseBlurRenderer.Blur(src, KawaseBlurRendererSO.KernelSize.Kernel7, 0);
				this._coverImage.texture = this._blurredCoverTexture;
				this._coverImage.enabled = true;
				cancellationToken = default(CancellationToken);
			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (this._settingTextureForLevelId == level.levelID)
				{
					this._settingTextureForLevelId = null;
				}
			}
		}
	}

	// Token: 0x04001412 RID: 5138
	[SerializeField]
	private Button _playButton;

	// Token: 0x04001413 RID: 5139
	[SerializeField]
	private Button _practiceButton;

	// Token: 0x04001414 RID: 5140
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x04001415 RID: 5141
	[SerializeField]
	private LevelParamsPanel _levelParamsPanel;

	// Token: 0x04001416 RID: 5142
	[SerializeField]
	private TextMeshProUGUI _highScoreText;

	// Token: 0x04001417 RID: 5143
	[SerializeField]
	private TextMeshProUGUI _maxComboText;

	// Token: 0x04001418 RID: 5144
	[SerializeField]
	private TextMeshProUGUI _maxRankText;

	// Token: 0x04001419 RID: 5145
	[SerializeField]
	private RawImage _coverImage;

	// Token: 0x0400141A RID: 5146
	[SerializeField]
	private BeatmapDifficultySegmentedControlController _beatmapDifficultySegmentedControlController;

	// Token: 0x0400141B RID: 5147
	[SerializeField]
	private BeatmapCharacteristicSegmentedControlController _beatmapCharacteristicSegmentedControlController;

	// Token: 0x0400141C RID: 5148
	[SerializeField]
	private GameObject _playerStatsContainer;

	// Token: 0x0400141D RID: 5149
	[SerializeField]
	private GameObject _separator;

	// Token: 0x0400141E RID: 5150
	[SerializeField]
	private Toggle _favoriteToggle;

	// Token: 0x0400141F RID: 5151
	[Space]
	[SerializeField]
	private KawaseBlurRendererSO _kawaseBlurRenderer;

	// Token: 0x04001422 RID: 5154
	private bool _showPlayerStats;

	// Token: 0x04001423 RID: 5155
	private IBeatmapLevel _level;

	// Token: 0x04001424 RID: 5156
	private PlayerData _playerData;

	// Token: 0x04001425 RID: 5157
	private IDifficultyBeatmap _selectedDifficultyBeatmap;

	// Token: 0x04001426 RID: 5158
	private string _settingTextureForLevelId;

	// Token: 0x04001427 RID: 5159
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x04001428 RID: 5160
	private ToggleBinder _toggleBinder;

	// Token: 0x04001429 RID: 5161
	private Texture2D _blurredCoverTexture;
}
