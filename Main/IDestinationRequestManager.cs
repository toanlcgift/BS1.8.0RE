using System;

// Token: 0x020000BA RID: 186
public interface IDestinationRequestManager
{
	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060002A0 RID: 672
	// (remove) Token: 0x060002A1 RID: 673
	event Action<MenuDestination> didSendMenuDestinationRequestEvent;

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060002A2 RID: 674
	MenuDestination currentMenuDestinationRequest { get; }

	// Token: 0x060002A3 RID: 675
	void Clear();
}
