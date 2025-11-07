using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x02000228 RID: 552
public class BeatLine : LightWithId
{
	// Token: 0x1700025E RID: 606
	// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00006FEB File Offset: 0x000051EB
	public bool isFinished
	{
		get
		{
			return this._highlights.Count == 0;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00006FFB File Offset: 0x000051FB
	public float rotation
	{
		get
		{
			return this._rotation;
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0002AF34 File Offset: 0x00029134
	public void Init(Vector3 position, float rotation)
	{
		this._rotation = rotation;
		this._tubeBloomPrePassLight.transform.localPosition = position;
		base.transform.eulerAngles = new Vector3(90f, rotation, 0f);
		this._tubeBloomPrePassLight.color = this._color.ColorWithAlpha(this._arriveFadeCurve.Evaluate(0f));
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00007003 File Offset: 0x00005203
	public override void ColorWasSet(Color color)
	{
		this._color = color;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0002AF9C File Offset: 0x0002919C
	public void AddHighlight(float startTime, float arriveDuration, float jumpDuration)
	{
		BeatLine.HighlightData item = default(BeatLine.HighlightData);
		item.startTime = startTime;
		item.arriveDuration = arriveDuration;
		item.halfJumpDuration = jumpDuration * 0.5f;
		this._highlights.Add(item);
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0002AFDC File Offset: 0x000291DC
	public void ManualUpdate(float songTime)
	{
		float num = 0f;
		for (int i = this._highlights.Count - 1; i >= 0; i--)
		{
			BeatLine.HighlightData highlightData = this._highlights[i];
			float num2 = songTime - highlightData.startTime;
			if (num2 >= highlightData.arriveDuration + highlightData.halfJumpDuration)
			{
				this._highlights.RemoveAt(i);
			}
			else if (num2 < highlightData.arriveDuration)
			{
				num += this._arriveFadeCurve.Evaluate(num2 / highlightData.arriveDuration) * this._alphaMul;
			}
			else
			{
				num += this._jumpFadeCurve.Evaluate((num2 - highlightData.arriveDuration) / highlightData.halfJumpDuration) * this._alphaMul;
			}
		}
		if (this._highlights.Count == 0)
		{
			return;
		}
		if (num > this._maxAlpha)
		{
			num = this._maxAlpha;
		}
		this._tubeBloomPrePassLight.color = this._color.ColorWithAlpha(num);
	}

	// Token: 0x0400091D RID: 2333
	[SerializeField]
	private TubeBloomPrePassLight _tubeBloomPrePassLight;

	// Token: 0x0400091E RID: 2334
	[SerializeField]
	private AnimationCurve _arriveFadeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400091F RID: 2335
	[SerializeField]
	private AnimationCurve _jumpFadeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04000920 RID: 2336
	[SerializeField]
	private float _alphaMul = 1f;

	// Token: 0x04000921 RID: 2337
	[SerializeField]
	private float _maxAlpha = 1f;

	// Token: 0x04000922 RID: 2338
	private List<BeatLine.HighlightData> _highlights = new List<BeatLine.HighlightData>(10);

	// Token: 0x04000923 RID: 2339
	private Color _color;

	// Token: 0x04000924 RID: 2340
	private float _rotation;

	// Token: 0x02000229 RID: 553
	public class Pool : MonoMemoryPool<BeatLine>
	{
	}

	// Token: 0x0200022A RID: 554
	private struct HighlightData
	{
		// Token: 0x04000925 RID: 2341
		public float startTime;

		// Token: 0x04000926 RID: 2342
		public float arriveDuration;

		// Token: 0x04000927 RID: 2343
		public float halfJumpDuration;
	}
}
