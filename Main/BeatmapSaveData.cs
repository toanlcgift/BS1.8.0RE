using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000104 RID: 260
[Serializable]
public class BeatmapSaveData
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000469F File Offset: 0x0000289F
	public string version
	{
		get
		{
			return this._version;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000046A7 File Offset: 0x000028A7
	public List<BeatmapSaveData.EventData> events
	{
		get
		{
			return this._events;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000046AF File Offset: 0x000028AF
	public List<BeatmapSaveData.NoteData> notes
	{
		get
		{
			return this._notes;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000046B7 File Offset: 0x000028B7
	public List<BeatmapSaveData.ObstacleData> obstacles
	{
		get
		{
			return this._obstacles;
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000046BF File Offset: 0x000028BF
	public BeatmapSaveData(List<BeatmapSaveData.EventData> events, List<BeatmapSaveData.NoteData> notes, List<BeatmapSaveData.ObstacleData> obstacles)
	{
		this._version = "2.0.0";
		this._events = events;
		this._notes = notes;
		this._obstacles = obstacles;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00020570 File Offset: 0x0001E770
	public byte[] SerializeToBinary()
	{
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			new BinaryFormatter().Serialize(memoryStream, this);
			memoryStream.Close();
			result = memoryStream.ToArray();
		}
		return result;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x000205BC File Offset: 0x0001E7BC
	public static BeatmapSaveData DeserializeFromFromBinary(byte[] data)
	{
		BeatmapSaveData result;
		using (MemoryStream memoryStream = new MemoryStream(data))
		{
			result = (BeatmapSaveData)new BinaryFormatter().Deserialize(memoryStream);
		}
		return result;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000046E7 File Offset: 0x000028E7
	public string SerializeToJSONString()
	{
		return JsonUtility.ToJson(this);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00020600 File Offset: 0x0001E800
	public static BeatmapSaveData DeserializeFromJSONString(string stringData)
	{
		BeatmapSaveData beatmapSaveData = JsonUtility.FromJson<BeatmapSaveData>(stringData);
		if (beatmapSaveData != null)
		{
			//beatmapSaveData.version != "2.0.0";
		}
		return beatmapSaveData;
	}

	// Token: 0x0400045B RID: 1115
	private const string kCurrentVersion = "2.0.0";

	// Token: 0x0400045C RID: 1116
	[SerializeField]
	private string _version;

	// Token: 0x0400045D RID: 1117
	[SerializeField]
	private List<BeatmapSaveData.EventData> _events;

	// Token: 0x0400045E RID: 1118
	[SerializeField]
	private List<BeatmapSaveData.NoteData> _notes;

	// Token: 0x0400045F RID: 1119
	[SerializeField]
	private List<BeatmapSaveData.ObstacleData> _obstacles;

	// Token: 0x02000105 RID: 261
	public interface ITime
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003FF RID: 1023
		float time { get; }

		// Token: 0x06000400 RID: 1024
		void MoveTime(float offset);
	}

	// Token: 0x02000106 RID: 262
	[Serializable]
	public class EventData : BeatmapSaveData.ITime
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000046EF File Offset: 0x000028EF
		public float time
		{
			get
			{
				return this._time;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000046F7 File Offset: 0x000028F7
		public BeatmapEventType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000046FF File Offset: 0x000028FF
		public int value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00004707 File Offset: 0x00002907
		public EventData(float time, BeatmapEventType type, int value)
		{
			this._time = time;
			this._type = type;
			this._value = value;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00004724 File Offset: 0x00002924
		public void MoveTime(float offset)
		{
			this._time += offset;
		}

		// Token: 0x04000460 RID: 1120
		[SerializeField]
		private float _time;

		// Token: 0x04000461 RID: 1121
		[SerializeField]
		private BeatmapEventType _type;

		// Token: 0x04000462 RID: 1122
		[SerializeField]
		private int _value;
	}

	// Token: 0x02000107 RID: 263
	[Serializable]
	public class NoteData : BeatmapSaveData.ITime
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00004734 File Offset: 0x00002934
		public float time
		{
			get
			{
				return this._time;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000473C File Offset: 0x0000293C
		public int lineIndex
		{
			get
			{
				return this._lineIndex;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00004744 File Offset: 0x00002944
		public NoteLineLayer lineLayer
		{
			get
			{
				return this._lineLayer;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000474C File Offset: 0x0000294C
		public NoteType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00004754 File Offset: 0x00002954
		public NoteCutDirection cutDirection
		{
			get
			{
				return this._cutDirection;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000475C File Offset: 0x0000295C
		public NoteData(float time, int lineIndex, NoteLineLayer lineLayer, NoteType type, NoteCutDirection cutDirection)
		{
			this._time = time;
			this._lineIndex = lineIndex;
			this._lineLayer = lineLayer;
			this._type = type;
			this._cutDirection = cutDirection;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00004789 File Offset: 0x00002989
		public void MoveTime(float offset)
		{
			this._time += offset;
		}

		// Token: 0x04000463 RID: 1123
		[SerializeField]
		private float _time;

		// Token: 0x04000464 RID: 1124
		[SerializeField]
		private int _lineIndex;

		// Token: 0x04000465 RID: 1125
		[SerializeField]
		private NoteLineLayer _lineLayer;

		// Token: 0x04000466 RID: 1126
		[SerializeField]
		private NoteType _type;

		// Token: 0x04000467 RID: 1127
		[SerializeField]
		private NoteCutDirection _cutDirection;
	}

	// Token: 0x02000108 RID: 264
	[Serializable]
	public class ObstacleData : BeatmapSaveData.ITime
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00004799 File Offset: 0x00002999
		public float time
		{
			get
			{
				return this._time;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000047A1 File Offset: 0x000029A1
		public int lineIndex
		{
			get
			{
				return this._lineIndex;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000047A9 File Offset: 0x000029A9
		public ObstacleType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000047B1 File Offset: 0x000029B1
		public float duration
		{
			get
			{
				return this._duration;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000047B9 File Offset: 0x000029B9
		public int width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000047C1 File Offset: 0x000029C1
		public ObstacleData(float time, int lineIndex, ObstacleType type, float duration, int width)
		{
			this._time = time;
			this._lineIndex = lineIndex;
			this._type = type;
			this._duration = duration;
			this._width = width;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000047EE File Offset: 0x000029EE
		public void MoveTime(float offset)
		{
			this._time += offset;
		}

		// Token: 0x04000468 RID: 1128
		[SerializeField]
		private float _time;

		// Token: 0x04000469 RID: 1129
		[SerializeField]
		private int _lineIndex;

		// Token: 0x0400046A RID: 1130
		[SerializeField]
		private ObstacleType _type;

		// Token: 0x0400046B RID: 1131
		[SerializeField]
		private float _duration;

		// Token: 0x0400046C RID: 1132
		[SerializeField]
		private int _width;
	}
}
