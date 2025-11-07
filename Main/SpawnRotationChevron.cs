using System;
using UnityEngine;
using Zenject;

// Token: 0x0200029B RID: 667
public class SpawnRotationChevron : MonoBehaviour
{
	// Token: 0x06000B42 RID: 2882 RVA: 0x00033E98 File Offset: 0x00032098
	protected void Start()
	{
		GameObject[] array = this._chevronGameObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(this._initData.enableChevron);
		}
		array = this._sideLineGameObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(this._initData.enableSideLines);
		}
	}

	// Token: 0x04000BE1 RID: 3041
	[SerializeField]
	private GameObject[] _chevronGameObjects;

	// Token: 0x04000BE2 RID: 3042
	[SerializeField]
	private GameObject[] _sideLineGameObjects;

	// Token: 0x04000BE3 RID: 3043
	[Inject]
	private SpawnRotationChevron.InitData _initData;

	// Token: 0x0200029C RID: 668
	public class InitData
	{
		// Token: 0x06000B44 RID: 2884 RVA: 0x00008D9E File Offset: 0x00006F9E
		public InitData(bool enableChevron, bool enableSideLines)
		{
			this.enableChevron = enableChevron;
			this.enableSideLines = enableSideLines;
		}

		// Token: 0x04000BE4 RID: 3044
		public readonly bool enableChevron;

		// Token: 0x04000BE5 RID: 3045
		public readonly bool enableSideLines;
	}
}
