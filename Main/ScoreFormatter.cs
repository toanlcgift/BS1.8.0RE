using System;
using System.Globalization;

// Token: 0x020000D0 RID: 208
public class ScoreFormatter
{
	// Token: 0x0600030E RID: 782 RVA: 0x00003ED9 File Offset: 0x000020D9
	static ScoreFormatter()
	{
		ScoreFormatter._numberFormatInfo.NumberGroupSeparator = " ";
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00003F03 File Offset: 0x00002103
	public static string Format(int score)
	{
		return score.ToString("#,0", ScoreFormatter._numberFormatInfo);
	}

	// Token: 0x04000388 RID: 904
	[DoesNotRequireDomainReloadInit]
	private static readonly NumberFormatInfo _numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
}
