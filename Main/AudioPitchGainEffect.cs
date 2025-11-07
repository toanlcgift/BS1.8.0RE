using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class AudioPitchGainEffect : MonoBehaviour
{
	// Token: 0x06000122 RID: 290 RVA: 0x00002E04 File Offset: 0x00001004
	private IEnumerator StartEffectCoroutine(float volumeScale, Action finishCallback)
	{
		float startPitch = this._audioSource.pitch;
		float time = 0f;
		while (time < this._duration)
		{
			float time2 = time / this._duration;
			this._audioSource.pitch = startPitch * this._pitchCurve.Evaluate(time2);
			this._audioSource.volume = this._gainCurve.Evaluate(time2) * volumeScale;
			time += Time.deltaTime;
			yield return null;
		}
		this._audioSource.pitch = startPitch * this._pitchCurve.Evaluate(1f);
		this._audioSource.volume = this._gainCurve.Evaluate(1f) * volumeScale;
		if (finishCallback != null)
		{
			finishCallback();
		}
		yield break;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00002E21 File Offset: 0x00001021
	public void StartEffect(float volumeScale, Action finishCallback)
	{
		base.StartCoroutine(this.StartEffectCoroutine(volumeScale, finishCallback));
	}

	// Token: 0x040000F4 RID: 244
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x040000F5 RID: 245
	[SerializeField]
	private float _duration = 0.3f;

	// Token: 0x040000F6 RID: 246
	[SerializeField]
	private AnimationCurve _pitchCurve;

	// Token: 0x040000F7 RID: 247
	[SerializeField]
	private AnimationCurve _gainCurve;
}
