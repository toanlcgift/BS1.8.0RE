using System;

namespace BeatmapEditor
{
	// Token: 0x02000523 RID: 1315
	public struct BeatmapCharacteristicDifficulty
	{
		// Token: 0x0600192D RID: 6445 RVA: 0x00012AC1 File Offset: 0x00010CC1
		public static bool operator ==(BeatmapCharacteristicDifficulty obj1, BeatmapCharacteristicDifficulty obj2)
		{
			return obj1.characteristicSerializedName == obj2.characteristicSerializedName && obj1.difficulty == obj2.difficulty;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00012AE6 File Offset: 0x00010CE6
		public static bool operator !=(BeatmapCharacteristicDifficulty obj1, BeatmapCharacteristicDifficulty obj2)
		{
			return !(obj1 == obj2);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00012AF2 File Offset: 0x00010CF2
		public override bool Equals(object other)
		{
			return other is BeatmapCharacteristicDifficulty && this == (BeatmapCharacteristicDifficulty)other;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00012B0F File Offset: 0x00010D0F
		public override int GetHashCode()
		{
			return ((this.characteristicSerializedName != null) ? this.characteristicSerializedName.GetHashCode() : 1) + this.difficulty.GetHashCode();
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00012B39 File Offset: 0x00010D39
		public override string ToString()
		{
			return string.Format("BeatmapCharacteristicDifficulty ({0} {1})", this.characteristicSerializedName, this.difficulty);
		}

		// Token: 0x04001876 RID: 6262
		public string characteristicSerializedName;

		// Token: 0x04001877 RID: 6263
		public BeatmapDifficulty difficulty;
	}
}
