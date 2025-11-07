using System;

// Token: 0x02000163 RID: 355
public struct GetAssetBundleFileResult
{
	// Token: 0x0600058E RID: 1422 RVA: 0x00005477 File Offset: 0x00003677
	public GetAssetBundleFileResult(bool isError, string assetBundlePath)
	{
		this.isError = isError;
		this.assetBundlePath = assetBundlePath;
	}

	// Token: 0x040005B6 RID: 1462
	public readonly bool isError;

	// Token: 0x040005B7 RID: 1463
	public readonly string assetBundlePath;
}
