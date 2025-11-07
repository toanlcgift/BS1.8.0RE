using System;
using System.Windows.Forms;

namespace SFB
{
	// Token: 0x020004C0 RID: 1216
	public class WindowWrapper : IWin32Window
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x000106B9 File Offset: 0x0000E8B9
		public WindowWrapper(IntPtr handle)
		{
			this._hwnd = handle;
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000106C8 File Offset: 0x0000E8C8
		public IntPtr Handle
		{
			get
			{
				return this._hwnd;
			}
		}

		// Token: 0x0400168A RID: 5770
		private IntPtr _hwnd;
	}
}
