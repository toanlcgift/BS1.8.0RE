using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000259 RID: 601
public class StretchableObstacle : MonoBehaviour
{
	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00007E16 File Offset: 0x00006016
	public Bounds bounds
	{
		get
		{
			return this._bounds;
		}
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
	public void SetSizeAndColor(float width, float height, float length, Color color)
	{
		this._bounds = new Bounds(new Vector3(0f, height * 0.5f, length * 0.5f), new Vector3(width, height, length));
		Vector3 vector = new Vector3(width - this._coreOffset, height - this._coreOffset, length - this._coreOffset);
		this._obstacleCore.localScale = vector;
		this._obstacleCore.localPosition = new Vector3(0f, height * 0.5f, length * 0.5f);
		this._obstacleFrame.width = width;
		this._obstacleFrame.height = height;
		this._obstacleFrame.length = length;
		this._obstacleFrame.edgeSize = this._edgeSize;
		this._obstacleFrame.localPosition = new Vector3(0f, height * 0.5f, length * 0.5f);
		this._obstacleFrame.color = color;
		this._obstacleFrame.Refresh();
		this._obstacleFakeGlow.width = width;
		this._obstacleFakeGlow.height = height;
		this._obstacleFakeGlow.length = length;
		this._obstacleFakeGlow.edgeSize = this._edgeSize * 1.5f;
		this._obstacleFakeGlow.localPosition = new Vector3(0f, height * 0.5f, length * 0.5f);
		this._obstacleFakeGlow.color = color;
		this._obstacleFakeGlow.Refresh();
		Color value = color * this._addColorMultiplier;
		value.a = 0f;
		foreach (MaterialPropertyBlockController materialPropertyBlockController in this._materialPropertyBlockControllers)
		{
			materialPropertyBlockController.materialPropertyBlock.SetVector(StretchableObstacle._uvScaleID, vector);
			materialPropertyBlockController.materialPropertyBlock.SetColor(StretchableObstacle._addColorID, value);
			materialPropertyBlockController.materialPropertyBlock.SetColor(StretchableObstacle._tintColorID, Color.Lerp(color, Color.white, this._obstacleCoreLerpToWhiteFactor));
			materialPropertyBlockController.ApplyChanges();
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00007E1E File Offset: 0x0000601E
	protected void OnValidate()
	{
		Scene scene = base.gameObject.scene;
		this.SetSizeAndColor(1f, 1f, 1f, Color.white);
	}

	// Token: 0x04000A6B RID: 2667
	[SerializeField]
	private float _edgeSize = 0.05f;

	// Token: 0x04000A6C RID: 2668
	[SerializeField]
	private float _coreOffset = 0.01f;

	// Token: 0x04000A6D RID: 2669
	[SerializeField]
	private float _addColorMultiplier = 0.1f;

	// Token: 0x04000A6E RID: 2670
	[SerializeField]
	private float _obstacleCoreLerpToWhiteFactor = 0.75f;

	// Token: 0x04000A6F RID: 2671
	[Space]
	[SerializeField]
	private Transform _obstacleCore;

	// Token: 0x04000A70 RID: 2672
	[SerializeField]
	private MaterialPropertyBlockController[] _materialPropertyBlockControllers;

	// Token: 0x04000A71 RID: 2673
	[SerializeField]
	private ParametricBoxFrameController _obstacleFrame;

	// Token: 0x04000A72 RID: 2674
	[SerializeField]
	private ParametricBoxFakeGlowController _obstacleFakeGlow;

	// Token: 0x04000A73 RID: 2675
	[DoesNotRequireDomainReloadInit]
	private static readonly int _uvScaleID = Shader.PropertyToID("_UVScale");

	// Token: 0x04000A74 RID: 2676
	[DoesNotRequireDomainReloadInit]
	private static readonly int _tintColorID = Shader.PropertyToID("_TintColor");

	// Token: 0x04000A75 RID: 2677
	[DoesNotRequireDomainReloadInit]
	private static readonly int _addColorID = Shader.PropertyToID("_AddColor");

	// Token: 0x04000A76 RID: 2678
	private Bounds _bounds;
}
