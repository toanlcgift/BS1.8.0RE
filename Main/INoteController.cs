using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public interface INoteController
{
	// Token: 0x17000271 RID: 625
	// (get) Token: 0x0600096D RID: 2413
	NoteData noteData { get; }

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x0600096E RID: 2414
	Transform noteTransform { get; }

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x0600096F RID: 2415
	Quaternion worldRotation { get; }

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000970 RID: 2416
	Quaternion inverseWorldRotation { get; }

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000971 RID: 2417
	Vector3 jumpMoveVec { get; }
}
