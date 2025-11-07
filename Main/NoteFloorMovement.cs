using System;
using UnityEngine;
using Zenject;

// Token: 0x0200024C RID: 588
public class NoteFloorMovement : MonoBehaviour
{
	// Token: 0x14000030 RID: 48
	// (add) Token: 0x060009AD RID: 2477 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
	// (remove) Token: 0x060009AE RID: 2478 RVA: 0x0002E210 File Offset: 0x0002C410
	public event Action floorMovementDidFinishEvent;

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x060009AF RID: 2479 RVA: 0x000079BD File Offset: 0x00005BBD
	public float distanceToPlayer
	{
		get
		{
			return Mathf.Abs(this._localPosition.z - (this._inverseWorldRotation * this._playerController.headPos).z);
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x060009B0 RID: 2480 RVA: 0x000079EB File Offset: 0x00005BEB
	public Vector3 startPos
	{
		get
		{
			return this._startPos;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000079F3 File Offset: 0x00005BF3
	public Vector3 endPos
	{
		get
		{
			return this._endPos;
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000079FB File Offset: 0x00005BFB
	public float startTime
	{
		get
		{
			return this._startTime;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00007A03 File Offset: 0x00005C03
	public float moveDuration
	{
		get
		{
			return this._moveDuration;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00007A0B File Offset: 0x00005C0B
	public Quaternion worldRotation
	{
		get
		{
			return this._worldRotation;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00007A13 File Offset: 0x00005C13
	public Quaternion inverseWorldRotation
	{
		get
		{
			return this._inverseWorldRotation;
		}
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0002E248 File Offset: 0x0002C448
	public void Init(float worldRotation, Vector3 startPos, Vector3 endPos, float moveDuration, float startTime)
	{
		this._worldRotation = Quaternion.Euler(0f, worldRotation, 0f);
		this._inverseWorldRotation = Quaternion.Euler(0f, -worldRotation, 0f);
		this._startPos = startPos;
		this._endPos = endPos;
		this._moveDuration = moveDuration;
		this._startTime = startTime;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
	public Vector3 SetToStart()
	{
		this._localPosition = this._startPos;
		Vector3 vector = this._worldRotation * this._localPosition;
		base.transform.SetPositionAndRotation(vector, this._worldRotation);
		this._rotatedObject.transform.localRotation = Quaternion.identity;
		return vector;
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
	public Vector3 ManualUpdate()
	{
		float num = this._audioTimeSyncController.songTime - this._startTime;
		this._localPosition = Vector3.Lerp(this._startPos, this._endPos, num / this._moveDuration);
		Vector3 vector = this._worldRotation * this._localPosition;
		base.transform.position = vector;
		if (num >= this._moveDuration)
		{
			Action action = this.floorMovementDidFinishEvent;
			if (action != null)
			{
				action();
			}
		}
		return vector;
	}

	// Token: 0x040009E4 RID: 2532
	[SerializeField]
	private Transform _rotatedObject;

	// Token: 0x040009E5 RID: 2533
	[Inject]
	private PlayerController _playerController;

	// Token: 0x040009E6 RID: 2534
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x040009E8 RID: 2536
	private Vector3 _startPos;

	// Token: 0x040009E9 RID: 2537
	private Vector3 _endPos;

	// Token: 0x040009EA RID: 2538
	private float _moveDuration;

	// Token: 0x040009EB RID: 2539
	private float _startTime;

	// Token: 0x040009EC RID: 2540
	private Quaternion _worldRotation;

	// Token: 0x040009ED RID: 2541
	private Quaternion _inverseWorldRotation;

	// Token: 0x040009EE RID: 2542
	private Vector3 _localPosition;
}
