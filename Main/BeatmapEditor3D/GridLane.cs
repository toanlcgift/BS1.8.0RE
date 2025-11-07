using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004E6 RID: 1254
	public class GridLane : MonoBehaviour
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0001139A File Offset: 0x0000F59A
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x000113A2 File Offset: 0x0000F5A2
		public BeatmapData beatmapData
		{
			get
			{
				return this._beatmapData;
			}
			set
			{
				this._beatmapData = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000113AB File Offset: 0x0000F5AB
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x000113B3 File Offset: 0x0000F5B3
		public float angle
		{
			get
			{
				return this._angle;
			}
			set
			{
				this._angle = value;
				this._angleText.text = string.Format("{0}", (int)this._angle);
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000113DD File Offset: 0x0000F5DD
		protected void Awake()
		{
			this._groundRenderer = this._groundGO.GetComponent<Renderer>();
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000113F0 File Offset: 0x0000F5F0
		public void Init(GridLaneConfig gridLaneConfig, GridColorScheme colorScheme, float angle)
		{
			this._gridLaneConfig = gridLaneConfig;
			this._colorScheme = colorScheme;
			this.angle = angle;
			this.Init();
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0001140D File Offset: 0x0000F60D
		private void Init()
		{
			this.InitGround();
			this.CalculateBottomLeftLinePosition();
			this.InitFrontQuads();
			this.InitBeatLanes();
			this.InitNotes();
			this.SetColorScheme(this._colorScheme);
			this.HighlightAllFrontQuads(false);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000542AC File Offset: 0x000524AC
		private void InitGround()
		{
			Vector3 localScale = this._groundGO.transform.localScale;
			localScale.x = this._gridLaneConfig.laneWidth;
			localScale.y = this._gridLaneConfig.groundHeight;
			localScale.z = this._gridLaneConfig.laneLength;
			this._groundGO.transform.localScale = localScale;
			Vector3 groundTopFrontCenterPosition = this._groundTopFrontCenterPosition;
			groundTopFrontCenterPosition.y -= localScale.y * 0.5f;
			groundTopFrontCenterPosition.z += localScale.z * 0.5f;
			this._groundGO.transform.localPosition = groundTopFrontCenterPosition;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00054358 File Offset: 0x00052558
		private void CalculateBottomLeftLinePosition()
		{
			Vector2 vector = new Vector2(this._gridLaneConfig.laneWidth, this._gridLaneConfig.groundHeight);
			Vector2 vector2 = this._gridLaneConfig.lineCellSize;
			Vector3 groundTopFrontCenterPosition = this._groundTopFrontCenterPosition;
			groundTopFrontCenterPosition.x += -vector.x * 0.5f + vector2.x * 0.5f;
			groundTopFrontCenterPosition.y += vector2.y * 0.5f;
			this._lineBottomLeftCenterPosition = groundTopFrontCenterPosition;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000543E0 File Offset: 0x000525E0
		private void InitFrontQuads()
		{
			if (this._frontQuads != null)
			{
				GridLaneFrontQuad[,] frontQuads = this._frontQuads;
				int upperBound = frontQuads.GetUpperBound(0);
				int upperBound2 = frontQuads.GetUpperBound(1);
				for (int i = frontQuads.GetLowerBound(0); i <= upperBound; i++)
				{
					for (int j = frontQuads.GetLowerBound(1); j <= upperBound2; j++)
					{
						UnityEngine.Object.Destroy(frontQuads[i, j].gameObject);
					}
				}
			}
			int columns = this._gridLaneConfig.columns;
			int rows = this._gridLaneConfig.rows;
			this._frontQuads = new GridLaneFrontQuad[columns, rows];
			Vector2 vector = this._gridLaneConfig.lineCellSize;
			Vector3 localScale = vector - this._gridLaneConfig.frontQuadMargin;
			localScale.z = 1f;
			Vector3 lineBottomLeftCenterPosition = this._lineBottomLeftCenterPosition;
			for (int k = 0; k < rows; k++)
			{
				for (int l = 0; l < columns; l++)
				{
					GameObject gameObject = this._container.InstantiatePrefab(this._frontQuadPrefab, Vector3.zero, Quaternion.identity, this._frontQuadsGO.transform);
					gameObject.name = string.Format("FrontQuad({0},{1})", l, k);
					gameObject.transform.localScale = localScale;
					Vector3 localPosition = lineBottomLeftCenterPosition;
					localPosition.x += (float)l * vector.x;
					localPosition.y += (float)k * vector.y;
					gameObject.transform.localPosition = localPosition;
					GridLaneFrontQuad component = gameObject.GetComponent<GridLaneFrontQuad>();
					component.laneAngle = this._angle;
					component.positionIndex = new Vector2Int(l, k);
					this._frontQuads[l, k] = component;
				}
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000545A4 File Offset: 0x000527A4
		private void InitBeatLanes()
		{
			if (this._beatLanes != null)
			{
				BeatLane[] beatLanes = this._beatLanes;
				for (int i = 0; i < beatLanes.Length; i++)
				{
					UnityEngine.Object.Destroy(beatLanes[i].gameObject);
				}
			}
			float baseBeatLength = this._gridLaneConfig.baseBeatLength;
			int num = Mathf.CeilToInt(this._gridLaneConfig.laneLength / baseBeatLength);
			this._beatLanes = new BeatLane[num];
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = this._container.InstantiatePrefab(this._beatLanePrefab, Vector3.zero, Quaternion.identity, this._beatLanesGO.transform);
				gameObject.name = string.Format("BeatLane({0})", j);
				Vector3 localPosition = new Vector3(0f, 0f, (float)j * baseBeatLength);
				gameObject.transform.localPosition = localPosition;
				BeatLane component = gameObject.GetComponent<BeatLane>();
				this._beatLanes[j] = component;
				component.Init(this._gridLaneConfig, 4);
				component.SetBeatNumber(j);
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000546A8 File Offset: 0x000528A8
		public void SetColorAtGradientPosition(float gradientPosition)
		{
			this._angleText.color = this._angleTextColorGradient.Evaluate(gradientPosition);
			this._groundRenderer.material.color = this._groundColorGradient.Evaluate(gradientPosition);
			GridLaneFrontQuad[,] frontQuads = this._frontQuads;
			int upperBound = frontQuads.GetUpperBound(0);
			int i = frontQuads.GetUpperBound(1);
			for (int j = frontQuads.GetLowerBound(0); j <= upperBound; j++)
			{
				for (int k = frontQuads.GetLowerBound(1); k <= i; k++)
				{
					frontQuads[j, k].SetColorAtGradientPosition(gradientPosition);
				}
			}
			BeatLane[] beatLanes = this._beatLanes;
			for (i = 0; i < beatLanes.Length; i++)
			{
				beatLanes[i].SetColorAtGradientPosition(gradientPosition);
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00054758 File Offset: 0x00052958
		public void SetColorScheme(GridColorScheme colorScheme)
		{
			this._angleTextColorGradient = colorScheme.degreesTextColorGradient;
			this._groundColorGradient = colorScheme.groundColorGradient;
			GridLaneFrontQuad[,] frontQuads = this._frontQuads;
			int upperBound = frontQuads.GetUpperBound(0);
			int i = frontQuads.GetUpperBound(1);
			for (int j = frontQuads.GetLowerBound(0); j <= upperBound; j++)
			{
				for (int k = frontQuads.GetLowerBound(1); k <= i; k++)
				{
					frontQuads[j, k].SetColorScheme(colorScheme);
				}
			}
			BeatLane[] beatLanes = this._beatLanes;
			for (i = 0; i < beatLanes.Length; i++)
			{
				beatLanes[i].SetColorScheme(colorScheme);
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000547EC File Offset: 0x000529EC
		public void HighlightAllFrontQuads(bool highlight)
		{
			GridLaneFrontQuad[,] frontQuads = this._frontQuads;
			int upperBound = frontQuads.GetUpperBound(0);
			int upperBound2 = frontQuads.GetUpperBound(1);
			for (int i = frontQuads.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = frontQuads.GetLowerBound(1); j <= upperBound2; j++)
				{
					frontQuads[i, j].isHighlighted = highlight;
				}
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00054848 File Offset: 0x00052A48
		public void HighlightFrontQuad(bool highlight, int x, int y)
		{
			GridLaneFrontQuad gridLaneFrontQuad = this.FrontQuadAtIndex(x, y);
			if (gridLaneFrontQuad != null)
			{
				gridLaneFrontQuad.isHighlighted = highlight;
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00054870 File Offset: 0x00052A70
		public GridLaneFrontQuad FrontQuadAtIndex(int x, int y)
		{
			int length = this._frontQuads.GetLength(0);
			int length2 = this._frontQuads.GetLength(1);
			if (x < length && y < length2)
			{
				return this._frontQuads[x, y];
			}
			return null;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000548B0 File Offset: 0x00052AB0
		public void SetPlayheadPosition(float distanceFromOrigin)
		{
			Vector3 lineBottomLeftCenterPosition = this._lineBottomLeftCenterPosition;
			lineBottomLeftCenterPosition.z = distanceFromOrigin;
			this._notesGO.transform.localPosition = lineBottomLeftCenterPosition;
			this.UpdateBeatmapObjectVisibility();
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000548E4 File Offset: 0x00052AE4
		public void UpdateBeatmapObjectVisibility()
		{
			Transform transform = this._notesGO.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).gameObject;
				this.UpdateBeatmapGameObjectVisibility(gameObject);
			}
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00054924 File Offset: 0x00052B24
		public void UpdateBeatmapObjectVisibility(BeatmapObject beatmapObject)
		{
			GameObject gameObject;
			if ((gameObject = beatmapObject.gameObject) != null)
			{
				this.UpdateBeatmapGameObjectVisibility(gameObject);
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00054944 File Offset: 0x00052B44
		private void UpdateBeatmapGameObjectVisibility(GameObject go)
		{
			float num = this.GameObjectDistanceFromLaneOrigin(go);
			float num2 = this.GameObjectDistanceFromLaneEnd(go);
			bool flag = num > this._gridLaneConfig.firstVisibleNoteDistanceOffset && num2 < this._gridLaneConfig.lastVisibleNoteDistanceOffset;
			if (go.activeSelf != flag)
			{
				go.SetActive(flag);
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00054990 File Offset: 0x00052B90
		public float GameObjectDistanceFromLaneOrigin(GameObject go)
		{
			float z = this._notesGO.transform.localPosition.z;
			float num = go.transform.localPosition.z + go.transform.localScale.z * 0.5f;
			return z + num;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000549DC File Offset: 0x00052BDC
		public float GameObjectDistanceFromLaneEnd(GameObject go)
		{
			float z = this._notesGO.transform.localPosition.z;
			float num = go.transform.localPosition.z - go.transform.localScale.z * 0.5f;
			return z + num - this._gridLaneConfig.laneLength;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00011440 File Offset: 0x0000F640
		private void InitNotes()
		{
			this._notesGO.transform.localPosition = this._lineBottomLeftCenterPosition;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00054A34 File Offset: 0x00052C34
		public void SetBeatmapObject(BeatmapObject beatmapObject)
		{
			GameObject gameObject = beatmapObject.gameObject;
			gameObject.name = string.Format("BeatmapObject at {0} at {1} degrees lane", beatmapObject.position, this._angleText.text);
			BeatmapGameObject component;
			if ((component = gameObject.GetComponent<BeatmapGameObject>()) != null)
			{
				component.beatmapObjectPosition = beatmapObject.position;
			}
			BeatmapNoteGameObject component2;
			if ((component2 = gameObject.GetComponent<BeatmapNoteGameObject>()) != null)
			{
				bool flag = beatmapObject.direction == NoteCutDirection.None || beatmapObject.direction == NoteCutDirection.Any;
				component2.ShowArrow(!flag);
			}
			Transform transform = gameObject.transform;
			transform.parent = this._notesGO.transform;
			transform.localRotation = (beatmapObject.type.IsNote() ? beatmapObject.direction.Rotation() : Quaternion.identity);
			BeatmapObjectPosition position = beatmapObject.position;
			Vector3 lineCellSize = this._gridLaneConfig.lineCellSize;
			if (position.sizeIndex.hasSize)
			{
				float num = this._beatmapData.BeatLengthMultiplierAtBeatTime(beatmapObject.position.beatIndex.beatTime) * this._gridLaneConfig.baseBeatLength;
				Vector3 localScale = new Vector3(position.sizeIndex.columns * lineCellSize.x, position.sizeIndex.rows * lineCellSize.y, position.sizeIndex.beats * num);
				transform.localScale = localScale;
			}
			float z = this.beatmapData.BeatDistanceFromOrigin(position.beatIndex.beatTime, this._gridLaneConfig.baseBeatLength);
			Vector3 localPosition = new Vector3((float)position.lineIndex.x * lineCellSize.x, (float)position.lineIndex.y * lineCellSize.y, z);
			if (position.sizeIndex.hasSize)
			{
				localPosition.x += (transform.localScale.x - lineCellSize.x) * 0.5f;
				localPosition.y += (transform.localScale.y - lineCellSize.y) * 0.5f;
				localPosition.z += transform.localScale.z * 0.5f;
			}
			transform.localPosition = localPosition;
		}

		// Token: 0x04001723 RID: 5923
		[SerializeField]
		private TextMeshPro _angleText;

		// Token: 0x04001724 RID: 5924
		[SerializeField]
		private GameObject _groundGO;

		// Token: 0x04001725 RID: 5925
		[SerializeField]
		private GameObject _frontQuadsGO;

		// Token: 0x04001726 RID: 5926
		[SerializeField]
		private GameObject _frontQuadPrefab;

		// Token: 0x04001727 RID: 5927
		[SerializeField]
		private GameObject _beatLanesGO;

		// Token: 0x04001728 RID: 5928
		[SerializeField]
		private GameObject _beatLanePrefab;

		// Token: 0x04001729 RID: 5929
		[SerializeField]
		private GameObject _notesGO;

		// Token: 0x0400172A RID: 5930
		[Inject]
		private DiContainer _container;

		// Token: 0x0400172B RID: 5931
		private GridLaneConfig _gridLaneConfig;

		// Token: 0x0400172C RID: 5932
		private GridColorScheme _colorScheme;

		// Token: 0x0400172D RID: 5933
		private GridLaneFrontQuad[,] _frontQuads;

		// Token: 0x0400172E RID: 5934
		private BeatLane[] _beatLanes;

		// Token: 0x0400172F RID: 5935
		private Vector3 _lineBottomLeftCenterPosition = Vector3.zero;

		// Token: 0x04001730 RID: 5936
		private Vector3 _groundTopFrontCenterPosition = Vector3.zero;

		// Token: 0x04001731 RID: 5937
		private float _angle;

		// Token: 0x04001732 RID: 5938
		private Gradient _angleTextColorGradient;

		// Token: 0x04001733 RID: 5939
		private Renderer _groundRenderer;

		// Token: 0x04001734 RID: 5940
		private Gradient _groundColorGradient;

		// Token: 0x04001735 RID: 5941
		private BeatmapData _beatmapData;
	}
}
