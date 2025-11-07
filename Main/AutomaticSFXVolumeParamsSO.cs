using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class AutomaticSFXVolumeParamsSO : ScriptableObject
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000147 RID: 327 RVA: 0x0000303B File Offset: 0x0000123B
	public float musicVolumeMultiplier
	{
		get
		{
			return this._musicVolumeMultiplier;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000148 RID: 328 RVA: 0x00003043 File Offset: 0x00001243
	public float threshold
	{
		get
		{
			return this._threshold;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000149 RID: 329 RVA: 0x0000304B File Offset: 0x0000124B
	public float impact
	{
		get
		{
			return this._impact;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600014A RID: 330 RVA: 0x00003053 File Offset: 0x00001253
	public float attackTime
	{
		get
		{
			return this._attackTime;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600014B RID: 331 RVA: 0x0000305B File Offset: 0x0000125B
	public float releaseTime
	{
		get
		{
			return this._releaseTime;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600014C RID: 332 RVA: 0x00003063 File Offset: 0x00001263
	public float minVolume
	{
		get
		{
			return this._minVolume;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600014D RID: 333 RVA: 0x0000306B File Offset: 0x0000126B
	public float maxVolume
	{
		get
		{
			return this._maxVolume;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600014E RID: 334 RVA: 0x00003073 File Offset: 0x00001273
	public float volumeSmooth
	{
		get
		{
			return this._volumeSmooth;
		}
	}

	// Token: 0x04000126 RID: 294
	[SerializeField]
	private float _musicVolumeMultiplier = 1f;

	// Token: 0x04000127 RID: 295
	[SerializeField]
	private float _threshold = 0.3f;

	// Token: 0x04000128 RID: 296
	[SerializeField]
	private float _impact = 16f;

	// Token: 0x04000129 RID: 297
	[SerializeField]
	private float _attackTime = 1f;

	// Token: 0x0400012A RID: 298
	[SerializeField]
	private float _releaseTime = 2f;

	// Token: 0x0400012B RID: 299
	[SerializeField]
	private float _minVolume = -9f;

	// Token: 0x0400012C RID: 300
	[SerializeField]
	private float _maxVolume = 20f;

	// Token: 0x0400012D RID: 301
	[SerializeField]
	private float _volumeSmooth = 4f;
}
