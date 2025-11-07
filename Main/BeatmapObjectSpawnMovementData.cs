using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
[Serializable]
public class BeatmapObjectSpawnMovementData
{
	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000924 RID: 2340 RVA: 0x00007329 File Offset: 0x00005529
	public float spawnAheadTime
	{
		get
		{
			return this._spawnAheadTime;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000925 RID: 2341 RVA: 0x00007331 File Offset: 0x00005531
	public float moveDuration
	{
		get
		{
			return this._moveDuration;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000926 RID: 2342 RVA: 0x00007339 File Offset: 0x00005539
	public float jumpDuration
	{
		get
		{
			return this._jumpDuration;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x00007341 File Offset: 0x00005541
	public float noteLinesDistance
	{
		get
		{
			return this._noteLinesDistance;
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0002CE54 File Offset: 0x0002B054
	public void Init(int noteLinesCount, float startNoteJumpMovementSpeed, float startBPM, float noteJumpStartBeatOffset, float jumpOffsetY, Vector3 centerPos, Vector3 rightVec, Vector3 forwardVec)
	{
		this._noteLinesCount = (float)noteLinesCount;
		this._startNoteJumpMovementSpeed = startNoteJumpMovementSpeed;
		this._noteJumpStartBeatOffset = noteJumpStartBeatOffset;
		this._startBPM = startBPM;
		this._rightVec = rightVec;
		this._forwardVec = forwardVec;
		this._centerPos = centerPos;
		this._jumpOffsetY = jumpOffsetY;
		this._moveDistance = this._moveSpeed * this._moveDuration;
		this.Update(this._startBPM, this._jumpOffsetY);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0002CEC4 File Offset: 0x0002B0C4
	public void Update(float bpm, float jumpOffsetY)
	{
		this._jumpOffsetY = jumpOffsetY;
		this._noteJumpMovementSpeed = this._startNoteJumpMovementSpeed * bpm / this._startBPM;
		float num = 60f / bpm;
		float num2 = this._startHalfJumpDurationInBeats;
		while (this._noteJumpMovementSpeed * num * num2 > this._maxHalfJumpDistance)
		{
			num2 /= 2f;
		}
		num2 += this._noteJumpStartBeatOffset;
		if (num2 < 1f)
		{
			num2 = 1f;
		}
		this._jumpDuration = num * num2 * 2f;
		this._jumpDistance = this._noteJumpMovementSpeed * this._jumpDuration;
		this._spawnAheadTime = this._moveDuration + this._jumpDuration * 0.5f;
		this._moveStartPos = this._centerPos + this._forwardVec * (this._moveDistance + this._jumpDistance * 0.5f);
		this._moveEndPos = this._centerPos + this._forwardVec * this._jumpDistance * 0.5f;
		this._jumpEndPos = this._centerPos - this._forwardVec * this._jumpDistance * 0.5f;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0002CFF4 File Offset: 0x0002B1F4
	public void GetObstacleSpawnMovementData(ObstacleData obstacleData, out Vector3 moveStartPos, out Vector3 moveEndPos, out Vector3 jumpEndPos, out float obstacleHeight)
	{
		Vector3 noteOffset = this.GetNoteOffset(obstacleData.lineIndex, NoteLineLayer.Base);
		noteOffset.y = ((obstacleData.obstacleType == ObstacleType.Top) ? (this._topObstaclePosY + this._jumpOffsetY) : this._verticalObstaclePosY);
		obstacleHeight = ((obstacleData.obstacleType == ObstacleType.Top) ? this._topObstacleHeight : this._verticalObstacleHeight);
		moveStartPos = this._moveStartPos + noteOffset;
		moveEndPos = this._moveEndPos + noteOffset;
		jumpEndPos = this._jumpEndPos + noteOffset;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0002D088 File Offset: 0x0002B288
	public void GetNoteSpawnMovementData(NoteData noteData, out Vector3 moveStartPos, out Vector3 moveEndPos, out Vector3 jumpEndPos, out float jumpGravity)
	{
		Vector3 noteOffset = this.GetNoteOffset(noteData.lineIndex, noteData.startNoteLineLayer);
		jumpGravity = this.NoteJumpGravityForLineLayer(noteData.noteLineLayer, noteData.startNoteLineLayer);
		jumpEndPos = this._jumpEndPos + noteOffset;
		if (noteData.noteType.IsBasicNote())
		{
			Vector3 noteOffset2 = this.GetNoteOffset(noteData.flipLineIndex, noteData.startNoteLineLayer);
			moveStartPos = this._moveStartPos + noteOffset2;
			moveEndPos = this._moveEndPos + noteOffset2;
			return;
		}
		moveStartPos = this._moveStartPos + noteOffset;
		moveEndPos = this._moveEndPos + noteOffset;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0002D13C File Offset: 0x0002B33C
	public Vector3 GetNoteOffset(int noteLineIndex, NoteLineLayer noteLineLayer)
	{
		float num = -(this._noteLinesCount - 1f) * 0.5f;
		num = (num + (float)noteLineIndex) * this._noteLinesDistance;
		return this._rightVec * num + new Vector3(0f, this.LineYPosForLineLayer(noteLineLayer), 0f);
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00007349 File Offset: 0x00005549
	public Vector2 Get2DNoteOffset(int noteLineIndex, NoteLineLayer noteLineLayer)
	{
		return new Vector2((-(this._noteLinesCount - 1f) * 0.5f + (float)noteLineIndex) * this._noteLinesDistance, this.LineYPosForLineLayer(noteLineLayer));
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00007374 File Offset: 0x00005574
	private float HighestJumpPosYForLineLayer(NoteLineLayer lineLayer)
	{
		if (lineLayer == NoteLineLayer.Base)
		{
			return this._baseLinesHighestJumpPosY + this._jumpOffsetY;
		}
		if (lineLayer == NoteLineLayer.Upper)
		{
			return this._upperLinesHighestJumpPosY + this._jumpOffsetY;
		}
		return this._topLinesHighestJumpPosY + this._jumpOffsetY;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x000073A6 File Offset: 0x000055A6
	private float LineYPosForLineLayer(NoteLineLayer lineLayer)
	{
		if (lineLayer == NoteLineLayer.Base)
		{
			return this._baseLinesYPos;
		}
		if (lineLayer == NoteLineLayer.Upper)
		{
			return this._upperLinesYPos;
		}
		return this._topLinesYPos;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x000073C3 File Offset: 0x000055C3
	private float NoteJumpGravityForLineLayer(NoteLineLayer lineLayer, NoteLineLayer startLineLayer)
	{
		return 2f * (this.HighestJumpPosYForLineLayer(lineLayer) - this.LineYPosForLineLayer(startLineLayer)) / Mathf.Pow(this._jumpDistance / this._noteJumpMovementSpeed * 0.5f, 2f);
	}

	// Token: 0x0400097B RID: 2427
	[Header("Global")]
	[SerializeField]
	private float _noteLinesDistance = 0.6f;

	// Token: 0x0400097C RID: 2428
	[Header("Jump")]
	[Tooltip("If half jump distance computed using halfJumpDurationInBeats is longer than this value, it is devided by two until it's smaller.")]
	[SerializeField]
	private float _maxHalfJumpDistance = 18f;

	// Token: 0x0400097D RID: 2429
	[SerializeField]
	private float _startHalfJumpDurationInBeats = 4f;

	// Token: 0x0400097E RID: 2430
	[SerializeField]
	private float _baseLinesHighestJumpPosY = 0.85f;

	// Token: 0x0400097F RID: 2431
	[SerializeField]
	private float _upperLinesHighestJumpPosY = 1.4f;

	// Token: 0x04000980 RID: 2432
	[SerializeField]
	private float _topLinesHighestJumpPosY = 1.9f;

	// Token: 0x04000981 RID: 2433
	[Header("Arrival")]
	[SerializeField]
	private float _moveSpeed = 200f;

	// Token: 0x04000982 RID: 2434
	[SerializeField]
	private float _moveDuration = 1f;

	// Token: 0x04000983 RID: 2435
	[SerializeField]
	private float _baseLinesYPos = 0.25f;

	// Token: 0x04000984 RID: 2436
	[SerializeField]
	private float _upperLinesYPos = 0.85f;

	// Token: 0x04000985 RID: 2437
	[SerializeField]
	private float _topLinesYPos = 1.45f;

	// Token: 0x04000986 RID: 2438
	[Header("Obstacles")]
	[SerializeField]
	private float _verticalObstaclePosY = 0.1f;

	// Token: 0x04000987 RID: 2439
	[SerializeField]
	private float _topObstaclePosY = 1.3f;

	// Token: 0x04000988 RID: 2440
	[SerializeField]
	private float _verticalObstacleHeight = 3f;

	// Token: 0x04000989 RID: 2441
	[SerializeField]
	private float _topObstacleHeight = 1.5f;

	// Token: 0x0400098A RID: 2442
	private float _spawnAheadTime;

	// Token: 0x0400098B RID: 2443
	private float _jumpDuration;

	// Token: 0x0400098C RID: 2444
	private float _startBPM;

	// Token: 0x0400098D RID: 2445
	private float _startNoteJumpMovementSpeed;

	// Token: 0x0400098E RID: 2446
	private float _noteJumpStartBeatOffset;

	// Token: 0x0400098F RID: 2447
	private float _noteJumpMovementSpeed;

	// Token: 0x04000990 RID: 2448
	private float _jumpDistance;

	// Token: 0x04000991 RID: 2449
	private float _moveDistance;

	// Token: 0x04000992 RID: 2450
	private Vector3 _moveStartPos;

	// Token: 0x04000993 RID: 2451
	private Vector3 _moveEndPos;

	// Token: 0x04000994 RID: 2452
	private Vector3 _jumpEndPos;

	// Token: 0x04000995 RID: 2453
	private float _noteLinesCount = 4f;

	// Token: 0x04000996 RID: 2454
	private float _jumpOffsetY;

	// Token: 0x04000997 RID: 2455
	private Vector3 _rightVec;

	// Token: 0x04000998 RID: 2456
	private Vector3 _forwardVec;

	// Token: 0x04000999 RID: 2457
	private Vector3 _centerPos;
}
