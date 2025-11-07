using System;
using TMPro;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class MissionStageLockView : MonoBehaviour
{
	// Token: 0x0600112E RID: 4398 RVA: 0x00041FF0 File Offset: 0x000401F0
	public void UpdateLocalPositionY(float dstPosY, bool animated, float animationDuration)
	{
		this._dstPosY = dstPosY;
		if (!animated)
		{
			this._rectTransform.localPosition = new Vector3(0f, dstPosY, 0f);
			return;
		}
		this._startAnimationTime = Time.time;
		this._animationDuration = animationDuration;
		base.enabled = true;
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x0004203C File Offset: 0x0004023C
	protected void Update()
	{
		float time = Time.time;
		float y;
		if (time > this._startAnimationTime + this._animationDuration || this._animationDuration == 0f)
		{
			y = this._dstPosY;
			base.enabled = false;
		}
		else
		{
			float t = (time - this._startAnimationTime) / this._animationDuration;
			y = Mathf.Lerp(this._rectTransform.transform.localPosition.y, this._dstPosY, t);
		}
		this._rectTransform.transform.localPosition = new Vector3(0f, y, 0f);
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
	public void UpdateStageLockText(string text)
	{
		this._text.text = text;
	}

	// Token: 0x04001114 RID: 4372
	[SerializeField]
	private TMP_Text _text;

	// Token: 0x04001115 RID: 4373
	[SerializeField]
	private RectTransform _rectTransform;

	// Token: 0x04001116 RID: 4374
	private float _dstPosY;

	// Token: 0x04001117 RID: 4375
	private float _animationDuration;

	// Token: 0x04001118 RID: 4376
	private float _startAnimationTime;
}
