using System;
using UnityEngine;
using Zenject;

// Token: 0x02000304 RID: 772
public class SaberActivityCounter : MonoBehaviour
{
	// Token: 0x14000068 RID: 104
	// (add) Token: 0x06000D41 RID: 3393 RVA: 0x00038444 File Offset: 0x00036644
	// (remove) Token: 0x06000D42 RID: 3394 RVA: 0x0003847C File Offset: 0x0003667C
	public event Action<float> totalDistanceDidChangeEvent;

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0000A44E File Offset: 0x0000864E
	public float leftSaberMovementDistance
	{
		get
		{
			return this._leftSaberMovementDistance;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0000A456 File Offset: 0x00008656
	public float rightSaberMovementDistance
	{
		get
		{
			return this._rightSaberMovementDistance;
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0000A45E File Offset: 0x0000865E
	public float leftHandMovementDistance
	{
		get
		{
			return this._leftHandMovementDistance;
		}
	}

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0000A466 File Offset: 0x00008666
	public float rightHandMovementDistance
	{
		get
		{
			return this._rightHandMovementDistance;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0000A46E File Offset: 0x0000866E
	public AveragingValueRecorder saberMovementAveragingValueRecorder
	{
		get
		{
			return this._saberMovementHistoryRecorder.averagingValueRecorer;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0000A47B File Offset: 0x0000867B
	public AveragingValueRecorder handMovementAveragingValueRecorder
	{
		get
		{
			return this._handMovementHistoryRecorder.averagingValueRecorer;
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x000384B4 File Offset: 0x000366B4
	protected void Awake()
	{
		this._saberMovementHistoryRecorder = new MovementHistoryRecorder(this._averageWindowDuration, this._valuesPerSecond, this._increaseSpeed, this._deceraseSpeed);
		this._handMovementHistoryRecorder = new MovementHistoryRecorder(this._averageWindowDuration, this._valuesPerSecond, this._increaseSpeed, this._deceraseSpeed);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0000A488 File Offset: 0x00008688
	protected void Start()
	{
		this._leftSaber = this._playerController.leftSaber;
		this._rightSaber = this._playerController.rightSaber;
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00038508 File Offset: 0x00036708
	protected void Update()
	{
		if (Time.timeSinceLevelLoad < 1f)
		{
			return;
		}
		if (!this._hasPrevPos)
		{
			this._prevLeftSaberTipPos = this._leftSaber.saberBladeTopPos;
			this._prevRightSaberTipPos = this._rightSaber.saberBladeTopPos;
			this._prevLeftHandPos = this._leftSaber.handlePos;
			this._prevRightHandPos = this._rightSaber.handlePos;
			this._hasPrevPos = true;
		}
		float num = this._leftHandMovementDistance + this._rightHandMovementDistance;
		Vector3 vector = this._leftSaber.saberBladeTopPos;
		float num2 = Vector3.Distance(vector, this._prevLeftSaberTipPos);
		if (num2 > this._movementSensitivityThreshold)
		{
			this._leftSaberMovementDistance += num2;
			this._prevLeftSaberTipPos = vector;
			this._saberMovementHistoryRecorder.AddMovement(num2);
		}
		vector = this._rightSaber.saberBladeTopPos;
		num2 = Vector3.Distance(vector, this._prevRightSaberTipPos);
		if (num2 > this._movementSensitivityThreshold)
		{
			this._rightSaberMovementDistance += num2;
			this._prevRightSaberTipPos = vector;
			this._saberMovementHistoryRecorder.AddMovement(num2);
		}
		this._saberMovementHistoryRecorder.ManualUpdate(Time.deltaTime);
		vector = this._leftSaber.handlePos;
		num2 = Vector3.Distance(vector, this._prevLeftHandPos);
		if (num2 > this._movementSensitivityThreshold)
		{
			this._leftHandMovementDistance += num2;
			this._prevLeftHandPos = vector;
			this._handMovementHistoryRecorder.AddMovement(num2);
		}
		vector = this._rightSaber.handlePos;
		num2 = Vector3.Distance(vector, this._prevRightHandPos);
		if (num2 > this._movementSensitivityThreshold)
		{
			this._rightHandMovementDistance += num2;
			this._prevRightHandPos = vector;
			this._handMovementHistoryRecorder.AddMovement(num2);
		}
		this._handMovementHistoryRecorder.ManualUpdate(Time.deltaTime);
		float num3 = this._leftHandMovementDistance + this._rightHandMovementDistance;
		if (num3 != num)
		{
			Action<float> action = this.totalDistanceDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action(num3);
		}
	}

	// Token: 0x04000DA7 RID: 3495
	[SerializeField]
	private float _averageWindowDuration = 0.5f;

	// Token: 0x04000DA8 RID: 3496
	[SerializeField]
	private float _valuesPerSecond = 2f;

	// Token: 0x04000DA9 RID: 3497
	[SerializeField]
	private float _increaseSpeed = 100f;

	// Token: 0x04000DAA RID: 3498
	[SerializeField]
	private float _deceraseSpeed = 20f;

	// Token: 0x04000DAB RID: 3499
	[SerializeField]
	private float _movementSensitivityThreshold = 0.05f;

	// Token: 0x04000DAC RID: 3500
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000DAE RID: 3502
	private Saber _leftSaber;

	// Token: 0x04000DAF RID: 3503
	private Saber _rightSaber;

	// Token: 0x04000DB0 RID: 3504
	private Vector3 _prevLeftSaberTipPos;

	// Token: 0x04000DB1 RID: 3505
	private Vector3 _prevRightSaberTipPos;

	// Token: 0x04000DB2 RID: 3506
	private Vector3 _prevLeftHandPos;

	// Token: 0x04000DB3 RID: 3507
	private Vector3 _prevRightHandPos;

	// Token: 0x04000DB4 RID: 3508
	private bool _hasPrevPos;

	// Token: 0x04000DB5 RID: 3509
	private float _leftSaberMovementDistance;

	// Token: 0x04000DB6 RID: 3510
	private float _rightSaberMovementDistance;

	// Token: 0x04000DB7 RID: 3511
	private float _leftHandMovementDistance;

	// Token: 0x04000DB8 RID: 3512
	private float _rightHandMovementDistance;

	// Token: 0x04000DB9 RID: 3513
	private MovementHistoryRecorder _saberMovementHistoryRecorder;

	// Token: 0x04000DBA RID: 3514
	private MovementHistoryRecorder _handMovementHistoryRecorder;
}
