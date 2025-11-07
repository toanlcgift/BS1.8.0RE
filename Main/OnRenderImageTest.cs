using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class OnRenderImageTest : MonoBehaviour
{
	// Token: 0x060000A3 RID: 163 RVA: 0x000027FF File Offset: 0x000009FF
	private void Start()
	{
		this._rt = new RenderTexture(512, 512, 0);
		this._rt.hideFlags = HideFlags.DontSave;
		this._stereoCopyMaterial = new Material(Shader.Find("Hidden/CopyStereo"));
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00002839 File Offset: 0x00000A39
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, this._rt, this._stereoCopyMaterial);
		Graphics.Blit(this._rt, destination);
	}

	// Token: 0x04000089 RID: 137
	[SerializeField]
	private RenderTexture _rt;

	// Token: 0x0400008A RID: 138
	private Material _stereoCopyMaterial;
}
