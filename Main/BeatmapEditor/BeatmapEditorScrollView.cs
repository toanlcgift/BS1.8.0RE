using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000552 RID: 1362
	public class BeatmapEditorScrollView : MonoBehaviour, IEndDragHandler, IEventSystemHandler, IBeginDragHandler, IPlayheadBeatIndex
	{
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06001A48 RID: 6728 RVA: 0x0005BA64 File Offset: 0x00059C64
		// (remove) Token: 0x06001A49 RID: 6729 RVA: 0x0005BA9C File Offset: 0x00059C9C
		public event Action scrollPosDidChangeEvent;

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06001A4A RID: 6730 RVA: 0x0005BAD4 File Offset: 0x00059CD4
		// (remove) Token: 0x06001A4B RID: 6731 RVA: 0x0005BB0C File Offset: 0x00059D0C
		public event Action scrollDragDidBeginEvent;

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06001A4C RID: 6732 RVA: 0x0005BB44 File Offset: 0x00059D44
		// (remove) Token: 0x06001A4D RID: 6733 RVA: 0x0005BB7C File Offset: 0x00059D7C
		public event Action scrollDragDidEndEvent;

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00013733 File Offset: 0x00011933
		public ScrollRect scrollRect
		{
			get
			{
				return this._scrollRect;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0001373B File Offset: 0x0001193B
		public float rowHeight
		{
			get
			{
				return this._rowHeight;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00013743 File Offset: 0x00011943
		public float playHeadPointsOffset
		{
			get
			{
				return this._playHeadPointsOffset;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00013774 File Offset: 0x00011974
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x0001374B File Offset: 0x0001194B
		public float contentHeight
		{
			get
			{
				return this._scrollRect.content.sizeDelta.y - this.playHeadPointsOffset;
			}
			private set
			{
				this._scrollRect.content.sizeDelta = new Vector3(0f, value + this.playHeadPointsOffset);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00013792 File Offset: 0x00011992
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x0001379A File Offset: 0x0001199A
		public float visibleAreaTimeDuration { get; private set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x000137A3 File Offset: 0x000119A3
		// (set) Token: 0x06001A56 RID: 6742 RVA: 0x000137AB File Offset: 0x000119AB
		public float playHeadSongTimeOffset { get; private set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x000137B4 File Offset: 0x000119B4
		public int playheadBeatIndex
		{
			get
			{
				return (int)(this.scrollPositionSongTime / this._beatDuration + 0.5f);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000137CA File Offset: 0x000119CA
		public float scrollPositionSongTime
		{
			get
			{
				return this.scrollRect.verticalNormalizedPosition * this._songDuration;
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		protected void Awake()
		{
			this.contentHeight = 20000f;
			this._scrollRect.verticalNormalizedPosition = 0f;
			this._playHead.anchorMax = new Vector2(1f, 0f);
			this._playHead.anchorMin = new Vector2(0f, 0f);
			this._playHead.anchoredPosition = new Vector2(0f, this._playHeadPointsOffset);
			this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.ScrollViewDidScroll));
			this.ChangeParams(60f, 4, 600f);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000137DE File Offset: 0x000119DE
		protected void OnDestroy()
		{
			PlayerPrefs.SetFloat("BeatmapEditor.BpmBeatPosition", this._lastUsedBpmBeatPosition);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0005BC58 File Offset: 0x00059E58
		public void ChangeParams(float songBPM, int beatsPerBPMBeat, float songDuration)
		{
			float num = (this._bpmBeatDuration > 0f) ? (this.scrollPositionSongTime / this._bpmBeatDuration) : 0f;
			this._songDuration = songDuration;
			this._bpmBeatDuration = 60f / songBPM;
			this._beatDuration = this._bpmBeatDuration / (float)beatsPerBPMBeat;
			this.contentHeight = songDuration / this._beatDuration * this._rowHeight + this._scrollRect.viewport.rect.height - this._rowHeight * 8f;
			this.visibleAreaTimeDuration = this._scrollRect.viewport.rect.height / this._rowHeight * this._beatDuration;
			this.playHeadSongTimeOffset = this._playHeadPointsOffset / this._rowHeight * this._beatDuration;
			this.SetPositionToSongTime(num * this._bpmBeatDuration, false);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0005BD3C File Offset: 0x00059F3C
		public void MoveToSavedBeatPosition()
		{
			float @float = PlayerPrefs.GetFloat("BeatmapEditor.BpmBeatPosition", 0f);
			this.SetPositionToSongTime(@float * this._bpmBeatDuration, false);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000137F0 File Offset: 0x000119F0
		public void SetPositionToBPMBeatPosition(float bpmBeatPosition)
		{
			this.SetPositionToSongTime(bpmBeatPosition * this._bpmBeatDuration, false);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0005BD68 File Offset: 0x00059F68
		public void SetPositionToSongTime(float songTime, bool snapToBeat = false)
		{
			if (snapToBeat)
			{
				songTime = (float)Mathf.RoundToInt(songTime / this._beatDuration) * this._beatDuration;
			}
			float verticalNormalizedPosition = Mathf.Min(Mathf.Max(0f, songTime / this._songDuration), 1f);
			this._scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
			this._lastUsedBpmBeatPosition = this.scrollPositionSongTime / this._bpmBeatDuration;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00013801 File Offset: 0x00011A01
		public void SnapPositionToBeat()
		{
			this.SetPositionToSongTime(this.scrollPositionSongTime, true);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00013810 File Offset: 0x00011A10
		private void ScrollViewDidScroll(Vector2 normalizedPos)
		{
			this._lastUsedBpmBeatPosition = this.scrollPositionSongTime / this._bpmBeatDuration;
			Action action = this.scrollPosDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00013835 File Offset: 0x00011A35
		public void OnBeginDrag(PointerEventData eventData)
		{
			Action action = this.scrollDragDidBeginEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00013847 File Offset: 0x00011A47
		public void OnEndDrag(PointerEventData eventData)
		{
			this.SnapPositionToBeat();
			Action action = this.scrollDragDidEndEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0005BDCC File Offset: 0x00059FCC
		public int GetRowForWorldPos(Vector3 worldPos)
		{
			Vector3 vector = this._scrollRect.content.transform.InverseTransformPoint(worldPos);
			int num = Mathf.FloorToInt((this.contentHeight + vector.y) / this.rowHeight);
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0001385F File Offset: 0x00011A5F
		public float GetRowWorldPos(int row)
		{
			return this._scrollRect.content.TransformPoint(new Vector2(0f, (float)row * this.rowHeight - this.contentHeight)).y;
		}

		// Token: 0x04001946 RID: 6470
		[SerializeField]
		private ScrollRect _scrollRect;

		// Token: 0x04001947 RID: 6471
		[SerializeField]
		private RectTransform _playHead;

		// Token: 0x04001948 RID: 6472
		[Space]
		[SerializeField]
		private float _rowHeight = 20f;

		// Token: 0x04001949 RID: 6473
		[SerializeField]
		private float _playHeadPointsOffset = 160f;

		// Token: 0x0400194F RID: 6479
		private const string kSavedBpmBeatPosition = "BeatmapEditor.BpmBeatPosition";

		// Token: 0x04001950 RID: 6480
		private float _songDuration;

		// Token: 0x04001951 RID: 6481
		private float _bpmBeatDuration;

		// Token: 0x04001952 RID: 6482
		private float _beatDuration;

		// Token: 0x04001953 RID: 6483
		private float _lastUsedBpmBeatPosition;
	}
}
