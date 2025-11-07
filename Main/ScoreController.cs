using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x02000314 RID: 788
public class ScoreController : MonoBehaviour
{
	// Token: 0x14000069 RID: 105
	// (add) Token: 0x06000D85 RID: 3461 RVA: 0x00038D38 File Offset: 0x00036F38
	// (remove) Token: 0x06000D86 RID: 3462 RVA: 0x00038D70 File Offset: 0x00036F70
	public event Action<NoteData, NoteCutInfo, int> noteWasCutEvent;

	// Token: 0x1400006A RID: 106
	// (add) Token: 0x06000D87 RID: 3463 RVA: 0x00038DA8 File Offset: 0x00036FA8
	// (remove) Token: 0x06000D88 RID: 3464 RVA: 0x00038DE0 File Offset: 0x00036FE0
	public event Action<NoteData, int> noteWasMissedEvent;

	// Token: 0x1400006B RID: 107
	// (add) Token: 0x06000D89 RID: 3465 RVA: 0x00038E18 File Offset: 0x00037018
	// (remove) Token: 0x06000D8A RID: 3466 RVA: 0x00038E50 File Offset: 0x00037050
	public event Action<int, int> scoreDidChangeEvent;

	// Token: 0x1400006C RID: 108
	// (add) Token: 0x06000D8B RID: 3467 RVA: 0x00038E88 File Offset: 0x00037088
	// (remove) Token: 0x06000D8C RID: 3468 RVA: 0x00038EC0 File Offset: 0x000370C0
	public event Action<int, int> immediateMaxPossibleScoreDidChangeEvent;

	// Token: 0x1400006D RID: 109
	// (add) Token: 0x06000D8D RID: 3469 RVA: 0x00038EF8 File Offset: 0x000370F8
	// (remove) Token: 0x06000D8E RID: 3470 RVA: 0x00038F30 File Offset: 0x00037130
	public event Action<int, float> multiplierDidChangeEvent;

	// Token: 0x1400006E RID: 110
	// (add) Token: 0x06000D8F RID: 3471 RVA: 0x00038F68 File Offset: 0x00037168
	// (remove) Token: 0x06000D90 RID: 3472 RVA: 0x00038FA0 File Offset: 0x000371A0
	public event Action<int> comboDidChangeEvent;

	// Token: 0x1400006F RID: 111
	// (add) Token: 0x06000D91 RID: 3473 RVA: 0x00038FD8 File Offset: 0x000371D8
	// (remove) Token: 0x06000D92 RID: 3474 RVA: 0x00039010 File Offset: 0x00037210
	public event Action<float> feverModeChargeProgressDidChangeEvent;

	// Token: 0x14000070 RID: 112
	// (add) Token: 0x06000D93 RID: 3475 RVA: 0x00039048 File Offset: 0x00037248
	// (remove) Token: 0x06000D94 RID: 3476 RVA: 0x00039080 File Offset: 0x00037280
	public event Action feverDidStartEvent;

	// Token: 0x14000071 RID: 113
	// (add) Token: 0x06000D95 RID: 3477 RVA: 0x000390B8 File Offset: 0x000372B8
	// (remove) Token: 0x06000D96 RID: 3478 RVA: 0x000390F0 File Offset: 0x000372F0
	public event Action feverDidFinishEvent;

