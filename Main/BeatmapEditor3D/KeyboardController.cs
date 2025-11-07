using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x0200050E RID: 1294
	public class KeyboardController : MonoBehaviour
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00012080 File Offset: 0x00010280
		private UserInputKeyBinding _keyBinding
		{
			get
			{
				return this._userInputConfig.keyBinding;
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000566A0 File Offset: 0x000548A0
		protected void Awake()
		{
			this._keyboardShortcutActions = new Dictionary<KeyboardShortcut, Action>
			{
				{
					this._keyBinding.play,
					delegate()
					{
						this._songAudioController.PlayOrPause();
					}
				},
				{
					this._keyBinding.cameraToPreviousLane,
					delegate()
					{
						this.RotateCameraToLane(-this._gridConfig.laneDegreesStep);
					}
				},
				{
					this._keyBinding.cameraToNextLane,
					delegate()
					{
						this.RotateCameraToLane(this._gridConfig.laneDegreesStep);
					}
				},
				{
					this._keyBinding.switchVariablePlayheadSpeed,
					delegate()
					{
						this._gridController.SwitchPlayheadSpeedVariability();
					}
				},
				{
					this._keyBinding.activateDeleteMode,
					delegate()
					{
						this._gridController.editMode = GridEditMode.Delete;
					}
				},
				{
					this._keyBinding.preselectObjectTypeNoteA,
					delegate()
					{
						this._gridController.PreselectBeatmapObjectType(BeatmapObjectType.NoteA);
					}
				},
				{
					this._keyBinding.preselectObjectTypeNoteB,
					delegate()
					{
						this._gridController.PreselectBeatmapObjectType(BeatmapObjectType.NoteB);
					}
				},
				{
					this._keyBinding.preselectObjectTypeBomb,
					delegate()
					{
						this._gridController.PreselectBeatmapObjectType(BeatmapObjectType.Bomb);
					}
				},
				{
					this._keyBinding.preselectObjectTypeObstacle,
					delegate()
					{
						this._gridController.PreselectBeatmapObjectType(BeatmapObjectType.Obstacle);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionAny,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.Any);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionUp,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.Up);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionDown,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.Down);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionLeft,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.Left);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionRight,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.Right);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionUpLeft,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.UpLeft);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionUpRight,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.UpRight);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionDownLeft,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.DownLeft);
					}
				},
				{
					this._keyBinding.preselectNoteDirectionDownRight,
					delegate()
					{
						this._gridController.PreselectNoteDirection(NoteCutDirection.DownRight);
					}
				}
			};
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0001208D File Offset: 0x0001028D
		protected void OnEnable()
		{
			this._keyboardShortcutManager.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000120A0 File Offset: 0x000102A0
		protected void OnDisable()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutManager.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000120BB File Offset: 0x000102BB
		private void RotateCameraToLane(float degrees)
		{
			this._cameraController.RotateToLane(degrees, true);
		}

		// Token: 0x0400180F RID: 6159
		[Inject]
		private CameraController _cameraController;

		// Token: 0x04001810 RID: 6160
		[Inject]
		private GridConfig _gridConfig;

		// Token: 0x04001811 RID: 6161
		[Inject]
		private GridController _gridController;

		// Token: 0x04001812 RID: 6162
		[Inject]
		private KeyboardShortcutManager _keyboardShortcutManager;

		// Token: 0x04001813 RID: 6163
		[Inject]
		private SongAudioController _songAudioController;

		// Token: 0x04001814 RID: 6164
		[Inject]
		private UserInputConfig _userInputConfig;

		// Token: 0x04001815 RID: 6165
		private Dictionary<KeyboardShortcut, Action> _keyboardShortcutActions;
	}
}
