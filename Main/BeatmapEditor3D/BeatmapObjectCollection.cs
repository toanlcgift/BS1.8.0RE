using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004D7 RID: 1239
	public class BeatmapObjectCollection
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x00010E95 File Offset: 0x0000F095
		public void RemoveAllData()
		{
			this._beatDict = new Dictionary<int, List<BeatmapObject>>();
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00053170 File Offset: 0x00051370
		public void ExecuteBlockForEachBeatmapObject(Action<BeatmapObject> callback)
		{
			foreach (List<BeatmapObject> list in this._beatDict.Values)
			{
				foreach (BeatmapObject obj in list)
				{
					callback(obj);
				}
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000531FC File Offset: 0x000513FC
		private List<BeatmapObject> GetAllBeatmapObjectList()
		{
			List<BeatmapObject> list = new List<BeatmapObject>();
			foreach (List<BeatmapObject> list2 in this._beatDict.Values)
			{
				foreach (BeatmapObject item in list2)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00053290 File Offset: 0x00051490
		private List<BeatmapObject> GetOrAddBeatmapObjectList(int beat)
		{
			List<BeatmapObject> list = this.GetBeatmapObjectList(beat);
			if (list == null)
			{
				list = this.AddBeatmapObjectList(beat);
			}
			return list;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00010EA2 File Offset: 0x0000F0A2
		private List<BeatmapObject> GetBeatmapObjectList(int beat)
		{
			if (!this._beatDict.ContainsKey(beat))
			{
				return null;
			}
			return this._beatDict[beat];
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000532B4 File Offset: 0x000514B4
		private List<BeatmapObject> AddBeatmapObjectList(int beat)
		{
			if (this._beatDict.ContainsKey(beat))
			{
				Debug.LogWarning(string.Format("Tried to add new BeatmapObjectList at beat {0}, but the list already exists.", beat));
				return this._beatDict[beat];
			}
			List<BeatmapObject> list = new List<BeatmapObject>();
			this._beatDict[beat] = list;
			return list;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00053308 File Offset: 0x00051508
		private BeatmapObject GetBeatmapObject(List<BeatmapObject> objectList, BeatmapObjectPosition position)
		{
			if (objectList == null)
			{
				Debug.LogError(string.Format("Tried to get BeatmapObject from null BeatmapObjectList at position {0}.", position));
				return null;
			}
			foreach (BeatmapObject beatmapObject in objectList)
			{
				if (beatmapObject.position.Equals(position))
				{
					if (this._verboseLog)
					{
						Debug.Log("Found BeatmapObject at " + position);
					}
					return beatmapObject;
				}
			}
			return null;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00053394 File Offset: 0x00051594
		public BeatmapObject GetBeatmapObjectAtPosition(BeatmapObjectPosition position)
		{
			List<BeatmapObject> beatmapObjectList = this.GetBeatmapObjectList(position.beatIndex.beat);
			if (beatmapObjectList == null)
			{
				return null;
			}
			BeatmapObject beatmapObject = this.GetBeatmapObject(beatmapObjectList, position);
			if (this._verboseLog && beatmapObject == null)
			{
				Debug.Log("BeatmapObject not found. " + position);
			}
			return beatmapObject;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		public void RemoveBeatmapObjectAtPosition(BeatmapObjectPosition position)
		{
			this.SetBeatmapObject(null, position);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00010ECA File Offset: 0x0000F0CA
		public void SetBeatmapObject(BeatmapObject beatmapObject)
		{
			this.SetBeatmapObject(beatmapObject, beatmapObject.position);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000533E0 File Offset: 0x000515E0
		private void SetBeatmapObject(BeatmapObject beatmapObject, BeatmapObjectPosition position)
		{
			int beat = position.beatIndex.beat;
			List<BeatmapObject> orAddBeatmapObjectList = this.GetOrAddBeatmapObjectList(beat);
			BeatmapObject beatmapObject2 = this.GetBeatmapObject(orAddBeatmapObjectList, position);
			if (beatmapObject2 != null)
			{
				if (this._verboseLog)
				{
					Debug.Log("RemoveBeatmapObject at " + position);
				}
				orAddBeatmapObjectList.Remove(beatmapObject2);
			}
			if (beatmapObject != null)
			{
				if (!beatmapObject.position.Equals(position))
				{
					Debug.LogError("SetBeatmapObject: BeatmapObject position and BeatmapObjectPosition doesn't match. Will use BeatmapObjectPosition.");
					beatmapObject.position = position;
				}
				if (this._verboseLog)
				{
					Debug.Log("SetBeatmapObject at " + position);
				}
				orAddBeatmapObjectList.Add(beatmapObject);
			}
		}

		// Token: 0x040016EA RID: 5866
		private bool _verboseLog;

		// Token: 0x040016EB RID: 5867
		private Dictionary<int, List<BeatmapObject>> _beatDict = new Dictionary<int, List<BeatmapObject>>();
	}
}
