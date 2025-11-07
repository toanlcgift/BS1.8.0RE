using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000590 RID: 1424
	public class SongParamsPanelController : MonoBehaviour
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
		protected void Awake()
		{
			this._binder = new InputFieldDataBinder();
			InputFieldDataBinder binder = this._binder;
			List<Tuple<InputField, FloatSO, Func<string, float>, Func<float, string>>> list = new List<Tuple<InputField, FloatSO, Func<string, float>, Func<float, string>>>();
			list.Add(this._beatsPerMinuteInputField, this._beatsPerMinute, (string s) => this.ConvertAndClamp(s, this._beatsPerMinute, 30f, 300f, 1f), (float f) => f.ToString());
			list.Add(this._songTimeOffsetInputField, this._songTimeOffset, (string s) => this.ConvertAndClamp(s, this._songTimeOffset, 0f, 10000f, 0.001f), (float f) => (f * 1000f).ToString());
			list.Add(this._shuffleStrengthInputField, this._shuffleStrength, (string s) => this.ConvertAndClamp(s, this._shuffleStrength, -1000f, 1000f, 0.001f), (float f) => Mathf.FloorToInt(f * 1000f + 0.5f).ToString());
			list.Add(this._previewStartTimeInputField, this._previewStartTime, (string s) => this.ConvertAndClamp(s, this._previewStartTime, 0f, 10000f, 1f), (float f) => f.ToString());
			list.Add(this._previewDurationInputField, this._previewDuration, (string s) => this.ConvertAndClamp(s, this._previewDuration, 0f, 10000f, 1f), (float f) => f.ToString());
			binder.AddBindings<FloatSO, float>(list);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0005F71C File Offset: 0x0005D91C
		private float ConvertAndClamp(string s, float originalValue, float min, float max, float factor)
		{
			float value;
			if (float.TryParse(s, out value))
			{
				return Mathf.Clamp(value, min, max) * factor;
			}
			return originalValue;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000149B4 File Offset: 0x00012BB4
		protected void OnDestroy()
		{
			if (this._binder != null)
			{
				this._binder.ClearBindings();
			}
		}

		// Token: 0x04001A5B RID: 6747
		[SerializeField]
		private FloatSO _beatsPerMinute;

		// Token: 0x04001A5C RID: 6748
		[SerializeField]
		private FloatSO _songTimeOffset;

		// Token: 0x04001A5D RID: 6749
		[SerializeField]
		private FloatSO _shuffleStrength;

		// Token: 0x04001A5E RID: 6750
		[SerializeField]
		private FloatSO _previewStartTime;

		// Token: 0x04001A5F RID: 6751
		[SerializeField]
		private FloatSO _previewDuration;

		// Token: 0x04001A60 RID: 6752
		[Space]
		[SerializeField]
		private InputField _beatsPerMinuteInputField;

		// Token: 0x04001A61 RID: 6753
		[SerializeField]
		private InputField _songTimeOffsetInputField;

		// Token: 0x04001A62 RID: 6754
		[SerializeField]
		private InputField _shuffleStrengthInputField;

		// Token: 0x04001A63 RID: 6755
		[SerializeField]
		private InputField _previewStartTimeInputField;

		// Token: 0x04001A64 RID: 6756
		[SerializeField]
		private InputField _previewDurationInputField;

		// Token: 0x04001A65 RID: 6757
		private InputFieldDataBinder _binder;
	}
}
