using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020002AC RID: 684
public class GameEnergyUIPanel : MonoBehaviour
{
	// Token: 0x06000B89 RID: 2953 RVA: 0x00034764 File Offset: 0x00032964
	protected void Start()
	{
		this._gameEnergyCounter.gameEnergyDidChangeEvent += this.HandleGameEnergyDidChange;
		if (this._gameEnergyCounter.noFail)
		{
			this.Cleanup();
			base.gameObject.SetActive(false);
			return;
		}
		if (this._gameEnergyCounter.energyType == GameplayModifiers.EnergyType.Battery)
		{
			this._energyBar.gameObject.SetActive(false);
			this.CreateUIForBatteryEnergyType(this._gameEnergyCounter.batteryLives);
		}
		this.HandleGameEnergyDidChange(this._gameEnergyCounter.energy);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x000091A2 File Offset: 0x000073A2
	protected void OnDestroy()
	{
		this.Cleanup();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x000091AA File Offset: 0x000073AA
	private void Cleanup()
	{
		if (this._gameEnergyCounter != null)
		{
			this._gameEnergyCounter.gameEnergyDidChangeEvent -= this.HandleGameEnergyDidChange;
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x000347EC File Offset: 0x000329EC
	private void HandleGameEnergyDidChange(float energy)
	{
		if (this._gameEnergyCounter.energyType == GameplayModifiers.EnergyType.Battery)
		{
			int batteryEnergy = this._gameEnergyCounter.batteryEnergy;
			if (batteryEnergy < this._activeBatteryLifeSegmentsCount)
			{
				for (int i = batteryEnergy; i < this._activeBatteryLifeSegmentsCount; i++)
				{
					this._batteryLifeSegments[i].enabled = false;
				}
				this._activeBatteryLifeSegmentsCount = batteryEnergy;
				return;
			}
			if (batteryEnergy > this._activeBatteryLifeSegmentsCount)
			{
				for (int j = this._activeBatteryLifeSegmentsCount; j < batteryEnergy; j++)
				{
					this._batteryLifeSegments[j].enabled = true;
				}
				this._activeBatteryLifeSegmentsCount = batteryEnergy;
				return;
			}
		}
		else
		{
			this._energyBar.fillAmount = energy;
		}
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x00034888 File Offset: 0x00032A88
	public void CreateUIForBatteryEnergyType(int batteryLives)
	{
		this._batteryLifeSegments = new List<Image>(batteryLives);
		for (int i = 0; i < batteryLives; i++)
		{
			Image item = UnityEngine.Object.Instantiate<Image>(this._batteryLifeSegmentPrefab);
			this._batteryLifeSegments.Add(item);
		}
		Rect rect = this._energyBar.rectTransform.rect;
		float width = rect.width;
		float height = rect.height;
		float num = (width - 2f * this._batterySegmentHorizontalPadding - this._batterySegmentSeparatorWidth * (float)(batteryLives - 1)) / (float)batteryLives;
		for (int j = 0; j < batteryLives; j++)
		{
			RectTransform rectTransform = this._batteryLifeSegments[j].rectTransform;
			rectTransform.SetParent(base.transform, false);
			rectTransform.pivot = new Vector2(0f, 0.5f);
			rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			rectTransform.sizeDelta = new Vector2(num, height);
			rectTransform.anchoredPosition3D = new Vector3((num + this._batterySegmentSeparatorWidth) * (float)j + this._batterySegmentHorizontalPadding - width * 0.5f, -0.5f, 0f);
		}
		this._activeBatteryLifeSegmentsCount = batteryLives;
	}

	// Token: 0x04000C31 RID: 3121
	[SerializeField]
	private Image _energyBar;

	// Token: 0x04000C32 RID: 3122
	[Space]
	[SerializeField]
	private Image _batteryLifeSegmentPrefab;

	// Token: 0x04000C33 RID: 3123
	[Space]
	[SerializeField]
	private float _batterySegmentSeparatorWidth = 1f;

	// Token: 0x04000C34 RID: 3124
	[SerializeField]
	private float _batterySegmentHorizontalPadding = 1f;

	// Token: 0x04000C35 RID: 3125
	[Inject]
	private GameEnergyCounter _gameEnergyCounter;

	// Token: 0x04000C36 RID: 3126
	private List<Image> _batteryLifeSegments;

	// Token: 0x04000C37 RID: 3127
	private int _activeBatteryLifeSegmentsCount;
}
