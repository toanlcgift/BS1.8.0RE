using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Polyglot;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000538 RID: 1336
	public class EditorStandardLevelProjectSO : PersistentScriptableObject
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00012FE1 File Offset: 0x000111E1
		public bool beatmapIsInitialized
		{
			get
			{
				return this._editorBeatmap.hasBeatsData;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x00012FEE File Offset: 0x000111EE
		public bool canSaveProject
		{
			get
			{
				return this._editorAudio.isAudioLoaded;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00012FFB File Offset: 0x000111FB
		public EditorStandardLevelInfoSO levelInfo
		{
			get
			{
				return this._levelInfo;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x00013003 File Offset: 0x00011203
		public string openedProjectDirectoryPath
		{
			get
			{
				return this._openedProjectDirectoryPath;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0001300B File Offset: 0x0001120B
		protected override void OnEnable()
		{
			base.OnEnable();
			this._activeCharacteristicDifficulty.didChangeEvent += this.HandleActiveCharacteristicDifficultyDidChange;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0001302A File Offset: 0x0001122A
		protected void OnDisable()
		{
			if (this._activeCharacteristicDifficulty != null)
			{
				this._activeCharacteristicDifficulty.didChangeEvent -= this.HandleActiveCharacteristicDifficultyDidChange;
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00059B4C File Offset: 0x00057D4C
		public void InitNewProject()
		{
			this._editorBeatmap.InitWithEmptyData(1);
			this._editorAudio.Clear();
			this._coverImage.Clear();
			this._levelInfo.SetDefaults();
			this._environment.SetDefaults();
			this._allDirectionsEnvironment.SetDefaults();
			this._songParams.SetDefaults();
			this._beatsDataSet.Clear();
			this._activeCharacteristicDifficulty.characteristicDifficulty = new BeatmapCharacteristicDifficulty
			{
				difficulty = BeatmapDifficulty.Expert,
				characteristicSerializedName = this._defaultBeatmapCharacteristic.serializedName
			};
			this._openedProjectDirectoryPath = null;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00059BE8 File Offset: 0x00057DE8
		public void CheckObstacleCollisions(out string resultMessage)
		{
			resultMessage = "";
			foreach (BeatmapCharacteristicSO beatmapCharacteristicSO in this._beatmapCharacteristicCollection.beatmapCharacteristics)
			{
				foreach (object obj in Enum.GetValues(typeof(BeatmapDifficulty)))
				{
					BeatmapDifficulty difficulty = (BeatmapDifficulty)obj;
					BeatmapCharacteristicDifficulty beatmapCharacteristicDifficulty = new BeatmapCharacteristicDifficulty
					{
						difficulty = difficulty,
						characteristicSerializedName = beatmapCharacteristicSO.serializedName
					};
					EditorBeatsData editorBeatsData = this._beatsDataSet[beatmapCharacteristicDifficulty];
					if (editorBeatsData != null)
					{
						HashSet<int> hashSet = editorBeatsData.FindBeatsWithObstacleCollision();
						if (hashSet.Count > 0)
						{
							resultMessage += string.Format("Collision with obstacles in beat ({0} {1}):", Localization.Get(beatmapCharacteristicSO.characteristicNameLocalizationKey), difficulty.Name());
							foreach (int num in hashSet)
							{
								resultMessage = resultMessage + " " + num;
							}
							resultMessage += ".";
						}
					}
				}
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00013051 File Offset: 0x00011251
		public IEnumerator SaveProjectCoroutine(string projectDirectoryPath, bool saveAudio, Action<bool, string> finishCallback)
		{
			this._beatsDataSet[this._activeCharacteristicDifficulty.characteristicDifficulty] = this._editorBeatmap.beatsData.Clone();
			List<BeatmapSaveData> beatmapSaveDataList = new List<BeatmapSaveData>();
			List<StandardLevelInfoSaveData.DifficultyBeatmap> allDifficultyBeatmaps = new List<StandardLevelInfoSaveData.DifficultyBeatmap>();
			List<StandardLevelInfoSaveData.DifficultyBeatmapSet> list = new List<StandardLevelInfoSaveData.DifficultyBeatmapSet>();
			foreach (BeatmapCharacteristicSO beatmapCharacteristicSO in this._beatmapCharacteristicCollection.beatmapCharacteristics)
			{
				List<StandardLevelInfoSaveData.DifficultyBeatmap> list2 = new List<StandardLevelInfoSaveData.DifficultyBeatmap>();
				foreach (object obj in Enum.GetValues(typeof(BeatmapDifficulty)))
				{
					BeatmapDifficulty difficulty = (BeatmapDifficulty)obj;
					string text = difficulty.SerializedName();
					string text2 = beatmapCharacteristicSO.compoundIdPartName + text + ".dat";
					string path = Path.Combine(projectDirectoryPath, text2);
					BeatmapCharacteristicDifficulty beatmapCharacteristicDifficulty = new BeatmapCharacteristicDifficulty
					{
						difficulty = difficulty,
						characteristicSerializedName = beatmapCharacteristicSO.serializedName
					};
					EditorBeatsData editorBeatsData = this._beatsDataSet[beatmapCharacteristicDifficulty];
					if (editorBeatsData == null)
					{
						if (File.Exists(path))
						{
							File.Delete(path);
						}
					}
					else
					{
						BeatmapSaveData beatmapSaveData = editorBeatsData.ConvertToBeatmapSaveData(this._songParams.beatsPerMinute, true, this._editorAudio.songDuration);
						if (beatmapSaveData == null)
						{
							if (File.Exists(path))
							{
								File.Delete(path);
							}
						}
						else
						{
							StandardLevelInfoSaveData.DifficultyBeatmap item = new StandardLevelInfoSaveData.DifficultyBeatmap(text, difficulty.DefaultRating(), text2, editorBeatsData.noteJumpMovementSpeed, editorBeatsData.noteJumpStartBeatOffset);
							list2.Add(item);
							beatmapSaveDataList.Add(beatmapSaveData);
						}
					}
				}
				allDifficultyBeatmaps.AddRange(list2);
				if (list2.Count > 0)
				{
					StandardLevelInfoSaveData.DifficultyBeatmapSet item2 = new StandardLevelInfoSaveData.DifficultyBeatmapSet(beatmapCharacteristicSO.serializedName, list2.ToArray());
					list.Add(item2);
				}
			}
			StandardLevelInfoSaveData levelSaveData = new StandardLevelInfoSaveData(this._levelInfo.songName, this._levelInfo.songSubName, this._levelInfo.songAuthorName, this._levelInfo.levelAuthorName, this._songParams.beatsPerMinute, this._songParams.songTimeOffset, this._songParams.shuffleStrength, this._songParams.shufflePeriod, this._songParams.previewStartTime, this._songParams.previewDuration, this._editorAudio.audioFileName, this._coverImage.imageFileName, this._environment.value.serializedName, this._allDirectionsEnvironment.value.serializedName, list.ToArray());
			yield return null;
			if (!Directory.Exists(projectDirectoryPath))
			{
				Directory.CreateDirectory(projectDirectoryPath);
			}
			string allBackupsDirectoryPath = Path.Combine(projectDirectoryPath, "Backups");
			if (!Directory.Exists(allBackupsDirectoryPath))
			{
				Directory.CreateDirectory(allBackupsDirectoryPath);
			}
			yield return null;
			string thisBackupsDirectoryPath = Path.Combine(allBackupsDirectoryPath, DateTime.Now.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ss"));
			if (!Directory.Exists(thisBackupsDirectoryPath))
			{
				Directory.CreateDirectory(thisBackupsDirectoryPath);
			}
			yield return null;
			foreach (string text3 in FileHelpers.GetFilePaths(projectDirectoryPath, new HashSet<string>
			{
				"dat"
			}))
			{
				string destFileName = Path.Combine(thisBackupsDirectoryPath, Path.GetFileName(text3));
				File.Copy(text3, destFileName, false);
			}
			DirectoryInfo[] directories = new DirectoryInfo(allBackupsDirectoryPath).GetDirectories();
			if (directories.Length > 7)
			{
				Array.Sort<DirectoryInfo>(directories, (DirectoryInfo dir1, DirectoryInfo dir2) => dir1.CreationTime.CompareTo(dir2.CreationTime));
				for (int j = 0; j < directories.Length - 7; j++)
				{
					DirectoryInfo directoryInfo = directories[j];
					foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
					{
						fileInfo.Delete();
					}
					foreach (DirectoryInfo directoryInfo2 in directoryInfo.EnumerateDirectories())
					{
						directoryInfo2.Delete(true);
					}
					directoryInfo.Delete();
				}
			}
			foreach (string text4 in FileHelpers.GetFilePaths(projectDirectoryPath, new HashSet<string>
			{
				"wav",
				"ogg"
			}))
			{
				if (text4 != this._editorAudio.audioFilePath)
				{
					File.Delete(text4);
				}
			}
			yield return null;
			foreach (string text5 in FileHelpers.GetFilePaths(projectDirectoryPath, new HashSet<string>
			{
				"png"
			}))
			{
				if (text5 != this._coverImage.imageFilePath)
				{
					File.Delete(text5);
				}
			}
			yield return null;
			string text6 = Path.Combine(projectDirectoryPath, "Info.dat");
			FileHelpers.SaveToJSONFile(levelSaveData, text6, text6 + ".tmp", null);
			yield return null;
			for (int k = 0; k < allDifficultyBeatmaps.Count; k++)
			{
				string text7 = Path.Combine(projectDirectoryPath, allDifficultyBeatmaps[k].beatmapFilename);
				FileHelpers.SaveToJSONFile(beatmapSaveDataList[k], text7, text7 + ".tmp", null);
			}
			yield return null;
			string text8 = Path.Combine(projectDirectoryPath, this._editorAudio.audioFileName);
			if (saveAudio && this._editorAudio.audioFilePath != text8)
			{
				File.Copy(this._editorAudio.audioFilePath, text8, true);
			}
			yield return null;
			if (this._coverImage.texture)
			{
				string text9 = Path.Combine(projectDirectoryPath, this._coverImage.imageFileName);
				if (this._coverImage.imageFilePath != text9)
				{
					File.WriteAllBytes(text9, this._coverImage.texture.EncodeToPNG());
				}
			}
			this._openedProjectDirectoryPath = projectDirectoryPath;
			string arg;
			this.CheckObstacleCollisions(out arg);
			finishCallback(true, arg);
			yield break;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00013075 File Offset: 0x00011275
		public IEnumerator OpenProjectCoroutine(string projectDirectoryPath, Action<string> finishCallback)
		{
			string stringData = File.ReadAllText(Path.Combine(projectDirectoryPath, "Info.dat"));
			StandardLevelInfoSaveData levelSaveData = StandardLevelInfoSaveData.DeserializeFromJSONString(stringData);
			if (levelSaveData == null || !levelSaveData.hasAllData)
			{
				finishCallback("Info.dat could not be loaded.");
				yield break;
			}
			yield return null;
			this._levelInfo.SetValues(levelSaveData.songName, levelSaveData.songSubName, levelSaveData.songAuthorName, levelSaveData.levelAuthorName);
			this._songParams.SetValues(levelSaveData.beatsPerMinute, levelSaveData.songTimeOffset, levelSaveData.shuffle, levelSaveData.shufflePeriod, levelSaveData.previewStartTime, levelSaveData.previewDuration);
			this._environment.SetValues(levelSaveData.environmentName);
			this._beatsDataSet.Clear();
			foreach (StandardLevelInfoSaveData.DifficultyBeatmapSet difficultyBeatmapSet in levelSaveData.difficultyBeatmapSets)
			{
				foreach (StandardLevelInfoSaveData.DifficultyBeatmap difficultyBeatmap in difficultyBeatmapSet.difficultyBeatmaps)
				{
					BeatmapSaveData beatmapSaveData = FileHelpers.LoadFromJSONFile<BeatmapSaveData>(Path.Combine(projectDirectoryPath, difficultyBeatmap.beatmapFilename), null);
					if (beatmapSaveData == null)
					{
						finishCallback("Difficulty beatmap could not be loaded.");
						yield break;
					}
					BeatmapDifficulty difficulty;
					if (!difficultyBeatmap.difficulty.BeatmapDifficultyFromSerializedName(out difficulty))
					{
						finishCallback("Unknown difficulty name in beatmap found.");
						yield break;
					}
					BeatmapCharacteristicDifficulty beatmapCharacteristicDifficulty = new BeatmapCharacteristicDifficulty
					{
						characteristicSerializedName = difficultyBeatmapSet.beatmapCharacteristicName,
						difficulty = difficulty
					};
					this._beatsDataSet[beatmapCharacteristicDifficulty] = beatmapSaveData.ConvertToEditorBeatsData(difficultyBeatmap.noteJumpMovementSpeed, difficultyBeatmap.noteJumpStartBeatOffset);
				}
			}
			yield return null;
			this.SetActiveCharacteristicBeatmapDataFromBeatmapDataSet(this._activeCharacteristicDifficulty.characteristicDifficulty);
			string filePath = Path.Combine(projectDirectoryPath, levelSaveData.coverImageFilename);
			yield return this._coverImage.LoadImageCoroutine(filePath, delegate(EditorLevelCoverImageSO.LoadingResult loadingResult)
			{
				if (loadingResult != EditorLevelCoverImageSO.LoadingResult.Sucess)
				{
					this._coverImage.Clear();
				}
			});
			yield return null;
			string filePath2 = Path.Combine(projectDirectoryPath, levelSaveData.songFilename);
			yield return this._editorAudio.LoadAudioCoroutine(filePath2, delegate
			{
				if (!this._editorAudio.isAudioLoaded)
				{
					finishCallback("Song file could not be loaded.");
				}
			});
			if (!this._editorAudio.isAudioLoaded)
			{
				yield break;
			}
			this._openedProjectDirectoryPath = projectDirectoryPath;
			finishCallback(null);
			yield break;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00013092 File Offset: 0x00011292
		public IEnumerator ExportProjectCoroutine(string exportFilePath, bool exportAudio, Action<bool, string> finishCallback)
		{
			string directoryName = Path.GetDirectoryName(exportFilePath);
			string tempDirectoryPath;
			do
			{
				tempDirectoryPath = Path.Combine(directoryName, Path.GetRandomFileName());
			}
			while (Directory.Exists(tempDirectoryPath));
			try
			{
				Directory.CreateDirectory(tempDirectoryPath);
			}
			catch (Exception ex)
			{
				finishCallback(false, "Exception: " + ex.Message);
				yield break;
			}
			Action cleanupAction = delegate()
			{
				if (Directory.Exists(tempDirectoryPath))
				{
					try
					{
						new DirectoryInfo(tempDirectoryPath).Delete(true);
					}
					catch
					{
					}
				}
			};
			yield return this.SaveProjectCoroutine(tempDirectoryPath, exportAudio, delegate(bool success, string infoMessage)
			{
				if (!success)
				{
					cleanupAction();
					finishCallback(false, infoMessage);
				}
			});
			bool createZipFinished = false;
			FileCompressionHelper.CreateZipFromDirectoryAsync(tempDirectoryPath, exportFilePath, delegate(bool success)
			{
				cleanupAction();
				finishCallback(success, "");
				createZipFinished = true;
			});
			WaitUntil waitUntil = new WaitUntil(() => createZipFinished);
			yield return waitUntil;
			yield break;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000130B6 File Offset: 0x000112B6
		public IEnumerator ImportAudioCoroutine(string filePath, Action finishCallback)
		{
			bool loadingFinished = false;
			this._editorAudio.LoadAudio(filePath, delegate
			{
				if (finishCallback != null)
				{
					finishCallback();
				}
				loadingFinished = true;
			});
			WaitUntil waitUntil = new WaitUntil(() => loadingFinished);
			yield return waitUntil;
			yield break;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000130D3 File Offset: 0x000112D3
		private void HandleActiveCharacteristicDifficultyDidChange(BeatmapCharacteristicDifficulty prevCharacteristicDifficulty, BeatmapCharacteristicDifficulty currentCharacteristicDifficulty)
		{
			this._beatsDataSet[prevCharacteristicDifficulty] = this._editorBeatmap.beatsData.Clone();
			this.SetActiveCharacteristicBeatmapDataFromBeatmapDataSet(currentCharacteristicDifficulty);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00059D44 File Offset: 0x00057F44
		private void SetActiveCharacteristicBeatmapDataFromBeatmapDataSet(BeatmapCharacteristicDifficulty characteristicDifficulty)
		{
			if (this._beatsDataSet[characteristicDifficulty] != null)
			{
				this._editorBeatmap.LoadData(this._beatsDataSet[characteristicDifficulty]);
				return;
			}
			int numberOfBeats = Mathf.CeilToInt((this._editorAudio.isAudioLoaded ? this._editorAudio.songDuration : 0f) * this._songParams.beatsPerMinute / 60f) + 1;
			this._editorBeatmap.InitWithEmptyData(numberOfBeats);
		}

		// Token: 0x040018AD RID: 6317
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040018AE RID: 6318
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x040018AF RID: 6319
		[SerializeField]
		private EditorLevelCoverImageSO _coverImage;

		// Token: 0x040018B0 RID: 6320
		[SerializeField]
		private EditorStandardLevelInfoSO _levelInfo;

		// Token: 0x040018B1 RID: 6321
		[SerializeField]
		private EditorSongParamsSO _songParams;

		// Token: 0x040018B2 RID: 6322
		[SerializeField]
		private EditorEnvironmentSO _environment;

		// Token: 0x040018B3 RID: 6323
		[SerializeField]
		private EditorEnvironmentSO _allDirectionsEnvironment;

		// Token: 0x040018B4 RID: 6324
		[SerializeField]
		private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

		// Token: 0x040018B5 RID: 6325
		[SerializeField]
		private BeatmapCharacteristicSO _defaultBeatmapCharacteristic;

		// Token: 0x040018B6 RID: 6326
		[Space]
		[SerializeField]
		private ActiveCharacteristicDifficultySO _activeCharacteristicDifficulty;

		// Token: 0x040018B7 RID: 6327
		private EditorBeatsDataSet _beatsDataSet = new EditorBeatsDataSet();

		// Token: 0x040018B8 RID: 6328
		private string _openedProjectDirectoryPath;

		// Token: 0x040018B9 RID: 6329
		private const string kBackupDirectoryName = "Backups";

		// Token: 0x040018BA RID: 6330
		private const int kMaxNumberOfBackups = 7;
	}
}
