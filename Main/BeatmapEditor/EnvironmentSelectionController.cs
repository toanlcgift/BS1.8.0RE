using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000561 RID: 1377
	public class EnvironmentSelectionController : MonoBehaviour
	{
		// Token: 0x06001ACB RID: 6859 RVA: 0x00013D22 File Offset: 0x00011F22
		protected void Awake()
		{
			this._environment.didChangeEvent += this.HandleEnvironmentDidChange;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0005D150 File Offset: 0x0005B350
		protected void Start()
		{
			this._dropdown.onValueChanged.AddListener(new UnityAction<int>(this.HandleCharacteristicDropdownValueChanged));
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			foreach (EnvironmentInfoSO environmentInfoSO in this._environmentList.environmentInfos)
			{
				list.Add(new Dropdown.OptionData(environmentInfoSO.environmentName));
			}
			this._dropdown.AddOptions(list);
			this.RefreshUI();
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0005D1C0 File Offset: 0x0005B3C0
		protected void OnDestroy()
		{
			if (this._dropdown != null)
			{
				this._dropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.HandleCharacteristicDropdownValueChanged));
			}
			this._environment.didChangeEvent -= this.HandleEnvironmentDidChange;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00013D3B File Offset: 0x00011F3B
		private void HandleCharacteristicDropdownValueChanged(int value)
		{
			this._environment.SetValues(this._environmentList.environmentInfos[value]);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0005D210 File Offset: 0x0005B410
		private void RefreshUI()
		{
			for (int i = 0; i < this._dropdown.options.Count; i++)
			{
				if (this._dropdown.options[i].text == this._environment.value.environmentName)
				{
					this._dropdown.value = i;
					return;
				}
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00013D55 File Offset: 0x00011F55
		private void HandleEnvironmentDidChange()
		{
			this.RefreshUI();
		}

		// Token: 0x040019A1 RID: 6561
		[SerializeField]
		private EditorEnvironmentSO _environment;

		// Token: 0x040019A2 RID: 6562
		[SerializeField]
		private Dropdown _dropdown;

		// Token: 0x040019A3 RID: 6563
		[SerializeField]
		private EditorEnvironmentsListSO _environmentList;
	}
}
