using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public static class NoteCutDirectionExtensions
{
	// Token: 0x0600041D RID: 1053 RVA: 0x0002062C File Offset: 0x0001E82C
	public static Vector2 Direction(this NoteCutDirection cutDirection)
	{
		switch (cutDirection)
		{
		case NoteCutDirection.Up:
			return new Vector2(0f, 1f);
		case NoteCutDirection.Down:
			return new Vector2(0f, -1f);
		case NoteCutDirection.Left:
			return new Vector2(-1f, 0f);
		case NoteCutDirection.Right:
			return new Vector2(1f, 0f);
		case NoteCutDirection.UpLeft:
			return new Vector2(-0.7071f, 0.7071f);
		case NoteCutDirection.UpRight:
			return new Vector2(0.7071f, 0.7071f);
		case NoteCutDirection.DownLeft:
			return new Vector2(-0.7071f, -0.7071f);
		case NoteCutDirection.DownRight:
			return new Vector2(0.7071f, -0.7071f);
		default:
			return new Vector2(0f, 0f);
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x000206F4 File Offset: 0x0001E8F4
	public static float RotationAngle(this NoteCutDirection cutDirection)
	{
		switch (cutDirection)
		{
		case NoteCutDirection.Up:
			return -180f;
		case NoteCutDirection.Down:
			return 0f;
		case NoteCutDirection.Left:
			return -90f;
		case NoteCutDirection.Right:
			return 90f;
		case NoteCutDirection.UpLeft:
			return -135f;
		case NoteCutDirection.UpRight:
			return 135f;
		case NoteCutDirection.DownLeft:
			return -45f;
		case NoteCutDirection.DownRight:
			return 45f;
		default:
			return 0f;
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00004889 File Offset: 0x00002A89
	public static Quaternion Rotation(this NoteCutDirection cutDirection)
	{
		return Quaternion.Euler(0f, 0f, cutDirection.RotationAngle());
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000048A0 File Offset: 0x00002AA0
	public static bool IsMainDirection(this NoteCutDirection cutDirection)
	{
		switch (cutDirection)
		{
		case NoteCutDirection.Up:
			return true;
		case NoteCutDirection.Down:
			return true;
		case NoteCutDirection.Left:
			return true;
		case NoteCutDirection.Right:
			return true;
		default:
			return false;
		}
	}
}
