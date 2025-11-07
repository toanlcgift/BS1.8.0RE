using System;
using UnityEngine;
using Zenject;

// Token: 0x020002F0 RID: 752
public class DelayedPauseTrigger : ITickable, IPauseTrigger
{
	// Token: 0x14000063 RID: 99
	// (add) Token: 0x06000CE3 RID: 3299 RVA: 0x0003766C File Offset: 0x0003586C
	// (remove) Token: 0x06000CE4 RID: 3300 RVA: 0x000376A4 File Offset: 0x000358A4
	public event Action pauseTriggeredEvent;

	// Token: 0x06000CE5 RID: 3301 RVA: 0x000376DC File Offset: 0x000358DC
	public void Tick()
	{
		if (this._vrControllersInputManager.MenuButton() && !this._waitingForButtonRelease)
		{
			this._timer += Time.deltaTime;
			if (this._timer > this._pressDuration)
			{
				this._waitingForButtonRelease = true;
				Action action = this.pauseTriggeredEvent;
				if (action == null)
				{
					return;
				}
				action();
				return;
			}
		}
		else
		{
			this._waitingForButtonRelease = false;
			this._timer = 0f;
		}
	}

	// Token: 0x04000D4F RID: 3407
	[Inject]
	private float _pressDuration = 0.3f;

	// Token: 0x04000D50 RID: 3408
	private float _timer;

	// Token: 0x04000D51 RID: 3409
	private bool _waitingForButtonRelease;

	// Token: 0x04000D52 RID: 3410
	[Inject]
	private VRControllersInputManager _vrControllersInputManager;
}
