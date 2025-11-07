using System;
using System.Collections;
using System.IO;
using BeatmapEditor;
using Polyglot;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004F1 RID: 1265
	public class ProjectController : MonoBehaviour
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000116AF File Offset: 0x0000F8AF
		public BeatmapData beatmapData
		{
			get
			{
				return this._beatmapData;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000116B7 File Offset: 0x0000F8B7
		public BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection
		{
			get
			{
				return this._beatmapCharacteristicCollection;
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00055388 File Offset: 0x00053588
		protected void Start()
		{
			Debug.Log(string.Format("singleDirectionEnvironmentList.Count = {0}, allDirectionsEnvironmentList.Count = {1}", this._singleDirectionEnvironmentList.environmentInfos.Length, this._allDirectionsEnvironmentList.environmentInfos.Length));
			if (!this._editorStandardLevelProject.beatmapIsInitialized)
			{
				this.InitNewProject();
			}
			this._openLevelPanelController.didDeleteLevelEvent += this.HandleOpenLevelPanelControllerDidDeleteLevel;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000116BF File Offset: 0x0000F8BF
		protected void OnDestroy()
		{
			if (this._openLevelPanelController != null)
			{
				this._openLevelPanelController.didDeleteLevelEvent -= this.HandleOpenLevelPanelControllerDidDeleteLevel;
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000116E6 File Offset: 0x0000F8E6
		private void InitNewProject()
		{
			this._editorStandardLevelProject.InitNewProject();
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000116F3 File Offset: 0x0000F8F3
		public void OpenLevel()
		{
			base.StartCoroutine(this.OpenLevelCoroutine());
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00011702 File Offset: 0x0000F902
		private IEnumerator OpenLevelCoroutine()
		{
			string newLevelDirectoryPath = null;
			yield return this._openLevelPanelController.ShowCoroutine(delegate(string levelDirectoryPath)
			{
				newLevelDirectoryPath = levelDirectoryPath;
				this._openLevelPanelController.Hide();
			});
			if (string.IsNullOrEmpty(newLevelDirectoryPath))
			{
				yield break;
			}
			yield return this.OpenLevelCoroutine(newLevelDirectoryPath);
			yield break;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00011711 File Offset: 0x0000F911
		private IEnumerator OpenLevelCoroutine(string levelDirectoryPath)
		{
			this._fullscreenActivityIndicator.Show();
			yield return this.OpenProjectCoroutine(levelDirectoryPath, delegate(string errorString)
			{
				if (!string.IsNullOrEmpty(errorString))
				{
					this._popUpInfoPanelController.ShowInfo("Level loading failed - " + errorString, EditorPopUpInfoPanelController.InfoType.Warning);
					this.InitNewProject();
				}
			});
			this._fullscreenActivityIndicator.Hide();
			this.OpenBeatmapCharacteristicDifficulty(this._activeCharacteristicDifficulty.characteristicDifficulty);
			yield break;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00011727 File Offset: 0x0000F927
		private void HandleOpenLevelPanelControllerDidDeleteLevel(string levelDirectoryPath)
		{
			if (levelDirectoryPath == this._editorStandardLevelProject.openedProjectDirectoryPath)
			{
				this.InitNewProject();
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000553F4 File Offset: 0x000535F4
		public void OpenBeatmapCharacteristicDifficulty(BeatmapCharacteristicDifficulty characteristicDifficulty)
		{
			if (this._levelData == null)
			{
				return;
			}
			BeatmapDifficulty difficulty = characteristicDifficulty.difficulty;
			BeatmapCharacteristicSO beatmapCharacteristicSO = this.GetBeatmapCharacteristicSO(characteristicDifficulty.characteristicSerializedName);
			if (beatmapCharacteristicSO != null)
			{
				if (!this.OpenBeatmap(beatmapCharacteristicSO, difficulty))
				{
					string arg = Localization.Get(beatmapCharacteristicSO.characteristicNameLocalizationKey);
					string text = string.Format("Beatmap with {0} characteristic and {1} difficulty not found.", arg, difficulty.Name());
					this._popUpInfoPanelController.ShowInfo(text, EditorPopUpInfoPanelController.InfoType.Warning);
					Debug.LogWarning(text);
					return;
				}
			}
			else
			{
				Debug.LogError(string.Format("Beatmap Characteristic {0} not found in Characteristic collection.", characteristicDifficulty.characteristicSerializedName));
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00055478 File Offset: 0x00053678
		private BeatmapCharacteristicSO GetBeatmapCharacteristicSO(string serializedName)
		{
			foreach (BeatmapCharacteristicSO beatmapCharacteristicSO in this._beatmapCharacteristicCollection.beatmapCharacteristics)
			{
				if (beatmapCharacteristicSO.serializedName == serializedName)
				{
					return beatmapCharacteristicSO;
				}
			}
			return null;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000554B4 File Offset: 0x000536B4
		private bool FindFirstBeatmapCharacteristicDifficulty(out BeatmapCharacteristicSO characteristic, out BeatmapDifficulty difficulty)
		{
			for (int i = 0; i < this._beatmapCharacteristicCollection.beatmapCharacteristics.Length; i++)
			{
				BeatmapCharacteristicSO beatmapCharacteristicSO = this._beatmapCharacteristicCollection.beatmapCharacteristics[i];
				for (int j = 0; j < 4; j++)
				{
					BeatmapDifficulty beatmapDifficulty = (BeatmapDifficulty)j;
					if (this._levelData.ContainsBeatmapSaveData(beatmapCharacteristicSO.serializedName, beatmapDifficulty))
					{
						characteristic = beatmapCharacteristicSO;
						difficulty = beatmapDifficulty;
						return true;
					}
				}
			}
			characteristic = null;
			difficulty = BeatmapDifficulty.Easy;
			return false;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00055518 File Offset: 0x00053718
		private bool OpenBeatmap(BeatmapCharacteristicSO characteristic, BeatmapDifficulty difficulty)
		{
			if (this._levelData == null)
			{
				return false;
			}
			if (!this._levelData.ContainsBeatmapSaveData(characteristic.serializedName, difficulty))
			{
				return false;
			}
			BeatmapData beatmapData = new BeatmapData(this._levelData, characteristic, difficulty);
			this.SetBeatmapData(beatmapData);
			return true;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00011742 File Offset: 0x0000F942
		private void SetBeatmapData(BeatmapData beatmapData)
		{
			Debug.Log(string.Format("Opening beatmap with {0} characteristic and {1} difficulty...", beatmapData.characteristic.name, beatmapData.difficulty));
			this._beatmapData = beatmapData;
			this._gridController.SetBeatmapData(beatmapData);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0001177C File Offset: 0x0000F97C
		public IEnumerator OpenProjectCoroutine(string projectDirectoryPath, Action<string> finishCallback)
		{
			Debug.Log("Loading level project at " + projectDirectoryPath);
			LevelData ld = new LevelData();
			ld.directoryPath = projectDirectoryPath;
			string stringData = File.ReadAllText(Path.Combine(projectDirectoryPath, "Info.dat"));
			StandardLevelInfoSaveData levelSaveData = StandardLevelInfoSaveData.DeserializeFromJSONString(stringData);
			if (levelSaveData == null || !levelSaveData.hasAllData)
			{
				finishCallback("Info.dat could not be loaded.");
				yield break;
			}
			Debug.Log("Level info loaded.");
			ld.levelInfo = levelSaveData;
			yield return null;
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
					ld.SetBeatmapSaveData(difficultyBeatmapSet.beatmapCharacteristicName, difficulty, beatmapSaveData);
				}
			}
			Debug.Log("Beatmaps loaded.");
			string filePath = Path.Combine(projectDirectoryPath, levelSaveData.coverImageFilename);
			yield return this._coverImage.LoadImageCoroutine(filePath, delegate(EditorLevelCoverImageSO.LoadingResult loadingResult)
			{
				if (loadingResult != EditorLevelCoverImageSO.LoadingResult.Sucess)
				{
					this._coverImage.Clear();
					return;
				}
				Debug.Log("Cover image loaded.");
			});
			ld.coverImage = this._coverImage;
			yield return null;
			string filePath2 = Path.Combine(projectDirectoryPath, levelSaveData.songFilename);
			yield return this._editorAudio.LoadAudioCoroutine(filePath2, delegate
			{
				if (!this._editorAudio.isAudioLoaded)
				{
					finishCallback("Song file could not be loaded.");
					return;
				}
				Debug.Log("Audio loaded.");
			});
			if (!this._editorAudio.isAudioLoaded)
			{
				yield break;
			}
			ld.editorAudio = this._editorAudio;
			this._levelData = ld;
			Debug.Log(string.Format("Level loaded: Song duration: {0}   Base BPM: {1}", ld.editorAudio.songDuration, ld.levelInfo.beatsPerMinute));
			finishCallback(null);
			yield break;
		}

		// Token: 0x04001775 RID: 6005
		[SerializeField]
		private EditorStandardLevelProjectSO _editorStandardLevelProject;

		// Token: 0x04001776 RID: 6006
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x04001777 RID: 6007
		[SerializeField]
		private EditorLevelCoverImageSO _coverImage;

		// Token: 0x04001778 RID: 6008
		[SerializeField]
		private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

		// Token: 0x04001779 RID: 6009
		[SerializeField]
		private ActiveCharacteristicDifficultySO _activeCharacteristicDifficulty;

		// Token: 0x0400177A RID: 6010
		[SerializeField]
		private EditorEnvironmentsListSO _singleDirectionEnvironmentList;

		// Token: 0x0400177B RID: 6011
		[SerializeField]
		private EditorEnvironmentsListSO _allDirectionsEnvironmentList;

		// Token: 0x0400177C RID: 6012
		[Space]
		[SerializeField]
		private EditorPopUpInfoPanelController _popUpInfoPanelController;

		// Token: 0x0400177D RID: 6013
		[SerializeField]
		private OpenLevelPanelController _openLevelPanelController;

		// Token: 0x0400177E RID: 6014
		[Inject]
		private UIActivityIndicatorText _fullscreenActivityIndicator;

		// Token: 0x0400177F RID: 6015
		[Inject]
		private GridController _gridController;

		// Token: 0x04001780 RID: 6016
		private LevelData _levelData;

		// Token: 0x04001781 RID: 6017
		private BeatmapData _beatmapData;
	}
}
