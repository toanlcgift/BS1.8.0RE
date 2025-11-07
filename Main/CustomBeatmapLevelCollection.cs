using System;

// Token: 0x0200014B RID: 331
public class CustomBeatmapLevelCollection : IBeatmapLevelCollection
{
	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000514 RID: 1300 RVA: 0x00022E38 File Offset: 0x00021038
	public IPreviewBeatmapLevel[] beatmapLevels
	{
		get
		{
			return this._customPreviewBeatmapLevels;
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x000051D3 File Offset: 0x000033D3
	public CustomBeatmapLevelCollection(CustomPreviewBeatmapLevel[] customPreviewBeatmapLevels)
	{
		this._customPreviewBeatmapLevels = customPreviewBeatmapLevels;
	}

	// Token: 0x04000561 RID: 1377
	private CustomPreviewBeatmapLevel[] _customPreviewBeatmapLevels;
}
