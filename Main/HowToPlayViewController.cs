using System;
using HMUI;
using Polyglot;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DA RID: 986
public class HowToPlayViewController : ViewController
{
	// Token: 0x1400009C RID: 156
	// (add) Token: 0x06001243 RID: 4675 RVA: 0x00044894 File Offset: 0x00042A94
	// (remove) Token: 0x06001244 RID: 4676 RVA: 0x000448CC File Offset: 0x00042ACC
	public event Action didPressTutorialButtonEvent;

	// Token: 0x06001245 RID: 4677 RVA: 0x0000DE37 File Offset: 0x0000C037
	public void Setup(bool showTutorialButton)
	{
		this._tutorialButton.gameObject.SetActive(showTutorialButton);
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x00044904 File Offset: 0x00042B04
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._tutorialButton, delegate
			{
				Action action = this.didPressTutorialButtonEvent;
				if (action == null)
				{
					return;
				}
				action();
			});
			string[] texts = new string[]
			{
				Localization.Get("BUTTON_BASICS"),
				Localization.Get("BUTTON_SCORE_SYSTEM")
			};
			this._selectionSegmentedControl.SetTexts(texts);
			this._selectionSegmentedControl.didSelectCellEvent += this.HandleSelectionSegmentedControlDidSelectCell;
			this.SetActivePanel(this._selectionSegmentedControl.selectedCellNumber);
		}
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x0000DE4A File Offset: 0x0000C04A
	private void HandleSelectionSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellIdx)
	{
		this.SetActivePanel(cellIdx);
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x00044988 File Offset: 0x00042B88
	private void SetActivePanel(int panelIdx)
	{
		for (int i = 0; i < this._panels.Length; i++)
		{
			this._panels[i].SetActive(i == panelIdx);
		}
	}

	// Token: 0x04001203 RID: 4611
	[SerializeField]
	private Button _tutorialButton;

	// Token: 0x04001204 RID: 4612
	[SerializeField]
	private TextSegmentedControl _selectionSegmentedControl;

	// Token: 0x04001205 RID: 4613
	[SerializeField]
	private GameObject[] _panels;
}
