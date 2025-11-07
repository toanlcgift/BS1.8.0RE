using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003EE RID: 1006
public class MissionHelpViewController : ViewController
{
	// Token: 0x140000A9 RID: 169
	// (add) Token: 0x060012D9 RID: 4825 RVA: 0x00046828 File Offset: 0x00044A28
	// (remove) Token: 0x060012DA RID: 4826 RVA: 0x00046860 File Offset: 0x00044A60
	public event Action<MissionHelpViewController> didFinishEvent;

	// Token: 0x060012DB RID: 4827 RVA: 0x0000E420 File Offset: 0x0000C620
	public void Setup(MissionHelpSO missionHelp)
	{
		this._missionHelp = missionHelp;
		if (base.isInViewControllerHierarchy)
		{
			this.RefreshContent();
		}
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x0000E437 File Offset: 0x0000C637
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._okButton, new Action(this.OkButtonPressed));
		}
		this.RefreshContent();
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x00046898 File Offset: 0x00044A98
	public void RefreshContent()
	{
		foreach (MissionHelpViewController.MissionHelpGameObjectPair missionHelpGameObjectPair in this._missionHelpGameObjectPairs)
		{
			missionHelpGameObjectPair.gameObject.SetActive(missionHelpGameObjectPair.missionHelp == this._missionHelp);
		}
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x0000E45F File Offset: 0x0000C65F
	private void OkButtonPressed()
	{
		Action<MissionHelpViewController> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x04001290 RID: 4752
	[SerializeField]
	private Button _okButton;

	// Token: 0x04001291 RID: 4753
	[SerializeField]
	private MissionHelpViewController.MissionHelpGameObjectPair[] _missionHelpGameObjectPairs;

	// Token: 0x04001293 RID: 4755
	private MissionHelpSO _missionHelp;

	// Token: 0x020003EF RID: 1007
	[Serializable]
	public class MissionHelpGameObjectPair
	{
		// Token: 0x04001294 RID: 4756
		public MissionHelpSO missionHelp;

		// Token: 0x04001295 RID: 4757
		public GameObject gameObject;
	}
}
