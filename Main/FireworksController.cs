using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x02000270 RID: 624
public class FireworksController : MonoBehaviour
{
	// Token: 0x06000A84 RID: 2692 RVA: 0x0000830B File Offset: 0x0000650B
	protected void OnEnable()
	{
		base.StartCoroutine(this.SpawningCoroutine());
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0000831A File Offset: 0x0000651A
	private IEnumerator SpawningCoroutine()
	{
		while (base.enabled)
		{
			FireworkItemController fireworkItemController = this._fireworkItemPool.Spawn();
			fireworkItemController.didFinishEvent += this.HandleFireworkItemControllerDidFinish;
			fireworkItemController.transform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(-this._spawnSize.x * 0.5f, this._spawnSize.x * 0.5f), UnityEngine.Random.Range(-this._spawnSize.y * 0.5f, this._spawnSize.y * 0.5f), UnityEngine.Random.Range(-this._spawnSize.z * 0.5f, this._spawnSize.z * 0.5f));
			fireworkItemController.Fire();
			yield return new WaitForSeconds(UnityEngine.Random.Range(this._minSpawnInterval, this._maxSpawnInterval));
		}
		yield break;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00008329 File Offset: 0x00006529
	private void HandleFireworkItemControllerDidFinish(FireworkItemController fireworkItemController)
	{
		fireworkItemController.didFinishEvent -= this.HandleFireworkItemControllerDidFinish;
		this._fireworkItemPool.Despawn(fireworkItemController);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00008349 File Offset: 0x00006549
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 1f, 0f, 0.1f);
		Gizmos.DrawCube(base.transform.position, this._spawnSize);
	}

	// Token: 0x04000AEA RID: 2794
	[SerializeField]
	private Vector3 _spawnSize;

	// Token: 0x04000AEB RID: 2795
	[SerializeField]
	private float _minSpawnInterval = 0.2f;

	// Token: 0x04000AEC RID: 2796
	[SerializeField]
	private float _maxSpawnInterval = 1f;

	// Token: 0x04000AED RID: 2797
	[Inject]
	private FireworkItemController.Pool _fireworkItemPool;
}
