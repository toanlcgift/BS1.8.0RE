using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004EA RID: 1258
	public interface IAudioController
	{
		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06001775 RID: 6005
		// (remove) Token: 0x06001776 RID: 6006
		event Action didChangePlayStateEvent;

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001777 RID: 6007
		// (set) Token: 0x06001778 RID: 6008
		bool mute { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001779 RID: 6009
		// (set) Token: 0x0600177A RID: 6010
		float volume { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600177B RID: 6011
		bool isPlaying { get; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600177C RID: 6012
		float duration { get; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600177D RID: 6013
		// (set) Token: 0x0600177E RID: 6014
		float time { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600177F RID: 6015
		// (set) Token: 0x06001780 RID: 6016
		float pitch { get; set; }

		// Token: 0x06001781 RID: 6017
		void Play();

		// Token: 0x06001782 RID: 6018
		void Play(float time);

		// Token: 0x06001783 RID: 6019
		void Pause();

		// Token: 0x06001784 RID: 6020
		void PlayOrPause();

		// Token: 0x06001785 RID: 6021
		void Stop();

		// Token: 0x06001786 RID: 6022
		void GoToStart();

		// Token: 0x06001787 RID: 6023
		void GoToEnd();
	}
}
