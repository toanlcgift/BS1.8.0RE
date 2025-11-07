using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000044 RID: 68
public class AudioManagerSO : PersistentScriptableObject
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x0600011A RID: 282 RVA: 0x00002D56 File Offset: 0x00000F56
	public float sfxLatency
	{
		get
		{
			if (!(AudioSettings.GetSpatializerPluginName() == "MS HRTF Spatializer"))
			{
				return 0f;
			}
			return this._spatializerPluginLatancy;
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0001703C File Offset: 0x0001523C
	public void Init()
	{
		this._audioMixer.updateMode = AudioMixerUpdateMode.UnscaledTime;
		if (AudioSettings.GetSpatializerPluginName() == "MS HRTF Spatializer")
		{
			this._sfxVolumeOffset = this._spatializerSFXVolumeOffset;
		}
		else
		{
			this._sfxVolumeOffset = 0f;
		}
		this._audioMixer.GetFloat("SFXVolume", out this._sfxVolume);
	}

	// Token: 0x1700002A RID: 42
	// (set) Token: 0x0600011C RID: 284 RVA: 0x00002D75 File Offset: 0x00000F75
	public float mainVolume
	{
		set
		{
			this._audioMixer.SetFloat("MainVolume", value);
		}
	}

	// Token: 0x1700002B RID: 43
	// (set) Token: 0x0600011D RID: 285 RVA: 0x00002D89 File Offset: 0x00000F89
	public float sfxVolume
	{
		set
		{
			this._sfxVolume = value;
			if (this._sfxEnabled)
			{
				this._audioMixer.SetFloat("SFXVolume", this._sfxVolume + this._sfxVolumeOffset);
			}
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x0600011E RID: 286 RVA: 0x00002DB8 File Offset: 0x00000FB8
	// (set) Token: 0x0600011F RID: 287 RVA: 0x00002DC0 File Offset: 0x00000FC0
	public bool sfxEnabled
	{
		get
		{
			return this._sfxEnabled;
		}
		set
		{
			this._sfxEnabled = value;
			this.sfxVolume = (value ? this._sfxVolume : -100f);
		}
	}

	// Token: 0x1700002D RID: 45
	// (set) Token: 0x06000120 RID: 288 RVA: 0x00017098 File Offset: 0x00015298
	public float musicPitch
	{
		set
		{
			this._audioMixer.SetFloat("MusicPitch", value);
			if (Mathf.Approximately(value, 1f))
			{
				this._audioMixer.SetFloat("MusicPitchShifterWet", -100f);
				return;
			}
			this._audioMixer.SetFloat("MusicPitchShifterWet", 0f);
		}
	}

	// Token: 0x040000E9 RID: 233
	[SerializeField]
	private AudioMixer _audioMixer;

	// Token: 0x040000EA RID: 234
	[SerializeField]
	private float _spatializerPluginLatancy = 0.035f;

	// Token: 0x040000EB RID: 235
	[SerializeField]
	private float _spatializerSFXVolumeOffset = -2.5f;

	// Token: 0x040000EC RID: 236
	private const string kMSHRTFSpatializerPluginName = "MS HRTF Spatializer";

	// Token: 0x040000ED RID: 237
	private const string kSFXVolume = "SFXVolume";

	// Token: 0x040000EE RID: 238
	private const string kMainVolume = "MainVolume";

	// Token: 0x040000EF RID: 239
	private const string kMusicPitch = "MusicPitch";

	// Token: 0x040000F0 RID: 240
	private const string kMusicPitchShifterWet = "MusicPitchShifterWet";

	// Token: 0x040000F1 RID: 241
	private float _sfxVolumeOffset;

	// Token: 0x040000F2 RID: 242
	private float _sfxVolume;

	// Token: 0x040000F3 RID: 243
	private bool _sfxEnabled = true;
}
