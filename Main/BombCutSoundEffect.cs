using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class BombCutSoundEffect : MonoBehaviour
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000150 RID: 336 RVA: 0x000177EC File Offset: 0x000159EC
	// (remove) Token: 0x06000151 RID: 337 RVA: 0x00017824 File Offset: 0x00015A24
	public event Action<BombCutSoundEffect> didFinishEvent;

	// Token: 0x06000152 RID: 338 RVA: 0x0000307B File Offset: 0x0000127B
	public void Init(AudioClip audioClip, Saber saber, float volume)
	{
		base.enabled = true;
		this._audioSource.clip = audioClip;
		this._saber = saber;
		this._audioSource.volume = volume;
		this._audioSource.Play();
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000030AE File Offset: 0x000012AE
	protected void LateUpdate()
	{
		if (this._audioSource.timeSamples >= this._audioSource.clip.samples - 1)
		{
			this.StopPlayingAndFinish();
			return;
		}
		base.transform.position = this._saber.saberBladeTopPos;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000030EC File Offset: 0x000012EC
	private void StopPlayingAndFinish()
	{
		base.enabled = false;
		this._audioSource.Stop();
		Action<BombCutSoundEffect> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x0400012E RID: 302
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000130 RID: 304
	private Saber _saber;

	// Token: 0x0200004E RID: 78
	public class Pool : MemoryPoolWithActiveItems<BombCutSoundEffect>
	{
	}
}
