using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200058E RID: 1422
	public class SectionTogglesController : MonoBehaviour
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x0005F428 File Offset: 0x0005D628
		protected void Awake()
		{
			this._toggleBinder = new ToggleBinder(new List<Tuple<Toggle, Action<bool>>>
			{
				{
					this._eventsColumnToggle,
					delegate(bool on)
					{
						this._copyPasteFilter.copyEvents = on;
					}
				},
				{
					this._baseNotesColumnToggle,
					delegate(bool on)
					{
						this._copyPasteFilter.copyBaseNotes = on;
					}
				},
				{
					this._upperNotesColumnToggle,
					delegate(bool on)
					{
						this._copyPasteFilter.copyUpperNotes = on;
					}
				},
				{
					this._topNotesColumnToggle,
					delegate(bool on)
					{
						this._copyPasteFilter.copyTopNotes = on;
					}
				},
				{
					this._obstaclesColumnToggle,
					delegate(bool on)
					{
						this._copyPasteFilter.copyObstacles = on;
					}
				}
			});
			this._eventsColumnToggle.isOn = this._copyPasteFilter.copyEvents;
			this._baseNotesColumnToggle.isOn = this._copyPasteFilter.copyBaseNotes;
			this._upperNotesColumnToggle.isOn = this._copyPasteFilter.copyUpperNotes;
			this._topNotesColumnToggle.isOn = this._copyPasteFilter.copyTopNotes;
			this._obstaclesColumnToggle.isOn = this._copyPasteFilter.copyObstacles;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0001490B File Offset: 0x00012B0B
		protected void OnDestroy()
		{
			if (this._toggleBinder != null)
			{
				this._toggleBinder.ClearBindings();
			}
		}

		// Token: 0x04001A52 RID: 6738
		[SerializeField]
		private EditorCopyPasteFilterSO _copyPasteFilter;

		// Token: 0x04001A53 RID: 6739
		[SerializeField]
		private Toggle _eventsColumnToggle;

		// Token: 0x04001A54 RID: 6740
		[SerializeField]
		private Toggle _baseNotesColumnToggle;

		// Token: 0x04001A55 RID: 6741
		[SerializeField]
		private Toggle _upperNotesColumnToggle;

		// Token: 0x04001A56 RID: 6742
		[SerializeField]
		private Toggle _topNotesColumnToggle;

		// Token: 0x04001A57 RID: 6743
		[SerializeField]
		private Toggle _obstaclesColumnToggle;

		// Token: 0x04001A58 RID: 6744
		private ToggleBinder _toggleBinder;
	}
}
