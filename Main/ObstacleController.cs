using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x02000254 RID: 596
public class ObstacleController : MonoBehaviour
{
	// Token: 0x1400003C RID: 60
	// (add) Token: 0x060009FE RID: 2558 RVA: 0x0002F400 File Offset: 0x0002D600
	// (remove) Token: 0x060009FF RID: 2559 RVA: 0x0002F438 File Offset: 0x0002D638
	public event Action<ObstacleController> didInitEvent;

	// Token: 0x1400003D RID: 61
	// (add) Token: 0x06000A00 RID: 2560 RVA: 0x0002F470 File Offset: 0x0002D670
	// (remove) Token: 0x06000A01 RID: 2561 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
	public event Action<ObstacleController> finishedMovementEvent;

	// Token: 0x1400003E RID: 62
	// (add) Token: 0x06000A02 RID: 2562 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
	// (remove) Token: 0x06000A03 RID: 2563 RVA: 0x0002F518 File Offset: 0x0002D718
	public event Action<ObstacleController> passedThreeQuartersOfMove2Event;

	// Token: 0x1400003F RID: 63
	// (add) Token: 0x06000A04 RID: 2564 RVA: 0x0002F550 File Offset: 0x0002D750
	// (remove) Token: 0x06000A05 RID: 2565 RVA: 0x0002F588 File Offset: 0x0002D788
	public event Action<ObstacleController> passedAvoidedMarkEvent;

	// Token: 0x14000040 RID: 64
	// (add) Token: 0x06000A06 RID: 2566 RVA: 0x0002F5C0 File Offset: 0x0002D7C0
	// (remove) Token: 0x06000A07 RID: 2567 RVA: 0x0002F5F8 File Offset: 0x0002D7F8
	public event Action<ObstacleController, float> didStartDissolvingEvent;

	// Token: 0x14000041 RID: 65
	// (add) Token: 0x06000A08 RID: 2568 RVA: 0x0002F630 File Offset: 0x0002D830
	// (remove) Token: 0x06000A09 RID: 2569 RVA: 0x0002F668 File Offset: 0x0002D868
	public event Action<ObstacleController> didDissolveEvent;

