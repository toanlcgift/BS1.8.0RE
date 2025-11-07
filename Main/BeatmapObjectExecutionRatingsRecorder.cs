using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x02000237 RID: 567
public class BeatmapObjectExecutionRatingsRecorder : MonoBehaviour
{
	// Token: 0x17000263 RID: 611
	// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00007144 File Offset: 0x00005344
	public List<BeatmapObjectExecutionRating> beatmapObjectExecutionRatings
	{
		get
		{
			return this._beatmapObjectExecutionRatings;
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0002BE74 File Offset: 0x0002A074
	protected void Start()
	{
		this._scoreController.noteWasCutEvent += this.HandleScoreControllerNoteWasCut;
		this._scoreController.noteWasMissedEvent += this.HandleScoreControllerNoteWasMissed;
		this._beatmapObjectManager.obstacleDidPassAvoidedMarkEvent += this.HandleObstacleDidPassAvoidedMark;
		this._beatmapObjectExecutionRatings = new List<BeatmapObjectExecutionRating>(500);
		this._cutScoreHandlers = new List<BeatmapObjectExecutionRatingsRecorder.CutScoreHandler>(10);
		this._unusedCutScoreHandlers = new List<BeatmapObjectExecutionRatingsRecorder.CutScoreHandler>(10);
		for (int i = 0; i < this._unusedCutScoreHandlers.Capacity; i++)
		{
			this._unusedCutScoreHandlers.Add(new BeatmapObjectExecutionRatingsRecorder.CutScoreHandler(new BeatmapObjectExecutionRatingsRecorder.CutScoreHandler.DidFinishCallback(this.HandleCutScoreHandlerDidFinish)));
		}
		this._hitObstacles = new HashSet<int>();
		this._prevIntersectingObstacles = new List<ObstacleController>(10);
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0002BF3C File Offset: 0x0002A13C
	protected void OnDestroy()
	{
		if (this._scoreController != null)
		{
			this._scoreController.noteWasCutEvent -= this.HandleScoreControllerNoteWasCut;
			this._scoreController.noteWasMissedEvent -= this.HandleScoreControllerNoteWasMissed;
		}
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.obstacleDidPassAvoidedMarkEvent -= this.HandleObstacleDidPassAvoidedMark;
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0002BFAC File Offset: 0x0002A1AC
	protected void Update()
	{
		List<ObstacleController> intersectingObstacles = this._playerHeadAndObstacleInteraction.intersectingObstacles;
		for (int i = 0; i < intersectingObstacles.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < this._prevIntersectingObstacles.Count; j++)
			{
				if (intersectingObstacles[i] == this._prevIntersectingObstacles[j])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				int id = intersectingObstacles[i].obstacleData.id;
				this._hitObstacles.Add(id);
				ObstacleExecutionRating item = new ObstacleExecutionRating(this._audioTimeSyncController.songTime, ObstacleExecutionRating.Rating.NotGood);
				this._beatmapObjectExecutionRatings.Add(item);
			}
		}
		this._prevIntersectingObstacles.Clear();
		this._prevIntersectingObstacles.AddRange(intersectingObstacles);
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0000714C File Offset: 0x0000534C
	private void HandleCutScoreHandlerDidFinish(BeatmapObjectExecutionRatingsRecorder.CutScoreHandler cutScoreHandler)
	{
		this._unusedCutScoreHandlers.Add(cutScoreHandler);
		this._cutScoreHandlers.Remove(cutScoreHandler);
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0002C06C File Offset: 0x0002A26C
	private void HandleScoreControllerNoteWasCut(NoteData noteData, NoteCutInfo noteCutInfo, int multiplier)
	{
		if (noteData.noteType == NoteType.Bomb)
		{
			BombExecutionRating item = new BombExecutionRating(noteData.time, BombExecutionRating.Rating.NotGood);
			this._beatmapObjectExecutionRatings.Add(item);
			return;
		}
		if (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB)
		{
			NoteExecutionRating.Rating rating = noteCutInfo.allIsOK ? NoteExecutionRating.Rating.GoodCut : NoteExecutionRating.Rating.BadCut;
			NoteExecutionRating noteExecutionRating = new NoteExecutionRating(noteData.time, rating, 0, noteCutInfo.timeDeviation, noteCutInfo.cutDirDeviation);
			this._beatmapObjectExecutionRatings.Add(noteExecutionRating);
			if (noteCutInfo.allIsOK)
			{
				BeatmapObjectExecutionRatingsRecorder.CutScoreHandler cutScoreHandler;
				if (this._unusedCutScoreHandlers.Count > 0)
				{
					cutScoreHandler = this._unusedCutScoreHandlers[0];
					this._unusedCutScoreHandlers.RemoveAt(0);
				}
				else
				{
					cutScoreHandler = new BeatmapObjectExecutionRatingsRecorder.CutScoreHandler(new BeatmapObjectExecutionRatingsRecorder.CutScoreHandler.DidFinishCallback(this.HandleCutScoreHandlerDidFinish));
				}
				cutScoreHandler.Set(noteCutInfo, noteExecutionRating, noteCutInfo.swingRatingCounter);
				this._cutScoreHandlers.Add(cutScoreHandler);
			}
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0002C144 File Offset: 0x0002A344
	private void HandleScoreControllerNoteWasMissed(NoteData noteData, int multiplier)
	{
		if (noteData.noteType == NoteType.Bomb)
		{
			BombExecutionRating item = new BombExecutionRating(noteData.time, BombExecutionRating.Rating.OK);
			this._beatmapObjectExecutionRatings.Add(item);
			return;
		}
		if (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB)
		{
			NoteExecutionRating item2 = new NoteExecutionRating(noteData.time, NoteExecutionRating.Rating.Missed, 0, 0f, 0f);
			this._beatmapObjectExecutionRatings.Add(item2);
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0002C1AC File Offset: 0x0002A3AC
	private void HandleObstacleDidPassAvoidedMark(ObstacleController obstacleController)
	{
		if (this._hitObstacles.Contains(obstacleController.obstacleData.id))
		{
			this._hitObstacles.Remove(obstacleController.obstacleData.id);
			return;
		}
		ObstacleExecutionRating item = new ObstacleExecutionRating(obstacleController.obstacleData.time + obstacleController.obstacleData.duration, ObstacleExecutionRating.Rating.OK);
		this._beatmapObjectExecutionRatings.Add(item);
	}

	// Token: 0x04000950 RID: 2384
	[SerializeField]
	private ScoreController _scoreController;

	// Token: 0x04000951 RID: 2385
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000952 RID: 2386
	[Inject]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	// Token: 0x04000953 RID: 2387
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000954 RID: 2388
	private List<BeatmapObjectExecutionRating> _beatmapObjectExecutionRatings;

	// Token: 0x04000955 RID: 2389
	private HashSet<int> _hitObstacles;

	// Token: 0x04000956 RID: 2390
	private List<ObstacleController> _prevIntersectingObstacles;

	// Token: 0x04000957 RID: 2391
	private List<BeatmapObjectExecutionRatingsRecorder.CutScoreHandler> _cutScoreHandlers;

	// Token: 0x04000958 RID: 2392
	private List<BeatmapObjectExecutionRatingsRecorder.CutScoreHandler> _unusedCutScoreHandlers;

	// Token: 0x02000238 RID: 568
	private class CutScoreHandler
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00007167 File Offset: 0x00005367
		public CutScoreHandler(BeatmapObjectExecutionRatingsRecorder.CutScoreHandler.DidFinishCallback finishCallback)
		{
			this._finishCallback = finishCallback;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00007176 File Offset: 0x00005376
		public void Set(NoteCutInfo noteCutInfo, NoteExecutionRating noteExecutionRating, SaberSwingRatingCounter swingRatingCounter)
		{
			this._noteCutInfo = noteCutInfo;
			this._noteExecutionRating = noteExecutionRating;
			swingRatingCounter.didFinishEvent += this.HandleSwingRatingCounterDidFinishEvent;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002C214 File Offset: 0x0002A414
		private void HandleSwingRatingCounterDidFinishEvent(SaberSwingRatingCounter swingRatingCounter)
		{
			int num;
			int num2;
			int num3;
			ScoreModel.RawScoreWithoutMultiplier(this._noteCutInfo, out num, out num2, out num3);
			this._noteExecutionRating.cutScore += num + num2 + num3;
			swingRatingCounter.didFinishEvent -= this.HandleSwingRatingCounterDidFinishEvent;
			BeatmapObjectExecutionRatingsRecorder.CutScoreHandler.DidFinishCallback finishCallback = this._finishCallback;
			if (finishCallback == null)
			{
				return;
			}
			finishCallback(this);
		}

		// Token: 0x04000959 RID: 2393
		private BeatmapObjectExecutionRatingsRecorder.CutScoreHandler.DidFinishCallback _finishCallback;

		// Token: 0x0400095A RID: 2394
		private NoteExecutionRating _noteExecutionRating;

		// Token: 0x0400095B RID: 2395
		private NoteCutInfo _noteCutInfo;

		// Token: 0x02000239 RID: 569
		// (Invoke) Token: 0x060008EF RID: 2287
		public delegate void DidFinishCallback(BeatmapObjectExecutionRatingsRecorder.CutScoreHandler cutScoreHandler);
	}
}
