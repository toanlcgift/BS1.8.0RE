using System;
using UnityEngine;
using Zenject;

// Token: 0x0200023B RID: 571
public class BeatmapObjectSpawnController : MonoBehaviour
{
	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000913 RID: 2323 RVA: 0x00007227 File Offset: 0x00005427
	// (set) Token: 0x06000914 RID: 2324 RVA: 0x0000722F File Offset: 0x0000542F
	public float jumpOffsetY
	{
		get
		{
			return this._jumpOffsetY;
		}
		set
		{
			this._jumpOffsetY = value;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000915 RID: 2325 RVA: 0x00007238 File Offset: 0x00005438
	public float currentBPM
	{
		get
		{
			return this._variableBPMProcessor.currentBPM;
		}
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0002CA94 File Offset: 0x0002AC94
	protected void Start()
	{
		this._variableBPMProcessor.SetBPM(this._initData.beatsPerMinute);
		this._beatmapObjectSpawnMovementData.Init(this._initData.noteLinesCount, this._initData.noteJumpMovementSpeed, this._initData.beatsPerMinute, this._initData.noteJumpStartBeatOffset, this._initData.jumpOffsetY, base.transform.position, base.transform.right, base.transform.forward);
		this._beatmapCallbackItemDataList = new BeatmapCallbackItemDataList(new BeatmapCallbackItemDataList.SpawnNoteCallback(this.SpawnNote), new BeatmapCallbackItemDataList.SpawnObstacleCallback(this.SpawnObstacle), new BeatmapCallbackItemDataList.ProcessBeatmapEventCallback(this.ProcessEarlyBeatmapEventData), new BeatmapCallbackItemDataList.ProcessBeatmapEventCallback(this.ProcessLateBeatmapEventData), new Action(this.EarlyEventsWereProcessed), new BeatmapCallbackItemDataList.GetRelativeNoteOffsetCallback(this._beatmapObjectSpawnMovementData.Get2DNoteOffset));
		this._jumpOffsetY = this._initData.jumpOffsetY;
		this._disappearingArrows = this._initData.disappearingArrows;
		this._ghostNotes = this._initData.ghostNotes;
		if (this._beatmapObjectCallbackData != null)
		{
			this._beatmapObjectCallbackController.RemoveBeatmapObjectCallback(this._beatmapObjectCallbackData);
		}
		this._beatmapObjectCallbackData = this._beatmapObjectCallbackController.AddBeatmapObjectCallback(new BeatmapObjectCallbackController.BeatmapObjectCallback(this.HandleBeatmapObjectCallback), this._beatmapObjectSpawnMovementData.spawnAheadTime);
		if (this._beatmapEventCallbackData != null)
		{
			this._beatmapObjectCallbackController.RemoveBeatmapEventCallback(this._beatmapEventCallbackData);
		}
		this._beatmapEventCallbackData = this._beatmapObjectCallbackController.AddBeatmapEventCallback(new BeatmapObjectCallbackController.BeatmapEventCallback(this.HandleBeatmapEventCallback), this._beatmapObjectSpawnMovementData.spawnAheadTime);
		this._beatmapObjectCallbackController.callbacksForThisFrameWereProcessedEvent += this.HandleCallbacksForThisFrameWereProcessed;
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0002CC40 File Offset: 0x0002AE40
	protected void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.RemoveBeatmapObjectCallback(this._beatmapObjectCallbackData);
			this._beatmapObjectCallbackController.RemoveBeatmapEventCallback(this._beatmapEventCallbackData);
			this._beatmapObjectCallbackController.callbacksForThisFrameWereProcessedEvent -= this.HandleCallbacksForThisFrameWereProcessed;
		}
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00007245 File Offset: 0x00005445
	public Vector3 GetNoteOffset(int noteLineIndex, NoteLineLayer noteLineLayer)
	{
		return this._beatmapObjectSpawnMovementData.Get2DNoteOffset(noteLineIndex, noteLineLayer);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0002CC94 File Offset: 0x0002AE94
	private void SpawnObstacle(ObstacleData obstacleData)
	{
		if (this._disableSpawning)
		{
			return;
		}
		Vector3 moveStartPos;
		Vector3 moveEndPos;
		Vector3 jumpEndPos;
		float obstacleHeight;
		this._beatmapObjectSpawnMovementData.GetObstacleSpawnMovementData(obstacleData, out moveStartPos, out moveEndPos, out jumpEndPos, out obstacleHeight);
		float moveDuration = this._beatmapObjectSpawnMovementData.moveDuration;
		float jumpDuration = this._beatmapObjectSpawnMovementData.jumpDuration;
		float noteLinesDistance = this._beatmapObjectSpawnMovementData.noteLinesDistance;
		float rotation = this._spawnRotationProcesser.rotation;
		this._beatmapObjectSpawner.SpawnObstacle(obstacleData, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, rotation, noteLinesDistance, obstacleHeight);
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0002CD0C File Offset: 0x0002AF0C
	private void SpawnNote(NoteData noteData, float cutDirectionAngleOffset)
	{
		if (this._disableSpawning)
		{
			return;
		}
		Vector3 moveStartPos;
		Vector3 moveEndPos;
		Vector3 jumpEndPos;
		float jumpGravity;
		this._beatmapObjectSpawnMovementData.GetNoteSpawnMovementData(noteData, out moveStartPos, out moveEndPos, out jumpEndPos, out jumpGravity);
		float moveDuration = this._beatmapObjectSpawnMovementData.moveDuration;
		float jumpDuration = this._beatmapObjectSpawnMovementData.jumpDuration;
		float rotation = this._spawnRotationProcesser.rotation;
		if (noteData.noteType == NoteType.Bomb)
		{
			this._beatmapObjectSpawner.SpawnBombNote(noteData, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, rotation);
			return;
		}
		if (noteData.noteType.IsBasicNote())
		{
			if (this._firstBasicNoteTime == null)
			{
				this._firstBasicNoteTime = new float?(noteData.time);
			}
			float? firstBasicNoteTime = this._firstBasicNoteTime;
			float time = noteData.time;
			bool flag = firstBasicNoteTime.GetValueOrDefault() == time & firstBasicNoteTime != null;
			this._beatmapObjectSpawner.SpawnBasicNote(noteData, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, rotation, this._disappearingArrows, this._ghostNotes && !flag, cutDirectionAngleOffset);
		}
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00007259 File Offset: 0x00005459
	private void ProcessEarlyBeatmapEventData(BeatmapEventData beatmapEventData)
	{
		this._spawnRotationProcesser.ProcessBeatmapEventData(beatmapEventData);
		this._variableBPMProcessor.ProcessBeatmapEventData(beatmapEventData);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0002CDFC File Offset: 0x0002AFFC
	private void EarlyEventsWereProcessed()
	{
		this._beatmapObjectSpawnMovementData.Update(this._variableBPMProcessor.currentBPM, this._jumpOffsetY);
		this._beatmapEventCallbackData.aheadTime = this._beatmapObjectSpawnMovementData.spawnAheadTime;
		this._beatmapObjectCallbackData.aheadTime = this._beatmapObjectSpawnMovementData.spawnAheadTime;
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00007275 File Offset: 0x00005475
	private void ProcessLateBeatmapEventData(BeatmapEventData beatmapEventData)
	{
		this._spawnRotationProcesser.ProcessBeatmapEventData(beatmapEventData);
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00007284 File Offset: 0x00005484
	private void HandleBeatmapObjectCallback(BeatmapObjectData beatmapObjectData)
	{
		this._beatmapCallbackItemDataList.InsertBeatmapObjectData(beatmapObjectData);
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00007292 File Offset: 0x00005492
	private void HandleBeatmapEventCallback(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type.IsSpawnAffectingEvent())
		{
			this._beatmapCallbackItemDataList.InsertBeatmapEventData(beatmapEventData);
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x000072AD File Offset: 0x000054AD
	private void HandleCallbacksForThisFrameWereProcessed()
	{
		this._beatmapCallbackItemDataList.ProcessData();
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x000072BA File Offset: 0x000054BA
	public void StopSpawning()
	{
		this._disableSpawning = true;
	}

	// Token: 0x04000966 RID: 2406
	[SerializeField]
	private BeatmapObjectSpawnMovementData _beatmapObjectSpawnMovementData = new BeatmapObjectSpawnMovementData();

	// Token: 0x04000967 RID: 2407
	[Inject]
	private BeatmapObjectSpawnController.InitData _initData;

	// Token: 0x04000968 RID: 2408
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000969 RID: 2409
	[Inject]
	private IBeatmapObjectSpawner _beatmapObjectSpawner;

	// Token: 0x0400096A RID: 2410
	private float _jumpOffsetY;

	// Token: 0x0400096B RID: 2411
	private bool _disableSpawning;

	// Token: 0x0400096C RID: 2412
	private BeatmapObjectCallbackController.BeatmapObjectCallbackData _beatmapObjectCallbackData;

	// Token: 0x0400096D RID: 2413
	private BeatmapObjectCallbackController.BeatmapEventCallbackData _beatmapEventCallbackData;

	// Token: 0x0400096E RID: 2414
	private bool _disappearingArrows;

	// Token: 0x0400096F RID: 2415
	private bool _ghostNotes;

	// Token: 0x04000970 RID: 2416
	private float? _firstBasicNoteTime;

	// Token: 0x04000971 RID: 2417
	private BeatmapCallbackItemDataList _beatmapCallbackItemDataList;

	// Token: 0x04000972 RID: 2418
	private SpawnRotationProcessor _spawnRotationProcesser = new SpawnRotationProcessor();

	// Token: 0x04000973 RID: 2419
	private VariableBPMProcessor _variableBPMProcessor = new VariableBPMProcessor();

	// Token: 0x0200023C RID: 572
	public class InitData
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x000072EC File Offset: 0x000054EC
		public InitData(float beatsPerMinute, int noteLinesCount, float noteJumpMovementSpeed, float noteJumpStartBeatOffset, bool disappearingArrows, bool ghostNotes, float jumpOffsetY)
		{
			this.beatsPerMinute = beatsPerMinute;
			this.noteLinesCount = noteLinesCount;
			this.noteJumpMovementSpeed = noteJumpMovementSpeed;
			this.noteJumpStartBeatOffset = noteJumpStartBeatOffset;
			this.disappearingArrows = disappearingArrows;
			this.ghostNotes = ghostNotes;
			this.jumpOffsetY = jumpOffsetY;
		}

		// Token: 0x04000974 RID: 2420
		public readonly float beatsPerMinute;

		// Token: 0x04000975 RID: 2421
		public readonly int noteLinesCount;

		// Token: 0x04000976 RID: 2422
		public readonly float noteJumpMovementSpeed;

		// Token: 0x04000977 RID: 2423
		public readonly float noteJumpStartBeatOffset;

		// Token: 0x04000978 RID: 2424
		public readonly bool disappearingArrows;

		// Token: 0x04000979 RID: 2425
		public readonly bool ghostNotes;

		// Token: 0x0400097A RID: 2426
		public readonly float jumpOffsetY;
	}
}
