using System;
using HMUI;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class StartMiddleEndButtonBackgroundController : MonoBehaviour
{
	// Token: 0x06000F4F RID: 3919 RVA: 0x0000BC7D File Offset: 0x00009E7D
	public void SetStartSprite()
	{
		this._image.sprite = this._startSprite;
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x0000BC90 File Offset: 0x00009E90
	public void SetMiddleSprite()
	{
		this._image.sprite = this._middleSprite;
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x0000BCA3 File Offset: 0x00009EA3
	public void SetEndSprite()
	{
		this._image.sprite = this._endSprite;
	}

	// Token: 0x04000FBC RID: 4028
	[SerializeField]
	private Sprite _startSprite;

	// Token: 0x04000FBD RID: 4029
	[SerializeField]
	private Sprite _middleSprite;

	// Token: 0x04000FBE RID: 4030
	[SerializeField]
	private Sprite _endSprite;

	// Token: 0x04000FBF RID: 4031
	[SerializeField]
	private ImageView _image;
}
