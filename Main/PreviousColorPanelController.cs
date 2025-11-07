using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000351 RID: 849
public class PreviousColorPanelController : MonoBehaviour
{
	// Token: 0x1400007D RID: 125
	// (add) Token: 0x06000EEA RID: 3818 RVA: 0x0003C714 File Offset: 0x0003A914
	// (remove) Token: 0x06000EEB RID: 3819 RVA: 0x0003C74C File Offset: 0x0003A94C
	public event Action<Color> colorWasSelectedEvent;

	// Token: 0x06000EEC RID: 3820 RVA: 0x0000B748 File Offset: 0x00009948
	protected void Awake()
	{
		this._buttonBinder = new ButtonBinder();
		this._buttonBinder.AddBinding(this._button, delegate
		{
			Action<Color> action = this.colorWasSelectedEvent;
			if (action == null)
			{
				return;
			}
			action(this._graphicsColor);
		});
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x0000B772 File Offset: 0x00009972
	protected void OnDestroy()
	{
		this._buttonBinder.ClearBindings();
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x0003C784 File Offset: 0x0003A984
	public void AddColor(Color color)
	{
		this._graphicsColor = this._color;
		Graphic[] graphics = this._graphics;
		for (int i = 0; i < graphics.Length; i++)
		{
			graphics[i].color = this._graphicsColor;
		}
		this._color = color;
	}

	// Token: 0x04000F48 RID: 3912
	[SerializeField]
	private Graphic[] _graphics;

	// Token: 0x04000F49 RID: 3913
	[SerializeField]
	private Button _button;

	// Token: 0x04000F4B RID: 3915
	private const int kMaxColors = 2;

	// Token: 0x04000F4C RID: 3916
	private ButtonBinder _buttonBinder;

	// Token: 0x04000F4D RID: 3917
	private Color _color = Color.black;

	// Token: 0x04000F4E RID: 3918
	private Color _graphicsColor = Color.black;
}
