using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000366 RID: 870
public class StartMiddleEndButtonsGroup : MonoBehaviour, ILayoutController
{
	// Token: 0x06000F53 RID: 3923 RVA: 0x0003D974 File Offset: 0x0003BB74
	public void SetLayoutHorizontal()
	{
		StartMiddleEndButtonBackgroundController[] componentsInChildren = base.transform.GetComponentsInChildren<StartMiddleEndButtonBackgroundController>(false);
		if (componentsInChildren.Length == 1)
		{
			componentsInChildren[0].SetMiddleSprite();
			return;
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (i == 0)
			{
				componentsInChildren[i].SetStartSprite();
			}
			else if (i == componentsInChildren.Length - 1)
			{
				componentsInChildren[i].SetEndSprite();
			}
			else
			{
				componentsInChildren[i].SetMiddleSprite();
			}
		}
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x000023E9 File Offset: 0x000005E9
	public void SetLayoutVertical()
	{
	}
}
