using System;
using UnityEngine;
using Zenject;

// Token: 0x0200024D RID: 589
public class NoteJump : MonoBehaviour
{
	// Token: 0x14000031 RID: 49
	// (add) Token: 0x060009BA RID: 2490 RVA: 0x0002E36C File Offset: 0x0002C56C
	// (remove) Token: 0x060009BB RID: 2491 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
	public event Action noteJumpDidFinishEvent;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x060009BC RID: 2492 RVA: 0x0002E3DC File Offset: 0x0002C5DC
	// (remove) Token: 0x060009BD RID: 2493 RVA: 0x0002E414 File Offset: 0x0002C614
	public event Action noteJumpDidPassMissedMarkerEvent;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x060009BE RID: 2494 RVA: 0x0002E44C File Offset: 0x0002C64C
	// (remove) Token: 0x060009BF RID: 2495 RVA: 0x0002E484 File Offset: 0x0002C684
	public event Action<NoteJump> noteJumpDidPassThreeQuartersEvent;

	// Token: 0x14000034 RID: 52
	// (add) Token: 0x060009C0 RID: 2496 RVA: 0x0002E4BC File Offset: 0x0002C6BC
	// (remove) Token: 0x060009C1 RID: 2497 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
	public event Action noteJumpDidPassHalfEvent;

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00007A1B File Offset: 0x00005C1B
	public float distanceToPlayer
	{
		get
		{
			return Mathf.Abs(this._localPosition.z - (this._inverseWorldRotation * this._playerController.headPos).z);
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00007A49 File Offset: 0x00005C49
	public Vector3 beatPos
	{
		get
		{
			return (this._endPos + this._startPos) * 0.5f;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00007A66 File Offset: 0x00005C66
	public float jumpDuration
	{
		get
		{
			return this._jumpDuration;
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00007A6E File Offset: 0x00005C6E
	public Vector3 moveVec
	{
		get
		{
			return this._moveVec;
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0002E52C File Offset: 0x0002C72C
	public void Init(float beatTime, float worldRotation, Vector3 startPos, Vector3 endPos, float jumpDuration, float gravity, float flipYSide, NoteCutDirection cutDirection, float cutDirectionAngleOffset)
	{
		this._worldRotation = Quaternion.Euler(0f, worldRotation, 0f);
		this._inverseWorldRotation = Quaternion.Euler(0f, -worldRotation, 0f);
		this._startPos = startPos;
		this._endPos = endPos;
		this._jumpDuration = jumpDuration;
		this._moveVec = (this._endPos - this._startPos) / this._jumpDuration;
		this._beatTime = beatTime;
		this._gravity = gravity;
		if (flipYSide > 0f)
		{
			this._yAvoidance = flipYSide * this._yAvoidanceUp;
		}
		else
		{
			this._yAvoidance = flipYSide * this._yAvoidanceDown;
		}
		this._missedMarkReported = false;
		this._threeQuartersMarkReported = false;
		this._startVerticalVelocity = this._gravity * this._jumpDuration * 0.5f;
		this._endRotation = cutDirection.Rotation() * Quaternion.Euler(0f, 0f, cutDirectionAngleOffset);
		this._missedTime = beatTime + 0.15f;
		Vector3 vector = this._endRotation.eulerAngles;
		this._randomRotationIdx = (this._randomRotationIdx + Mathf.RoundToInt(Mathf.Abs(startPos.x) * 10f) + 1) % this._randomRotations.Length;
		vector += this._randomRotations[this._randomRotationIdx] * 20f;
		this._middleRotation = default(Quaternion);
		this._middleRotation.eulerAngles = vector;
		this._startRotation = Quaternion.identity;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00007A76 File Offset: 0x00005C76
	private float EaseInOutQuad(float t)
	{
		if (t >= 0.5f)
		{
			return -1f + (4f - 2f * t) * t;
		}
		return 2f * t * t;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
	public Vector3 ManualUpdate()
	{
		float songTime = this._audioTimeSyncController.songTime;
		float num = songTime - (this._beatTime - this._jumpDuration * 0.5f);
		float num2 = num / this._jumpDuration;
		if (this._startPos.x == this._endPos.x)
		{
			this._localPosition.x = this._startPos.x;
		}
		else if (num2 < 0.25f)
		{
			this._localPosition.x = this._startPos.x + (this._endPos.x - this._startPos.x) * this.EaseInOutQuad(num2 * 4f);
		}
		else
		{
			this._localPosition.x = this._endPos.x;
		}
		this._localPosition.z = this._playerController.MoveTowardsHead(this._startPos.z, this._endPos.z, this._inverseWorldRotation, num2);
		this._localPosition.y = this._startPos.y + this._startVerticalVelocity * num - this._gravity * num * num * 0.5f;
		if (this._yAvoidance != 0f && num2 < 0.25f)
		{
			float num3 = 0.5f - Mathf.Cos(num2 * 8f * 3.1415927f) * 0.5f;
			this._localPosition.y = this._localPosition.y + num3 * this._yAvoidance;
		}
		if (num2 < 0.5f)
		{
			Quaternion a;
			if (num2 < 0.125f)
			{
				a = Quaternion.Lerp(this._startRotation, this._middleRotation, Mathf.Sin(num2 * 3.1415927f * 4f));
			}
			else
			{
				a = Quaternion.Lerp(this._middleRotation, this._endRotation, Mathf.Sin((num2 - 0.125f) * 3.1415927f * 2f));
			}
			Vector3 vector = this._playerController.headPos;
			vector.y = Mathf.Lerp(vector.y, this._localPosition.y, 0.8f);
			vector = this._inverseWorldRotation * vector;
			Vector3 normalized = (this._localPosition - vector).normalized;
			Quaternion b = default(Quaternion);
			b.SetLookRotation(normalized, this._inverseWorldRotation * this._rotatedObject.up);
			this._rotatedObject.localRotation = Quaternion.Lerp(a, b, num2 * 2f);
		}
		if (num2 >= 0.5f && !this._halfJumpMarkReported)
		{
			this._halfJumpMarkReported = true;
			Action action = this.noteJumpDidPassHalfEvent;
			if (action != null)
			{
				action();
			}
		}
		if (num2 >= 0.75f && !this._threeQuartersMarkReported)
		{
			this._threeQuartersMarkReported = true;
			Action<NoteJump> action2 = this.noteJumpDidPassThreeQuartersEvent;
			if (action2 != null)
			{
				action2(this);
			}
		}
		if (songTime >= this._missedTime && !this._missedMarkReported)
		{
			this._missedMarkReported = true;
			Action action3 = this.noteJumpDidPassMissedMarkerEvent;
			if (action3 != null)
			{
				action3();
			}
		}
		if (this._threeQuartersMarkReported)
		{
			float num4 = (num2 - 0.75f) / 0.25f;
			num4 = num4 * num4 * num4;
			this._localPosition.z = this._localPosition.z - Mathf.LerpUnclamped(0f, this._endDistanceOffest, num4);
		}
		if (num2 >= 1f)
		{
			Action action4 = this.noteJumpDidFinishEvent;
			if (action4 != null)
			{
				action4();
			}
		}
		Vector3 result = this._worldRotation * this._localPosition;
		base.transform.position = this._worldRotation * this._localPosition;
		return result;
	}

	// Token: 0x040009EF RID: 2543
	[SerializeField]
	private Transform _rotatedObject;

	// Token: 0x040009F0 RID: 2544
	[Space]
	[SerializeField]
	private float _yAvoidanceUp = 0.45f;

	// Token: 0x040009F1 RID: 2545
	[SerializeField]
	private float _yAvoidanceDown = 0.15f;

	// Token: 0x040009F2 RID: 2546
	[Space]
	[SerializeField]
	private float _endDistanceOffest = 500f;

	// Token: 0x040009F3 RID: 2547
	[Inject]
	private PlayerController _playerController;

	// Token: 0x040009F4 RID: 2548
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x040009F9 RID: 2553
	private Vector3 _startPos;

	// Token: 0x040009FA RID: 2554
	private Vector3 _endPos;

	// Token: 0x040009FB RID: 2555
	private float _jumpDuration;

	// Token: 0x040009FC RID: 2556
	private Vector3 _moveVec;

	// Token: 0x040009FD RID: 2557
	private float _beatTime;

	// Token: 0x040009FE RID: 2558
	private float _startVerticalVelocity;

	// Token: 0x040009FF RID: 2559
	private Quaternion _startRotation;

	// Token: 0x04000A00 RID: 2560
	private Quaternion _middleRotation;

	// Token: 0x04000A01 RID: 2561
	private Quaternion _endRotation;

	// Token: 0x04000A02 RID: 2562
	private float _gravity;

	// Token: 0x04000A03 RID: 2563
	private float _yAvoidance;

	// Token: 0x04000A04 RID: 2564
	private float _missedTime;

	// Token: 0x04000A05 RID: 2565
	private bool _missedMarkReported;

	// Token: 0x04000A06 RID: 2566
	private bool _threeQuartersMarkReported;

	// Token: 0x04000A07 RID: 2567
	private bool _halfJumpMarkReported;

	// Token: 0x04000A08 RID: 2568
	private Vector3 _localPosition;

	// Token: 0x04000A09 RID: 2569
	private readonly Vector3[] _randomRotations = new Vector3[]
	{
		new Vector3(-0.9543871f, -0.1183784f, 0.2741019f),
		new Vector3(0.7680854f, -0.08805521f, 0.6342642f),
		new Vector3(-0.6780157f, 0.306681f, -0.6680131f),
		new Vector3(0.1255014f, 0.9398643f, 0.3176546f),
		new Vector3(0.365105f, -0.3664974f, -0.8557909f),
		new Vector3(-0.8790653f, -0.06244748f, -0.4725934f),
		new Vector3(0.01886305f, -0.8065798f, 0.5908241f),
		new Vector3(-0.1455435f, 0.8901445f, 0.4318099f),
		new Vector3(0.07651193f, 0.9474725f, -0.3105508f),
		new Vector3(0.1306983f, -0.2508438f, -0.9591639f)
	};

	// Token: 0x04000A0A RID: 2570
	private int _randomRotationIdx;

	// Token: 0x04000A0B RID: 2571
	public const float kMissedTimeOffset = 0.15f;

	// Token: 0x04000A0C RID: 2572
	private Quaternion _worldRotation;

	// Token: 0x04000A0D RID: 2573
	private Quaternion _inverseWorldRotation;
}
