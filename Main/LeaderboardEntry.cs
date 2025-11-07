using System;
using TMPro;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class LeaderboardEntry : MonoBehaviour
{
	// Token: 0x060010A8 RID: 4264 RVA: 0x0004116C File Offset: 0x0003F36C
	public void SetScore(int score, string playerName, int rank, bool highlighted, bool showSeparator)
	{
		this._scoreText.text = ScoreFormatter.Format(score);
		this._playerNameText.text = playerName;
		this._rankText.text = rank.ToString();
		Color color = highlighted ? this._color : (this._color * 0.5f);
		this._scoreText.color = color;
		this._playerNameText.color = color;
		this._rankText.color = color;
	}

	// Token: 0x040010C2 RID: 4290
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	// Token: 0x040010C3 RID: 4291
	[SerializeField]
	private TextMeshProUGUI _playerNameText;

	// Token: 0x040010C4 RID: 4292
	[SerializeField]
	private TextMeshProUGUI _rankText;

	// Token: 0x040010C5 RID: 4293
	[SerializeField]
	private Color _color = Color.black;
}