	// Token: 0x1700029A RID: 666
	// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
	public bool hide
	{
		set
		{
			GameObject[] visualWrappers = this._visualWrappers;
			for (int i = 0; i < visualWrappers.Length; i++)
			{
				visualWrappers[i].SetActive(!value);
			}
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00007CAA File Offset: 0x00005EAA
	public Bounds bounds
	{
		get
		{
			return this._bounds;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00007CB2 File Offset: 0x00005EB2
	public ObstacleData obstacleData
	{
		get
		{
			return this._obstacleData;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00007CBA File Offset: 0x00005EBA
	public bool hasPassedAvoidedMark
	{
		get
		{
			return this._passedAvoidedMarkReported;
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0002F6D0 File Offset: 0x0002D8D0
	public void Init(ObstacleData obstacleData, float worldRotation, Vector3 startPos, Vector3 midPos, Vector3 endPos, float move1Duration, float move2Duration, float singleLineWidth, float height)
	{
		this._worldRotation = Quaternion.Euler(0f, worldRotation, 0f);
		this._inverseWorldRotation = Quaternion.Euler(0f, -worldRotation, 0f);
		this._initialized = true;
		this._obstacleData = obstacleData;
		this._obstacleDuration = obstacleData.duration;
		float num = (float)obstacleData.width * singleLineWidth;
		Vector3 b = new Vector3((num - singleLineWidth) * 0.5f, 0f, 0f);
		this._startPos = startPos + b;
		this._midPos = midPos + b;
		this._endPos = endPos + b;
		this._move1Duration = move1Duration;
		this._move2Duration = move2Duration;
		this._startTimeOffset = obstacleData.time - this._move1Duration - this._move2Duration * 0.5f;
		float length = (this._endPos - this._midPos).magnitude / move2Duration * obstacleData.duration;
		this._stretchableObstacle.SetSizeAndColor(num * 0.98f, height, length, this._color);
		this._bounds = this._stretchableObstacle.bounds;
		this._passedThreeQuartersOfMove2Reported = false;
		this._passedAvoidedMarkReported = false;
		this._passedAvoidedMarkTime = this._move1Duration + this._move2Duration * 0.5f + this._obstacleDuration + 0.15f;
		this._finishMovementTime = this._move1Duration + this._move2Duration + this._obstacleDuration;
		base.transform.localRotation = this._worldRotation;
		Action<ObstacleController> action = this.didInitEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0002F868 File Offset: 0x0002DA68
	protected void Update()
	{
		if (!this._initialized)
		{
			return;
		}
		float num = this._audioTimeSyncController.songTime - this._startTimeOffset;
		Vector3 posForTime = this.GetPosForTime(num);
		base.transform.position = this._worldRotation * posForTime;
		if (!this._passedThreeQuartersOfMove2Reported && num > this._move1Duration + this._move2Duration * 0.75f)
		{
			this._passedThreeQuartersOfMove2Reported = true;
			Action<ObstacleController> action = this.passedThreeQuartersOfMove2Event;
			if (action != null)
			{
				action(this);
			}
		}
		if (!this._passedAvoidedMarkReported && num > this._passedAvoidedMarkTime)
		{
			this._passedAvoidedMarkReported = true;
			Action<ObstacleController> action2 = this.passedAvoidedMarkEvent;
			if (action2 != null)
			{
				action2(this);
			}
		}
		if (num > this._finishMovementTime)
		{
			Action<ObstacleController> action3 = this.finishedMovementEvent;
			if (action3 == null)
			{
				return;
			}
			action3(this);
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0002F92C File Offset: 0x0002DB2C
	private Vector3 GetPosForTime(float time)
	{
		Vector3 result;
		if (time < this._move1Duration)
		{
			result = Vector3.LerpUnclamped(this._startPos, this._midPos, time / this._move1Duration);
		}
		else
		{
			float t = (time - this._move1Duration) / this._move2Duration;
			result.x = this._startPos.x;
			result.y = this._startPos.y;
			result.z = this._playerController.MoveTowardsHead(this._midPos.z, this._endPos.z, this._inverseWorldRotation, t);
			if (this._passedAvoidedMarkReported)
			{
				float num = (time - this._passedAvoidedMarkTime) / (this._finishMovementTime - this._passedAvoidedMarkTime);
				num = num * num * num;
				result.z -= Mathf.LerpUnclamped(0f, this._endDistanceOffest, num);
			}
		}
		return result;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x00007CC2 File Offset: 0x00005EC2
	private IEnumerator DissolveCoroutine(float duration)
	{
		Action<ObstacleController, float> action = this.didStartDissolvingEvent;
		if (action != null)
		{
			action(this, duration);
		}
		yield return new WaitForSeconds(duration);
		this._dissolving = false;
		Action<ObstacleController> action2 = this.didDissolveEvent;
		if (action2 != null)
		{
			action2(this);
		}
		yield break;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00007CD8 File Offset: 0x00005ED8
	public void Dissolve(float duration)
	{
		if (this._dissolving)
		{
			return;
		}
		this._dissolving = true;
		base.StartCoroutine(this.DissolveCoroutine(duration));
	}

	// Token: 0x04000A31 RID: 2609
	[SerializeField]
	private StretchableObstacle _stretchableObstacle;

	// Token: 0x04000A32 RID: 2610
	[SerializeField]
	private SimpleColorSO _color;

	// Token: 0x04000A33 RID: 2611
	[Space]
	[SerializeField]
	private float _endDistanceOffest = 500f;

	// Token: 0x04000A34 RID: 2612
	[SerializeField]
	private GameObject[] _visualWrappers;

	// Token: 0x04000A35 RID: 2613
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000A36 RID: 2614
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000A3D RID: 2621
	public const float kAvoidMarkTimeOffset = 0.15f;

	// Token: 0x04000A3E RID: 2622
	private Vector3 _startPos;

	// Token: 0x04000A3F RID: 2623
	private Vector3 _midPos;

	// Token: 0x04000A40 RID: 2624
	private Vector3 _endPos;

	// Token: 0x04000A41 RID: 2625
	private float _move1Duration;

	// Token: 0x04000A42 RID: 2626
	private float _move2Duration;

	// Token: 0x04000A43 RID: 2627
	private float _startTimeOffset;

	// Token: 0x04000A44 RID: 2628
	private float _obstacleDuration;

	// Token: 0x04000A45 RID: 2629
	private bool _passedThreeQuartersOfMove2Reported;

	// Token: 0x04000A46 RID: 2630
	private bool _passedAvoidedMarkReported;

	// Token: 0x04000A47 RID: 2631
	private float _passedAvoidedMarkTime;

	// Token: 0x04000A48 RID: 2632
	private float _finishMovementTime;

	// Token: 0x04000A49 RID: 2633
	private bool _initialized;

	// Token: 0x04000A4A RID: 2634
	private Bounds _bounds;

	// Token: 0x04000A4B RID: 2635
	private bool _dissolving;

	// Token: 0x04000A4C RID: 2636
	private ObstacleData _obstacleData;

	// Token: 0x04000A4D RID: 2637
	private Quaternion _worldRotation;

	// Token: 0x04000A4E RID: 2638
	private Quaternion _inverseWorldRotation;

	// Token: 0x02000255 RID: 597
	public class Pool : MemoryPoolWithActiveItems<ObstacleController>
	{
	}
}
