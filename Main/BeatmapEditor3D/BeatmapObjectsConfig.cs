using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004D8 RID: 1240
	[Serializable]
	public class BeatmapObjectsConfig
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00010EEC File Offset: 0x0000F0EC
		public GameObject noteAPrefab
		{
			get
			{
				return this._noteAPrefab;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public GameObject noteBPrefab
		{
			get
			{
				return this._noteBPrefab;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00010EFC File Offset: 0x0000F0FC
		public GameObject bombPrefab
		{
			get
			{
				return this._bombPrefab;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00010F04 File Offset: 0x0000F104
		public GameObject obstaclePrefab
		{
			get
			{
				return this._obstaclePrefab;
			}
		}

		// Token: 0x040016EC RID: 5868
		[SerializeField]
		private GameObject _noteAPrefab;

		// Token: 0x040016ED RID: 5869
		[SerializeField]
		private GameObject _noteBPrefab;

		// Token: 0x040016EE RID: 5870
		[SerializeField]
		private GameObject _bombPrefab;

		// Token: 0x040016EF RID: 5871
		[SerializeField]
		private GameObject _obstaclePrefab;
	}
}
