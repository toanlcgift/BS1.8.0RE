using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x02000512 RID: 1298
	public class MouseController : MonoBehaviour
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00012245 File Offset: 0x00010445
		private UserInputKeyBinding _keyBinding
		{
			get
			{
				return this._userInputConfig.keyBinding;
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00056CE8 File Offset: 0x00054EE8
		protected void Awake()
		{
			this._keyboardShortcutActions = new Dictionary<KeyboardShortcut, Action>
			{
				{
					this._keyBinding.actionMouseButton,
					delegate()
					{
						this.PerformActionAtMousePosition(Input.mousePosition);
					}
				},
				{
					this._keyBinding.changeNoteType,
					delegate()
					{
						this.ChangeNoteTypeAtMousePosition(Input.mousePosition);
					}
				},
				{
					this._keyBinding.setNoteDirectionUp,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.Up);
					}
				},
				{
					this._keyBinding.setNoteDirectionDown,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.Down);
					}
				},
				{
					this._keyBinding.setNoteDirectionLeft,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.Left);
					}
				},
				{
					this._keyBinding.setNoteDirectionRight,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.Right);
					}
				},
				{
					this._keyBinding.setNoteDirectionUpLeft,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.UpLeft);
					}
				},
				{
					this._keyBinding.setNoteDirectionUpRight,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.UpRight);
					}
				},
				{
					this._keyBinding.setNoteDirectionDownLeft,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.DownLeft);
					}
				},
				{
					this._keyBinding.setNoteDirectionDownRight,
					delegate()
					{
						this.SetNoteCutDirectionAtMousePosition(Input.mousePosition, NoteCutDirection.DownRight);
					}
				}
			};
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00012252 File Offset: 0x00010452
		protected void OnEnable()
		{
			this._keyboardShortcutManager.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00012265 File Offset: 0x00010465
		protected void OnDisable()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutManager.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00012280 File Offset: 0x00010480
		protected void Update()
		{
			this.HighlightRaycastedObject(Input.mousePosition);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00056E24 File Offset: 0x00055024
		private void HighlightRaycastedObject(Vector2 screenPosition)
		{
			RaycastHit raycastHit;
			if (this._raycaster.RaycastFromCamera(this._camera, screenPosition, out raycastHit))
			{
				this.HighlightGameObject(raycastHit.collider.gameObject, true);
				return;
			}
			this.HighlightGameObject(this._highlightedGameObject, false);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00056E68 File Offset: 0x00055068
		private void HighlightGameObject(GameObject go, bool highlight)
		{
			if (go == null)
			{
				return;
			}
			if (highlight && this._highlightedGameObject == go)
			{
				return;
			}
			if (highlight)
			{
				this.HighlightGameObject(this._highlightedGameObject, false);
			}
			this._highlightedGameObject = (highlight ? go : null);
			this._gridController.HighlightGameObject(go, highlight);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00056EB8 File Offset: 0x000550B8
		private void ChangeNoteTypeAtMousePosition(Vector2 screenPosition)
		{
			GameObject go;
			if ((go = this._raycaster.RaycastedGameObjectFromCamera(this._camera, screenPosition)) != null)
			{
				this._gridController.ChangeNoteTypeForGameObject(go);
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00056EE8 File Offset: 0x000550E8
		private void SetNoteCutDirectionAtMousePosition(Vector2 screenPosition, NoteCutDirection direction)
		{
			GameObject go;
			if ((go = this._raycaster.RaycastedGameObjectFromCamera(this._camera, screenPosition)) != null)
			{
				this._gridController.SetNoteCutDirectionForGameObject(go, direction);
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00056F18 File Offset: 0x00055118
		private void PerformActionAtMousePosition(Vector2 screenPosition)
		{
			if (this._gridController.isPlacingObstacle)
			{
				this._gridController.FinishPlacingObstacle();
				return;
			}
			GameObject gameObject;
			if ((gameObject = this._raycaster.RaycastedGameObjectFromCamera(this._camera, screenPosition)) != null)
			{
				if (this._gridController.editMode == GridEditMode.Delete)
				{
					this._gridController.RemoveGameObject(gameObject);
					return;
				}
				GridLaneFrontQuad component;
				if ((component = gameObject.GetComponent<GridLaneFrontQuad>()) != null)
				{
					if (Mathf.CeilToInt(this._gridController.GetCurrentLaneAngle()) == Mathf.CeilToInt(component.laneAngle))
					{
						BeatmapObjectLineIndex lineIndex = new BeatmapObjectLineIndex(component.positionIndex);
						this._gridController.AddNewBeatmapObjectAtCurrentGridLaneAngle(lineIndex);
						return;
					}
				}
				else
				{
					this._gridController.SelectGameObject(gameObject);
				}
			}
		}

		// Token: 0x04001821 RID: 6177
		[Inject]
		private Camera _camera;

		// Token: 0x04001822 RID: 6178
		[Inject]
		private GridController _gridController;

		// Token: 0x04001823 RID: 6179
		[Inject]
		private KeyboardShortcutManager _keyboardShortcutManager;

		// Token: 0x04001824 RID: 6180
		[Inject]
		private Raycaster _raycaster;

		// Token: 0x04001825 RID: 6181
		[Inject]
		private UserInputConfig _userInputConfig;

		// Token: 0x04001826 RID: 6182
		private GameObject _highlightedGameObject;

		// Token: 0x04001827 RID: 6183
		private Dictionary<KeyboardShortcut, Action> _keyboardShortcutActions;
	}
}
