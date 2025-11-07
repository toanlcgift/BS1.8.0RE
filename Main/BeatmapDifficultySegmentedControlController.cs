using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

// Token: 0x020003C2 RID: 962
public class BeatmapDifficultySegmentedControlController : MonoBehaviour
{
	// Token: 0x14000095 RID: 149
	// (add) Token: 0x060011C0 RID: 4544 RVA: 0x00042F30 File Offset: 0x00041130
	// (remove) Token: 0x060011C1 RID: 4545 RVA: 0x00042F68 File Offset: 0x00041168
	public event Action<BeatmapDifficultySegmentedControlController, BeatmapDifficulty> didSelectDifficultyEvent;

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0000D8D3 File Offset: 0x0000BAD3
	public BeatmapDifficulty selectedDifficulty
	{
		get
		{
			return this._selectedDifficulty;
		}
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0000D8DB File Offset: 0x0000BADB
	protected void Awake()
	{
		this._difficultySegmentedControl.didSelectCellEvent += this.HandleDifficultySegmentedControlDidSelectCell;
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
	protected void OnDestroy()
	{
		if (this._difficultySegmentedControl != null)
		{
			this._difficultySegmentedControl.didSelectCellEvent -= this.HandleDifficultySegmentedControlDidSelectCell;
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x00042FA0 File Offset: 0x000411A0
	private int GetClosestDifficultyIndex(BeatmapDifficulty searchDifficulty)
	{
		int num = -1;
		foreach (BeatmapDifficulty beatmapDifficulty in this._difficulties)
		{
			if (searchDifficulty < beatmapDifficulty)
			{
				break;
			}
			num++;
		}
		if (num == -1)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0000D91B File Offset: 0x0000BB1B
	private void HandleDifficultySegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellIdx)
	{
		this._selectedDifficulty = this._difficulties[cellIdx];
		Action<BeatmapDifficultySegmentedControlController, BeatmapDifficulty> action = this.didSelectDifficultyEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._selectedDifficulty);
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x00043000 File Offset: 0x00041200
	public void SetData(IDifficultyBeatmap[] difficultyBeatmaps, BeatmapDifficulty selectedDifficulty)
	{
		this._difficulties.Clear();
		List<string> list = new List<string>(difficultyBeatmaps.Length);
		foreach (IDifficultyBeatmap difficultyBeatmap in difficultyBeatmaps)
		{
			list.Add(difficultyBeatmap.difficulty.Name());
			this._difficulties.Add(difficultyBeatmap.difficulty);
		}
		this._difficultySegmentedControl.SetTexts(list.ToArray());
		int closestDifficultyIndex = this.GetClosestDifficultyIndex(selectedDifficulty);
		if (closestDifficultyIndex != -1)
		{
			this._difficultySegmentedControl.SelectCellWithNumber(closestDifficultyIndex);
		}
		this._selectedDifficulty = this._difficulties[this._difficultySegmentedControl.selectedCellNumber];
	}

	// Token: 0x0400117E RID: 4478
	[SerializeField]
	private TextSegmentedControl _difficultySegmentedControl;

	// Token: 0x04001180 RID: 4480
	private List<BeatmapDifficulty> _difficulties = new List<BeatmapDifficulty>(5);

	// Token: 0x04001181 RID: 4481
	private BeatmapDifficulty _selectedDifficulty;
}
