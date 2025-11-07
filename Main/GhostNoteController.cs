using System;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class GhostNoteController : NoteController
{
	// Token: 0x17000270 RID: 624
	// (set) Token: 0x0600096B RID: 2411 RVA: 0x000077B2 File Offset: 0x000059B2
	public override bool hide
	{
		set
		{
			this._wrapperGO.SetActive(!value);
		}
	}

	// Token: 0x040009C4 RID: 2500
	[SerializeField]
	private GameObject _wrapperGO;
}
