using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

// Token: 0x020000A1 RID: 161
public class CustomLevelLoader : MonoBehaviour
{
	// Token: 0x06000258 RID: 600 RVA: 0x000039FA File Offset: 0x00001BFA
	public void ClearCache()
	{
		this._cachedMediaAsyncLoaderSO.ClearCache();
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0001C058 File Offset: 0x0001A258
	public async Task<CustomBeatmapLevelPack[]> LoadCustomPreviewBeatmapLevelPacksAsync(CustomLevelLoader.CustomPackFolderInfo[] customPackFolderInfos, CancellationToken cancellationToken)
	{
		int numberOfPacks = customPackFolderInfos.Length;
		List<CustomBeatmapLevelPack> customBeatmapLevelPacks = new List<CustomBeatmapLevelPack>(numberOfPacks);
		for (int i = 0; i < numberOfPacks; i++)
		{
			string customLevelPackPath = Path.Combine(CustomLevelPathHelper.baseProjectPath, customPackFolderInfos[i].folderName);
			TaskAwaiter<bool> taskAwaiter = this.CheckPathExistsAsync(customLevelPackPath).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				 taskAwaiter.GetResult();
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (taskAwaiter.GetResult())
			{
				CustomBeatmapLevelPack customBeatmapLevelPack = await this.LoadCustomBeatmapLevelPackAsync(customLevelPackPath, customPackFolderInfos[i].packName, cancellationToken);
				if (customBeatmapLevelPack != null && customBeatmapLevelPack.beatmapLevelCollection.beatmapLevels.Length != 0)
				{
					customBeatmapLevelPacks.Add(customBeatmapLevelPack);
				}
				customLevelPackPath = null;
			}
		}
		return customBeatmapLevelPacks.ToArray();
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
	public async Task<CustomLevelLoader.CustomPackFolderInfo[]> GetSubFoldersInfosAsync(string rootPath, CancellationToken cancellationToken)
	{
		rootPath = Path.Combine(CustomLevelPathHelper.baseProjectPath, rootPath);
		TaskAwaiter<bool> taskAwaiter = this.CheckPathExistsAsync(rootPath).GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			taskAwaiter.GetResult();
			TaskAwaiter<bool> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<bool>);
		}
		CustomLevelLoader.CustomPackFolderInfo[] result;
		if (!taskAwaiter.GetResult())
		{
			result = new CustomLevelLoader.CustomPackFolderInfo[0];
		}
		else
		{
			List<CustomLevelLoader.CustomPackFolderInfo> subDirFolderInfo = new List<CustomLevelLoader.CustomPackFolderInfo>();
			await Task.Run(delegate()
			{
				foreach (string path in Directory.GetDirectories(rootPath))
				{
					subDirFolderInfo.Add(new CustomLevelLoader.CustomPackFolderInfo(Path.GetFullPath(path), new DirectoryInfo(path).Name));
				}
			});
			cancellationToken.ThrowIfCancellationRequested();
			result = subDirFolderInfo.ToArray();
		}
		return result;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0001C108 File Offset: 0x0001A308
	public async Task<CustomBeatmapLevel> LoadCustomBeatmapLevelAsync(CustomPreviewBeatmapLevel customPreviewBeatmapLevel, CancellationToken cancellationToken)
	{
		StandardLevelInfoSaveData standardLevelInfoSaveData = customPreviewBeatmapLevel.standardLevelInfoSaveData;
		string customLevelPath = customPreviewBeatmapLevel.customLevelPath;
		Texture2D texture2D = await customPreviewBeatmapLevel.GetCoverImageTexture2DAsync(cancellationToken);
		Texture2D coverImageTexture2D = texture2D;
		AudioClip previewAudioClip = await customPreviewBeatmapLevel.GetPreviewAudioClipAsync(cancellationToken);
		CustomBeatmapLevel customBeatmapLevel = new CustomBeatmapLevel(customPreviewBeatmapLevel, previewAudioClip, coverImageTexture2D);
		BeatmapLevelData beatmapLevelData = await this.LoadBeatmapLevelDataAsync(customLevelPath, customBeatmapLevel, standardLevelInfoSaveData, cancellationToken);
		customBeatmapLevel.SetBeatmapLevelData(beatmapLevelData);
		return customBeatmapLevel;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0001C160 File Offset: 0x0001A360
	private async Task<bool> CheckPathExistsAsync(string fullCustomLevelPackPath)
	{
		bool pathExists = true;
		await Task.Run(delegate()
		{
			pathExists = Directory.Exists(fullCustomLevelPackPath);
		});
		return pathExists;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
	private async Task<CustomBeatmapLevelPack> LoadCustomBeatmapLevelPackAsync(string customLevelPackPath, string packName, CancellationToken cancellationToken)
	{
		CustomBeatmapLevelCollection customBeatmapLevelCollection = await this.LoadCustomBeatmapLevelCollectionAsync(customLevelPackPath, cancellationToken);
		CustomBeatmapLevelPack result;
		if (customBeatmapLevelCollection.beatmapLevels.Length == 0)
		{
			result = null;
		}
		else
		{
			string packID = "custom_levelpack_" + customLevelPackPath;
			Sprite coverImage = null;
			if (this._defaultPackCoverTexture2D != null)
			{
				coverImage = Sprite.Create(this._defaultPackCoverTexture2D, new Rect(0f, 0f, (float)this._defaultPackCoverTexture2D.width, (float)this._defaultPackCoverTexture2D.height), new Vector2(0.5f, 0.5f), 1024f, 1U, SpriteMeshType.FullRect, new Vector4(0f, 0f, 0f, 0f), false);
			}
			result = new CustomBeatmapLevelPack(packID, packName, packName, coverImage, customBeatmapLevelCollection);
		}
		return result;
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0001C208 File Offset: 0x0001A408
	private async Task<CustomBeatmapLevelCollection> LoadCustomBeatmapLevelCollectionAsync(string customLevelPackPath, CancellationToken cancellationToken)
	{
		TaskAwaiter<CustomPreviewBeatmapLevel[]> taskAwaiter = this.LoadCustomPreviewBeatmapLevelsAsync(customLevelPackPath, cancellationToken).GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			taskAwaiter.GetResult();
			TaskAwaiter<CustomPreviewBeatmapLevel[]> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<CustomPreviewBeatmapLevel[]>);
		}
		return new CustomBeatmapLevelCollection(taskAwaiter.GetResult());
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0001C260 File Offset: 0x0001A460
	private async Task<CustomPreviewBeatmapLevel[]> LoadCustomPreviewBeatmapLevelsAsync(string customLevelPackPath, CancellationToken cancellationToken)
	{
		List<CustomPreviewBeatmapLevel> customPreviewBeatmapLevels = new List<CustomPreviewBeatmapLevel>();
		string[] customLevelPaths = new string[0];
		await Task.Run(delegate()
		{
			if (Directory.Exists(customLevelPackPath))
			{
				customLevelPaths = Directory.GetDirectories(customLevelPackPath);
			}
		});
		foreach (string customLevelPath in customLevelPaths)
		{
			CustomPreviewBeatmapLevel customPreviewBeatmapLevel = await this.LoadCustomPreviewBeatmapLevelAsync(customLevelPath, await this.LoadCustomLevelInfoSaveDataAsync(customLevelPath, cancellationToken), cancellationToken);
			if (customPreviewBeatmapLevel != null && customPreviewBeatmapLevel.previewDifficultyBeatmapSets.Length != 0)
			{
				customPreviewBeatmapLevels.Add(customPreviewBeatmapLevel);
			}
		}
		string[] array = null;
		return customPreviewBeatmapLevels.ToArray();
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
	private async Task<BeatmapLevelData> LoadBeatmapLevelDataAsync(string customLevelPath, CustomBeatmapLevel customBeatmapLevel, StandardLevelInfoSaveData standardLevelInfoSaveData, CancellationToken cancellationToken)
	{
		IDifficultyBeatmapSet[] array = await this.LoadDifficultyBeatmapSetsAsync(customLevelPath, customBeatmapLevel, standardLevelInfoSaveData, cancellationToken);
		IDifficultyBeatmapSet[] difficultyBeatmapSets = array;
		AudioClip audioClip = await customBeatmapLevel.GetPreviewAudioClipAsync(cancellationToken);
		BeatmapLevelData result;
		if (audioClip == null)
		{
			result = null;
		}
		else
		{
			result = new BeatmapLevelData(audioClip, difficultyBeatmapSets);
		}
		return result;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0001C320 File Offset: 0x0001A520
	private async Task<IDifficultyBeatmapSet[]> LoadDifficultyBeatmapSetsAsync(string customLevelPath, CustomBeatmapLevel customBeatmapLevel, StandardLevelInfoSaveData standardLevelInfoSaveData, CancellationToken cancellationToken)
	{
		IDifficultyBeatmapSet[] difficultyBeatmapSets = new IDifficultyBeatmapSet[standardLevelInfoSaveData.difficultyBeatmapSets.Length];
		int num;
		for (int i = 0; i < difficultyBeatmapSets.Length; i = num + 1)
		{
			IDifficultyBeatmapSet difficultyBeatmapSet = await this.LoadDifficultyBeatmapSetAsync(customLevelPath, customBeatmapLevel, standardLevelInfoSaveData, standardLevelInfoSaveData.difficultyBeatmapSets[i], cancellationToken);
			IDifficultyBeatmapSet[] array = difficultyBeatmapSets;
			num = i;
			array[num] = difficultyBeatmapSet;
			num = i;
		}
		return difficultyBeatmapSets;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0001C388 File Offset: 0x0001A588
	private async Task<IDifficultyBeatmapSet> LoadDifficultyBeatmapSetAsync(string customLevelPath, CustomBeatmapLevel customBeatmapLevel, StandardLevelInfoSaveData standardLevelInfoSaveData, StandardLevelInfoSaveData.DifficultyBeatmapSet difficultyBeatmapSetSaveData, CancellationToken cancellationToken)
	{
		BeatmapCharacteristicSO beatmapCharacteristicBySerializedName = this._beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(difficultyBeatmapSetSaveData.beatmapCharacteristicName);
		CustomDifficultyBeatmap[] difficultyBeatmaps = new CustomDifficultyBeatmap[difficultyBeatmapSetSaveData.difficultyBeatmaps.Length];
		CustomDifficultyBeatmapSet difficultyBeatmapSet = new CustomDifficultyBeatmapSet(beatmapCharacteristicBySerializedName);
		int num;
		for (int i = 0; i < difficultyBeatmapSetSaveData.difficultyBeatmaps.Length; i = num + 1)
		{
			CustomDifficultyBeatmap customDifficultyBeatmap = await this.LoadDifficultyBeatmapAsync(customLevelPath, customBeatmapLevel, difficultyBeatmapSet, standardLevelInfoSaveData, difficultyBeatmapSetSaveData.difficultyBeatmaps[i], cancellationToken);
			CustomDifficultyBeatmap[] array = difficultyBeatmaps;
			num = i;
			array[num] = customDifficultyBeatmap;
			num = i;
		}
		difficultyBeatmapSet.SetCustomDifficultyBeatmaps(difficultyBeatmaps);
		return difficultyBeatmapSet;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0001C3F8 File Offset: 0x0001A5F8
	private async Task<CustomDifficultyBeatmap> LoadDifficultyBeatmapAsync(string customLevelPath, CustomBeatmapLevel parentCustomBeatmapLevel, CustomDifficultyBeatmapSet parentDifficultyBeatmapSet, StandardLevelInfoSaveData standardLevelInfoSaveData, StandardLevelInfoSaveData.DifficultyBeatmap difficultyBeatmapSaveData, CancellationToken cancellationToken)
	{
		BeatmapData beatmapData = await this.LoadBeatmapDataAsync(customLevelPath, difficultyBeatmapSaveData.beatmapFilename, standardLevelInfoSaveData, cancellationToken);
		BeatmapDifficulty difficulty;
		difficultyBeatmapSaveData.difficulty.BeatmapDifficultyFromSerializedName(out difficulty);
		return new CustomDifficultyBeatmap(parentCustomBeatmapLevel, parentDifficultyBeatmapSet, difficulty, difficultyBeatmapSaveData.difficultyRank, difficultyBeatmapSaveData.noteJumpMovementSpeed, difficultyBeatmapSaveData.noteJumpStartBeatOffset, beatmapData);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0001C470 File Offset: 0x0001A670
	private async Task<BeatmapData> LoadBeatmapDataAsync(string customLevelPath, string difficultyFileName, StandardLevelInfoSaveData standardLevelInfoSaveData, CancellationToken cancellationToken)
	{
		BeatmapData beatmapData = null;
		await Task.Run(delegate()
		{
			beatmapData = this.LoadBeatmapDataBeatmapData(customLevelPath, difficultyFileName, standardLevelInfoSaveData);
		}, cancellationToken);
		cancellationToken.ThrowIfCancellationRequested();
		return beatmapData;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0001C4D8 File Offset: 0x0001A6D8
	private BeatmapData LoadBeatmapDataBeatmapData(string customLevelPath, string difficultyFileName, StandardLevelInfoSaveData standardLevelInfoSaveData)
	{
		string path = Path.Combine(customLevelPath, difficultyFileName);
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			return this._beatmapDataLoader.GetBeatmapDataFromJson(json, standardLevelInfoSaveData.beatsPerMinute, standardLevelInfoSaveData.shuffle, standardLevelInfoSaveData.shufflePeriod);
		}
		return null;
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0001C51C File Offset: 0x0001A71C
	private EnvironmentInfoSO LoadEnvironmentInfo(string environmentName, bool allDirections)
	{
		EnvironmentInfoSO environmentInfoSO = this._enviromentSceneInfoColection.GetEnviromentInfoBySerializedName(environmentName);
		if (environmentInfoSO == null)
		{
			environmentInfoSO = (allDirections ? this._defaultAllDirectionsEnvironmentInfo : this._defaultEnviromentInfo);
		}
		return environmentInfoSO;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0001C554 File Offset: 0x0001A754
	private async Task<CustomPreviewBeatmapLevel> LoadCustomPreviewBeatmapLevelAsync(string customLevelPath, StandardLevelInfoSaveData standardLevelInfoSaveData, CancellationToken cancellationToken)
	{
		CustomPreviewBeatmapLevel result;
		try
		{
			string levelID = "custom_level_" + new DirectoryInfo(customLevelPath).Name;
			string songName = standardLevelInfoSaveData.songName;
			string songSubName = standardLevelInfoSaveData.songSubName;
			string songAuthorName = standardLevelInfoSaveData.songAuthorName;
			string levelAuthorName = standardLevelInfoSaveData.levelAuthorName;
			float beatsPerMinute = standardLevelInfoSaveData.beatsPerMinute;
			float songTimeOffset = standardLevelInfoSaveData.songTimeOffset;
			float shuffle = standardLevelInfoSaveData.shuffle;
			float shufflePeriod = standardLevelInfoSaveData.shufflePeriod;
			float previewStartTime = standardLevelInfoSaveData.previewStartTime;
			float previewDuration = standardLevelInfoSaveData.previewDuration;
			EnvironmentInfoSO environmentInfo = this.LoadEnvironmentInfo(standardLevelInfoSaveData.environmentName, false);
			EnvironmentInfoSO allDirectionsEnvironmentInfo = this.LoadEnvironmentInfo(standardLevelInfoSaveData.allDirectionsEnvironmentName, true);
			List<PreviewDifficultyBeatmapSet> list = new List<PreviewDifficultyBeatmapSet>();
			foreach (StandardLevelInfoSaveData.DifficultyBeatmapSet difficultyBeatmapSet in standardLevelInfoSaveData.difficultyBeatmapSets)
			{
				BeatmapCharacteristicSO beatmapCharacteristicBySerializedName = this._beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(difficultyBeatmapSet.beatmapCharacteristicName);
				if (beatmapCharacteristicBySerializedName != null)
				{
					BeatmapDifficulty[] array = new BeatmapDifficulty[difficultyBeatmapSet.difficultyBeatmaps.Length];
					for (int j = 0; j < difficultyBeatmapSet.difficultyBeatmaps.Length; j++)
					{
						BeatmapDifficulty beatmapDifficulty;
						difficultyBeatmapSet.difficultyBeatmaps[j].difficulty.BeatmapDifficultyFromSerializedName(out beatmapDifficulty);
						array[j] = beatmapDifficulty;
					}
					list.Add(new PreviewDifficultyBeatmapSet(beatmapCharacteristicBySerializedName, array));
				}
			}
			result = await Task.FromResult<CustomPreviewBeatmapLevel>(new CustomPreviewBeatmapLevel(this._defaultPackCoverTexture2D, standardLevelInfoSaveData, customLevelPath, this._cachedMediaAsyncLoaderSO, this._cachedMediaAsyncLoaderSO, levelID, songName, songSubName, songAuthorName, levelAuthorName, beatsPerMinute, songTimeOffset, shuffle, shufflePeriod, previewStartTime, previewDuration, environmentInfo, allDirectionsEnvironmentInfo, list.ToArray()));
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0001C5AC File Offset: 0x0001A7AC
	private async Task<StandardLevelInfoSaveData> LoadCustomLevelInfoSaveDataAsync(string customLevelPath, CancellationToken cancellationToken)
	{
		StandardLevelInfoSaveData customLevelInfoSaveData = null;
		await Task.Run(delegate()
		{
			customLevelInfoSaveData = this.LoadCustomLevelInfoSaveData(customLevelPath);
		}, cancellationToken);
		cancellationToken.ThrowIfCancellationRequested();
		return customLevelInfoSaveData;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0001C604 File Offset: 0x0001A804
	private StandardLevelInfoSaveData LoadCustomLevelInfoSaveData(string customLevelPath)
	{
		string path = Path.Combine(customLevelPath, "Info.dat");
		if (File.Exists(path))
		{
			return StandardLevelInfoSaveData.DeserializeFromJSONString(File.ReadAllText(path));
		}
		return null;
	}

	// Token: 0x0400028F RID: 655
	[SerializeField]
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x04000290 RID: 656
	[SerializeField]
	private EnvironmentInfoSO _defaultEnviromentInfo;

	// Token: 0x04000291 RID: 657
	[SerializeField]
	private EnvironmentInfoSO _defaultAllDirectionsEnvironmentInfo;

	// Token: 0x04000292 RID: 658
	[SerializeField]
	private EnvironmentsListSO _enviromentSceneInfoColection;

	// Token: 0x04000293 RID: 659
	[SerializeField]
	private Texture2D _defaultPackCoverTexture2D;

	// Token: 0x04000294 RID: 660
	[Inject]
	private CachedMediaAsyncLoader _cachedMediaAsyncLoaderSO;

	// Token: 0x04000295 RID: 661
	public const string kCustomLevelPrefixId = "custom_level_";

	// Token: 0x04000296 RID: 662
	public const string kCustomLevelPackPrefixId = "custom_levelpack_";

	// Token: 0x04000297 RID: 663
	private BeatmapDataLoader _beatmapDataLoader = new BeatmapDataLoader();

	// Token: 0x020000A2 RID: 162
	public struct CustomPackFolderInfo
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00003A1A File Offset: 0x00001C1A
		public CustomPackFolderInfo(string folderName, string packName)
		{
			this.folderName = folderName;
			this.packName = packName;
		}

		// Token: 0x04000298 RID: 664
		public readonly string folderName;

		// Token: 0x04000299 RID: 665
		public readonly string packName;
	}
}
