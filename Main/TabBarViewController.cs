using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000419 RID: 1049
public class TabBarViewController : ViewController
{
	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0000EE07 File Offset: 0x0000D007
	// (set) Token: 0x060013D7 RID: 5079 RVA: 0x0000EDF9 File Offset: 0x0000CFF9
	public bool sizeToFit
	{
		get
		{
			return this._contentSizeFilter.enabled;
		}
		set
		{
			this._contentSizeFilter.enabled = value;
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0000EE14 File Offset: 0x0000D014
	public int selectedCellNumber
	{
		get
		{
			return this._segmentedControll.selectedCellNumber;
		}
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x00049A44 File Offset: 0x00047C44
	public void Setup(TabBarViewController.TabBarItem[] items)
	{
		this._items = items;
		List<string> list = new List<string>(this._items.Length);
		foreach (TabBarViewController.TabBarItem tabBarItem in this._items)
		{
			list.Add(tabBarItem.title);
		}
		this._labels = list.ToArray();
		if (base.isActiveAndEnabled)
		{
			this._segmentedControll.SetTexts(this._labels);
			this._shouldReloadData = false;
			return;
		}
		this._shouldReloadData = true;
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x0000EE21 File Offset: 0x0000D021
	public void SelectItem(int index)
	{
		this._segmentedControll.SelectCellWithNumber(index);
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x0000EE2F File Offset: 0x0000D02F
	public void Clear()
	{
		this.Setup(new TabBarViewController.TabBarItem[0]);
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x00049AC0 File Offset: 0x00047CC0
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._segmentedControll.didSelectCellEvent += this.HandleDidSelectCell;
		}
		if (this._labels != null && this._shouldReloadData)
		{
			this._shouldReloadData = false;
			this._segmentedControll.SetTexts(this._labels);
		}
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x0000EE3D File Offset: 0x0000D03D
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._segmentedControll)
		{
			this._segmentedControll.didSelectCellEvent -= this.HandleDidSelectCell;
		}
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x0000EE69 File Offset: 0x0000D069
	private void HandleDidSelectCell(SegmentedControl segmentedControl, int cellNumber)
	{
		Action action = this._items[cellNumber].action;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x040013A2 RID: 5026
	[SerializeField]
	private TextSegmentedControl _segmentedControll;

	// Token: 0x040013A3 RID: 5027
	[SerializeField]
	private ContentSizeFitter _contentSizeFilter;

	// Token: 0x040013A4 RID: 5028
	private string[] _labels;

	// Token: 0x040013A5 RID: 5029
	private TabBarViewController.TabBarItem[] _items;

	// Token: 0x040013A6 RID: 5030
	private bool _shouldReloadData;

	// Token: 0x0200041A RID: 1050
	public class TabBarItem
	{
		// Token: 0x060013E1 RID: 5089 RVA: 0x0000EE82 File Offset: 0x0000D082
		public TabBarItem(string title, Action action)
		{
			this.title = title;
			this.action = action;
		}

		// Token: 0x040013A7 RID: 5031
		public readonly string title;

		// Token: 0x040013A8 RID: 5032
		public readonly Action action;
	}
}
