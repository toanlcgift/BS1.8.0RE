using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004FD RID: 1277
	public class UIDragAndDrop : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x0600180C RID: 6156 RVA: 0x00011BE6 File Offset: 0x0000FDE6
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this._originalParent == null)
			{
				this._originalParent = base.transform.parent;
			}
			this._originalBlocksRaycasts = this._canvasGroup.blocksRaycasts;
			this._canvasGroup.blocksRaycasts = false;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00011C24 File Offset: 0x0000FE24
		public void OnDrag(PointerEventData eventData)
		{
			((RectTransform)base.transform).anchoredPosition += eventData.delta;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00055C54 File Offset: 0x00053E54
		public void OnEndDrag(PointerEventData eventData)
		{
			this._canvasGroup.blocksRaycasts = this._originalBlocksRaycasts;
			if (this._snapToGrid)
			{
				this._uiSnapToGrid.SnapToGrid((RectTransform)base.transform, this._grid);
			}
			if (this._canChangeParent)
			{
				GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
				if (gameObject == null)
				{
					base.transform.SetParent(this._originalParent);
				}
				else
				{
					base.transform.SetParent(gameObject.transform);
				}
			}
			if (this._snapToParentBorder)
			{
				this._uiSnapToBorder.SnapToParentBorder((RectTransform)base.transform);
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00011C47 File Offset: 0x0000FE47
		private float RoundToNearest(float n, float multiple)
		{
			return Mathf.Round(n / multiple) * multiple;
		}

		// Token: 0x040017AF RID: 6063
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040017B0 RID: 6064
		[SerializeField]
		private bool _canChangeParent;

		// Token: 0x040017B1 RID: 6065
		[SerializeField]
		private bool _snapToParentBorder = true;

		// Token: 0x040017B2 RID: 6066
		[SerializeField]
		private bool _snapToGrid = true;

		// Token: 0x040017B3 RID: 6067
		[SerializeField]
		private Vector2 _grid = new Vector3(10f, 10f);

		// Token: 0x040017B4 RID: 6068
		[Inject]
		private IUISnapToGrid _uiSnapToGrid;

		// Token: 0x040017B5 RID: 6069
		[Inject]
		private IUISnapToBorder _uiSnapToBorder;

		// Token: 0x040017B6 RID: 6070
		private Transform _originalParent;

		// Token: 0x040017B7 RID: 6071
		private bool _originalBlocksRaycasts;
	}
}
