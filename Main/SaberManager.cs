using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class SaberManager : MonoBehaviour
{
	// Token: 0x06000D56 RID: 3414 RVA: 0x0003892C File Offset: 0x00036B2C
	protected void Update()
	{
		if (this._allowOnlyOneSaber)
		{
			if (this._leftSaber.saberType == this._oneSaberType)
			{
				this._leftSaber.ManualUpdate();
				return;
			}
			if (this._rightSaber.saberType == this._oneSaberType)
			{
				this._rightSaber.ManualUpdate();
				return;
			}
		}
		else
		{
			this._leftSaber.ManualUpdate();
			this._rightSaber.ManualUpdate();
		}
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0000A54D File Offset: 0x0000874D
	protected void OnDisable()
	{
		this.RefreshSabers();
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0000A54D File Offset: 0x0000874D
	protected void OnEnable()
	{
		this.RefreshSabers();
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0000A555 File Offset: 0x00008755
	public void AllowOnlyOneSaber(SaberType saberType)
	{
		this._allowOnlyOneSaber = true;
		this._oneSaberType = saberType;
		this.RefreshSabers();
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00038998 File Offset: 0x00036B98
	private void RefreshSabers()
	{
		if (!base.isActiveAndEnabled)
		{
			this._leftSaber.gameObject.SetActive(false);
			this._rightSaber.gameObject.SetActive(false);
			return;
		}
		if (this._allowOnlyOneSaber)
		{
			this._leftSaber.gameObject.SetActive(this._oneSaberType == this._leftSaber.saberType);
			this._rightSaber.gameObject.SetActive(this._oneSaberType == this._rightSaber.saberType);
			return;
		}
		this._leftSaber.gameObject.SetActive(true);
		this._rightSaber.gameObject.SetActive(true);
	}

	// Token: 0x04000DC1 RID: 3521
	[SerializeField]
	private Saber _leftSaber;

	// Token: 0x04000DC2 RID: 3522
	[SerializeField]
	private Saber _rightSaber;

	// Token: 0x04000DC3 RID: 3523
	private bool _allowOnlyOneSaber;

	// Token: 0x04000DC4 RID: 3524
	private SaberType _oneSaberType;
}
