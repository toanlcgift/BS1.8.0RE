using System;
using UnityEngine;
using Zenject;

// Token: 0x020002B1 RID: 689
public class RelativeScoreAndImmediateRankCounter : MonoBehaviour
{
	// Token: 0x14000049 RID: 73
	// (add) Token: 0x06000BA2 RID: 2978 RVA: 0x00034E2C File Offset: 0x0003302C
	// (remove) Token: 0x06000BA3 RID: 2979 RVA: 0x00034E64 File Offset: 0x00033064
	public event Action relativeScoreOrImmediateRankDidChangeEvent;

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x000092FB File Offset: 0x000074FB
	// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x00009303 File Offset: 0x00007503
	public float relativeScore { get; private set; }

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0000930C File Offset: 0x0000750C
	// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00009314 File Offset: 0x00007514
	public RankModel.Rank immediateRank { get; private set; }

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00034E9C File Offset: 0x0003309C
	protected void Start()
	{
		this._gameplayModifiersScoreMultiplier = this._gameplayModifiersModel.GetTotalMultiplier(this._gameplayModifiers);
		this.immediateRank = RankModel.MaxRankForGameplayModifiers(this._gameplayModifiers, this._gameplayModifiersModel);
		if (this.immediateRank == RankModel.Rank.SSS)
		{
			this.immediateRank = RankModel.Rank.SS;
		}
		this.relativeScore = 1f;
		this._scoreController.immediateMaxPossibleScoreDidChangeEvent += this.HandleScoreControllerImmediateMaxPossibleScoreDidChange;
		Action action = this.relativeScoreOrImmediateRankDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0000931D File Offset: 0x0000751D
	protected void OnDestroy()
	{
		if (this._scoreController)
		{
			this._scoreController.immediateMaxPossibleScoreDidChangeEvent -= this.HandleScoreControllerImmediateMaxPossibleScoreDidChange;
		}
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00009343 File Offset: 0x00007543
	private void HandleScoreControllerImmediateMaxPossibleScoreDidChange(int immediateMaxPossibleScore, int immediateMaxPossibleModifiedScore)
	{
		this.UpdateRelativeScoreAndImmediateRank(this._scoreController.prevFrameRawScore, this._scoreController.prevFrameModifiedScore, immediateMaxPossibleScore, immediateMaxPossibleModifiedScore);
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00034F1C File Offset: 0x0003311C
	private void UpdateRelativeScoreAndImmediateRank(int score, int modifiedScore, int maxPossibleScore, int maxPossibleModifiedScore)
	{
		this.immediateRank = RankModel.GetRankForScore(score, modifiedScore, maxPossibleScore, maxPossibleModifiedScore);
		if (this.immediateRank == RankModel.Rank.SSS)
		{
			this.immediateRank = RankModel.Rank.SS;
		}
		if (maxPossibleScore > 0)
		{
			this.relativeScore = (float)score / (float)maxPossibleScore;
		}
		Action action = this.relativeScoreOrImmediateRankDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000C54 RID: 3156
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModel;

	// Token: 0x04000C55 RID: 3157
	[SerializeField]
	private ScoreController _scoreController;

	// Token: 0x04000C56 RID: 3158
	[Inject]
	private GameplayModifiers _gameplayModifiers;

	// Token: 0x04000C5A RID: 3162
	private float _gameplayModifiersScoreMultiplier;
}
