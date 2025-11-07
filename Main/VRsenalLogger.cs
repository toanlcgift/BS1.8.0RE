using System;
using UnityEngine;
using Zenject;

// Token: 0x0200001F RID: 31
public class VRsenalLogger : MonoBehaviour
{
	// Token: 0x06000086 RID: 134 RVA: 0x00002694 File Offset: 0x00000894
	protected void Awake()
	{
		this._gameScenesManager.installEarlyBindingsEvent += this.HandleGameScenesManagerInstallEarlyBindings;
		this._playerNameWasEnteredSignal.Subscribe(new Action<string>(this.HandlePlayerNameWasEntered));
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000026C4 File Offset: 0x000008C4
	protected void OnDestroy()
	{
		this._gameScenesManager.installEarlyBindingsEvent -= this.HandleGameScenesManagerInstallEarlyBindings;
		this._playerNameWasEnteredSignal.Unsubscribe(new Action<string>(this.HandlePlayerNameWasEntered));
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00015DE8 File Offset: 0x00013FE8
	private void HandleGameScenesManagerInstallEarlyBindings(ScenesTransitionSetupDataSO scenesTransitionSetupData, DiContainer container)
	{
		if (scenesTransitionSetupData == this._standardLevelScenesTransitionSetupData)
		{
			container.Bind<VRsenalScoreLogger>().FromComponentInNewPrefab(this._vRsenalScoreLoggerPrefab).AsSingle().NonLazy();
			return;
		}
		if (scenesTransitionSetupData == this._tutorialScenesTransitionSetupData)
		{
			Debug.Log("VRsenalLogger: Clicked Tutorial.");
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000026F4 File Offset: 0x000008F4
	private void HandlePlayerNameWasEntered(string playerName)
	{
		Debug.Log("VRsenalLogger: Player entered their name for the leaderboard. Name=" + playerName);
	}

	// Token: 0x04000076 RID: 118
	[SerializeField]
	private ScenesTransitionSetupDataSO _standardLevelScenesTransitionSetupData;

	// Token: 0x04000077 RID: 119
	[SerializeField]
	private ScenesTransitionSetupDataSO _tutorialScenesTransitionSetupData;

	// Token: 0x04000078 RID: 120
	[SerializeField]
	private StringSignal _playerNameWasEnteredSignal;

	// Token: 0x04000079 RID: 121
	[SerializeField]
	private VRsenalScoreLogger _vRsenalScoreLoggerPrefab;

	// Token: 0x0400007A RID: 122
	[Inject]
	private GameScenesManager _gameScenesManager;
}
