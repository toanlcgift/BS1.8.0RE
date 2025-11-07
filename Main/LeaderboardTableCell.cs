using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200035B RID: 859
public class LeaderboardTableCell : TableCell
{
	// Token: 0x17000332 RID: 818
	// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0000B9BF File Offset: 0x00009BBF
	public int rank
	{
		set
		{
			this._rankText.text = value.ToString();
		}
	}

	// Token: 0x17000333 RID: 819
	// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0000B9D3 File Offset: 0x00009BD3
	public string playerName
	{
		set
		{
			this._playerNameText.text = value;
		}
	}

	// Token: 0x17000334 RID: 820
	// (set) Token: 0x06000F1E RID: 3870 RVA: 0x0000B9E1 File Offset: 0x00009BE1
	public int score
	{
		set
		{
			this._scoreText.text = ((value >= 0) ? ScoreFormatter.Format(value) : "");
		}
	}

	// Token: 0x17000335 RID: 821
	// (set) Token: 0x06000F1F RID: 3871 RVA: 0x0000B9FF File Offset: 0x00009BFF
	public bool showSeparator
	{
		set
		{
			this._separatorImage.enabled = value;
		}
	}

	// Token: 0x17000336 RID: 822
	// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0000BA0D File Offset: 0x00009C0D
	public bool showFullCombo
	{
		set
		{
			this._fullComboText.enabled = value;
		}
	}

	// Token: 0x17000337 RID: 823
	// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
	public bool specialScore
	{
		set
		{
			Color color = value ? this._specialScoreColor : this._normalColor;
			TMP_FontAsset font = value ? this._specialScoreFont : this._normalFont;
			this._scoreText.color = color;
			this._playerNameText.color = color;
			this._rankText.color = color;
			this._fullComboText.color = color;
			this._scoreText.font = font;
			this._playerNameText.font = font;
			this._rankText.font = font;
			this._fullComboText.font = font;
		}
	}

	// Token: 0x04000F74 RID: 3956
	[SerializeField]
	private TextMeshProUGUI _rankText;

	// Token: 0x04000F75 RID: 3957
	[SerializeField]
	private TextMeshProUGUI _playerNameText;

	// Token: 0x04000F76 RID: 3958
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	// Token: 0x04000F77 RID: 3959
	[SerializeField]
	private TextMeshProUGUI _fullComboText;

	// Token: 0x04000F78 RID: 3960
	[SerializeField]
	private Color _normalColor;

	// Token: 0x04000F79 RID: 3961
	[SerializeField]
	private TMP_FontAsset _normalFont;

	// Token: 0x04000F7A RID: 3962
	[SerializeField]
	private Color _specialScoreColor;

	// Token: 0x04000F7B RID: 3963
	[SerializeField]
	private TMP_FontAsset _specialScoreFont;

	// Token: 0x04000F7C RID: 3964
	[SerializeField]
	private Image _separatorImage;
}
