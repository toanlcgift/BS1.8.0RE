using System;
using UnityEngine;
using Zenject;

// Token: 0x02000051 RID: 81
public class FadeOutSongPreviewPlayerOnSceneTransitionStart : MonoBehaviour
{
	// Token: 0x0600015E RID: 350 RVA: 0x000031C2 File Offset: 0x000013C2
	protected void Start()
	{
		this._gameScenesManager.transitionDidStartEvent += this.HandleGameScenesManagerTransitionDidStart;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000031DB File Offset: 0x000013DB
	protected void OnDestroy()
	{
		if (this._gameScenesManager != null)
		{
			this._gameScenesManager.transitionDidStartEvent -= this.HandleGameScenesManagerTransitionDidStart;
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00003202 File Offset: 0x00001402
	private void HandleGameScenesManagerTransitionDidStart(float duration)
	{
		this._songPreviewPlayer.FadeOut();
	}

	// Token: 0x04000138 RID: 312
	[SerializeField]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x04000139 RID: 313
	[Inject]
	private GameScenesManager _gameScenesManager;
}
