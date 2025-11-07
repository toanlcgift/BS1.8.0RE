using System;

// Token: 0x020002B8 RID: 696
public interface ILevelEndActions
{
	// Token: 0x1400004A RID: 74
	// (add) Token: 0x06000BC5 RID: 3013
	// (remove) Token: 0x06000BC6 RID: 3014
	event Action levelFailedEvent;

	// Token: 0x1400004B RID: 75
	// (add) Token: 0x06000BC7 RID: 3015
	// (remove) Token: 0x06000BC8 RID: 3016
	event Action levelFinishedEvent;
}
