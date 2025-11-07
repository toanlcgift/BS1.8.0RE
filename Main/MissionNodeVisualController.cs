using System;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class MissionNodeVisualController : MonoBehaviour
{
	// Token: 0x1400008D RID: 141
	// (add) Token: 0x060010F9 RID: 4345 RVA: 0x00041A08 File Offset: 0x0003FC08
	// (remove) Token: 0x060010FA RID: 4346 RVA: 0x00041A40 File Offset: 0x0003FC40
	public event Action<MissionNodeVisualController> nodeWasSelectEvent;

	// Token: 0x1400008E RID: 142
	// (add) Token: 0x060010FB RID: 4347 RVA: 0x00041A78 File Offset: 0x0003FC78
	// (remove) Token: 0x060010FC RID: 4348 RVA: 0x00041AB0 File Offset: 0x0003FCB0
	public event Action<MissionNodeVisualController> nodeWasDisplayedEvent;

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x060010FD RID: 4349 RVA: 0x0000CDB6 File Offset: 0x0000AFB6
	public MissionNode missionNode
	{
		get
		{
			return this._missionNode;
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x060010FE RID: 4350 RVA: 0x0000CDBE File Offset: 0x0000AFBE
	public bool selected
	{
		get
		{
			return this._selected;
		}
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x060010FF RID: 4351 RVA: 0x0000CDC6 File Offset: 0x0000AFC6
	public bool isInitialized
	{
		get
		{
			return this._isInitialized;
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06001100 RID: 4352 RVA: 0x0000CDCE File Offset: 0x0000AFCE
	public bool cleared
	{
		get
		{
			return this._cleared;
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06001101 RID: 4353 RVA: 0x0000CDD6 File Offset: 0x0000AFD6
	public bool interactable
	{
		get
		{
			return this._interactable;
		}
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0000CDDE File Offset: 0x0000AFDE
	public void SetSelected(bool value)
	{
		this._selected = value;
		this._missionToggle.selected = this._selected;
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
	protected void OnEnable()
	{
		Action<MissionNodeVisualController> action = this.nodeWasDisplayedEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0000CE0B File Offset: 0x0000B00B
	protected void Awake()
	{
		this._missionToggle.selectionDidChangeEvent += this.HandleMissionToggleSelectionDidChange;
		this._missionToggle.interactable = true;
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0000CE30 File Offset: 0x0000B030
	protected void Start()
	{
		this.Init();
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0000CE38 File Offset: 0x0000B038
	public void Reset()
	{
		this._isInitialized = false;
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0000CE41 File Offset: 0x0000B041
	protected void OnDestroy()
	{
		if (this._missionToggle)
		{
			this._missionToggle.selectionDidChangeEvent -= this.HandleMissionToggleSelectionDidChange;
		}
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0000CE67 File Offset: 0x0000B067
	public void Init()
	{
		this._missionToggle.SetText(this._missionNode.formattedMissionNodeName);
		this.SetupToggle();
		this.ChangeNodeSelection(this.selected);
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0000CE91 File Offset: 0x0000B091
	public void Setup(bool cleared, bool interactable)
	{
		this._cleared = cleared;
		this._interactable = interactable;
		this._isInitialized = true;
		this.SetupToggle();
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0000CEAE File Offset: 0x0000B0AE
	private void SetupToggle()
	{
		this._missionToggle.missionCleared = this._cleared;
		this._missionToggle.interactable = this._interactable;
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0000CED2 File Offset: 0x0000B0D2
	public void SetMissionCleared()
	{
		this._cleared = true;
		this.SetupToggle();
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0000CEE1 File Offset: 0x0000B0E1
	public void SetInteractable()
	{
		this._interactable = true;
		this.SetupToggle();
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
	public void ChangeNodeSelection(bool selected)
	{
		this._missionToggle.ChangeSelection(selected, false, false);
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0000CF00 File Offset: 0x0000B100
	private void HandleMissionToggleSelectionDidChange(MissionToggle toggle)
	{
		if (toggle.selected)
		{
			Action<MissionNodeVisualController> action = this.nodeWasSelectEvent;
			if (action == null)
			{
				return;
			}
			action(this);
		}
	}

	// Token: 0x04001103 RID: 4355
	[SerializeField]
	private MissionToggle _missionToggle;

	// Token: 0x04001104 RID: 4356
	[SerializeField]
	private MissionNode _missionNode;

	// Token: 0x04001107 RID: 4359
	private bool _selected;

	// Token: 0x04001108 RID: 4360
	private bool _isInitialized;

	// Token: 0x04001109 RID: 4361
	private bool _cleared;

	// Token: 0x0400110A RID: 4362
	private bool _interactable;
}
