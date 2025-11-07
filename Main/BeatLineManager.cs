using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x0200022B RID: 555
public class BeatLineManager : MonoBehaviour
{
	// Token: 0x17000260 RID: 608
	// (get) Token: 0x060008AD RID: 2221 RVA: 0x00007014 File Offset: 0x00005214
	public bool isMidRotationValid
	{
		get
		{
			return this._isMidRotationValid;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x060008AE RID: 2222 RVA: 0x0000701C File Offset: 0x0000521C
	public float midRotation
	{
		get
		{
			return this._midRotation;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x060008AF RID: 2223 RVA: 0x00007024 File Offset: 0x00005224
	public float rotationRange
	{
		get
		{
			return this._rotationRange;
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0000702C File Offset: 0x0000522C
	protected void Start()
	{
		this._beatmapObjectManager.noteWasSpawnedEvent += this.HandleNoteWasSpawned;
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00007045 File Offset: 0x00005245
	protected void OnDestroy()
	{
		this._beatmapObjectManager.noteWasSpawnedEvent -= this.HandleNoteWasSpawned;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0002B138 File Offset: 0x00029338
	protected void Update()
	{
		Dictionary<Vector4, BeatLine>.ValueCollection values = this._activeBeatLines.Values;
		this._removeBeatLineKeyList.Clear();
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		int num4 = 0;
		foreach (KeyValuePair<Vector4, BeatLine> keyValuePair in this._activeBeatLines)
		{
			BeatLine value = keyValuePair.Value;
			value.ManualUpdate(this._audioTimeSyncController.songTime);
			if (value.isFinished)
			{
				this._removeBeatLineKeyList.Add(keyValuePair.Key);
			}
			else
			{
				if (num4 == 0)
				{
					num3 = value.rotation;
				}
				else
				{
					float num5 = Mathf.DeltaAngle(num3, value.rotation);
					if (num5 > 0f && num2 < num5)
					{
						num2 = num5;
					}
					else if (num5 < 0f && num > num5)
					{
						num = num5;
					}
				}
				num4++;
			}
		}
		if (num4 > 0)
		{
			this._rotationRange = num2 - num;
			this._midRotation = num3 + (num + num2) * 0.5f;
			this._isMidRotationValid = true;
		}
		else
		{
			this._isMidRotationValid = false;
		}
		foreach (Vector4 key in this._removeBeatLineKeyList)
		{
			this._beatLinePool.Despawn(this._activeBeatLines[key]);
			this._activeBeatLines.Remove(key);
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0002B2C4 File Offset: 0x000294C4
	private void HandleNoteWasSpawned(NoteController noteController)
	{
		Vector4 vector = noteController.beatPos;
		vector.z = -this._linesYPosition;
		vector.w = noteController.worldRotation.eulerAngles.y;
		BeatLine beatLine;
		if (!this._activeBeatLines.TryGetValue(vector, out beatLine))
		{
			beatLine = this._beatLinePool.Spawn();
			beatLine.Init(vector, vector.w);
			this._activeBeatLines[vector] = beatLine;
		}
		beatLine.AddHighlight(noteController.moveStartTime, noteController.moveDuration, noteController.jumpDuration);
	}

	// Token: 0x04000928 RID: 2344
	[SerializeField]
	private float _linesYPosition;

	// Token: 0x04000929 RID: 2345
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x0400092A RID: 2346
	[Inject]
	private BeatLine.Pool _beatLinePool;

	// Token: 0x0400092B RID: 2347
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x0400092C RID: 2348
	private Dictionary<Vector4, BeatLine> _activeBeatLines = new Dictionary<Vector4, BeatLine>(16);

	// Token: 0x0400092D RID: 2349
	private List<Vector4> _removeBeatLineKeyList = new List<Vector4>(8);

	// Token: 0x0400092E RID: 2350
	private bool _isMidRotationValid;

	// Token: 0x0400092F RID: 2351
	private float _midRotation;

	// Token: 0x04000930 RID: 2352
	private float _rotationRange;
}
