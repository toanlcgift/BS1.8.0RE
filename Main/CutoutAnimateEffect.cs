using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class CutoutAnimateEffect : MonoBehaviour
{
	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00008127 File Offset: 0x00006327
	// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0000812F File Offset: 0x0000632F
	public bool animating { get; private set; }

	// Token: 0x06000A60 RID: 2656 RVA: 0x00008138 File Offset: 0x00006338
	protected void Start()
	{
		this.SetCutout(0f);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00008145 File Offset: 0x00006345
	private IEnumerator AnimateToCutoutCoroutine(float cutoutStart, float cutoutEnd, float duration)
	{
		this.animating = true;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float time = elapsedTime / duration;
			this.SetCutout(Mathf.Lerp(cutoutStart, cutoutEnd, this._transitionCurve.Evaluate(time)));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.SetCutout(cutoutEnd);
		this.animating = false;
		yield break;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00030F98 File Offset: 0x0002F198
	private void SetCutout(float cutout)
	{
		CutoutEffect[] cuttoutEffects = this._cuttoutEffects;
		for (int i = 0; i < cuttoutEffects.Length; i++)
		{
			cuttoutEffects[i].SetCutout(cutout);
		}
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00008169 File Offset: 0x00006369
	public void ResetEffect()
	{
		this.animating = false;
		base.StopAllCoroutines();
		this.SetCutout(0f);
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00008183 File Offset: 0x00006383
	public void AnimateCutout(float cutoutStart, float cutoutEnd, float duration)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.AnimateToCutoutCoroutine(cutoutStart, cutoutEnd, duration));
	}

	// Token: 0x04000AC8 RID: 2760
	[SerializeField]
	private CutoutEffect[] _cuttoutEffects;

	// Token: 0x04000AC9 RID: 2761
	[SerializeField]
	private AnimationCurve _transitionCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
}
