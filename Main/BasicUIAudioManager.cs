using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class BasicUIAudioManager : MonoBehaviour
{
	// Token: 0x06000E80 RID: 3712 RVA: 0x0000B20A File Offset: 0x0000940A
	protected void Start()
	{
		this._randomSoundPicker = new RandomObjectPicker<AudioClip>(this._clickSounds, 0.07f);
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0003B6C4 File Offset: 0x000398C4
	protected void OnEnable()
	{
		for (int i = 0; i < this._buttonClickEvents.Length; i++)
		{
			this._buttonClickEvents[i].Subscribe(new Action(this.HandleButtonClickEvent));
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0003B700 File Offset: 0x00039900
	protected void OnDisable()
	{
		for (int i = 0; i < this._buttonClickEvents.Length; i++)
		{
			this._buttonClickEvents[i].Unsubscribe(new Action(this.HandleButtonClickEvent));
		}
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0003B73C File Offset: 0x0003993C
	private void HandleButtonClickEvent()
	{
		AudioClip audioClip = this._randomSoundPicker.PickRandomObject();
		if (audioClip)
		{
			this._audioSource.pitch = UnityEngine.Random.Range(this._minPitch, this._maxPitch);
			this._audioSource.PlayOneShot(audioClip, 1f);
		}
	}

	// Token: 0x04000EF6 RID: 3830
	[SerializeField]
	private Signal[] _buttonClickEvents;

	// Token: 0x04000EF7 RID: 3831
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000EF8 RID: 3832
	[SerializeField]
	private AudioClip[] _clickSounds;

	// Token: 0x04000EF9 RID: 3833
	[SerializeField]
	private float _minPitch = 1f;

	// Token: 0x04000EFA RID: 3834
	[SerializeField]
	private float _maxPitch = 1f;

	// Token: 0x04000EFB RID: 3835
	private RandomObjectPicker<AudioClip> _randomSoundPicker;
}
