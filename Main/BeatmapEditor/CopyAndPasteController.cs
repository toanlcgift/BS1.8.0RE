using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x02000558 RID: 1368
	public class CopyAndPasteController : MonoBehaviour
	{
		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06001A8B RID: 6795 RVA: 0x0005C720 File Offset: 0x0005A920
		// (remove) Token: 0x06001A8C RID: 6796 RVA: 0x0005C758 File Offset: 0x0005A958
		public event Action<int> dataCopiedEvent;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06001A8D RID: 6797 RVA: 0x0005C790 File Offset: 0x0005A990
		// (remove) Token: 0x06001A8E RID: 6798 RVA: 0x0005C7C8 File Offset: 0x0005A9C8
		public event Action<bool, string> dataPastedEvent;

		// Token: 0x06001A8F RID: 6799 RVA: 0x0005C800 File Offset: 0x0005AA00
		protected void Awake()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._copy1BarButton,
					delegate()
					{
						this.CopyBeatSegment(1);
					}
				},
				{
					this._copy2BarsButton,
					delegate()
					{
						this.CopyBeatSegment(2);
					}
				},
				{
					this._copy4BarsButton,
					delegate()
					{
						this.CopyBeatSegment(4);
					}
				},
				{
					this._copy8BarsButton,
					delegate()
					{
						this.CopyBeatSegment(8);
					}
				},
				{
					this._copyAllBarsButton,
					delegate()
					{
						this.CopyAllBars();
					}
				},
				{
					this._pasteButton,
					delegate()
					{
						this.PasteBeatSement();
					}
				}
			});
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00013A85 File Offset: 0x00011C85
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0005C8B0 File Offset: 0x0005AAB0
		private void CopyBeatSegment(int startBeatIndex, int numberOfBeats)
		{
			bool copyBaseNotes = this._copyPasteFilter.copyBaseNotes;
			bool copyUpperNotes = this._copyPasteFilter.copyUpperNotes;
			bool copyTopNotes = this._copyPasteFilter.copyTopNotes;
			bool copyObstacles = this._copyPasteFilter.copyObstacles;
			bool copyEvents = this._copyPasteFilter.copyEvents;
			if (!copyBaseNotes && !copyUpperNotes && !copyTopNotes && !copyObstacles && !copyEvents)
			{
				if (this.dataCopiedEvent != null)
				{
					this.dataCopiedEvent(0);
				}
				return;
			}
			if (copyBaseNotes)
			{
				this._copyPasteSegmentBaseNotesData = new EditorNoteData[numberOfBeats * 4];
			}
			else
			{
				this._copyPasteSegmentBaseNotesData = null;
			}
			if (copyUpperNotes)
			{
				this._copyPasteSegmentUpperNotesData = new EditorNoteData[numberOfBeats * 4];
			}
			else
			{
				this._copyPasteSegmentUpperNotesData = null;
			}
			if (copyTopNotes)
			{
				this._copyPasteSegmentTopNotesData = new EditorNoteData[numberOfBeats * 4];
			}
			else
			{
				this._copyPasteSegmentTopNotesData = null;
			}
			if (copyObstacles)
			{
				this._copyPasteSegmentObstaclesData = new EditorObstacleData[numberOfBeats * 4];
			}
			else
			{
				this._copyPasteSegmentObstaclesData = null;
			}
			if (copyEvents)
			{
				this._copyPasteSegmentEventsData = new EditorEventData[numberOfBeats * 32];
			}
			else
			{
				this._copyPasteSegmentEventsData = null;
			}
			this._copyPasteSegmenBeatsPerBpmBeat = this._editorBeatmap.beatsData.beatsPerBpmBeat;
			for (int i = 0; i < numberOfBeats; i++)
			{
				int num = startBeatIndex + i;
				if (num < this._editorBeatmap.beatsDataLength)
				{
					if (copyBaseNotes)
					{
						for (int j = 0; j < 4; j++)
						{
							this._copyPasteSegmentBaseNotesData[i * 4 + j] = this._editorBeatmap.GetBeatData(num).baseNotesData[j];
						}
					}
					if (copyUpperNotes)
					{
						for (int k = 0; k < 4; k++)
						{
							this._copyPasteSegmentUpperNotesData[i * 4 + k] = this._editorBeatmap.GetBeatData(num).upperNotesData[k];
						}
					}
					if (copyTopNotes)
					{
						for (int l = 0; l < 4; l++)
						{
							this._copyPasteSegmentTopNotesData[i * 4 + l] = this._editorBeatmap.GetBeatData(num).topNotesData[l];
						}
					}
					if (copyObstacles)
					{
						for (int m = 0; m < 4; m++)
						{
							this._copyPasteSegmentObstaclesData[i * 4 + m] = this._editorBeatmap.GetBeatData(num).obstaclesData[m];
						}
					}
					if (copyEvents)
					{
						for (int n = 0; n < 32; n++)
						{
							this._copyPasteSegmentEventsData[i * 32 + n] = this._editorBeatmap.GetBeatData(num).eventsData[n];
						}
					}
				}
			}
			Action<int> action = this.dataCopiedEvent;
			if (action == null)
			{
				return;
			}
			action(numberOfBeats / (4 * this._editorBeatmap.beatsPerBpmBeat));
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00013A9A File Offset: 0x00011C9A
		public void CopyBeatSegment(int numberOfBars)
		{
			this.CopyBeatSegment(this._playheadBeatIndex.playheadBeatIndex, numberOfBars * 4 * this._editorBeatmap.beatsPerBpmBeat);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00013ABC File Offset: 0x00011CBC
		public void CopyAllBars()
		{
			this.CopyBeatSegment(this._playheadBeatIndex.playheadBeatIndex, this._editorBeatmap.beatsDataLength);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0005CB14 File Offset: 0x0005AD14
		public void PasteBeatSement()
		{
			string arg = null;
			bool arg2 = this._editorBeatmap.PasteBeatSegments(this._playheadBeatIndex.playheadBeatIndex, this._copyPasteSegmentBaseNotesData, this._copyPasteSegmentUpperNotesData, this._copyPasteSegmentTopNotesData, this._copyPasteSegmentObstaclesData, this._copyPasteSegmentEventsData, this._copyPasteSegmenBeatsPerBpmBeat, out arg);
			if (this.dataPastedEvent != null)
			{
				this.dataPastedEvent(arg2, arg);
			}
		}

		// Token: 0x0400196A RID: 6506
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x0400196B RID: 6507
		[SerializeField]
		private EditorCopyPasteFilterSO _copyPasteFilter;

		// Token: 0x0400196C RID: 6508
		[SerializeField]
		private Button _copy1BarButton;

		// Token: 0x0400196D RID: 6509
		[SerializeField]
		private Button _copy2BarsButton;

		// Token: 0x0400196E RID: 6510
		[SerializeField]
		private Button _copy4BarsButton;

		// Token: 0x0400196F RID: 6511
		[SerializeField]
		private Button _copy8BarsButton;

		// Token: 0x04001970 RID: 6512
		[SerializeField]
		private Button _copyAllBarsButton;

		// Token: 0x04001971 RID: 6513
		[SerializeField]
		private Button _pasteButton;

		// Token: 0x04001972 RID: 6514
		[Inject]
		private IPlayheadBeatIndex _playheadBeatIndex;

		// Token: 0x04001975 RID: 6517
		private EditorNoteData[] _copyPasteSegmentBaseNotesData;

		// Token: 0x04001976 RID: 6518
		private EditorNoteData[] _copyPasteSegmentUpperNotesData;

		// Token: 0x04001977 RID: 6519
		private EditorNoteData[] _copyPasteSegmentTopNotesData;

		// Token: 0x04001978 RID: 6520
		private EditorObstacleData[] _copyPasteSegmentObstaclesData;

		// Token: 0x04001979 RID: 6521
		private EditorEventData[] _copyPasteSegmentEventsData;

		// Token: 0x0400197A RID: 6522
		private int _copyPasteSegmenBeatsPerBpmBeat;

		// Token: 0x0400197B RID: 6523
		private ButtonBinder _buttonBinder;
	}
}
