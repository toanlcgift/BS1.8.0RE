using System;

// Token: 0x02000471 RID: 1137
public class CreditsScenesTransitionSetupDataSO : NoSetupDataSingleFixedSceneScenesTransitionSetupDataSO
{
	// Token: 0x140000C9 RID: 201
	// (add) Token: 0x06001555 RID: 5461 RVA: 0x0004E56C File Offset: 0x0004C76C
	// (remove) Token: 0x06001556 RID: 5462 RVA: 0x0004E5A4 File Offset: 0x0004C7A4
	public event Action<CreditsScenesTransitionSetupDataSO> didFinishEvent;

	// Token: 0x06001557 RID: 5463 RVA: 0x00010028 File Offset: 0x0000E228
	public void Finish()
	{
		Action<CreditsScenesTransitionSetupDataSO> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}
}
