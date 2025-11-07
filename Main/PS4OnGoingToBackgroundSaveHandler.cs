using System;
using UnityEngine;
using Zenject;

// Token: 0x020001E2 RID: 482
public class PS4OnGoingToBackgroundSaveHandler : MonoBehaviour
{
	// Token: 0x0600075C RID: 1884 RVA: 0x0000636E File Offset: 0x0000456E
	protected void OnEnable()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		PersistentSingleton<PS4Helper>.instance.didGoToBackgroundExecutionEvent += this.HandlePS4HelperDidGoToBackgroundExecution;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0000638C File Offset: 0x0000458C
	protected void OnDisable()
	{
		if (PersistentSingleton<PS4Helper>.IsSingletonAvailable)
		{
			PersistentSingleton<PS4Helper>.instance.didGoToBackgroundExecutionEvent -= this.HandlePS4HelperDidGoToBackgroundExecution;
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x000063AB File Offset: 0x000045AB
	private void HandlePS4HelperDidGoToBackgroundExecution()
	{
		this._playerDataModel.Save();
		this._localLeaderboardModel.Save();
		this._mainSettingsModel.Save();
	}

	// Token: 0x040007CB RID: 1995
	[SerializeField]
	private LocalLeaderboardsModel _localLeaderboardModel;

	// Token: 0x040007CC RID: 1996
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040007CD RID: 1997
	[Inject]
	private PlayerDataModel _playerDataModel;
}
