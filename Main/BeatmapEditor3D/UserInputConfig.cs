using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000506 RID: 1286
	[Serializable]
	public class UserInputConfig
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00011EA1 File Offset: 0x000100A1
		public UserInputKeyBinding keyBinding
		{
			get
			{
				return this._keyBindingSO.keyBinding;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00011EAE File Offset: 0x000100AE
		public UserInputSensitivityConfig sensitivityConfig
		{
			get
			{
				return this._sensitivityConfigSO.sensitivityConfig;
			}
		}

		// Token: 0x040017CC RID: 6092
		[SerializeField]
		private UserInputKeyBindingSO _keyBindingSO;

		// Token: 0x040017CD RID: 6093
		[SerializeField]
		private UserInputSensitivityConfigSO _sensitivityConfigSO;
	}
}
