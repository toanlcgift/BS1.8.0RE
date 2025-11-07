using System;
using UnityEngine;
using Zenject;

// Token: 0x02000305 RID: 773
public class SaberClashChecker : MonoBehaviour
{
	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0000A4EB File Offset: 0x000086EB
	// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0000A4F3 File Offset: 0x000086F3
	public bool sabersAreClashing { get; private set; }

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0000A4FC File Offset: 0x000086FC
	// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0000A504 File Offset: 0x00008704
	public Vector3 clashingPoint { get; private set; }

	// Token: 0x06000D51 RID: 3409 RVA: 0x0000A50D File Offset: 0x0000870D
	protected void Start()
	{
		this._leftSaber = this._playerController.leftSaber;
		this._rightSaber = this._playerController.rightSaber;
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x000386D4 File Offset: 0x000368D4
	protected void Update()
	{
		Vector3 saberBladeTopPos = this._leftSaber.saberBladeTopPos;
		Vector3 saberBladeTopPos2 = this._rightSaber.saberBladeTopPos;
		Vector3 saberBladeBottomPos = this._leftSaber.saberBladeBottomPos;
		Vector3 saberBladeBottomPos2 = this._rightSaber.saberBladeBottomPos;
		if (saberBladeBottomPos == saberBladeBottomPos2)
		{
			this.sabersAreClashing = false;
			return;
		}
		Vector3 clashingPoint;
		if (this.SegmentToSegmentDist(saberBladeBottomPos, saberBladeTopPos, saberBladeBottomPos2, saberBladeTopPos2, out clashingPoint) < this._minDistanceToClash && this._leftSaber.isActiveAndEnabled && this._rightSaber.isActiveAndEnabled)
		{
			this.clashingPoint = clashingPoint;
			this.sabersAreClashing = true;
			return;
		}
		this.sabersAreClashing = false;
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0000A531 File Offset: 0x00008731
	protected void OnDisable()
	{
		this.sabersAreClashing = false;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00038768 File Offset: 0x00036968
	private float SegmentToSegmentDist(Vector3 fromA, Vector3 toA, Vector3 fromB, Vector3 toB, out Vector3 inbetweenPoint)
	{
		float num = 1E-06f;
		Vector3 vector = toA - fromA;
		Vector3 vector2 = toB - fromB;
		Vector3 rhs = fromA - fromB;
		float num2 = Vector3.Dot(vector, vector);
		float num3 = Vector3.Dot(vector, vector2);
		float num4 = Vector3.Dot(vector2, vector2);
		float num5 = Vector3.Dot(vector, rhs);
		float num6 = Vector3.Dot(vector2, rhs);
		float num8;
		float num7;
		float num9;
		float num10;
		if ((num7 = (num8 = num2 * num4 - num3 * num3)) < num)
		{
			num9 = 0f;
			num8 = 1f;
			num10 = num6;
			num7 = num4;
		}
		else
		{
			num9 = num3 * num6 - num4 * num5;
			num10 = num2 * num6 - num3 * num5;
			if (num9 < 0f)
			{
				num9 = 0f;
				num10 = num6;
				num7 = num4;
			}
			else if (num9 > num8)
			{
				num9 = num8;
				num10 = num6 + num3;
				num7 = num4;
			}
		}
		if (num10 < 0f)
		{
			num10 = 0f;
			if (-num5 < 0f)
			{
				num9 = 0f;
			}
			else if (-num5 > num2)
			{
				num9 = num8;
			}
			else
			{
				num9 = -num5;
				num8 = num2;
			}
		}
		else if (num10 > num7)
		{
			num10 = num7;
			if (-num5 + num3 < 0f)
			{
				num9 = 0f;
			}
			else if (-num5 + num3 > num2)
			{
				num9 = num8;
			}
			else
			{
				num9 = -num5 + num3;
				num8 = num2;
			}
		}
		float d = (Mathf.Abs(num9) < num) ? 0f : (num9 / num8);
		float d2 = (Mathf.Abs(num10) < num) ? 0f : (num10 / num7);
		Vector3 a = fromA + d * vector;
		Vector3 b = fromB + d2 * vector2;
		inbetweenPoint = (a + b) * 0.5f;
		return (a - b).magnitude;
	}

	// Token: 0x04000DBB RID: 3515
	[SerializeField]
	private float _minDistanceToClash = 0.08f;

	// Token: 0x04000DBC RID: 3516
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000DBF RID: 3519
	private Saber _leftSaber;

	// Token: 0x04000DC0 RID: 3520
	private Saber _rightSaber;
}
