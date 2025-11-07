using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[RequireComponent(typeof(Camera))]
public class GetActiveRT : MonoBehaviour
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600009C RID: 156 RVA: 0x000027D7 File Offset: 0x000009D7
	public RenderBuffer ColorBuffer
	{
		get
		{
			return this._colorBuffer;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600009D RID: 157 RVA: 0x000027DF File Offset: 0x000009DF
	public RenderBuffer DepthBuffer
	{
		get
		{
			return this._depthBuffer;
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000027E7 File Offset: 0x000009E7
	private void OnPreRender()
	{
		this._colorBuffer = Graphics.activeColorBuffer;
		this._depthBuffer = Graphics.activeDepthBuffer;
	}

	// Token: 0x04000087 RID: 135
	private RenderBuffer _colorBuffer;

	// Token: 0x04000088 RID: 136
	private RenderBuffer _depthBuffer;
}
