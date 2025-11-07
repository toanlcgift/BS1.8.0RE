using System;

// Token: 0x020002EA RID: 746
public interface IGamePause
{
	// Token: 0x14000059 RID: 89
	// (add) Token: 0x06000CAC RID: 3244
	// (remove) Token: 0x06000CAD RID: 3245
	event Action didPauseEvent;

	// Token: 0x1400005A RID: 90
	// (add) Token: 0x06000CAE RID: 3246
	// (remove) Token: 0x06000CAF RID: 3247
	event Action didResumeEvent;

	// Token: 0x06000CB0 RID: 3248
	void Pause();

	// Token: 0x06000CB1 RID: 3249
	void Resume();
}
