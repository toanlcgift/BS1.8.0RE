using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x02000336 RID: 822
[RequireComponent(typeof(FPSCounter))]
public class FPSCounterUIController : MonoBehaviour
{
	// Token: 0x06000E51 RID: 3665 RVA: 0x0000B07B File Offset: 0x0000927B
	protected void Awake()
	{
		this._fpsCounter = base.GetComponent<FPSCounter>();
		this._fpsCounter.enabled = false;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0000B095 File Offset: 0x00009295
	protected IEnumerator Start()
	{
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		yield return null;
		this._fpsCounter.enabled = true;
		yield break;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0003AB5C File Offset: 0x00038D5C
	protected void LateUpdate()
	{
		this._timeToUpdateUI -= Time.unscaledDeltaTime;
		if (this._timeToUpdateUI <= 0f)
		{
			this._timeToUpdateUI = this._uiUpdateTimeInterval;
			this._currentFPSText.text = this._fpsCounter.currentFPS.ToString();
			this._lowestFPSText.text = this._fpsCounter.lowestFPS.ToString();
			this._highestFPSText.text = this._fpsCounter.highestFPS.ToString();
			return;
		}
	}

	// Token: 0x04000EAE RID: 3758
	[SerializeField]
	private float _uiUpdateTimeInterval = 0.2f;

	// Token: 0x04000EAF RID: 3759
	[SerializeField]
	private TextMeshProUGUI _currentFPSText;

	// Token: 0x04000EB0 RID: 3760
	[SerializeField]
	private TextMeshProUGUI _lowestFPSText;

	// Token: 0x04000EB1 RID: 3761
	[SerializeField]
	private TextMeshProUGUI _highestFPSText;

	// Token: 0x04000EB2 RID: 3762
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04000EB3 RID: 3763
	private FPSCounter _fpsCounter;

	// Token: 0x04000EB4 RID: 3764
	private float _timeToUpdateUI;
}
