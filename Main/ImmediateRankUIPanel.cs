using System;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x020002AD RID: 685
public class ImmediateRankUIPanel : MonoBehaviour
{
	// Token: 0x06000B8F RID: 2959 RVA: 0x000091EF File Offset: 0x000073EF
	protected void Start()
	{
		this._stringBuilder = new StringBuilder(16);
		this.RefreshUI();
		this._relativeScoreAndImmediateRankCounter.relativeScoreOrImmediateRankDidChangeEvent += this.HandleRelativeScoreAndImmediateRankCounterRelativeScoreOrImmediateRankDidChange;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0000921B File Offset: 0x0000741B
	protected void HandleRelativeScoreAndImmediateRankCounterRelativeScoreOrImmediateRankDidChange()
	{
		this.RefreshUI();
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x000349C4 File Offset: 0x00032BC4
	private void RefreshUI()
	{
		RankModel.Rank immediateRank = this._relativeScoreAndImmediateRankCounter.immediateRank;
		if (immediateRank != this._prevImmediateRank)
		{
			this._rankText.text = RankModel.GetRankName(immediateRank);
			this._prevImmediateRank = immediateRank;
		}
		float relativeScore = this._relativeScoreAndImmediateRankCounter.relativeScore;
		if (Mathf.Abs(this._prevRelativeScore - relativeScore) >= 0.001f)
		{
			this._stringBuilder.Remove(0, this._stringBuilder.Length);
			this._stringBuilder.AppendNumber((int)(relativeScore * 100f));
			this._stringBuilder.Append('.');
			this._stringBuilder.AppendNumber((int)((relativeScore * 100f - (float)((int)(relativeScore * 100f))) * 10f));
			this._stringBuilder.Append('%');
			this._relativeScoreText.text = this._stringBuilder.ToString();
			this._prevRelativeScore = relativeScore;
		}
	}

	// Token: 0x04000C38 RID: 3128
	[SerializeField]
	private TextMeshProUGUI _rankText;

	// Token: 0x04000C39 RID: 3129
	[SerializeField]
	private TextMeshProUGUI _relativeScoreText;

	// Token: 0x04000C3A RID: 3130
	[Inject]
	private RelativeScoreAndImmediateRankCounter _relativeScoreAndImmediateRankCounter;

	// Token: 0x04000C3B RID: 3131
	private StringBuilder _stringBuilder;

	// Token: 0x04000C3C RID: 3132
	private float _prevRelativeScore = -1f;

	// Token: 0x04000C3D RID: 3133
	private RankModel.Rank _prevImmediateRank = RankModel.Rank.SSS;
}
