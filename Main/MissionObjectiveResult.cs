using System;

// Token: 0x0200018D RID: 397
public class MissionObjectiveResult
{
	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000634 RID: 1588 RVA: 0x000059F9 File Offset: 0x00003BF9
	// (set) Token: 0x06000635 RID: 1589 RVA: 0x00005A01 File Offset: 0x00003C01
	public MissionObjective missionObjective { get; private set; }

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000636 RID: 1590 RVA: 0x00005A0A File Offset: 0x00003C0A
	// (set) Token: 0x06000637 RID: 1591 RVA: 0x00005A12 File Offset: 0x00003C12
	public bool cleared { get; private set; }

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000638 RID: 1592 RVA: 0x00005A1B File Offset: 0x00003C1B
	// (set) Token: 0x06000639 RID: 1593 RVA: 0x00005A23 File Offset: 0x00003C23
	public int value { get; private set; }

	// Token: 0x0600063A RID: 1594 RVA: 0x00005A2C File Offset: 0x00003C2C
	public MissionObjectiveResult(MissionObjective missionObjective, bool cleared, int value)
	{
		this.missionObjective = missionObjective;
		this.cleared = cleared;
		this.value = value;
	}
}
