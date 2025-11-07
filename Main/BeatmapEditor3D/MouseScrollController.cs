using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x02000513 RID: 1299
	public class MouseScrollController : MonoBehaviour
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0001234E File Offset: 0x0001054E
		private UserInputSensitivityConfig _sensitivityConfig
		{
			get
			{
				return this._userInputConfig.sensitivityConfig;
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00056FC0 File Offset: 0x000551C0
		protected void Update()
		{
			float y = Input.mouseScrollDelta.y;
			if (y != 0f && !this._songAudioController.isPlaying)
			{
				float time = this._songAudioController.time;
				this._songAudioController.time = time + y * this._sensitivityConfig.mouseScrollSensitivity;
			}
		}

		// Token: 0x04001828 RID: 6184
		[Inject]
		private SongAudioController _songAudioController;

		// Token: 0x04001829 RID: 6185
		[Inject]
		private UserInputConfig _userInputConfig;
	}
}
