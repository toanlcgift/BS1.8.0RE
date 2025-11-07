using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x0200026D RID: 621
public class FireworkItemController : MonoBehaviour
{
	// Token: 0x14000045 RID: 69
	// (add) Token: 0x06000A74 RID: 2676 RVA: 0x000310C0 File Offset: 0x0002F2C0
	// (remove) Token: 0x06000A75 RID: 2677 RVA: 0x000310F8 File Offset: 0x0002F2F8
	public event Action<FireworkItemController> didFinishEvent;

	// Token: 0x06000A76 RID: 2678 RVA: 0x00008272 File Offset: 0x00006472
	protected void Awake()
	{
		this._randomAudioPicker = new RandomObjectPicker<AudioClip>(this._explosionClips, 0.2f);
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0000828A File Offset: 0x0000648A
	protected void OnDisable()
	{
		this.SetLightsColor(this._lightIntensityCurve.Evaluate(1f));
		Action<FireworkItemController> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x000082B3 File Offset: 0x000064B3
	public void Fire()
	{
		base.StartCoroutine(this.FireCoroutine());
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x000082C2 File Offset: 0x000064C2
	private IEnumerator FireCoroutine()
	{
		float magnitude = base.transform.position.magnitude;
		float soundTimeToCenter = magnitude / 343f;
		this.SetLightsColor(0f);
		this._particleSystem.Emit(this._numberOfParticles);
		float elapsedTime = 0f;
		bool explosionSoundFired = false;
		while (elapsedTime < this._lightFlashDuration)
		{
			if (!explosionSoundFired && elapsedTime > soundTimeToCenter)
			{
				this.PlayExplosionSound();
				explosionSoundFired = true;
			}
			float time = elapsedTime / this._lightFlashDuration;
			float lightsColor = this._lightIntensityCurve.Evaluate(time);
			this.SetLightsColor(lightsColor);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.SetLightsColor(this._lightIntensityCurve.Evaluate(1f));
		Action<FireworkItemController> action = this.didFinishEvent;
		if (action != null)
		{
			action(this);
		}
		yield break;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00031130 File Offset: 0x0002F330
	private void SetLightsColor(float intensity)
	{
		Color color = this._lightsColor.ColorWithAlpha(intensity);
		for (int i = 0; i < this._lights.Length; i++)
		{
			this._lights[i].color = color;
		}
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0003116C File Offset: 0x0002F36C
	private void PlayExplosionSound()
	{
		AudioClip audioClip = this._randomAudioPicker.PickRandomObject();
		if (audioClip != null)
		{
			this._audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
			this._audioSource.PlayOneShot(audioClip);
		}
	}

	// Token: 0x04000ADA RID: 2778
	[SerializeField]
	private TubeBloomPrePassLight[] _lights;

	// Token: 0x04000ADB RID: 2779
	[SerializeField]
	private ParticleSystem _particleSystem;

	// Token: 0x04000ADC RID: 2780
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000ADD RID: 2781
	[Space]
	[SerializeField]
	private int _numberOfParticles = 70;

	// Token: 0x04000ADE RID: 2782
	[SerializeField]
	private float _lightFlashDuration = 1f;

	// Token: 0x04000ADF RID: 2783
	[SerializeField]
	private AnimationCurve _lightIntensityCurve;

	// Token: 0x04000AE0 RID: 2784
	[SerializeField]
	private Color _lightsColor;

	// Token: 0x04000AE1 RID: 2785
	[SerializeField]
	private AudioClip[] _explosionClips;

	// Token: 0x04000AE2 RID: 2786
	private RandomObjectPicker<AudioClip> _randomAudioPicker;

	// Token: 0x0200026E RID: 622
	public class Pool : MonoMemoryPool<FireworkItemController>
	{
	}
}
