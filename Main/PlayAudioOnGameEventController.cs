using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class PlayAudioOnGameEventController : MonoBehaviour
{
	// Token: 0x06000189 RID: 393 RVA: 0x00018490 File Offset: 0x00016690
	protected void Awake()
	{
		PlayAudioOnGameEventController.EventAudioBinding[] eventAudioBindings = this._eventAudioBindings;
		for (int i = 0; i < eventAudioBindings.Length; i++)
		{
			eventAudioBindings[i].Init(this._audioClipQueue);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x000184C0 File Offset: 0x000166C0
	protected void OnDestroy()
	{
		PlayAudioOnGameEventController.EventAudioBinding[] eventAudioBindings = this._eventAudioBindings;
		for (int i = 0; i < eventAudioBindings.Length; i++)
		{
			eventAudioBindings[i].Deinit();
		}
	}

	// Token: 0x0400017A RID: 378
	[SerializeField]
	private AudioClipQueue _audioClipQueue;

	// Token: 0x0400017B RID: 379
	[SerializeField]
	private PlayAudioOnGameEventController.EventAudioBinding[] _eventAudioBindings;

	// Token: 0x0200005A RID: 90
	[Serializable]
	private class EventAudioBinding
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000033FB File Offset: 0x000015FB
		public void Init(AudioClipQueue audioClipQueue)
		{
			this._audioClipQueue = audioClipQueue;
			this._randomObjectPicker = new RandomObjectPicker<AudioClip>(this._audioClips, 0.2f);
			this._signal.Subscribe(new Action(this.HandleGameEvent));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003431 File Offset: 0x00001631
		public void Deinit()
		{
			this._signal.Unsubscribe(new Action(this.HandleGameEvent));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000184EC File Offset: 0x000166EC
		private void HandleGameEvent()
		{
			AudioClip audioClip = this._randomObjectPicker.PickRandomObject();
			if (audioClip)
			{
				this._audioClipQueue.PlayAudioClipWithDelay(audioClip, this._delay);
			}
		}

		// Token: 0x0400017C RID: 380
		[Header("==================")]
		[SerializeField]
		private Signal _signal;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		private float _delay;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		private AudioClip[] _audioClips;

		// Token: 0x0400017F RID: 383
		private AudioClipQueue _audioClipQueue;

		// Token: 0x04000180 RID: 384
		private RandomObjectPicker<AudioClip> _randomObjectPicker;
	}
}
