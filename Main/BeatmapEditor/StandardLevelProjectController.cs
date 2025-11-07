using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HMUI;
using SFB;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x02000541 RID: 1345
	public class StandardLevelProjectController : MonoBehaviour
	{
		// Token: 0x060019D9 RID: 6617 RVA: 0x0005AB60 File Offset: 0x00058D60
		protected void Start()
		{
			if (!this._editorStandardLevelProject.beatmapIsInitialized)
			{
				this._editorStandardLevelProject.InitNewProject();
			}
			this._playbackController.ResumeSavedPosition();
			if (!string.IsNullOrEmpty(this._sceneSetupData.levelDirPath))
			{
				base.StartCoroutine(this.OpenLevelCoroutine(this._sceneSetupData.levelDirPath));
			}
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._testBeatmapButton,
					new Action(this.TestBeatmap)
				},
				{
					this._newButton,
					new Action(this.HandleNewButtonPressed)
				},
				{
					this._openButton,
					delegate()
					{
						base.StartCoroutine(this.OpenLevelCoroutine());
					}
				},
				{
					this._saveButton,
					new Action(this.SaveLevel)
				},
				{
					this._saveAsButton,
					delegate()
					{
						base.StartCoroutine(this.SaveAsLevelCoroutine());
					}
				},
				{
					this._importButton,
					delegate()
					{
						base.StartCoroutine(this.ImportLevelCoroutine());
					}
				},
				{
					this._exportButton,
					delegate()
					{
						base.StartCoroutine(this.ExportLevelCoroutine());
					}
				},
				{
					this._importAudioButton,
					delegate()
					{
						base.StartCoroutine(this.ImportAudioCoroutine());
					}
				},
				{
					this._helpButton,
					delegate()
					{
						base.StartCoroutine(this._helpPanelController.ShowCoroutine(new Action(this._helpPanelController.Hide)));
					}
				},
				{
					this._backToMenuButton,
					new Action(this.HandleBackToMenuButtonPressed)
				}
			});
			this._openLevelPanelController.didDeleteLevelEvent += this.HandleOpenLevelPanelControllerDidDeleteLevel;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0001321A File Offset: 0x0001141A
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			if (this._openLevelPanelController != null)
			{
				this._openLevelPanelController.didDeleteLevelEvent -= this.HandleOpenLevelPanelControllerDidDeleteLevel;
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00013254 File Offset: 0x00011454
		protected void Update()
		{
			if (this._beatmapTestStarter.CanTestBeatmap() == BeatmapTestStarter.TestStartResult.Success && (this._vrControllersInputManager.TriggerValue(XRNode.LeftHand) > 0.7f || this._vrControllersInputManager.TriggerValue(XRNode.RightHand) > 0.7f))
			{
				this.TestBeatmap();
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0005ACD4 File Offset: 0x00058ED4
		private void HandleNewButtonPressed()
		{
			this._alertPanelController.Show("New Project", "Do you really want to create a new project? All unsaved data will be lost.", "Cancel", delegate()
			{
				this._alertPanelController.Hide();
			}, "Create New", delegate()
			{
				this._alertPanelController.Hide();
				this._editorStandardLevelProject.InitNewProject();
			}, null, null);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0005AD1C File Offset: 0x00058F1C
		private void HandleBackToMenuButtonPressed()
		{
			this._alertPanelController.Show("Quit the Editor", "Do you really want to quit the editor and return to the game menu?", "Cancel", delegate()
			{
				this._alertPanelController.Hide();
			}, "Quit", delegate()
			{
				this._alertPanelController.Hide();
				this._loadingIndicator.ShowLoading("Continue in VR.");
				this._beatmapEditorScenesTransitionSetupData.Finish();
			}, null, null);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0005AD64 File Offset: 0x00058F64
		public void TestBeatmap()
		{
			BeatmapTestStarter.TestStartResult testStartResult = this._beatmapTestStarter.TestBeatmap();
			if (testStartResult == BeatmapTestStarter.TestStartResult.NoAudio)
			{
				this._popUpInfoPanelController.ShowInfo("Can not start test, because there is no audio loaded.", EditorPopUpInfoPanelController.InfoType.Warning);
				return;
			}
			if (testStartResult == BeatmapTestStarter.TestStartResult.NoBeatsData)
			{
				this._popUpInfoPanelController.ShowInfo("Can not start test, because there are no beat data.", EditorPopUpInfoPanelController.InfoType.Warning);
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00013290 File Offset: 0x00011490
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

		// Token: 0x060019E0 RID: 6624 RVA: 0x0001329F File Offset: 0x0001149F
		private IEnumerator OpenLevelCoroutine(string levelDirectoryPath)
		{
			this._loadingIndicator.ShowLoading("WORKING ...");
			yield return this._editorStandardLevelProject.OpenProjectCoroutine(levelDirectoryPath, delegate(string errorString)
			{
				if (!string.IsNullOrEmpty(errorString))
				{
					this._popUpInfoPanelController.ShowInfo("Level loading failed - " + errorString, EditorPopUpInfoPanelController.InfoType.Warning);
					this._editorStandardLevelProject.InitNewProject();
				}
			});
			this._loadingIndicator.HideLoading();
			yield break;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000132B5 File Offset: 0x000114B5
		public void SaveLevel()
		{
			if (string.IsNullOrEmpty(this._editorStandardLevelProject.openedProjectDirectoryPath))
			{
				base.StartCoroutine(this.SaveAsLevelCoroutine());
				return;
			}
			base.StartCoroutine(this.SaveLevelCoroutine(this._editorStandardLevelProject.openedProjectDirectoryPath, true));
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000132F0 File Offset: 0x000114F0
		private IEnumerator SaveAsLevelCoroutine()
		{
			StandardLevelProjectController standardLevelProjectController = new StandardLevelProjectController();
			standardLevelProjectController = this;
			if (!this._editorStandardLevelProject.canSaveProject)
			{
				this._alertPanelController.Show("Warning", "This level can not be saved, because it does not contain any audio data. Use IMPORT AUDIO button in files panel to add audio.", "OK", delegate()
				{
					standardLevelProjectController._alertPanelController.Hide();
				}, null, null, null, null);
				yield break;
			}
			string defaultNameForCustomLevel = CustomLevelPathHelper.GetDefaultNameForCustomLevel(this._editorStandardLevelProject.levelInfo.songName, this._editorStandardLevelProject.levelInfo.songAuthorName, this._editorStandardLevelProject.levelInfo.levelAuthorName);
			string newLevelName = null;
			yield return this._saveAsPanelController.ShowCoroutine(defaultNameForCustomLevel, delegate(string levelName)
			{
				newLevelName = levelName;
				standardLevelProjectController._saveAsPanelController.Hide();
			});
			if (string.IsNullOrEmpty(newLevelName))
			{
				yield break;
			}
			string levelDirectoryPath = Path.Combine(CustomLevelPathHelper.customLevelsDirectoryPath, newLevelName);
			if (Directory.Exists(levelDirectoryPath))
			{
				StandardLevelProjectController standardLevelProjectController2 = new StandardLevelProjectController();
				standardLevelProjectController2 = standardLevelProjectController;
				var shouldOverwrite = false;
				yield return this._alertPanelController.ShowCoroutine("Warning", "Level with specified name already exist, do you want to overwrite it?", "Cancel", delegate
				{
					standardLevelProjectController2._alertPanelController.Hide();
				}, "Overwrite", delegate
				{
					standardLevelProjectController2._alertPanelController.Hide();
					shouldOverwrite = true;
				}, null, null);
				if (!shouldOverwrite)
				{
					yield break;
				}
				standardLevelProjectController2 = null;
			}
			yield return this.SaveLevelCoroutine(levelDirectoryPath, true);
			yield break;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000132FF File Offset: 0x000114FF
		private IEnumerator SaveLevelCoroutine(string levelDirectoryPath, bool saveAudio)
		{
			this._loadingIndicator.ShowLoading("WORKING ...");
			yield return this._editorStandardLevelProject.SaveProjectCoroutine(levelDirectoryPath, saveAudio, delegate(bool success, string infoMessage)
			{
				if (success)
				{
					this._popUpInfoPanelController.ShowInfo("Level was saved." + infoMessage, EditorPopUpInfoPanelController.InfoType.Info);
					return;
				}
				this._popUpInfoPanelController.ShowInfo("Error occured while saving the level." + infoMessage, EditorPopUpInfoPanelController.InfoType.Warning);
			});
			this._loadingIndicator.HideLoading();
			yield break;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0001331C File Offset: 0x0001151C
		private IEnumerator ImportLevelCoroutine()
		{
			this._loadingIndicator.ShowLoading("WORKING ...");
			string bslFilePath = NativeFileDialogs.OpenFileDialog("Import Level", new ExtensionFilter[]
			{
				new ExtensionFilter("Levels", new string[]
				{
					"bsl"
				})
			}, null);
			if (bslFilePath == null)
			{
				this._loadingIndicator.HideLoading();
				yield break;
			}
			CustomLevelImporter.UniversalImportFromFileResult? importFileResult = null;
			string importedLevelDirPath = null;
			string extractDirectoryPath = null;
			CustomLevelImporter.UniversalImportLevelFromFile(bslFilePath, delegate(CustomLevelImporter.UniversalImportFromFileResult result, string directoryPath, string dstDirPath)
			{
				importFileResult = new CustomLevelImporter.UniversalImportFromFileResult?(result);
				extractDirectoryPath = directoryPath;
				importedLevelDirPath = dstDirPath;
			});
			yield return new WaitUntil(() => importFileResult != null);
			CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult? importDirectoryResult = null;
			CustomLevelImporter.UniversalImportFromFileResult? importFileResult2 = importFileResult;
			CustomLevelImporter.UniversalImportFromFileResult universalImportFromFileResult = CustomLevelImporter.UniversalImportFromFileResult.ExtractedButMissingSongFile;
			if (importFileResult2.GetValueOrDefault() == universalImportFromFileResult & importFileResult2 != null)
			{
				string text = NativeFileDialogs.OpenFileDialog("Import Audio", new ExtensionFilter[]
				{
					new ExtensionFilter("Audio", new string[]
					{
						"wav",
						"ogg"
					})
				}, Path.GetDirectoryName(bslFilePath));
				if (string.IsNullOrEmpty(text))
				{
					CustomLevelImporter.Cleanup(extractDirectoryPath);
					this._loadingIndicator.HideLoading();
					yield break;
				}
				CustomLevelImporter.UniversalImportLevelFromExtractedDirectory(extractDirectoryPath, text, delegate(CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult result, string dstDirPath)
				{
					importDirectoryResult = new CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult?(result);
					importedLevelDirPath = dstDirPath;
				});
				yield return new WaitUntil(() => importDirectoryResult != null);
			}
			this._loadingIndicator.HideLoading();
			yield return null;
			importFileResult2 = importFileResult;
			universalImportFromFileResult = CustomLevelImporter.UniversalImportFromFileResult.Success;
			if (!(importFileResult2.GetValueOrDefault() == universalImportFromFileResult & importFileResult2 != null))
			{
				CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult? importDirectoryResult2 = importDirectoryResult;
				CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult universalImportLevelFromExtractedDirectoryResult = CustomLevelImporter.UniversalImportLevelFromExtractedDirectoryResult.Success;
				if (!(importDirectoryResult2.GetValueOrDefault() == universalImportLevelFromExtractedDirectoryResult & importDirectoryResult2 != null))
				{
					this._popUpInfoPanelController.ShowInfo("Error occured while importing level.", EditorPopUpInfoPanelController.InfoType.Warning);
					goto IL_2C3;
				}
			}
			this._alertPanelController.Show("Warning", "Do you want to open imported level? All your unsaved work will be lost.", "Yes", delegate()
			{
				this._alertPanelController.Hide();
				this.StartCoroutine(this.OpenLevelCoroutine(importedLevelDirPath));
			}, "No", delegate()
			{
				this._alertPanelController.Hide();
			}, null, null);
			IL_2C3:
			yield break;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0001332B File Offset: 0x0001152B
		private IEnumerator ExportLevelCoroutine()
		{
			if (!this._editorStandardLevelProject.canSaveProject)
			{
				this._alertPanelController.Show("Warning", "This level can not be exported, because it does not contain any audio data. Use IMPORT AUDIO button in files panel to add audio.", "OK", delegate()
				{
					this._alertPanelController.Hide();
				}, null, null, null, null);
				yield break;
			}
			bool exportAudioAlertFinished = false;
			bool exportAudio = false;
			bool cancelled = false;
			this._alertPanelController.Show("Warning", "Do you want to include audio in the exported file? By pressing yes you agree that you own all the distrubution rights for all the audio data.", "YES", delegate()
			{
				exportAudioAlertFinished = true;
				exportAudio = true;
			}, "NO", delegate()
			{
				exportAudioAlertFinished = true;
				exportAudio = false;
			}, "CANCEL", delegate()
			{
				exportAudioAlertFinished = true;
				cancelled = true;
			});
			yield return new WaitUntil(() => exportAudioAlertFinished);
			this._alertPanelController.Hide();
			if (cancelled)
			{
				yield break;
			}
			this._loadingIndicator.ShowLoading("WORKING ...");
			yield return null;
			string defaultNameForCustomLevel = CustomLevelPathHelper.GetDefaultNameForCustomLevel(this._editorStandardLevelProject.levelInfo.songName, this._editorStandardLevelProject.levelInfo.songAuthorName, this._editorStandardLevelProject.levelInfo.levelAuthorName);
			string filePath = NativeFileDialogs.SaveFileDialog("Export Level", defaultNameForCustomLevel, "bsl", null);
			yield return null;
			if (string.IsNullOrEmpty(filePath))
			{
				this._loadingIndicator.HideLoading();
				yield break;
			}
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			yield return this._editorStandardLevelProject.ExportProjectCoroutine(filePath, exportAudio, delegate(bool success, string infoMessage)
			{
				if (success)
				{
					this._popUpInfoPanelController.ShowInfo("Level was successfully exported. " + infoMessage, EditorPopUpInfoPanelController.InfoType.Info);
					return;
				}
				this._popUpInfoPanelController.ShowInfo("Export failed. " + infoMessage, EditorPopUpInfoPanelController.InfoType.Warning);
			});
			this._loadingIndicator.HideLoading();
			yield break;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0001333A File Offset: 0x0001153A
		private IEnumerator ImportAudioCoroutine()
		{
			this._loadingIndicator.ShowLoading("WORKING ...");
			yield return null;
			string text = NativeFileDialogs.OpenFileDialog("Import Audio", new ExtensionFilter[]
			{
				new ExtensionFilter("Audio Files", new string[]
				{
					"wav",
					"ogg"
				})
			}, null);
			if (text != null && text != "")
			{
				yield return this._editorStandardLevelProject.ImportAudioCoroutine(text, delegate
				{
					this._loadingIndicator.HideLoading();
				});
			}
			this._loadingIndicator.HideLoading();
			yield break;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00013349 File Offset: 0x00011549
		private void HandleOpenLevelPanelControllerDidDeleteLevel(string levelDirectoryPath)
		{
			if (levelDirectoryPath == this._editorStandardLevelProject.openedProjectDirectoryPath)
			{
				this._editorStandardLevelProject.InitNewProject();
			}
		}

		// Token: 0x040018E3 RID: 6371
		[SerializeField]
		private EditorStandardLevelProjectSO _editorStandardLevelProject;

		// Token: 0x040018E4 RID: 6372
		[SerializeField]
		private BeatmapEditorScenesTransitionSetupDataSO _beatmapEditorScenesTransitionSetupData;

		// Token: 0x040018E5 RID: 6373
		[Space]
		[SerializeField]
		private Button _testBeatmapButton;

		// Token: 0x040018E6 RID: 6374
		[SerializeField]
		private Button _newButton;

		// Token: 0x040018E7 RID: 6375
		[SerializeField]
		private Button _openButton;

		// Token: 0x040018E8 RID: 6376
		[SerializeField]
		private Button _saveButton;

		// Token: 0x040018E9 RID: 6377
		[SerializeField]
		private Button _saveAsButton;

		// Token: 0x040018EA RID: 6378
		[SerializeField]
		private Button _importButton;

		// Token: 0x040018EB RID: 6379
		[SerializeField]
		private Button _exportButton;

		// Token: 0x040018EC RID: 6380
		[SerializeField]
		private Button _importAudioButton;

		// Token: 0x040018ED RID: 6381
		[SerializeField]
		private Button _helpButton;

		// Token: 0x040018EE RID: 6382
		[SerializeField]
		private Button _backToMenuButton;

		// Token: 0x040018EF RID: 6383
		[Space]
		[SerializeField]
		private PlaybackController _playbackController;

		// Token: 0x040018F0 RID: 6384
		[SerializeField]
		private BeatmapTestStarter _beatmapTestStarter;

		// Token: 0x040018F1 RID: 6385
		[Space]
		[SerializeField]
		private OpenLevelPanelController _openLevelPanelController;

		// Token: 0x040018F2 RID: 6386
		[SerializeField]
		private SaveAsPanelController _saveAsPanelController;

		// Token: 0x040018F3 RID: 6387
		[SerializeField]
		private HelpPanelController _helpPanelController;

		// Token: 0x040018F4 RID: 6388
		[Space]
		[SerializeField]
		private LoadingIndicator _loadingIndicator;

		// Token: 0x040018F5 RID: 6389
		[SerializeField]
		private AlertPanelController _alertPanelController;

		// Token: 0x040018F6 RID: 6390
		[SerializeField]
		private EditorPopUpInfoPanelController _popUpInfoPanelController;

		// Token: 0x040018F7 RID: 6391
		[InjectOptional]
		private BeatmapEditorSceneSetupData _sceneSetupData = new BeatmapEditorSceneSetupData(null, null);

		// Token: 0x040018F8 RID: 6392
		[Inject]
		private VRControllersInputManager _vrControllersInputManager;

		// Token: 0x040018F9 RID: 6393
		private ButtonBinder _buttonBinder;
	}
}
