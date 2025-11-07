using System;

// Token: 0x0200021B RID: 539
public interface ISaberMovementDataProcessor
{
	// Token: 0x06000870 RID: 2160
	void ProcessNewData(SaberMovementData.Data newData, SaberMovementData.Data prevData, bool prevDataAreValid);
}
