using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000593 RID: 1427
	public class WaveformImage : MonoBehaviour
	{
		// Token: 0x06001BF0 RID: 7152 RVA: 0x0005F7D8 File Offset: 0x0005D9D8
		public void SetDataFromAudioClip(AudioClip audioClip)
		{
			if (audioClip == null)
			{
				if (this._texture != null)
				{
					UnityEngine.Object.Destroy(this._texture);
				}
				this._texture = null;
				this._sampleData = null;
				this._textureData = null;
				this._image.materialForRendering.SetTexture("_MainTex2", null);
				return;
			}
			this._sampleData = this.GetSampleData(audioClip);
			this._samplingFrequency = (float)(this._sampleData.Length / 2) / audioClip.length;
			this.DrawAtSongTime(this._beatmapEditorScrollView.scrollPositionSongTime);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00014ABC File Offset: 0x00012CBC
		public void ChangeParams(float timeOffset)
		{
			this._timeOffset = timeOffset;
			this.DrawAtSongTime(this._beatmapEditorScrollView.scrollPositionSongTime);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00014AD6 File Offset: 0x00012CD6
		protected virtual void Awake()
		{
			this._scrollRect = this._beatmapEditorScrollView.scrollRect;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00014AE9 File Offset: 0x00012CE9
		protected virtual void Start()
		{
			this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.ScrollViewDidScroll));
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00014B07 File Offset: 0x00012D07
		protected virtual void OnDestroy()
		{
			this._scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.ScrollViewDidScroll));
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00014B25 File Offset: 0x00012D25
		private void ScrollViewDidScroll(Vector2 normalizedPos)
		{
			this.DrawAtSongTime(this._beatmapEditorScrollView.scrollPositionSongTime);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0005F86C File Offset: 0x0005DA6C
		public void DrawAtSongTime(float songTime)
		{
			if (this._sampleData == null)
			{
				return;
			}
			Rect rect = this._image.rectTransform.rect;
			int num = (int)(RectTransformUtility.PixelAdjustRect(this._image.rectTransform, this._image.canvas.rootCanvas).height * this._image.canvas.scaleFactor + 0.5f);
			if (this._texture == null || this._texture.width != num)
			{
				this._textureData = new Color32[num];
				this._texture = new Texture2D(this._textureData.Length, 1, TextureFormat.RGBA32, false, true);
				this._texture.wrapMode = TextureWrapMode.Clamp;
				this._texture.filterMode = FilterMode.Point;
			}
			int num2 = (int)((songTime - this._beatmapEditorScrollView.playHeadSongTimeOffset + this._timeOffset) * this._samplingFrequency) * 2;
			int num3 = (int)(this._beatmapEditorScrollView.visibleAreaTimeDuration * this._samplingFrequency);
			byte b = 0;
			byte b2 = 0;
			byte b3 = byte.MaxValue;
			byte b4 = byte.MaxValue;
			float num4 = 0f;
			int i = 0;
			bool flag = false;
			for (int j = 0; j < num3; j++)
			{
				num4 += (float)this._textureData.Length / (float)num3;
				if (num4 <= 1f)
				{
					goto IL_198;
				}
				if (flag)
				{
					this._textureData[i] = new Color32(b, b2, b3, b4);
				}
				else
				{
					this._textureData[i] = new Color32(127, 127, 127, 127);
				}
				num4 -= 1f;
				b = 0;
				b2 = 0;
				b3 = byte.MaxValue;
				b4 = byte.MaxValue;
				flag = false;
				i++;
				if (i < this._textureData.Length)
				{
					goto IL_198;
				}
				IL24A:
				while (i < this._textureData.Length)
				{
					this._textureData[i] = new Color32(127, 127, 127, 127);
					i++;
				}
				this._texture.SetPixels32(this._textureData);
				this._texture.Apply();
				this._image.materialForRendering.SetTexture("_MainTex2", this._texture);
				return;
				IL_198:
				if (num2 < 0)
				{
					num2 += 2;
				}
				else
				{
					flag = true;
					byte b5 = (byte)((this._sampleData[num2] + 1f) / 2f * 255f);
					if (b5 > b)
					{
						b = b5;
					}
					if (b5 < b3)
					{
						b3 = b5;
					}
					num2++;
					b5 = (byte)((this._sampleData[num2] + 1f) / 2f * 255f);
					if (b5 > b2)
					{
						b2 = b5;
					}
					if (b5 < b4)
					{
						b4 = b5;
					}
					num2++;
					if (num2 >= this._sampleData.Length)
					{
						break;
					}
				}
			}
			//goto IL24A;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0005FB08 File Offset: 0x0005DD08
		private float[] GetSampleData(AudioClip audioClip)
		{
			if (audioClip == null)
			{
				return null;
			}
			float[] array = new float[audioClip.samples * 2];
			float[] array2 = new float[audioClip.samples * audioClip.channels];
			audioClip.GetData(array2, 0);
			if (audioClip.channels > 1)
			{
				for (int i = 0; i < audioClip.samples; i++)
				{
					array[i * 2] = array2[i * audioClip.channels];
					array[i * 2 + 1] = array2[i * audioClip.channels + 1];
				}
			}
			else
			{
				for (int j = 0; j < audioClip.samples; j++)
				{
					array[j * 2] = (array[j * 2 + 1] = array2[j]);
				}
			}
			return array;
		}

		// Token: 0x04001A70 RID: 6768
		[SerializeField]
		private BeatmapEditorScrollView _beatmapEditorScrollView;

		// Token: 0x04001A71 RID: 6769
		[SerializeField]
		private Image _image;

		// Token: 0x04001A72 RID: 6770
		private ScrollRect _scrollRect;

		// Token: 0x04001A73 RID: 6771
		private Texture2D _texture;

		// Token: 0x04001A74 RID: 6772
		private Color32[] _textureData;

		// Token: 0x04001A75 RID: 6773
		private float[] _sampleData;

		// Token: 0x04001A76 RID: 6774
		private float _samplingFrequency;

		// Token: 0x04001A77 RID: 6775
		private float _timeOffset;
	}
}
