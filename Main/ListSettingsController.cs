using System;

// Token: 0x020003B4 RID: 948
public abstract class ListSettingsController : IncDecSettingsController
{
	// Token: 0x06001169 RID: 4457
	protected abstract bool GetInitValues(out int idx, out int numberOfElements);

	// Token: 0x0600116A RID: 4458
	protected abstract void ApplyValue(int idx);

	// Token: 0x0600116B RID: 4459
	protected abstract string TextForValue(int idx);

	// Token: 0x0600116C RID: 4460 RVA: 0x0000D31E File Offset: 0x0000B51E
	protected override void OnEnable()
	{
		base.OnEnable();
		if (this.GetInitValues(out this._idx, out this._numberOfElements))
		{
			this.RefreshUI();
			this.ApplyValue(this._idx);
		}
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x0000D34C File Offset: 0x0000B54C
	private void RefreshUI()
	{
		base.text = this.TextForValue(this._idx);
		base.enableDec = (this._idx > 0);
		base.enableInc = (this._idx < this._numberOfElements - 1);
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0000D385 File Offset: 0x0000B585
	public void Refresh(bool applyValue)
	{
		if (this.GetInitValues(out this._idx, out this._numberOfElements))
		{
			this.RefreshUI();
			if (applyValue)
			{
				this.ApplyValue(this._idx);
			}
		}
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
	public override void IncButtonPressed()
	{
		if (this._idx < this._numberOfElements - 1)
		{
			this._idx++;
		}
		this.RefreshUI();
		this.ApplyValue(this._idx);
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
	public override void DecButtonPressed()
	{
		if (this._idx > 0)
		{
			this._idx--;
		}
		this.RefreshUI();
		this.ApplyValue(this._idx);
	}

	// Token: 0x0400114D RID: 4429
	private int _idx;

	// Token: 0x0400114E RID: 4430
	private int _numberOfElements;
}
