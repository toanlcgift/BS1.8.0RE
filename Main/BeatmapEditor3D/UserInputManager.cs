using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x02000514 RID: 1300
	public class UserInputManager : MonoBehaviour
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0001235B File Offset: 0x0001055B
		private UserInputKeyBinding _keyBindings
		{
			get
			{
				return this._userInputConfig.keyBinding;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00012368 File Offset: 0x00010568
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x00012375 File Offset: 0x00010575
		public bool isFpsCameraControllerActive
		{
			get
			{
				return this._cameraController.isFpsCameraControllerActive;
			}
			set
			{
				this._cameraController.isFpsCameraControllerActive = value;
				this._keyboardController.gameObject.SetActive(!value);
				this.UpdateMouseControllerActiveState();
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0001239D File Offset: 0x0001059D
		protected void Awake()
		{
			this._songAudioController.didChangePlayStateEvent += this.HandleSongDidChangeState;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000123B6 File Offset: 0x000105B6
		protected void OnDestroy()
		{
			this._songAudioController.didChangePlayStateEvent -= this.HandleSongDidChangeState;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000123CF File Offset: 0x000105CF
		protected void Start()
		{
			this.isFpsCameraControllerActive = false;
			this._cameraController.UpdateDefaultPositions(this._gridConfig.innerCircleRadius);
			this._cameraController.ResetPosition(false);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000123FA File Offset: 0x000105FA
		protected void Update()
		{
			if (Input.GetKeyDown(this._keyBindings.cameraMovementActivation))
			{
				this.isFpsCameraControllerActive = true;
				return;
			}
			if (Input.GetKeyUp(this._keyBindings.cameraMovementActivation))
			{
				this.isFpsCameraControllerActive = false;
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0001242F File Offset: 0x0001062F
		private void HandleSongDidChangeState()
		{
			this.UpdateMouseControllerActiveState();
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00057014 File Offset: 0x00055214
		private void UpdateMouseControllerActiveState()
		{
			bool active = false;
			if (!this._songAudioController.isPlaying && !this.isFpsCameraControllerActive)
			{
				active = true;
			}
			this._mouseController.gameObject.SetActive(active);
		}

		// Token: 0x0400182A RID: 6186
		[Inject]
		private CameraController _cameraController;

		// Token: 0x0400182B RID: 6187
		[Inject]
		private GridConfig _gridConfig;

		// Token: 0x0400182C RID: 6188
		[Inject]
		private KeyboardController _keyboardController;

		// Token: 0x0400182D RID: 6189
		[Inject]
		private MouseController _mouseController;

		// Token: 0x0400182E RID: 6190
		[Inject]
		private SongAudioController _songAudioController;

		// Token: 0x0400182F RID: 6191
		[Inject]
		private UserInputConfig _userInputConfig;
	}
}
