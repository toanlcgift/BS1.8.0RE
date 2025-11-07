using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020002B6 RID: 694
public class SongProgressUIController : MonoBehaviour
{
	// Token: 0x06000BBF RID: 3007 RVA: 0x000351C0 File Offset: 0x000333C0
	protected void Start()
	{
		float songLength = this._audioTimeSyncController.songLength;
		this._durationMinutesText.text = songLength.Minutes().ToString("0");
		this._durationSecondsText.text = songLength.Seconds().ToString("00");
		this._stringBuilder = new StringBuilder(4);
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00035224 File Offset: 0x00033424
	protected void Update()
	{
		float songTime = this._audioTimeSyncController.songTime;
		int num = songTime.Minutes();
		int num2 = songTime.Seconds();
		if (this._prevMinutes != num)
		{
			this._progressMinutesText.text = num.ToString();
			this._prevMinutes = num;
		}
		if (this._prevSeconds != num2)
		{
			this._stringBuilder.Remove(0, this._stringBuilder.Length);
			if (num2 < 10)
			{
				this._stringBuilder.Append('0');
			}
			this._stringBuilder.AppendNumber(num2);
			this._progressSecondsText.text = this._stringBuilder.ToString();
			this._prevSeconds = num2;
			float num3 = this._audioTimeSyncController.songTime / this._audioTimeSyncController.songLength;
			this._slider.value = num3;
			this._progressImage.fillAmount = num3;
		}
	}

	// Token: 0x04000C6B RID: 3179
	[SerializeField]
	private Slider _slider;

	// Token: 0x04000C6C RID: 3180
	[SerializeField]
	private Image _progressImage;

	// Token: 0x04000C6D RID: 3181
	[SerializeField]
	private TextMeshProUGUI _durationMinutesText;

	// Token: 0x04000C6E RID: 3182
	[SerializeField]
	private TextMeshProUGUI _durationSecondsText;

	// Token: 0x04000C6F RID: 3183
	[SerializeField]
	private TextMeshProUGUI _progressMinutesText;

	// Token: 0x04000C70 RID: 3184
	[SerializeField]
	private TextMeshProUGUI _progressSecondsText;

	// Token: 0x04000C71 RID: 3185
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000C72 RID: 3186
	private int _prevMinutes = -1;

	// Token: 0x04000C73 RID: 3187
	private int _prevSeconds = -1;

	// Token: 0x04000C74 RID: 3188
	private StringBuilder _stringBuilder;
}
