using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000510 RID: 1296
	public class KeyboardShortcut
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x000121CF File Offset: 0x000103CF
		public bool isContinuous
		{
			get
			{
				return this.keyPressType == KeyPressType.Holding;
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000121DA File Offset: 0x000103DA
		public KeyboardShortcut(KeyCode key1, KeyCode key2 = KeyCode.None, KeyCode modificationKey1 = KeyCode.None, KeyCode modificationKey2 = KeyCode.None, KeyPressType keyPressType = KeyPressType.Down)
		{
			this.key1 = key1;
			this.key2 = key2;
			this.modificationKey1 = modificationKey1;
			this.modificationKey2 = modificationKey2;
			this.keyPressType = keyPressType;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00012207 File Offset: 0x00010407
		public override int GetHashCode()
		{
			bool isContinuous = this.isContinuous;
			return (int)(this.key1 | this.key2 | this.modificationKey1 | this.modificationKey2 | (KeyCode)this.keyPressType);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000568C4 File Offset: 0x00054AC4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			KeyboardShortcut keyboardShortcut = obj as KeyboardShortcut;
			return keyboardShortcut != null && (keyboardShortcut.key1 == this.key1 && keyboardShortcut.key2 == this.key2 && keyboardShortcut.modificationKey1 == this.modificationKey1 && keyboardShortcut.modificationKey2 == this.modificationKey2) && keyboardShortcut.keyPressType == this.keyPressType;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0005692C File Offset: 0x00054B2C
		public override string ToString()
		{
			return string.Format("{0} + {1} + {2} + {3}   Key press type: {4}", new object[]
			{
				this.key1,
				this.key2,
				this.modificationKey1,
				this.modificationKey2,
				this.keyPressType
			});
		}

		// Token: 0x0400181A RID: 6170
		public KeyCode key1;

		// Token: 0x0400181B RID: 6171
		public KeyCode key2;

		// Token: 0x0400181C RID: 6172
		public KeyCode modificationKey1;

		// Token: 0x0400181D RID: 6173
		public KeyCode modificationKey2;

		// Token: 0x0400181E RID: 6174
		public KeyPressType keyPressType;
	}
}
