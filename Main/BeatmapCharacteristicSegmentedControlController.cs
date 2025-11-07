using System;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using UnityEngine;

// Token: 0x020003BE RID: 958
public class BeatmapCharacteristicSegmentedControlController : MonoBehaviour
{
	// Token: 0x14000092 RID: 146
	// (add) Token: 0x060011A4 RID: 4516 RVA: 0x00042C28 File Offset: 0x00040E28
	// (remove) Token: 0x060011A5 RID: 4517 RVA: 0x00042C60 File Offset: 0x00040E60
	public event Action<BeatmapCharacteristicSegmentedControlController, BeatmapCharacteristicSO> didSelectBeatmapCharacteristicEvent;

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0000D724 File Offset: 0x0000B924
	public BeatmapCharacteristicSO selectedBeatmapCharacteristic
	{
		get
		{
			return this._selectedBeatmapCharacteristic;
		}
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0000D72C File Offset: 0x0000B92C
	protected void Awake()
	{
		this._segmentedControl.didSelectCellEvent += this.HandleDifficultySegmentedControlDidSelectCell;
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x0000D745 File Offset: 0x0000B945
	protected void OnDestroy()
	{
		if (this._segmentedControl != null)
		{
			this._segmentedControl.didSelectCellEvent -= this.HandleDifficultySegmentedControlDidSelectCell;
		}
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x00042C98 File Offset: 0x00040E98
	public void SetData(IDifficultyBeatmapSet[] difficultyBeatmapSets, BeatmapCharacteristicSO selectedBeatmapCharacteristic)
	{
		this._beatmapCharacteristics.Clear();
		List<IDifficultyBeatmapSet> list = new List<IDifficultyBeatmapSet>(difficultyBeatmapSets);
		list.Sort((IDifficultyBeatmapSet a, IDifficultyBeatmapSet b) => a.beatmapCharacteristic.sortingOrder.CompareTo(b.beatmapCharacteristic.sortingOrder));
		difficultyBeatmapSets = list.ToArray();
		IconSegmentedControl.DataItem[] array = new IconSegmentedControl.DataItem[difficultyBeatmapSets.Length];
		int num = 0;
		for (int i = 0; i < difficultyBeatmapSets.Length; i++)
		{
			BeatmapCharacteristicSO beatmapCharacteristic = difficultyBeatmapSets[i].beatmapCharacteristic;
			array[i] = new IconSegmentedControl.DataItem(beatmapCharacteristic.icon, Localization.Get(beatmapCharacteristic.descriptionLocalizationKey));
			this._beatmapCharacteristics.Add(beatmapCharacteristic);
			if (beatmapCharacteristic == selectedBeatmapCharacteristic)
			{
				num = i;
			}
		}
		this._segmentedControl.SetData(array);
		this._segmentedControl.SelectCellWithNumber(num);
		this._selectedBeatmapCharacteristic = this._beatmapCharacteristics[num];
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0000D76C File Offset: 0x0000B96C
	private void HandleDifficultySegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellIdx)
	{
		this._selectedBeatmapCharacteristic = this._beatmapCharacteristics[cellIdx];
		Action<BeatmapCharacteristicSegmentedControlController, BeatmapCharacteristicSO> action = this.didSelectBeatmapCharacteristicEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._selectedBeatmapCharacteristic);
	}

	// Token: 0x04001170 RID: 4464
	[SerializeField]
	private IconSegmentedControl _segmentedControl;

	// Token: 0x04001172 RID: 4466
	private BeatmapCharacteristicSO _selectedBeatmapCharacteristic;

	// Token: 0x04001173 RID: 4467
	private List<BeatmapCharacteristicSO> _beatmapCharacteristics = new List<BeatmapCharacteristicSO>(5);
}
