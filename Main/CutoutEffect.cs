using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class CutoutEffect : MonoBehaviour
{
	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06000A6C RID: 2668 RVA: 0x000081D9 File Offset: 0x000063D9
	public bool useRandomCutoutOffset
	{
		get
		{
			return this._useRandomCutoutOffset;
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x000081E6 File Offset: 0x000063E6
	protected void Start()
	{
		this._randomNoiseTexOffset = UnityEngine.Random.onUnitSphere * 10f;
		this.SetCutout(this._cutout);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00031080 File Offset: 0x0002F280
	public void SetCutout(float cutout)
	{
		Vector3 cutoutOffset = (this._useRandomCutoutOffset != null && this._useRandomCutoutOffset.value) ? this._randomNoiseTexOffset : this._cutoutOffset;
		this.SetCutout(cutout, cutoutOffset);
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00008209 File Offset: 0x00006409
	public void SetCutout(float cutout, Vector3 cutoutOffset)
	{
		this._cutout = cutout;
		MaterialPropertyBlock materialPropertyBlock = this._materialPropertyBlockController.materialPropertyBlock;
		materialPropertyBlock.SetVector(CutoutEffect._cutoutTexOffsetPropertyID, cutoutOffset);
		materialPropertyBlock.SetFloat(CutoutEffect._cutoutPropertyID, cutout);
		this._materialPropertyBlockController.ApplyChanges();
	}

	// Token: 0x04000AD2 RID: 2770
	[SerializeField]
	private MaterialPropertyBlockController _materialPropertyBlockController;

	// Token: 0x04000AD3 RID: 2771
	[SerializeField]
	[NullAllowed]
	private BoolSO _useRandomCutoutOffset;

	// Token: 0x04000AD4 RID: 2772
	[SerializeField]
	private Vector3 _cutoutOffset;

	// Token: 0x04000AD5 RID: 2773
	private Vector3 _randomNoiseTexOffset;

	// Token: 0x04000AD6 RID: 2774
	private float _cutout;

	// Token: 0x04000AD7 RID: 2775
	[DoesNotRequireDomainReloadInit]
	private static readonly int _cutoutPropertyID = Shader.PropertyToID("_Cutout");

	// Token: 0x04000AD8 RID: 2776
	[DoesNotRequireDomainReloadInit]
	private static readonly int _cutoutTexOffsetPropertyID = Shader.PropertyToID("_CutoutTexOffset");
}
