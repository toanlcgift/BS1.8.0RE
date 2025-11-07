using System;

// Token: 0x020002E8 RID: 744
public class VariableBPMProcessor
{
	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00009DB4 File Offset: 0x00007FB4
	public float currentBPM
	{
		get
		{
			return this._currentBPM;
		}
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00009DBC File Offset: 0x00007FBC
	public void SetBPM(float newBPM)
	{
		this._currentBPM = newBPM;
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00009DC5 File Offset: 0x00007FC5
	public bool ProcessBeatmapEventData(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type.IsBPMChangeEvent())
		{
			this._currentBPM = (float)beatmapEventData.value;
			return true;
		}
		return false;
	}

	// Token: 0x04000D21 RID: 3361
	private float _currentBPM;
}
