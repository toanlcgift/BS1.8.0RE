using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x020002E1 RID: 737
public class MultiplierValuesRecorder : MonoBehaviour
{
	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00009C45 File Offset: 0x00007E45
	public List<MultiplierValuesRecorder.MultiplierValue> multiplierValues
	{
		get
		{
			return this._multiplierValues;
		}
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00009C4D File Offset: 0x00007E4D
	protected void Start()
	{
		this._scoreController.multiplierDidChangeEvent += this.HandleScoreControllerMultiplierDidChange;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00009C66 File Offset: 0x00007E66
	protected void OnDestroy()
	{
		this._scoreController.multiplierDidChangeEvent -= this.HandleScoreControllerMultiplierDidChange;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00036B30 File Offset: 0x00034D30
	private void HandleScoreControllerMultiplierDidChange(int multiplier, float multiplierProgress)
	{
		if (this._multiplierValues.Count > 0 && this._multiplierValues[this._multiplierValues.Count - 1].multiplier == multiplier)
		{
			return;
		}
		MultiplierValuesRecorder.MultiplierValue item = new MultiplierValuesRecorder.MultiplierValue(multiplier, this._audioTimeSyncController.songTime);
		this._multiplierValues.Add(item);
	}

	// Token: 0x04000D07 RID: 3335
	[SerializeField]
	private ScoreController _scoreController;

	// Token: 0x04000D08 RID: 3336
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000D09 RID: 3337
	private List<MultiplierValuesRecorder.MultiplierValue> _multiplierValues = new List<MultiplierValuesRecorder.MultiplierValue>(1000);

	// Token: 0x020002E2 RID: 738
	public struct MultiplierValue
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00009C97 File Offset: 0x00007E97
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x00009C9F File Offset: 0x00007E9F
		public int multiplier { get; private set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00009CA8 File Offset: 0x00007EA8
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x00009CB0 File Offset: 0x00007EB0
		public float time { get; private set; }

		// Token: 0x06000C8F RID: 3215 RVA: 0x00009CB9 File Offset: 0x00007EB9
		public MultiplierValue(int multiplier, float time)
		{
			this.multiplier = multiplier;
			this.time = time;
		}
	}
}
