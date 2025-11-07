using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000550 RID: 1360
	public class BasicLevelInfoController : MonoBehaviour
	{
		// Token: 0x06001A44 RID: 6724 RVA: 0x0005B9F4 File Offset: 0x00059BF4
		protected void Awake()
		{
			this._binder = new InputFieldDataBinder();
			this._binder.AddStringBindings<StringSO>(new List<Tuple<InputField, StringSO>>
			{
				{
					this._songNameInputField,
					this._songName
				},
				{
					this._songSubNameInputField,
					this._songSubName
				},
				{
					this._songAuthorInputField,
					this._songAuthor
				},
				{
					this._beatmapAuthorInputField,
					this._beatmapAuthor
				}
			});
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0001371E File Offset: 0x0001191E
		protected void OnDestroy()
		{
			if (this._binder != null)
			{
				this._binder.ClearBindings();
			}
		}

		// Token: 0x0400193D RID: 6461
		[SerializeField]
		private StringSO _songName;

		// Token: 0x0400193E RID: 6462
		[SerializeField]
		private StringSO _songSubName;

		// Token: 0x0400193F RID: 6463
		[SerializeField]
		private StringSO _songAuthor;

		// Token: 0x04001940 RID: 6464
		[SerializeField]
		private StringSO _beatmapAuthor;

		// Token: 0x04001941 RID: 6465
		[SerializeField]
		private InputField _songNameInputField;

		// Token: 0x04001942 RID: 6466
		[SerializeField]
		private InputField _songSubNameInputField;

		// Token: 0x04001943 RID: 6467
		[SerializeField]
		private InputField _songAuthorInputField;

		// Token: 0x04001944 RID: 6468
		[SerializeField]
		private InputField _beatmapAuthorInputField;

		// Token: 0x04001945 RID: 6469
		private InputFieldDataBinder _binder;
	}
}
