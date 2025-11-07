using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000562 RID: 1378
	public class EventsHeader : MonoBehaviour
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00013D5D File Offset: 0x00011F5D
		protected void Start()
		{
			this.RefreshUI(this._plus16Toggle.isOn);
			this._toggleBinder = new ToggleBinder();
			this._toggleBinder.AddBinding(this._plus16Toggle, delegate(bool on)
			{
				this.RefreshUI(on);
			});
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00013D98 File Offset: 0x00011F98
		protected void OnDestroy()
		{
			ToggleBinder toggleBinder = this._toggleBinder;
			if (toggleBinder == null)
			{
				return;
			}
			toggleBinder.ClearBindings();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005D274 File Offset: 0x0005B474
		private void RefreshUI(bool plus16)
		{
			for (int i = 0; i < this._eventHeaderCells.Length; i++)
			{
				EventSetDrawStyleSO.SpecificEventDrawStyle specificEventDrawStyle = this._eventSetDrawStyle.specificEvents[i + (plus16 ? 16 : 0)];
				EventHeaderCell eventHeaderCell = this._eventHeaderCells[i];
				if (specificEventDrawStyle.eventDrawStyle == null)
				{
					eventHeaderCell.gameObject.SetActive(false);
				}
				else
				{
					eventHeaderCell.gameObject.SetActive(true);
					eventHeaderCell.image.sprite = ((specificEventDrawStyle.overrideImage == null) ? specificEventDrawStyle.eventDrawStyle.image : specificEventDrawStyle.overrideImage);
					eventHeaderCell.hoverHint.text = (string.IsNullOrEmpty(specificEventDrawStyle.hintText) ? specificEventDrawStyle.eventDrawStyle.hintText : specificEventDrawStyle.hintText);
				}
			}
		}

		// Token: 0x040019A4 RID: 6564
		[SerializeField]
		private EventSetDrawStyleSO _eventSetDrawStyle;

		// Token: 0x040019A5 RID: 6565
		[SerializeField]
		private EventHeaderCell[] _eventHeaderCells;

		// Token: 0x040019A6 RID: 6566
		[SerializeField]
		private Toggle _plus16Toggle;

		// Token: 0x040019A7 RID: 6567
		private ToggleBinder _toggleBinder;
	}
}
