using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class BaseNoteVisuals : MonoBehaviour
{
	// Token: 0x06000935 RID: 2357 RVA: 0x000073F8 File Offset: 0x000055F8
	protected void Awake()
	{
		this._noteController.didInitEvent += this.HandleNoteControllerDidInitEvent;
		this._noteController.noteDidStartDissolvingEvent += this.HandleNoteDidStartDissolvingEvent;
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00007428 File Offset: 0x00005628
	protected void OnDestroy()
	{
		if (this._noteController)
		{
			this._noteController.didInitEvent -= this.HandleNoteControllerDidInitEvent;
			this._noteController.noteDidStartDissolvingEvent -= this.HandleNoteDidStartDissolvingEvent;
		}
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00007465 File Offset: 0x00005665
	public void HandleNoteControllerDidInitEvent(NoteController noteController)
	{
		NoteType noteType = noteController.noteData.noteType;
		this._cutoutAnimateEffect.ResetEffect();
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0000747E File Offset: 0x0000567E
	private void HandleNoteDidStartDissolvingEvent(NoteController noteController, float duration)
	{
		this.AnimateCutout(0f, 1f, duration);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00007491 File Offset: 0x00005691
	private void AnimateCutout(float cutoutStart, float cutoutEnd, float duration)
	{
		if (this._cutoutAnimateEffect.animating)
		{
			return;
		}
		this._cutoutAnimateEffect.AnimateCutout(cutoutStart, cutoutEnd, duration);
	}

	// Token: 0x0400099A RID: 2458
	[SerializeField]
	private NoteController _noteController;

	// Token: 0x0400099B RID: 2459
	[Space]
	[SerializeField]
	private CutoutAnimateEffect _cutoutAnimateEffect;
}
