using System;
using System.IO;

// Token: 0x02000224 RID: 548
public static class StandardLevelLoader
{
	// Token: 0x0600089A RID: 2202 RVA: 0x00006FB7 File Offset: 0x000051B7
	public static StandardLevelInfoSaveData LoadStandardLevelSaveData(string levelInfoFilenamePath)
	{
		return StandardLevelInfoSaveData.DeserializeFromJSONString(File.ReadAllText(levelInfoFilenamePath));
	}
}
