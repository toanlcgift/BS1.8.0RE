using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000345 RID: 837
public class VisualMetronome : MonoBehaviour
{
	// Token: 0x1700031E RID: 798
	// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0000B240 File Offset: 0x00009440
	public Color tickerColor
	{
		set
		{
			this._tickerImage.color = value;
		}
	}

	// Token: 0x1700031F RID: 799
	// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0000B24E File Offset: 0x0000944E
	public Color movingTickerColor
	{
		set
		{
			this._movingTickerImage.color = value;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0000B26C File Offset: 0x0000946C
	// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0000B25C File Offset: 0x0000945C
	public float zeroOffset
	{
		get
		{
			return this._zeroOffset;
		}
		set
		{
			this._zeroOffset = value;
			this._dontTickThisFrame = true;
		}
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0003B78C File Offset: 0x0003998C
	protected void Awake()
	{
		this._ticker.anchorMin = new Vector2(0.5f, 0.5f);
		this._ticker.anchorMax = new Vector2(0.5f, 0.5f);
		this._ticker.sizeDelta = this._normalTickerSize;
		this._movingTicker.anchorMin = new Vector2(0f, 0.5f);
		this._movingTicker.anchorMax = new Vector2(0f, 0.5f);
		this._movingTicker.sizeDelta = this._normalTickerSize;
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0000B274 File Offset: 0x00009474
	protected void OnEnable()
	{
		this._audioSource.Play();
		this._tickerImage.enabled = true;
		this._movingTickerImage.enabled = true;
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0000B299 File Offset: 0x00009499
	protected void OnDisable()
	{
		this._audioSource.Stop();
		this._tickerImage.enabled = false;
		this._movingTickerImage.enabled = false;
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0003B824 File Offset: 0x00039A24
	protected void Update()
	{
		float num = this._audioSource.time - this._zeroOffset;
		float num2 = num - Mathf.Floor(num / this._metronomeInterval) * this._metronomeInterval;
		if (num2 < this._prevAudioTime)
		{
			this._direction *= -1f;
			if (!this._dontTickThisFrame)
			{
				this._ticker.sizeDelta = this._tickTickerSize0;
			}
		}
		if (this._prevAudioTime <= 0.5f && num2 >= 0.5f)
		{
			if (!this._dontTickThisFrame)
			{
				this._ticker.sizeDelta = this._tickTickerSize1;
			}
		}
		else
		{
			this._ticker.sizeDelta = Vector2.Lerp(this._ticker.sizeDelta, this._normalTickerSize, Time.deltaTime * this._smooth);
		}
		float num3 = num2 / this._metronomeInterval;
		if (this._direction < 0f)
		{
			num3 = 1f - num3;
		}
		this.SetMovingTickerNormalizedPosition(num3);
		this._dontTickThisFrame = false;
		this._prevAudioTime = num2;
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0003B920 File Offset: 0x00039B20
	private void SetMovingTickerNormalizedPosition(float t)
	{
		this._movingTicker.anchoredPosition = new Vector2(t * (((RectTransform)base.transform).sizeDelta.x - (this._leftPadding + this._rightPadding)) + this._leftPadding, 0f);
	}

	// Token: 0x04000EFC RID: 3836
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000EFD RID: 3837
	[SerializeField]
	private float _leftPadding;

	// Token: 0x04000EFE RID: 3838
	[SerializeField]
	private float _rightPadding;

	// Token: 0x04000EFF RID: 3839
	[SerializeField]
	private RectTransform _ticker;

	// Token: 0x04000F00 RID: 3840
	[SerializeField]
	private RectTransform _movingTicker;

	// Token: 0x04000F01 RID: 3841
	[SerializeField]
	private Image _tickerImage;

	// Token: 0x04000F02 RID: 3842
	[SerializeField]
	private Image _movingTickerImage;

	// Token: 0x04000F03 RID: 3843
	[SerializeField]
	private float _metronomeInterval = 0.5f;

	// Token: 0x04000F04 RID: 3844
	[SerializeField]
	private Vector2 _normalTickerSize = new Vector2(1.2f, 3f);

	// Token: 0x04000F05 RID: 3845
	[SerializeField]
	private Vector2 _tickTickerSize0 = new Vector2(40f, 4f);

	// Token: 0x04000F06 RID: 3846
	[SerializeField]
	private Vector2 _tickTickerSize1 = new Vector2(0.8f, 5f);

	// Token: 0x04000F07 RID: 3847
	[SerializeField]
	private float _smooth = 16f;

	// Token: 0x04000F08 RID: 3848
	private float _prevAudioTime;

	// Token: 0x04000F09 RID: 3849
	private float _zeroOffset;

	// Token: 0x04000F0A RID: 3850
	private float _direction = 1f;

	// Token: 0x04000F0B RID: 3851
	private bool _dontTickThisFrame;
}
