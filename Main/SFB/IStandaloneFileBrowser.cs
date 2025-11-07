using System;

namespace SFB
{
	// Token: 0x020004BD RID: 1213
	public interface IStandaloneFileBrowser
	{
		// Token: 0x0600161A RID: 5658
		string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);

		// Token: 0x0600161B RID: 5659
		string[] OpenFolderPanel(string title, string directory, bool multiselect);

		// Token: 0x0600161C RID: 5660
		string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);

		// Token: 0x0600161D RID: 5661
		void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb);

		// Token: 0x0600161E RID: 5662
		void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb);

		// Token: 0x0600161F RID: 5663
		void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb);
	}
}
