using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000507 RID: 1287
	public class UserInputConfigSO : PersistentScriptableObject
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00011EBB File Offset: 0x000100BB
		public UserInputConfig userInputConfig
		{
			get
			{
				return this._userInputConfig;
			}
		}

		// Token: 0x040017CE RID: 6094
		[SerializeField]
		private UserInputConfig _userInputConfig;
	}
}
