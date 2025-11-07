using System;
using UnityEngine;
using Zenject;

// Token: 0x0200029E RID: 670
public class Spectrogram : MonoBehaviour
{
	// Token: 0x06000B4B RID: 2891 RVA: 0x00008E46 File Offset: 0x00007046
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void NoDomainReloadInit()
	{
		Spectrogram._materialPropertyBlock = null;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00008E4E File Offset: 0x0000704E
	protected void Awake()
	{
		if (Spectrogram._materialPropertyBlock == null)
		{
			Spectrogram._materialPropertyBlock = new MaterialPropertyBlock();
		}
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00033FBC File Offset: 0x000321BC
	protected void Update()
	{
		Spectrogram._materialPropertyBlock.SetFloatArray(Spectrogram._spectrogramDataID, this._spectrogramData.ProcessedSamples);
		MeshRenderer[] meshRenderers = this._meshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(Spectrogram._materialPropertyBlock);
		}
	}

	// Token: 0x04000BEC RID: 3052
	[SerializeField]
	private MeshRenderer[] _meshRenderers;

	// Token: 0x04000BED RID: 3053
	[Inject]
	private BasicSpectrogramData _spectrogramData;

	// Token: 0x04000BEE RID: 3054
	[DoesNotRequireDomainReloadInit]
	private static readonly int _spectrogramDataID = Shader.PropertyToID("_SpectrogramData");

	// Token: 0x04000BEF RID: 3055
	private static MaterialPropertyBlock _materialPropertyBlock;
}
