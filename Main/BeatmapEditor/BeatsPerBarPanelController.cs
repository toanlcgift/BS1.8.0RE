using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000555 RID: 1365
	public class BeatsPerBarPanelController : MonoBehaviour
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x00013973 File Offset: 0x00011B73
		protected void Start()
		{
			this.RefreshUI();
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00013992 File Offset: 0x00011B92
		protected void OnDestroy()
		{
			if (this._editorBeatmap != null)
			{
				this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0005C2F4 File Offset: 0x0005A4F4
		private void RefreshUI()
		{
			this._beatSubdivisionsText.text = this._editorBeatmap.beatsPerBpmBeat.ToString();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000139B9 File Offset: 0x00011BB9
		private void HandleEditorBeatmapDidChangeAllData()
		{
			this.RefreshUI();
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0005C320 File Offset: 0x0005A520
		public void Stretch2xButtonPressed()
		{
			if (this._editorBeatmap.beatsPerBpmBeat >= 64)
			{
				return;
			}
			this._beatSubdivisionsText.text = this._editorBeatmap.beatsPerBpmBeat.ToString();
			this._editorBeatmap.Stretch(2);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0005C368 File Offset: 0x0005A568
		public void Squish2xButtonPressed(bool forced)
		{
			if (this._editorBeatmap.beatsPerBpmBeat <= 3)
			{
				return;
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				forced = true;
			}
			int num;
			if (!forced && !this._editorBeatmap.CanSquish2x(out num))
			{
				if (num >= 0)
				{
					int num2 = this._editorBeatmap.beatsPerBpmBeat * 4;
					this._popUpInfoPanelController.ShowInfo(string.Concat(new object[]
					{
						"Can't squish. Problematic beat at ",
						num / num2,
						":",
						(num % num2 + 1).ToString()
					}), EditorPopUpInfoPanelController.InfoType.Warning);
					this._beatmapEditorScrollView.SetPositionToBPMBeatPosition((float)num / (float)this._editorBeatmap.beatsPerBpmBeat);
				}
				return;
			}
			this._beatSubdivisionsText.text = this._editorBeatmap.beatsPerBpmBeat.ToString();
			this._editorBeatmap.Squish(2);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0005C44C File Offset: 0x0005A64C
		public void Stretch3xButtonPressed()
		{
			if (this._editorBeatmap.beatsPerBpmBeat >= 64)
			{
				return;
			}
			this._beatSubdivisionsText.text = this._editorBeatmap.beatsPerBpmBeat.ToString();
			this._editorBeatmap.Stretch(3);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0005C494 File Offset: 0x0005A694
		public void Squish3xButtonPressed(bool forced)
		{
			if (this._editorBeatmap.beatsPerBpmBeat <= 3 || this._editorBeatmap.beatsPerBpmBeat % 3 != 0)
			{
				return;
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				forced = true;
			}
			int num;
			if (!forced && !this._editorBeatmap.CanSquish3x(out num))
			{
				if (num >= 0)
				{
					int num2 = this._editorBeatmap.beatsPerBpmBeat * 4;
					this._popUpInfoPanelController.ShowInfo(string.Concat(new object[]
					{
						"Can't squish. Problematic beat at ",
						num / num2,
						":",
						(num % num2 + 1).ToString()
					}), EditorPopUpInfoPanelController.InfoType.Warning);
					this._beatmapEditorScrollView.SetPositionToBPMBeatPosition((float)num / (float)this._editorBeatmap.beatsPerBpmBeat);
				}
				return;
			}
			this._beatSubdivisionsText.text = this._editorBeatmap.beatsPerBpmBeat.ToString();
			this._editorBeatmap.Squish(3);
		}

		// Token: 0x04001961 RID: 6497
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001962 RID: 6498
		[SerializeField]
		private BeatmapEditorScrollView _beatmapEditorScrollView;

		// Token: 0x04001963 RID: 6499
		[SerializeField]
		private EditorPopUpInfoPanelController _popUpInfoPanelController;

		// Token: 0x04001964 RID: 6500
		[SerializeField]
		private Text _beatSubdivisionsText;
	}
}
