using System;

namespace LeaderboardsDTO
{
	// Token: 0x02000489 RID: 1161
	[Serializable]
	public enum GameplayModifiersDTO : uint
	{
		// Token: 0x040015AB RID: 5547
		None,
		// Token: 0x040015AC RID: 5548
		NoFail,
		// Token: 0x040015AD RID: 5549
		InstaFail,
		// Token: 0x040015AE RID: 5550
		FailOnSaberClash = 4U,
		// Token: 0x040015AF RID: 5551
		FastNotes = 8U,
		// Token: 0x040015B0 RID: 5552
		DisappearingArrows = 16U,
		// Token: 0x040015B1 RID: 5553
		NoBombs = 32U,
		// Token: 0x040015B2 RID: 5554
		SongSpeedFaster = 64U,
		// Token: 0x040015B3 RID: 5555
		SongSpeedSlower = 128U,
		// Token: 0x040015B4 RID: 5556
		EnabledObstacleTypeFullHeightOnly = 256U,
		// Token: 0x040015B5 RID: 5557
		EnabledObstacleTypeNoObstacles = 512U,
		// Token: 0x040015B6 RID: 5558
		EnergyTypeBattery = 1024U,
		// Token: 0x040015B7 RID: 5559
		StrictAngles = 2048U,
		// Token: 0x040015B8 RID: 5560
		NoArrows = 4096U,
		// Token: 0x040015B9 RID: 5561
		GhostNotes = 8192U
	}
}
