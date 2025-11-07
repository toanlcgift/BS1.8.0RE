using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x02000438 RID: 1080
public class FullVRControllersRecorder : MonoBehaviour
{
	// Token: 0x140000C7 RID: 199
	// (add) Token: 0x060014AF RID: 5295 RVA: 0x0004B9A4 File Offset: 0x00049BA4
	// (remove) Token: 0x060014B0 RID: 5296 RVA: 0x0004B9DC File Offset: 0x00049BDC
	public event Action<VRController> didSetControllerTransformEvent;

	// Token: 0x060014B1 RID: 5297 RVA: 0x0004BA14 File Offset: 0x00049C14
	protected void Start()
	{
		if (this._recordingFilePath == "")
		{
			this._recordingFilePath = "Recordings/Recording.rd";
		}
		if (this._mode == FullVRControllersRecorder.Mode.Off)
		{
			base.enabled = false;
			return;
		}
		if (this._mode == FullVRControllersRecorder.Mode.Playback)
		{
			VRController[] controllers = this._controllers;
			for (int i = 0; i < controllers.Length; i++)
			{
				controllers[i].enabled = false;
			}
		}
		List<VRControllersRecorderData.NodeInfo> list = new List<VRControllersRecorderData.NodeInfo>(this._controllers.Length);
		foreach (VRController vrcontroller in this._controllers)
		{
			list.Add(new VRControllersRecorderData.NodeInfo(vrcontroller.node, vrcontroller.nodeIdx));
		}
		this._data = new VRControllersRecorderData(list.ToArray());
		if (this._mode == FullVRControllersRecorder.Mode.Playback)
		{
			VRControllersRecorderSaveAndLoad.LoadFromFile(this._recordingFilePath, this._data);
			if (this._data.numberOfKeyframes > 0)
			{
				foreach (VRController vrcontroller2 in this._controllers)
				{
					vrcontroller2.gameObject.SetActive(true);
					VRControllersRecorderData.PositionAndRotation positionAndRotation = this._data.GetPositionAndRotation(0, vrcontroller2.node, vrcontroller2.nodeIdx);
					vrcontroller2.transform.localPosition = positionAndRotation.pos;
					vrcontroller2.transform.localRotation = positionAndRotation.rot;
					Action<VRController> action = this.didSetControllerTransformEvent;
					if (action != null)
					{
						action(vrcontroller2);
					}
				}
				return;
			}
		}
		else if (this._mode == FullVRControllersRecorder.Mode.Record)
		{
			Debug.LogWarning("Recording performance.");
		}
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x0000F95F File Offset: 0x0000DB5F
	protected void OnDestroy()
	{
		if (this._mode == FullVRControllersRecorder.Mode.Record)
		{
			VRControllersRecorderSaveAndLoad.SaveToFile(this._recordingFilePath, this._data);
			Debug.Log("Performance saved to file " + this._recordingFilePath);
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x0004BB84 File Offset: 0x00049D84
	private void PlaybackTick()
	{
		float num = this._audioTimeSyncController.songTime + this._timeOffset;
		while (this._keyframeIndex < this._data.numberOfKeyframes - 2 && this._data.GetFrameTime(this._keyframeIndex + 1) < num)
		{
			this._keyframeIndex++;
		}
		float t = (num - this._data.GetFrameTime(this._keyframeIndex)) / Mathf.Max(1E-06f, this._data.GetFrameTime(this._keyframeIndex + 1) - this._data.GetFrameTime(this._keyframeIndex));
		foreach (VRController vrcontroller in this._controllers)
		{
			VRControllersRecorderData.PositionAndRotation lerpedPositionAndRotation = this._data.GetLerpedPositionAndRotation(this._keyframeIndex, t, vrcontroller.node, vrcontroller.nodeIdx);
			Vector3 vector = vrcontroller.position;
			Quaternion quaternion = vrcontroller.rotation;
			float t2;
			if (vrcontroller.node == XRNode.LeftHand || vrcontroller.node == XRNode.RightHand)
			{
				t2 = ((this._handsSmooth == 0f) ? 1f : (Time.deltaTime * this._handsSmooth));
			}
			else
			{
				t2 = ((this._othersSmooth == 0f) ? 1f : (Time.deltaTime * this._othersSmooth));
			}
			vector = Vector3.Lerp(vector, lerpedPositionAndRotation.pos + new Vector3(0f, this._playbackFloorOffset, 0f), t2);
			quaternion = Quaternion.Lerp(quaternion, lerpedPositionAndRotation.rot, t2);
			vrcontroller.transform.position = vector;
			vrcontroller.transform.rotation = quaternion;
			Action<VRController> action = this.didSetControllerTransformEvent;
			if (action != null)
			{
				action(vrcontroller);
			}
		}
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x0004BD48 File Offset: 0x00049F48
	private void RecordTick()
	{
		if (this._audioTimeSyncController.songTime == 0f)
		{
			return;
		}
		VRControllersRecorderData.PositionAndRotation[] array = new VRControllersRecorderData.PositionAndRotation[this._controllers.Length];
		for (int i = 0; i < this._controllers.Length; i++)
		{
			VRController vrcontroller = this._controllers[i];
			Vector3 position = vrcontroller.position;
			Quaternion rotation = vrcontroller.rotation;
			array[i] = new VRControllersRecorderData.PositionAndRotation(position, rotation);
		}
		this._data.AddKeyFrame(array, this._audioTimeSyncController.songTime);
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x0000F98F File Offset: 0x0000DB8F
	protected void Update()
	{
		if (this._mode == FullVRControllersRecorder.Mode.Playback && this._data != null && this._data.numberOfKeyframes > 1)
		{
			this.PlaybackTick();
		}
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x0000F9B6 File Offset: 0x0000DBB6
	protected void LateUpdate()
	{
		if (this._mode == FullVRControllersRecorder.Mode.Record)
		{
			this.RecordTick();
		}
	}

	// Token: 0x0400145F RID: 5215
	[SerializeField]
	private string _recordingFilePath = "Recording.dat";

	// Token: 0x04001460 RID: 5216
	[SerializeField]
	private FullVRControllersRecorder.Mode _mode = FullVRControllersRecorder.Mode.Off;

	// Token: 0x04001461 RID: 5217
	[SerializeField]
	private float _timeOffset;

	// Token: 0x04001462 RID: 5218
	[SerializeField]
	private float _othersSmooth = 8f;

	// Token: 0x04001463 RID: 5219
	[SerializeField]
	private float _handsSmooth = 8f;

	// Token: 0x04001464 RID: 5220
	[SerializeField]
	private float _playbackFloorOffset;

	// Token: 0x04001465 RID: 5221
	[Space]
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private VRController[] _controllers;

	// Token: 0x04001466 RID: 5222
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04001468 RID: 5224
	private int _keyframeIndex;

	// Token: 0x04001469 RID: 5225
	private VRControllersRecorderData _data;

	// Token: 0x02000439 RID: 1081
	public enum Mode
	{
		// Token: 0x0400146B RID: 5227
		Record,
		// Token: 0x0400146C RID: 5228
		Playback,
		// Token: 0x0400146D RID: 5229
		Off
	}
}
