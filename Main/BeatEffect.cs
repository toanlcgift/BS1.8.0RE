using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class BeatEffect : MonoBehaviour
{
	// Token: 0x14000044 RID: 68
	// (add) Token: 0x06000A55 RID: 2645 RVA: 0x00030DFC File Offset: 0x0002EFFC
	// (remove) Token: 0x06000A56 RID: 2646 RVA: 0x00030E34 File Offset: 0x0002F034
	public event Action<BeatEffect> didFinishEvent;

	// Token: 0x06000A57 RID: 2647 RVA: 0x00030E6C File Offset: 0x0002F06C
	public void Init(Color color, float animationDuration, Quaternion rotation)
	{
		this._elapsedTime = 0f;
		this._animationDuration = animationDuration;
		this._color = color.ColorWithAlpha(1f);
		this._spriteRenderer.color = color.ColorWithAlpha(0f);
		this._tubeBloomPrePassLight.color = color.ColorWithAlpha(0f);
		base.transform.rotation = rotation;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00030ED4 File Offset: 0x0002F0D4
	protected void Update()
	{
		this._elapsedTime += Time.deltaTime;
		float time = Mathf.Clamp01(this._elapsedTime / this._animationDuration);
		this._tubeBloomPrePassLight.color = this._color.ColorWithAlpha(this._lightIntensityCurve.Evaluate(time));
		this._spriteRenderer.color = this._color.ColorWithAlpha(this._transparencyCurve.Evaluate(time));
		this._spriteTransform.localScale = new Vector3(this._spriteXScaleCurve.Evaluate(time), this._spriteYScaleCurve.Evaluate(time), 1f);
		if (this._elapsedTime > this._animationDuration)
		{
			Action<BeatEffect> action = this.didFinishEvent;
			if (action == null)
			{
				return;
			}
			action(this);
		}
	}

	// Token: 0x04000AB8 RID: 2744
	[SerializeField]
	private SpriteRenderer _spriteRenderer;

	// Token: 0x04000AB9 RID: 2745
	[SerializeField]
	private Transform _spriteTransform;

	// Token: 0x04000ABA RID: 2746
	[SerializeField]
	private TubeBloomPrePassLight _tubeBloomPrePassLight;

	// Token: 0x04000ABB RID: 2747
	[Space]
	[SerializeField]
	private AnimationCurve _lightIntensityCurve;

	// Token: 0x04000ABC RID: 2748
	[SerializeField]
	private AnimationCurve _spriteXScaleCurve;

	// Token: 0x04000ABD RID: 2749
	[SerializeField]
	private AnimationCurve _spriteYScaleCurve;

	// Token: 0x04000ABE RID: 2750
	[SerializeField]
	private AnimationCurve _transparencyCurve;

	// Token: 0x04000ABF RID: 2751
	private float _animationDuration;

	// Token: 0x04000AC0 RID: 2752
	private float _elapsedTime;

	// Token: 0x04000AC1 RID: 2753
	private Color _color;

	// Token: 0x02000267 RID: 615
	public class Pool : MemoryPoolWithActiveItems<BeatEffect>
	{
	}
}
