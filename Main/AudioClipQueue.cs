using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class AudioClipQueue : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x00002CAB File Offset: 0x00000EAB
	protected void Awake()
	{
		this._audioSource.loop = false;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00016F38 File Offset: 0x00015138
	protected void Update()
	{
		if (this._delay > 0f)
		{
			this._delay -= Time.deltaTime;
			return;
		}
		if (this._queue.Count > 0 && !this._audioSource.isPlaying)
		{
			AudioClip clip = this._queue[0];
			this._queue.RemoveAt(0);
			this._audioSource.clip = clip;
			this._audioSource.Play();
			return;
		}
		if (this._queue.Count == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00002CB9 File Offset: 0x00000EB9
	public void PlayAudioClipWithDelay(AudioClip audioClip, float delay)
	{
		this._delay = Mathf.Max(this._delay, delay);
		this._queue.Add(audioClip);
		base.enabled = true;
	}

	// Token: 0x040000E2 RID: 226
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x040000E3 RID: 227
	private List<AudioClip> _queue = new List<AudioClip>();

	// Token: 0x040000E4 RID: 228
	private float _delay;
}
