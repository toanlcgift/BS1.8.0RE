using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200052D RID: 1325
	public class EditorEnvironmentSO : ObservableVariableSO<EnvironmentInfoSO>
	{
		// Token: 0x06001977 RID: 6519 RVA: 0x00012E6E File Offset: 0x0001106E
		public void SetDefaults()
		{
			base.value = this._defaultEnvironmentInfo;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00012E7C File Offset: 0x0001107C
		public void SetValues(EnvironmentInfoSO environmentInfo)
		{
			base.value = environmentInfo;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005979C File Offset: 0x0005799C
		public void SetValues(string environmentSerializedName)
		{
			foreach (EnvironmentInfoSO environmentInfoSO in this._environmentList.environmentInfos)
			{
				if (environmentInfoSO.serializedName == environmentSerializedName)
				{
					base.value = environmentInfoSO;
					return;
				}
			}
			base.value = this._defaultEnvironmentInfo;
		}

		// Token: 0x04001890 RID: 6288
		[SerializeField]
		private EnvironmentInfoSO _defaultEnvironmentInfo;

		// Token: 0x04001891 RID: 6289
		[SerializeField]
		private EditorEnvironmentsListSO _environmentList;
	}
}
