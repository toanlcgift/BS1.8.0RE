using System;
using UnityEngine;
using Zenject;

// Token: 0x0200024E RID: 590
public class NoteLineConnectionController : MonoBehaviour
{
	// Token: 0x14000035 RID: 53
	// (add) Token: 0x060009CA RID: 2506 RVA: 0x0002EB70 File Offset: 0x0002CD70
	// (remove) Token: 0x060009CB RID: 2507 RVA: 0x0002EBA8 File Offset: 0x0002CDA8
	public event Action<NoteLineConnectionController> didFinishEvent;

	// Token: 0x060009CC RID: 2508 RVA: 0x0002EBE0 File Offset: 0x0002CDE0
	public void Setup(NoteController noteController0, NoteController noteController1, float fadeOutStartDistance, float fadeOutEndDistance, float noteTime)
	{
		this._noteController0 = noteController0;
		this._noteController1 = noteController1;
		this._fadeOutStartDistance = fadeOutStartDistance;
		this._fadeOutEndDistance = fadeOutEndDistance;
		this._noteTime = noteTime;
		this._didFinish = false;
		this._color0 = this._colorManager.ColorForNoteType(this._noteController0.noteData.noteType);
		this._color1 = this._colorManager.ColorForNoteType(this._noteController1.noteData.noteType);
		this.UpdatePositionsAndColors();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00007A9F File Offset: 0x00005C9F
	protected void Update()
	{
		if (this._didFinish)
		{
			return;
		}
		if (this._audioTimeSyncController.songTime >= this._noteTime)
		{
			this._didFinish = true;
			Action<NoteLineConnectionController> action = this.didFinishEvent;
			if (action != null)
			{
				action(this);
			}
		}
		this.UpdatePositionsAndColors();
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0002EC64 File Offset: 0x0002CE64
	private void UpdatePositionsAndColors()
	{
		Vector3 position = this._noteController0.transform.position;
		Vector3 position2 = this._noteController1.transform.position;
		this._lineRenderer.SetPosition(0, position);
		this._lineRenderer.SetPosition(1, position2);
		float num = Vector3.Magnitude(this._playerController.headPos - (position + position2) * 0.5f);
		float num2 = this._fadeOutStartDistance - this._fadeOutEndDistance;
		float num3 = (num - this._fadeOutEndDistance) / num2;
		this._color0.a = num3;
		this._color1.a = num3;
		this._lineRenderer.startColor = this._color0;
		this._lineRenderer.endColor = this._color1;
		this._lineRenderer.enabled = (num3 > 0f);
	}

	// Token: 0x04000A0E RID: 2574
	[SerializeField]
	private LineRenderer _lineRenderer;

	// Token: 0x04000A0F RID: 2575
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000A10 RID: 2576
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000A11 RID: 2577
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000A13 RID: 2579
	private NoteController _noteController0;

	// Token: 0x04000A14 RID: 2580
	private NoteController _noteController1;

	// Token: 0x04000A15 RID: 2581
	private Color _color0;

	// Token: 0x04000A16 RID: 2582
	private Color _color1;

	// Token: 0x04000A17 RID: 2583
	private float _fadeOutStartDistance;

	// Token: 0x04000A18 RID: 2584
	private float _fadeOutEndDistance;

	// Token: 0x04000A19 RID: 2585
	private float _noteTime;

	// Token: 0x04000A1A RID: 2586
	private bool _didFinish;

	// Token: 0x0200024F RID: 591
	public class Pool : MemoryPoolWithActiveItems<NoteLineConnectionController>
	{
	}
}
