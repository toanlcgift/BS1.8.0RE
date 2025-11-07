using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class Cutter
{
	// Token: 0x06000D29 RID: 3369 RVA: 0x00037F40 File Offset: 0x00036140
	public Cutter()
	{
		for (int i = 0; i < 16; i++)
		{
			this._cuttableBySaberSortParams[i] = new Cutter.CuttableBySaberSortParams();
		}
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x00037F94 File Offset: 0x00036194
	public void Cut(Saber saber, Vector3 topPos, Vector3 bottomPos, Vector3 prevTopPos, Vector3 prevBottomPos)
	{
		Vector3 vector;
		Vector3 halfExtents;
		Quaternion orientation;
		if (GeometryTools.ThreePointsToBox(topPos, bottomPos, (prevBottomPos + prevTopPos) * 0.5f, out vector, out halfExtents, out orientation))
		{
			int num = Physics.OverlapBoxNonAlloc(vector, halfExtents, this._colliders, orientation, LayerMasks.noteLayerMask);
			if (num == 0)
			{
				return;
			}
			if (num == 1)
			{
				this._colliders[0].gameObject.GetComponent<CuttableBySaber>().Cut(saber, vector, orientation, topPos - prevTopPos);
				return;
			}
			for (int i = 0; i < num; i++)
			{
				CuttableBySaber component = this._colliders[i].gameObject.GetComponent<CuttableBySaber>();
				Vector3 position = component.transform.position;
				Cutter.CuttableBySaberSortParams cuttableBySaberSortParams = this._cuttableBySaberSortParams[i];
				cuttableBySaberSortParams.cuttableBySaber = component;
				cuttableBySaberSortParams.distance = (prevTopPos - position).sqrMagnitude - component.radius * component.radius;
				cuttableBySaberSortParams.pos = position;
			}
			if (num == 2)
			{
				if (this._comparer.Compare(this._cuttableBySaberSortParams[0], this._cuttableBySaberSortParams[1]) > 0)
				{
					this._cuttableBySaberSortParams[0].cuttableBySaber.Cut(saber, vector, orientation, topPos - prevTopPos);
					this._cuttableBySaberSortParams[1].cuttableBySaber.Cut(saber, vector, orientation, topPos - prevTopPos);
					return;
				}
				this._cuttableBySaberSortParams[1].cuttableBySaber.Cut(saber, vector, orientation, topPos - prevTopPos);
				this._cuttableBySaberSortParams[0].cuttableBySaber.Cut(saber, vector, orientation, topPos - prevTopPos);
				return;
			}
			else
			{
				Array.Sort(this._cuttableBySaberSortParams, 0, num, this._comparer);
				for (int j = 0; j < num; j++)
				{
					this._cuttableBySaberSortParams[j].cuttableBySaber.Cut(saber, vector, orientation, topPos - prevTopPos);
				}
			}
		}
	}

	// Token: 0x04000D90 RID: 3472
	private const int kMaxNumberOfColliders = 16;

	// Token: 0x04000D91 RID: 3473
	private Collider[] _colliders = new Collider[16];

	// Token: 0x04000D92 RID: 3474
	private Cutter.CuttableBySaberSortParams[] _cuttableBySaberSortParams = new Cutter.CuttableBySaberSortParams[16];

	// Token: 0x04000D93 RID: 3475
	private Cutter.CuttableBySaberSortParamsComparer _comparer = new Cutter.CuttableBySaberSortParamsComparer();

	// Token: 0x020002FF RID: 767
	private class CuttableBySaberSortParams
	{
		// Token: 0x04000D94 RID: 3476
		public CuttableBySaber cuttableBySaber;

		// Token: 0x04000D95 RID: 3477
		public float distance;

		// Token: 0x04000D96 RID: 3478
		public Vector3 pos;
	}

	// Token: 0x02000300 RID: 768
	public class CuttableBySaberSortParamsComparer : IComparer
	{
		// Token: 0x06000D2C RID: 3372 RVA: 0x0003815C File Offset: 0x0003635C
		public int Compare(object p0, object p1)
		{
			Cutter.CuttableBySaberSortParams cuttableBySaberSortParams = (Cutter.CuttableBySaberSortParams)p0;
			Cutter.CuttableBySaberSortParams cuttableBySaberSortParams2 = (Cutter.CuttableBySaberSortParams)p1;
			if (cuttableBySaberSortParams.distance > cuttableBySaberSortParams2.distance)
			{
				return 1;
			}
			if (cuttableBySaberSortParams.distance < cuttableBySaberSortParams2.distance)
			{
				return -1;
			}
			if (cuttableBySaberSortParams.pos.x < cuttableBySaberSortParams2.pos.x)
			{
				return 1;
			}
			if (cuttableBySaberSortParams.pos.x > cuttableBySaberSortParams2.pos.x)
			{
				return -1;
			}
			if (cuttableBySaberSortParams.pos.y < cuttableBySaberSortParams2.pos.y)
			{
				return 1;
			}
			if (cuttableBySaberSortParams.pos.y > cuttableBySaberSortParams2.pos.y)
			{
				return -1;
			}
			if (cuttableBySaberSortParams.pos.z < cuttableBySaberSortParams2.pos.z)
			{
				return 1;
			}
			if (cuttableBySaberSortParams.pos.z > cuttableBySaberSortParams2.pos.z)
			{
				return -1;
			}
			return 0;
		}
	}
}
