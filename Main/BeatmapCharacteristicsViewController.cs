using System;
using HMUI;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class BeatmapCharacteristicsViewController : ViewController
{
	// Token: 0x14000094 RID: 148
	// (add) Token: 0x060011B7 RID: 4535 RVA: 0x00042E74 File Offset: 0x00041074
	// (remove) Token: 0x060011B8 RID: 4536 RVA: 0x00042EAC File Offset: 0x000410AC
	public event Action<BeatmapCharacteristicSO> didSelectBeatmapCharacteristicEvent;

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0000D81C File Offset: 0x0000BA1C
	public BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection
	{
		get
		{
			return this._beatmapCharacteristicCollection;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x060011BA RID: 4538 RVA: 0x0000D824 File Offset: 0x0000BA24
	public BeatmapCharacteristicSO selectedBeatmapCharacteristic
	{
		get
		{
			if (!(this._beatmapCharacteristicCollection != null) || this._beatmapCharacteristicCollection.beatmapCharacteristics == null)
			{
				return null;
			}
			return this._beatmapCharacteristicCollection.beatmapCharacteristics[this._selectedBeatmapCharacteristicNum];
		}
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0000D855 File Offset: 0x0000BA55
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._beatmapCharacteristicsTableView.didSelectCharacteristic += this.HandleBeatmapCharacteristicsTableViewDidSelecteCharacteristic;
			this._beatmapCharacteristicsTableView.SelectCellWithIdx(this._selectedBeatmapCharacteristicNum);
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x0000D882 File Offset: 0x0000BA82
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._beatmapCharacteristicsTableView.didSelectCharacteristic -= this.HandleBeatmapCharacteristicsTableViewDidSelecteCharacteristic;
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x0000D89E File Offset: 0x0000BA9E
	public void SetData(BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection, int selectedCharacteristicNum)
	{
		this._beatmapCharacteristicCollection = beatmapCharacteristicCollection;
		this._beatmapCharacteristicsTableView.SetData(this._beatmapCharacteristicCollection);
		this._selectedBeatmapCharacteristicNum = selectedCharacteristicNum;
		if (base.isInViewControllerHierarchy)
		{
			this._beatmapCharacteristicsTableView.SelectCellWithIdx(selectedCharacteristicNum);
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x00042EE4 File Offset: 0x000410E4
	private void HandleBeatmapCharacteristicsTableViewDidSelecteCharacteristic(BeatmapCharacteristicSO beatmapCharacteristic)
	{
		BeatmapCharacteristicSO[] beatmapCharacteristics = this._beatmapCharacteristicCollection.beatmapCharacteristics;
		for (int i = 0; i < beatmapCharacteristics.Length; i++)
		{
			if (beatmapCharacteristic == beatmapCharacteristics[i])
			{
				this._selectedBeatmapCharacteristicNum = i;
				break;
			}
		}
		Action<BeatmapCharacteristicSO> action = this.didSelectBeatmapCharacteristicEvent;
		if (action == null)
		{
			return;
		}
		action(beatmapCharacteristic);
	}

	// Token: 0x0400117A RID: 4474
	[SerializeField]
	private BeatmapCharacteristicsTableView _beatmapCharacteristicsTableView;

	// Token: 0x0400117C RID: 4476
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x0400117D RID: 4477
	private int _selectedBeatmapCharacteristicNum;
}