	// Token: 0x14000072 RID: 114
	// (add) Token: 0x06000D97 RID: 3479 RVA: 0x00039128 File Offset: 0x00037328
	// (remove) Token: 0x06000D98 RID: 3480 RVA: 0x00039160 File Offset: 0x00037360
	public event Action comboBreakingEventHappenedEvent;

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0000A7E4 File Offset: 0x000089E4
	public int prevFrameRawScore
	{
		get
		{
			return this._prevFrameRawScore;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0000A7EC File Offset: 0x000089EC
	public int prevFrameModifiedScore
	{
		get
		{
			return ScoreModel.GetModifiedScoreForGameplayModifiersScoreMultiplier(this._prevFrameRawScore, this.gameplayModifiersScoreMultiplier);
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0000A7FF File Offset: 0x000089FF
	public int maxCombo
	{
		get
		{
			return this._maxCombo;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0000A807 File Offset: 0x00008A07
	public int multiplierWithFever
	{
		get
		{
			return this._multiplier * (this._feverIsActive ? 2 : 1);
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0000A81C File Offset: 0x00008A1C
	public bool feverModeActive
	{
		get
		{
			return this._feverIsActive;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0000A824 File Offset: 0x00008A24
	public float feverModeDrainProgress
	{
		get
		{
			return Mathf.Clamp01((this._audioTimeSyncController.songTime - this._feverStartTime) / this._feverModeDuration);
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0000A844 File Offset: 0x00008A44
	public float feverModeChargeProgress
	{
		get
		{
			return (float)this._feverCombo / (float)this._feverModeRequiredCombo;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0000A855 File Offset: 0x00008A55
	public float immediateMaxPossibleRawScore
	{
		get
		{
			return (float)this._immediateMaxPossibleRawScore;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0000A85E File Offset: 0x00008A5E
	public float gameplayModifiersScoreMultiplier
	{
		get
		{
			return this._gameplayModifiersScoreMultiplier;
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00039198 File Offset: 0x00037398
	protected void Start()
	{
		this._gameplayModifiersScoreMultiplier = this._gameplayModifiersModel.GetTotalMultiplier(this._gameplayModifiers);
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCutEvent;
		this._beatmapObjectManager.noteWasMissedEvent += this.HandleNoteWasMissedEvent;
		this._multiplier = 1;
		this._multiplierIncreaseProgress = 0;
		this._multiplierIncreaseMaxProgress = 2;
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0000A866 File Offset: 0x00008A66
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCutEvent;
			this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissedEvent;
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x00039200 File Offset: 0x00037400
	protected void Update()
	{
		if (this._feverIsActive && this._audioTimeSyncController.songTime - this._feverStartTime > this._feverModeDuration)
		{
			this._feverCombo = 0;
			this._feverIsActive = false;
			Action action = this.feverDidFinishEvent;
			if (action != null)
			{
				action();
			}
			Action<int, float> action2 = this.multiplierDidChangeEvent;
			if (action2 != null)
			{
				action2(this._multiplier, (float)this._multiplierIncreaseProgress / (float)this._multiplierIncreaseMaxProgress);
			}
		}
		bool comboChanged = false;
		bool multiplierChanged = false;
		if (this._playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0)
		{
			if (!this._playerHeadWasInObstacle)
			{
				this.LoseMultiplier(out comboChanged, out multiplierChanged);
			}
			this._playerHeadWasInObstacle = true;
		}
		else
		{
			this._playerHeadWasInObstacle = false;
		}
		this.NotifyForChange(comboChanged, multiplierChanged);
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x000392B8 File Offset: 0x000374B8
	protected void LateUpdate()
	{
		int num = this._baseRawScore;
		int count = this._cutScoreBuffers.Count;
		for (int i = 0; i < count; i++)
		{
			num += this._cutScoreBuffers[i].scoreWithMultiplier;
		}
		bool flag = false;
		if (num != this._prevFrameRawScore)
		{
			this._prevFrameRawScore = num;
			flag = true;
		}
		int num2 = ScoreModel.MaxRawScoreForNumberOfNotes(this._cutOrMissedNotes);
		for (int j = 0; j < count; j++)
		{
			num2 -= this._cutScoreBuffers[j].multiplier * 115;
			num2 += this._cutScoreBuffers[j].scoreWithMultiplier;
		}
		bool flag2 = false;
		if (this._immediateMaxPossibleRawScore != num2)
		{
			this._immediateMaxPossibleRawScore = num2;
			flag2 = true;
		}
		if (flag)
		{
			Action<int, int> action = this.scoreDidChangeEvent;
			if (action != null)
			{
				action(num, ScoreModel.GetModifiedScoreForGameplayModifiersScoreMultiplier(num, this.gameplayModifiersScoreMultiplier));
			}
		}
		if (flag2)
		{
			Action<int, int> action2 = this.immediateMaxPossibleScoreDidChangeEvent;
			if (action2 == null)
			{
				return;
			}
			action2(this._immediateMaxPossibleRawScore, ScoreModel.GetModifiedScoreForGameplayModifiersScoreMultiplier(this._immediateMaxPossibleRawScore, this.gameplayModifiersScoreMultiplier));
		}
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x000393BC File Offset: 0x000375BC
	private void LoseMultiplier(out bool comboChanged, out bool multiplierChanged)
	{
		comboChanged = false;
		multiplierChanged = false;
		this._feverCombo = 0;
		if (this._combo > 0)
		{
			this._combo = 0;
			comboChanged = true;
		}
		if (this._multiplierIncreaseProgress > 0)
		{
			this._multiplierIncreaseProgress = 0;
			multiplierChanged = true;
		}
		if (this._multiplier > 1)
		{
			this._multiplier /= 2;
			this._multiplierIncreaseMaxProgress = this._multiplier * 2;
			multiplierChanged = true;
		}
		Action action = this.comboBreakingEventHappenedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x00039434 File Offset: 0x00037634
	private void NotifyForChange(bool comboChanged, bool multiplierChanged)
	{
		if (multiplierChanged)
		{
			Action<int, float> action = this.multiplierDidChangeEvent;
			if (action != null)
			{
				action(this._multiplier, (float)this._multiplierIncreaseProgress / (float)this._multiplierIncreaseMaxProgress);
			}
		}
		if (comboChanged)
		{
			Action<int> action2 = this.comboDidChangeEvent;
			if (action2 != null)
			{
				action2(this._combo);
			}
		}
		if (!this._feverIsActive)
		{
			Action<float> action3 = this.feverModeChargeProgressDidChangeEvent;
			if (action3 == null)
			{
				return;
			}
			action3(this.feverModeChargeProgress);
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x000394A4 File Offset: 0x000376A4
	private void HandleNoteWasCutEvent(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		bool multiplierChanged = false;
		bool comboChanged = false;
		if (noteCutInfo.allIsOK)
		{
			this._combo++;
			if (this._maxCombo < this._combo)
			{
				this._maxCombo = this._combo;
			}
			comboChanged = true;
			if (this._multiplier < 8)
			{
				if (this._multiplierIncreaseProgress < this._multiplierIncreaseMaxProgress)
				{
					this._multiplierIncreaseProgress++;
					multiplierChanged = true;
				}
				if (this._multiplierIncreaseProgress >= this._multiplierIncreaseMaxProgress)
				{
					this._multiplier *= 2;
					this._multiplierIncreaseProgress = 0;
					this._multiplierIncreaseMaxProgress = this._multiplier * 2;
					multiplierChanged = true;
				}
			}
			CutScoreBuffer cutScoreBuffer = new CutScoreBuffer(noteCutInfo, this.multiplierWithFever);
			CutScoreBuffer cutScoreBuffer2 = cutScoreBuffer;
			cutScoreBuffer2.didFinishEvent = (Action<CutScoreBuffer>)Delegate.Combine(cutScoreBuffer2.didFinishEvent, new Action<CutScoreBuffer>(this.HandleCutScoreBufferDidFinishEvent));
			this._cutScoreBuffers.Add(cutScoreBuffer);
		}
		else
		{
			this.LoseMultiplier(out comboChanged, out multiplierChanged);
		}
		if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			this._cutOrMissedNotes++;
		}
		this.NotifyForChange(comboChanged, multiplierChanged);
		Action<NoteData, NoteCutInfo, int> action = this.noteWasCutEvent;
		if (action == null)
		{
			return;
		}
		action(noteController.noteData, noteCutInfo, this._multiplier);
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x000395DC File Offset: 0x000377DC
	private void HandleNoteWasMissedEvent(INoteController noteController)
	{
		if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			this._cutOrMissedNotes++;
			bool comboChanged = false;
			bool multiplierChanged = false;
			this.LoseMultiplier(out comboChanged, out multiplierChanged);
			this.NotifyForChange(comboChanged, multiplierChanged);
		}
		Action<NoteData, int> action = this.noteWasMissedEvent;
		if (action == null)
		{
			return;
		}
		action(noteController.noteData, this._multiplier);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0000A8A4 File Offset: 0x00008AA4
	private void HandleCutScoreBufferDidFinishEvent(CutScoreBuffer cutScoreBuffer)
	{
		this._baseRawScore += cutScoreBuffer.scoreWithMultiplier;
		this._cutScoreBuffers.Remove(cutScoreBuffer);
	}

	// Token: 0x04000DF5 RID: 3573
	[Tooltip("When this amount of cuts is made without any mistake, fever mode will start.")]
	[SerializeField]
	private int _feverModeRequiredCombo = 100000;

	// Token: 0x04000DF6 RID: 3574
	[SerializeField]
	private float _feverModeDuration = 10f;

	// Token: 0x04000DF7 RID: 3575
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModel;

	// Token: 0x04000DF8 RID: 3576
	[Inject]
	private GameplayModifiers _gameplayModifiers;

	// Token: 0x04000DF9 RID: 3577
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000DFA RID: 3578
	[Inject]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	// Token: 0x04000DFB RID: 3579
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000E06 RID: 3590
	private int _baseRawScore;

	// Token: 0x04000E07 RID: 3591
	private int _prevFrameRawScore;

	// Token: 0x04000E08 RID: 3592
	private int _multiplier;

	// Token: 0x04000E09 RID: 3593
	private int _multiplierIncreaseProgress;

	// Token: 0x04000E0A RID: 3594
	private int _multiplierIncreaseMaxProgress;

	// Token: 0x04000E0B RID: 3595
	private int _combo;

	// Token: 0x04000E0C RID: 3596
	private int _maxCombo;

	// Token: 0x04000E0D RID: 3597
	private bool _feverIsActive;

	// Token: 0x04000E0E RID: 3598
	private float _feverStartTime;

	// Token: 0x04000E0F RID: 3599
	private int _feverCombo;

	// Token: 0x04000E10 RID: 3600
	private bool _playerHeadWasInObstacle;

	// Token: 0x04000E11 RID: 3601
	private int _immediateMaxPossibleRawScore;

	// Token: 0x04000E12 RID: 3602
	private int _cutOrMissedNotes;

	// Token: 0x04000E13 RID: 3603
	private List<CutScoreBuffer> _cutScoreBuffers = new List<CutScoreBuffer>();

	// Token: 0x04000E14 RID: 3604
	private float _gameplayModifiersScoreMultiplier;
}
