using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200057C RID: 1404
	public class NoteTypeAndDirectionPanelController : MonoBehaviour
	{
		// Token: 0x06001B50 RID: 6992 RVA: 0x0005E690 File Offset: 0x0005C890
		protected void Awake()
		{
			this.RefreshNoteCutDirectionToggles();
			this.RefreshNoteTypeToggles();
			this._typeToggleBinder.AddBindings(new List<Tuple<Toggle, Action<bool>>>
			{
				{
					this._typeToggleNoteA,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteType.value = NoteType.NoteA;
						}
					}
				},
				{
					this._typeToggleNoteB,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteType.value = NoteType.NoteB;
						}
					}
				},
				{
					this._typeToggleBombNote,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteType.value = NoteType.Bomb;
						}
					}
				}
			});
			this._directionalToggleBinder.AddBindings(new List<Tuple<Toggle, Action<bool>>>
			{
				{
					this._directionToggleUp,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.Up;
						}
					}
				},
				{
					this._directionToggleUpLeft,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.UpLeft;
						}
					}
				},
				{
					this._directionToggleUpRight,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.UpRight;
						}
					}
				},
				{
					this._directionToggleLeft,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.Left;
						}
					}
				},
				{
					this._directionToggleRight,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.Right;
						}
					}
				},
				{
					this._directionToggleDown,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.Down;
						}
					}
				},
				{
					this._directionToggleDownLeft,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.DownLeft;
						}
					}
				},
				{
					this._directionToggleDownRight,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.DownRight;
						}
					}
				},
				{
					this._directionToggleAnyDirection,
					delegate(bool on)
					{
						if (on)
						{
							this._selectedNoteCutDirection.value = NoteCutDirection.Any;
						}
					}
				}
			});
			this._selectedNoteType.didChangeEvent += this.RefreshNoteTypeToggles;
			this._selectedNoteCutDirection.didChangeEvent += this.RefreshNoteCutDirectionToggles;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0005E818 File Offset: 0x0005CA18
		protected void OnDestroy()
		{
			if (this._directionalToggleBinder != null)
			{
				this._directionalToggleBinder.ClearBindings();
			}
			if (this._typeToggleBinder != null)
			{
				this._typeToggleBinder.ClearBindings();
			}
			this._selectedNoteType.didChangeEvent -= this.RefreshNoteTypeToggles;
			this._selectedNoteCutDirection.didChangeEvent -= this.RefreshNoteCutDirectionToggles;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0005E87C File Offset: 0x0005CA7C
		private void RefreshNoteCutDirectionToggles()
		{
			this._directionalToggleBinder.Disable();
			this._directionToggleUp.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.Up);
			this._directionToggleUpLeft.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.UpLeft);
			this._directionToggleUpRight.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.UpRight);
			this._directionToggleLeft.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.Left);
			this._directionToggleRight.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.Right);
			this._directionToggleDown.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.Down);
			this._directionToggleDownLeft.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.DownLeft);
			this._directionToggleDownRight.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.DownRight);
			this._directionToggleAnyDirection.isOn = (this._selectedNoteCutDirection.value == NoteCutDirection.Any);
			this._directionalToggleBinder.Enable();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0005E980 File Offset: 0x0005CB80
		private void RefreshNoteTypeToggles()
		{
			this._typeToggleBinder.Disable();
			this._typeToggleNoteA.isOn = (this._selectedNoteType.value == NoteType.NoteA);
			this._typeToggleNoteB.isOn = (this._selectedNoteType.value == NoteType.NoteB);
			this._typeToggleBombNote.isOn = (this._selectedNoteType.value == NoteType.Bomb);
			this._typeToggleBinder.Enable();
		}

		// Token: 0x040019FE RID: 6654
		[SerializeField]
		private EditorSelectedNoteTypeSO _selectedNoteType;

		// Token: 0x040019FF RID: 6655
		[SerializeField]
		private EditorSelectedNoteCutDirectionSO _selectedNoteCutDirection;

		// Token: 0x04001A00 RID: 6656
		[Space]
		[SerializeField]
		private Toggle _directionToggleUp;

		// Token: 0x04001A01 RID: 6657
		[SerializeField]
		private Toggle _directionToggleUpLeft;

		// Token: 0x04001A02 RID: 6658
		[SerializeField]
		private Toggle _directionToggleUpRight;

		// Token: 0x04001A03 RID: 6659
		[SerializeField]
		private Toggle _directionToggleLeft;

		// Token: 0x04001A04 RID: 6660
		[SerializeField]
		private Toggle _directionToggleRight;

		// Token: 0x04001A05 RID: 6661
		[SerializeField]
		private Toggle _directionToggleDown;

		// Token: 0x04001A06 RID: 6662
		[SerializeField]
		private Toggle _directionToggleDownLeft;

		// Token: 0x04001A07 RID: 6663
		[SerializeField]
		private Toggle _directionToggleDownRight;

		// Token: 0x04001A08 RID: 6664
		[SerializeField]
		private Toggle _directionToggleAnyDirection;

		// Token: 0x04001A09 RID: 6665
		[SerializeField]
		private Toggle _typeToggleNoteA;

		// Token: 0x04001A0A RID: 6666
		[SerializeField]
		private Toggle _typeToggleNoteB;

		// Token: 0x04001A0B RID: 6667
		[SerializeField]
		private Toggle _typeToggleBombNote;

		// Token: 0x04001A0C RID: 6668
		private ToggleBinder _directionalToggleBinder = new ToggleBinder();

		// Token: 0x04001A0D RID: 6669
		private ToggleBinder _typeToggleBinder = new ToggleBinder();
	}
}
