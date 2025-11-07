using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class SetActiveRT : MonoBehaviour
{
	// Token: 0x060000B0 RID: 176 RVA: 0x000028BF File Offset: 0x00000ABF
	private void OnPreRender()
	{
		Graphics.SetRenderTarget(this._getActiveRT.ColorBuffer, this._getActiveRT.DepthBuffer);
	}

	// Token: 0x04000099 RID: 153
	[SerializeField]
	private GetActiveRT _getActiveRT;
}
