using System;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x020002B3 RID: 691
public class ScoreUIController : MonoBehaviour
{
	// Token: 0x06000BB5 RID: 2997 RVA: 0x000093DA File Offset: 0x000075DA
	protected void Start()
	{
		this.RegisterForEvents();
		this._stringBuilder = new StringBuilder(40);
		this.UpdateScore(0, 0);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x000093F7 File Offset: 0x000075F7
	protected void OnEnable()
	{
		this.RegisterForEvents();
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x000093FF File Offset: 0x000075FF
	protected void OnDisable()
	{
		this.UnregisterFromEvents();
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x00009407 File Offset: 0x00007607
	private void RegisterForEvents()
	{
		if (this._scoreController == null)
		{
			return;
		}
		this._scoreController.scoreDidChangeEvent -= this.HandleScoreDidChangeRealtime;
		this._scoreController.scoreDidChangeEvent += this.HandleScoreDidChangeRealtime;
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x00009446 File Offset: 0x00007646
	private void UnregisterFromEvents()
	{
		if (this._scoreController == null)
		{
			return;
		}
		this._scoreController.scoreDidChangeEvent -= this.HandleScoreDidChangeRealtime;
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0000946E File Offset: 0x0000766E
	private void HandleScoreDidChangeRealtime(int rawScore, int modifiedScore)
	{
		this.UpdateScore(rawScore, modifiedScore);
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x00035090 File Offset: 0x00033290
	protected void UpdateScore(int rawScore, int modifiedScore)
	{
		int num = (this._initData.scoreDisplayType == ScoreUIController.ScoreDisplayType.ModifiedScore) ? modifiedScore : rawScore;
		this._stringBuilder.Remove(0, this._stringBuilder.Length);
		if (num > 999999)
		{
			this._stringBuilder.AppendNumber(num / 1000000 - num / 1000000000 * 1000000);
			this._stringBuilder.Append(' ');
			this.Append000Number(this._stringBuilder, num / 1000 - num / 1000000 * 1000);
			this._stringBuilder.Append(' ');
			this.Append000Number(this._stringBuilder, num - num / 1000 * 1000);
		}
		else if (num > 999)
		{
			this._stringBuilder.AppendNumber(num / 1000 - num / 1000000 * 1000);
			this._stringBuilder.Append(' ');
			this.Append000Number(this._stringBuilder, num - num / 1000 * 1000);
		}
		else
		{
			this._stringBuilder.AppendNumber(num);
		}
		this._scoreText.text = this._stringBuilder.ToString();
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x00009478 File Offset: 0x00007678
	private void Append000Number(StringBuilder stringBuilder, int number)
	{
		if (number < 100)
		{
			stringBuilder.Append('0');
		}
		if (number < 10)
		{
			stringBuilder.Append('0');
		}
		stringBuilder.AppendNumber(number);
	}

	// Token: 0x04000C62 RID: 3170
	[SerializeField]
	private TextMeshProUGUI _scoreText;

	// Token: 0x04000C63 RID: 3171
	[InjectOptional]
	private ScoreUIController.InitData _initData = new ScoreUIController.InitData(ScoreUIController.ScoreDisplayType.ModifiedScore);

	// Token: 0x04000C64 RID: 3172
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x04000C65 RID: 3173
	private StringBuilder _stringBuilder;

	// Token: 0x04000C66 RID: 3174
	private const int kMaxNumberOfDigits = 9;

	// Token: 0x020002B4 RID: 692
	public class InitData
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x000094B1 File Offset: 0x000076B1
		public InitData(ScoreUIController.ScoreDisplayType scoreDisplayType)
		{
			this.scoreDisplayType = scoreDisplayType;
		}

		// Token: 0x04000C67 RID: 3175
		public readonly ScoreUIController.ScoreDisplayType scoreDisplayType;
	}

	// Token: 0x020002B5 RID: 693
	public enum ScoreDisplayType
	{
		// Token: 0x04000C69 RID: 3177
		RawScore,
		// Token: 0x04000C6A RID: 3178
		ModifiedScore
	}
}
