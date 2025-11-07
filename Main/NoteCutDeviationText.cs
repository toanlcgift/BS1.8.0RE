using System;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x020002B0 RID: 688
public class NoteCutDeviationText : MonoBehaviour
{
	// Token: 0x06000B9E RID: 2974 RVA: 0x000092C9 File Offset: 0x000074C9
	protected void Start()
	{
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCut;
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x000092E2 File Offset: 0x000074E2
	protected void OnDestroy()
	{
		this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00034E04 File Offset: 0x00033004
	private void HandleNoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		this._text.text = noteCutInfo.timeDeviation.ToString();
	}

	// Token: 0x04000C52 RID: 3154
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000C53 RID: 3155
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;
}
