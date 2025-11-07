using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class ObstacleDissolve : MonoBehaviour
{
	// Token: 0x06000A1B RID: 2587 RVA: 0x00007D2A File Offset: 0x00005F2A
	protected void Awake()
	{
		this._obstacleController.didStartDissolvingEvent += this.HandleObcstacleDidStartDissolvingEvent;
		this._obstacleController.didInitEvent += this.HandleObstacleDidInitEvent;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00007D5A File Offset: 0x00005F5A
	protected void OnDestroy()
	{
		if (this._obstacleController)
		{
			this._obstacleController.didStartDissolvingEvent -= this.HandleObcstacleDidStartDissolvingEvent;
			this._obstacleController.didInitEvent -= this.HandleObstacleDidInitEvent;
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00007D97 File Offset: 0x00005F97
	private void HandleObstacleDidInitEvent(ObstacleController obstacleController)
	{
		this._cutoutAnimateEffect.ResetEffect();
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00007DA4 File Offset: 0x00005FA4
	private void HandleObcstacleDidStartDissolvingEvent(ObstacleController obstacleController, float duration)
	{
		this._cutoutAnimateEffect.AnimateCutout(0f, 1f, duration);
	}

	// Token: 0x04000A53 RID: 2643
	[SerializeField]
	private ObstacleController _obstacleController;

	// Token: 0x04000A54 RID: 2644
	[SerializeField]
	private CutoutAnimateEffect _cutoutAnimateEffect;
}
