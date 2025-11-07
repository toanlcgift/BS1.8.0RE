using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000572 RID: 1394
	public class NotesTableCell : BeatmapEditorTableCell
	{
		// Token: 0x06001B21 RID: 6945 RVA: 0x0005E1C8 File Offset: 0x0005C3C8
		protected void Awake()
		{
			this._iconTransforms = new RectTransform[4];
			for (int i = 0; i < 4; i++)
			{
				this._iconTransforms[i] = this._icons[i].rectTransform;
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0001411C File Offset: 0x0001231C
		public void SetLineActive(int lineIndex, bool active)
		{
			this._backgrounds[lineIndex].gameObject.SetActive(active);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0005E204 File Offset: 0x0005C404
		public void SetLineType(int noteIdx, NoteType type, NoteCutDirection dir, bool highlight)
		{
			if (type == NoteType.NoteA || type == NoteType.NoteB)
			{
				if (!this._backgrounds[noteIdx].enabled)
				{
					this._backgrounds[noteIdx].enabled = true;
				}
				Color color = (type == NoteType.NoteA) ? Color.red : Color.blue;
				this._backgrounds[noteIdx].color = (highlight ? color.ColorWithAlpha(0.1f) : color);
				this._icons[noteIdx].sprite = this._arrowNoteSprite;
				this._icons[noteIdx].color = (highlight ? new Color(1f, 1f, 1f, 0.2f) : Color.white);
				float z = 0f;
				if (dir == NoteCutDirection.Up)
				{
					z = 0f;
				}
				else if (dir == NoteCutDirection.Down)
				{
					z = 180f;
				}
				else if (dir == NoteCutDirection.Left)
				{
					z = 90f;
				}
				else if (dir == NoteCutDirection.Right)
				{
					z = -90f;
				}
				else if (dir == NoteCutDirection.UpLeft)
				{
					z = 45f;
				}
				else if (dir == NoteCutDirection.UpRight)
				{
					z = -45f;
				}
				else if (dir == NoteCutDirection.DownLeft)
				{
					z = 135f;
				}
				else if (dir == NoteCutDirection.DownRight)
				{
					z = -135f;
				}
				if (dir == NoteCutDirection.Any)
				{
					z = 0f;
					if (this._icons[noteIdx].enabled)
					{
						this._icons[noteIdx].enabled = false;
					}
				}
				else if (!this._icons[noteIdx].enabled)
				{
					this._icons[noteIdx].enabled = true;
				}
				this._iconTransforms[noteIdx].localEulerAngles = new Vector3(0f, 0f, z);
				return;
			}
			if (this._backgrounds[noteIdx].enabled)
			{
				this._backgrounds[noteIdx].enabled = false;
			}
			if (!this._icons[noteIdx].enabled)
			{
				this._icons[noteIdx].enabled = true;
			}
			if (type == NoteType.GhostNote)
			{
				this._icons[noteIdx].sprite = this._ghostNoteSprite;
			}
			else if (type == NoteType.Bomb)
			{
				this._icons[noteIdx].sprite = this._bombNoteSprite;
			}
			this._iconTransforms[noteIdx].localEulerAngles = new Vector3(0f, 0f, 0f);
		}

		// Token: 0x040019E1 RID: 6625
		[SerializeField]
		private Image[] _backgrounds;

		// Token: 0x040019E2 RID: 6626
		[SerializeField]
		private Image[] _icons;

		// Token: 0x040019E3 RID: 6627
		[SerializeField]
		private Sprite _arrowNoteSprite;

		// Token: 0x040019E4 RID: 6628
		[SerializeField]
		private Sprite _ghostNoteSprite;

		// Token: 0x040019E5 RID: 6629
		[SerializeField]
		private Sprite _bombNoteSprite;

		// Token: 0x040019E6 RID: 6630
		private RectTransform[] _iconTransforms;
	}
}
