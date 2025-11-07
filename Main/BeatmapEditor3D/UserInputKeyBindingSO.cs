using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000509 RID: 1289
	public class UserInputKeyBindingSO : PersistentScriptableObject
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00011EC3 File Offset: 0x000100C3
		public UserInputKeyBinding keyBinding
		{
			get
			{
				return this._keyBinding;
			}
		}

		// Token: 0x040017FA RID: 6138
		[SerializeField]
		private UserInputKeyBinding _keyBinding;
	}
}
