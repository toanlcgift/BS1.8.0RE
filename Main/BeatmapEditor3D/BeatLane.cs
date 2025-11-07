using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004DE RID: 1246
	public class BeatLane : MonoBehaviour
	{
		// Token: 0x060016E0 RID: 5856 RVA: 0x00010F82 File Offset: 0x0000F182
		public void Init(GridLaneConfig gridLaneConfig, int barsPerBeat)
		{
			this.InitBeatLines(gridLaneConfig, barsPerBeat);
			this.InitBeatNumber(gridLaneConfig);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000534D4 File Offset: 0x000516D4
		private void InitBeatNumber(GridLaneConfig gridLaneConfig)
		{
			Vector3 localPosition = this._beatText.transform.localPosition;
			localPosition.x = gridLaneConfig.laneWidth * 0.5f + 0.1f;
			localPosition.y = -gridLaneConfig.groundHeight * 0.5f;
			this._beatText.transform.localPosition = localPosition;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00053530 File Offset: 0x00051730
		private void InitBeatLines(GridLaneConfig gridLaneConfig, int barsPerBeat)
		{
			float baseBeatLength = gridLaneConfig.baseBeatLength;
			int num = 1;
			float num2 = baseBeatLength / (float)num;
			Vector3 lineSize = new Vector3(gridLaneConfig.laneWidth, gridLaneConfig.fullBeatLineThicknes, 1f);
			for (int i = 0; i < num; i++)
			{
				Vector3 position = new Vector3(0f, 0f, (float)i * num2);
				this.InitBeatLine(this._fullBeatLinesGO.transform, position, lineSize);
			}
			int num3 = barsPerBeat - num;
			float num4 = baseBeatLength / (float)barsPerBeat;
			Vector3 lineSize2 = new Vector3(gridLaneConfig.laneWidth, gridLaneConfig.majorBeatLineThicknes, 1f);
			for (int j = 0; j < num3; j++)
			{
				Vector3 position2 = new Vector3(0f, 0f, ((float)j + 1f / (float)num) * num4 * (float)num);
				this.InitBeatLine(this._majorBeatLinesGO.transform, position2, lineSize2);
			}
			float num5 = baseBeatLength / (float)barsPerBeat;
			Vector3 lineSize3 = new Vector3(gridLaneConfig.laneWidth, gridLaneConfig.minorBeatLineThicknes, 1f);
			for (int k = 0; k < barsPerBeat; k++)
			{
				Vector3 position3 = new Vector3(0f, 0f, ((float)k + 0.5f) * num5);
				this.InitBeatLine(this._minorBeatLinesGO.transform, position3, lineSize3);
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00053674 File Offset: 0x00051874
		private GameObject InitBeatLine(Transform parent, Vector3 position, Vector3 lineSize)
		{
			GameObject gameObject = this._container.InstantiatePrefab(this._beatLinePrefab, Vector3.zero, Quaternion.identity, parent);
			gameObject.name = string.Format("BeatLine({0})", position.z);
			Vector3 localPosition = parent.transform.localPosition;
			position.z += lineSize.y * 0.5f;
			gameObject.transform.localPosition = localPosition + position;
			gameObject.transform.localScale = lineSize;
			gameObject.transform.Rotate(Vector3.right, 90f);
			return gameObject;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00010F93 File Offset: 0x0000F193
		public void SetColorScheme(GridColorScheme colorScheme)
		{
			this._colorScheme = colorScheme;
			this.SetColorAtGradientPosition(0f);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00053710 File Offset: 0x00051910
		public void SetColorAtGradientPosition(float gradientPosition)
		{
			this._beatText.color = this._colorScheme.beatTextColorGradient.Evaluate(gradientPosition);
			this.SetColorForChildern(this._colorScheme.fullBeatLineColorGradient.Evaluate(gradientPosition), this._fullBeatLinesGO.transform);
			this.SetColorForChildern(this._colorScheme.majorBeatLineColorGradient.Evaluate(gradientPosition), this._majorBeatLinesGO.transform);
			this.SetColorForChildern(this._colorScheme.minorBeatLineColorGradient.Evaluate(gradientPosition), this._minorBeatLinesGO.transform);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000537A0 File Offset: 0x000519A0
		private void SetColorForChildern(Color color, Transform parent)
		{
			for (int i = 0; i < parent.childCount; i++)
			{
				parent.GetChild(i).gameObject.GetComponent<Renderer>().material.color = color;
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00010FA7 File Offset: 0x0000F1A7
		public void SetBeatNumber(int beat)
		{
			this._beatText.text = string.Format("{0}", beat);
		}

		// Token: 0x040016F9 RID: 5881
		[SerializeField]
		private TextMeshPro _beatText;

		// Token: 0x040016FA RID: 5882
		[SerializeField]
		private GameObject _beatLinePrefab;

		// Token: 0x040016FB RID: 5883
		[SerializeField]
		private GameObject _fullBeatLinesGO;

		// Token: 0x040016FC RID: 5884
		[SerializeField]
		private GameObject _majorBeatLinesGO;

		// Token: 0x040016FD RID: 5885
		[SerializeField]
		private GameObject _minorBeatLinesGO;

		// Token: 0x040016FE RID: 5886
		[Inject]
		private DiContainer _container;

		// Token: 0x040016FF RID: 5887
		private GridColorScheme _colorScheme;
	}
}
