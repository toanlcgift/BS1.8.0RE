using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200000C RID: 12
public class MainSelectionPanelController : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x00014CD0 File Offset: 0x00012ED0
	protected void Start()
	{
		this._togglePanelDict = new Dictionary<Toggle, GameObject>
		{
			{
				this._toolsToggle,
				this._toolsPanel
			},
			{
				this._editToggle,
				this._editPanel
			},
			{
				this._songParamsToggle,
				this._songParamsPanel
			},
			{
				this._projectImageToggle,
				this._projectImagePanel
			},
			{
				this._projectParamsToggle,
				this._projectParamsPanel
			},
			{
				this._filesToggle,
				this._filesPanel
			}
		};
		using (Dictionary<Toggle, GameObject>.KeyCollection.Enumerator enumerator = this._togglePanelDict.Keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Toggle toggle = enumerator.Current;
				UnityAction<bool> unityAction = delegate(bool isOn)
				{
					this.ToggleValueChanged(toggle);
				};
				toggle.onValueChanged.AddListener(unityAction);
				this._toggleOnValueChangedHandlers[toggle] = unityAction;
			}
		}
		this._toolsToggle.isOn = true;
		this.ActivatePanelWithToggle(this._toolsToggle);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00014DF4 File Offset: 0x00012FF4
	protected void OnDestroy()
	{
		foreach (KeyValuePair<Toggle, UnityAction<bool>> keyValuePair in this._toggleOnValueChangedHandlers)
		{
			if (keyValuePair.Key != null)
			{
				keyValuePair.Key.onValueChanged.RemoveListener(keyValuePair.Value);
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00014E68 File Offset: 0x00013068
	private void ActivatePanelWithToggle(Toggle toggle)
	{
		foreach (KeyValuePair<Toggle, GameObject> keyValuePair in this._togglePanelDict)
		{
			if (keyValuePair.Key == toggle)
			{
				keyValuePair.Value.SetActive(true);
			}
			else
			{
				keyValuePair.Value.SetActive(false);
			}
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002269 File Offset: 0x00000469
	public void ToggleValueChanged(Toggle toggle)
	{
		if (!toggle.isOn)
		{
			return;
		}
		this.ActivatePanelWithToggle(toggle);
	}

	// Token: 0x0400001C RID: 28
	[SerializeField]
	private Toggle _toolsToggle;

	// Token: 0x0400001D RID: 29
	[SerializeField]
	private Toggle _editToggle;

	// Token: 0x0400001E RID: 30
	[SerializeField]
	private Toggle _songParamsToggle;

	// Token: 0x0400001F RID: 31
	[SerializeField]
	private Toggle _projectImageToggle;

	// Token: 0x04000020 RID: 32
	[SerializeField]
	private Toggle _projectParamsToggle;

	// Token: 0x04000021 RID: 33
	[SerializeField]
	private Toggle _filesToggle;

	// Token: 0x04000022 RID: 34
	[Space]
	[SerializeField]
	private GameObject _toolsPanel;

	// Token: 0x04000023 RID: 35
	[SerializeField]
	private GameObject _editPanel;

	// Token: 0x04000024 RID: 36
	[SerializeField]
	private GameObject _songParamsPanel;

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private GameObject _projectImagePanel;

	// Token: 0x04000026 RID: 38
	[SerializeField]
	private GameObject _projectParamsPanel;

	// Token: 0x04000027 RID: 39
	[SerializeField]
	private GameObject _filesPanel;

	// Token: 0x04000028 RID: 40
	private Dictionary<Toggle, GameObject> _togglePanelDict;

	// Token: 0x04000029 RID: 41
	private Dictionary<Toggle, UnityAction<bool>> _toggleOnValueChangedHandlers = new Dictionary<Toggle, UnityAction<bool>>();
}
