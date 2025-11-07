using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000040 RID: 64
public class AudioClipLoaderSO : PersistentScriptableObject
{
	// Token: 0x06000106 RID: 262 RVA: 0x00002C2E File Offset: 0x00000E2E
	protected override void OnEnable()
	{
		base.OnEnable();
		this._isLoading = false;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00002C3D File Offset: 0x00000E3D
	public void LoadAudioFile(string filePath, Action<AudioClip> finishCallback)
	{
		if (this._isLoading)
		{
			return;
		}
		PersistentSingleton<SharedCoroutineStarter>.instance.StartCoroutine(this.LoadAudioFileCoroutine(filePath, finishCallback));
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00002C5B File Offset: 0x00000E5B
	public IEnumerator LoadAudioFileCoroutine(string filePath, Action<AudioClip> finishCallback)
	{
		if (!File.Exists(filePath))
		{
			finishCallback(null);
			yield break;
		}
		this._isLoading = true;
		using (UnityWebRequest wwwa = UnityWebRequestMultimedia.GetAudioClip(FileHelpers.GetEscapedURLForFilePath(filePath), AudioType.UNKNOWN))
		{
			yield return wwwa.SendWebRequest();
			this._isLoading = false;
			AudioClip audioClip = null;
			if (!wwwa.isNetworkError)
			{
				audioClip = DownloadHandlerAudioClip.GetContent(wwwa);
				if (audioClip != null && audioClip.loadState != AudioDataLoadState.Loaded)
				{
					audioClip = null;
				}
			}
			if (finishCallback != null)
			{
				finishCallback(audioClip);
			}
		}
		UnityWebRequest wwwab = null;
		yield break;
	}

	// Token: 0x040000DB RID: 219
	private bool _isLoading;
}
