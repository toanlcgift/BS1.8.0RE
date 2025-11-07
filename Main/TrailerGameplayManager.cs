using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x020002BF RID: 703
public class TrailerGameplayManager : MonoBehaviour
{
	// Token: 0x06000BF0 RID: 3056 RVA: 0x00009654 File Offset: 0x00007854
	private IEnumerator Start()
	{
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		yield return null;
		this._mainCamera.enableCamera = !this._disableMainCamera;
		this._gameSongController.StartSong();
		yield break;
	}

	// Token: 0x04000C9B RID: 3227
	[SerializeField]
	private bool _disableMainCamera;

	// Token: 0x04000C9C RID: 3228
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04000C9D RID: 3229
	[Inject]
	private GameSongController _gameSongController;

	// Token: 0x04000C9E RID: 3230
	[Inject]
	private MainCamera _mainCamera;
}
