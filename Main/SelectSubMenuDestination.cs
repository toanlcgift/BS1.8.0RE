using System;

// Token: 0x020000BE RID: 190
public sealed class SelectSubMenuDestination : MenuDestination
{
	// Token: 0x060002AD RID: 685 RVA: 0x00003C37 File Offset: 0x00001E37
	public SelectSubMenuDestination(SelectSubMenuDestination.Destination menuDestination)
	{
		this.menuDestination = menuDestination;
	}

	// Token: 0x0400033C RID: 828
	public readonly SelectSubMenuDestination.Destination menuDestination;

	// Token: 0x020000BF RID: 191
	public enum Destination
	{
		// Token: 0x0400033E RID: 830
		MainMenu,
		// Token: 0x0400033F RID: 831
		Campaign,
		// Token: 0x04000340 RID: 832
		SoloFreePlay,
		// Token: 0x04000341 RID: 833
		PartyFreePlay,
		// Token: 0x04000342 RID: 834
		Settings,
		// Token: 0x04000343 RID: 835
		Tutorial
	}
}
