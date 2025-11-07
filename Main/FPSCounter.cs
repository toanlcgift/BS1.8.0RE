using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class FPSCounter : MonoBehaviour
{
	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0000B034 File Offset: 0x00009234
	// (set) Token: 0x06000E49 RID: 3657 RVA: 0x0000B03C File Offset: 0x0000923C
	public int currentFPS { get; private set; }

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0000B045 File Offset: 0x00009245
	// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0000B04D File Offset: 0x0000924D
	public int lowestFPS { get; private set; }

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0000B056 File Offset: 0x00009256
	// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0000B05E File Offset: 0x0000925E
	public int highestFPS { get; private set; }

	// Token: 0x06000E4E RID: 3662 RVA: 0x0000B067 File Offset: 0x00009267
	protected void Awake()
	{
		this.lowestFPS = 999;
		this.highestFPS = 0;
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0003AAC8 File Offset: 0x00038CC8
	protected void Update()
	{
		this._timeBuffer += Time.unscaledDeltaTime;
		this._frameCounter++;
		if (this._timeBuffer >= 1f)
		{
			this.currentFPS = this._frameCounter;
			this.lowestFPS = Mathf.Min(this.currentFPS, this.lowestFPS);
			this.highestFPS = Mathf.Max(this.currentFPS, this.highestFPS);
			this._timeBuffer -= Mathf.Floor(this._timeBuffer);
			this._frameCounter = 0;
		}
	}

	// Token: 0x04000EAC RID: 3756
	private float _timeBuffer;

	// Token: 0x04000EAD RID: 3757
	private int _frameCounter;
}
