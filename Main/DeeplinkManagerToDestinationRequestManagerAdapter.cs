using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Zenject;

// Token: 0x020000B7 RID: 183
public class DeeplinkManagerToDestinationRequestManagerAdapter : IDestinationRequestManager
{
	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000295 RID: 661 RVA: 0x0001DC70 File Offset: 0x0001BE70
	// (remove) Token: 0x06000296 RID: 662 RVA: 0x0001DCA8 File Offset: 0x0001BEA8
	public event Action<MenuDestination> didSendMenuDestinationRequestEvent;

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000297 RID: 663 RVA: 0x00003BB0 File Offset: 0x00001DB0
	public MenuDestination currentMenuDestinationRequest
	{
		get
		{
			return this._currentMenuDestinationRequest;
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00003BB8 File Offset: 0x00001DB8
	[Inject]
	public void Init(IDeeplinkManager deeplinkManager)
	{
		deeplinkManager.didReceiveDeeplinkEvent += this.HandleDeeplinkManagerDidReceiveDeeplink;
		if (deeplinkManager.currentDeeplink != null)
		{
			this.HandleDeeplinkManagerDidReceiveDeeplink(deeplinkManager.currentDeeplink);
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00003BE0 File Offset: 0x00001DE0
	public void Clear()
	{
		this._currentMenuDestinationRequest = null;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
	protected void HandleDeeplinkManagerDidReceiveDeeplink(Deeplink deeplink)
	{
		this._currentMenuDestinationRequest = null;
		if (deeplink.LevelID != null)
		{
			IPreviewBeatmapLevel previewBeatmapLevel = null;
			IBeatmapLevelPack levelPackForLevelId = this._beatmapLevelsModel.GetLevelPackForLevelId(deeplink.LevelID);
			if (levelPackForLevelId != null)
			{
				foreach (IPreviewBeatmapLevel previewBeatmapLevel2 in levelPackForLevelId.beatmapLevelCollection.beatmapLevels)
				{
					if (previewBeatmapLevel2.levelID == deeplink.LevelID)
					{
						previewBeatmapLevel = previewBeatmapLevel2;
						break;
					}
				}
			}
			BeatmapCharacteristicSO beatmapCharacteristicSO = null;
			if (deeplink.Characteristic != null)
			{
				beatmapCharacteristicSO = this._beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(deeplink.Characteristic);
			}
			BeatmapDifficulty beatmapDifficulty;
			deeplink.Difficulty.BeatmapDifficultyFromSerializedName(out beatmapDifficulty);
			IBeatmapLevelPack beatmapLevelPack = levelPackForLevelId;
			IPreviewBeatmapLevel previewBeatmapLevel3 = previewBeatmapLevel;
			BeatmapCharacteristicSO beatmapCharacteristic = beatmapCharacteristicSO;
			this._currentMenuDestinationRequest = new SelectLevelDestination(beatmapLevelPack, previewBeatmapLevel3, beatmapDifficulty, beatmapCharacteristic);
		}
		else if (deeplink.PackID != null)
		{
			IBeatmapLevelPack levelPack = this._beatmapLevelsModel.GetLevelPack(deeplink.PackID);
			if (levelPack != null)
			{
				this._currentMenuDestinationRequest = new SelectLevelPackDestination(levelPack);
			}
		}
		else if (deeplink.Destination != null)
		{
			string text = deeplink.Destination.ToLower().Replace(" ", "");
			uint num = PrivateImplementationDetails.ComputeStringHash(text);
			SelectSubMenuDestination.Destination menuDestination;
			if (num <= 1666399712U)
			{
				if (num <= 413633715U)
				{
					if (num != 291338772U)
					{
						if (num != 413633715U)
						{
							goto IL_294;
						}
						if (!(text == "partyfreeplay"))
						{
							goto IL_294;
						}
					}
					else
					{
						if (!(text == "song"))
						{
							goto IL_294;
						}
						goto IL_280;
					}
				}
				else if (num != 912332847U)
				{
					if (num != 1581100439U)
					{
						if (num != 1666399712U)
						{
							goto IL_294;
						}
						if (!(text == "pack"))
						{
							goto IL_294;
						}
						goto IL_280;
					}
					else if (!(text == "party"))
					{
						goto IL_294;
					}
				}
				else
				{
					if (!(text == "tutorial"))
					{
						goto IL_294;
					}
					menuDestination = SelectSubMenuDestination.Destination.Tutorial;
					goto IL_297;
				}
				menuDestination = SelectSubMenuDestination.Destination.PartyFreePlay;
				goto IL_297;
			}
			if (num <= 3201311667U)
			{
				if (num != 1745255176U)
				{
					if (num != 2610554845U)
					{
						if (num != 3201311667U)
						{
							goto IL_294;
						}
						if (!(text == "campaign"))
						{
							goto IL_294;
						}
						menuDestination = SelectSubMenuDestination.Destination.Campaign;
						goto IL_297;
					}
					else if (!(text == "level"))
					{
						goto IL_294;
					}
				}
				else
				{
					if (!(text == "settings"))
					{
						goto IL_294;
					}
					menuDestination = SelectSubMenuDestination.Destination.Settings;
					goto IL_297;
				}
			}
			else if (num != 3664733674U)
			{
				if (num != 3743656066U)
				{
					if (num != 3915598666U)
					{
						goto IL_294;
					}
					if (!(text == "solo"))
					{
						goto IL_294;
					}
				}
				else if (!(text == "solofreeplay"))
				{
					goto IL_294;
				}
			}
			else if (!(text == "dlc"))
			{
				goto IL_294;
			}
			IL_280:
			menuDestination = SelectSubMenuDestination.Destination.SoloFreePlay;
			goto IL_297;
			IL_294:
			menuDestination = SelectSubMenuDestination.Destination.MainMenu;
			IL_297:
			this._currentMenuDestinationRequest = new SelectSubMenuDestination(menuDestination);
		}
		if (this._currentMenuDestinationRequest != null)
		{
			Action<MenuDestination> action = this.didSendMenuDestinationRequestEvent;
			if (action == null)
			{
				return;
			}
			action(this._currentMenuDestinationRequest);
		}
	}

	// Token: 0x0400032A RID: 810
	[Inject]
	private BeatmapLevelsModel _beatmapLevelsModel;

	// Token: 0x0400032B RID: 811
	[Inject]
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x0400032D RID: 813
	private MenuDestination _currentMenuDestinationRequest;
}

[CompilerGenerated]
internal sealed class PrivateImplementationDetails
{
	public static RuntimeFieldHandle FieldHandler { get; internal set; }

	// Token: 0x06001BFE RID: 7166 RVA: 0x0005FC18 File Offset: 0x0005DE18
	internal static uint ComputeStringHash(string s)
	{
		uint num = 0;
		if (s != null)
		{
			num = 2166136261U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619U;
			}
		}
		return num;
	}
}
