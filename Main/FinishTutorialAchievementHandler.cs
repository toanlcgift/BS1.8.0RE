using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class FinishTutorialAchievementHandler : MonoBehaviour
{
	// Token: 0x060001AC RID: 428 RVA: 0x00003555 File Offset: 0x00001755
	protected void Start()
	{
		this._tutorialFinishedSignal.Subscribe(new Action(this.HandleTutorialFinished));
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000356E File Offset: 0x0000176E
	protected void OnDestroy()
	{
		this._tutorialFinishedSignal.Unsubscribe(new Action(this.HandleTutorialFinished));
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00003587 File Offset: 0x00001787
	private void HandleTutorialFinished()
	{
		this._achievementsModel.UnlockAchievement(this._finishTutorialAchievement);
	}

	// Token: 0x040001B1 RID: 433
	[SerializeField]
	private AchievementsModelSO _achievementsModel;

	// Token: 0x040001B2 RID: 434
	[Space]
	[SerializeField]
	private Signal _tutorialFinishedSignal;

	// Token: 0x040001B3 RID: 435
	[SerializeField]
	private AchievementSO _finishTutorialAchievement;
}
