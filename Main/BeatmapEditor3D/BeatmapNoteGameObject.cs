using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004CF RID: 1231
	public class BeatmapNoteGameObject : MonoBehaviour
	{
		// Token: 0x060016AD RID: 5805 RVA: 0x00010BE6 File Offset: 0x0000EDE6
		public void ShowArrow(bool show)
		{
			this._arrowGO.SetActive(show);
		}

		// Token: 0x040016D5 RID: 5845
		[SerializeField]
		private GameObject _arrowGO;
	}
}
