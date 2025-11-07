using System;
using UnityEngine;

// Token: 0x020003BB RID: 955
public class WindowResolutionSettingsController : ListSettingsController
{
	// Token: 0x0600118E RID: 4494 RVA: 0x0004285C File Offset: 0x00040A5C
	protected override bool GetInitValues(out int idx, out int numberOfElements)
	{
		this._windowResolutions = new Vector2Int[Screen.resolutions.Length + 1];
		idx = -1;
		numberOfElements = 0;
		for (int i = 0; i < Screen.resolutions.Length; i++)
		{
			int width = Screen.resolutions[i].width;
			int height = Screen.resolutions[i].height;
			if (numberOfElements == 0 || this._windowResolutions[numberOfElements - 1].x != width || this._windowResolutions[numberOfElements - 1].y != height)
			{
				this._windowResolutions[numberOfElements] = new Vector2Int(width, height);
				Vector2Int vector2Int = this._windowResolution;
				if (width == vector2Int.x && height == vector2Int.y)
				{
					idx = numberOfElements;
				}
				numberOfElements++;
			}
		}
		if (idx == -1)
		{
			idx = numberOfElements;
			this._windowResolutions[idx] = this._windowResolution;
			numberOfElements++;
		}
		return true;
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x0000D5C5 File Offset: 0x0000B7C5
	protected override void ApplyValue(int idx)
	{
		this._windowResolution.value = this._windowResolutions[idx];
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0000D5DE File Offset: 0x0000B7DE
	protected override string TextForValue(int idx)
	{
		return this._windowResolutions[idx].x + " x " + this._windowResolutions[idx].y;
	}

	// Token: 0x04001160 RID: 4448
	[SerializeField]
	private Vector2IntSO _windowResolution;

	// Token: 0x04001161 RID: 4449
	private Vector2Int[] _windowResolutions;
}
