using System;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class NoteBasicCutInfo
{
	// Token: 0x06000647 RID: 1607 RVA: 0x00024AB8 File Offset: 0x00022CB8
	public static void GetBasicCutInfo(Transform noteTransform, NoteType noteType, NoteCutDirection cutDirection, SaberType saberType, float saberBladeSpeed, Vector3 cutDirVec, out bool directionOK, out bool speedOK, out bool saberTypeOK, out float cutDirDeviation)
	{
		cutDirVec = noteTransform.InverseTransformVector(cutDirVec);
		bool flag = Mathf.Abs(cutDirVec.z) > Mathf.Abs(cutDirVec.x) * 10f && Mathf.Abs(cutDirVec.z) > Mathf.Abs(cutDirVec.y) * 10f;
		float num = Mathf.Atan2(cutDirVec.y, cutDirVec.x) * 57.29578f;
		directionOK = ((!flag && num > -150f && num < -30f) || cutDirection == NoteCutDirection.Any);
		speedOK = (saberBladeSpeed > 2f);
		saberTypeOK = ((noteType == NoteType.NoteA && saberType == SaberType.SaberA) || (noteType == NoteType.NoteB && saberType == SaberType.SaberB));
		cutDirDeviation = (flag ? 90f : (num + 90f));
		if (cutDirDeviation > 180f)
		{
			cutDirDeviation -= 360f;
		}
	}

	// Token: 0x040006B4 RID: 1716
	private const float kCutAngleTolerance = 60f;

	// Token: 0x040006B5 RID: 1717
	private const float kMinBladeSpeedForCut = 2f;
}
