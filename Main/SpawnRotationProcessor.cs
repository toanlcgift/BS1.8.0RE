using System;

// Token: 0x020002E7 RID: 743
public class SpawnRotationProcessor
{
	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00009D43 File Offset: 0x00007F43
	public float rotation
	{
		get
		{
			return this._rotation;
		}
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00009D4B File Offset: 0x00007F4B
	public bool ProcessBeatmapEventData(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type.IsRotationEvent())
		{
			this._rotation += this.RotationForEventValue(beatmapEventData.value);
			return true;
		}
		return false;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00009D76 File Offset: 0x00007F76
	public float RotationForEventValue(int index)
	{
		if (index >= 0 && index < this._rotations.Length)
		{
			return this._rotations[index];
		}
		return 0f;
	}

	// Token: 0x04000D1F RID: 3359
	private float _rotation;

	// Token: 0x04000D20 RID: 3360
	private readonly float[] _rotations = new float[]
	{
		-60f,
		-45f,
		-30f,
		-15f,
		15f,
		30f,
		45f,
		60f
	};
}
