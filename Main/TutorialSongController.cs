using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

// Token: 0x02000323 RID: 803
public class TutorialSongController : SongController
{
	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0000AE10 File Offset: 0x00009010
	// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0000AE18 File Offset: 0x00009018
	public bool specialTutorialMode { get; set; }

	// Token: 0x06000E14 RID: 3604 RVA: 0x0003A358 File Offset: 0x00038558
	protected void Awake()
	{
		this._normalModeTutorialObjectsSpawnData = new TutorialSongController.TutorialObjectSpawnData[]
		{
			new TutorialSongController.TutorialNoteSpawnData(this._noteCuttingTutorialPartDidStartSignal, 9, 9, 2, NoteLineLayer.Base, NoteCutDirection.Down, NoteType.NoteB),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.Down, NoteType.NoteA),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 2, NoteLineLayer.Base, NoteCutDirection.Right, NoteType.NoteB),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 2, NoteLineLayer.Upper, NoteCutDirection.Right, NoteType.NoteA),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Upper, NoteCutDirection.Left, NoteType.NoteB),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.Up, NoteType.NoteA),
			new TutorialSongController.TutorialNoteSpawnData(this._noteCuttingInAnyDirectionDidStartSignal, 9, 9, 2, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteB),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteA),
			new TutorialSongController.TutorialNoteSpawnData(this._bombCuttingTutorialPartDidStartSignal, 17, 9, 2, NoteLineLayer.Base, NoteCutDirection.None, NoteType.Bomb),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.None, NoteType.Bomb),
			new TutorialSongController.TutorialObstacleSpawnData(this._rightObstacleTutorialPartDidStartSignal, 9, 9, 2, 1, ObstacleType.FullHeight),
			new TutorialSongController.TutorialObstacleSpawnData(this._leftObstacleTutorialPartDidStartSignal, 9, 9, 1, 1, ObstacleType.FullHeight),
			new TutorialSongController.TutorialObstacleSpawnData(this._topObstacleTutorialPartDidStartSignal, 9, 9, 0, 4, ObstacleType.Top)
		};
		this._specialModeTutorialObjectsSpawnData = new TutorialSongController.TutorialObjectSpawnData[]
		{
			new TutorialSongController.TutorialNoteSpawnData(this._noteCuttingInAnyDirectionDidStartSignal, 9, 9, 2, NoteLineLayer.Base, NoteCutDirection.Any, NoteType.NoteB),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.Any, NoteType.NoteA),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteA),
			new TutorialSongController.TutorialNoteSpawnData(null, 9, 9, 2, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteB)
		};
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0003A4C8 File Offset: 0x000386C8
	protected void Start()
	{
		this._songBPM = this._initData.songBPM;
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCutEvent;
		this._beatmapObjectManager.noteWasMissedEvent += this.HandleNoteWasMissed;
		this._beatmapObjectManager.obstacleDidPassThreeQuartersOfMove2Event += this.HandleObstacleDidPassThreeQuartersOfMove2;
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0003A52C File Offset: 0x0003872C
	protected void OnDestroy()
	{
		this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCutEvent;
		this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissed;
		this._beatmapObjectManager.obstacleDidPassThreeQuartersOfMove2Event -= this.HandleObstacleDidPassThreeQuartersOfMove2;
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0003A580 File Offset: 0x00038780
	public override void StartSong()
	{
		float num = 60f / this._songBPM;
		this.UpdateBeatmapData(num * (float)this._startWaitTimeInBeats);
		base.StartCoroutine(this.StartSongCoroutine());
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x0000AE21 File Offset: 0x00009021
	private IEnumerator StartSongCoroutine()
	{
		WaitUntil waitUntilAudioIsLoaded = this._audioTimeSyncController.waitUntilAudioIsLoaded;
		yield return waitUntilAudioIsLoaded;
		this._audioTimeSyncController.StartSong();
		yield break;
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x0000AE30 File Offset: 0x00009030
	public override void StopSong()
	{
		base.StopAllCoroutines();
		this._audioTimeSyncController.StopSong();
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0000AE43 File Offset: 0x00009043
	public override void PauseSong()
	{
		base.StopAllCoroutines();
		this._audioTimeSyncController.Pause();
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0000AE56 File Offset: 0x00009056
	public override void ResumeSong()
	{
		this._audioTimeSyncController.Resume();
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0003A5B8 File Offset: 0x000387B8
	private void HandleNoteWasCutEvent(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			this._bombWasCutSignal.Raise();
			this.UpdateBeatmapData(-1f);
			return;
		}
		if (this.specialTutorialMode)
		{
			if (!noteCutInfo.allExceptSaberTypeIsOK || noteCutInfo.saberTypeOK)
			{
				this.UpdateBeatmapData(-1f);
				return;
			}
		}
		else if (!noteCutInfo.allIsOK)
		{
			if (noteCutInfo.wasCutTooSoon)
			{
				this._noteWasCutTooSoonSignal.Raise();
			}
			else if (!noteCutInfo.saberTypeOK)
			{
				this._noteWasCutWithWrongColorSignal.Raise();
			}
			else if (!noteCutInfo.speedOK)
			{
				this._noteWasCutWithSlowSpeedSignal.Raise();
			}
			else if (!noteCutInfo.directionOK)
			{
				this._noteWasCutFromDifferentDirectionSignal.Raise();
			}
			this.UpdateBeatmapData(-1f);
			return;
		}
		this._noteWasCutOKSignal.Raise();
		this._tutorialBeatmapObjectIndex++;
		this.UpdateBeatmapData(-1f);
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0003A698 File Offset: 0x00038898
	private void HandleNoteWasMissed(INoteController noteController)
	{
		if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			this.UpdateBeatmapData(-1f);
			return;
		}
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			this._noteWasCutOKSignal.Raise();
			this._tutorialBeatmapObjectIndex++;
			this.UpdateBeatmapData(-1f);
		}
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x0000AE63 File Offset: 0x00009063
	private void HandleObstacleDidPassThreeQuartersOfMove2(ObstacleController obstacleController)
	{
		this._tutorialBeatmapObjectIndex++;
		this.UpdateBeatmapData(-1f);
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x0003A700 File Offset: 0x00038900
	protected void UpdateBeatmapData(float noteTime)
	{
		TutorialSongController.TutorialObjectSpawnData[] array;
		if (this.specialTutorialMode)
		{
			array = this._specialModeTutorialObjectsSpawnData;
		}
		else
		{
			array = this._normalModeTutorialObjectsSpawnData;
		}
		if (this._tutorialBeatmapObjectIndex >= array.Length)
		{
			base.SendSongDidFinishEvent();
			return;
		}
		BeatmapLineData[] beatmapLinesData = null;
		TutorialSongController.TutorialObjectSpawnData tutorialObjectSpawnData = array[this._tutorialBeatmapObjectIndex];
		bool flag = this._prevSpawnedBeatmapObjectIndex != this._tutorialBeatmapObjectIndex;
		if (flag && tutorialObjectSpawnData.gameEvent != null)
		{
			tutorialObjectSpawnData.gameEvent.Raise();
		}
		float time = (noteTime > 0f) ? noteTime : this.GetNextBeatmapObjectTime(flag ? tutorialObjectSpawnData.firstTimeBeatOffset : tutorialObjectSpawnData.beatOffset);
		if (tutorialObjectSpawnData is TutorialSongController.TutorialNoteSpawnData)
		{
			if (this.specialTutorialMode)
			{
				int num = this._nextBeatmapObjectId % 4;
				tutorialObjectSpawnData = this._specialModeTutorialObjectsSpawnData[num];
			}
			beatmapLinesData = this.CreateBeatmapLinesData(time, tutorialObjectSpawnData as TutorialSongController.TutorialNoteSpawnData);
		}
		else if (tutorialObjectSpawnData is TutorialSongController.TutorialObstacleSpawnData)
		{
			beatmapLinesData = this.CreateBeatmapLinesData(time, tutorialObjectSpawnData as TutorialSongController.TutorialObstacleSpawnData);
		}
		BeatmapData newBeatmapData = new BeatmapData(beatmapLinesData, new BeatmapEventData[0]);
		this._beatmapObjectCallbackController.SetNewBeatmapData(newBeatmapData);
		this._prevSpawnedBeatmapObjectIndex = this._tutorialBeatmapObjectIndex;
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0003A808 File Offset: 0x00038A08
	private float GetNextBeatmapObjectTime(int beatOffset)
	{
		float num = 60f / this._songBPM;
		return (float)((int)(this._audioTimeSyncController.songTime / (num * (float)this._numberOfBeatsToSnap) + 0.5f) * this._numberOfBeatsToSnap + 1 + (beatOffset - 1) - 1) * num;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0003A850 File Offset: 0x00038A50
	private BeatmapLineData[] CreateBeatmapLinesData(float time, TutorialSongController.TutorialObstacleSpawnData tutorialObstacleSpawnData)
	{
		float num = 60f / this._songBPM;
		int lineIndex = tutorialObstacleSpawnData.lineIndex;
		BeatmapLineData[] array = this.CreateBeatmapLines(4, lineIndex);
		BeatmapObjectData[] beatmapObjectsData = array[lineIndex].beatmapObjectsData;
		int num2 = 0;
		int nextBeatmapObjectId = this._nextBeatmapObjectId;
		this._nextBeatmapObjectId = nextBeatmapObjectId + 1;
		beatmapObjectsData[num2] = new ObstacleData(nextBeatmapObjectId, time, lineIndex, tutorialObstacleSpawnData.obstacleType, (float)this._obstacleDurationInBeats * num, tutorialObstacleSpawnData.width);
		return array;
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0003A8B0 File Offset: 0x00038AB0
	private BeatmapLineData[] CreateBeatmapLinesData(float time, TutorialSongController.TutorialNoteSpawnData tutorialNoteSpawnData)
	{
		float num = 60f / this._songBPM;
		int lineIndex = tutorialNoteSpawnData.lineIndex;
		NoteLineLayer noteLineLayer = tutorialNoteSpawnData.noteLineLayer;
		BeatmapLineData[] array = this.CreateBeatmapLines(4, lineIndex);
		BeatmapObjectData[] beatmapObjectsData = array[lineIndex].beatmapObjectsData;
		int num2 = 0;
		int nextBeatmapObjectId = this._nextBeatmapObjectId;
		this._nextBeatmapObjectId = nextBeatmapObjectId + 1;
		beatmapObjectsData[num2] = new NoteData(nextBeatmapObjectId, time, lineIndex, noteLineLayer, NoteLineLayer.Base, tutorialNoteSpawnData.noteType, tutorialNoteSpawnData.cutDirection, num * 4f, num * 4f);
		return array;
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0003A920 File Offset: 0x00038B20
	private BeatmapLineData[] CreateBeatmapLines(int lineCount, int activeLineIndex)
	{
		BeatmapLineData[] array = new BeatmapLineData[lineCount];
		for (int i = 0; i < lineCount; i++)
		{
			array[i] = new BeatmapLineData();
			bool flag = activeLineIndex == i || activeLineIndex == -1;
			array[i].beatmapObjectsData = new BeatmapObjectData[flag ? 1 : 0];
		}
		return array;
	}

	// Token: 0x04000E68 RID: 3688
	[SerializeField]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000E69 RID: 3689
	[SerializeField]
	private int _startWaitTimeInBeats = 8;

	// Token: 0x04000E6A RID: 3690
	[SerializeField]
	private int _numberOfBeatsToSnap = 8;

	// Token: 0x04000E6B RID: 3691
	[SerializeField]
	private int _obstacleDurationInBeats = 2;

	// Token: 0x04000E6C RID: 3692
	[Space]
	[FormerlySerializedAs("noteCuttingTutorialPartDidStartEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteCuttingTutorialPartDidStartSignal;

	// Token: 0x04000E6D RID: 3693
	[FormerlySerializedAs("noteCuttingInAnyDirectionDidStartEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteCuttingInAnyDirectionDidStartSignal;

	// Token: 0x04000E6E RID: 3694
	[FormerlySerializedAs("bombCuttingTutorialPartDidStartEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _bombCuttingTutorialPartDidStartSignal;

	// Token: 0x04000E6F RID: 3695
	[FormerlySerializedAs("leftObstacleTutorialPartDidStartEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _leftObstacleTutorialPartDidStartSignal;

	// Token: 0x04000E70 RID: 3696
	[FormerlySerializedAs("rightObstacleTutorialPartDidStartEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _rightObstacleTutorialPartDidStartSignal;

	// Token: 0x04000E71 RID: 3697
	[FormerlySerializedAs("topObstacleTutorialPartDidStartEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _topObstacleTutorialPartDidStartSignal;

	// Token: 0x04000E72 RID: 3698
	[FormerlySerializedAs("noteWasCutOKEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteWasCutOKSignal;

	// Token: 0x04000E73 RID: 3699
	[FormerlySerializedAs("noteWasCutTooSoonEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteWasCutTooSoonSignal;

	// Token: 0x04000E74 RID: 3700
	[FormerlySerializedAs("noteWasCutWithWrongColorEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteWasCutWithWrongColorSignal;

	// Token: 0x04000E75 RID: 3701
	[FormerlySerializedAs("noteWasCutFromDifferentDirectionEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteWasCutFromDifferentDirectionSignal;

	// Token: 0x04000E76 RID: 3702
	[FormerlySerializedAs("noteWasCutWithSlowSpeedEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _noteWasCutWithSlowSpeedSignal;

	// Token: 0x04000E77 RID: 3703
	[FormerlySerializedAs("bombWasCutEvent")]
	[SerializeField]
	[SignalSender]
	private Signal _bombWasCutSignal;

	// Token: 0x04000E78 RID: 3704
	[Inject]
	private TutorialSongController.InitData _initData;

	// Token: 0x04000E79 RID: 3705
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000E7A RID: 3706
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000E7C RID: 3708
	private int _tutorialBeatmapObjectIndex;

	// Token: 0x04000E7D RID: 3709
	private int _prevSpawnedBeatmapObjectIndex = -1;

	// Token: 0x04000E7E RID: 3710
	private int _nextBeatmapObjectId;

	// Token: 0x04000E7F RID: 3711
	private float _songBPM;

	// Token: 0x04000E80 RID: 3712
	private TutorialSongController.TutorialObjectSpawnData[] _normalModeTutorialObjectsSpawnData;

	// Token: 0x04000E81 RID: 3713
	private TutorialSongController.TutorialObjectSpawnData[] _specialModeTutorialObjectsSpawnData;

	// Token: 0x02000324 RID: 804
	public class InitData
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x0000AEA2 File Offset: 0x000090A2
		public InitData(float songBPM)
		{
			this.songBPM = songBPM;
		}

		// Token: 0x04000E82 RID: 3714
		public readonly float songBPM;
	}

	// Token: 0x02000325 RID: 805
	public class TutorialObjectSpawnData
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x0000AEB1 File Offset: 0x000090B1
		public TutorialObjectSpawnData(Signal gameEvent, int firstTimeBeatOffset, int beatOffset)
		{
			this.gameEvent = gameEvent;
			this.firstTimeBeatOffset = firstTimeBeatOffset;
			this.beatOffset = beatOffset;
		}

		// Token: 0x04000E83 RID: 3715
		public Signal gameEvent;

		// Token: 0x04000E84 RID: 3716
		public int beatOffset;

		// Token: 0x04000E85 RID: 3717
		public int firstTimeBeatOffset;
	}

	// Token: 0x02000326 RID: 806
	public class TutorialNoteSpawnData : TutorialSongController.TutorialObjectSpawnData
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x0000AECE File Offset: 0x000090CE
		public TutorialNoteSpawnData(Signal gameEvent, int firstTimeBeatOffset, int beatOffset, int line, NoteLineLayer noteLineLayer, NoteCutDirection cutDirection, NoteType noteType) : base(gameEvent, firstTimeBeatOffset, beatOffset)
		{
			this.gameEvent = gameEvent;
			this.noteLineLayer = noteLineLayer;
			this.lineIndex = line;
			this.cutDirection = cutDirection;
			this.noteType = noteType;
		}

		// Token: 0x04000E86 RID: 3718
		public int lineIndex;

		// Token: 0x04000E87 RID: 3719
		public NoteLineLayer noteLineLayer;

		// Token: 0x04000E88 RID: 3720
		public NoteCutDirection cutDirection;

		// Token: 0x04000E89 RID: 3721
		public NoteType noteType;
	}

	// Token: 0x02000327 RID: 807
	public class TutorialObstacleSpawnData : TutorialSongController.TutorialObjectSpawnData
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x0000AF00 File Offset: 0x00009100
		public TutorialObstacleSpawnData(Signal gameEvent, int firstTimeBeatOffset, int beatOffset, int lineIndex, int width, ObstacleType obstacleType) : base(gameEvent, firstTimeBeatOffset, beatOffset)
		{
			this.gameEvent = gameEvent;
			this.lineIndex = lineIndex;
			this.width = width;
			this.obstacleType = obstacleType;
		}

		// Token: 0x04000E8A RID: 3722
		public int lineIndex;

		// Token: 0x04000E8B RID: 3723
		public int width;

		// Token: 0x04000E8C RID: 3724
		public ObstacleType obstacleType;
	}
}
