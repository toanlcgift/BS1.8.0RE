using System;
using System.Collections;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200055A RID: 1370
	public class EditorLevelCoverImagePanelController : MonoBehaviour
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x0005CD00 File Offset: 0x0005AF00
		protected void Awake()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._importButton,
					new Action(this.ImportButtonPressed)
				},
				{
					this._clearButton,
					delegate()
					{
						this._coverImage.Clear();
					}
				}
			});
			this.RefreshImageTexture(this._coverImage.texture);
			this._coverImage.didChangeTextureEvent += this.HandleCoverImageDidChangeTexture;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00013B16 File Offset: 0x00011D16
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			this._coverImage.didChangeTextureEvent -= this.HandleCoverImageDidChangeTexture;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00013B42 File Offset: 0x00011D42
		private void HandleCoverImageDidChangeTexture(Texture2D texture)
		{
			this.RefreshImageTexture(texture);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00013B4B File Offset: 0x00011D4B
		private void RefreshImageTexture(Texture2D texture)
		{
			if (texture == null)
			{
				this._image.texture = this._defaultTexture;
				return;
			}
			this._image.texture = texture;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00013B74 File Offset: 0x00011D74
		private void ImportButtonPressed()
		{
			base.StartCoroutine(this.ImportImageCoroutine());
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00013B83 File Offset: 0x00011D83
		private IEnumerator ImportImageCoroutine()
		{
			this._loadingIndicator.ShowLoading("WORKING ...");
			yield return null;
			string text = NativeFileDialogs.OpenFileDialog("Choose image", "png", null);
			if (text == null)
			{
				this._loadingIndicator.HideLoading();
				yield break;
			}
			yield return this._coverImage.LoadImageCoroutine(text, delegate(EditorLevelCoverImageSO.LoadingResult loadingResult)
			{
				switch (loadingResult)
				{
				case EditorLevelCoverImageSO.LoadingResult.NoTextureLoaded:
					this._alertPanelController.Show("Error", "Texture could not be loaded.", "OK", delegate()
					{
						this._alertPanelController.Hide();
					}, null, null, null, null);
					return;
				case EditorLevelCoverImageSO.LoadingResult.LowResolution:
					this._alertPanelController.Show("Warning", "Loading failed. Only images with 1:1 aspect ratio and resoluton higher or equal to 256x256 pixels are supported.", "OK", delegate()
					{
						this._alertPanelController.Hide();
					}, null, null, null, null);
					return;
				case EditorLevelCoverImageSO.LoadingResult.BadAspect:
					this._alertPanelController.Show("Warning", "Loading failed. Only images with 1:1 aspect ratio and resoluton higher or equal to 256x256 pixels are supported.", "OK", delegate()
					{
						this._alertPanelController.Hide();
					}, null, null, null, null);
					return;
				default:
					return;
				}
			});
			this._loadingIndicator.HideLoading();
			yield break;
		}

		// Token: 0x0400197F RID: 6527
		[SerializeField]
		private EditorLevelCoverImageSO _coverImage;

		// Token: 0x04001980 RID: 6528
		[SerializeField]
		private AlertPanelController _alertPanelController;

		// Token: 0x04001981 RID: 6529
		[SerializeField]
		private LoadingIndicator _loadingIndicator;

		// Token: 0x04001982 RID: 6530
		[SerializeField]
		private Texture _defaultTexture;

		// Token: 0x04001983 RID: 6531
		[SerializeField]
		private RawImage _image;

		// Token: 0x04001984 RID: 6532
		[SerializeField]
		private Button _importButton;

		// Token: 0x04001985 RID: 6533
		[SerializeField]
		private Button _clearButton;

		// Token: 0x04001986 RID: 6534
		private ButtonBinder _buttonBinder;
	}
}
