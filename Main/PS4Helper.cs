using System;

// Token: 0x020001DC RID: 476
public class PS4Helper : PersistentSingleton<PS4Helper>
{
	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000740 RID: 1856 RVA: 0x000281C8 File Offset: 0x000263C8
	// (remove) Token: 0x06000741 RID: 1857 RVA: 0x00028200 File Offset: 0x00026400
	public event Action didGoToBackgroundExecutionEvent;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000742 RID: 1858 RVA: 0x00028238 File Offset: 0x00026438
	// (remove) Token: 0x06000743 RID: 1859 RVA: 0x00028270 File Offset: 0x00026470
	public event Action didGoToForegroundExecutionEvent;

	// Token: 0x06000744 RID: 1860 RVA: 0x000282A8 File Offset: 0x000264A8
	protected void Update()
	{
		if (false)
		{
			if (!this._backgroundExecution)
			{
				this._backgroundExecution = true;
				Action action = this.didGoToBackgroundExecutionEvent;
				if (action == null)
				{
					return;
				}
				action();
				return;
			}
		}
		else if (this._backgroundExecution)
		{
			this._backgroundExecution = false;
			Action action2 = this.didGoToForegroundExecutionEvent;
			if (action2 == null)
			{
				return;
			}
			action2();
		}
	}

	// Token: 0x040007BB RID: 1979
	private bool _backgroundExecution;
}
