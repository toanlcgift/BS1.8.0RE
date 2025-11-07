using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200055D RID: 1373
	public class EditorPopUpInfoPanelController : MonoBehaviour
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x0005CED4 File Offset: 0x0005B0D4
		protected void OnEnable()
		{
			foreach (GameObject gameObject in this._activePopups)
			{
				UnityEngine.Object.Destroy(gameObject.gameObject);
			}
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00013C1E File Offset: 0x00011E1E
		public void ShowInfo(string text, EditorPopUpInfoPanelController.InfoType infoType)
		{
			base.StartCoroutine(this.ShowInfoCoroutine(text, infoType));
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00013C2F File Offset: 0x00011E2F
		private IEnumerator ShowInfoCoroutine(string text, EditorPopUpInfoPanelController.InfoType infoType)
		{
			EditorPopUpInfoPanel panel = UnityEngine.Object.Instantiate<EditorPopUpInfoPanel>(this._popupPanelPrefab, base.transform);
			this._activePopups.Add(panel.gameObject);
			panel.color = ((infoType == EditorPopUpInfoPanelController.InfoType.Info) ? this._infoColor : this._warningColor);
			panel.text = text;
			panel.anchoredPosition = this._hidePos;
			float duration = 0.2f;
			float elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				float num = elapsedTime / duration;
				panel.anchoredPosition = Vector2.Lerp(this._hidePos, this._showPos, -num * (num - 2f));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			panel.anchoredPosition = this._showPos;
			yield return new WaitForSeconds(3f);
			elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				float num2 = elapsedTime / duration;
				panel.anchoredPosition = Vector2.Lerp(this._showPos, this._hidePos, num2 * num2);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			panel.anchoredPosition = this._hidePos;
			this._activePopups.Remove(panel.gameObject);
			UnityEngine.Object.Destroy(panel.gameObject);
			yield break;
		}

		// Token: 0x0400198D RID: 6541
		[SerializeField]
		private EditorPopUpInfoPanel _popupPanelPrefab;

		// Token: 0x0400198E RID: 6542
		[SerializeField]
		private Vector2 _showPos;

		// Token: 0x0400198F RID: 6543
		[SerializeField]
		private Vector2 _hidePos;

		// Token: 0x04001990 RID: 6544
		[SerializeField]
		private Color _warningColor = Color.red;

		// Token: 0x04001991 RID: 6545
		[SerializeField]
		private Color _infoColor = Color.blue;

		// Token: 0x04001992 RID: 6546
		private List<GameObject> _activePopups = new List<GameObject>();

		// Token: 0x0200055E RID: 1374
		public enum InfoType
		{
			// Token: 0x04001994 RID: 6548
			Info,
			// Token: 0x04001995 RID: 6549
			Warning
		}
	}
}
