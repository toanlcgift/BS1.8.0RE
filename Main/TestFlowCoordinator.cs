using System;
using HMUI;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class TestFlowCoordinator : FlowCoordinator
{
	// Token: 0x0600109C RID: 4252 RVA: 0x0000CA21 File Offset: 0x0000AC21
	protected override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			base.ProvideInitialViewControllers(this._viewController, this._leftViewController, this._rightViewController, this._bottomScreenViewController, this._topScreenViewController);
		}
	}

	// Token: 0x040010B5 RID: 4277
	[SerializeField]
	[NullAllowed]
	private ViewController _viewController;

	// Token: 0x040010B6 RID: 4278
	[SerializeField]
	[NullAllowed]
	private ViewController _leftViewController;

	// Token: 0x040010B7 RID: 4279
	[SerializeField]
	[NullAllowed]
	private ViewController _rightViewController;

	// Token: 0x040010B8 RID: 4280
	[SerializeField]
	[NullAllowed]
	private ViewController _bottomScreenViewController;

	// Token: 0x040010B9 RID: 4281
	[SerializeField]
	[NullAllowed]
	private ViewController _topScreenViewController;
}
