using System;
using UnityEngine;
using Zenject;

// Token: 0x02000241 RID: 577
public class ColorNoteVisuals : MonoBehaviour
{
	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06000944 RID: 2372 RVA: 0x0002D2DC File Offset: 0x0002B4DC
	// (remove) Token: 0x06000945 RID: 2373 RVA: 0x0002D314 File Offset: 0x0002B514
	public event Action<ColorNoteVisuals, NoteController> didInitEvent;

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000946 RID: 2374 RVA: 0x0000754D File Offset: 0x0000574D
	public Color noteColor
	{
		get
		{
			return this._noteColor;
		}
	}

	// Token: 0x1700026C RID: 620
	// (set) Token: 0x06000947 RID: 2375 RVA: 0x00007555 File Offset: 0x00005755
	private bool showArrow
	{
		set
		{
			this._arrowMeshRenderer.enabled = value;
			this._arrowGlowSpriteRenderer.enabled = value;
		}
	}

	// Token: 0x1700026D RID: 621
	// (set) Token: 0x06000948 RID: 2376 RVA: 0x0000756F File Offset: 0x0000576F
	private bool showCircle
	{
		set
		{
			this._circleGlowSpriteRenderer.enabled = value;
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0002D34C File Offset: 0x0002B54C
	protected void Awake()
	{
		this._noteController.didInitEvent += this.HandleNoteControllerDidInitEvent;
		this._noteController.noteDidPassJumpThreeQuartersEvent += this.HandleNoteControllerNoteDidPassJumpThreeQuartersEvent;
		this._noteController.noteDidStartDissolvingEvent += this.HandleNoteDidStartDissolvingEvent;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0000757D File Offset: 0x0000577D
	protected void OnDestroy()
	{
		if (this._noteController)
		{
			this._noteController.didInitEvent -= this.HandleNoteControllerDidInitEvent;
			this._noteController.noteDidPassJumpThreeQuartersEvent -= this.HandleNoteControllerNoteDidPassJumpThreeQuartersEvent;
		}
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0002D3A0 File Offset: 0x0002B5A0
	public void HandleNoteControllerDidInitEvent(NoteController noteController)
	{
		NoteData noteData = noteController.noteData;
		NoteType noteType = noteData.noteType;
		if (noteData.cutDirection == NoteCutDirection.Any)
		{
			this.showArrow = false;
			this.showCircle = true;
		}
		else
		{
			this.showArrow = true;
			this.showCircle = false;
		}
		this._noteColor = this._colorManager.ColorForNoteType(noteType);
		this._arrowGlowSpriteRenderer.color = this._noteColor.ColorWithAlpha(this._arrowGlowIntensity);
		this._circleGlowSpriteRenderer.color = this._noteColor;
		foreach (MaterialPropertyBlockController materialPropertyBlockController in this._materialPropertyBlockControllers)
		{
			materialPropertyBlockController.materialPropertyBlock.SetColor(ColorNoteVisuals._colorID, this._noteColor.ColorWithAlpha(1f));
			materialPropertyBlockController.ApplyChanges();
		}
		Action<ColorNoteVisuals, NoteController> action = this.didInitEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._noteController);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000075BA File Offset: 0x000057BA
	private bool NoteIsOnDifferentSide(NoteData noteData)
	{
		return (noteData.noteType == NoteType.NoteA && noteData.lineIndex > 1) || (noteData.noteType == NoteType.NoteB && noteData.lineIndex < 2);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x000075E2 File Offset: 0x000057E2
	private void HandleNoteControllerNoteDidPassJumpThreeQuartersEvent(NoteController noteController)
	{
		this.showArrow = false;
		this.showCircle = false;
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x000075F2 File Offset: 0x000057F2
	private void HandleNoteDidStartDissolvingEvent(NoteController noteController, float duration)
	{
		this._arrowGlowSpriteRenderer.enabled = false;
		this._circleGlowSpriteRenderer.enabled = false;
	}

	// Token: 0x0400099E RID: 2462
	[SerializeField]
	private float _arrowGlowIntensity = 0.6f;

	// Token: 0x0400099F RID: 2463
	[Space]
	[SerializeField]
	private NoteController _noteController;

	// Token: 0x040009A0 RID: 2464
	[Space]
	[SerializeField]
	private SpriteRenderer _arrowGlowSpriteRenderer;

	// Token: 0x040009A1 RID: 2465
	[SerializeField]
	private SpriteRenderer _circleGlowSpriteRenderer;

	// Token: 0x040009A2 RID: 2466
	[SerializeField]
	private MaterialPropertyBlockController[] _materialPropertyBlockControllers;

	// Token: 0x040009A3 RID: 2467
	[SerializeField]
	private MeshRenderer _arrowMeshRenderer;

	// Token: 0x040009A4 RID: 2468
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x040009A6 RID: 2470
	[DoesNotRequireDomainReloadInit]
	private static readonly int _colorID = Shader.PropertyToID("_Color");

	// Token: 0x040009A7 RID: 2471
	private Color _noteColor;
}
