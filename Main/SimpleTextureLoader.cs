using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200033A RID: 826
public static class SimpleTextureLoader
{
	// Token: 0x06000E62 RID: 3682 RVA: 0x0000B0CE File Offset: 0x000092CE
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void NoDomainReloadInit()
	{
		SimpleTextureLoader._cache = new HMCache<string, Texture2D>(100);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0000B0DC File Offset: 0x000092DC
	public static void LoadTexture(string filePath, bool useCache, Action<Texture2D> finishedCallback)
	{
		PersistentSingleton<SharedCoroutineStarter>.instance.StartCoroutine(SimpleTextureLoader.LoadTextureCoroutine(filePath, useCache, finishedCallback));
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0000B0F1 File Offset: 0x000092F1
	public static IEnumerator LoadTextureCoroutine(string filePath, bool useCache, Action<Texture2D> finishedCallback)
	{
		if (useCache && SimpleTextureLoader._cache.IsInCache(filePath))
		{
			if (finishedCallback != null)
			{
				finishedCallback(SimpleTextureLoader._cache.GetFromCache(filePath));
			}
			yield break;
		}
		using (UnityWebRequest uwrab = UnityWebRequestTexture.GetTexture(FileHelpers.GetEscapedURLForFilePath(filePath)))
		{
			yield return uwrab.SendWebRequest();
			if (uwrab.isNetworkError || uwrab.isHttpError)
			{
				if (finishedCallback != null)
				{
					finishedCallback(null);
				}
			}
			else
			{
				Texture2D content = DownloadHandlerTexture.GetContent(uwrab);
				if (useCache)
				{
					SimpleTextureLoader._cache.PutToCache(filePath, content);
				}
				if (finishedCallback != null)
				{
					finishedCallback(content);
				}
			}
		}
		UnityWebRequest uwr = null;
		yield break;
	}

	// Token: 0x04000EBB RID: 3771
	private static HMCache<string, Texture2D> _cache = new HMCache<string, Texture2D>(100);
}
