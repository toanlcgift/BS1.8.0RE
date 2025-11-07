using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x0200031C RID: 796
public class IntroTutorialRing : MonoBehaviour
{
	// Token: 0x1700030C RID: 780
	// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0000AB9E File Offset: 0x00008D9E
	public float alpha
	{
		set
		{
			this._canvasGroup.alpha = value;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0000ABAC File Offset: 0x00008DAC
	public bool fullyActivated
	{
		get
		{
			return this._highlighted && this._activationProgress == 1f;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0000ABC5 File Offset: 0x00008DC5
	// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0000ABCD File Offset: 0x00008DCD
	public SaberType saberType
	{
		get
		{
			return this._saberType;
		}
		set
		{
			this._saberType = value;
		}
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00039B74 File Offset: 0x00037D74
	protected void Start()
	{
		Image[] ringGLowImages = this._ringGLowImages;
		for (int i = 0; i < ringGLowImages.Length; i++)
		{
			ringGLowImages[i].color = this._colorManager.ColorForSaberType(this._saberType);
		}
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0000ABD6 File Offset: 0x00008DD6
	protected void OnEnable()
	{
		this._sabersInside.Clear();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00039BB0 File Offset: 0x00037DB0
	protected void Update()
	{
		bool flag = this._sabersInside.Contains(this._saberType);
		if (flag && !this._highlighted)
		{
			this._highlighted = true;
			this._emitNextParticleTimer = 0f;
		}
		else if (!flag && this._highlighted)
		{
			this._highlighted = false;
		}
		if (this._highlighted)
		{
			this._activationProgress = Mathf.Min(this._activationProgress + Time.deltaTime / this._activationDuration, 1f);
			this.SetProgressImagesfillAmount(this._activationProgress);
			if (this._emitNextParticleTimer <= 0f)
			{
				this._particleSystem.Emit(1);
				this._emitNextParticleTimer = 0.25f;
			}
			this._emitNextParticleTimer -= Time.deltaTime;
			return;
		}
		this._activationProgress = Mathf.Max(this._activationProgress - Time.deltaTime / this._activationDuration, 0f);
		this.SetProgressImagesfillAmount(this._activationProgress);
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00039CA0 File Offset: 0x00037EA0
	private void SetProgressImagesfillAmount(float fillAmount)
	{
		Image[] progressImages = this._progressImages;
		for (int i = 0; i < progressImages.Length; i++)
		{
			progressImages[i].fillAmount = fillAmount;
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00039CCC File Offset: 0x00037ECC
	private void OnTriggerEnter(Collider other)
	{
		if (LayerMasks.saberLayerMask.ContainsLayer(other.gameObject.layer))
		{
			Saber component = other.gameObject.GetComponent<Saber>();
			this._sabersInside.Add(component.saberType);
		}
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00039D10 File Offset: 0x00037F10
	private void OnTriggerExit(Collider other)
	{
		if (LayerMasks.saberLayerMask.ContainsLayer(other.gameObject.layer))
		{
			Saber component = other.gameObject.GetComponent<Saber>();
			this._sabersInside.Remove(component.saberType);
		}
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00039D54 File Offset: 0x00037F54
	private void OnTriggerStay(Collider other)
	{
		if (!this._sabersInsideAfterOnEnable)
		{
			return;
		}
		this._sabersInsideAfterOnEnable = false;
		if (LayerMasks.saberLayerMask.ContainsLayer(other.gameObject.layer))
		{
			Saber component = other.gameObject.GetComponent<Saber>();
			this._sabersInside.Add(component.saberType);
		}
	}

	// Token: 0x04000E38 RID: 3640
	[SerializeField]
	private Image[] _progressImages;

	// Token: 0x04000E39 RID: 3641
	[SerializeField]
	private SaberType _saberType;

	// Token: 0x04000E3A RID: 3642
	[SerializeField]
	private ParticleSystem _particleSystem;

	// Token: 0x04000E3B RID: 3643
	[SerializeField]
	private CanvasGroup _canvasGroup;

	// Token: 0x04000E3C RID: 3644
	[SerializeField]
	private float _activationDuration = 0.7f;

	// Token: 0x04000E3D RID: 3645
	[SerializeField]
	private Image[] _ringGLowImages;

	// Token: 0x04000E3E RID: 3646
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000E3F RID: 3647
	private bool _highlighted;

	// Token: 0x04000E40 RID: 3648
	private float _emitNextParticleTimer;

	// Token: 0x04000E41 RID: 3649
	private float _activationProgress;

	// Token: 0x04000E42 RID: 3650
	private HashSet<SaberType> _sabersInside = new HashSet<SaberType>();

	// Token: 0x04000E43 RID: 3651
	private bool _sabersInsideAfterOnEnable;
}
