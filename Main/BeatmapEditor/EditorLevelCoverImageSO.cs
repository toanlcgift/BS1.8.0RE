using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200052F RID: 1327
	public class EditorLevelCoverImageSO : PersistentScriptableObject
	{
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x0600197D RID: 6525 RVA: 0x000597EC File Offset: 0x000579EC
		// (remove) Token: 0x0600197E RID: 6526 RVA: 0x00059824 File Offset: 0x00057A24
		public event Action<Texture2D> didChangeTextureEvent;

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00012E95 File Offset: 0x00011095
		public Texture2D texture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00012E9D File Offset: 0x0001109D
		public string imageFilePath
		{
			get
			{
				return this._imageFilePath;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x00012EA5 File Offset: 0x000110A5
		public string imageFileName
		{
			get
			{
				return Path.GetFileName(this._imageFilePath);
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005985C File Offset: 0x00057A5C
		public IEnumerator LoadImageCoroutine(string filePath, Action<EditorLevelCoverImageSO.LoadingResult> didLoadAction)
		{
			return SimpleTextureLoader.LoadTextureCoroutine(filePath, false, delegate(Texture2D texture)
			{
				this.HandleLoadedTexture(texture, filePath, didLoadAction);
			});
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005989C File Offset: 0x00057A9C
		public void LoadImage(string filePath, Action<EditorLevelCoverImageSO.LoadingResult> didLoadAction)
		{
			SimpleTextureLoader.LoadTexture(filePath, false, delegate(Texture2D texture)
			{
				this.HandleLoadedTexture(texture, filePath, didLoadAction);
			});
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000598DC File Offset: 0x00057ADC
		private void HandleLoadedTexture(Texture2D texture, string filePath, Action<EditorLevelCoverImageSO.LoadingResult> didLoadAction)
		{
			EditorLevelCoverImageSO.LoadingResult obj = EditorLevelCoverImageSO.LoadingResult.Sucess;
			if (texture == null)
			{
				obj = EditorLevelCoverImageSO.LoadingResult.NoTextureLoaded;
			}
			else if (texture.width != texture.height)
			{
				texture = null;
				obj = EditorLevelCoverImageSO.LoadingResult.BadAspect;
			}
			else if (texture.width < 256 || texture.height < 256)
			{
				texture = null;
				obj = EditorLevelCoverImageSO.LoadingResult.LowResolution;
			}
			if (texture != null)
			{
				this._imageFilePath = filePath;
				this._texture = texture;
				Action<Texture2D> action = this.didChangeTextureEvent;
				if (action != null)
				{
					action(texture);
				}
			}
			if (didLoadAction != null)
			{
				didLoadAction(obj);
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00012EB2 File Offset: 0x000110B2
		public void Clear()
		{
			this._texture = null;
			this._imageFilePath = null;
			Action<Texture2D> action = this.didChangeTextureEvent;
			if (action == null)
			{
				return;
			}
			action(null);
		}

		// Token: 0x04001894 RID: 6292
		private Texture2D _texture;

		// Token: 0x04001895 RID: 6293
		private string _imageFilePath;

		// Token: 0x02000530 RID: 1328
		public enum LoadingResult
		{
			// Token: 0x04001897 RID: 6295
			Sucess,
			// Token: 0x04001898 RID: 6296
			NoTextureLoaded,
			// Token: 0x04001899 RID: 6297
			LowResolution,
			// Token: 0x0400189A RID: 6298
			BadAspect
		}
	}
}
