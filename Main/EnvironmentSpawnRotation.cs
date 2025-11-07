using System;
using UnityEngine;
using Zenject;

// Token: 0x02000293 RID: 659
public class EnvironmentSpawnRotation : MonoBehaviour
{
	// Token: 0x14000048 RID: 72
	// (add) Token: 0x06000B16 RID: 2838 RVA: 0x00033960 File Offset: 0x00031B60
	// (remove) Token: 0x06000B17 RID: 2839 RVA: 0x00033998 File Offset: 0x00031B98
	public event Action<Quaternion> didRotateEvent;

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00008AEA File Offset: 0x00006CEA
	public float targetRotation
	{
		get
		{
			return this._spawnRotationProcessor.rotation;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00008AF7 File Offset: 0x00006CF7
	public float currentRotation
	{
		get
		{
			return this._currentRotation;
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00008AFF File Offset: 0x00006CFF
	protected void Awake()
	{
		this._currentRotation = this._spawnRotationProcessor.rotation;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x000339D0 File Offset: 0x00031BD0
	protected void Start()
	{
		Shader.SetGlobalMatrix(EnvironmentSpawnRotation._spawnRotationID, Matrix4x4.identity);
		this._eventCallbackData = this._beatmapObjectCallbackController.AddBeatmapEventCallback(new BeatmapObjectCallbackController.BeatmapEventCallback(this.BeatmapEventAtNoteSpawnCallback), this._aheadTime);
		if (this._smooth == 0f)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00008B12 File Offset: 0x00006D12
	protected void OnDestroy()
	{
		this._beatmapObjectCallbackController.RemoveBeatmapEventCallback(this._eventCallbackData);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00033A24 File Offset: 0x00031C24
	private void BeatmapEventAtNoteSpawnCallback(BeatmapEventData beatmapEventData)
	{
		if (!this._spawnRotationProcessor.ProcessBeatmapEventData(beatmapEventData))
		{
			return;
		}
		if (this._smooth == 0f)
		{
			this._currentRotation = this._spawnRotationProcessor.rotation;
			base.transform.localRotation = Quaternion.Euler(0f, this._currentRotation, 0f);
		}
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00033A80 File Offset: 0x00031C80
	protected void Update()
	{
		this._currentRotation = Mathf.Lerp(this._currentRotation, this._spawnRotationProcessor.rotation, Time.deltaTime * this._smooth);
		Quaternion obj = Quaternion.Euler(0f, this._currentRotation, 0f);
		Shader.SetGlobalMatrix(EnvironmentSpawnRotation._spawnRotationID, Matrix4x4.Rotate(Quaternion.Euler(0f, -this._currentRotation, 0f)));
		Action<Quaternion> action = this.didRotateEvent;
		if (action == null)
		{
			return;
		}
		action(obj);
	}

	// Token: 0x04000BB9 RID: 3001
	[SerializeField]
	private float _aheadTime;

	// Token: 0x04000BBA RID: 3002
	[SerializeField]
	private float _smooth;

	// Token: 0x04000BBB RID: 3003
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000BBC RID: 3004
	[DoesNotRequireDomainReloadInit]
	private static readonly int _spawnRotationID = Shader.PropertyToID("_SpawnRotation");

	// Token: 0x04000BBE RID: 3006
	private SpawnRotationProcessor _spawnRotationProcessor = new SpawnRotationProcessor();

	// Token: 0x04000BBF RID: 3007
	private BeatmapObjectCallbackController.BeatmapEventCallbackData _eventCallbackData;

	// Token: 0x04000BC0 RID: 3008
	private float _currentRotation;
}
