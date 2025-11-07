using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000022 RID: 34
[ExecuteInEditMode]
public class DirectionalESMLight : MonoBehaviour
{
	// Token: 0x06000096 RID: 150 RVA: 0x00015F44 File Offset: 0x00014144
	protected void OnEnable()
	{
		this._esmShadowTexture = new RenderTexture(2048, 2048, 0, RenderTextureFormat.RFloat);
		this._copyBuffer = new CommandBuffer();
		this._esmBlitMaterial = new Material(Shader.Find("Hidden/ESMBlit"));
		base.GetComponent<Light>().AddCommandBuffer(LightEvent.AfterShadowMap, this._copyBuffer);
		GraphicsSettings.SetShaderMode(BuiltinShaderType.ScreenSpaceShadows, BuiltinShaderMode.UseCustom);
		GraphicsSettings.SetCustomShader(BuiltinShaderType.ScreenSpaceShadows, Shader.Find("Hidden/ScreenSpaceShadowsESM"));
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00015FB4 File Offset: 0x000141B4
	protected void Update()
	{
		this._copyBuffer.Clear();
		this._copyBuffer.SetShadowSamplingMode(BuiltinRenderTextureType.CurrentActive, ShadowSamplingMode.RawDepth);
		this._copyBuffer.SetGlobalTexture("_ShadowMapMain", BuiltinRenderTextureType.CurrentActive);
		this._copyBuffer.Blit(BuiltinRenderTextureType.CurrentActive, this._esmShadowTexture, this._esmBlitMaterial, 0);
		int nameID = Shader.PropertyToID("_ShadowMapTemp0");
		int nameID2 = Shader.PropertyToID("_ShadowMapTemp1");
		this._copyBuffer.GetTemporaryRT(nameID, this._esmShadowTexture.width / 2, this._esmShadowTexture.height / 2, 0, FilterMode.Bilinear, RenderTextureFormat.RFloat);
		this._copyBuffer.SetGlobalVector("_Parameter", new Vector4(this._blurSize, -this._blurSize, 0f, 0f));
		this._copyBuffer.Blit(this._esmShadowTexture, nameID, this._esmBlitMaterial, 1);
		this._copyBuffer.Blit(nameID, nameID2, this._esmBlitMaterial, 2);
		this._copyBuffer.ReleaseTemporaryRT(nameID);
		this._copyBuffer.Blit(nameID2, this._esmShadowTexture, this._esmBlitMaterial, 3);
		this._copyBuffer.ReleaseTemporaryRT(nameID2);
		this._copyBuffer.SetGlobalTexture("_ShadowMapESM", this._esmShadowTexture);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00002784 File Offset: 0x00000984
	protected void OnDisable()
	{
		base.GetComponent<Light>().RemoveCommandBuffer(LightEvent.AfterShadowMap, this._copyBuffer);
		GraphicsSettings.SetCustomShader(BuiltinShaderType.ScreenSpaceShadows, null);
		GraphicsSettings.SetShaderMode(BuiltinShaderType.ScreenSpaceShadows, BuiltinShaderMode.UseBuiltin);
		UnityEngine.Object.DestroyImmediate(this._esmShadowTexture);
	}

	// Token: 0x04000082 RID: 130
	[Range(1f, 10f)]
	[SerializeField]
	private float _blurSize = 1f;

	// Token: 0x04000083 RID: 131
	private RenderTexture _esmShadowTexture;

	// Token: 0x04000084 RID: 132
	private CommandBuffer _copyBuffer;

	// Token: 0x04000085 RID: 133
	private Material _esmBlitMaterial;
}
