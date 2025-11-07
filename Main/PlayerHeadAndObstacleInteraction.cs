using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x020002E3 RID: 739
public class PlayerHeadAndObstacleInteraction : MonoBehaviour
{
	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00009CC9 File Offset: 0x00007EC9
	public Vector3 headPos
	{
		get
		{
			return this._playerController.headPos;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00036B90 File Offset: 0x00034D90
	public List<ObstacleController> intersectingObstacles
	{
		get
		{
			int frameCount = Time.frameCount;
			if (this._lastFrameNumCheck == frameCount)
			{
				return this._intersectingObstacles;
			}
			this.GetObstaclesCointainingPoint(this._playerController.headPos, this._intersectingObstacles);
			this._lastFrameNumCheck = frameCount;
			return this._intersectingObstacles;
		}
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00036BD8 File Offset: 0x00034DD8
	public void GetObstaclesCointainingPoint(Vector3 worldPos, List<ObstacleController> obstacleControllers)
	{
		obstacleControllers.Clear();
		foreach (ObstacleController obstacleController in this._obstaclePool.activeItems)
		{
			if (!obstacleController.hasPassedAvoidedMark)
			{
				Vector3 point = obstacleController.transform.InverseTransformPoint(worldPos);
				if (obstacleController.bounds.Contains(point))
				{
					obstacleControllers.Add(obstacleController);
				}
			}
		}
	}

	// Token: 0x04000D0C RID: 3340
	[Inject]
	private ObstacleController.Pool _obstaclePool;

	// Token: 0x04000D0D RID: 3341
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000D0E RID: 3342
	private int _lastFrameNumCheck = -1;

	// Token: 0x04000D0F RID: 3343
	private List<ObstacleController> _intersectingObstacles = new List<ObstacleController>(10);
}
