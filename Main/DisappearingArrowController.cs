using System;
using UnityEngine;

// Token: 0x02000242 RID: 578
public class DisappearingArrowController : MonoBehaviour, IManualUpdate
{
	// Token: 0x06000951 RID: 2385 RVA: 0x0002D474 File Offset: 0x0002B674
	protected void Awake()
	{
		this._initialSpriteAlphas = new float[this._spriteRenderers.Length];
		for (int i = 0; i < this._initialSpriteAlphas.Length; i++)
		{
			this._initialSpriteAlphas[i] = this._spriteRenderers[i].color.a;
		}
		this._colorNoteVisuals.didInitEvent += this.HandleColorNoteVisualsDidInitEvent;
		this._noteMovement.didInitEvent += this.HandleNoteMovementDidInit;
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
	protected void OnDestroy()
	{
		if (this._colorNoteVisuals != null)
		{
			this._colorNoteVisuals.didInitEvent -= this.HandleColorNoteVisualsDidInitEvent;
		}
		if (this._noteMovement != null)
		{
			this._noteMovement.didInitEvent -= this.HandleNoteMovementDidInit;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0002D548 File Offset: 0x0002B748
	public void ManualUpdate()
	{
		float arrowTransparency = Mathf.Clamp01((this._noteMovement.distanceToPlayer - this._minDistance) / (this._maxDistance - this._minDistance));
		this.SetArrowTransparency(arrowTransparency);
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0002D584 File Offset: 0x0002B784
	private void HandleNoteMovementDidInit()
	{
		this._maxDistance = Mathf.Min(this._noteMovement.moveEndPos.z * 0.8f, this._ghostNote ? this._disappearingGhostStart : this._disappearingNormalStart);
		this._minDistance = Mathf.Min(this._noteMovement.moveEndPos.z * 0.5f, this._ghostNote ? this._disappearingGhostEnd : this._disappearingNormalEnd);
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0002D600 File Offset: 0x0002B800
	private void HandleColorNoteVisualsDidInitEvent(ColorNoteVisuals colorNoteVisuals, NoteController noteController)
	{
		if (!this._initialized)
		{
			this._initialized = true;
			for (int i = 0; i < this._initialSpriteAlphas.Length; i++)
			{
				this._initialSpriteAlphas[i] = this._spriteRenderers[i].color.a;
			}
		}
		GameNoteController gameNoteController = noteController as GameNoteController;
		if (gameNoteController)
		{
			this._ghostNote = gameNoteController.ghostNote;
		}
		this._cubeMeshRenderer.enabled = !this._ghostNote;
		this.SetArrowTransparency(1f);
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0002D684 File Offset: 0x0002B884
	private void SetArrowTransparency(float arrowTransparency)
	{
		if (arrowTransparency == this._prevArrowTransparency)
		{
			return;
		}
		this._prevArrowTransparency = arrowTransparency;
		for (int i = 0; i < this._initialSpriteAlphas.Length; i++)
		{
			SpriteRenderer spriteRenderer = this._spriteRenderers[i];
			spriteRenderer.color = spriteRenderer.color.ColorWithAlpha(arrowTransparency * this._initialSpriteAlphas[i]);
		}
		if (this._arrowCutoutEffect.useRandomCutoutOffset)
		{
			this._arrowCutoutEffect.SetCutout(1f - arrowTransparency);
			return;
		}
		this._arrowCutoutEffect.SetCutout(1f - arrowTransparency, Vector3.zero);
	}

	// Token: 0x040009A8 RID: 2472
	[SerializeField]
	private ColorNoteVisuals _colorNoteVisuals;

	// Token: 0x040009A9 RID: 2473
	[SerializeField]
	private SpriteRenderer[] _spriteRenderers;

	// Token: 0x040009AA RID: 2474
	[SerializeField]
	private MeshRenderer _cubeMeshRenderer;

	// Token: 0x040009AB RID: 2475
	[SerializeField]
	private CutoutEffect _arrowCutoutEffect;

	// Token: 0x040009AC RID: 2476
	[SerializeField]
	private NoteMovement _noteMovement;

	// Token: 0x040009AD RID: 2477
	[SerializeField]
	private float _disappearingNormalStart = 14f;

	// Token: 0x040009AE RID: 2478
	[SerializeField]
	private float _disappearingNormalEnd = 8f;

	// Token: 0x040009AF RID: 2479
	[SerializeField]
	private float _disappearingGhostStart = 10f;

	// Token: 0x040009B0 RID: 2480
	[SerializeField]
	private float _disappearingGhostEnd = 4f;

	// Token: 0x040009B1 RID: 2481
	private float[] _initialSpriteAlphas;

	// Token: 0x040009B2 RID: 2482
	private bool _initialized;

	// Token: 0x040009B3 RID: 2483
	private bool _ghostNote;

	// Token: 0x040009B4 RID: 2484
	private float _prevArrowTransparency;

	// Token: 0x040009B5 RID: 2485
	private float _minDistance;

	// Token: 0x040009B6 RID: 2486
	private float _maxDistance;
}
