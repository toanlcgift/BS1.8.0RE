using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000557 RID: 1367
	public class CopyAndPastInfoPopups : MonoBehaviour
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x000139C9 File Offset: 0x00011BC9
		protected void Start()
		{
			this._copyAndPasteController.dataCopiedEvent += this.HandleCopyAndPasteControllerDataCopied;
			this._copyAndPasteController.dataPastedEvent += this.HandleCopyAndPasteControllerDataPasted;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000139F9 File Offset: 0x00011BF9
		protected void OnDestroy()
		{
			if (this._copyAndPasteController != null)
			{
				this._copyAndPasteController.dataCopiedEvent -= this.HandleCopyAndPasteControllerDataCopied;
				this._copyAndPasteController.dataPastedEvent -= this.HandleCopyAndPasteControllerDataPasted;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00013A37 File Offset: 0x00011C37
		private void HandleCopyAndPasteControllerDataCopied(int numberOfBars)
		{
			if (numberOfBars > 0)
			{
				this._popupInfoPanelController.ShowInfo(numberOfBars + " bars copied", EditorPopUpInfoPanelController.InfoType.Info);
				return;
			}
			this._popupInfoPanelController.ShowInfo("Nothing was copied. No columns are selected or there is no data.", EditorPopUpInfoPanelController.InfoType.Warning);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00013A6B File Offset: 0x00011C6B
		private void HandleCopyAndPasteControllerDataPasted(bool success, string errorMessage)
		{
			if (!success && !string.IsNullOrEmpty(errorMessage))
			{
				this._popupInfoPanelController.ShowInfo(errorMessage, EditorPopUpInfoPanelController.InfoType.Warning);
			}
		}

		// Token: 0x04001968 RID: 6504
		[SerializeField]
		private CopyAndPasteController _copyAndPasteController;

		// Token: 0x04001969 RID: 6505
		[SerializeField]
		private EditorPopUpInfoPanelController _popupInfoPanelController;
	}
}
