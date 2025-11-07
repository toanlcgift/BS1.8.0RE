using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x0200050B RID: 1291
	public class UserInputSensitivityConfigSO : PersistentScriptableObject
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00011F0A File Offset: 0x0001010A
		public UserInputSensitivityConfig sensitivityConfig
		{
			get
			{
				return this._sensitivityConfig;
			}
		}

		// Token: 0x04001800 RID: 6144
		[SerializeField]
		private UserInputSensitivityConfig _sensitivityConfig;
	}
}
