using System;

namespace SFB
{
	// Token: 0x020004BE RID: 1214
	public struct ExtensionFilter
	{
		// Token: 0x06001620 RID: 5664 RVA: 0x0001063A File Offset: 0x0000E83A
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this._name = filterName;
			this._extensions = filterExtensions;
		}

		// Token: 0x04001687 RID: 5767
		public readonly string _name;

		// Token: 0x04001688 RID: 5768
		public readonly string[] _extensions;
	}
}
