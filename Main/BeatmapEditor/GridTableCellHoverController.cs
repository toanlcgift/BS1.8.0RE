using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x0200056F RID: 1391
	public class GridTableCellHoverController : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x00014007 File Offset: 0x00012207
		protected void Awake()
		{
			this._hover = UnityEngine.Object.Instantiate<GridTableCellHover>(this._hoverPrefab, base.transform);
			this._hover.visible = false;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0001402C File Offset: 0x0001222C
		public void OnPointerEnter(PointerEventData eventData)
		{
			this._hover.visible = true;
			base.StartCoroutine(this.UpdateHoverPositionCoroutine());
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00014047 File Offset: 0x00012247
		public void OnPointerExit(PointerEventData eventData)
		{
			this._hover.visible = false;
			base.StopAllCoroutines();
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0001405B File Offset: 0x0001225B
		private IEnumerator UpdateHoverPositionCoroutine()
		{
			for (;;)
			{
				Vector3 worldPos;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(base.transform as RectTransform, Input.mousePosition, this._canvasCamera, out worldPos);
				this._hover.position = this._gridTable.GridCellBottomLeftPosforWorldPos(worldPos);
				yield return null;
			}
			yield break;
		}

		// Token: 0x040019D5 RID: 6613
		[SerializeField]
		private GridTable _gridTable;

		// Token: 0x040019D6 RID: 6614
		[SerializeField]
		private GridTableCellHover _hoverPrefab;

		// Token: 0x040019D7 RID: 6615
		[Inject]
		private Camera _canvasCamera;

		// Token: 0x040019D8 RID: 6616
		private GridTableCellHover _hover;
	}
}
