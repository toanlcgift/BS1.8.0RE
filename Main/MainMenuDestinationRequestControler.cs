using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

// Token: 0x020000BB RID: 187
public class MainMenuDestinationRequestControler : IInitializable, IDisposable
{
	// Token: 0x060002A4 RID: 676 RVA: 0x00003BE9 File Offset: 0x00001DE9
	public void Initialize()
	{
		this._destinationRequestManager.didSendMenuDestinationRequestEvent += this.HandleDestinationRequestManagerDidSendMenuDestinationRequest;
		if (this._destinationRequestManager.currentMenuDestinationRequest != null)
		{
			this.ProcessDestinationRequest(this._destinationRequestManager.currentMenuDestinationRequest);
		}
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
	public void Dispose()
	{
		if (this._destinationRequestManager != null)
		{
			this._destinationRequestManager.didSendMenuDestinationRequestEvent -= this.HandleDestinationRequestManagerDidSendMenuDestinationRequest;
		}
		if (this._gameScenesManager != null)
		{
			this._gameScenesManager.installEarlyBindingsEvent -= this.HandleGameScenesManagerInstallEarlyBindings;
		}
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource == null)
		{
			return;
		}
		cancellationTokenSource.Cancel();
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0001E014 File Offset: 0x0001C214
	private void HandleGameScenesManagerInstallEarlyBindings(ScenesTransitionSetupDataSO scenesTransitionSetupData, DiContainer container)
	{
		if (this._destinationRequestManager.currentMenuDestinationRequest != null && scenesTransitionSetupData == this._menuScenesTransitionSetupData)
		{
			container.Bind<MenuDestination>().FromInstance(this._destinationRequestManager.currentMenuDestinationRequest).AsSingle().NonLazy();
			this._destinationRequestManager.Clear();
			this._gameScenesManager.installEarlyBindingsEvent -= this.HandleGameScenesManagerInstallEarlyBindings;
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00003C20 File Offset: 0x00001E20
	private void HandleDestinationRequestManagerDidSendMenuDestinationRequest(MenuDestination menuDestination)
	{
		this.ProcessDestinationRequest(menuDestination);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0001E080 File Offset: 0x0001C280
	private async void ProcessDestinationRequest(MenuDestination menuDestination)
	{
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = new CancellationTokenSource();
		CancellationToken cancellationToken = this._cancellationTokenSource.Token;
		try
		{
			while (this._gameScenesManager.isInTransition)
			{
				await Task.Delay(100, cancellationToken);
			}
			bool flag = false;
			foreach (SceneInfo sceneInfo in this._menuScenesTransitionSetupData.scenes)
			{
				if (this._gameScenesManager.GetCurrentlyLoadedSceneNames().Contains(sceneInfo.sceneName))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this._gameScenesManager.ClearAndOpenScenes(this._menuScenesTransitionSetupData, 0f, null, null, false);
			}
			this._gameScenesManager.installEarlyBindingsEvent -= this.HandleGameScenesManagerInstallEarlyBindings;
			this._gameScenesManager.installEarlyBindingsEvent += this.HandleGameScenesManagerInstallEarlyBindings;
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x04000333 RID: 819
	[Inject]
	private IDestinationRequestManager _destinationRequestManager;

	// Token: 0x04000334 RID: 820
	[Inject]
	private MenuScenesTransitionSetupDataSO _menuScenesTransitionSetupData;

	// Token: 0x04000335 RID: 821
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04000336 RID: 822
	private CancellationTokenSource _cancellationTokenSource;
}
