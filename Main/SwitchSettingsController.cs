using System;

// Token: 0x020003B8 RID: 952
public abstract class SwitchSettingsController : IncDecSettingsController
{
	// Token: 0x0600117C RID: 4476
	protected abstract bool GetInitValue();

	// Token: 0x0600117D RID: 4477
	protected abstract void ApplyValue(bool value);

	// Token: 0x0600117E RID: 4478
	protected abstract string TextForValue(bool value);

	// Token: 0x0600117F RID: 4479 RVA: 0x0000D4B2 File Offset: 0x0000B6B2
	protected override void OnEnable()
	{
		base.OnEnable();
		this._on = this.GetInitValue();
		this.RefreshUI();
		this.ApplyValue(this._on);
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
	private void RefreshUI()
	{
		base.text = this.TextForValue(this._on);
		base.enableDec = this._on;
		base.enableInc = !this._on;
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0000D507 File Offset: 0x0000B707
	public override void IncButtonPressed()
	{
		this._on = true;
		this.RefreshUI();
		this.ApplyValue(this._on);
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0000D522 File Offset: 0x0000B722
	public override void DecButtonPressed()
	{
		this._on = false;
		this.RefreshUI();
		this.ApplyValue(this._on);
	}

	// Token: 0x04001155 RID: 4437
	private bool _on;
}
