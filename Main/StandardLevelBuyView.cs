using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200042C RID: 1068
public class StandardLevelBuyView : MonoBehaviour
{
	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06001463 RID: 5219 RVA: 0x0000F645 File Offset: 0x0000D845
	public Button buyButton
	{
		get
		{
			return this._buyButton;
		}
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0000F64D File Offset: 0x0000D84D
	public void SetContent(IPreviewBeatmapLevel previewBeatmapLevel)
	{
		this._previewBeatmapLevel = previewBeatmapLevel;
		if (base.gameObject.activeInHierarchy)
		{
			this.LoadDataAsync(previewBeatmapLevel);
		}
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x0000F66A File Offset: 0x0000D86A
	protected void OnEnable()
	{
		this.LoadDataAsync(this._previewBeatmapLevel);
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x0000F678 File Offset: 0x0000D878
	protected void OnDisable()
	{
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = null;
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x0000F692 File Offset: 0x0000D892
	protected void OnDestroy()
	{
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = null;
		if (this._blurredCoverTexture != null)
		{
			UnityEngine.Object.Destroy(this._blurredCoverTexture);
			this._blurredCoverTexture = null;
		}
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x0004A7A8 File Offset: 0x000489A8
	public async void LoadDataAsync(IPreviewBeatmapLevel level)
	{
		if (!(this._loadingLevelId == level.levelID))
		{
			try
			{
				this._loadingLevelId = level.levelID;
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._cancellationTokenSource = new CancellationTokenSource();
				this._coverRawImage.enabled = false;
				CancellationToken cancellationToken = this._cancellationTokenSource.Token;
				Texture2D src = await level.GetCoverImageTexture2DAsync(cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				if (this._blurredCoverTexture != null)
				{
					UnityEngine.Object.Destroy(this._blurredCoverTexture);
				}
				this._blurredCoverTexture = this._kawaseBlurRenderer.Blur(src, KawaseBlurRendererSO.KernelSize.Kernel7, 0);
				this._coverRawImage.texture = this._blurredCoverTexture;
				this._coverRawImage.enabled = true;
				this._songNameText.text = level.songName;
				this._levelParamsPanel.duration = level.songDuration;
				this._levelParamsPanel.bpm = level.beatsPerMinute;
				cancellationToken = default(CancellationToken);
			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (this._loadingLevelId == level.levelID)
				{
					this._loadingLevelId = null;
				}
			}
		}
	}

	// Token: 0x04001403 RID: 5123
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x04001404 RID: 5124
	[SerializeField]
	private BasicLevelParamsPanel _levelParamsPanel;

	// Token: 0x04001405 RID: 5125
	[SerializeField]
	private RawImage _coverRawImage;

	// Token: 0x04001406 RID: 5126
	[SerializeField]
	private Button _buyButton;

	// Token: 0x04001407 RID: 5127
	[Space]
	[SerializeField]
	private KawaseBlurRendererSO _kawaseBlurRenderer;

	// Token: 0x04001408 RID: 5128
	private string _loadingLevelId;

	// Token: 0x04001409 RID: 5129
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x0400140A RID: 5130
	private Texture2D _blurredCoverTexture;

	// Token: 0x0400140B RID: 5131
	private IPreviewBeatmapLevel _previewBeatmapLevel;
}
