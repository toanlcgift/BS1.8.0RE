using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class FlyingSpriteEffect : FlyingObjectEffect
{
	// Token: 0x06000A9C RID: 2716 RVA: 0x00008416 File Offset: 0x00006616
	public void InitAndPresent(float duration, Vector3 targetPos, Quaternion rotation, Sprite sprite, Material material, Color color, bool shake)
	{
		this._spriteRenderer.sprite = sprite;
		this._spriteRenderer.material = material;
		this._color = color;
		base.InitAndPresent(duration, targetPos, rotation, shake);
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x00008445 File Offset: 0x00006645
	protected override void ManualUpdate(float t)
	{
		this._spriteRenderer.color = this._color.ColorWithAlpha(this._fadeAnimationCurve.Evaluate(t));
	}

	// Token: 0x04000B04 RID: 2820
	[SerializeField]
	private SpriteRenderer _spriteRenderer;

	// Token: 0x04000B05 RID: 2821
	[SerializeField]
	private AnimationCurve _fadeAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	// Token: 0x04000B06 RID: 2822
	protected Color _color;

	// Token: 0x02000276 RID: 630
	public class Pool : MemoryPoolWithActiveItems<FlyingSpriteEffect>
	{
	}
}
