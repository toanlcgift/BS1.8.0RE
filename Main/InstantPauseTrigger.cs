using System;
using Zenject;

// Token: 0x020002F2 RID: 754
public class InstantPauseTrigger : ITickable, IPauseTrigger
{
	// Token: 0x14000065 RID: 101
	// (add) Token: 0x06000CE9 RID: 3305 RVA: 0x00037748 File Offset: 0x00035948
	// (remove) Token: 0x06000CEA RID: 3306 RVA: 0x00037780 File Offset: 0x00035980
	public event Action pauseTriggeredEvent;

	// Token: 0x06000CEB RID: 3307 RVA: 0x0000A027 File Offset: 0x00008227
	public void Tick()
	{
		if (this._vrControllersInputManager.MenuButtonDown())
		{
			Action action = this.pauseTriggeredEvent;
			if (action == null)
			{
				return;
			}
			action();
		}
	}

	// Token: 0x04000D53 RID: 3411
	[Inject]
	private VRControllersInputManager _vrControllersInputManager;
}
