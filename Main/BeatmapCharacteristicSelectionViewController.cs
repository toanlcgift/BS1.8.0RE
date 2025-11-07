using System;
using HMUI;
using Polyglot;
using UnityEngine;

// Token: 0x020003C0 RID: 960
public class BeatmapCharacteristicSelectionViewController : ViewController
{
	// Token: 0x14000093 RID: 147
	// (add) Token: 0x060011AF RID: 4527 RVA: 0x00042D8C File Offset: 0x00040F8C
	// (remove) Token: 0x060011B0 RID: 4528 RVA: 0x00042DC4 File Offset: 0x00040FC4
	public event Action<BeatmapCharacteristicSelectionViewController, BeatmapCharacteristicSO> didSelectBeatmapCharacteristicEvent;

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
	public BeatmapCharacteristicSO selectedBeatmapCharacteristic
	{
		get
		{
			return this._selectedBeatmapCharacteristic;
		}
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x0000D7BF File Offset: 0x0000B9BF
	public void Init()
	{
		this._selectedBeatmapCharacteristic = this._beatmapCharacteristicCollection.beatmapCharacteristics[0];
		this._beatmapCharacteristicSegmentedControl.SelectCellWithNumber(0);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x00042DFC File Offset: 0x00040FFC
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			BeatmapCharacteristicSO[] beatmapCharacteristics = this._beatmapCharacteristicCollection.beatmapCharacteristics;
			IconSegmentedControl.DataItem[] array = new IconSegmentedControl.DataItem[beatmapCharacteristics.Length];
			for (int i = 0; i < beatmapCharacteristics.Length; i++)
			{
				BeatmapCharacteristicSO beatmapCharacteristicSO = beatmapCharacteristics[i];
				array[i] = new IconSegmentedControl.DataItem(beatmapCharacteristicSO.icon, Localization.Get(beatmapCharacteristicSO.descriptionLocalizationKey));
			}
			this._beatmapCharacteristicSegmentedControl.SetData(array);
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._beatmapCharacteristicSegmentedControl.didSelectCellEvent += this.HandleBeatmapCharacteristicSegmentedControlDidSelectCell;
		}
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._beatmapCharacteristicSegmentedControl.didSelectCellEvent -= this.HandleBeatmapCharacteristicSegmentedControlDidSelectCell;
		}
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x0000D7FC File Offset: 0x0000B9FC
	private void HandleBeatmapCharacteristicSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellNumber)
	{
		Action<BeatmapCharacteristicSelectionViewController, BeatmapCharacteristicSO> action = this.didSelectBeatmapCharacteristicEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._beatmapCharacteristicCollection.beatmapCharacteristics[cellNumber]);
	}

	// Token: 0x04001176 RID: 4470
	[SerializeField]
	private IconSegmentedControl _beatmapCharacteristicSegmentedControl;

	// Token: 0x04001177 RID: 4471
	[SerializeField]
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x04001179 RID: 4473
	private BeatmapCharacteristicSO _selectedBeatmapCharacteristic;
}
