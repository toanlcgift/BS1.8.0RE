using System;
using Polyglot;
using UnityEngine;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x02000516 RID: 1302
	public class BeatmapTestStarter : MonoBehaviour
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x000125A5 File Offset: 0x000107A5
		public BeatmapTestStarter.TestStartResult CanTestBeatmap()
		{
			if (!this._editorBeatmap.hasBeatsData)
			{
				return BeatmapTestStarter.TestStartResult.NoBeatsData;
			}
			if (!this._editorAudio.isAudioLoaded)
			{
				return BeatmapTestStarter.TestStartResult.NoAudio;
			}
			return BeatmapTestStarter.TestStartResult.Success;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00057218 File Offset: 0x00055418
		public BeatmapTestStarter.TestStartResult TestBeatmap()
		{
			BeatmapTestStarter.TestStartResult testStartResult = this.CanTestBeatmap();
			if (testStartResult != BeatmapTestStarter.TestStartResult.Success)
			{
				return testStartResult;
			}
			float songTime = this._playbackController.songTime;
			this._playbackController.PauseSong();
			GameplayModifiers defaultModifiers = GameplayModifiers.defaultModifiers;
			defaultModifiers.noFail = true;
			PracticeSettings defaultPracticeSettings = PracticeSettings.defaultPracticeSettings;
			defaultPracticeSettings.startSongTime = songTime;
			defaultPracticeSettings.startInAdvanceAndClearNotes = false;
			BeatmapSaveData beatmapSaveData = this._editorBeatmap.beatsData.ConvertToBeatmapSaveData(this._beatsPerMinute, true, this._editorAudio.songDuration);
			if (beatmapSaveData == null)
			{
				return BeatmapTestStarter.TestStartResult.NoBeatsData;
			}
			BeatmapLevelSO beatmapLevelSO = ScriptableObject.CreateInstance<BeatmapLevelSO>();
			BeatmapDataSO beatmapDataSO = ScriptableObject.CreateInstance<BeatmapDataSO>();
			beatmapDataSO.beatmapData = this._beatmapDataLoader.GetBeatmapDataFromBeatmapSaveData(beatmapSaveData.notes, beatmapSaveData.obstacles, beatmapSaveData.events, this._beatsPerMinute, this._shuffleStrength, this._shufflePeriod);
			BeatmapCharacteristicDifficulty characteristicDifficulty = this._activeDifficulty.characteristicDifficulty;
			BeatmapCharacteristicSO beatmapCharacteristicBySerializedName = this._beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(characteristicDifficulty.characteristicSerializedName);
			BeatmapLevelSO.DifficultyBeatmapSet[] array = new BeatmapLevelSO.DifficultyBeatmapSet[]
			{
				new BeatmapLevelSO.DifficultyBeatmapSet(beatmapCharacteristicBySerializedName, new BeatmapLevelSO.DifficultyBeatmap[]
				{
					new BeatmapLevelSO.DifficultyBeatmap(beatmapLevelSO, characteristicDifficulty.difficulty, characteristicDifficulty.difficulty.DefaultRating(), this._editorBeatmap.noteJumpMovementSpeed, this._editorBeatmap.noteJumpStartBeatOffset, beatmapDataSO)
				})
			};
			BeatmapLevelSO beatmapLevelSO2 = beatmapLevelSO;
			string levelID = "CustomLevel";
			string songName = "CustomLevel";
			string songSubName = "";
			string songAuthorName = "";
			string levelAuthorName = "";
			AudioClip audioClip = this._editorAudio.audioClip;
			float beatsPerMinute = this._beatsPerMinute;
			float songTimeOffset = this._songTimeOffset;
			float shuffle = this._shuffleStrength;
			float shufflePeriod = this._shufflePeriod;
			float previewStartTime = 0f;
			float previewDuration = 10f;
			Texture2D coverImageTexture2D = null;
			BeatmapLevelSO.DifficultyBeatmapSet[] difficultyBeatmapSets = array;
			beatmapLevelSO2.InitFull(levelID, songName, songSubName, songAuthorName, levelAuthorName, audioClip, beatsPerMinute, songTimeOffset, shuffle, shufflePeriod, previewStartTime, previewDuration, coverImageTexture2D, this._environment, this._allDirectionsEnvironment, difficultyBeatmapSets);
			this._menuTransitionsHelper.StartStandardLevel(beatmapLevelSO.difficultyBeatmapSets[0].difficultyBeatmaps[0], null, null, defaultModifiers, PlayerSpecificSettings.defaultSettings, defaultPracticeSettings, Localization.Get("BUTTON_LEVEL_EDITOR"), false, null, new Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults>(this.HandleStandardLevelDidFinish));
			return BeatmapTestStarter.TestStartResult.Success;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000125C6 File Offset: 0x000107C6
		private void HandleStandardLevelDidFinish(StandardLevelScenesTransitionSetupDataSO standardLevelSceneSetupData, LevelCompletionResults levelCompletionResults)
		{
			if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Restart)
			{
				this.TestBeatmap();
				return;
			}
			if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Quit)
			{
				this._beatmapEditorScrollView.SetPositionToSongTime(levelCompletionResults.endSongTime, true);
			}
		}

		// Token: 0x04001837 RID: 6199
		[SerializeField]
		private EditorEnvironmentSO _environment;

		// Token: 0x04001838 RID: 6200
		[SerializeField]
		private EditorEnvironmentSO _allDirectionsEnvironment;

		// Token: 0x04001839 RID: 6201
		[SerializeField]
		private FloatSO _beatsPerMinute;

		// Token: 0x0400183A RID: 6202
		[SerializeField]
		private FloatSO _shuffleStrength;

		// Token: 0x0400183B RID: 6203
		[SerializeField]
		private FloatSO _shufflePeriod;

		// Token: 0x0400183C RID: 6204
		[SerializeField]
		private FloatSO _songTimeOffset;

		// Token: 0x0400183D RID: 6205
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x0400183E RID: 6206
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x0400183F RID: 6207
		[SerializeField]
		private ActiveCharacteristicDifficultySO _activeDifficulty;

		// Token: 0x04001840 RID: 6208
		[SerializeField]
		private PlaybackController _playbackController;

		// Token: 0x04001841 RID: 6209
		[SerializeField]
		private BeatmapEditorScrollView _beatmapEditorScrollView;

		// Token: 0x04001842 RID: 6210
		[SerializeField]
		private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

		// Token: 0x04001843 RID: 6211
		[Inject]
		private MenuTransitionsHelper _menuTransitionsHelper;

		// Token: 0x04001844 RID: 6212
		private BeatmapDataLoader _beatmapDataLoader = new BeatmapDataLoader();

		// Token: 0x02000517 RID: 1303
		public enum TestStartResult
		{
			// Token: 0x04001846 RID: 6214
			None,
			// Token: 0x04001847 RID: 6215
			Success,
			// Token: 0x04001848 RID: 6216
			NoAudio,
			// Token: 0x04001849 RID: 6217
			NoBeatsData
		}
	}
}
