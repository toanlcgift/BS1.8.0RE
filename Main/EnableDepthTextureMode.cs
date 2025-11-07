using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class EnableDepthTextureMode : MonoBehaviour
{
	// Token: 0x0600009A RID: 154 RVA: 0x000027C4 File Offset: 0x000009C4
	protected void Awake()
	{
		base.GetComponent<Camera>().depthTextureMode = this._depthTextureMode;
	}

	// Token: 0x04000086 RID: 134
	[SerializeField]
	private DepthTextureMode _depthTextureMode;
}
