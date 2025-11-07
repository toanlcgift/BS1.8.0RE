using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
public interface IBeatmapObjectSpawner
{
	// Token: 0x06000932 RID: 2354
	void SpawnObstacle(ObstacleData obstacleData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float rotation, float noteLinesDistance, float obstacleHeight);

	// Token: 0x06000933 RID: 2355
	void SpawnBombNote(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float rotation);

	// Token: 0x06000934 RID: 2356
	void SpawnBasicNote(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float rotation, bool disappearingArrow, bool ghostNote, float cutDirectionAngleOffset);
}
