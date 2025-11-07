using System;
using UnityEngine;
using Zenject;

// Token: 0x02000283 RID: 643
[RequireComponent(typeof(Renderer))]
public class SaberBurnMarkArea : MonoBehaviour
{
	// Token: 0x06000AC9 RID: 2761 RVA: 0x00031CC8 File Offset: 0x0002FEC8
	protected void Start()
	{
		this._sabers = new Saber[2];
		this._sabers[0] = this._playerController.leftSaber;
		this._sabers[1] = this._playerController.rightSaber;
		this._emitParams = default(ParticleSystem.EmitParams);
		this._emitParams.applyShapeToPosition = true;
		this._prevBurnMarkPos = new Vector3[2];
		this._prevBurnMarkPosValid = new bool[2];
		this._renderer = base.GetComponent<Renderer>();
		this._renderer.enabled = true;
		this._plane = new Plane(base.transform.up, base.transform.position);
		this._linePoints = new Vector3[100];
		this._fadeOutStrengthShaderPropertyID = Shader.PropertyToID("_FadeOutStrength");
		this._lineRenderers = new LineRenderer[2];
		int num = 31;
		for (int i = 0; i < 2; i++)
		{
			Color color = this._colorManager.EffectsColorForSaberType(this._sabers[i].saberType);
			this._lineRenderers[i] = UnityEngine.Object.Instantiate<LineRenderer>(this._saberBurnMarkLinePrefab, Vector3.zero, Quaternion.identity, null);
			this._lineRenderers[i].startColor = color;
			this._lineRenderers[i].endColor = color;
			this._lineRenderers[i].positionCount = 2;
			Quaternion.Euler(-90f, 0f, 0f);
			this._prevBurnMarkPosValid[i] = false;
		}
		this._renderTextures = new RenderTexture[2];
		this._renderTextures[0] = new RenderTexture(this._textureWidth, this._textureHeight, 0, RenderTextureFormat.ARGB32);
		this._renderTextures[0].name = "SaberBurnMarkArea Textue 0";
		this._renderTextures[0].hideFlags = HideFlags.DontSave;
		this._renderTextures[1] = new RenderTexture(this._textureWidth, this._textureHeight, 0, RenderTextureFormat.ARGB32);
		this._renderTextures[1].name = "SaberBurnMarkArea Textue 1";
		this._renderTextures[1].hideFlags = HideFlags.DontSave;
		GameObject gameObject = new GameObject("BurnMarksCamera");
		this._camera = gameObject.AddComponent<Camera>();
		this._camera.name = "BurnMarksCamera";
		this._camera.orthographic = true;
		this._camera.orthographicSize = 1f;
		this._camera.nearClipPlane = 0f;
		this._camera.farClipPlane = 1f;
		this._camera.clearFlags = CameraClearFlags.Nothing;
		this._camera.backgroundColor = Color.black;
		this._camera.cullingMask = 1 << num;
		this._camera.targetTexture = this._renderTextures[0];
		this._camera.allowMSAA = false;
		this._camera.allowHDR = false;
		this._camera.enabled = false;
		this._renderer.sharedMaterial.mainTexture = this._renderTextures[1];
		this._fadeOutMaterial = new Material(this._fadeOutShader);
		this._fadeOutMaterial.hideFlags = HideFlags.HideAndDontSave;
		this._fadeOutMaterial.mainTexture = this._renderTextures[0];
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x00031FD0 File Offset: 0x000301D0
	protected void OnDestroy()
	{
		if (this._camera)
		{
			UnityEngine.Object.Destroy(this._camera.gameObject);
		}
		if (this._lineRenderers[0])
		{
			UnityEngine.Object.Destroy(this._lineRenderers[0].gameObject);
		}
		if (this._lineRenderers[1])
		{
			UnityEngine.Object.Destroy(this._lineRenderers[1].gameObject);
		}
		EssentialHelpers.SafeDestroy(this._fadeOutMaterial);
		foreach (RenderTexture renderTexture in this._renderTextures)
		{
			if (renderTexture)
			{
				renderTexture.Release();
				EssentialHelpers.SafeDestroy(renderTexture);
			}
		}
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x00032074 File Offset: 0x00030274
	private bool GetBurnMarkPos(Vector3 bladeBottomPos, Vector3 bladeTopPos, out Vector3 burnMarkPos)
	{
		float num = Vector3.Distance(bladeBottomPos, bladeTopPos);
		Vector3 vector = (bladeTopPos - bladeBottomPos) / num;
		float num2;
		if (this._plane.Raycast(new Ray(bladeBottomPos, vector), out num2) && num2 <= num)
		{
			burnMarkPos = bladeBottomPos + vector * num2;
			Bounds bounds = this._renderer.bounds;
			return bounds.min.x < burnMarkPos.x && bounds.max.x > burnMarkPos.x && bounds.min.z < burnMarkPos.z && bounds.max.z > burnMarkPos.z;
		}
		burnMarkPos = Vector3.zero;
		return false;
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00032130 File Offset: 0x00030330
	private Vector3 WorldToCameraBurnMarkPos(Vector3 pos)
	{
		pos = base.transform.InverseTransformPoint(pos);
		Bounds bounds = this._renderer.bounds;
		Vector3 localScale = base.transform.localScale;
		return new Vector3(pos.x * localScale.x / bounds.extents.x * (float)this._textureWidth / (float)this._textureHeight, pos.z * localScale.z / bounds.extents.z, 0f);
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x000321B4 File Offset: 0x000303B4
	protected void LateUpdate()
	{
		if (this._sabers[0] == null)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			Vector3 zero = Vector3.zero;
			bool flag = this._sabers[i].isActiveAndEnabled && this.GetBurnMarkPos(this._sabers[i].saberBladeBottomPos, this._sabers[i].saberBladeTopPos, out zero);
			if (flag && this._prevBurnMarkPosValid[i])
			{
				Vector3 vector = zero - this._prevBurnMarkPos[i];
				float magnitude = vector.magnitude;
				float num = 0.007f;
				int num2 = (int)(magnitude / num);
				int num3 = (num2 > 0) ? num2 : 1;
				num = 0.01f;
				num2 = (int)(magnitude / num);
				int num4 = (num2 > 0) ? num2 : 1;
				Vector3 normalized = new Vector3(vector.z, 0f, -vector.x).normalized;
				int num5 = 0;
				while (num5 <= num4 && num5 < this._linePoints.Length)
				{
					Vector3 vector2 = this._prevBurnMarkPos[i] + vector * (float)num5 / (float)num4;
					vector2 += normalized * UnityEngine.Random.Range(-this._blackMarkLineRandomOffset, this._blackMarkLineRandomOffset);
					this._linePoints[num5] = this.WorldToCameraBurnMarkPos(vector2);
					num5++;
				}
				this._lineRenderers[i].positionCount = num4 + 1;
				this._lineRenderers[i].SetPositions(this._linePoints);
				this._lineRenderers[i].enabled = true;
			}
			else
			{
				this._lineRenderers[i].enabled = false;
			}
			this._prevBurnMarkPosValid[i] = flag;
			this._prevBurnMarkPos[i] = zero;
		}
		if (this._lineRenderers[0].enabled || this._lineRenderers[1].enabled)
		{
			this._camera.Render();
		}
		this._camera.targetTexture = this._renderTextures[1];
		this._renderer.sharedMaterial.mainTexture = this._renderTextures[1];
		this._fadeOutMaterial.mainTexture = this._renderTextures[0];
		this._fadeOutMaterial.SetFloat(this._fadeOutStrengthShaderPropertyID, Mathf.Max(0f, 1f - Time.deltaTime * this._burnMarksFadeOutStrength));
		Graphics.Blit(this._renderTextures[0], this._renderTextures[1], this._fadeOutMaterial);
		RenderTexture renderTexture = this._renderTextures[0];
		this._renderTextures[0] = this._renderTextures[1];
		this._renderTextures[1] = renderTexture;
	}

	// Token: 0x04000B2C RID: 2860
	[SerializeField]
	private LineRenderer _saberBurnMarkLinePrefab;

	// Token: 0x04000B2D RID: 2861
	[SerializeField]
	private float _blackMarkLineRandomOffset = 0.001f;

	// Token: 0x04000B2E RID: 2862
	[SerializeField]
	private int _textureWidth = 1024;

	// Token: 0x04000B2F RID: 2863
	[SerializeField]
	private int _textureHeight = 512;

	// Token: 0x04000B30 RID: 2864
	[SerializeField]
	private float _burnMarksFadeOutStrength = 0.3f;

	// Token: 0x04000B31 RID: 2865
	[SerializeField]
	private Shader _fadeOutShader;

	// Token: 0x04000B32 RID: 2866
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000B33 RID: 2867
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000B34 RID: 2868
	private Renderer _renderer;

	// Token: 0x04000B35 RID: 2869
	private int _fadeOutStrengthShaderPropertyID;

	// Token: 0x04000B36 RID: 2870
	private Saber[] _sabers;

	// Token: 0x04000B37 RID: 2871
	private Plane _plane;

	// Token: 0x04000B38 RID: 2872
	private Vector3[] _prevBurnMarkPos;

	// Token: 0x04000B39 RID: 2873
	private bool[] _prevBurnMarkPosValid;

	// Token: 0x04000B3A RID: 2874
	private LineRenderer[] _lineRenderers;

	// Token: 0x04000B3B RID: 2875
	private Camera _camera;

	// Token: 0x04000B3C RID: 2876
	private Vector3[] _linePoints;

	// Token: 0x04000B3D RID: 2877
	private RenderTexture[] _renderTextures;

	// Token: 0x04000B3E RID: 2878
	private ParticleSystem.EmitParams _emitParams;

	// Token: 0x04000B3F RID: 2879
	private Material _fadeOutMaterial;
}
