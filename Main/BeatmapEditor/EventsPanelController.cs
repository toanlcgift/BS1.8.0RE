using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x02000563 RID: 1379
	public class EventsPanelController : MonoBehaviour
	{
		// Token: 0x06001AD7 RID: 6871 RVA: 0x0005D33C File Offset: 0x0005B53C
		protected void Awake()
		{
			this._eventButtons = new List<EventsPanelButton>();
			HashSet<string> hashSet = new HashSet<string>();
			int num = 0;
			EventSetDrawStyleSO.SpecificEventDrawStyle[] specificEvents = this._eventSetDrawStyle.specificEvents;
			for (int i = 0; i < specificEvents.Length; i++)
			{
				EventDrawStyleSO eventDrawStyle = specificEvents[i].eventDrawStyle;
				if (eventDrawStyle != null && !hashSet.Contains(eventDrawStyle.eventID))
				{
					hashSet.Add(eventDrawStyle.eventID);
					RectTransform rectTransform = new GameObject("Panel").AddComponent<RectTransform>();
					rectTransform.SetParent(this._buttonContainerRectTransform, false);
					rectTransform.anchorMax = new Vector3(1f, 1f);
					rectTransform.anchorMin = new Vector3(0f, 1f);
					rectTransform.pivot = new Vector3(0f, 1f);
					rectTransform.anchoredPosition = new Vector2(0f, (float)(-(float)num) * (this._buttonHeight + this._verticalSeparator));
					rectTransform.sizeDelta = new Vector2(0f, this._buttonHeight);
					EventHeaderCell eventHeaderCell = this._eventHeaderCellFactory.Create();
					RectTransform rectTransform2 = (RectTransform)eventHeaderCell.transform;
					rectTransform2.transform.SetParent(rectTransform, false);
					rectTransform2.anchoredPosition = Vector3.zero;
					eventHeaderCell.image.sprite = eventDrawStyle.image;
					eventHeaderCell.hoverHint.text = eventDrawStyle.hintText;
					this.AddEventButtons(eventDrawStyle, rectTransform);
					num++;
				}
			}
			this.RefreshToggles();
			this._selectedBeatmapEventValues.selectedBeatmapEventValueDidChangeEvent += this.HandleSelectedBeatmapEventValuesDidChange;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00013DB3 File Offset: 0x00011FB3
		protected void OnDestroy()
		{
			this._selectedBeatmapEventValues.selectedBeatmapEventValueDidChangeEvent -= this.HandleSelectedBeatmapEventValuesDidChange;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0005D4E0 File Offset: 0x0005B6E0
		private void AddEventButtons(EventDrawStyleSO eventDrawStyle, Transform parentTransform)
		{
			ToggleGroup toggleGroup = parentTransform.gameObject.AddComponent<ToggleGroup>();
			int num = 0;
			EventDrawStyleSO.SubEventDrawStyle[] subEvents = eventDrawStyle.subEvents;
			for (int i = 0; i < subEvents.Length; i++)
			{
				EventDrawStyleSO.SubEventDrawStyle subEventDrawStyle = subEvents[i];
				EventsPanelButton eventsPanelButton = this._eventsPanelButtonFactory.Create();
				Action<bool> onValueChangedCallback = delegate(bool value)
				{
					this._selectedBeatmapEventValues.SetSelectedBeatmapEventValue(eventDrawStyle.eventID, subEventDrawStyle.eventValue);
				};
				if (subEventDrawStyle.image != null)
				{
					eventsPanelButton.Init(eventDrawStyle.eventID, subEventDrawStyle.eventValue, subEventDrawStyle.image, subEventDrawStyle.color, subEventDrawStyle.hintText, onValueChangedCallback, toggleGroup);
				}
				else
				{
					eventsPanelButton.Init(eventDrawStyle.eventID, subEventDrawStyle.eventValue, subEventDrawStyle.eventValue.ToString(), subEventDrawStyle.color, subEventDrawStyle.hintText, onValueChangedCallback, toggleGroup);
				}
				eventsPanelButton.gameObject.transform.SetParent(parentTransform);
				RectTransform component = eventsPanelButton.GetComponent<RectTransform>();
				component.localPosition = Vector3.zero;
				component.localRotation = Quaternion.identity;
				component.localScale = Vector3.one;
				component.anchoredPosition = new Vector2((float)(num + 2) * this._buttonWidth, 0f);
				num++;
				this._eventButtons.Add(eventsPanelButton);
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0005D684 File Offset: 0x0005B884
		private void RefreshToggles()
		{
			foreach (EventsPanelButton eventsPanelButton in this._eventButtons)
			{
				eventsPanelButton.callChangeCallback = false;
			}
			foreach (EventsPanelButton eventsPanelButton2 in this._eventButtons)
			{
				eventsPanelButton2.isOn = (eventsPanelButton2.eventValue == this._selectedBeatmapEventValues.GetEventValueForBeatmapEventWithID(eventsPanelButton2.eventID));
			}
			foreach (EventsPanelButton eventsPanelButton3 in this._eventButtons)
			{
				eventsPanelButton3.callChangeCallback = true;
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00013DCC File Offset: 0x00011FCC
		private void HandleSelectedBeatmapEventValuesDidChange()
		{
			this.RefreshToggles();
		}

		// Token: 0x040019A8 RID: 6568
		[SerializeField]
		private EventSetDrawStyleSO _eventSetDrawStyle;

		// Token: 0x040019A9 RID: 6569
		[SerializeField]
		private EditorSelectedBeatmapEventValues _selectedBeatmapEventValues;

		// Token: 0x040019AA RID: 6570
		[Space]
		[SerializeField]
		private RectTransform _buttonContainerRectTransform;

		// Token: 0x040019AB RID: 6571
		[SerializeField]
		private float _buttonWidth = 20f;

		// Token: 0x040019AC RID: 6572
		[SerializeField]
		private float _buttonHeight = 20f;

		// Token: 0x040019AD RID: 6573
		[SerializeField]
		private float _verticalSeparator = 2f;

		// Token: 0x040019AE RID: 6574
		[Inject]
		private EventsPanelButton.Factory _eventsPanelButtonFactory;

		// Token: 0x040019AF RID: 6575
		[Inject]
		private EventHeaderCell.Factory _eventHeaderCellFactory;

		// Token: 0x040019B0 RID: 6576
		private List<EventsPanelButton> _eventButtons;
	}
}
