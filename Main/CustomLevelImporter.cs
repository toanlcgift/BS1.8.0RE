using System;
using System.IO;
using UnityEngine;

// Token: 0x02000096 RID: 150
public static class CustomLevelImporter
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000241 RID: 577 RVA: 0x00003909 File Offset: 0x00001B09
	public static string lastUsedDirectory
	{
		get
		{
			if (Directory.Exists(PlayerPrefs.GetString("CustomLevelImporter.LastUsedFilePath", null)))
			{
				return PlayerPrefs.GetString("CustomLevelImporter.LastUsedFilePath", null);
			}
			return Application.dataPath;
		}
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0001B94C File Offset: 0x00019B4C
	public static void SetLastUsedDirectory(string path)
	{
		string value = null;
		try
		{
			value = Path.GetDirectoryName(path);
		}
		catch
		{
		}
		PlayerPrefs.SetString("CustomLevelImporter.LastUsedFilePath", value);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0001B984 File Offset: 0x00019B84
	public static void DeleteLevel(string levelDirectoryPath)
	{
		try
		{
			Directory.Delete(levelDirectoryPath, true);
		}
		catch
		{
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0001B9B0 File Offset: 0x00019BB0
	public static void UniversalImportLevelFromFile(string filePath, Action<CustomLevelImporter.UniversalImportFromFileResult, string, string> callback)
	{
		CustomLevelImporter.SetLastUsedDirectory(filePath);
		if (Path.GetExtension(filePath) == ".bsl")
		{
			CustomLevelImporter.ImportStandardLevelFromFile(filePath, delegate(CustomLevelImporter.ImportStandardLevelFromFileResult importStandardLevelFromFileResult, string extractDirPath, string dstDirPath)
			{
				if (importStandardLevelFromFileResult == CustomLevelImporter.ImportStandardLevelFromFileResult.Success)
				{
					callback(CustomLevelImporter.UniversalImportFromFileResult.Success, null, dstDirPath);
					return;
				}
				if (importStandardLevelFromFileResult == CustomLevelImporter.ImportStandardLevelFromFileResult.Error)
				{
					callback(CustomLevelImporter.UniversalImportFromFileResult.Error, null, dstDirPath);
					return;
				}
				if (importStandardLevelFromFileResult == CustomLevelImporter.ImportStandardLevelFromFileResult.ExtractedButMissingSongFile)
				{
					callback(CustomLevelImporter.UniversalImportFromFileResult.ExtractedButMissingSongFile, extractDirPath, dstDirPath);
				}
			});
			return;
		}
		callback(CustomLevelImporter.UniversalImportFromFileResult.Error, null, null);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0001BA04 File Offset: 0x00019C04
	public static void UniversalImportLevelFromExtractedDirectory(string extractDirPath, string songFilePath, Action<CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult, string> callback)
	{
		CustomLevelImporter.SetLastUsedDirectory(songFilePath);
		string path = Path.Combine(extractDirPath, "Info.dat");
		try
		{
			if (File.Exists(path))
			{
				CustomLevelImporter.ImportStandardLevelFromDirectory(extractDirPath, songFilePath, delegate(CustomLevelImporter.ImportStandardLevelFromDirectoryResult importStandardLevelFromDirectoryResult, string dstDirPath)
				{
					if (importStandardLevelFromDirectoryResult == CustomLevelImporter.ImportStandardLevelFromDirectoryResult.Success)
					{
						callback(CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult.Success, dstDirPath);
						return;
					}
					callback(CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult.Success, dstDirPath);
				});
			}
		}
		catch
		{
			callback(CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult.Error, null);
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0001BA70 File Offset: 0x00019C70
	public static void Cleanup(string extractDirPath)
	{
		try
		{
			if (Directory.Exists(extractDirPath))
			{
				Directory.Delete(extractDirPath, true);
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0001BAA4 File Offset: 0x00019CA4
	public static void ImportStandardLevelFromFile(string levelFilenamePath, Action<CustomLevelImporter.ImportStandardLevelFromFileResult, string, string> finishCallback)
	{
		string extractDirPath = null;
		try
		{
			extractDirPath = Path.Combine(Application.temporaryCachePath, "LevelImport");
			if (Directory.Exists(extractDirPath))
			{
				Directory.Delete(extractDirPath, true);
			}
			Directory.CreateDirectory(extractDirPath);
		}
		catch
		{
			extractDirPath = null;
		}
		if (extractDirPath != null)
		{
			Action<CustomLevelImporter.ImportStandardLevelFromDirectoryResult, string> actionFinishCallback2 = null;
			CustomLevelImporter.ExtractStandardLevelAndGetSongFilename(levelFilenamePath, extractDirPath, delegate(CustomLevelImporter.ExtractStandardLevelAndGetSongFilenameResult extractResult, string songFilename)
			{
				string extractDirPath2 = string.Empty;
				if (extractResult == CustomLevelImporter.ExtractStandardLevelAndGetSongFilenameResult.Success)
				{
					string text = Path.Combine(extractDirPath, songFilename);
					if (!File.Exists(text))
					{
						text = Path.Combine(Path.GetDirectoryName(levelFilenamePath), songFilename);
						if (!File.Exists(text))
						{
							if (finishCallback != null)
							{
								finishCallback(CustomLevelImporter.ImportStandardLevelFromFileResult.ExtractedButMissingSongFile, extractDirPath, null);
							}
							return;
						}
					}
					extractDirPath2 = extractDirPath;
					string songFilenamePath = text;
					Action<CustomLevelImporter.ImportStandardLevelFromDirectoryResult, string> finishCallback2;
					if ((finishCallback2 = actionFinishCallback2) == null)
					{
						finishCallback2 = (actionFinishCallback2 = delegate(CustomLevelImporter.ImportStandardLevelFromDirectoryResult importResult, string dstDirPath)
						{
							CustomLevelImporter.Cleanup(extractDirPath);
							if (finishCallback != null)
							{
								CustomLevelImporter.ImportStandardLevelFromFileResult arg = CustomLevelImporter.ImportStandardLevelFromFileResult.Success;
								if (importResult != CustomLevelImporter.ImportStandardLevelFromDirectoryResult.Success)
								{
									arg = CustomLevelImporter.ImportStandardLevelFromFileResult.Error;
								}
								finishCallback(arg, null, dstDirPath);
							}
						});
					}
					CustomLevelImporter.ImportStandardLevelFromDirectory(extractDirPath, songFilenamePath, finishCallback2);
					return;
				}
				CustomLevelImporter.Cleanup(extractDirPath);
				if (finishCallback != null)
				{
					finishCallback(CustomLevelImporter.ImportStandardLevelFromFileResult.Error, null, null);
				}
			});
			return;
		}
		if (finishCallback != null)
		{
			finishCallback(CustomLevelImporter.ImportStandardLevelFromFileResult.Error, null, null);
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0001BB60 File Offset: 0x00019D60
	public static void ExtractStandardLevelAndGetSongFilename(string levelFilenamePath, string dstDirPath, Action<CustomLevelImporter.ExtractStandardLevelAndGetSongFilenameResult, string> finishCallback)
	{
		CustomLevelImporter.ExtractStandardLevelAndGetSongFilenameResult result = CustomLevelImporter.ExtractStandardLevelAndGetSongFilenameResult.Error;
		string songFilename = null;
		Action job = delegate()
		{
			try
			{
				if (File.Exists(levelFilenamePath))
				{
					if (FileCompressionHelper.ExtractZipToDirectory(levelFilenamePath, dstDirPath))
					{
						string text = Path.Combine(dstDirPath, "Info.dat");
						if (File.Exists(text))
						{
							StandardLevelInfoSaveData standardLevelInfoSaveData = StandardLevelLoader.LoadStandardLevelSaveData(text);
							if (standardLevelInfoSaveData != null)
							{
								result = CustomLevelImporter.ExtractStandardLevelAndGetSongFilenameResult.Success;
								songFilename = standardLevelInfoSaveData.songFilename;
							}
						}
					}
				}
			}
			catch
			{
			}
		};
		Action finishCallback2 = delegate()
		{
			if (finishCallback != null)
			{
				finishCallback(result, songFilename);
			}
		};
		new HMTask(job, finishCallback2).Run();
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0001BBBC File Offset: 0x00019DBC
	public static void ImportStandardLevelFromDirectory(string scrDirPath, string songFilenamePath, Action<CustomLevelImporter.ImportStandardLevelFromDirectoryResult, string> finishCallback)
	{
		CustomLevelImporter.ImportStandardLevelFromDirectoryResult result = CustomLevelImporter.ImportStandardLevelFromDirectoryResult.Error;
		string customLevelsDirectoryPath = CustomLevelPathHelper.customLevelsDirectoryPath;
		string dstDirPath = null;
		Action job = delegate()
		{
			try
			{
				string path = Path.Combine(scrDirPath, "Info.dat");
				if (File.Exists(path))
				{
					string text = File.ReadAllText(path);
					StandardLevelInfoSaveData standardLevelInfoSaveData = StandardLevelInfoSaveData.DeserializeFromJSONString(text);
					if (standardLevelInfoSaveData != null)
					{
						string fileName = Path.GetFileName(songFilenamePath);
						if (fileName != standardLevelInfoSaveData.songFilename)
						{
							standardLevelInfoSaveData.SetSongFilename(fileName);
						}
						if (standardLevelInfoSaveData.difficultyBeatmapSets.Length != 0)
						{
							int num = 0;
							foreach (StandardLevelInfoSaveData.DifficultyBeatmapSet difficultyBeatmapSet in standardLevelInfoSaveData.difficultyBeatmapSets)
							{
								num += difficultyBeatmapSet.difficultyBeatmaps.Length;
							}
							if (num != 0)
							{
								BeatmapSaveData[] array = new BeatmapSaveData[num];
								int num2 = 0;
								StandardLevelInfoSaveData.DifficultyBeatmapSet[] difficultyBeatmapSets = standardLevelInfoSaveData.difficultyBeatmapSets;
								for (int i = 0; i < difficultyBeatmapSets.Length; i++)
								{
									foreach (StandardLevelInfoSaveData.DifficultyBeatmap difficultyBeatmap in difficultyBeatmapSets[i].difficultyBeatmaps)
									{
										string path2 = Path.Combine(scrDirPath, difficultyBeatmap.beatmapFilename);
										if (!File.Exists(path2))
										{
											return;
										}
										text = File.ReadAllText(path2);
										BeatmapSaveData beatmapSaveData = BeatmapSaveData.DeserializeFromJSONString(text);
										if (beatmapSaveData == null)
										{
											return;
										}
										array[num2] = beatmapSaveData;
										num2++;
									}
								}
								string defaultNameForCustomLevel = CustomLevelPathHelper.GetDefaultNameForCustomLevel(standardLevelInfoSaveData.songName, standardLevelInfoSaveData.songAuthorName, standardLevelInfoSaveData.levelAuthorName);
								dstDirPath = Path.Combine(customLevelsDirectoryPath, defaultNameForCustomLevel);
								dstDirPath = FileHelpers.GetUniqueDirectoryNameByAppendingNumber(dstDirPath);
								Directory.CreateDirectory(dstDirPath);
								string text2 = Path.Combine(dstDirPath, "Info.dat");
								text = standardLevelInfoSaveData.SerializeToJSONString();
								File.WriteAllText(text2, text);
								num2 = 0;
								difficultyBeatmapSets = standardLevelInfoSaveData.difficultyBeatmapSets;
								for (int i = 0; i < difficultyBeatmapSets.Length; i++)
								{
									foreach (StandardLevelInfoSaveData.DifficultyBeatmap difficultyBeatmap2 in difficultyBeatmapSets[i].difficultyBeatmaps)
									{
										text2 = Path.Combine(dstDirPath, difficultyBeatmap2.beatmapFilename);
										text = array[num2].SerializeToJSONString();
										File.WriteAllText(text2, text);
										num2++;
									}
								}
								string text3 = Path.Combine(scrDirPath, standardLevelInfoSaveData.coverImageFilename);
								text2 = Path.Combine(dstDirPath, standardLevelInfoSaveData.coverImageFilename);
								if (File.Exists(text3))
								{
									File.Copy(text3, text2);
								}
								text2 = Path.Combine(dstDirPath, standardLevelInfoSaveData.songFilename);
								File.Copy(songFilenamePath, text2);
								result = CustomLevelImporter.ImportStandardLevelFromDirectoryResult.Success;
							}
						}
					}
				}
			}
			catch
			{
				if (dstDirPath != null)
				{
					try
					{
						Directory.Delete(dstDirPath, true);
					}
					catch
					{
					}
				}
			}
		};
		Action finishCallback2 = delegate()
		{
			if (finishCallback != null)
			{
				finishCallback(result, dstDirPath);
			}
		};
		new HMTask(job, finishCallback2).Run();
	}

	// Token: 0x04000269 RID: 617
	public const string kStandardLevelExtension = ".bsl";

	// Token: 0x0400026A RID: 618
	[DoesNotRequireDomainReloadInit]
	public static readonly string[] kAllSupportedLevelFileExtensions = new string[]
	{
		".bsl"
	};

	// Token: 0x0400026B RID: 619
	[DoesNotRequireDomainReloadInit]
	public static readonly string[] kAllSupportedSongFileExtensions = new string[]
	{
		".wav",
		".ogg"
	};

	// Token: 0x0400026C RID: 620
	private const string kLastUsedDirectoryPathKey = "CustomLevelImporter.LastUsedFilePath";

	// Token: 0x02000097 RID: 151
	public enum UniversalImportFromFileResult
	{
		// Token: 0x0400026E RID: 622
		Error,
		// Token: 0x0400026F RID: 623
		Success,
		// Token: 0x04000270 RID: 624
		ExtractedButMissingSongFile
	}

	// Token: 0x02000098 RID: 152
	public enum UniversalImportLevelFromExtractedDirectoryResult
	{
		// Token: 0x04000272 RID: 626
		Error,
		// Token: 0x04000273 RID: 627
		Success
	}

	// Token: 0x02000099 RID: 153
	public enum ImportStandardLevelFromFileResult
	{
		// Token: 0x04000275 RID: 629
		Error,
		// Token: 0x04000276 RID: 630
		Success,
		// Token: 0x04000277 RID: 631
		ExtractedButMissingSongFile
	}

	// Token: 0x0200009A RID: 154
	public enum ExtractStandardLevelAndGetSongFilenameResult
	{
		// Token: 0x04000279 RID: 633
		Error,
		// Token: 0x0400027A RID: 634
		Success
	}

	// Token: 0x0200009B RID: 155
	public enum ImportStandardLevelFromDirectoryResult
	{
		// Token: 0x0400027C RID: 636
		Error,
		// Token: 0x0400027D RID: 637
		Success
	}
}
