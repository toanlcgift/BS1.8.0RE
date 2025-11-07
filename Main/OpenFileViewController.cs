using System;
using System.Collections.Generic;
using System.IO;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000F RID: 15
public class OpenFileViewController : ViewController
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000032 RID: 50 RVA: 0x0001503C File Offset: 0x0001323C
	// (remove) Token: 0x06000033 RID: 51 RVA: 0x00015074 File Offset: 0x00013274
	public event Action<OpenFileViewController> didCancelEvent;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000034 RID: 52 RVA: 0x000150AC File Offset: 0x000132AC
	// (remove) Token: 0x06000035 RID: 53 RVA: 0x000150E4 File Offset: 0x000132E4
	public event Action<OpenFileViewController, string> didSelectFileEvent;

	// Token: 0x06000036 RID: 54 RVA: 0x0001511C File Offset: 0x0001331C
	public void Init(string title, string buttonTitle, string initialDirectory, string[] extensions)
	{
		this._title = title;
		this._currentDirectory = initialDirectory;
		this._extensions = extensions;
		this._fileBrowserItems = new FileBrowserItem[0];
		this._fileNameText.enabled = false;
		this._openButtonText.text = buttonTitle;
		this._titleText.text = this._title;
		this._openButton.interactable = false;
		this.ShowDirectory(this._currentDirectory);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0001518C File Offset: 0x0001338C
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._fileBrowserTableView.didSelectRow += this.HandleSelectRow;
			this._navigationPanelTableView.didSelectRow += this.HandleSelectRow;
			base.buttonBinder.AddBindings(new List<Tuple<Button, Action>>
			{
				{
					this._backButton,
					new Action(this.BackButtonWasPressed)
				},
				{
					this._openButton,
					new Action(this.OpenButtonWasPressed)
				}
			});
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._fileBrowserTableView.Init(this._fileBrowserItems);
			this._navigationPanelTableView.Init(this._bookmarksFoldersModel.bookmarksFolders);
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000022AE File Offset: 0x000004AE
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._fileBrowserTableView != null)
		{
			this._fileBrowserTableView.didSelectRow -= this.HandleSelectRow;
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000022DB File Offset: 0x000004DB
	public void ShowLoading()
	{
		this._loadingIndicator.ShowLoading("WORKING ...");
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000022ED File Offset: 0x000004ED
	public void HideLoading()
	{
		this._loadingIndicator.HideLoading();
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00015234 File Offset: 0x00013434
	private void ShowDirectory(string dirPath)
	{
		try
		{
			this._filepPathText.text = Path.GetFullPath(dirPath);
		}
		catch
		{
			this._filepPathText.text = dirPath;
		}
		Action<FileBrowserItem[]> callback = delegate(FileBrowserItem[] items)
		{
			this._fileBrowserItems = items;
			if (this._fileBrowserTableView.isActiveAndEnabled)
			{
				this._fileBrowserTableView.SetItems(this._fileBrowserItems);
			}
		};
		FileBrowserModel.GetContentOfDirectory(dirPath, this._extensions, callback);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00015290 File Offset: 0x00013490
	private void HandleSelectRow(FileBrowserTableView fileBrowserTableView, FileBrowserItem item)
	{
		this._filepPathText.text = item.fullPath;
		if (item.isDirectory)
		{
			this.ShowDirectory(item.fullPath);
			this._fileNameText.enabled = false;
			this._openButton.interactable = false;
			return;
		}
		this._fileNameText.enabled = true;
		this._selectedFile = item.fullPath;
		this._fileNameText.text = item.displayName;
		this._openButton.interactable = true;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000022FA File Offset: 0x000004FA
	public void OpenButtonWasPressed()
	{
		if (this.didSelectFileEvent != null)
		{
			this.didSelectFileEvent(this, this._selectedFile);
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002316 File Offset: 0x00000516
	public void BackButtonWasPressed()
	{
		if (this.didCancelEvent != null)
		{
			this.didCancelEvent(this);
		}
	}

	// Token: 0x0400002D RID: 45
	[SerializeField]
	private BookmarksFoldersModel _bookmarksFoldersModel;

	// Token: 0x0400002E RID: 46
	[Space]
	[SerializeField]
	private FileBrowserTableView _fileBrowserTableView;

	// Token: 0x0400002F RID: 47
	[SerializeField]
	private FileBrowserTableView _navigationPanelTableView;

	// Token: 0x04000030 RID: 48
	[SerializeField]
	private TextMeshProUGUI _titleText;

	// Token: 0x04000031 RID: 49
	[SerializeField]
	private TextMeshProUGUI _fileNameText;

	// Token: 0x04000032 RID: 50
	[SerializeField]
	private TextMeshProUGUI _filepPathText;

	// Token: 0x04000033 RID: 51
	[Space]
	[SerializeField]
	private Button _backButton;

	// Token: 0x04000034 RID: 52
	[SerializeField]
	private Button _openButton;

	// Token: 0x04000035 RID: 53
	[SerializeField]
	private TextMeshProUGUI _openButtonText;

	// Token: 0x04000036 RID: 54
	[Space]
	[SerializeField]
	private LoadingIndicator _loadingIndicator;

	// Token: 0x04000039 RID: 57
	private FileBrowserItem[] _fileBrowserItems;

	// Token: 0x0400003A RID: 58
	private string _title;

	// Token: 0x0400003B RID: 59
	private string _currentDirectory;

	// Token: 0x0400003C RID: 60
	private string _selectedFile;

	// Token: 0x0400003D RID: 61
	private string[] _extensions;
}
