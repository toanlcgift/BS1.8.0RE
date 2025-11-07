using System;
using UnityEngine;
using Zenject;

// Token: 0x02000288 RID: 648
public class HeadObstacleLowPassAudioEffect : MonoBehaviour
{
	// Token: 0x06000AE3 RID: 2787 RVA: 0x00032A0C File Offset: 0x00030C0C
	protected void Update()
	{
		bool flag = this._playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0;
		if (flag == this._headWasInObstacle)
		{
			return;
		}
		if (flag)
		{
			this._mainAudioEffects.TriggerLowPass();
		}
		else
		{
			this._mainAudioEffects.ResumeNormalSound();
		}
		this._headWasInObstacle = flag;
	}

	// Token: 0x04000B5C RID: 2908
	[SerializeField]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	// Token: 0x04000B5D RID: 2909
	[Inject]
	private MainAudioEffects _mainAudioEffects;

	// Token: 0x04000B5E RID: 2910
	private bool _headWasInObstacle;
}
