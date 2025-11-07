using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000402 RID: 1026
public class PromoViewController : ViewController
{
	// Token: 0x140000B1 RID: 177
	// (add) Token: 0x06001357 RID: 4951 RVA: 0x00047F74 File Offset: 0x00046174
	// (remove) Token: 0x06001358 RID: 4952 RVA: 0x00047FAC File Offset: 0x000461AC
	public event Action<PromoViewController, IAnnotatedBeatmapLevelCollection> promoButtonWasPressedEvent;

	// Token: 0x06001359 RID: 4953 RVA: 0x00047FE4 File Offset: 0x000461E4
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			PromoViewController.ButtonPromoTypePair[] elements = this._elements;
			for (int i = 0; i < elements.Length; i++)
			{
				PromoViewController.ButtonPromoTypePair item = elements[i];
				base.buttonBinder.AddBinding(item.button, delegate
				{
					Action<PromoViewController, IAnnotatedBeatmapLevelCollection> action = this.promoButtonWasPressedEvent;
					if (action == null)
					{
						return;
					}
					action(this, item.annotatedBeatmapLevelCollection);
				});
			}
		}
	}

	// Token: 0x0400130A RID: 4874
	[SerializeField]
	private PromoViewController.ButtonPromoTypePair[] _elements;

	// Token: 0x02000403 RID: 1027
	[Serializable]
	private class ButtonPromoTypePair
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x00048044 File Offset: 0x00046244
		public IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection
		{
			get
			{
				if (this._annotatedBeatmapLevelCollection == null)
				{
					if (this.previewLevelPack != null)
					{
						this._annotatedBeatmapLevelCollection = this.previewLevelPack;
					}
					if (this.levelPack != null)
					{
						this._annotatedBeatmapLevelCollection = this.levelPack;
					}
					else
					{
						this._annotatedBeatmapLevelCollection = this.filteredByBeatmapCharacteristicPlaylist;
					}
				}
				return this._annotatedBeatmapLevelCollection;
			}
		}

		// Token: 0x0400130C RID: 4876
		public Button button;

		// Token: 0x0400130D RID: 4877
		[NullAllowed]
		public PreviewBeatmapLevelPackSO previewLevelPack;

		// Token: 0x0400130E RID: 4878
		[NullAllowed]
		public BeatmapLevelPackSO levelPack;

		// Token: 0x0400130F RID: 4879
		[NullAllowed]
		public FilteredByBeatmapCharacteristicPlaylistSO filteredByBeatmapCharacteristicPlaylist;

		// Token: 0x04001310 RID: 4880
		private IAnnotatedBeatmapLevelCollection _annotatedBeatmapLevelCollection;
	}
}
