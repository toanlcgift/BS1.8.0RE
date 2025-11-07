using System;
using System.Collections.Generic;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200041F RID: 1055
public class DemoMenuLevelPanelView : MonoBehaviour
{
	// Token: 0x140000BF RID: 191
	// (add) Token: 0x06001405 RID: 5125 RVA: 0x00049E00 File Offset: 0x00048000
	// (remove) Token: 0x06001406 RID: 5126 RVA: 0x00049E38 File Offset: 0x00048038
	public event Action<DemoMenuLevelPanelView> playButtonWasPressedEvent;

	// Token: 0x06001407 RID: 5127 RVA: 0x0000F12C File Offset: 0x0000D32C
	protected void Start()
	{
		this._buttonBinder = new ButtonBinder(this._playButton, new Action(this.PlayButtonWasPressed));
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0000F14B File Offset: 0x0000D34B
	protected void OnDestroy()
	{
		if (this._buttonBinder != null)
		{
			this._buttonBinder.ClearBindings();
		}
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x00049E70 File Offset: 0x00048070
	public void Init(IDifficultyBeatmap difficultyBeatmap)
	{
		this._songNameText.text = difficultyBeatmap.level.songName;
		this._difficultyText.text = difficultyBeatmap.difficulty.Name();
		LocalLeaderboardsModel.LeaderboardType leaderboardType = LocalLeaderboardsModel.LeaderboardType.Daily;
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
		List<LocalLeaderboardsModel.ScoreData> scores = this._localLeaderboardsModel.GetScores(leaderboardID, leaderboardType);
		int lastScorePosition = this._localLeaderboardsModel.GetLastScorePosition(leaderboardID, leaderboardType);
		this._localLeaderboardTableView.SetScores(scores, lastScorePosition, 10);
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x0000F160 File Offset: 0x0000D360
	public void PlayButtonWasPressed()
	{
		if (this.playButtonWasPressedEvent != null)
		{
			this.playButtonWasPressedEvent(this);
		}
	}

	// Token: 0x040013BF RID: 5055
	[SerializeField]
	private LocalLeaderboardsModel _localLeaderboardsModel;

	// Token: 0x040013C0 RID: 5056
	[Space]
	[SerializeField]
	private Button _playButton;

	// Token: 0x040013C1 RID: 5057
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x040013C2 RID: 5058
	[SerializeField]
	private TextMeshProUGUI _difficultyText;

	// Token: 0x040013C3 RID: 5059
	[SerializeField]
	private LocalLeaderboardTableView _localLeaderboardTableView;

	// Token: 0x040013C5 RID: 5061
	private const int kMaxNumberOfCells = 10;

	// Token: 0x040013C6 RID: 5062
	private ButtonBinder _buttonBinder;
}
