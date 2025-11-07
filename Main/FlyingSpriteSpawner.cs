using System;
using UnityEngine;
using Zenject;

// Token: 0x0200025E RID: 606
public class FlyingSpriteSpawner : MonoBehaviour
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x000302E0 File Offset: 0x0002E4E0
	public void SpawnFlyingSprite(Vector3 pos, Quaternion rotation, Quaternion inverseRotation)
	{
		FlyingSpriteEffect flyingSpriteEffect = this._flyingSpriteEffectPool.Spawn();
		flyingSpriteEffect.didFinishEvent += this.HandleFlyingSpriteEffectDidFinish;
		flyingSpriteEffect.transform.localPosition = pos;
		pos = inverseRotation * pos;
		Vector3 targetPos = rotation * new Vector3(Mathf.Sign(pos.x) * this._xSpread, this._targetYPos, this._targetZPos);
		flyingSpriteEffect.InitAndPresent(this._duration, targetPos, rotation, this._sprite, this._material, this._color, this._shake);
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00007F4F File Offset: 0x0000614F
	private void HandleFlyingSpriteEffectDidFinish(FlyingObjectEffect flyingObjectEffect)
	{
		flyingObjectEffect.didFinishEvent -= this.HandleFlyingSpriteEffectDidFinish;
		this._flyingSpriteEffectPool.Despawn(flyingObjectEffect as FlyingSpriteEffect);
	}

	// Token: 0x04000A82 RID: 2690
	[SerializeField]
	private Sprite _sprite;

	// Token: 0x04000A83 RID: 2691
	[SerializeField]
	private Material _material;

	// Token: 0x04000A84 RID: 2692
	[SerializeField]
	private float _duration = 1f;

	// Token: 0x04000A85 RID: 2693
	[SerializeField]
	private float _xSpread = 1.15f;

	// Token: 0x04000A86 RID: 2694
	[SerializeField]
	private float _targetYPos = 1.3f;

	// Token: 0x04000A87 RID: 2695
	[SerializeField]
	private float _targetZPos = 10f;

	// Token: 0x04000A88 RID: 2696
	[SerializeField]
	private Color _color = new Color(0f, 0.75f, 1f);

	// Token: 0x04000A89 RID: 2697
	[SerializeField]
	private bool _shake;

	// Token: 0x04000A8A RID: 2698
	[Inject]
	private FlyingSpriteEffect.Pool _flyingSpriteEffectPool;
}
