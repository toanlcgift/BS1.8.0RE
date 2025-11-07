using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class FlickeringNeonSign : MonoBehaviour
{
	// Token: 0x06000B21 RID: 2849 RVA: 0x00008B49 File Offset: 0x00006D49
	protected void Start()
	{
		this._spriteOnColor = this._flickeringSprite.color;
		this._lightOnColor = this._light.color;
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00008B6D File Offset: 0x00006D6D
	protected void OnEnable()
	{
		base.StartCoroutine(this.FlickeringCoroutine());
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00008B7C File Offset: 0x00006D7C
	private IEnumerator FlickeringCoroutine()
	{
		for (;;)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(this._minOnDelay, this._maxOnDelay));
			this.SetOn(false);
			yield return new WaitForSeconds(UnityEngine.Random.Range(this._minOffDelay, this._maxOffDelay));
			this.SetOn(true);
		}
		yield break;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00033B04 File Offset: 0x00031D04
	private void SetOn(bool on)
	{
		this._flickeringSprite.material = (on ? this._onMaterial : this._offMaterial);
		this._flickeringSprite.color = (on ? this._spriteOnColor : Color.black);
		this._light.color = (on ? this._lightOnColor : Color.black);
	}

	// Token: 0x04000BC1 RID: 3009
	[SerializeField]
	private SpriteRenderer _flickeringSprite;

	// Token: 0x04000BC2 RID: 3010
	[SerializeField]
	private TubeBloomPrePassLight _light;

	// Token: 0x04000BC3 RID: 3011
	[SerializeField]
	private float _minOnDelay = 0.05f;

	// Token: 0x04000BC4 RID: 3012
	[SerializeField]
	private float _maxOnDelay = 0.4f;

	// Token: 0x04000BC5 RID: 3013
	[SerializeField]
	private float _minOffDelay = 0.05f;

	// Token: 0x04000BC6 RID: 3014
	[SerializeField]
	private float _maxOffDelay = 0.4f;

	// Token: 0x04000BC7 RID: 3015
	[SerializeField]
	private Color _spriteOnColor;

	// Token: 0x04000BC8 RID: 3016
	[SerializeField]
	private Color _lightOnColor;

	// Token: 0x04000BC9 RID: 3017
	[SerializeField]
	private Material _onMaterial;

	// Token: 0x04000BCA RID: 3018
	[SerializeField]
	private Material _offMaterial;
}
