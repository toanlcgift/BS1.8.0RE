using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004CA RID: 1226
	public class BeatmapData
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00010A06 File Offset: 0x0000EC06
		public BeatmapSaveData beatmapSaveData
		{
			get
			{
				return this._beatmapSaveData;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00010A0E File Offset: 0x0000EC0E
		public BeatmapCharacteristicSO characteristic
		{
			get
			{
				return this._characteristic;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00010A16 File Offset: 0x0000EC16
		public BeatmapDifficulty difficulty
		{
			get
			{
				return this._difficulty;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00010A1E File Offset: 0x0000EC1E
		public BeatmapObjectCollection beatmapObjectCollection
		{
			get
			{
				return this._beatmapObjectCollection;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00010A26 File Offset: 0x0000EC26
		public float songDuration
		{
			get
			{
				return this._levelData.songDuration;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00010A33 File Offset: 0x0000EC33
		public float baseBeatsPerMinute
		{
			get
			{
				return this._levelData.levelInfo.beatsPerMinute;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00010A45 File Offset: 0x0000EC45
		public float baseBeatsPerSecond
		{
			get
			{
				return this.baseBeatsPerMinute / 60f;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00010A53 File Offset: 0x0000EC53
		public bool hasVariableBpm
		{
			get
			{
				return this._bpmEvents.Count > 0;
			}
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00052A90 File Offset: 0x00050C90
		public BeatmapData(LevelData levelData, BeatmapCharacteristicSO characteristic, BeatmapDifficulty difficulty)
		{
			this._levelData = levelData;
			this._characteristic = characteristic;
			this._difficulty = difficulty;
			this._beatmapSaveData = levelData.GetBeatmapSaveData(characteristic.serializedName, difficulty);
			if (this._beatmapSaveData != null)
			{
				this.LoadRotationEvents();
				this.LoadBpmEvents();
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00052B0C File Offset: 0x00050D0C
		private void LoadRotationEvents()
		{
			List<BeatmapSaveData.EventData> list = (from e in this._beatmapSaveData.events
			where e.type.IsRotationEvent()
			select e).ToList<BeatmapSaveData.EventData>();
			List<BeatmapData.BeatmapRotationEvent> list2 = new List<BeatmapData.BeatmapRotationEvent>();
			float num = 0f;
			foreach (BeatmapSaveData.EventData eventData in list)
			{
				BeatmapObjectBeatIndex beatIndex = new BeatmapObjectBeatIndex(eventData.time);
				num += this.AngleChangeForEventValue(eventData.value);
				num = num.AngleClampedTo360();
				BeatmapData.BeatmapRotationEvent item = new BeatmapData.BeatmapRotationEvent
				{
					beatIndex = beatIndex,
					eventType = eventData.type,
					eventValue = eventData.value,
					totalAngle = num
				};
				list2.Add(item);
			}
			Debug.Log(string.Format("Beatmap contains {0} rotation events.", list2.Count));
			this._rotationEvents = list2;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00052C0C File Offset: 0x00050E0C
		public float AngleAtBeatIndex(BeatmapObjectBeatIndex beatIndex)
		{
			float result = 0f;
			for (int i = 0; i < this._rotationEvents.Count; i++)
			{
				BeatmapData.BeatmapRotationEvent beatmapRotationEvent = this._rotationEvents[i];
				if (beatmapRotationEvent.beatIndex.beat > beatIndex.beat || (beatmapRotationEvent.beatIndex.beat == beatIndex.beat && beatmapRotationEvent.beatIndex.fraction > beatIndex.fraction))
				{
					break;
				}
				result = beatmapRotationEvent.totalAngle;
			}
			return result;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00010A63 File Offset: 0x0000EC63
		private float AngleChangeForEventValue(int eventValue)
		{
			return this._rotationProcessor.RotationForEventValue(eventValue);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00052C84 File Offset: 0x00050E84
		private void LoadBpmEvents()
		{
			List<BeatmapSaveData.EventData> list = (from e in this._beatmapSaveData.events
			where e.type.IsBPMChangeEvent()
			select e).ToList<BeatmapSaveData.EventData>();
			List<BeatmapData.BeatmapBpmEvent> list2 = new List<BeatmapData.BeatmapBpmEvent>();
			foreach (BeatmapSaveData.EventData eventData in list)
			{
				list2.Add(new BeatmapData.BeatmapBpmEvent
				{
					beatTime = eventData.time,
					beatsPerMinute = (float)eventData.value
				});
			}
			Debug.Log(string.Format("Beatmap contains {0} BPM events.", list2.Count));
			this._bpmEvents = list2;
			this.RecalculateBpmEvents();
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00052D54 File Offset: 0x00050F54
		private void RecalculateBpmEvents()
		{
			List<BeatmapData.BeatmapBpmEvent> bpmEvents = this._bpmEvents;
			for (int i = 0; i < bpmEvents.Count; i++)
			{
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent = bpmEvents[i];
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent2 = (i > 0) ? bpmEvents[i - 1] : null;
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent3 = (i + 1 < bpmEvents.Count) ? bpmEvents[i + 1] : null;
				if (beatmapBpmEvent2 == null)
				{
					beatmapBpmEvent.songTime = beatmapBpmEvent.beatTime / this.baseBeatsPerSecond;
				}
				else
				{
					beatmapBpmEvent.songTime = beatmapBpmEvent2.songTime + beatmapBpmEvent2.songPartDuration;
				}
				if (beatmapBpmEvent3 == null)
				{
					beatmapBpmEvent.nextEventSongTime = 0f;
					beatmapBpmEvent.nextEventBeatTime = 0f;
				}
				else
				{
					beatmapBpmEvent.nextEventBeatTime = beatmapBpmEvent3.beatTime;
					float num = beatmapBpmEvent.numberOfBeatsTillNextEvent / beatmapBpmEvent.beatsPerSecond;
					beatmapBpmEvent.nextEventSongTime = beatmapBpmEvent.songTime + num;
				}
				if (this._verboseLog)
				{
					Debug.Log(beatmapBpmEvent);
				}
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00052E34 File Offset: 0x00051034
		private BeatmapData.BeatmapBpmEvent BpmEventAtSongTime(float songTime)
		{
			for (int i = 0; i < this._bpmEvents.Count; i++)
			{
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent = this._bpmEvents[i];
				if (beatmapBpmEvent.ContainsSongTime(songTime))
				{
					return beatmapBpmEvent;
				}
			}
			return null;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00052E70 File Offset: 0x00051070
		private BeatmapData.BeatmapBpmEvent BpmEventAtBeatTime(float beatTime)
		{
			for (int i = 0; i < this._bpmEvents.Count; i++)
			{
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent = this._bpmEvents[i];
				if (beatmapBpmEvent.ContainsBeatTime(beatTime))
				{
					return beatmapBpmEvent;
				}
			}
			return null;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00010A71 File Offset: 0x0000EC71
		public float BeatTimeFromSongTime(float songTime)
		{
			if (!this.hasVariableBpm)
			{
				return this.BeatTimeFromSongTimeConstantBpm(songTime);
			}
			return this.BeatTimeFromSongTimeVariableBpm(songTime);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00010A8A File Offset: 0x0000EC8A
		public float BeatTimeFromSongTimeConstantBpm(float songTime)
		{
			return songTime * this.baseBeatsPerSecond;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00052EAC File Offset: 0x000510AC
		private float BeatTimeFromSongTimeVariableBpm(float songTime)
		{
			for (int i = 0; i < this._bpmEvents.Count; i++)
			{
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent = this._bpmEvents[i];
				if (i == 0 && songTime < beatmapBpmEvent.songTime)
				{
					return songTime * this.baseBeatsPerSecond;
				}
				if (beatmapBpmEvent.ContainsSongTime(songTime))
				{
					float num = (songTime - beatmapBpmEvent.songTime) * beatmapBpmEvent.beatsPerSecond;
					return beatmapBpmEvent.beatTime + num;
				}
			}
			Debug.LogError("Couldn't find beatTime from songTime " + songTime);
			return 0f;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00010A94 File Offset: 0x0000EC94
		public BeatmapObjectBeatIndex BeatIndexFromSongTime(float songTime)
		{
			return new BeatmapObjectBeatIndex(this.BeatTimeFromSongTime(songTime));
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00010AA2 File Offset: 0x0000ECA2
		public float SongTimeFromBeatTime(float beatTime)
		{
			if (!this.hasVariableBpm)
			{
				return this.SongTimeFromBeatTimeConstantBpm(beatTime);
			}
			return this.SongTimeFromBeatTimeVariableBpm(beatTime);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00010ABB File Offset: 0x0000ECBB
		public float SongTimeFromBeatTimeConstantBpm(float beatTime)
		{
			if (this.baseBeatsPerSecond != 0f)
			{
				return beatTime / this.baseBeatsPerSecond;
			}
			return 0f;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00052F30 File Offset: 0x00051130
		public float SongTimeFromBeatTimeVariableBpm(float beatTime)
		{
			for (int i = 0; i < this._bpmEvents.Count; i++)
			{
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent = this._bpmEvents[i];
				if (i == 0 && beatTime < beatmapBpmEvent.beatTime)
				{
					return beatTime / this.baseBeatsPerSecond;
				}
				if (beatmapBpmEvent.ContainsBeatTime(beatTime))
				{
					float num = (beatTime - beatmapBpmEvent.beatTime) / beatmapBpmEvent.beatsPerSecond;
					return beatmapBpmEvent.songTime + num;
				}
			}
			Debug.LogError("Couldn't find songTime from beatTime " + beatTime);
			return 0f;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public float SongTimeFromBeatIndex(BeatmapObjectBeatIndex beatIndex)
		{
			if (!this.hasVariableBpm)
			{
				return this.SongTimeFromBeatTimeConstantBpm(beatIndex.beatTime);
			}
			return this.SongTimeFromBeatTimeVariableBpm(beatIndex.beatTime);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00052FB4 File Offset: 0x000511B4
		public float BeatLengthMultiplierAtSongTime(float songTime)
		{
			BeatmapData.BeatmapBpmEvent beatmapBpmEvent = this.BpmEventAtSongTime(songTime);
			if (beatmapBpmEvent == null)
			{
				return 1f;
			}
			return this.baseBeatsPerMinute / beatmapBpmEvent.beatsPerMinute;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00052FE0 File Offset: 0x000511E0
		public float BeatLengthMultiplierAtBeatTime(float beatTime)
		{
			BeatmapData.BeatmapBpmEvent beatmapBpmEvent = this.BpmEventAtBeatTime(beatTime);
			if (beatmapBpmEvent == null)
			{
				return 1f;
			}
			return this.baseBeatsPerMinute / beatmapBpmEvent.beatsPerMinute;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00010AFB File Offset: 0x0000ECFB
		public float BeatDistanceFromOrigin(float beatTime, float baseBeatLength)
		{
			if (!this.hasVariableBpm)
			{
				return this.BeatDistanceFromOrigin_ConstantBpm(beatTime, baseBeatLength);
			}
			return this.BeatDistanceFromOrigin_VariableBpm(beatTime, baseBeatLength);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00010B16 File Offset: 0x0000ED16
		public float BeatDistanceFromOrigin_ConstantBpm(float beatTime, float baseBeatLength)
		{
			return beatTime * baseBeatLength;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0005300C File Offset: 0x0005120C
		public float BeatDistanceFromOrigin_VariableBpm(float beatTime, float baseBeatLength)
		{
			List<BeatmapData.BeatmapBpmEvent> bpmEvents = this._bpmEvents;
			float num = 0f;
			for (int i = 0; i < bpmEvents.Count; i++)
			{
				BeatmapData.BeatmapBpmEvent beatmapBpmEvent = bpmEvents[i];
				if (i == 0)
				{
					if (beatTime < beatmapBpmEvent.beatTime)
					{
						return beatTime * baseBeatLength;
					}
					num = beatmapBpmEvent.beatTime * baseBeatLength;
				}
				float num2 = this.baseBeatsPerMinute / beatmapBpmEvent.beatsPerMinute;
				float num3 = baseBeatLength * num2;
				if (beatmapBpmEvent.ContainsBeatTime(beatTime))
				{
					float num4 = (beatTime - beatmapBpmEvent.beatTime) * num3;
					return num + num4;
				}
				float num5 = (beatmapBpmEvent.nextEventBeatTime - beatmapBpmEvent.beatTime) * num3;
				num += num5;
			}
			Debug.LogWarning("Beat distance not found for beatTime " + beatTime);
			return 0f;
		}

		// Token: 0x040016BC RID: 5820
		public const BeatmapEventType kEarlyRotationEvent = BeatmapEventType.Event14;

		// Token: 0x040016BD RID: 5821
		public const BeatmapEventType kLateRotationEvent = BeatmapEventType.Event15;

		// Token: 0x040016BE RID: 5822
		public const BeatmapEventType kBPMChangeEvent = BeatmapEventType.Event10;

		// Token: 0x040016BF RID: 5823
		private bool _verboseLog;

		// Token: 0x040016C0 RID: 5824
		private LevelData _levelData;

		// Token: 0x040016C1 RID: 5825
		private BeatmapSaveData _beatmapSaveData;

		// Token: 0x040016C2 RID: 5826
		private BeatmapCharacteristicSO _characteristic;

		// Token: 0x040016C3 RID: 5827
		private BeatmapDifficulty _difficulty;

		// Token: 0x040016C4 RID: 5828
		private BeatmapObjectCollection _beatmapObjectCollection = new BeatmapObjectCollection();

		// Token: 0x040016C5 RID: 5829
		private SpawnRotationProcessor _rotationProcessor = new SpawnRotationProcessor();

		// Token: 0x040016C6 RID: 5830
		private List<BeatmapData.BeatmapRotationEvent> _rotationEvents = new List<BeatmapData.BeatmapRotationEvent>();

		// Token: 0x040016C7 RID: 5831
		private List<BeatmapData.BeatmapBpmEvent> _bpmEvents = new List<BeatmapData.BeatmapBpmEvent>();

		// Token: 0x020004CB RID: 1227
		private class BeatmapRotationEvent
		{
			// Token: 0x040016C8 RID: 5832
			public BeatmapObjectBeatIndex beatIndex;

			// Token: 0x040016C9 RID: 5833
			public BeatmapEventType eventType;

			// Token: 0x040016CA RID: 5834
			public int eventValue;

			// Token: 0x040016CB RID: 5835
			public float totalAngle;
		}

		// Token: 0x020004CC RID: 1228
		private class BeatmapBpmEvent
		{
			// Token: 0x1700042C RID: 1068
			// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00010B1B File Offset: 0x0000ED1B
			public float beatsPerSecond
			{
				get
				{
					return this.beatsPerMinute / 60f;
				}
			}

			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00010B29 File Offset: 0x0000ED29
			public bool hasNextEvent
			{
				get
				{
					return this.nextEventSongTime != 0f;
				}
			}

			// Token: 0x1700042E RID: 1070
			// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00010B3B File Offset: 0x0000ED3B
			public float songPartDuration
			{
				get
				{
					return this.nextEventSongTime - this.songTime;
				}
			}

			// Token: 0x1700042F RID: 1071
			// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00010B4A File Offset: 0x0000ED4A
			public float numberOfBeatsTillNextEvent
			{
				get
				{
					return this.nextEventBeatTime - this.beatTime;
				}
			}

			// Token: 0x060016A4 RID: 5796 RVA: 0x00010B59 File Offset: 0x0000ED59
			public bool ContainsBeatTime(float beatTime)
			{
				return (beatTime >= this.beatTime && beatTime < this.nextEventBeatTime) || (!this.hasNextEvent && beatTime >= this.beatTime);
			}

			// Token: 0x060016A5 RID: 5797 RVA: 0x00010B83 File Offset: 0x0000ED83
			public bool ContainsSongTime(float songTime)
			{
				return (songTime >= this.songTime && songTime < this.nextEventSongTime) || (!this.hasNextEvent && songTime >= this.songTime);
			}

			// Token: 0x060016A6 RID: 5798 RVA: 0x000530BC File Offset: 0x000512BC
			public override string ToString()
			{
				return string.Format("BpmEvent: beatTime = {0}; BPM = {1}; BPS = {2}; songTime = {3}; songPartDuration = {4};", new object[]
				{
					this.beatTime,
					this.beatsPerMinute,
					this.beatsPerSecond,
					this.songTime,
					this.songPartDuration
				});
			}

			// Token: 0x040016CC RID: 5836
			public float beatTime;

			// Token: 0x040016CD RID: 5837
			public float beatsPerMinute;

			// Token: 0x040016CE RID: 5838
			public float nextEventBeatTime;

			// Token: 0x040016CF RID: 5839
			public float nextEventSongTime;

			// Token: 0x040016D0 RID: 5840
			public float songTime;
		}
	}
}
