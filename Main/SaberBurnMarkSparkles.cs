using System;
using UnityEngine;
using Zenject;

// Token: 0x02000284 RID: 644
public class SaberBurnMarkSparkles : MonoBehaviour
{
	// Token: 0x06000ACF RID: 2767 RVA: 0x00032448 File Offset: 0x00030648
	protected void Start()
	{
		this._sabers = new Saber[2];
		this._sabers[0] = this._playerController.leftSaber;
		this._sabers[1] = this._playerController.rightSaber;
		this._sparklesEmitParams = default(ParticleSystem.EmitParams);
		this._sparklesEmitParams.applyShapeToPosition = true;
		this._prevBurnMarkPos = new Vector3[2];
		this._prevBurnMarkPosValid = new bool[2];
		this._plane = new Plane(base.transform.up, base.transform.position);
		this._burnMarksPS = new ParticleSystem[2];
		this._burnMarksEmmisionModules = new ParticleSystem.EmissionModule[2];
		for (int i = 0; i < 2; i++)
		{
			Quaternion rotation = default(Quaternion);
			rotation.eulerAngles = new Vector3(-90f, 0f, 0f);
			this._burnMarksPS[i] = UnityEngine.Object.Instantiate<ParticleSystem>(this._burnMarksPSPrefab, Vector3.zero, rotation, null);
			this._burnMarksEmmisionModules[i] = this._burnMarksPS[i].emission;
			this._burnMarksPS[i].startColor = this._colorManager.EffectsColorForSaberType(this._sabers[i].saberType);
			this._prevBurnMarkPosValid[i] = false;
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00032594 File Offset: 0x00030794
	protected void OnDestroy()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this._burnMarksPS[i] != null)
			{
				UnityEngine.Object.Destroy(this._burnMarksPS[i].gameObject);
			}
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x000325D0 File Offset: 0x000307D0
	private bool GetBurnMarkPos(Vector3 bladeBottomPos, Vector3 bladeTopPos, out Vector3 burnMarkPos)
	{
		float num = Vector3.Distance(bladeBottomPos, bladeTopPos);
		Vector3 vector = (bladeTopPos - bladeBottomPos) / num;
		float num2;
		if (this._plane.Raycast(new Ray(bladeBottomPos, vector), out num2) && num2 <= num)
		{
			burnMarkPos = bladeBottomPos + vector * num2;
			Bounds bounds = this._boxCollider.bounds;
			return bounds.min.x < burnMarkPos.x && bounds.max.x > burnMarkPos.x && bounds.min.z < burnMarkPos.z && bounds.max.z > burnMarkPos.z;
		}
		burnMarkPos = new Vector3(0f, 0f, 0f);
		return false;
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0003269C File Offset: 0x0003089C
	protected void LateUpdate()
	{
		for (int i = 0; i < 2; i++)
		{
			Vector3 vector = new Vector3(0f, 0f, 0f);
			bool flag = this._sabers[i].isActiveAndEnabled && this.GetBurnMarkPos(this._sabers[i].saberBladeBottomPos, this._sabers[i].saberBladeTopPos, out vector);
			if (flag)
			{
				this._burnMarksPS[i].transform.localPosition = vector;
			}
			if (flag && !this._prevBurnMarkPosValid[i])
			{
				this._burnMarksEmmisionModules[i].enabled = flag;
			}
			else if (!flag && !this._prevBurnMarkPosValid[i])
			{
				this._burnMarksEmmisionModules[i].enabled = false;
				this._burnMarksPS[i].Clear();
			}
			this._sparklesEmitParams.startColor = this._colorManager.ColorForSaberType(this._sabers[i].saberType);
			if (flag && this._prevBurnMarkPosValid[i])
			{
				Vector3 a = vector - this._prevBurnMarkPos[i];
				float magnitude = a.magnitude;
				float num = 0.05f;
				int num2 = (int)(magnitude / num);
				int num3 = (num2 > 0) ? num2 : 1;
				for (int j = 0; j <= num2; j++)
				{
					this._sparklesEmitParams.position = this._prevBurnMarkPos[i] + a * (float)j / (float)num3;
					this._sparklesPS.Emit(this._sparklesEmitParams, 1);
				}
			}
			this._prevBurnMarkPosValid[i] = flag;
			this._prevBurnMarkPos[i] = vector;
		}
	}

	// Token: 0x04000B40 RID: 2880
	[SerializeField]
	private ParticleSystem _sparklesPS;

	// Token: 0x04000B41 RID: 2881
	[SerializeField]
	private ParticleSystem _burnMarksPSPrefab;

	// Token: 0x04000B42 RID: 2882
	[SerializeField]
	private BoxCollider _boxCollider;

	// Token: 0x04000B43 RID: 2883
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000B44 RID: 2884
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000B45 RID: 2885
	private Saber[] _sabers;

	// Token: 0x04000B46 RID: 2886
	private Plane _plane;

	// Token: 0x04000B47 RID: 2887
	private Vector3[] _prevBurnMarkPos;

	// Token: 0x04000B48 RID: 2888
	private bool[] _prevBurnMarkPosValid;

	// Token: 0x04000B49 RID: 2889
	private ParticleSystem[] _burnMarksPS;

	// Token: 0x04000B4A RID: 2890
	private ParticleSystem.EmissionModule[] _burnMarksEmmisionModules;

	// Token: 0x04000B4B RID: 2891
	private ParticleSystem.EmitParams _sparklesEmitParams;
}
