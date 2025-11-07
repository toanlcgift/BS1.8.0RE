using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x020003CB RID: 971
public class EnterPlayerGuestNameViewController : ViewController
{
	// Token: 0x060011ED RID: 4589 RVA: 0x0000DB30 File Offset: 0x0000BD30
	public void Init(EnterPlayerGuestNameViewController.FinishDelegate didFinishCallback)
	{
		this._didFinishCallback = didFinishCallback;
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x00043A68 File Offset: 0x00041C68
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._textEntryController.okButtonWasPressedEvent += this.OkButtonPressed;
			this._textEntryController.text = "";
			this._textEntryController.hideCancelButton = true;
			List<string> guestPlayerNames = this._playerDataModel.playerData.guestPlayerNames;
			int num = 0;
			int i;
			for (i = 0; i < Mathf.Min(guestPlayerNames.Count, 5); i++)
			{
				num += guestPlayerNames[i].Length + 1;
				if (num > 40)
				{
					break;
				}
			}
			this._guestNameButtonsListItemsList.SetData(Mathf.Min(guestPlayerNames.Count, i + 1), delegate(int idx, GuestNameButtonsListItem item)
			{
				string guestPlayerName = guestPlayerNames[idx];
				item.nameText = guestPlayerName;
				item.buttonPressed = delegate()
				{
					this._textEntryController.text = guestPlayerName;
				};
			});
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0000DB39 File Offset: 0x0000BD39
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._textEntryController.okButtonWasPressedEvent -= this.OkButtonPressed;
			this._didFinishCallback = null;
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x00043B38 File Offset: 0x00041D38
	private void OkButtonPressed()
	{
		string text = this._textEntryController.text;
		EnterPlayerGuestNameViewController.FinishDelegate didFinishCallback = this._didFinishCallback;
		if (didFinishCallback != null)
		{
			didFinishCallback(this, text);
		}
		this._didFinishCallback = null;
		this._playerDataModel.playerData.AddGuestPlayerName(text);
		this._playerNameWasEnteredSignal.Raise(text);
	}

	// Token: 0x040011B0 RID: 4528
	private const int kMaxPlayerNameCompoundLenght = 40;

	// Token: 0x040011B1 RID: 4529
	private const int kMaxShowPlayer = 5;

	// Token: 0x040011B2 RID: 4530
	[SerializeField]
	[SignalSender]
	private StringSignal _playerNameWasEnteredSignal;

	// Token: 0x040011B3 RID: 4531
	[Space]
	[SerializeField]
	private VRTextEntryController _textEntryController;

	// Token: 0x040011B4 RID: 4532
	[SerializeField]
	private GuestNameButtonsListItemsList _guestNameButtonsListItemsList;

	// Token: 0x040011B5 RID: 4533
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x040011B6 RID: 4534
	private EnterPlayerGuestNameViewController.FinishDelegate _didFinishCallback;

	// Token: 0x020003CC RID: 972
	// (Invoke) Token: 0x060011F3 RID: 4595
	public delegate void FinishDelegate(EnterPlayerGuestNameViewController viewController, string playerName);
}
