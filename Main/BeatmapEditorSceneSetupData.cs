using System;
using UnityEngine;

// Token: 0x02000464 RID: 1124
[Serializable]
public class BeatmapEditorSceneSetupData : SceneSetupData
{
	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x0600152F RID: 5423 RVA: 0x0000FE4A File Offset: 0x0000E04A
	public string levelDirPath
	{
		get
		{
			return this._levelDirPath;
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06001530 RID: 5424 RVA: 0x0000FE52 File Offset: 0x0000E052
	public string levelAssetPath
	{
		get
		{
			return this._levelAssetPath;
		}
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x0000FE5A File Offset: 0x0000E05A
	public BeatmapEditorSceneSetupData(string levelDirPath, string levelAssetPath)
	{
		this._levelDirPath = levelDirPath;
		this._levelAssetPath = levelAssetPath;
	}

	// Token: 0x04001529 RID: 5417
	[SerializeField]
	private string _levelDirPath;

	// Token: 0x0400152A RID: 5418
	[SerializeField]
	private string _levelAssetPath;
}
