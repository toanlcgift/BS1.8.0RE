using System;
using UnityEngine;
using Zenject;

// Token: 0x02000297 RID: 663
public class FlyingGameHUDRotation : MonoBehaviour
{
	// Token: 0x06000B30 RID: 2864 RVA: 0x00008C34 File Offset: 0x00006E34
	protected void Start()
	{
		this._yAngle = this._beatLineManager.midRotation;
		base.transform.eulerAngles = new Vector3(0f, this._yAngle, 0f);
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00033CAC File Offset: 0x00031EAC
	protected void Update()
	{
		float num4;
		if (this._beatLineManager.isMidRotationValid)
		{
			float midRotation = this._beatLineManager.midRotation;
			float targetRotation = this._environmentSpawnRotation.targetRotation;
			float num = Mathf.DeltaAngle(midRotation, targetRotation);
			float num2 = -this._beatLineManager.rotationRange * 0.5f;
			float num3 = this._beatLineManager.rotationRange * 0.5f;
			if (num > num3)
			{
				num3 = num;
			}
			else if (num < num2)
			{
				num2 = num;
			}
			num4 = midRotation + (num2 + num3) * 0.5f;
			num4 = this._yAngle + Mathf.DeltaAngle(this._yAngle, num4);
		}
		else
		{
			num4 = this._environmentSpawnRotation.targetRotation;
			num4 = this._yAngle + Mathf.DeltaAngle(this._yAngle, num4);
		}
		this._yAngle = Mathf.Lerp(this._yAngle, num4, Time.deltaTime * this._smooth);
		base.transform.eulerAngles = new Vector3(0f, this._yAngle, 0f);
	}

	// Token: 0x04000BD3 RID: 3027
	[SerializeField]
	private float _smooth = 8f;

	// Token: 0x04000BD4 RID: 3028
	[Inject]
	private BeatLineManager _beatLineManager;

	// Token: 0x04000BD5 RID: 3029
	[Inject]
	private EnvironmentSpawnRotation _environmentSpawnRotation;

	// Token: 0x04000BD6 RID: 3030
	private float _yAngle;
}
