using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

// Token: 0x0200044F RID: 1103
public class VRTrackersRecorder : MonoBehaviour
{
	// Token: 0x060014F5 RID: 5365 RVA: 0x0004D510 File Offset: 0x0004B710
	protected void Awake()
	{
		if (this._saveFilename == "")
		{
			this._saveFilename = "VRControllersRecording.dat";
		}
		this._keyframes = new List<VRTrackersRecorder.Keyframe>();
		if (this._mode == VRTrackersRecorder.RecordMode.Playback)
		{
			this.Load();
		}
		else if (this._mode == VRTrackersRecorder.RecordMode.Record)
		{
			this._newPosesAction = SteamVR_Events.NewPosesAction(new UnityAction<TrackedDevicePose_t[]>(this.OnNewPoses));
			Debug.LogWarning("Recording performance.");
		}
		if (this._playbackTransforms.Length == 0)
		{
			Debug.LogWarning("No playback transforms in VR trackers recorder.");
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x0000FC4A File Offset: 0x0000DE4A
	protected void OnDestroy()
	{
		if (this._mode == VRTrackersRecorder.RecordMode.Record)
		{
			this.Save();
		}
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x0000FC5A File Offset: 0x0000DE5A
	protected void OnEnable()
	{
		if (this._newPosesAction != null && this._mode == VRTrackersRecorder.RecordMode.Record)
		{
			this._newPosesAction.enabled = true;
		}
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x0000FC78 File Offset: 0x0000DE78
	protected void OnDisable()
	{
		if (this._newPosesAction != null && this._mode == VRTrackersRecorder.RecordMode.Record)
		{
			this._newPosesAction.enabled = false;
		}
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x0004D594 File Offset: 0x0004B794
	private void OnNewPoses(TrackedDevicePose_t[] poses)
	{
		VRTrackersRecorder.Keyframe keyframe = new VRTrackersRecorder.Keyframe();
		keyframe._transforms = new VRTrackersRecorder.Keyframe.KeyframeTransform[poses.Length];
		for (int i = 0; i < poses.Length; i++)
		{
			VRTrackersRecorder.Keyframe.KeyframeTransform keyframeTransform = keyframe._transforms[i] = new VRTrackersRecorder.Keyframe.KeyframeTransform();
			if (poses[i].bDeviceIsConnected && poses[i].bPoseIsValid)
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);
				keyframeTransform._pos = rigidTransform.pos;
				keyframeTransform._rot = rigidTransform.rot;
				keyframeTransform._valid = true;
			}
			else
			{
				keyframeTransform._pos = Vector3.zero;
				keyframeTransform._rot = Quaternion.identity;
				keyframeTransform._valid = false;
			}
		}
		keyframe._time = this._songTime.value;
		this._keyframes.Add(keyframe);
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x0004D668 File Offset: 0x0004B868
	protected void Update()
	{
		if (this._mode != VRTrackersRecorder.RecordMode.Playback || this._keyframes.Count < 2)
		{
			return;
		}
		float value = this._songTime.value;
		while (this._keyframeIndex < this._keyframes.Count - 2 && this._keyframes[this._keyframeIndex + 1]._time < value)
		{
			this._keyframeIndex++;
		}
		Vector3 position = this._originTransform.position;
		Quaternion rotation = this._originTransform.rotation;
		VRTrackersRecorder.Keyframe keyframe = this._keyframes[this._keyframeIndex];
		VRTrackersRecorder.Keyframe keyframe2 = this._keyframes[this._keyframeIndex + 1];
		float t = (value - keyframe._time) / Mathf.Max(1E-06f, keyframe2._time - keyframe._time);
		for (int i = 0; i < this._playbackTransforms.Length; i++)
		{
			if (!(this._playbackTransforms[i] == null) && i < keyframe._transforms.Length && i < keyframe2._transforms.Length)
			{
				Vector3 pos = keyframe._transforms[i]._pos;
				Vector3 pos2 = keyframe2._transforms[i]._pos;
				Quaternion rot = keyframe._transforms[i]._rot;
				Quaternion rot2 = keyframe2._transforms[i]._rot;
				this._playbackTransforms[i].position = rotation * Vector3.Lerp(pos, pos2, t) + position;
				this._playbackTransforms[i].rotation = rotation * Quaternion.Slerp(rot, rot2, t);
			}
		}
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x0004D814 File Offset: 0x0004BA14
	private void Save()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Open(this._saveFilename, FileMode.OpenOrCreate);
		VRTrackersRecorder.SavedData savedData = new VRTrackersRecorder.SavedData();
		savedData._keyframes = new VRTrackersRecorder.SavedData.KeyframeSerializable[this._keyframes.Count];
		for (int i = 0; i < this._keyframes.Count; i++)
		{
			VRTrackersRecorder.Keyframe keyframe = this._keyframes[i];
			VRTrackersRecorder.SavedData.KeyframeSerializable keyframeSerializable = new VRTrackersRecorder.SavedData.KeyframeSerializable();
			keyframeSerializable._transforms = new VRTrackersRecorder.SavedData.KeyframeSerializable.TransformSerializable[keyframe._transforms.Length];
			for (int j = 0; j < keyframe._transforms.Length; j++)
			{
				VRTrackersRecorder.SavedData.KeyframeSerializable.TransformSerializable transformSerializable = keyframeSerializable._transforms[j] = new VRTrackersRecorder.SavedData.KeyframeSerializable.TransformSerializable();
				transformSerializable._valid = keyframeSerializable._transforms[j]._valid;
				Vector3 pos = keyframe._transforms[j]._pos;
				Quaternion rot = keyframe._transforms[j]._rot;
				transformSerializable._xPos = pos.x;
				transformSerializable._yPos = pos.y;
				transformSerializable._zPos = pos.z;
				transformSerializable._xRot = rot.x;
				transformSerializable._yRot = rot.y;
				transformSerializable._zRot = rot.z;
				transformSerializable._wRot = rot.w;
			}
			keyframeSerializable._time = keyframe._time;
			savedData._keyframes[i] = keyframeSerializable;
		}
		binaryFormatter.Serialize(fileStream, savedData);
		fileStream.Close();
		Debug.Log("Performance saved to file " + this._saveFilename);
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x0004D994 File Offset: 0x0004BB94
	private void Load()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = null;
		VRTrackersRecorder.SavedData savedData = null;
		try
		{
			fileStream = File.Open(this._saveFilename, FileMode.Open);
			savedData = (VRTrackersRecorder.SavedData)binaryFormatter.Deserialize(fileStream);
		}
		catch
		{
			savedData = null;
		}
		finally
		{
			if (fileStream != null)
			{
				fileStream.Close();
			}
		}
		if (savedData != null)
		{
			this._keyframes = new List<VRTrackersRecorder.Keyframe>(savedData._keyframes.Length);
			for (int i = 0; i < savedData._keyframes.Length; i++)
			{
				VRTrackersRecorder.SavedData.KeyframeSerializable keyframeSerializable = savedData._keyframes[i];
				VRTrackersRecorder.Keyframe keyframe = new VRTrackersRecorder.Keyframe();
				keyframe._transforms = new VRTrackersRecorder.Keyframe.KeyframeTransform[keyframeSerializable._transforms.Length];
				for (int j = 0; j < keyframe._transforms.Length; j++)
				{
					VRTrackersRecorder.SavedData.KeyframeSerializable.TransformSerializable transformSerializable = keyframeSerializable._transforms[j];
					keyframe._transforms[j] = new VRTrackersRecorder.Keyframe.KeyframeTransform();
					keyframe._transforms[j]._valid = transformSerializable._valid;
					keyframe._transforms[j]._pos = new Vector3(transformSerializable._xPos, transformSerializable._yPos, transformSerializable._zPos);
					keyframe._transforms[j]._rot = new Quaternion(transformSerializable._xRot, transformSerializable._yRot, transformSerializable._zRot, transformSerializable._wRot);
				}
				keyframe._time = keyframeSerializable._time;
				this._keyframes.Add(keyframe);
			}
			Debug.Log("Performance loaded from file " + this._saveFilename);
			return;
		}
		Debug.Log("Loading performance file failed (" + this._saveFilename + ")");
		base.enabled = false;
	}

	// Token: 0x040014D7 RID: 5335
	[SerializeField]
	private FloatSO _songTime;

	// Token: 0x040014D8 RID: 5336
	[SerializeField]
	private string _saveFilename = "VRControllersRecording.dat";

	// Token: 0x040014D9 RID: 5337
	[SerializeField]
	private VRTrackersRecorder.RecordMode _mode = VRTrackersRecorder.RecordMode.Off;

	// Token: 0x040014DA RID: 5338
	[SerializeField]
	private Transform _originTransform;

	// Token: 0x040014DB RID: 5339
	[SerializeField]
	private Transform[] _playbackTransforms;

	// Token: 0x040014DC RID: 5340
	private List<VRTrackersRecorder.Keyframe> _keyframes;

	// Token: 0x040014DD RID: 5341
	private int _keyframeIndex;

	// Token: 0x040014DE RID: 5342
	private SteamVR_Events.Action _newPosesAction;

	// Token: 0x040014DF RID: 5343
	private Vector3 _loadedOriginPos;

	// Token: 0x040014E0 RID: 5344
	private Quaternion _loadedOriginRot;

	// Token: 0x02000450 RID: 1104
	[Serializable]
	private class SavedData
	{
		// Token: 0x040014E1 RID: 5345
		public VRTrackersRecorder.SavedData.KeyframeSerializable[] _keyframes;

		// Token: 0x02000451 RID: 1105
		[Serializable]
		public class KeyframeSerializable
		{
			// Token: 0x040014E2 RID: 5346
			[SerializeField]
			public VRTrackersRecorder.SavedData.KeyframeSerializable.TransformSerializable[] _transforms;

			// Token: 0x040014E3 RID: 5347
			[SerializeField]
			public float _time;

			// Token: 0x02000452 RID: 1106
			[Serializable]
			public class TransformSerializable
			{
				// Token: 0x040014E4 RID: 5348
				[SerializeField]
				public float _xPos;

				// Token: 0x040014E5 RID: 5349
				[SerializeField]
				public float _yPos;

				// Token: 0x040014E6 RID: 5350
				[SerializeField]
				public float _zPos;

				// Token: 0x040014E7 RID: 5351
				[SerializeField]
				public float _xRot;

				// Token: 0x040014E8 RID: 5352
				[SerializeField]
				public float _yRot;

				// Token: 0x040014E9 RID: 5353
				[SerializeField]
				public float _zRot;

				// Token: 0x040014EA RID: 5354
				[SerializeField]
				public float _wRot;

				// Token: 0x040014EB RID: 5355
				[SerializeField]
				public bool _valid;
			}
		}
	}

	// Token: 0x02000453 RID: 1107
	private enum RecordMode
	{
		// Token: 0x040014ED RID: 5357
		Record,
		// Token: 0x040014EE RID: 5358
		Playback,
		// Token: 0x040014EF RID: 5359
		Off
	}

	// Token: 0x02000454 RID: 1108
	private class Keyframe
	{
		// Token: 0x040014F0 RID: 5360
		public VRTrackersRecorder.Keyframe.KeyframeTransform[] _transforms;

		// Token: 0x040014F1 RID: 5361
		public float _time;

		// Token: 0x02000455 RID: 1109
		public class KeyframeTransform
		{
			// Token: 0x040014F2 RID: 5362
			public Vector3 _pos;

			// Token: 0x040014F3 RID: 5363
			public Quaternion _rot;

			// Token: 0x040014F4 RID: 5364
			public bool _valid;
		}
	}
}
