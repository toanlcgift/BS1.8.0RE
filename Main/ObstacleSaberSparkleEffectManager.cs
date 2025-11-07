using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x02000265 RID: 613
public class ObstacleSaberSparkleEffectManager : MonoBehaviour
{
	// Token: 0x14000042 RID: 66
	// (add) Token: 0x06000A4A RID: 2634 RVA: 0x000308DC File Offset: 0x0002EADC
	// (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00030914 File Offset: 0x0002EB14
	public event Action<SaberType> sparkleEffectDidStartEvent;

	// Token: 0x14000043 RID: 67
	// (add) Token: 0x06000A4C RID: 2636 RVA: 0x0003094C File Offset: 0x0002EB4C
	// (remove) Token: 0x06000A4D RID: 2637 RVA: 0x00030984 File Offset: 0x0002EB84
	public event Action<SaberType> sparkleEffectDidEndEvent;

	// Token: 0x06000A4E RID: 2638 RVA: 0x000309BC File Offset: 0x0002EBBC
	protected void Start()
	{
		this._sabers = new Saber[2];
		this._sabers[0] = this._playerController.leftSaber;
		this._sabers[1] = this._playerController.rightSaber;
		this._effects = new ObstacleSaberSparkleEffect[2];
		this._effectsTransforms = new Transform[2];
		for (int i = 0; i < 2; i++)
		{
			this._effects[i] = UnityEngine.Object.Instantiate<ObstacleSaberSparkleEffect>(this._obstacleSaberSparkleEffectPefab);
			this._effects[i].color = this._colorManager.GetObstacleEffectColor();
			this._effectsTransforms[i] = this._effects[i].transform;
		}
		this._burnMarkPositions = new Vector3[2];
		this._isSystemActive = new bool[2];
		this._wasSystemActive = new bool[2];
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00030A84 File Offset: 0x0002EC84
	protected void OnDisable()
	{
		if (this._hapticFeedbackController != null)
		{
			for (int i = 0; i < 2; i++)
			{
				if (this._isSystemActive[i])
				{
					this._isSystemActive[i] = false;
				}
			}
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00030AC0 File Offset: 0x0002ECC0
	protected void Update()
	{
		this._wasSystemActive[0] = this._isSystemActive[0];
		this._wasSystemActive[1] = this._isSystemActive[1];
		this._isSystemActive[0] = false;
		this._isSystemActive[1] = false;
		foreach (ObstacleController obstacleController in this._obstaclePool.activeItems)
		{
			Bounds bounds = obstacleController.bounds;
			for (int i = 0; i < 2; i++)
			{
				Vector3 vector;
				if (this._sabers[i].isActiveAndEnabled && this.GetBurnMarkPos(bounds, obstacleController.transform, this._sabers[i].saberBladeBottomPos, this._sabers[i].saberBladeTopPos, out vector))
				{
					this._isSystemActive[i] = true;
					this._burnMarkPositions[i] = vector;
					this._effects[i].SetPositionAndRotation(vector, this.GetEffectRotation(vector, obstacleController.transform, bounds));
					XRNode node = (i == 0) ? XRNode.LeftHand : XRNode.RightHand;
					this._hapticFeedbackController.ContinuousRumble(node);
					if (!this._wasSystemActive[i])
					{
						this._effects[i].StartEmission();
						Action<SaberType> action = this.sparkleEffectDidStartEvent;
						if (action != null)
						{
							action(this._sabers[i].saberType);
						}
					}
				}
			}
		}
		for (int j = 0; j < 2; j++)
		{
			if (!this._isSystemActive[j] && this._wasSystemActive[j])
			{
				this._effects[j].StopEmission();
				Action<SaberType> action2 = this.sparkleEffectDidEndEvent;
				if (action2 != null)
				{
					action2(this._sabers[j].saberType);
				}
			}
		}
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00030C80 File Offset: 0x0002EE80
	private Quaternion GetEffectRotation(Vector3 pos, Transform transform, Bounds bounds)
	{
		pos = transform.InverseTransformPoint(pos);
		Vector3 direction;
		if (pos.x >= bounds.max.x - 0.01f)
		{
			direction = new Vector3(0f, 90f, 0f);
		}
		else if (pos.x <= bounds.min.x + 0.01f)
		{
			direction = new Vector3(0f, -90f, 0f);
		}
		else if (pos.y >= bounds.max.y - 0.01f)
		{
			direction = new Vector3(-90f, 0f, 0f);
		}
		else if (pos.y <= bounds.min.y + 0.01f)
		{
			direction = new Vector3(90f, 0f, 0f);
		}
		else
		{
			direction = new Vector3(180f, 0f, 0f);
		}
		return Quaternion.Euler(transform.TransformDirection(direction));
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00008086 File Offset: 0x00006286
	public Vector3 BurnMarkPosForSaberType(SaberType saberType)
	{
		if (saberType == this._sabers[0].saberType)
		{
			return this._burnMarkPositions[0];
		}
		return this._burnMarkPositions[1];
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00030D84 File Offset: 0x0002EF84
	private bool GetBurnMarkPos(Bounds bounds, Transform transform, Vector3 bladeBottomPos, Vector3 bladeTopPos, out Vector3 burnMarkPos)
	{
		bladeBottomPos = transform.InverseTransformPoint(bladeBottomPos);
		bladeTopPos = transform.InverseTransformPoint(bladeTopPos);
		float num = Vector3.Distance(bladeBottomPos, bladeTopPos);
		Vector3 vector = bladeTopPos - bladeBottomPos;
		vector.Normalize();
		float num2;
		if (bounds.IntersectRay(new Ray(bladeBottomPos, vector), out num2) && num2 <= num)
		{
			burnMarkPos = transform.TransformPoint(bladeBottomPos + vector * num2);
			return true;
		}
		burnMarkPos = Vector3.zero;
		return false;
	}

	// Token: 0x04000AAA RID: 2730
	[SerializeField]
	private ObstacleSaberSparkleEffect _obstacleSaberSparkleEffectPefab;

	// Token: 0x04000AAB RID: 2731
	[Inject]
	private ObstacleController.Pool _obstaclePool;

	// Token: 0x04000AAC RID: 2732
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000AAD RID: 2733
	[Inject]
	private HapticFeedbackController _hapticFeedbackController;

	// Token: 0x04000AAE RID: 2734
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000AB1 RID: 2737
	private Saber[] _sabers;

	// Token: 0x04000AB2 RID: 2738
	private ObstacleSaberSparkleEffect[] _effects;

	// Token: 0x04000AB3 RID: 2739
	private Transform[] _effectsTransforms;

	// Token: 0x04000AB4 RID: 2740
	private bool[] _isSystemActive;

	// Token: 0x04000AB5 RID: 2741
	private bool[] _wasSystemActive;

	// Token: 0x04000AB6 RID: 2742
	private Vector3[] _burnMarkPositions;
}
