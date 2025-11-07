using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000537 RID: 1335
	public class EditorStandardLevelInfoSO : PersistentScriptableObject
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00012F7A File Offset: 0x0001117A
		public string songName
		{
			get
			{
				return this._songName;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00012F87 File Offset: 0x00011187
		public string songSubName
		{
			get
			{
				return this._songSubName;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00012F94 File Offset: 0x00011194
		public string songAuthorName
		{
			get
			{
				return this._songAuthorName;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00012FA1 File Offset: 0x000111A1
		public string levelAuthorName
		{
			get
			{
				return this._levelAuthorName;
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00059AFC File Offset: 0x00057CFC
		public void SetDefaults()
		{
			this._songName.value = "No Name";
			this._songSubName.value = "No Name";
			this._songAuthorName.value = "No Name";
			this._levelAuthorName.value = "No Name";
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00012FAE File Offset: 0x000111AE
		public void SetValues(string songName, string songSubName, string songAuthorName, string levelAuthorName)
		{
			this._songName.value = songName;
			this._songSubName.value = songSubName;
			this._songAuthorName.value = songAuthorName;
			this._levelAuthorName.value = levelAuthorName;
		}

		// Token: 0x040018A9 RID: 6313
		[SerializeField]
		private StringSO _songName;

		// Token: 0x040018AA RID: 6314
		[SerializeField]
		private StringSO _songSubName;

		// Token: 0x040018AB RID: 6315
		[SerializeField]
		private StringSO _songAuthorName;

		// Token: 0x040018AC RID: 6316
		[SerializeField]
		private StringSO _levelAuthorName;
	}
}
