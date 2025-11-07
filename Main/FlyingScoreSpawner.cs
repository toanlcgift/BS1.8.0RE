using System;
using UnityEngine;
using Zenject;

// Token: 0x0200025B RID: 603
public class FlyingScoreSpawner : MonoBehaviour
{
	// Token: 0x06000A31 RID: 2609 RVA: 0x0003022C File Offset: 0x0002E42C
	public void SpawnFlyingScore(NoteCutInfo noteCutInfo, int noteLineIndex, int multiplier, Vector3 pos, Quaternion rotation, Quaternion inverseRotation, Color color)
	{
		FlyingScoreEffect flyingScoreEffect = this._flyingScoreEffectPool.Spawn();
		flyingScoreEffect.didFinishEvent += this.HandleFlyingScoreEffectDidFinish;
		flyingScoreEffect.transform.localPosition = pos;
		pos = inverseRotation * pos;
		pos.z = 0f;
		float y = 0f;
		if (this._initData.spawnPosition == FlyingScoreSpawner.SpawnPosition.Underground)
		{
			pos.y = -0.24f;
		}
		else
		{
			pos.y = 0.25f;
			y = -0.1f;
		}
		Vector3 targetPos = rotation * (pos + new Vector3(0f, y, 7.55f));
		flyingScoreEffect.InitAndPresent(noteCutInfo, multiplier, 0.7f, targetPos, rotation, color);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00007F1B File Offset: 0x0000611B
	private void HandleFlyingScoreEffectDidFinish(FlyingObjectEffect flyingObjectEffect)
	{
		flyingObjectEffect.didFinishEvent -= this.HandleFlyingScoreEffectDidFinish;
		this._flyingScoreEffectPool.Despawn(flyingObjectEffect as FlyingScoreEffect);
	}

	// Token: 0x04000A7C RID: 2684
	[Inject]
	private FlyingScoreEffect.Pool _flyingScoreEffectPool;

	// Token: 0x04000A7D RID: 2685
	[Inject]
	private FlyingScoreSpawner.InitData _initData;

	// Token: 0x0200025C RID: 604
	public enum SpawnPosition
	{
		// Token: 0x04000A7F RID: 2687
		Underground,
		// Token: 0x04000A80 RID: 2688
		AboveGround
	}

	// Token: 0x0200025D RID: 605
	public class InitData
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x00007F40 File Offset: 0x00006140
		public InitData(FlyingScoreSpawner.SpawnPosition spawnPosition)
		{
			this.spawnPosition = spawnPosition;
		}

		// Token: 0x04000A81 RID: 2689
		public readonly FlyingScoreSpawner.SpawnPosition spawnPosition;
	}
}
