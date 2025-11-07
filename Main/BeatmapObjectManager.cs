using System;
using UnityEngine;
using Zenject;

// Token: 0x0200023A RID: 570
public class BeatmapObjectManager : MonoBehaviour, IBeatmapObjectSpawner
{
	// Token: 0x14000020 RID: 32
	// (add) Token: 0x060008F2 RID: 2290 RVA: 0x0002C26C File Offset: 0x0002A46C
	// (remove) Token: 0x060008F3 RID: 2291 RVA: 0x0002C2A4 File Offset: 0x0002A4A4
	public event Action<NoteController> noteWasSpawnedEvent;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x060008F4 RID: 2292 RVA: 0x0002C2DC File Offset: 0x0002A4DC
	// (remove) Token: 0x060008F5 RID: 2293 RVA: 0x0002C314 File Offset: 0x0002A514
	public event Action<INoteController> noteWasMissedEvent;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x060008F6 RID: 2294 RVA: 0x0002C34C File Offset: 0x0002A54C
	// (remove) Token: 0x060008F7 RID: 2295 RVA: 0x0002C384 File Offset: 0x0002A584
	public event Action<INoteController, NoteCutInfo> noteWasCutEvent;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x060008F8 RID: 2296 RVA: 0x0002C3BC File Offset: 0x0002A5BC
	// (remove) Token: 0x060008F9 RID: 2297 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
	public event Action<NoteController> noteDidStartJumpEvent;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x060008FA RID: 2298 RVA: 0x0002C42C File Offset: 0x0002A62C
	// (remove) Token: 0x060008FB RID: 2299 RVA: 0x0002C464 File Offset: 0x0002A664
	public event Action<ObstacleController> obstacleDidPassThreeQuartersOfMove2Event;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x060008FC RID: 2300 RVA: 0x0002C49C File Offset: 0x0002A69C
	// (remove) Token: 0x060008FD RID: 2301 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
	public event Action<ObstacleController> obstacleDidPassAvoidedMarkEvent;

