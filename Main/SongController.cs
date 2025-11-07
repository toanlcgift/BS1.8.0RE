using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public abstract class SongController : MonoBehaviour
{
	// Token: 0x14000073 RID: 115
	// (add) Token: 0x06000DC7 RID: 3527 RVA: 0x00039918 File Offset: 0x00037B18
	// (remove) Token: 0x06000DC8 RID: 3528 RVA: 0x00039950 File Offset: 0x00037B50
	public event Action songDidFinishEvent;

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0000AA5A File Offset: 0x00008C5A
	public void SendSongDidFinishEvent()
	{
		Action action = this.songDidFinishEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000DCA RID: 3530
	public abstract void StartSong();

	// Token: 0x06000DCB RID: 3531
	public abstract void StopSong();

	// Token: 0x06000DCC RID: 3532
	public abstract void PauseSong();

	// Token: 0x06000DCD RID: 3533
	public abstract void ResumeSong();
}
