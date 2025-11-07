using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200056B RID: 1387
	public class EventsTable : GridTable
	{
		// Token: 0x170004F2 RID: 1266
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00013F5D File Offset: 0x0001215D
		public bool plus16
		{
			set
			{
				this._plus16 = value;
				base.UpdateAllCells();
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00013F6C File Offset: 0x0001216C
		protected override int numberOfColumns
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00013F70 File Offset: 0x00012170
		protected override float columnWidth
		{
			get
			{
				return this._columnWidth;
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0005DB1C File Offset: 0x0005BD1C
		public void Init(EditorBeatmapSO editorBeatmap, EventSetDrawStyleSO eventSetDrawStyle, bool plus16)
		{
			this._editorBeatmap = editorBeatmap;
			this._eventSetDrawStyle = eventSetDrawStyle;
			this._plus16 = plus16;
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00013F78 File Offset: 0x00012178
		protected override void Awake()
		{
			base.Awake();
			this._columnWidth = this._eventsTableCellPrefab.GetComponent<RectTransform>().sizeDelta.x / (float)this.numberOfColumns;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00013FA3 File Offset: 0x000121A3
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00013F4D File Offset: 0x0001214D
		private void HandleEditorBeatmapDidChangeAllData()
		{
			base.UpdateAllCells();
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0005DB6C File Offset: 0x0005BD6C
		protected override BeatmapEditorTableCell CellForRow(int row)
		{
			BeatData beatData = null;
			bool flag = false;
			if (row < this._editorBeatmap.beatsDataLength)
			{
				beatData = this._editorBeatmap.GetBeatData(row);
			}
			else if (this._editorBeatmap.beatsDataLength > 0)
			{
				beatData = this._editorBeatmap.GetBeatData(this._editorBeatmap.beatsDataLength - 1);
				flag = true;
			}
			if (beatData == null)
			{
				return null;
			}
			bool flag2 = true;
			EditorEventData[] array = null;
			if (beatData != null)
			{
				array = beatData.eventsData;
				for (int i = 0; i < this.numberOfColumns; i++)
				{
					if (array[i + (this._plus16 ? 16 : 0)] != null)
					{
						flag2 = false;
						break;
					}
				}
			}
			if (flag2)
			{
				return null;
			}
			EventsTableCell eventsTableCell = (EventsTableCell)base.DequeueReusableCell("Cell");
			if (eventsTableCell == null)
			{
				eventsTableCell = UnityEngine.Object.Instantiate<EventsTableCell>(this._eventsTableCellPrefab);
				eventsTableCell.reuseIdentifier = "Cell";
			}
			for (int j = 0; j < this.numberOfColumns; j++)
			{
				int num = j + (this._plus16 ? 16 : 0);
				int num2 = 0;
				Color color = Color.clear;
				Sprite image = null;
				if (array[num] != null)
				{
					EditorEventData editorEventData = array[num];
					num2 = editorEventData.value;
					if (this._eventSetDrawStyle.specificEvents[num] != null)
					{
						foreach (EventDrawStyleSO.SubEventDrawStyle subEventDrawStyle in this._eventSetDrawStyle.specificEvents[num].eventDrawStyle.subEvents)
						{
							if (subEventDrawStyle.eventValue == num2)
							{
								if (editorEventData.isPreviousValidValue || flag)
								{
									color = subEventDrawStyle.eventActiveColor;
								}
								else
								{
									color = subEventDrawStyle.color;
									image = subEventDrawStyle.image;
								}
							}
						}
					}
				}
				bool flag3 = array[num] != null;
				string text = "";
				if (flag3 && !array[num].isPreviousValidValue && !flag)
				{
					text = num2.ToString();
				}
				eventsTableCell.SetLineActive(j, flag3, color, image, text);
			}
			string text2 = null;
			Color color2 = Color.white;
			if ((array[14] != null && !array[14].isPreviousValidValue) || (array[15] != null && !array[15].isPreviousValidValue))
			{
				int num3 = Mathf.RoundToInt(this.FinalRotationAtRow(row));
				num3 %= 360;
				if (num3 > 180)
				{
					num3 -= 360;
				}
				else if (num3 < -179)
				{
					num3 += 360;
				}
				text2 = num3.ToString();
				if (num3 < -45 || num3 > 45)
				{
					color2 = Color.red;
				}
			}
			eventsTableCell.SetDescriptionText(text2, color2);
			return eventsTableCell;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0005DDD8 File Offset: 0x0005BFD8
		private float FinalRotationAtRow(int targetRow)
		{
			float num = 0f;
			int num2 = 0;
			while (num2 < targetRow + 1 && num2 < this._editorBeatmap.beatsDataLength)
			{
				EditorEventData[] eventsData = this._editorBeatmap.GetBeatData(num2).eventsData;
				int num3 = this._plus16 ? 16 : 0;
				num += this.RotationForEventDataAtIndex(eventsData, num3 + 14);
				num += this.RotationForEventDataAtIndex(eventsData, num3 + 15);
				num2++;
			}
			return num;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0005DE44 File Offset: 0x0005C044
		private float RotationForEventDataAtIndex(EditorEventData[] eventsData, int eventIndex)
		{
			if (eventIndex >= eventsData.Length)
			{
				return 0f;
			}
			EditorEventData editorEventData = eventsData[eventIndex];
			if (editorEventData == null)
			{
				return 0f;
			}
			if (editorEventData.isPreviousValidValue)
			{
				return 0f;
			}
			return this._rotationProcessor.RotationForEventValue(editorEventData.value);
		}

		// Token: 0x040019CA RID: 6602
		[SerializeField]
		private EventsTableCell _eventsTableCellPrefab;

		// Token: 0x040019CB RID: 6603
		private const string kCellIdentifier = "Cell";

		// Token: 0x040019CC RID: 6604
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019CD RID: 6605
		private EventSetDrawStyleSO _eventSetDrawStyle;

		// Token: 0x040019CE RID: 6606
		private float _columnWidth;

		// Token: 0x040019CF RID: 6607
		private bool _plus16;

		// Token: 0x040019D0 RID: 6608
		private SpawnRotationProcessor _rotationProcessor = new SpawnRotationProcessor();
	}
}
