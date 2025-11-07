using System;
using UnityEngine;
using VRUIControls;

// Token: 0x0200036F RID: 879
public class MenuShockwave : MonoBehaviour
{
	// Token: 0x06000F91 RID: 3985 RVA: 0x0000BE91 File Offset: 0x0000A091
	protected void Awake()
	{
		this._shockwavePSEmitParams = default(ParticleSystem.EmitParams);
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x0003E3D4 File Offset: 0x0003C5D4
	protected void OnEnable()
	{
		for (int i = 0; i < this._buttonClickEvents.Length; i++)
		{
			this._buttonClickEvents[i].Subscribe(new Action(this.HandleButtonClickEvent));
		}
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x0003E410 File Offset: 0x0003C610
	protected void OnDisable()
	{
		for (int i = 0; i < this._buttonClickEvents.Length; i++)
		{
			this._buttonClickEvents[i].Unsubscribe(new Action(this.HandleButtonClickEvent));
		}
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x0000BE9F File Offset: 0x0000A09F
	private void HandleButtonClickEvent()
	{
		this.SpawnShockwave(this._vrPointer.cursorPosition);
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
	public void SpawnShockwave(Vector3 pos)
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this._shockwavePSEmitParams.position = pos;
		this._shockwavePS.Emit(this._shockwavePSEmitParams, 1);
	}

	// Token: 0x04000FEC RID: 4076
	[SerializeField]
	private ParticleSystem _shockwavePS;

	// Token: 0x04000FED RID: 4077
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private VRPointer _vrPointer;

	// Token: 0x04000FEE RID: 4078
	[SerializeField]
	private Signal[] _buttonClickEvents;

	// Token: 0x04000FEF RID: 4079
	private ParticleSystem.EmitParams _shockwavePSEmitParams;
}
