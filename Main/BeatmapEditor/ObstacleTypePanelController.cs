using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000581 RID: 1409
	public class ObstacleTypePanelController : MonoBehaviour, IBeatmapEditorObstacleLength, IBeatmapEditorObstacleType
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000144E1 File Offset: 0x000126E1
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x000144E9 File Offset: 0x000126E9
		public ObstacleType obstacleType { get; private set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x000144F2 File Offset: 0x000126F2
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x000144FA File Offset: 0x000126FA
		public int obstacleLength { get; private set; }

		// Token: 0x06001B72 RID: 7026 RVA: 0x0005EB60 File Offset: 0x0005CD60
		protected void Awake()
		{
			this.obstacleLength = 8;
			this._lengthText.text = this.obstacleLength.ToString();
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00014503 File Offset: 0x00012703
		public void SetObstacleType(int index)
		{
			this.obstacleType = (ObstacleType)index;
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0005EB90 File Offset: 0x0005CD90
		public void IncreaseLength(bool doubleLength)
		{
			if (doubleLength)
			{
				if (this.obstacleLength < 64)
				{
					this.obstacleLength *= 2;
				}
			}
			else
			{
				this.obstacleLength /= 2;
			}
			if (this.obstacleLength < 1)
			{
				this.obstacleLength = 1;
			}
			this._lengthText.text = this.obstacleLength.ToString();
		}

		// Token: 0x04001A18 RID: 6680
		[SerializeField]
		private Text _lengthText;
	}
}
