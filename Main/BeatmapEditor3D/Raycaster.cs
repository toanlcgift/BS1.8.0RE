using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004F8 RID: 1272
	public class Raycaster
	{
		// Token: 0x060017DD RID: 6109 RVA: 0x00055A28 File Offset: 0x00053C28
		public GameObject RaycastedGameObjectFromCamera(Camera camera, Vector2 screenPosition)
		{
			RaycastHit raycastHit;
			if (!this.RaycastFromCamera(camera, screenPosition, out raycastHit))
			{
				return null;
			}
			return raycastHit.collider.gameObject;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000118D3 File Offset: 0x0000FAD3
		public bool RaycastFromCamera(Camera camera, Vector2 screenPosition, out RaycastHit hitResult)
		{
			if (camera == null)
			{
				Debug.LogWarning("Tried to raycast from null camera");
				hitResult = default(RaycastHit);
				return false;
			}
			return Physics.Raycast(camera.ScreenPointToRay(screenPosition), out hitResult);
		}
	}
}
