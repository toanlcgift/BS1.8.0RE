using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004E4 RID: 1252
	public class GridController : MonoBehaviour
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00011061 File Offset: 0x0000F261
		private int lanesCount
		{
			get
			{
				return this._lanes.Length;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0001106B File Offset: 0x0000F26B
		private float _baseBeatLength
		{
			get
			{
				return this._gridConfig.gridLaneConfig.baseBeatLength;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0001107D File Offset: 0x0000F27D
		private float _currentSongTime
		{
			get
			{
				return this._songAudioController.time;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0001108A File Offset: 0x0000F28A
		private float _currentBeatTime
		{
			get
			{
				if (this._beatmapData == null)
				{
					return 0f;
				}
				return this._beatmapData.BeatTimeFromSongTime(this._currentSongTime);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x000110AB File Offset: 0x0000F2AB
		private BeatmapObjectBeatIndex _currentBeatIndex
		{
			get
			{
				if (this._beatmapData == null)
				{
					return new BeatmapObjectBeatIndex(0, 0);
				}
				return this._beatmapData.BeatIndexFromSongTime(this._currentSongTime);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x000110CE File Offset: 0x0000F2CE
		public bool hasBeatmapData
		{
			get
			{
				return this._beatmapData != null;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x000110D9 File Offset: 0x0000F2D9
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x000110E6 File Offset: 0x0000F2E6
		public bool variablePlayheadSpeed
		{
			get
			{
				return this._gridConfig.variablePlayheadSpeed;
			}
			set
			{
				this._gridConfig.variablePlayheadSpeed = value;
				this.Reconfigure();
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000110FA File Offset: 0x0000F2FA
		public bool isPlacingObstacle
		{
			get
			{
				return this._placedObstacleBeatmapObject != null;
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00011105 File Offset: 0x0000F305
		protected void Awake()
		{
			this._songAudioController.didChangePlayStateEvent += this.HandleSongDidChangeState;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0001111E File Offset: 0x0000F31E
		protected void OnDestroy()
		{
			this._songAudioController.didChangePlayStateEvent -= this.HandleSongDidChangeState;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00011137 File Offset: 0x0000F337
		protected void Update()
		{
			if (this._songAudioController.isPlaying)
			{
				this.UpdatePlayhead();
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0001114C File Offset: 0x0000F34C
		public void Init()
		{
			this.InitLanes(this._gridConfig);
			this.SetActiveLanes(0, false);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00011162 File Offset: 0x0000F362
		public void SwitchPlayheadSpeedVariability()
		{
			this.variablePlayheadSpeed = !this.variablePlayheadSpeed;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000537DC File Offset: 0x000519DC
		private void SetActiveLanes(BeatmapCharacteristicSO characteristic)
		{
			int num = 0;
			if (characteristic.requires360Movement)
			{
				num = this.lanesCount;
			}
			else if (characteristic.containsRotationEvents)
			{
				num = Mathf.CeilToInt(90f / this._gridConfig.laneDegreesStep);
				num /= 2;
			}
			this.SetActiveLanes(num, true);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00053828 File Offset: 0x00051A28
		private void SetActiveLanes(int activeSideLanesCount, bool centralLane = true)
		{
			this._lanes[0].gameObject.SetActive(centralLane);
			int num = this._lanes.Length;
			int num2 = num / 2;
			for (int i = 1; i <= num2; i++)
			{
				bool active = i <= activeSideLanesCount;
				this._lanes[i].gameObject.SetActive(active);
				this._lanes[num - i].gameObject.SetActive(active);
			}
			this.UpdateLaneVisibility();
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00053898 File Offset: 0x00051A98
		private void UpdateLaneVisibility()
		{
			GridLane currentLane;
			if ((currentLane = this.GetCurrentLane()) != null)
			{
				this.UpdateLaneVisibility(currentLane.angle);
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000538BC File Offset: 0x00051ABC
		private void UpdateLaneVisibility(float centralAngle)
		{
			int num = this._lanes.Length;
			int num2 = num / 2;
			for (int i = 0; i < num; i++)
			{
				GridLane gridLane = this._lanes[i];
				float colorAtGradientPosition = this.LaneAngleDifference01(centralAngle, gridLane.angle);
				this._lanes[i].SetColorAtGradientPosition(colorAtGradientPosition);
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00053908 File Offset: 0x00051B08
		private float LaneAngleDifference01(float centralAngle, float angle)
		{
			centralAngle = centralAngle.AngleClampedTo360();
			angle = angle.AngleClampedTo360();
			float num = angle - centralAngle;
			if (num > 180f)
			{
				num -= 360f;
			}
			return Mathf.Abs(num / 180f);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00053948 File Offset: 0x00051B48
		private void InitLanes(GridConfig gridConfig)
		{
			if (this._lanes != null)
			{
				GridLane[] lanes = this._lanes;
				for (int i = 0; i < lanes.Length; i++)
				{
					UnityEngine.Object.Destroy(lanes[i].gameObject);
				}
			}
			int totalLanesCount = gridConfig.totalLanesCount;
			this._lanes = new GridLane[totalLanesCount];
			for (int j = 0; j < totalLanesCount; j++)
			{
				float degrees = (float)j * gridConfig.laneDegreesStep;
				GridLane gridLane = this.InstantiateLane(gridConfig, degrees);
				this._lanes[j] = gridLane;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000539C0 File Offset: 0x00051BC0
		private GridLane InstantiateLane(GridConfig gridConfig, float degrees)
		{
			GameObject gameObject = this._container.InstantiatePrefab(this._gridLanePrefab, Vector3.zero, Quaternion.identity, this._gridLanesGO.transform);
			gameObject.name = string.Format("GridLane({0})", (int)degrees);
			GridLane component = gameObject.GetComponent<GridLane>();
			component.Init(gridConfig.gridLaneConfig, this._colorScheme, degrees);
			gameObject.transform.RotateAround(this._gridLanesGO.transform.position, Vector3.up, degrees);
			gameObject.transform.localPosition += gameObject.transform.forward * gridConfig.innerCircleRadius;
			return component;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00011173 File Offset: 0x0000F373
		private GridLane GetCurrentLane()
		{
			return this.GetLane(this._currentBeatIndex);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00011181 File Offset: 0x0000F381
		public float GetCurrentLaneAngle()
		{
			return this.GetLaneAngle(this._currentBeatIndex);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0001118F File Offset: 0x0000F38F
		private GridLane GetLane(BeatmapObject beatmapObject)
		{
			return this.GetLane(beatmapObject.position.beatIndex);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00053A74 File Offset: 0x00051C74
		private GridLane GetLane(BeatmapObjectBeatIndex beatIndex)
		{
			int laneIndex = this.GetLaneIndex(beatIndex);
			return this._lanes[laneIndex];
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00053A94 File Offset: 0x00051C94
		private int GetLaneIndex(BeatmapObjectBeatIndex beatIndex)
		{
			float laneAngle = this.GetLaneAngle(beatIndex);
			return this.GetLaneIndex(laneAngle);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000111A2 File Offset: 0x0000F3A2
		private float GetLaneAngle(BeatmapObjectBeatIndex beatIndex)
		{
			if (!this.hasBeatmapData)
			{
				return 0f;
			}
			return this._beatmapData.AngleAtBeatIndex(beatIndex);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000111BE File Offset: 0x0000F3BE
		private int GetLaneIndex(float angle)
		{
			return Mathf.RoundToInt(angle / this._gridConfig.laneDegreesStep);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00053AB0 File Offset: 0x00051CB0
		public void SetBeatmapData(BeatmapData beatmapData)
		{
			this.RemoveAllData();
			if (beatmapData == null)
			{
				Debug.LogWarning("Tried to set null BeatmapData.");
				return;
			}
			this._beatmapData = beatmapData;
			GridLane[] lanes = this._lanes;
			for (int i = 0; i < lanes.Length; i++)
			{
				lanes[i].beatmapData = beatmapData;
			}
			this.SetActiveLanes(beatmapData.characteristic);
			List<BeatmapSaveData.NoteData> notes = beatmapData.beatmapSaveData.notes;
			for (int j = 0; j < notes.Count; j++)
			{
				BeatmapSaveData.NoteData noteData = notes[j];
				BeatmapObjectBeatIndex beatIndex = new BeatmapObjectBeatIndex(noteData.time);
				BeatmapObjectLineIndex lineIndex = new BeatmapObjectLineIndex(noteData.lineIndex, (int)noteData.lineLayer);
				BeatmapObjectPosition position = new BeatmapObjectPosition(beatIndex, lineIndex);
				this.AddNewBeatmapObject(this.BeatmapObjectTypeFromNoteType(noteData.type), position, noteData.cutDirection);
			}
			List<BeatmapSaveData.ObstacleData> obstacles = this._beatmapData.beatmapSaveData.obstacles;
			for (int k = 0; k < obstacles.Count; k++)
			{
				BeatmapSaveData.ObstacleData obstacleData = obstacles[k];
				bool flag = obstacleData.type == ObstacleType.FullHeight;
				int lineIndex2 = obstacleData.lineIndex;
				int y = flag ? 0 : (this._gridConfig.gridLaneConfig.rows - 1);
				Vector3 lineCellSize = this._gridConfig.gridLaneConfig.lineCellSize;
				this._beatmapData.BeatLengthMultiplierAtBeatTime(obstacleData.time);
				float baseBeatLength = this._gridConfig.gridLaneConfig.baseBeatLength;
				int num = flag ? this._gridConfig.gridLaneConfig.rows : 1;
				BeatmapObjectSizeIndex sizeIndex = new BeatmapObjectSizeIndex((float)obstacleData.width, (float)num, obstacleData.duration);
				BeatmapObjectBeatIndex beatIndex2 = new BeatmapObjectBeatIndex(obstacleData.time);
				BeatmapObjectLineIndex lineIndex3 = new BeatmapObjectLineIndex(lineIndex2, y);
				BeatmapObjectPosition position2 = new BeatmapObjectPosition(beatIndex2, lineIndex3, sizeIndex);
				this.AddNewBeatmapObject(BeatmapObjectType.Obstacle, position2, NoteCutDirection.None);
			}
			this.UpdatePlayhead();
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00053C70 File Offset: 0x00051E70
		public BeatmapObject AddNewBeatmapObjectAtCurrentGridLaneAngle(BeatmapObjectLineIndex lineIndex)
		{
			if (!this.hasBeatmapData)
			{
				return null;
			}
			BeatmapObjectPosition position = new BeatmapObjectPosition(this._currentBeatIndex, lineIndex);
			NoteCutDirection direction = this._preselectedBeatmapObjectType.IsNote() ? this._preselectedNoteCutDirection : NoteCutDirection.None;
			if (this._preselectedBeatmapObjectType == BeatmapObjectType.Obstacle)
			{
				return this.AddNewObstacleBeatmapObject(this._preselectedBeatmapObjectType, position, direction);
			}
			if (this.isPlacingObstacle)
			{
				this.RemoveBeatmapObject(this._placedObstacleBeatmapObject);
			}
			return this.AddNewBeatmapObject(this._preselectedBeatmapObjectType, position, direction);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00053CE8 File Offset: 0x00051EE8
		public BeatmapObject FinishPlacingObstacle()
		{
			if (!this.hasBeatmapData)
			{
				return null;
			}
			if (!this.isPlacingObstacle)
			{
				Debug.LogWarning("Tried to finish placing obstacle but there is no obstacle being placed.");
				return null;
			}
			BeatmapObject placedObstacleBeatmapObject = this._placedObstacleBeatmapObject;
			this.UpdatePlacedObstacleLength(placedObstacleBeatmapObject.position, this._currentBeatTime);
			this.BeatmapObjectPropertiesDidChange(placedObstacleBeatmapObject);
			Debug.Log(string.Format("Obstacle placement finished at {0}", placedObstacleBeatmapObject.position));
			this._placedObstacleBeatmapObject = null;
			return placedObstacleBeatmapObject;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00053D50 File Offset: 0x00051F50
		private BeatmapObject AddNewObstacleBeatmapObject(BeatmapObjectType type, BeatmapObjectPosition position, NoteCutDirection direction)
		{
			if (!this.hasBeatmapData)
			{
				return null;
			}
			if (this.isPlacingObstacle)
			{
				Debug.LogWarning("Tried to add new obstacle but there is one being placed already.");
				return null;
			}
			if (!this.CanPlaceNewBeatmapObjectAtPosition(position))
			{
				return null;
			}
			Debug.Log(string.Format("Obstacle placement started at {0}", position));
			int num = this._gridConfig.gridLaneConfig.rows - 1;
			bool flag = position.lineIndex.y < num;
			position.lineIndex.y = (flag ? 0 : num);
			position.lineIndex.x = (flag ? position.lineIndex.x : 0);
			position.sizeIndex = (flag ? new BeatmapObjectSizeIndex(1f, (float)this._gridConfig.gridLaneConfig.rows, 1f) : new BeatmapObjectSizeIndex((float)this._gridConfig.gridLaneConfig.columns, 1f, 1f));
			this.UpdatePlacedObstacleLength(position, 0f);
			BeatmapObject beatmapObject = this.NewBeatmapObject(type, position, direction);
			this.AddBeatmapObject(beatmapObject);
			this._placedObstacleBeatmapObject = beatmapObject;
			return beatmapObject;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00053E58 File Offset: 0x00052058
		private void UpdatePlacedObstacleLength(BeatmapObjectPosition position, float currentBeatTime)
		{
			if (!this.hasBeatmapData)
			{
				return;
			}
			if (position == null)
			{
				return;
			}
			float num = currentBeatTime - position.beatIndex.beatTime;
			float num2 = 0.25f;
			if (num < num2)
			{
				num = num2;
			}
			position.sizeIndex.beats = num;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000111D2 File Offset: 0x0000F3D2
		private void BeatmapObjectPropertiesDidChange(BeatmapObject beatmapObject)
		{
			GridLane lane = this.GetLane(beatmapObject);
			lane.SetBeatmapObject(beatmapObject);
			lane.UpdateBeatmapObjectVisibility(beatmapObject);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00053E98 File Offset: 0x00052098
		private BeatmapObject AddNewBeatmapObject(BeatmapObjectType type, BeatmapObjectPosition position, NoteCutDirection direction)
		{
			if (!this.hasBeatmapData)
			{
				return null;
			}
			if (!this.CanPlaceNewBeatmapObjectAtPosition(position))
			{
				return null;
			}
			BeatmapObject beatmapObject = this.NewBeatmapObject(type, position, direction);
			this.AddBeatmapObject(beatmapObject);
			return beatmapObject;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00053ECC File Offset: 0x000520CC
		private BeatmapObject NewBeatmapObject(BeatmapObjectType type, BeatmapObjectPosition position, NoteCutDirection direction)
		{
			BeatmapObject beatmapObject = new BeatmapObject(type, position, direction);
			beatmapObject.gameObject = this.NewGameObjectForBeatmapObjectType(beatmapObject.type);
			return beatmapObject;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00053EF8 File Offset: 0x000520F8
		private GameObject NewGameObjectForBeatmapObjectType(BeatmapObjectType type)
		{
			GameObject prefab = this.PrefabFromBeatmapObjectType(type);
			return this._container.InstantiatePrefab(prefab, Vector3.zero, Quaternion.identity, null);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000111E8 File Offset: 0x0000F3E8
		public bool CanPlaceNewBeatmapObjectAtPosition(BeatmapObjectPosition position)
		{
			if (this._isBeatmapObjectReplacingEnabled)
			{
				return true;
			}
			bool flag = this._beatmapData.beatmapObjectCollection.GetBeatmapObjectAtPosition(position) == null;
			if (!flag)
			{
				Debug.Log(string.Format("Cannot place new object at {0} because there is already some object.", position));
			}
			return flag;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0001121B File Offset: 0x0000F41B
		private void AddBeatmapObject(BeatmapObject beatmapObject)
		{
			this.RemoveBeatmapObjectAtPosition(beatmapObject.position);
			this._beatmapData.beatmapObjectCollection.SetBeatmapObject(beatmapObject);
			this.GetLane(beatmapObject).SetBeatmapObject(beatmapObject);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00011247 File Offset: 0x0000F447
		private void UpdateBeatmapObjectDirection(BeatmapObject beatmapObject, NoteCutDirection newDirection)
		{
			beatmapObject.direction = newDirection;
			this.BeatmapObjectPropertiesDidChange(beatmapObject);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00011257 File Offset: 0x0000F457
		private void UpdateBeatmapObjectPosition(BeatmapObject beatmapObject, BeatmapObjectPosition newPosition)
		{
			beatmapObject.position = newPosition;
			this.BeatmapObjectPropertiesDidChange(beatmapObject);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00011267 File Offset: 0x0000F467
		private BeatmapObjectType BeatmapObjectTypeFromNoteType(NoteType noteType)
		{
			switch (noteType)
			{
			case NoteType.NoteA:
				return BeatmapObjectType.NoteA;
			case NoteType.NoteB:
				return BeatmapObjectType.NoteB;
			case NoteType.Bomb:
				return BeatmapObjectType.Bomb;
			}
			Debug.LogError("Unsupported NoteType: " + noteType);
			return BeatmapObjectType.Bomb;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00053F24 File Offset: 0x00052124
		private GameObject PrefabFromBeatmapObjectType(BeatmapObjectType type)
		{
			switch (type)
			{
			case BeatmapObjectType.NoteA:
				return this._beatmapObjectsConfig.noteAPrefab;
			case BeatmapObjectType.NoteB:
				return this._beatmapObjectsConfig.noteBPrefab;
			case BeatmapObjectType.Bomb:
				return this._beatmapObjectsConfig.bombPrefab;
			case BeatmapObjectType.Obstacle:
				return this._beatmapObjectsConfig.obstaclePrefab;
			default:
				Debug.LogError("Unsupported BeatmapObjectType: " + type);
				return this._beatmapObjectsConfig.obstaclePrefab;
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0001129D File Offset: 0x0000F49D
		public void PreselectBeatmapObjectType(BeatmapObjectType type)
		{
			this._preselectedBeatmapObjectType = type;
			this.editMode = GridEditMode.Edit;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000112AD File Offset: 0x0000F4AD
		public void PreselectNoteDirection(NoteCutDirection direction)
		{
			this._preselectedNoteCutDirection = direction;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00053F9C File Offset: 0x0005219C
		public void ChangeNoteTypeForGameObject(GameObject go)
		{
			BeatmapGameObject component;
			if ((component = go.GetComponent<BeatmapGameObject>()) != null)
			{
				this.ChangeNoteTypeAtBeatmapObjectPosition(component.beatmapObjectPosition);
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00053FC0 File Offset: 0x000521C0
		private void ChangeNoteTypeAtBeatmapObjectPosition(BeatmapObjectPosition position)
		{
			BeatmapObject beatmapObjectAtPosition;
			if ((beatmapObjectAtPosition = this._beatmapData.beatmapObjectCollection.GetBeatmapObjectAtPosition(position)) != null && beatmapObjectAtPosition.type.IsNote())
			{
				this.RemoveBeatmapObject(beatmapObjectAtPosition);
				beatmapObjectAtPosition.type = ((beatmapObjectAtPosition.type == BeatmapObjectType.NoteA) ? BeatmapObjectType.NoteB : BeatmapObjectType.NoteA);
				this.AddNewBeatmapObject(beatmapObjectAtPosition.type, beatmapObjectAtPosition.position, beatmapObjectAtPosition.direction);
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00054024 File Offset: 0x00052224
		public void SetNoteCutDirectionForGameObject(GameObject go, NoteCutDirection direction)
		{
			BeatmapGameObject component;
			if ((component = go.GetComponent<BeatmapGameObject>()) != null)
			{
				this.SetNoteCutDirectionAtBeatmapObjectPosition(component.beatmapObjectPosition, direction);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00054048 File Offset: 0x00052248
		private void SetNoteCutDirectionAtBeatmapObjectPosition(BeatmapObjectPosition position, NoteCutDirection direction)
		{
			BeatmapObject beatmapObjectAtPosition;
			if ((beatmapObjectAtPosition = this._beatmapData.beatmapObjectCollection.GetBeatmapObjectAtPosition(position)) != null && beatmapObjectAtPosition.type.IsNote())
			{
				beatmapObjectAtPosition.direction = direction;
				this.BeatmapObjectPropertiesDidChange(beatmapObjectAtPosition);
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000112B6 File Offset: 0x0000F4B6
		public void SelectGameObject(GameObject go)
		{
			this.HighlightGameObject(go, true);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00054088 File Offset: 0x00052288
		public void HighlightGameObject(GameObject go, bool highlight)
		{
			if (go == null)
			{
				return;
			}
			GridLaneFrontQuad component;
			if ((component = go.GetComponent<GridLaneFrontQuad>()) != null)
			{
				component.isHighlighted = highlight;
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public bool IsObjectSelectable(GameObject go)
		{
			return !(go == null) && (go.GetComponent<GridLaneFrontQuad>() != null || go.GetComponent<BeatmapGameObject>() != null);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000112E2 File Offset: 0x0000F4E2
		public void Reconfigure()
		{
			base.StartCoroutine(this.ReconfigureCoroutine());
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000112F1 File Offset: 0x0000F4F1
		private IEnumerator ReconfigureCoroutine()
		{
			this._fullscreenActivityIndicator.Show();
			yield return null;
			this.Init();
			yield return null;
			this.SetBeatmapData(this._beatmapData);
			this._fullscreenActivityIndicator.Hide();
			yield break;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00011300 File Offset: 0x0000F500
		public void ReloadAllData()
		{
			if (!this.hasBeatmapData)
			{
				return;
			}
			this._beatmapData.beatmapObjectCollection.ExecuteBlockForEachBeatmapObject(delegate(BeatmapObject beatmapObject)
			{
				this.GetLane(beatmapObject).SetBeatmapObject(beatmapObject);
			});
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000540B0 File Offset: 0x000522B0
		public void RemoveAllData()
		{
			GridLane[] lanes = this._lanes;
			for (int i = 0; i < lanes.Length; i++)
			{
				lanes[i].beatmapData = null;
			}
			if (this.hasBeatmapData)
			{
				this._beatmapData.beatmapObjectCollection.ExecuteBlockForEachBeatmapObject(delegate(BeatmapObject beatmapObject)
				{
					this.DestroyBeatmapGameObject(beatmapObject);
				});
				this._beatmapData.beatmapObjectCollection.RemoveAllData();
				this._beatmapData = null;
			}
			this.UpdatePlayhead();
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0005411C File Offset: 0x0005231C
		public void RemoveGameObject(GameObject go)
		{
			BeatmapGameObject component;
			if ((component = go.GetComponent<BeatmapGameObject>()) != null)
			{
				this.RemoveBeatmapObjectAtPosition(component.beatmapObjectPosition);
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00054140 File Offset: 0x00052340
		public void RemoveBeatmapObjectAtPosition(BeatmapObjectPosition position)
		{
			BeatmapObject beatmapObjectAtPosition;
			if ((beatmapObjectAtPosition = this._beatmapData.beatmapObjectCollection.GetBeatmapObjectAtPosition(position)) != null)
			{
				this.RemoveBeatmapObject(beatmapObjectAtPosition);
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00011327 File Offset: 0x0000F527
		private void RemoveBeatmapObject(BeatmapObject beatmapObject)
		{
			this.DestroyBeatmapGameObject(beatmapObject);
			this._beatmapData.beatmapObjectCollection.RemoveBeatmapObjectAtPosition(beatmapObject.position);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0005416C File Offset: 0x0005236C
		private void DestroyBeatmapGameObject(BeatmapObject beatmapObject)
		{
			GameObject gameObject;
			if ((gameObject = beatmapObject.gameObject) != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00011346 File Offset: 0x0000F546
		private void HandleSongDidChangeState()
		{
			this.UpdatePlayhead();
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0001134E File Offset: 0x0000F54E
		private void UpdatePlayhead()
		{
			this.SetPlayheadPosition(this._currentSongTime);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0005418C File Offset: 0x0005238C
		private void SetPlayheadPosition(float songTime)
		{
			if (!this.hasBeatmapData)
			{
				return;
			}
			float num = this.variablePlayheadSpeed ? this._beatmapData.BeatTimeFromSongTime(songTime) : this._beatmapData.BeatTimeFromSongTimeConstantBpm(songTime);
			float num2 = num * this._baseBeatLength;
			GridLane[] lanes = this._lanes;
			for (int i = 0; i < lanes.Length; i++)
			{
				lanes[i].SetPlayheadPosition(-num2);
			}
			if (this._placedObstacleBeatmapObject != null)
			{
				this.UpdatePlacedObstacleLength(this._placedObstacleBeatmapObject.position, num);
				this.BeatmapObjectPropertiesDidChange(this._placedObstacleBeatmapObject);
			}
			this.UpdateLaneVisibility();
		}

		// Token: 0x04001711 RID: 5905
		[SerializeField]
		private GameObject _gridLanePrefab;

		// Token: 0x04001712 RID: 5906
		[SerializeField]
		private GameObject _gridLanesGO;

		// Token: 0x04001713 RID: 5907
		[Inject]
		private GridConfig _gridConfig;

		// Token: 0x04001714 RID: 5908
		[Inject]
		private GridColorScheme _colorScheme;

		// Token: 0x04001715 RID: 5909
		[Inject]
		private BeatmapObjectsConfig _beatmapObjectsConfig;

		// Token: 0x04001716 RID: 5910
		[Inject]
		private SongAudioController _songAudioController;

		// Token: 0x04001717 RID: 5911
		[Inject]
		private UIActivityIndicatorText _fullscreenActivityIndicator;

		// Token: 0x04001718 RID: 5912
		[Inject]
		private DiContainer _container;

		// Token: 0x04001719 RID: 5913
		private GridLane[] _lanes;

		// Token: 0x0400171A RID: 5914
		private BeatmapData _beatmapData;

		// Token: 0x0400171B RID: 5915
		private BeatmapObjectType _preselectedBeatmapObjectType;

		// Token: 0x0400171C RID: 5916
		private NoteCutDirection _preselectedNoteCutDirection = NoteCutDirection.Down;

		// Token: 0x0400171D RID: 5917
		private bool _isBeatmapObjectReplacingEnabled;

		// Token: 0x0400171E RID: 5918
		private BeatmapObject _placedObstacleBeatmapObject;

		// Token: 0x0400171F RID: 5919
		public GridEditMode editMode;
	}
}
