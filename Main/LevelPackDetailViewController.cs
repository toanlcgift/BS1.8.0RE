using System;
using System.Threading;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020003E2 RID: 994
public class LevelPackDetailViewController : ViewController
{
	// Token: 0x0600127B RID: 4731 RVA: 0x0004575C File Offset: 0x0004395C
	public void SetData(IBeatmapLevelPack pack)
	{
		this._pack = pack;
		if (this._blurredPackArtwork != null)
		{
			UnityEngine.Object.Destroy(this._blurredPackArtwork);
			this._blurredPackArtwork = null;
		}
		int num = 2;
		Texture2D texture2D = this._kawaseBlurRenderer.Blur(pack.coverImage.texture, KawaseBlurRendererSO.KernelSize.Kernel7, num);
		this._blurredPackArtwork = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), (float)(1024 >> num), 1U, SpriteMeshType.FullRect, new Vector4(0f, 0f, 0f, 0f), false);
		this._packImage.sprite = pack.coverImage;
		this.RefreshAvailabilityAsync();
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x00045824 File Offset: 0x00043A24
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._buyButton, new Action(this.OpenLevelPackProductStoreAsync));
			Action handleDidPressRefreshButton = delegate()
			{
				this.RefreshAvailabilityAsync();
			};
			this._eventBinder.Bind(delegate
			{
				this._loadingControl.didPressRefreshButtonEvent += handleDidPressRefreshButton;
			}, delegate
			{
				this._loadingControl.didPressRefreshButtonEvent -= handleDidPressRefreshButton;
			});
		}
		this._additionalContentModel.didInvalidateDataEvent += this.HandleAdditionalContentModelDidInvalidateData;
		this.RefreshAvailabilityAsync();
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x0000E01D File Offset: 0x0000C21D
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource == null)
		{
			return;
		}
		cancellationTokenSource.Cancel();
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000458B0 File Offset: 0x00043AB0
	protected override void OnDestroy()
	{
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
		this._eventBinder.ClearAllBindings();
		if (this._blurredPackArtwork != null)
		{
			UnityEngine.Object.Destroy(this._blurredPackArtwork);
			this._blurredPackArtwork = null;
		}
		base.OnDestroy();
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x00045908 File Offset: 0x00043B08
	private async void RefreshAvailabilityAsync()
	{
		if (base.isActiveAndEnabled)
		{
			try
			{
				this.ShowContent(LevelPackDetailViewController.ContentType.Loading, "");
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._cancellationTokenSource = new CancellationTokenSource();
				this._requireInternetContainer.SetActive(false);
				AdditionalContentModel.EntitlementStatus entitlementStatus = await this._additionalContentModel.GetPackEntitlementStatusAsync(this._pack.packID, this._cancellationTokenSource.Token);
				if (entitlementStatus != AdditionalContentModel.EntitlementStatus.Owned)
				{
					if (entitlementStatus != AdditionalContentModel.EntitlementStatus.NotOwned)
					{
						this.ShowContent(LevelPackDetailViewController.ContentType.Error, "Error loading data.");
					}
					else
					{
						this.ShowContent(LevelPackDetailViewController.ContentType.Buy, "");
					}
				}
				else
				{
					this.ShowContent(LevelPackDetailViewController.ContentType.Owned, "");
				}
			}
			catch (OperationCanceledException)
			{
			}
		}
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x00045944 File Offset: 0x00043B44
	private async void OpenLevelPackProductStoreAsync()
	{
		try
		{
			this.ShowContent(LevelPackDetailViewController.ContentType.Loading, "");
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this._cancellationTokenSource = new CancellationTokenSource();
			await this._additionalContentModel.OpenLevelPackProductStoreAsync(this._pack.packID, this._cancellationTokenSource.Token);
			this.RefreshAvailabilityAsync();
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x00045980 File Offset: 0x00043B80
	private void ShowContent(LevelPackDetailViewController.ContentType contentType, string errorText = "")
	{
		this._detailWrapper.SetActive(contentType == LevelPackDetailViewController.ContentType.Owned || contentType == LevelPackDetailViewController.ContentType.Buy);
		this._buyContainer.gameObject.SetActive(contentType == LevelPackDetailViewController.ContentType.Buy);
		if (contentType == LevelPackDetailViewController.ContentType.Buy)
		{
			this._packImage.sprite = this._blurredPackArtwork;
			this._packImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		}
		else
		{
			this._packImage.sprite = this._pack.coverImage;
			this._packImage.color = Color.white;
		}
		if (contentType == LevelPackDetailViewController.ContentType.Loading)
		{
			this._loadingControl.ShowLoading();
			return;
		}
		if (contentType != LevelPackDetailViewController.ContentType.Error)
		{
			this._loadingControl.Hide();
			return;
		}
		this._loadingControl.ShowText(errorText, true);
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x0000E046 File Offset: 0x0000C246
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this.RefreshAvailabilityAsync();
	}

	// Token: 0x0400123A RID: 4666
	[SerializeField]
	private GameObject _detailWrapper;

	// Token: 0x0400123B RID: 4667
	[SerializeField]
	private ImageView _packImage;

	// Token: 0x0400123C RID: 4668
	[SerializeField]
	private Button _buyButton;

	// Token: 0x0400123D RID: 4669
	[SerializeField]
	private GameObject _buyContainer;

	// Token: 0x0400123E RID: 4670
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x0400123F RID: 4671
	[SerializeField]
	private GameObject _requireInternetContainer;

	// Token: 0x04001240 RID: 4672
	[Space]
	[SerializeField]
	private KawaseBlurRendererSO _kawaseBlurRenderer;

	// Token: 0x04001241 RID: 4673
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x04001242 RID: 4674
	private EventBinder _eventBinder = new EventBinder();

	// Token: 0x04001243 RID: 4675
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x04001244 RID: 4676
	private IBeatmapLevelPack _pack;

	// Token: 0x04001245 RID: 4677
	private Sprite _blurredPackArtwork;

	// Token: 0x020003E3 RID: 995
	private enum ContentType
	{
		// Token: 0x04001247 RID: 4679
		Loading,
		// Token: 0x04001248 RID: 4680
		Owned,
		// Token: 0x04001249 RID: 4681
		Buy,
		// Token: 0x0400124A RID: 4682
		Error
	}
}