	// Token: 0x060008FE RID: 2302 RVA: 0x0002C50C File Offset: 0x0002A70C
	public void SpawnObstacle(ObstacleData obstacleData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float rotation, float noteLinesDistance, float obstacleHeight)
	{
		ObstacleController obstacleController = this._obstaclePool.Spawn();
		this.SetObstacleEventCallbacks(obstacleController);
		obstacleController.transform.SetPositionAndRotation(moveStartPos, Quaternion.identity);
		obstacleController.Init(obstacleData, rotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, noteLinesDistance, obstacleHeight);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0002C554 File Offset: 0x0002A754
	public void SpawnBombNote(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float rotation)
	{
		NoteController noteController = this._bombNotePool.Spawn();
		this.SetNoteControllerEventCallbacks(noteController);
		noteController.transform.SetPositionAndRotation(moveStartPos, Quaternion.identity);
		noteController.Init(noteData, rotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, 0f);
		Action<NoteController> action = this.noteWasSpawnedEvent;
		if (action == null)
		{
			return;
		}
		action(noteController);
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
	public void SpawnBasicNote(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float rotation, bool disappearingArrow, bool ghostNote, float cutDirectionAngleOffset)
	{
		NoteController noteController = ((noteData.noteType == NoteType.NoteA) ? this._noteAPool : this._noteBPool).Spawn();
		this.SetNoteControllerEventCallbacks(noteController);
		noteController.transform.SetPositionAndRotation(moveStartPos, Quaternion.identity);
		GameNoteController gameNoteController = noteController as GameNoteController;
		if (gameNoteController != null)
		{
			gameNoteController.Init(noteData, rotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, disappearingArrow, ghostNote, cutDirectionAngleOffset);
		}
		else
		{
			noteController.Init(noteData, rotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, cutDirectionAngleOffset);
		}
		Action<NoteController> action = this.noteWasSpawnedEvent;
		if (action == null)
		{
			return;
		}
		action(noteController);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0002C644 File Offset: 0x0002A844
	private void SetNoteControllerEventCallbacks(NoteController noteController)
	{
		noteController.noteDidStartJumpEvent += this.HandleNoteDidStartJump;
		noteController.noteDidFinishJumpEvent += this.HandleNoteDidFinishJump;
		noteController.noteWasCutEvent += this.HandleNoteWasCut;
		noteController.noteWasMissedEvent += this.HandleNoteWasMissed;
		noteController.noteDidDissolveEvent += this.HandleNoteDidDissolve;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0002C6AC File Offset: 0x0002A8AC
	private void RemoveNoteControllerEventCallbacks(NoteController noteController)
	{
		noteController.noteDidStartJumpEvent -= this.HandleNoteDidStartJump;
		noteController.noteDidFinishJumpEvent -= this.HandleNoteDidFinishJump;
		noteController.noteWasCutEvent -= this.HandleNoteWasCut;
		noteController.noteWasMissedEvent -= this.HandleNoteWasMissed;
		noteController.noteDidDissolveEvent -= this.HandleNoteDidDissolve;
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0002C714 File Offset: 0x0002A914
	private void SetObstacleEventCallbacks(ObstacleController obstacleController)
	{
		obstacleController.finishedMovementEvent += this.HandleObstacleFinishedMovement;
		obstacleController.passedThreeQuartersOfMove2Event += this.HandleObstaclePassedThreeQuartersOfMove2;
		obstacleController.passedAvoidedMarkEvent += this.HandleObstaclePassedAvoidedMark;
		obstacleController.didDissolveEvent += this.HandleObstacleDidDissolve;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0002C76C File Offset: 0x0002A96C
	private void RemoveObstacleEventCallbacks(ObstacleController obstacleController)
	{
		obstacleController.finishedMovementEvent -= this.HandleObstacleFinishedMovement;
		obstacleController.passedThreeQuartersOfMove2Event -= this.HandleObstaclePassedThreeQuartersOfMove2;
		obstacleController.passedAvoidedMarkEvent -= this.HandleObstaclePassedAvoidedMark;
		obstacleController.didDissolveEvent -= this.HandleObstacleDidDissolve;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
	private void Despawn(NoteController noteController)
	{
		if (noteController.noteData.noteType == NoteType.NoteA)
		{
			this.RemoveNoteControllerEventCallbacks(noteController);
			this._noteAPool.Despawn(noteController);
			return;
		}
		if (noteController.noteData.noteType == NoteType.NoteB)
		{
			this.RemoveNoteControllerEventCallbacks(noteController);
			this._noteBPool.Despawn(noteController);
			return;
		}
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			this.RemoveNoteControllerEventCallbacks(noteController);
			this._bombNotePool.Despawn(noteController);
		}
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00007198 File Offset: 0x00005398
	private void Despawn(ObstacleController obstacleController)
	{
		this.RemoveObstacleEventCallbacks(obstacleController);
		this._obstaclePool.Despawn(obstacleController);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x000071AD File Offset: 0x000053AD
	private void HandleNoteDidStartJump(NoteController noteController)
	{
		Action<NoteController> action = this.noteDidStartJumpEvent;
		if (action == null)
		{
			return;
		}
		action(noteController);
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000071C0 File Offset: 0x000053C0
	private void HandleNoteWasMissed(NoteController noteController)
	{
		Action<INoteController> action = this.noteWasMissedEvent;
		if (action == null)
		{
			return;
		}
		action(noteController);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x000071D3 File Offset: 0x000053D3
	private void HandleNoteDidFinishJump(NoteController noteController)
	{
		this.Despawn(noteController);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x000071D3 File Offset: 0x000053D3
	private void HandleNoteDidDissolve(NoteController noteController)
	{
		this.Despawn(noteController);
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x000071DC File Offset: 0x000053DC
	private void HandleNoteWasCut(NoteController noteController, NoteCutInfo noteCutInfo)
	{
		Action<INoteController, NoteCutInfo> action = this.noteWasCutEvent;
		if (action != null)
		{
			action(noteController, noteCutInfo);
		}
		this.Despawn(noteController);
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x000071F8 File Offset: 0x000053F8
	private void HandleObstaclePassedThreeQuartersOfMove2(ObstacleController obstacleController)
	{
		Action<ObstacleController> action = this.obstacleDidPassThreeQuartersOfMove2Event;
		if (action == null)
		{
			return;
		}
		action(obstacleController);
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0000720B File Offset: 0x0000540B
	private void HandleObstaclePassedAvoidedMark(ObstacleController obstacleController)
	{
		Action<ObstacleController> action = this.obstacleDidPassAvoidedMarkEvent;
		if (action == null)
		{
			return;
		}
		action(obstacleController);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0000721E File Offset: 0x0000541E
	private void HandleObstacleFinishedMovement(ObstacleController obstacleController)
	{
		this.Despawn(obstacleController);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0000721E File Offset: 0x0000541E
	private void HandleObstacleDidDissolve(ObstacleController obstacleController)
	{
		this.Despawn(obstacleController);
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0002C838 File Offset: 0x0002AA38
	public void DissolveAllObjects()
	{
		float duration = 1.4f;
		foreach (NoteController noteController in this._noteAPool.activeItems)
		{
			noteController.Dissolve(duration);
		}
		foreach (NoteController noteController2 in this._noteBPool.activeItems)
		{
			noteController2.Dissolve(duration);
		}
		foreach (NoteController noteController3 in this._bombNotePool.activeItems)
		{
			((BombNoteController)noteController3).Dissolve(duration);
		}
		foreach (ObstacleController obstacleController in this._obstaclePool.activeItems)
		{
			obstacleController.Dissolve(duration);
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0002C968 File Offset: 0x0002AB68
	public void HideAllBeatmapObjects(bool hide)
	{
		foreach (NoteController noteController in this._noteAPool.activeItems)
		{
			noteController.hide = hide;
		}
		foreach (NoteController noteController2 in this._noteBPool.activeItems)
		{
			noteController2.hide = hide;
		}
		foreach (NoteController noteController3 in this._bombNotePool.activeItems)
		{
			((BombNoteController)noteController3).hide = hide;
		}
		foreach (ObstacleController obstacleController in this._obstaclePool.activeItems)
		{
			obstacleController.hide = hide;
		}
	}

	// Token: 0x0400095C RID: 2396
	[Inject(Id = NoteType.NoteA)]
	private NoteController.Pool _noteAPool;

	// Token: 0x0400095D RID: 2397
	[Inject(Id = NoteType.NoteB)]
	private NoteController.Pool _noteBPool;

	// Token: 0x0400095E RID: 2398
	[Inject(Id = NoteType.Bomb)]
	private NoteController.Pool _bombNotePool;

	// Token: 0x0400095F RID: 2399
	[Inject]
	private ObstacleController.Pool _obstaclePool;
}
