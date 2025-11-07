using System;
using UnityEngine;
using Zenject;

// Token: 0x020003A2 RID: 930
public class MissionNodeGizmos : MonoBehaviour
{
	// Token: 0x040010FA RID: 4346
	[SerializeField]
	private MissionNode _missionNode;

	// Token: 0x040010FB RID: 4347
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModel;

	// Token: 0x040010FC RID: 4348
	[Inject]
	private CampaignProgressModel _missionProgressModel;
}
