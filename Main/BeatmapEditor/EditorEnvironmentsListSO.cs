using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200052E RID: 1326
	public class EditorEnvironmentsListSO : PersistentScriptableObject
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x00012E8D File Offset: 0x0001108D
		public EnvironmentInfoSO[] environmentInfos
		{
			get
			{
				return this._environmentInfos;
			}
		}

		// Token: 0x04001892 RID: 6290
		[SerializeField]
		private EnvironmentInfoSO[] _environmentInfos;
	}
}
