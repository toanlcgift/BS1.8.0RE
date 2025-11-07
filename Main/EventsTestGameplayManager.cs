using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x020002B7 RID: 695
public class EventsTestGameplayManager : MonoBehaviour
{
	// Token: 0x06000BC2 RID: 3010 RVA: 0x000352FC File Offset: 0x000334FC
	protected void Awake()
	{
		this._beatmapEventTypeBindings = new Dictionary<KeyCode, BeatmapEventType>
		{
			{
				KeyCode.Alpha1,
				BeatmapEventType.Event0
			},
			{
				KeyCode.Alpha2,
				BeatmapEventType.Event1
			},
			{
				KeyCode.Alpha3,
				BeatmapEventType.Event2
			},
			{
				KeyCode.Alpha4,
				BeatmapEventType.Event3
			},
			{
				KeyCode.Alpha5,
				BeatmapEventType.Event4
			},
			{
				KeyCode.Alpha6,
				BeatmapEventType.Event5
			},
			{
				KeyCode.Alpha7,
				BeatmapEventType.Event6
			},
			{
				KeyCode.Alpha8,
				BeatmapEventType.Event7
			},
			{
				KeyCode.Alpha9,
				BeatmapEventType.Event8
			},
			{
				KeyCode.Alpha0,
				BeatmapEventType.Event9
			}
		};
		this._beatmapValuesBindings = new Dictionary<KeyCode, int>
		{
			{
				KeyCode.Keypad0,
				0
			},
			{
				KeyCode.Keypad1,
				1
			},
			{
				KeyCode.Keypad2,
				2
			},
			{
				KeyCode.Keypad3,
				3
			},
			{
				KeyCode.Keypad4,
				4
			},
			{
				KeyCode.Keypad5,
				5
			},
			{
				KeyCode.Keypad6,
				6
			},
			{
				KeyCode.Keypad7,
				7
			}
		};
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x000353DC File Offset: 0x000335DC
	protected void Update()
	{
		foreach (KeyValuePair<KeyCode, BeatmapEventType> keyValuePair in this._beatmapEventTypeBindings)
		{
			if (Input.GetKeyDown(keyValuePair.Key))
			{
				this._beatmapEventType = keyValuePair.Value;
			}
		}
		foreach (KeyValuePair<KeyCode, int> keyValuePair2 in this._beatmapValuesBindings)
		{
			if (Input.GetKeyDown(keyValuePair2.Key))
			{
				BeatmapEventData beatmapEventData = new BeatmapEventData(0f, this._beatmapEventType, keyValuePair2.Value);
				this._beatmapObjectCallbackController.SendBeatmapEventDidTriggerEvent(beatmapEventData);
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			int value = 0;
			this._rotatingLasers = !this._rotatingLasers;
			if (this._rotatingLasers)
			{
				value = 4;
			}
			this._beatmapObjectCallbackController.SendBeatmapEventDidTriggerEvent(new BeatmapEventData(0f, BeatmapEventType.Event12, value));
			this._beatmapObjectCallbackController.SendBeatmapEventDidTriggerEvent(new BeatmapEventData(0f, BeatmapEventType.Event13, value));
		}
	}

	// Token: 0x04000C75 RID: 3189
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000C76 RID: 3190
	private BeatmapEventType _beatmapEventType;

	// Token: 0x04000C77 RID: 3191
	private Dictionary<KeyCode, BeatmapEventType> _beatmapEventTypeBindings;

	// Token: 0x04000C78 RID: 3192
	private Dictionary<KeyCode, int> _beatmapValuesBindings;

	// Token: 0x04000C79 RID: 3193
	private bool _rotatingLasers;
}
