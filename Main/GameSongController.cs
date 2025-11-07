using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class GameSongController : SongController
{
	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0000A994 File Offset: 0x00008B94
	public float songLength
	{
		get
		{
			return this._audioTimeSyncController.songLength;
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0000A9A1 File Offset: 0x00008BA1
	protected void LateUpdate()
	{
		if (!this._songDidFinish && this._audioTimeSyncController.songTime >= this._audioTimeSyncController.songEndTime - 0.2f)
		{
			this._songDidFinish = true;
			base.SendSongDidFinishEvent();
		}
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x0000A9D6 File Offset: 0x00008BD6
	public override void StartSong()
	{
		this._songDidFinish = false;
		base.StartCoroutine(this.StartSongCoroutine());
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0000A9EC File Offset: 0x00008BEC
	private IEnumerator StartSongCoroutine()
	{
		WaitUntil waitUntilAudioIsLoaded = this._audioTimeSyncController.waitUntilAudioIsLoaded;
		yield return waitUntilAudioIsLoaded;
		this._audioTimeSyncController.StartSong();
		yield break;
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0000A9FB File Offset: 0x00008BFB
	public override void StopSong()
	{
		base.StopAllCoroutines();
		this._audioTimeSyncController.StopSong();
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0000AA0E File Offset: 0x00008C0E
	public override void PauseSong()
	{
		base.StopAllCoroutines();
		this._audioTimeSyncController.Pause();
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0000AA21 File Offset: 0x00008C21
	public override void ResumeSong()
	{
		this._audioTimeSyncController.Resume();
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00039854 File Offset: 0x00037A54
	public void FailStopSong()
	{
		for (int i = 0; i < 16; i++)
		{
			BeatmapEventData beatmapEventData = new BeatmapEventData(0f, (BeatmapEventType)i, -1);
			this._beatmapObjectCallbackController.SendBeatmapEventDidTriggerEvent(beatmapEventData);
		}
		this._beatmapObjectCallbackController.enabled = false;
		this._audioTimeSyncController.forcedNoAudioSync = true;
		this._failAudioPitchGainEffect.StartEffect(1f, delegate
		{
			this._audioTimeSyncController.StopSong();
		});
	}

	// Token: 0x04000E1F RID: 3615
	[SerializeField]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000E20 RID: 3616
	[SerializeField]
	private AudioPitchGainEffect _failAudioPitchGainEffect;

	// Token: 0x04000E21 RID: 3617
	[SerializeField]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000E22 RID: 3618
	private bool _songDidFinish;
}
