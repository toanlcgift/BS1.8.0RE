using System;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x020001D4 RID: 468
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000721 RID: 1825 RVA: 0x0000614B File Offset: 0x0000434B
	private static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0000616F File Offset: 0x0000436F
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	protected static void NoDomainReloadInit()
	{
		SteamManager.s_instance = null;
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000723 RID: 1827 RVA: 0x00006177 File Offset: 0x00004377
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00006183 File Offset: 0x00004383
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00027CA8 File Offset: 0x00025EA8
	protected void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		SteamManager.s_instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		if (!this.DeleteSteamAppIDFile())
		{
			Application.Quit();
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary((AppId_t)620980U))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException arg)
		{
			Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + arg, this);
			Application.Quit();
			return;
		}
		try
		{
			this.m_bInitialized = SteamAPI.Init();
			if (!this.m_bInitialized)
			{
				Debug.Log("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
				Application.Quit();
			}
		}
		catch (DllNotFoundException arg2)
		{
			Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + arg2, this);
			Application.Quit();
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00027DA0 File Offset: 0x00025FA0
	protected void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0000618B File Offset: 0x0000438B
	protected void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x000061AF File Offset: 0x000043AF
	protected void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00027DF0 File Offset: 0x00025FF0
	private bool DeleteSteamAppIDFile()
	{
		if (File.Exists("steam_appid.txt"))
		{
			try
			{
				File.Delete("steam_appid.txt");
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
			if (File.Exists("steam_appid.txt"))
			{
				Debug.LogError("Cannot delete steam_appid.txt. Quitting...");
				return false;
			}
		}
		return true;
	}

	// Token: 0x040007A7 RID: 1959
	private static SteamManager s_instance;

	// Token: 0x040007A8 RID: 1960
	private bool m_bInitialized;

	// Token: 0x040007A9 RID: 1961
	private SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
