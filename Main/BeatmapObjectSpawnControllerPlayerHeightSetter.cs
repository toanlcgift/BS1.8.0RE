using System;
using UnityEngine;
using Zenject;

// Token: 0x020002F5 RID: 757
public class BeatmapObjectSpawnControllerPlayerHeightSetter : MonoBehaviour
{
	// Token: 0x06000CFA RID: 3322 RVA: 0x0000A13F File Offset: 0x0000833F
	protected void Start()
	{
		this._playerHeightDetector.playerHeightDidChangeEvent += this.HandlePlayerHeightDidChange;
		this.HandlePlayerHeightDidChange(this._playerHeightDetector.playerHeight);
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0000A169 File Offset: 0x00008369
	protected void OnDestroy()
	{
		if (this._playerHeightDetector != null)
		{
			this._playerHeightDetector.playerHeightDidChangeEvent -= this.HandlePlayerHeightDidChange;
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0000A190 File Offset: 0x00008390
	private void HandlePlayerHeightDidChange(float playerHeight)
	{
		this._beatmapObjectSpawnController.jumpOffsetY = BeatmapObjectSpawnControllerPlayerHeightSetter.JumpOffsetYForPlayerHeight(playerHeight);
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0000A1A3 File Offset: 0x000083A3
	public static float JumpOffsetYForPlayerHeight(float playerHeight)
	{
		return Mathf.Clamp((playerHeight - 1.8f) * 0.5f, -0.2f, 0.6f);
	}

	// Token: 0x04000D61 RID: 3425
	[Inject]
	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	// Token: 0x04000D62 RID: 3426
	[Inject]
	private PlayerHeightDetector _playerHeightDetector;
}
