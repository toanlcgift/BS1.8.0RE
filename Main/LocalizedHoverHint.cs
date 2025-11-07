using System;
using HMUI;
using Polyglot;

// Token: 0x02000397 RID: 919
public class LocalizedHoverHint : LocalizedTextComponent<HoverHint>
{
	// Token: 0x060010AA RID: 4266 RVA: 0x0000CABC File Offset: 0x0000ACBC
	protected override void SetText(HoverHint hoverHint, string value)
	{
		hoverHint.text = value;
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x000023E9 File Offset: 0x000005E9
	protected override void UpdateAlignment(HoverHint hoverHint, LanguageDirection direction)
	{
	}
}
