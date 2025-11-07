using System;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

// Token: 0x0200043B RID: 1083
public class PlayableDirectorTimer : MonoBehaviour
{
	// Token: 0x060014BC RID: 5308 RVA: 0x0000FA36 File Offset: 0x0000DC36
	protected void Update()
	{
		this._playableDirector.time = (double)this._audioTimeSyncController.songTime;
		this._playableDirector.Evaluate();
	}

	// Token: 0x04001470 RID: 5232
	[SerializeField]
	private PlayableDirector _playableDirector;

	// Token: 0x04001471 RID: 5233
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;
}
