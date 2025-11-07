using System;
using UnityEngine;
using Zenject;

// Token: 0x020002DE RID: 734
public class GameEnergyCounter : MonoBehaviour
{
	// Token: 0x14000055 RID: 85
	// (add) Token: 0x06000C6B RID: 3179 RVA: 0x0003669C File Offset: 0x0003489C
	// (remove) Token: 0x06000C6C RID: 3180 RVA: 0x000366D4 File Offset: 0x000348D4
	public event Action gameEnergyDidReach0Event;

	// Token: 0x14000056 RID: 86
	// (add) Token: 0x06000C6D RID: 3181 RVA: 0x0003670C File Offset: 0x0003490C
	// (remove) Token: 0x06000C6E RID: 3182 RVA: 0x00036744 File Offset: 0x00034944
	public event Action<float> gameEnergyDidChangeEvent;

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00009B22 File Offset: 0x00007D22
	// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00009B2A File Offset: 0x00007D2A
	public float energy { get; private set; }

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00009B33 File Offset: 0x00007D33
	public int batteryEnergy
	{
		get
		{
			return Mathf.FloorToInt(this.energy * (float)this.batteryLives + 0.5f);
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00009B4E File Offset: 0x00007D4E
	public int batteryLives
	{
		get
		{
			return this._batteryLives;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00009B56 File Offset: 0x00007D56
	// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00009B5E File Offset: 0x00007D5E
	public GameplayModifiers.EnergyType energyType { get; private set; }

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00009B67 File Offset: 0x00007D67
	// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00009B6F File Offset: 0x00007D6F
	public bool noFail { get; private set; }

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00009B78 File Offset: 0x00007D78
	// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00009B80 File Offset: 0x00007D80
	public bool instaFail { get; private set; }

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00009B89 File Offset: 0x00007D89
	// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00009B91 File Offset: 0x00007D91
	public bool failOnSaberClash { get; private set; }

	// Token: 0x06000C7B RID: 3195 RVA: 0x0003677C File Offset: 0x0003497C
	protected void Start()
	{
		this.energyType = this._initData.energyType;
		this.noFail = this._initData.noFail;
		this.instaFail = this._initData.instaFail;
		this.failOnSaberClash = this._initData.failOnSaberClash;
		if (this.energyType == GameplayModifiers.EnergyType.Battery)
		{
			this.energy = 1f;
		}
		else
		{
			this.energy = 0.5f;
		}
		if (this.instaFail)
		{
			this.energyType = GameplayModifiers.EnergyType.Battery;
			this._batteryLives = 1;
			this.energy = 1f;
		}
		Action<float> action = this.gameEnergyDidChangeEvent;
		if (action != null)
		{
			action(this.energy);
		}
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCutEvent;
		this._beatmapObjectManager.noteWasMissedEvent += this.HandleNoteWasMissedEvent;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00009B9A File Offset: 0x00007D9A
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCutEvent;
			this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissedEvent;
		}
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00036854 File Offset: 0x00034A54
	protected void Update()
	{
		if (this._playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0)
		{
			this.AddEnergy(Time.deltaTime * -this._obstacleEnergyDrainPerSecond);
		}
		if (this._saberClashChecker.sabersAreClashing && this.failOnSaberClash)
		{
			this.AddEnergy(-this.energy);
		}
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x000368AC File Offset: 0x00034AAC
	private void HandleNoteWasCutEvent(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		NoteType noteType = noteController.noteData.noteType;
		if (noteType != NoteType.Bomb && noteType != NoteType.NoteA && noteType != NoteType.NoteB)
		{
			return;
		}
		if (noteCutInfo.allIsOK)
		{
			this.AddEnergy(this._goodNoteEnergyCharge);
			return;
		}
		if (noteType == NoteType.Bomb)
		{
			this.AddEnergy(-this._hitBombEnergyDrain);
			return;
		}
		this.AddEnergy(-this._badNoteEnergyDrain);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00036908 File Offset: 0x00034B08
	private void HandleNoteWasMissedEvent(INoteController noteController)
	{
		NoteType noteType = noteController.noteData.noteType;
		if (noteType == NoteType.NoteA || noteType == NoteType.NoteB)
		{
			this.AddEnergy(-this._missNoteEnergyDrain);
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00036938 File Offset: 0x00034B38
	private void AddEnergy(float value)
	{
		if (this.noFail)
		{
			return;
		}
		if (value < 0f)
		{
			if (this.energy <= 0f)
			{
				return;
			}
			if (this.instaFail)
			{
				this.energy = 0f;
			}
			else if (this.energyType == GameplayModifiers.EnergyType.Battery)
			{
				this.energy -= 1f / (float)this._batteryLives;
			}
			else
			{
				this.energy += value;
			}
			if (this.energy <= 1E-05f)
			{
				this.energy = 0f;
				if (this.gameEnergyDidReach0Event != null)
				{
					this.gameEnergyDidReach0Event();
				}
			}
		}
		else if (this.energyType == GameplayModifiers.EnergyType.Bar)
		{
			if (this.energy >= 1f)
			{
				return;
			}
			this.energy += value;
			if (this.energy >= 1f)
			{
				this.energy = 1f;
			}
		}
		Action<float> action = this.gameEnergyDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this.energy);
	}

	// Token: 0x04000CEC RID: 3308
	[SerializeField]
	private float _badNoteEnergyDrain = 0.1f;

	// Token: 0x04000CED RID: 3309
	[SerializeField]
	private float _missNoteEnergyDrain = 0.1f;

	// Token: 0x04000CEE RID: 3310
	[SerializeField]
	private float _hitBombEnergyDrain = 0.15f;

	// Token: 0x04000CEF RID: 3311
	[SerializeField]
	private float _goodNoteEnergyCharge = 0.05f;

	// Token: 0x04000CF0 RID: 3312
	[SerializeField]
	private float _obstacleEnergyDrainPerSecond = 0.1f;

	// Token: 0x04000CF1 RID: 3313
	[SerializeField]
	private int _batteryLives = 4;

	// Token: 0x04000CF2 RID: 3314
	[Inject]
	private GameEnergyCounter.InitData _initData;

	// Token: 0x04000CF3 RID: 3315
	[Inject]
	private SaberClashChecker _saberClashChecker;

	// Token: 0x04000CF4 RID: 3316
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000CF5 RID: 3317
	[Inject]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	// Token: 0x020002DF RID: 735
	public class InitData
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public InitData(GameplayModifiers.EnergyType energyType, bool noFail, bool instaFail, bool failOnSaberClash)
		{
			this.energyType = energyType;
			this.noFail = noFail;
			this.instaFail = instaFail;
			this.failOnSaberClash = failOnSaberClash;
		}

		// Token: 0x04000CFD RID: 3325
		public readonly GameplayModifiers.EnergyType energyType;

		// Token: 0x04000CFE RID: 3326
		public readonly bool noFail;

		// Token: 0x04000CFF RID: 3327
		public readonly bool instaFail;

		// Token: 0x04000D00 RID: 3328
		public readonly bool failOnSaberClash;
	}
}
