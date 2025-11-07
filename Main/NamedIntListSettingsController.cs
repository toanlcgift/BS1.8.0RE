using System;
using Polyglot;
using UnityEngine;

// Token: 0x020003B5 RID: 949
public class NamedIntListSettingsController : ListSettingsController
{
	// Token: 0x06001172 RID: 4466 RVA: 0x000426A8 File Offset: 0x000408A8
	protected override bool GetInitValues(out int idx, out int numberOfElements)
	{
		numberOfElements = this._textValuePairs.Length;
		idx = numberOfElements - 1;
		for (int i = 0; i < this._textValuePairs.Length; i++)
		{
			if (this._settingsValue == this._textValuePairs[i].value)
			{
				idx = i;
				return true;
			}
		}
		return true;
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x0000D415 File Offset: 0x0000B615
	protected override void ApplyValue(int idx)
	{
		this._settingsValue.value = this._textValuePairs[idx].value;
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0000D42F File Offset: 0x0000B62F
	protected override string TextForValue(int idx)
	{
		return this._textValuePairs[idx].localizedText;
	}

	// Token: 0x0400114F RID: 4431
	[SerializeField]
	private IntSO _settingsValue;

	// Token: 0x04001150 RID: 4432
	[SerializeField]
	private NamedIntListSettingsController.TextValuePair[] _textValuePairs;

	// Token: 0x020003B6 RID: 950
	[Serializable]
	public class TextValuePair
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0000D446 File Offset: 0x0000B646
		public string localizedText
		{
			get
			{
				return Localization.Get(this.text);
			}
		}

		// Token: 0x04001151 RID: 4433
		public string text;

		// Token: 0x04001152 RID: 4434
		public int value;
	}
}
