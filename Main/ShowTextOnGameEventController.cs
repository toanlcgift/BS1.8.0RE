using System;
using Polyglot;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class ShowTextOnGameEventController : MonoBehaviour
{
	// Token: 0x06000C96 RID: 3222 RVA: 0x00036D68 File Offset: 0x00034F68
	protected void Awake()
	{
		ShowTextOnGameEventController.EventTextBinding[] eventTextBindings = this._eventTextBindings;
		for (int i = 0; i < eventTextBindings.Length; i++)
		{
			eventTextBindings[i].Init(this._textFadeTransitions);
		}
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x00036D98 File Offset: 0x00034F98
	protected void OnDestroy()
	{
		ShowTextOnGameEventController.EventTextBinding[] eventTextBindings = this._eventTextBindings;
		for (int i = 0; i < eventTextBindings.Length; i++)
		{
			eventTextBindings[i].Deinit();
		}
	}

	// Token: 0x04000D1A RID: 3354
	[SerializeField]
	private TextFadeTransitions _textFadeTransitions;

	// Token: 0x04000D1B RID: 3355
	[SerializeField]
	private ShowTextOnGameEventController.EventTextBinding[] _eventTextBindings;

	// Token: 0x020002E6 RID: 742
	[Serializable]
	private class EventTextBinding
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x00009CF2 File Offset: 0x00007EF2
		public void Init(TextFadeTransitions textFadeTransitions)
		{
			this._textFadeTransitions = textFadeTransitions;
			this._signal.Subscribe(new Action(this.HandleGameEvent));
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00009D12 File Offset: 0x00007F12
		public void Deinit()
		{
			this._signal.Unsubscribe(new Action(this.HandleGameEvent));
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00009D2B File Offset: 0x00007F2B
		private void HandleGameEvent()
		{
			this._textFadeTransitions.ShowText(Localization.Get(this._text));
		}

		// Token: 0x04000D1C RID: 3356
		[SerializeField]
		private Signal _signal;

		// Token: 0x04000D1D RID: 3357
		[TextArea(2, 2)]
		[SerializeField]
		private string _text;

		// Token: 0x04000D1E RID: 3358
		private TextFadeTransitions _textFadeTransitions;
	}
}
