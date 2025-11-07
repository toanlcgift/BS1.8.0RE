using System;

namespace BeatmapEditor
{
	// Token: 0x02000525 RID: 1317
	public class ActiveCharacteristicDifficultySO : PersistentScriptableObject
	{
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06001936 RID: 6454 RVA: 0x000582F8 File Offset: 0x000564F8
		// (remove) Token: 0x06001937 RID: 6455 RVA: 0x00058330 File Offset: 0x00056530
		public event Action<BeatmapCharacteristicDifficulty, BeatmapCharacteristicDifficulty> didChangeEvent = delegate(BeatmapCharacteristicDifficulty p0, BeatmapCharacteristicDifficulty p1)
		{
		};

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00012B8D File Offset: 0x00010D8D
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x00058368 File Offset: 0x00056568
		public BeatmapCharacteristicDifficulty characteristicDifficulty
		{
			get
			{
				return this._characteristicDifficulty;
			}
			set
			{
				if (this._characteristicDifficulty == value)
				{
					return;
				}
				BeatmapCharacteristicDifficulty characteristicDifficulty = this._characteristicDifficulty;
				this._characteristicDifficulty = value;
				this.didChangeEvent(characteristicDifficulty, value);
			}
		}

		// Token: 0x0400187A RID: 6266
		private BeatmapCharacteristicDifficulty _characteristicDifficulty;
	}
}
