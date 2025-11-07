using System;
using System.IO;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class CustomLevelPathHelper
{
	// Token: 0x06000292 RID: 658 RVA: 0x00003B5F File Offset: 0x00001D5F
	static CustomLevelPathHelper()
	{
		CustomLevelPathHelper.customLevelsDirectoryPath = Path.Combine(CustomLevelPathHelper.baseProjectPath, "CustomLevels");
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00003B7F File Offset: 0x00001D7F
	public static string GetDefaultNameForCustomLevel(string songName, string songAuthorName, string levelAuthorName)
	{
		return string.Concat(new string[]
		{
			songAuthorName,
			" - ",
			songName,
			" (",
			levelAuthorName,
			")"
		});
	}

	// Token: 0x04000326 RID: 806
	public const string kStandardLevelInfoFilename = "Info.dat";

	// Token: 0x04000327 RID: 807
	public const string kCustomLevelsDirectoryName = "CustomLevels";

	// Token: 0x04000328 RID: 808
	[DoesNotRequireDomainReloadInit]
	public static readonly string customLevelsDirectoryPath;

	// Token: 0x04000329 RID: 809
	[DoesNotRequireDomainReloadInit]
	public static readonly string baseProjectPath = Application.dataPath;
}
