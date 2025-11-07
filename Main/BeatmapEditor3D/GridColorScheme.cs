using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004DF RID: 1247
	[Serializable]
	public class GridColorScheme
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public Gradient groundColorGradient
		{
			get
			{
				return this._groundColorGradient;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public Gradient frontQuadNormalColorGradient
		{
			get
			{
				return this._frontQuadNormalColorGradient;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00010FD4 File Offset: 0x0000F1D4
		public Gradient frontQuadHighlightedColorGradient
		{
			get
			{
				return this._frontQuadHighlightedColorGradient;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public Gradient fullBeatLineColorGradient
		{
			get
			{
				return this._fullBeatLineColorGradient;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00010FE4 File Offset: 0x0000F1E4
		public Gradient majorBeatLineColorGradient
		{
			get
			{
				return this._majorBeatLineColorGradient;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public Gradient minorBeatLineColorGradient
		{
			get
			{
				return this._minorBeatLineColorGradient;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		public Gradient beatTextColorGradient
		{
			get
			{
				return this._beatTextColorGradient;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public Gradient degreesTextColorGradient
		{
			get
			{
				return this._degreesTextColorGradient;
			}
		}

		// Token: 0x04001700 RID: 5888
		[SerializeField]
		private Gradient _groundColorGradient;

		// Token: 0x04001701 RID: 5889
		[SerializeField]
		private Gradient _frontQuadNormalColorGradient;

		// Token: 0x04001702 RID: 5890
		[SerializeField]
		private Gradient _frontQuadHighlightedColorGradient;

		// Token: 0x04001703 RID: 5891
		[SerializeField]
		private Gradient _fullBeatLineColorGradient;

		// Token: 0x04001704 RID: 5892
		[SerializeField]
		private Gradient _majorBeatLineColorGradient;

		// Token: 0x04001705 RID: 5893
		[SerializeField]
		private Gradient _minorBeatLineColorGradient;

		// Token: 0x04001706 RID: 5894
		[SerializeField]
		private Gradient _beatTextColorGradient;

		// Token: 0x04001707 RID: 5895
		[SerializeField]
		private Gradient _degreesTextColorGradient;
	}
}
