using System;
using TMPro;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class LevelParamsPanel : MonoBehaviour
{
	// Token: 0x170003D1 RID: 977
	// (set) Token: 0x0600143C RID: 5180 RVA: 0x0000F452 File Offset: 0x0000D652
	public float duration
	{
		set
		{
			this._durationText.text = value.MinSecDurationText();
		}
	}

	// Token: 0x170003D2 RID: 978
	// (set) Token: 0x0600143D RID: 5181 RVA: 0x0000F465 File Offset: 0x0000D665
	public float notesPerSecond
	{
		set
		{
			this._notesPerSecondText.text = value.ToString("F2");
		}
	}

	// Token: 0x170003D3 RID: 979
	// (set) Token: 0x0600143E RID: 5182 RVA: 0x0000F47E File Offset: 0x0000D67E
	public float bpm
	{
		set
		{
			this._bpmText.text = value.ToString();
		}
	}

	// Token: 0x170003D4 RID: 980
	// (set) Token: 0x0600143F RID: 5183 RVA: 0x0000F492 File Offset: 0x0000D692
	public int notesCount
	{
		set
		{
			this._notesCountText.text = value.ToString();
		}
	}

	// Token: 0x170003D5 RID: 981
	// (set) Token: 0x06001440 RID: 5184 RVA: 0x0000F4A6 File Offset: 0x0000D6A6
	public int obstaclesCount
	{
		set
		{
			this._obstaclesCountText.text = value.ToString();
		}
	}

	// Token: 0x170003D6 RID: 982
	// (set) Token: 0x06001441 RID: 5185 RVA: 0x0000F4BA File Offset: 0x0000D6BA
	public int bombsCount
	{
		set
		{
			this._bombsCountText.text = value.ToString();
		}
	}

	// Token: 0x040013EC RID: 5100
	[SerializeField]
	private TextMeshProUGUI _durationText;

	// Token: 0x040013ED RID: 5101
	[SerializeField]
	private TextMeshProUGUI _notesPerSecondText;

	// Token: 0x040013EE RID: 5102
	[SerializeField]
	private TextMeshProUGUI _bpmText;

	// Token: 0x040013EF RID: 5103
	[SerializeField]
	private TextMeshProUGUI _notesCountText;

	// Token: 0x040013F0 RID: 5104
	[SerializeField]
	private TextMeshProUGUI _obstaclesCountText;

	// Token: 0x040013F1 RID: 5105
	[SerializeField]
	private TextMeshProUGUI _bombsCountText;
}
