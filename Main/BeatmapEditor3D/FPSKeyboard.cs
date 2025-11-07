using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x0200050C RID: 1292
	public class FPSKeyboard : MonoBehaviour
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00011F12 File Offset: 0x00010112
		private UserInputKeyBinding _keyBinding
		{
			get
			{
				return this._userInputConfig.keyBinding;
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0005640C File Offset: 0x0005460C
		protected void Awake()
		{
			this._cameraTransform = this._camera.transform;
			this._keyboardShortcutActions = new Dictionary<KeyboardShortcut, Action>
			{
				{
					this._keyBinding.cameraForward,
					delegate()
					{
						this.Move(this._cameraTransform.forward);
					}
				},
				{
					this._keyBinding.cameraBackwards,
					delegate()
					{
						this.Move(-this._cameraTransform.forward);
					}
				},
				{
					this._keyBinding.cameraRight,
					delegate()
					{
						this.Move(this._cameraTransform.right);
					}
				},
				{
					this._keyBinding.cameraLeft,
					delegate()
					{
						this.Move(-this._cameraTransform.right);
					}
				},
				{
					this._keyBinding.cameraUp,
					delegate()
					{
						this.Move(this._cameraTransform.up);
					}
				},
				{
					this._keyBinding.cameraDown,
					delegate()
					{
						this.Move(-this._cameraTransform.up);
					}
				},
				{
					this._keyBinding.cameraUp2,
					delegate()
					{
						this.Move(this._cameraTransform.up);
					}
				},
				{
					this._keyBinding.cameraDown2,
					delegate()
					{
						this.Move(-this._cameraTransform.up);
					}
				},
				{
					this._keyBinding.resetCameraPosition,
					delegate()
					{
						this.ResetCameraPosition();
					}
				},
				{
					this._keyBinding.cameraPosition1,
					delegate()
					{
						this.ResetCameraPosition(0);
					}
				},
				{
					this._keyBinding.cameraPosition2,
					delegate()
					{
						this.ResetCameraPosition(1);
					}
				},
				{
					this._keyBinding.cameraPosition3,
					delegate()
					{
						this.ResetCameraPosition(2);
					}
				}
			};
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00011F1F File Offset: 0x0001011F
		protected void OnEnable()
		{
			this._keyboardShortcutManager.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00011F32 File Offset: 0x00010132
		protected void OnDisable()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutManager.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00056594 File Offset: 0x00054794
		private void Move(Vector3 direction)
		{
			if (Input.GetKey(this._keyBinding.fasterCameraMovement))
			{
				this._sensitivityMultiplier = this._fasterSensitivityMultiplier;
			}
			else if (Input.GetKey(this._keyBinding.slowerCameraMovement))
			{
				this._sensitivityMultiplier = this._slowerSensitivityMultiplier;
			}
			else
			{
				this._sensitivityMultiplier = 1f;
			}
			Vector3 vector = this._cameraTransform.position;
			vector += direction * this._sensitivity * this._sensitivityMultiplier * TimeHelper.deltaTime;
			this._cameraTransform.position = vector;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00011F4D File Offset: 0x0001014D
		private void ResetCameraPosition()
		{
			this._cameraController.ResetPosition(true);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00011F5B File Offset: 0x0001015B
		private void ResetCameraPosition(int positionIndex)
		{
			this._cameraController.ResetPosition(positionIndex, true);
		}

		// Token: 0x04001801 RID: 6145
		[Inject]
		private Camera _camera;

		// Token: 0x04001802 RID: 6146
		private float _sensitivity = 5f;

		// Token: 0x04001803 RID: 6147
		private float _sensitivityMultiplier = 1f;

		// Token: 0x04001804 RID: 6148
		private float _fasterSensitivityMultiplier = 2f;

		// Token: 0x04001805 RID: 6149
		private float _slowerSensitivityMultiplier = 0.5f;

		// Token: 0x04001806 RID: 6150
		[Inject]
		private KeyboardShortcutManager _keyboardShortcutManager;

		// Token: 0x04001807 RID: 6151
		[Inject]
		private UserInputConfig _userInputConfig;

		// Token: 0x04001808 RID: 6152
		[Inject]
		private CameraController _cameraController;

		// Token: 0x04001809 RID: 6153
		private Transform _cameraTransform;

		// Token: 0x0400180A RID: 6154
		private Dictionary<KeyboardShortcut, Action> _keyboardShortcutActions;
	}
}
