using System;
using UnityEngine;
using Zenject;

// Token: 0x0200025F RID: 607
public class FlyingTextSpawner : MonoBehaviour
{
	// Token: 0x06000A38 RID: 2616 RVA: 0x000303CC File Offset: 0x0002E5CC
	public void SpawnText(Vector3 pos, Quaternion rotation, Quaternion inverseRotation, string text)
	{
		FlyingTextEffect flyingTextEffect = this._flyingTextEffectPool.Spawn();
		flyingTextEffect.transform.localPosition = pos;
		pos = inverseRotation * pos;
		Vector3 targetPos = rotation * new Vector3(pos.x * this._xSpread, this._targetYPos, this._targetZPos);
		flyingTextEffect.InitAndPresent(text, this._duration, targetPos, rotation, this._color, this._fontSize, this._shake);
		flyingTextEffect.didFinishEvent += this.HandleFlyingTextEffectDidFinish;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00007F74 File Offset: 0x00006174
	private void HandleFlyingTextEffectDidFinish(FlyingObjectEffect flyingObjectEffect)
	{
		flyingObjectEffect.didFinishEvent -= this.HandleFlyingTextEffectDidFinish;
		this._flyingTextEffectPool.Despawn(flyingObjectEffect as FlyingTextEffect);
	}

	// Token: 0x04000A8B RID: 2699
	[SerializeField]
	private float _duration = 1f;

	// Token: 0x04000A8C RID: 2700
	[SerializeField]
	private float _xSpread = 1.15f;

	// Token: 0x04000A8D RID: 2701
	[SerializeField]
	private float _targetYPos = 1.3f;

	// Token: 0x04000A8E RID: 2702
	[SerializeField]
	private float _targetZPos = 10f;

	// Token: 0x04000A8F RID: 2703
	[SerializeField]
	private Color _color = new Color(0f, 0.75f, 1f);

	// Token: 0x04000A90 RID: 2704
	[SerializeField]
	private float _fontSize = 5f;

	// Token: 0x04000A91 RID: 2705
	[SerializeField]
	private bool _shake;

	// Token: 0x04000A92 RID: 2706
	[Inject]
	private FlyingTextEffect.Pool _flyingTextEffectPool;
}
