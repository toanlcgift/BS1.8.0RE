using System;
using UnityEngine;
using Zenject;

// Token: 0x02000431 RID: 1073
public class ColorManager : MonoBehaviour
{
	// Token: 0x140000C6 RID: 198
	// (add) Token: 0x06001481 RID: 5249 RVA: 0x0004B1B4 File Offset: 0x000493B4
	// (remove) Token: 0x06001482 RID: 5250 RVA: 0x0004B1EC File Offset: 0x000493EC
	public event Action colorsDidChangeEvent;

	// Token: 0x06001483 RID: 5251 RVA: 0x0000F756 File Offset: 0x0000D956
	protected void Awake()
	{
		if (this._colorScheme == null)
		{
			this._colorScheme = this._defaultColorScheme.colorScheme;
		}
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x0004B224 File Offset: 0x00049424
	protected void Start()
	{
		this._saberAColor.SetColor(this._colorScheme.saberAColor);
		this._saberBColor.SetColor(this._colorScheme.saberBColor);
		this._environmentColor0.SetColor(this._colorScheme.environmentColor0);
		this._environmentColor1.SetColor(this._colorScheme.environmentColor1);
		this._obstaclesColor.SetColor(this._colorScheme.obstaclesColor);
		Action action = this.colorsDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x0000F771 File Offset: 0x0000D971
	public Color ColorForNoteType(NoteType type)
	{
		if (type == NoteType.NoteB)
		{
			return this._colorScheme.saberBColor;
		}
		return this._colorScheme.saberAColor;
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x0000F771 File Offset: 0x0000D971
	public Color ColorForSaberType(SaberType type)
	{
		if (type == SaberType.SaberB)
		{
			return this._colorScheme.saberBColor;
		}
		return this._colorScheme.saberAColor;
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x0004B2B0 File Offset: 0x000494B0
	public Color EffectsColorForSaberType(SaberType type)
	{
		Color rgbColor;
		if (type == SaberType.SaberB)
		{
			rgbColor = this._colorScheme.saberBColor;
		}
		else
		{
			rgbColor = this._colorScheme.saberAColor;
		}
		float h;
		float s;
		float v;
		Color.RGBToHSV(rgbColor, out h, out s, out v);
		v = 1f;
		return Color.HSVToRGB(h, s, v);
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x0004B2F8 File Offset: 0x000494F8
	public Color GetObstacleEffectColor()
	{
		float h;
		float s;
		float v;
		Color.RGBToHSV(this._colorScheme.obstaclesColor, out h, out s, out v);
		v = 1f;
		return Color.HSVToRGB(h, s, v);
	}

	// Token: 0x04001431 RID: 5169
	[SerializeField]
	private ColorSchemeSO _defaultColorScheme;

	// Token: 0x04001432 RID: 5170
	[Space]
	[SerializeField]
	private SimpleColorSO _saberAColor;

	// Token: 0x04001433 RID: 5171
	[SerializeField]
	private SimpleColorSO _saberBColor;

	// Token: 0x04001434 RID: 5172
	[SerializeField]
	private SimpleColorSO _environmentColor0;

	// Token: 0x04001435 RID: 5173
	[SerializeField]
	private SimpleColorSO _environmentColor1;

	// Token: 0x04001436 RID: 5174
	[SerializeField]
	private SimpleColorSO _obstaclesColor;

	// Token: 0x04001437 RID: 5175
	[InjectOptional]
	private ColorScheme _colorScheme;
}
