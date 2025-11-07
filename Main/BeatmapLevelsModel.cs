using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000144 RID: 324
public class BeatmapLevelsModel : MonoBehaviour
{
	// Token: 0x1400000E RID: 14
	// (add) Token: 0x060004F2 RID: 1266 RVA: 0x000226A4 File Offset: 0x000208A4
	// (remove) Token: 0x060004F3 RID: 1267 RVA: 0x000226DC File Offset: 0x000208DC
	public event Action<BeatmapLevelsModel.LevelDownloadingUpdate> levelDownloadingUpdateEvent;

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00005040 File Offset: 0x00003240
	public BeatmapLevelPackCollectionSO ostAndExtrasPackCollection
	{
		get
		{
			return this._ostAndExtrasPackCollection;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00005048 File Offset: 0x00003248
	public IBeatmapLevelPackCollection dlcBeatmapLevelPackCollection
	{
		get
		{
			return this._dlcLevelPackCollectionContainer.beatmapLevelPackCollection;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00005055 File Offset: 0x00003255
	public IBeatmapLevelPackCollection allLoadedBeatmapLevelPackCollection
	{
		get
		{
			return this._allLoadedBeatmapLevelPackCollection;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000505D File Offset: 0x0000325D
	public IBeatmapLevelPackCollection customLevelPackCollection
	{
		get
		{
			return this._customLevelPackCollection;
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00005065 File Offset: 0x00003265
	[Inject]
	private void Init()
	{
		this._beatmapLevelLoader = new BeatmapLevelLoader(this._beatmapLevelDataLoader, this._beatmapDataAssetFileModel);
		this._loadedBeatmapLevels = new HMCache<string, IBeatmapLevel>(this._maxCachedBeatmapLevels);
		this.UpdateLoadedPreviewLevels();
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00005095 File Offset: 0x00003295
	protected void Start()
	{
		this._beatmapDataAssetFileModel.levelDataAssetDownloadUpdateEvent += this.HandleLevelDataAssetDownloadUpdate;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x000050AE File Offset: 0x000032AE
	protected void OnDestroy()
	{
		this._beatmapDataAssetFileModel.levelDataAssetDownloadUpdateEvent -= this.HandleLevelDataAssetDownloadUpdate;
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x000050C7 File Offset: 0x000032C7
	public void ClearLoadedBeatmapLevelsCaches()
	{
		this._customLevelLoader.ClearCache();
		this._loadedBeatmapLevels.Clear();
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00022714 File Offset: 0x00020914
	public async Task<IBeatmapLevelPackCollection> GetCustomLevelPackCollectionAsync(CancellationToken cancellationToken)
	{
		CustomLevelLoader.CustomPackFolderInfo[] collection = await this._customLevelLoader.GetSubFoldersInfosAsync("CustomLevels", cancellationToken);
		List<CustomLevelLoader.CustomPackFolderInfo> list = new List<CustomLevelLoader.CustomPackFolderInfo>(new CustomLevelLoader.CustomPackFolderInfo[]
		{
			new CustomLevelLoader.CustomPackFolderInfo("CustomLevels", Localization.Get("TITLE_CUSTOM_LEVELS"))
		});
		list.AddRange(collection);
		CustomBeatmapLevelPack[] beatmapLevelPacks = await this._customLevelLoader.LoadCustomPreviewBeatmapLevelPacksAsync(list.ToArray(), cancellationToken);
		this._customLevelPackCollection = new BeatmapLevelPackCollection(beatmapLevelPacks);
		this.UpdateLoadedPreviewLevels();
		return this._customLevelPackCollection;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00022764 File Offset: 0x00020964
	public IBeatmapLevelPack GetLevelPackForLevelId(string levelId)
	{
		foreach (IBeatmapLevelPack beatmapLevelPack in this._allLoadedBeatmapLevelPackCollection.beatmapLevelPacks)
		{
			IPreviewBeatmapLevel[] beatmapLevels = beatmapLevelPack.beatmapLevelCollection.beatmapLevels;
			for (int j = 0; j < beatmapLevels.Length; j++)
			{
				if (beatmapLevels[j].levelID == levelId)
				{
					return beatmapLevelPack;
				}
			}
		}
		return null;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x000227C4 File Offset: 0x000209C4
	public IBeatmapLevelPack GetLevelPack(string levePacklId)
	{
		foreach (IBeatmapLevelPack beatmapLevelPack in this._allLoadedBeatmapLevelPackCollection.beatmapLevelPacks)
		{
			if (beatmapLevelPack.packID == levePacklId)
			{
				return beatmapLevelPack;
			}
		}
		return null;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000050DF File Offset: 0x000032DF
	public bool IsBeatmapLevelLoaded(string levelId)
	{
		return (this._loadedPreviewBeatmapLevels.ContainsKey(levelId) && this._loadedPreviewBeatmapLevels[levelId] is IBeatmapLevel) || this._loadedBeatmapLevels.IsInCache(levelId);
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00022800 File Offset: 0x00020A00
	public IBeatmapLevel GetBeatmapLevelIfLoaded(string levelId)
	{
		if (this._loadedPreviewBeatmapLevels.ContainsKey(levelId))
		{
			IPreviewBeatmapLevel previewBeatmapLevel = this._loadedPreviewBeatmapLevels[levelId];
			if (previewBeatmapLevel is IBeatmapLevel)
			{
				return (IBeatmapLevel)previewBeatmapLevel;
			}
		}
		return this._loadedBeatmapLevels.GetFromCache(levelId);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00022844 File Offset: 0x00020A44
	public async Task<BeatmapLevelsModel.GetBeatmapLevelResult> GetBeatmapLevelAsync(string levelID, CancellationToken cancellationToken)
	{
		TaskAwaiter<AdditionalContentModel.EntitlementStatus> taskAwaiter = this._additionalContentModel.GetLevelEntitlementStatusAsync(levelID, cancellationToken).GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			taskAwaiter.GetResult();
			TaskAwaiter<AdditionalContentModel.EntitlementStatus> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<AdditionalContentModel.EntitlementStatus>);
		}
		if (taskAwaiter.GetResult() == AdditionalContentModel.EntitlementStatus.Owned)
		{
			if (this._loadedBeatmapLevels.IsInCache(levelID))
			{
				return new BeatmapLevelsModel.GetBeatmapLevelResult(false, this._loadedBeatmapLevels.GetFromCache(levelID));
			}
			if (!this._loadedPreviewBeatmapLevels.ContainsKey(levelID))
			{
				return new BeatmapLevelsModel.GetBeatmapLevelResult(true, null);
			}
			IPreviewBeatmapLevel previewBeatmapLevel = this._loadedPreviewBeatmapLevels[levelID];
			if (previewBeatmapLevel is IBeatmapLevel)
			{
				return new BeatmapLevelsModel.GetBeatmapLevelResult(false, (IBeatmapLevel)previewBeatmapLevel);
			}
			if (previewBeatmapLevel is CustomPreviewBeatmapLevel)
			{
				CustomPreviewBeatmapLevel customPreviewBeatmapLevel = (CustomPreviewBeatmapLevel)previewBeatmapLevel;
				CustomBeatmapLevel customBeatmapLevel = await this._customLevelLoader.LoadCustomBeatmapLevelAsync(customPreviewBeatmapLevel, cancellationToken);
				if (customBeatmapLevel == null || customBeatmapLevel.beatmapLevelData == null)
				{
					return new BeatmapLevelsModel.GetBeatmapLevelResult(true, null);
				}
				this._loadedBeatmapLevels.PutToCache(levelID, customBeatmapLevel);
				return new BeatmapLevelsModel.GetBeatmapLevelResult(false, customBeatmapLevel);
			}
			else
			{
				BeatmapLevelLoader.LoadBeatmapLevelResult loadBeatmapLevelResult = await this._beatmapLevelLoader.LoadBeatmapLevelAsync(previewBeatmapLevel, cancellationToken);
				if (loadBeatmapLevelResult.isError)
				{
					return new BeatmapLevelsModel.GetBeatmapLevelResult(true, null);
				}
				if (loadBeatmapLevelResult.beatmapLevel != null)
				{
					this._loadedBeatmapLevels.PutToCache(levelID, loadBeatmapLevelResult.beatmapLevel);
					return new BeatmapLevelsModel.GetBeatmapLevelResult(false, loadBeatmapLevelResult.beatmapLevel);
				}
			}
		}
		return new BeatmapLevelsModel.GetBeatmapLevelResult(true, null);
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0002289C File Offset: 0x00020A9C
	private void HandleLevelDataAssetDownloadUpdate(LevelDataAssetDownloadUpdate update)
	{
		BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState downloadingState = BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState.PreparingToDownload;
		switch (update.assetDownloadingState)
		{
		case LevelDataAssetDownloadUpdate.AssetDownloadingState.PreparingToDownload:
			downloadingState = BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState.PreparingToDownload;
			break;
		case LevelDataAssetDownloadUpdate.AssetDownloadingState.Downloading:
			downloadingState = BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState.Downloading;
			break;
		case LevelDataAssetDownloadUpdate.AssetDownloadingState.Completed:
			downloadingState = BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState.Completed;
			break;
		}
		Action<BeatmapLevelsModel.LevelDownloadingUpdate> action = this.levelDownloadingUpdateEvent;
		if (action == null)
		{
			return;
		}
		action(new BeatmapLevelsModel.LevelDownloadingUpdate(update.levelID, update.bytesTotal, update.bytesTransferred, downloadingState));
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000228F8 File Offset: 0x00020AF8
	private void UpdateLoadedPreviewLevels()
	{
		this.UpdateAllLoadedBeatmapLevelPacks();
		IBeatmapLevelPack[] beatmapLevelPacks = this._allLoadedBeatmapLevelPackCollection.beatmapLevelPacks;
		for (int i = 0; i < beatmapLevelPacks.Length; i++)
		{
			foreach (IPreviewBeatmapLevel previewBeatmapLevel in beatmapLevelPacks[i].beatmapLevelCollection.beatmapLevels)
			{
				this._loadedPreviewBeatmapLevels[previewBeatmapLevel.levelID] = previewBeatmapLevel;
			}
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0002295C File Offset: 0x00020B5C
	private void UpdateAllLoadedBeatmapLevelPacks()
	{
		List<IBeatmapLevelPack> list = new List<IBeatmapLevelPack>(this._ostAndExtrasPackCollection.beatmapLevelPacks);
		list.AddRange(this._dlcLevelPackCollectionContainer.beatmapLevelPackCollection.beatmapLevelPacks);
		if (this._customLevelPackCollection != null)
		{
			list.AddRange(this._customLevelPackCollection.beatmapLevelPacks);
		}
		this._allLoadedBeatmapLevelPackCollection = new BeatmapLevelPackCollection(list.ToArray());
	}

	// Token: 0x04000537 RID: 1335
	[SerializeField]
	private BeatmapLevelPackCollectionContainerSO _dlcLevelPackCollectionContainer;

	// Token: 0x04000538 RID: 1336
	[SerializeField]
	private BeatmapLevelPackCollectionSO _ostAndExtrasPackCollection;

	// Token: 0x04000539 RID: 1337
	[SerializeField]
	private BeatmapLevelDataLoaderSO _beatmapLevelDataLoader;

	// Token: 0x0400053A RID: 1338
	[SerializeField]
	private int _maxCachedBeatmapLevels = 30;

	// Token: 0x0400053B RID: 1339
	[Inject]
	private CustomLevelLoader _customLevelLoader;

	// Token: 0x0400053C RID: 1340
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x0400053D RID: 1341
	[Inject]
	private IBeatmapDataAssetFileModel _beatmapDataAssetFileModel;

	// Token: 0x0400053F RID: 1343
	private IBeatmapLevelPackCollection _allLoadedBeatmapLevelPackCollection;

	// Token: 0x04000540 RID: 1344
	private IBeatmapLevelPackCollection _customLevelPackCollection;

	// Token: 0x04000541 RID: 1345
	private HMCache<string, IBeatmapLevel> _loadedBeatmapLevels;

	// Token: 0x04000542 RID: 1346
	private Dictionary<string, IPreviewBeatmapLevel> _loadedPreviewBeatmapLevels = new Dictionary<string, IPreviewBeatmapLevel>();

	// Token: 0x04000543 RID: 1347
	private BeatmapLevelLoader _beatmapLevelLoader;

	// Token: 0x02000145 RID: 325
	public struct GetBeatmapLevelResult
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0000512B File Offset: 0x0000332B
		public GetBeatmapLevelResult(bool isError, IBeatmapLevel beatmapLevel)
		{
			this.isError = isError;
			this.beatmapLevel = beatmapLevel;
		}

		// Token: 0x04000544 RID: 1348
		public readonly bool isError;

		// Token: 0x04000545 RID: 1349
		public readonly IBeatmapLevel beatmapLevel;
	}

	// Token: 0x02000146 RID: 326
	public struct LevelDownloadingUpdate
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000513B File Offset: 0x0000333B
		public LevelDownloadingUpdate(string levelID, uint bytesTotal, uint bytesTransferred, BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState downloadingState)
		{
			this.levelID = levelID;
			this.bytesTotal = bytesTotal;
			this.bytesTransferred = bytesTransferred;
			this.downloadingState = downloadingState;
		}

		// Token: 0x04000546 RID: 1350
		public readonly string levelID;

		// Token: 0x04000547 RID: 1351
		public readonly uint bytesTotal;

		// Token: 0x04000548 RID: 1352
		public readonly uint bytesTransferred;

		// Token: 0x04000549 RID: 1353
		public readonly BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState downloadingState;

		// Token: 0x02000147 RID: 327
		public enum DownloadingState
		{
			// Token: 0x0400054B RID: 1355
			PreparingToDownload,
			// Token: 0x0400054C RID: 1356
			Downloading,
			// Token: 0x0400054D RID: 1357
			Completed
		}
	}
}
