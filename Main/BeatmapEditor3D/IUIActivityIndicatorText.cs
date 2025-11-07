using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004FB RID: 1275
	public interface IUIActivityIndicatorText
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001801 RID: 6145
		// (set) Token: 0x06001802 RID: 6146
		string text { get; set; }

		// Token: 0x06001803 RID: 6147
		void Show();

		// Token: 0x06001804 RID: 6148
		void Show(string text);

		// Token: 0x06001805 RID: 6149
		void Hide();
	}
}
