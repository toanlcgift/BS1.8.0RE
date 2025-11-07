using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class BeatmapLevelDataLoaderSO : PersistentScriptableObject
{
	// Token: 0x06000455 RID: 1109 RVA: 0x000210FC File Offset: 0x0001F2FC
	public async Task<IBeatmapLevel> LoadBeatmapLevelFormAssetBundleAsync(IPreviewBeatmapLevel previewBeatmapLevel, string assetBundlePath, string levelDataAssetName, CancellationToken cancellationToken)
	{
		string levelID = previewBeatmapLevel.levelID;
		if (!this._bundleLevelInfos.ContainsKey(levelID))
		{
			BeatmapLevelDataLoaderSO.AssetBundleLevelInfo value = new BeatmapLevelDataLoaderSO.AssetBundleLevelInfo(assetBundlePath, levelDataAssetName, previewBeatmapLevel);
			this._bundleLevelInfos[levelID] = value;
		}
		if (this._beatmapLevelsAsyncCache == null)
		{
			this._beatmapLevelsAsyncCache = new AsyncCache<string, IBeatmapLevel>(new Func<string, Task<IBeatmapLevel>>(this.LoadBeatmapLevelAsync));
		}
		IBeatmapLevel beatmapLevel = await this._beatmapLevelsAsyncCache[levelID];
		if (beatmapLevel == null)
		{
			this._beatmapLevelsAsyncCache.RemoveKey(levelID);
		}
		cancellationToken.ThrowIfCancellationRequested();
		return beatmapLevel;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00021164 File Offset: 0x0001F364
	private async Task<IBeatmapLevel> LoadBeatmapLevelAsync(string levelID)
	{
		IBeatmapLevel result;
		if (!this._bundleLevelInfos.ContainsKey(levelID))
		{
			result = null;
		}
		else
		{
			try
			{
				BeatmapLevelDataSO beatmapLevelData = await this.LoadBeatmalLevelDataAsync(this._bundleLevelInfos[levelID].assetBundlePath, this._bundleLevelInfos[levelID].levelDataAssetName);
				BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview beatmapLevelFromPreview = new BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview(this._bundleLevelInfos[levelID].previewBeatmapLevel);
				beatmapLevelFromPreview.LoadData(this._allBeatmapCharacteristicCollection, beatmapLevelData);
				result = beatmapLevelFromPreview;
			}
			catch
			{
				result = null;
			}
		}
		return result;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x000211B4 File Offset: 0x0001F3B4
	private async Task<BeatmapLevelDataSO> LoadBeatmalLevelDataAsync(string assetBundlePath, string levelDataAssetName)
	{
		TaskCompletionSource<BeatmapLevelDataSO> taskSource = new TaskCompletionSource<BeatmapLevelDataSO>();
		AssetBundleCreateRequest asetBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
		asetBundleCreateRequest.completed += delegate(AsyncOperation asyncOperation)
		{
			AssetBundle assetBundle = asetBundleCreateRequest.assetBundle;
			try
			{
				AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync<BeatmapLevelDataSO>(levelDataAssetName);
				assetBundleRequest.completed += delegate(AsyncOperation asyncOperation2)
				{
					BeatmapLevelDataSO beatmapLevelDataSO = (BeatmapLevelDataSO)assetBundleRequest.asset;
					if (beatmapLevelDataSO == null || beatmapLevelDataSO.audioClip == null)
					{
						assetBundle.Unload(true);
					}
					taskSource.TrySetResult(beatmapLevelDataSO);
				};
			}
			catch
			{
				taskSource.TrySetResult(null);
			}
		};
		return await taskSource.Task;
	}

	// Token: 0x04000499 RID: 1177
	[SerializeField]
	private BeatmapCharacteristicCollectionSO _allBeatmapCharacteristicCollection;

	// Token: 0x0400049A RID: 1178
	private AsyncCache<string, IBeatmapLevel> _beatmapLevelsAsyncCache;

	// Token: 0x0400049B RID: 1179
	private Dictionary<string, BeatmapLevelDataLoaderSO.AssetBundleLevelInfo> _bundleLevelInfos = new Dictionary<string, BeatmapLevelDataLoaderSO.AssetBundleLevelInfo>();

	// Token: 0x02000119 RID: 281
	private struct AssetBundleLevelInfo
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00004AE2 File Offset: 0x00002CE2
		public AssetBundleLevelInfo(string assetBundlePath, string levelDataAssetName, IPreviewBeatmapLevel previewBeatmapLevel)
		{
			this.assetBundlePath = assetBundlePath;
			this.levelDataAssetName = levelDataAssetName;
			this.previewBeatmapLevel = previewBeatmapLevel;
		}

		// Token: 0x0400049C RID: 1180
		public readonly string assetBundlePath;

		// Token: 0x0400049D RID: 1181
		public readonly string levelDataAssetName;

		// Token: 0x0400049E RID: 1182
		public readonly IPreviewBeatmapLevel previewBeatmapLevel;
	}

	// Token: 0x0200011A RID: 282
	private class BeatmapLevelFromPreview : IBeatmapLevel, IPreviewBeatmapLevel
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00004AF9 File Offset: 0x00002CF9
		public string levelID
		{
			get
			{
				return this._level.levelID;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00004B06 File Offset: 0x00002D06
		public string songName
		{
			get
			{
				return this._level.songName;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00004B13 File Offset: 0x00002D13
		public string songSubName
		{
			get
			{
				return this._level.songSubName;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00004B20 File Offset: 0x00002D20
		public string songAuthorName
		{
			get
			{
				return this._level.songAuthorName;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00004B2D File Offset: 0x00002D2D
		public string levelAuthorName
		{
			get
			{
				return this._level.levelAuthorName;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00004B3A File Offset: 0x00002D3A
		public float beatsPerMinute
		{
			get
			{
				return this._level.beatsPerMinute;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00004B47 File Offset: 0x00002D47
		public float songTimeOffset
		{
			get
			{
				return this._level.songTimeOffset;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00004B54 File Offset: 0x00002D54
		public float songDuration
		{
			get
			{
				return this._level.songDuration;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00004B61 File Offset: 0x00002D61
		public float shuffle
		{
			get
			{
				return this._level.shuffle;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00004B6E File Offset: 0x00002D6E
		public float shufflePeriod
		{
			get
			{
				return this._level.shufflePeriod;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00004B7B File Offset: 0x00002D7B
		public float previewStartTime
		{
			get
			{
				return this._level.previewStartTime;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00004B88 File Offset: 0x00002D88
		public float previewDuration
		{
			get
			{
				return this._level.previewDuration;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00004B95 File Offset: 0x00002D95
		public EnvironmentInfoSO environmentInfo
		{
			get
			{
				return this._level.environmentInfo;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00004BA2 File Offset: 0x00002DA2
		public EnvironmentInfoSO allDirectionsEnvironmentInfo
		{
			get
			{
				return this._level.allDirectionsEnvironmentInfo;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00004BAF File Offset: 0x00002DAF
		public PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets
		{
			get
			{
				return this._level.previewDifficultyBeatmapSets;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00004BBC File Offset: 0x00002DBC
		public IBeatmapLevelData beatmapLevelData
		{
			get
			{
				return this._beatmapLevelData;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00021204 File Offset: 0x0001F404
		public async Task<AudioClip> GetPreviewAudioClipAsync(CancellationToken cancellationToken)
		{
			return await this._level.GetPreviewAudioClipAsync(cancellationToken);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00021254 File Offset: 0x0001F454
		public async Task<Texture2D> GetCoverImageTexture2DAsync(CancellationToken cancellationToken)
		{
			return await this._level.GetCoverImageTexture2DAsync(cancellationToken);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public BeatmapLevelFromPreview(IPreviewBeatmapLevel previewLevel)
		{
			this._level = previewLevel;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00004BD3 File Offset: 0x00002DD3
		public void LoadData(BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection, BeatmapLevelDataSO beatmapLevelData)
		{
			this._beatmapLevelData = new BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview.BeatmapLevelData(beatmapLevelData, beatmapCharacteristicCollection, this);
		}

		// Token: 0x0400049F RID: 1183
		private IPreviewBeatmapLevel _level;

		// Token: 0x040004A0 RID: 1184
		private BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview.BeatmapLevelData _beatmapLevelData;

		// Token: 0x0200011B RID: 283
		private class BeatmapLevelData : IBeatmapLevelData
		{
			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600046E RID: 1134 RVA: 0x00004BE3 File Offset: 0x00002DE3
			public AudioClip audioClip
			{
				get
				{
					return this._audioClip;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x000212A4 File Offset: 0x0001F4A4
			public IDifficultyBeatmapSet[] difficultyBeatmapSets
			{
				get
				{
					return this._difficultyBeatmapSets;
				}
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x000212BC File Offset: 0x0001F4BC
			public BeatmapLevelData(BeatmapLevelDataSO beatmapLevelData, BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection, IBeatmapLevel parentLevel)
			{
				this._audioClip = beatmapLevelData.audioClip;
				this._difficultyBeatmapSets = new BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview.BeatmapLevelData.DifficultyBeatmapSet[beatmapLevelData.difficultyBeatmapSets.Length];
				for (int i = 0; i < beatmapLevelData.difficultyBeatmapSets.Length; i++)
				{
					this._difficultyBeatmapSets[i] = new BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview.BeatmapLevelData.DifficultyBeatmapSet(beatmapLevelData.difficultyBeatmapSets[i], beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(beatmapLevelData.difficultyBeatmapSets[i].beatmapCharacteristicSerializedName), parentLevel);
				}
			}

			// Token: 0x040004A1 RID: 1185
			private AudioClip _audioClip;

			// Token: 0x040004A2 RID: 1186
			private BeatmapLevelDataLoaderSO.BeatmapLevelFromPreview.BeatmapLevelData.DifficultyBeatmapSet[] _difficultyBeatmapSets;

			// Token: 0x0200011C RID: 284
			public class DifficultyBeatmapSet : IDifficultyBeatmapSet
			{
				// Token: 0x170000D8 RID: 216
				// (get) Token: 0x06000471 RID: 1137 RVA: 0x00004BEB File Offset: 0x00002DEB
				public BeatmapCharacteristicSO beatmapCharacteristic
				{
					get
					{
						return this._beatmapCharacteristic;
					}
				}

				// Token: 0x170000D9 RID: 217
				// (get) Token: 0x06000472 RID: 1138 RVA: 0x0002132C File Offset: 0x0001F52C
				public IDifficultyBeatmap[] difficultyBeatmaps
				{
					get
					{
						return this._difficultyBeatmapSet.difficultyBeatmaps;
					}
				}

				// Token: 0x06000473 RID: 1139 RVA: 0x00021348 File Offset: 0x0001F548
				public DifficultyBeatmapSet(BeatmapLevelDataSO.DifficultyBeatmapSet difficultyBeatmapSet, BeatmapCharacteristicSO beatmapCharacteristicSerializedName, IBeatmapLevel parentLevel)
				{
					this._difficultyBeatmapSet = difficultyBeatmapSet;
					this._beatmapCharacteristic = beatmapCharacteristicSerializedName;
					foreach (BeatmapLevelSO.DifficultyBeatmap difficultyBeatmap in this._difficultyBeatmapSet.difficultyBeatmaps)
					{
						BeatmapLevelSO.DifficultyBeatmap[] difficultyBeatmaps2 = difficultyBeatmapSet.difficultyBeatmaps;
						for (int j = 0; j < difficultyBeatmaps2.Length; j++)
						{
							difficultyBeatmaps2[j].SetParents(parentLevel, this);
						}
					}
				}

				// Token: 0x040004A3 RID: 1187
				private BeatmapCharacteristicSO _beatmapCharacteristic;

				// Token: 0x040004A4 RID: 1188
				private BeatmapLevelSO.DifficultyBeatmap[] _difficultyBeatmaps;

				// Token: 0x040004A5 RID: 1189
				private BeatmapLevelDataSO.DifficultyBeatmapSet _difficultyBeatmapSet;
			}
		}
	}
}
