using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

// Token: 0x02000440 RID: 1088
public class VRControllersRecorder : MonoBehaviour
{
	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x060014CA RID: 5322 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
	// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
	public VRControllersRecorder.Mode mode
	{
		get
		{
			return this._mode;
		}
		set
		{
			this._mode = value;
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x060014CC RID: 5324 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
	// (set) Token: 0x060014CB RID: 5323 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
	public TextAsset recordingTextAsset
	{
		get
		{
			return this._recordingTextAsset;
		}
		set
		{
			this._recordingTextAsset = value;
		}
	}

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x060014CE RID: 5326 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
	// (set) Token: 0x060014CD RID: 5325 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
	public string recordingFileName
	{
		get
		{
			return this._recordingFileName;
		}
		set
		{
			this._recordingFileName = value;
		}
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0000FB03 File Offset: 0x0000DD03
	// (set) Token: 0x060014CF RID: 5327 RVA: 0x0000FAFA File Offset: 0x0000DCFA
	public bool changeToNonVRCamera
	{
		get
		{
			return this._changeToNonVRCamera;
		}
		set
		{
			this._changeToNonVRCamera = value;
		}
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x0004C224 File Offset: 0x0004A424
	protected void Start()
	{
		if (this._recordingFileName == "")
		{
			this._recordingFileName = "Recordings/VRControllersRecording.rd";
		}
		if (this._mode == VRControllersRecorder.Mode.Off)
		{
			base.enabled = false;
		}
		this._recorderCamera.gameObject.SetActive(false);
		if (this._mode == VRControllersRecorder.Mode.Playback)
		{
			this._controller0.enabled = false;
			this._controller1.enabled = false;
			if (this._changeToNonVRCamera)
			{
				Camera camera = new GameObject("TempVRCamera").AddComponent<Camera>();
				camera.CopyFrom(this._camera);
				camera.targetDisplay = 2;
				this._camera.enabled = false;
				this._recorderCamera.fieldOfView = this._cameraFOV;
				this._recorderCamera.gameObject.SetActive(true);
			}
		}
		this._keyframes = new List<VRControllersRecorder.Keyframe>();
		if (this._mode == VRControllersRecorder.Mode.Playback)
		{
			this.Load();
			if (this._keyframes.Count > 0 && this._headTransform != null && !this._dontMoveHead)
			{
				this._headTransform.transform.SetPositionAndRotation(this._keyframes[0]._pos3, this._keyframes[0]._rot3);
				return;
			}
		}
		else if (this._mode == VRControllersRecorder.Mode.Record)
		{
			Debug.LogWarning("Recording performance.");
		}
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x0000FB0B File Offset: 0x0000DD0B
	protected void OnDestroy()
	{
		if (this._mode == VRControllersRecorder.Mode.Record)
		{
			this.Save();
		}
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x0004C36C File Offset: 0x0004A56C
	public void SetDefaultSettings()
	{
		this._dontMoveHead = false;
		this._changeToNonVRCamera = true;
		this._adjustSabersPositionBasedOnHeadPosition = false;
		this._headRotationOffset = Vector3.zero;
		this._headPositionOffset = Vector3.zero;
		this._headSmooth = 0f;
		this._cameraFOV = 65f;
		this._controllersTimeOffset = 0f;
		this._controllersSmooth = 0f;
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
	public void SetInGamePlaybackDefaultSettings()
	{
		this._dontMoveHead = true;
		this._changeToNonVRCamera = false;
		this._adjustSabersPositionBasedOnHeadPosition = true;
		this._headRotationOffset = Vector3.zero;
		this._headPositionOffset = Vector3.zero;
		this._headSmooth = 0f;
		this._cameraFOV = 65f;
		this._controllersTimeOffset = 0f;
		this._controllersSmooth = 0f;
		this._mode = VRControllersRecorder.Mode.Playback;
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x0004C43C File Offset: 0x0004A63C
	private void PlaybackTick()
	{
		float num = this._audioTimeSyncController.songTime + this._controllersTimeOffset;
		while (this._keyframeIndex < this._keyframes.Count - 2 && this._keyframes[this._keyframeIndex + 1]._time < num)
		{
			this._keyframeIndex++;
		}
		VRControllersRecorder.Keyframe keyframe = this._keyframes[this._keyframeIndex];
		VRControllersRecorder.Keyframe keyframe2 = this._keyframes[this._keyframeIndex + 1];
		float t = (num - keyframe._time) / Mathf.Max(1E-06f, keyframe2._time - keyframe._time);
		float t2 = (this._controllersSmooth == 0f) ? 1f : (Time.deltaTime * this._controllersSmooth);
		Vector3 vector = Vector3.Lerp(keyframe._pos3, keyframe2._pos3, t);
		if (this._controller0.transform != null)
		{
			if (!this._controller0.transform.gameObject.activeSelf)
			{
				this._controller0.transform.gameObject.SetActive(true);
			}
			Vector3 targetPos = Vector3.Lerp(keyframe._pos1, keyframe2._pos1, t);
			if (this._adjustSabersPositionBasedOnHeadPosition)
			{
				targetPos.z += this._headTransform.position.z - vector.z;
			}
			Quaternion targetRot = Quaternion.Lerp(keyframe._rot1, keyframe2._rot1, t);
			this.SetPositionAndRotation(this._controller0.transform.transform, targetPos, targetRot, t2);
		}
		if (this._controller1.transform != null)
		{
			if (!this._controller1.transform.gameObject.activeSelf)
			{
				this._controller1.transform.gameObject.SetActive(true);
			}
			Vector3 targetPos2 = Vector3.Lerp(keyframe._pos2, keyframe2._pos2, t);
			if (this._adjustSabersPositionBasedOnHeadPosition)
			{
				targetPos2.z += this._headTransform.position.z - vector.z;
			}
			Quaternion targetRot2 = Quaternion.Lerp(keyframe._rot2, keyframe2._rot2, t);
			this.SetPositionAndRotation(this._controller1.transform.transform, targetPos2, targetRot2, t2);
		}
		if (this._headTransform != null)
		{
			Vector3 vector2 = vector;
			if (!this._dontMoveHead)
			{
				Quaternion quaternion = Quaternion.Lerp(keyframe._rot3, keyframe2._rot3, t);
				this._headTransform.SetPositionAndRotation(vector2, quaternion);
				Vector3 vector3 = quaternion.eulerAngles;
				vector3 += this._headRotationOffset;
				quaternion.eulerAngles = vector3;
				vector2 += this._spawnRotationTransform.TransformPoint(this._headPositionOffset);
				if (this._headSmooth == 0f)
				{
					this._recorderCamera.transform.SetPositionAndRotation(vector2, quaternion);
					return;
				}
				t2 = ((this._headSmooth == 0f) ? 1f : (Time.deltaTime * this._headSmooth));
				this._recorderCamera.transform.SetPositionAndRotation(Vector3.Lerp(this._recorderCamera.transform.position, vector2, t2), Quaternion.Lerp(this._recorderCamera.transform.rotation, quaternion, t2));
			}
		}
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x0004C784 File Offset: 0x0004A984
	private void SetPositionAndRotation(Transform transf, Vector3 targetPos, Quaternion targetRot, float t)
	{
		Vector3 vector = transf.position;
		Quaternion quaternion = transf.rotation;
		vector = Vector3.Lerp(vector, targetPos, t);
		quaternion = Quaternion.Lerp(quaternion, targetRot, t);
		transf.SetPositionAndRotation(vector, quaternion);
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x0004C7BC File Offset: 0x0004A9BC
	private void RecordTick()
	{
		if (this._audioTimeSyncController.songTime == 0f)
		{
			return;
		}
		VRControllersRecorder.Keyframe keyframe = new VRControllersRecorder.Keyframe();
		if (this._controller0.transform != null)
		{
			keyframe._pos1 = this._controller0.transform.position;
			keyframe._rot1 = this._controller0.transform.rotation;
		}
		if (this._controller1.transform != null)
		{
			keyframe._pos2 = this._controller1.transform.position;
			keyframe._rot2 = this._controller1.transform.rotation;
		}
		if (this._headTransform != null)
		{
			keyframe._pos3 = this._headTransform.position;
			keyframe._rot3 = this._headTransform.rotation;
		}
		keyframe._time = this._audioTimeSyncController.songTime;
		this._keyframes.Add(keyframe);
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x0000FB1B File Offset: 0x0000DD1B
	protected void Update()
	{
		if (this._mode == VRControllersRecorder.Mode.Playback && this._keyframes.Count > 1)
		{
			this.PlaybackTick();
		}
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x0000FB3A File Offset: 0x0000DD3A
	protected void LateUpdate()
	{
		if (this._mode == VRControllersRecorder.Mode.Record)
		{
			this.RecordTick();
		}
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x0004C8B0 File Offset: 0x0004AAB0
	private void Save()
	{
		if (this._keyframes == null || this._keyframes.Count == 0)
		{
			return;
		}
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.Binder = new VRControllersRecorder.TypeSerializationBinder();
		FileStream fileStream = File.Open(this._recordingFileName, FileMode.OpenOrCreate);
		VRControllersRecorder.SavedData savedData = new VRControllersRecorder.SavedData();
		savedData._keyframes = new VRControllersRecorder.SavedData.KeyframeSerializable[this._keyframes.Count];
		for (int i = 0; i < this._keyframes.Count; i++)
		{
			VRControllersRecorder.Keyframe keyframe = this._keyframes[i];
			VRControllersRecorder.SavedData.KeyframeSerializable keyframeSerializable = new VRControllersRecorder.SavedData.KeyframeSerializable();
			keyframeSerializable._xPos1 = keyframe._pos1.x;
			keyframeSerializable._yPos1 = keyframe._pos1.y;
			keyframeSerializable._zPos1 = keyframe._pos1.z;
			keyframeSerializable._xPos2 = keyframe._pos2.x;
			keyframeSerializable._yPos2 = keyframe._pos2.y;
			keyframeSerializable._zPos2 = keyframe._pos2.z;
			keyframeSerializable._xPos3 = keyframe._pos3.x;
			keyframeSerializable._yPos3 = keyframe._pos3.y;
			keyframeSerializable._zPos3 = keyframe._pos3.z;
			keyframeSerializable._xRot1 = keyframe._rot1.x;
			keyframeSerializable._yRot1 = keyframe._rot1.y;
			keyframeSerializable._zRot1 = keyframe._rot1.z;
			keyframeSerializable._wRot1 = keyframe._rot1.w;
			keyframeSerializable._xRot2 = keyframe._rot2.x;
			keyframeSerializable._yRot2 = keyframe._rot2.y;
			keyframeSerializable._zRot2 = keyframe._rot2.z;
			keyframeSerializable._wRot2 = keyframe._rot2.w;
			keyframeSerializable._xRot3 = keyframe._rot3.x;
			keyframeSerializable._yRot3 = keyframe._rot3.y;
			keyframeSerializable._zRot3 = keyframe._rot3.z;
			keyframeSerializable._wRot3 = keyframe._rot3.w;
			keyframeSerializable._time = keyframe._time;
			savedData._keyframes[i] = keyframeSerializable;
		}
		binaryFormatter.Serialize(fileStream, savedData);
		fileStream.Close();
		Debug.Log("Performance saved to file " + this._recordingFileName);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x0004CB08 File Offset: 0x0004AD08
	private void Load()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.Binder = new VRControllersRecorder.TypeSerializationBinder();
		VRControllersRecorder.SavedData savedData = null;
		if (this._recordingTextAsset)
		{
			Stream serializationStream = new MemoryStream(this._recordingTextAsset.bytes);
			savedData = (binaryFormatter.Deserialize(serializationStream) as VRControllersRecorder.SavedData);
			if (savedData != null)
			{
				Debug.Log("Performance loaded from text asset " + this._recordingTextAsset);
			}
		}
		else
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.Open(this._recordingFileName, FileMode.Open);
				savedData = (VRControllersRecorder.SavedData)binaryFormatter.Deserialize(fileStream);
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
				Debug.Log("Performance loaded from file " + this._recordingFileName);
			}
		}
		if (savedData != null)
		{
			this._keyframes = new List<VRControllersRecorder.Keyframe>(savedData._keyframes.Length);
			for (int i = 0; i < savedData._keyframes.Length; i++)
			{
				VRControllersRecorder.SavedData.KeyframeSerializable keyframeSerializable = savedData._keyframes[i];
				VRControllersRecorder.Keyframe keyframe = new VRControllersRecorder.Keyframe();
				keyframe._pos1 = new Vector3(keyframeSerializable._xPos1, keyframeSerializable._yPos1, keyframeSerializable._zPos1);
				keyframe._pos2 = new Vector3(keyframeSerializable._xPos2, keyframeSerializable._yPos2, keyframeSerializable._zPos2);
				keyframe._pos3 = new Vector3(keyframeSerializable._xPos3, keyframeSerializable._yPos3, keyframeSerializable._zPos3);
				keyframe._rot1 = new Quaternion(keyframeSerializable._xRot1, keyframeSerializable._yRot1, keyframeSerializable._zRot1, keyframeSerializable._wRot1);
				keyframe._rot2 = new Quaternion(keyframeSerializable._xRot2, keyframeSerializable._yRot2, keyframeSerializable._zRot2, keyframeSerializable._wRot2);
				keyframe._rot3 = new Quaternion(keyframeSerializable._xRot3, keyframeSerializable._yRot3, keyframeSerializable._zRot3, keyframeSerializable._wRot3);
				keyframe._time = keyframeSerializable._time;
				this._keyframes.Add(keyframe);
			}
			return;
		}
		Debug.LogWarning("Loading performance file failed (" + this._recordingFileName + ")");
		base.enabled = false;
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x0004CD30 File Offset: 0x0004AF30
	public static AnimationClip CreateAnimationClipFromRecording(string recordingfilePath)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = null;
		VRControllersRecorder.SavedData savedData = null;
		try
		{
			fileStream = File.Open(recordingfilePath, FileMode.Open);
			savedData = (VRControllersRecorder.SavedData)binaryFormatter.Deserialize(fileStream);
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
			AnimationClip animationClip = new AnimationClip();
			AnimationCurve[] array = new AnimationCurve[3];
			AnimationCurve[] array2 = new AnimationCurve[3];
			AnimationCurve[] array3 = new AnimationCurve[3];
			AnimationCurve[] array4 = new AnimationCurve[3];
			AnimationCurve[] array5 = new AnimationCurve[3];
			AnimationCurve[] array6 = new AnimationCurve[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = new AnimationCurve();
				array2[i] = new AnimationCurve();
				array3[i] = new AnimationCurve();
				array4[i] = new AnimationCurve();
				array5[i] = new AnimationCurve();
				array6[i] = new AnimationCurve();
			}
			for (int j = 0; j < savedData._keyframes.Length; j++)
			{
				VRControllersRecorder.SavedData.KeyframeSerializable keyframeSerializable = savedData._keyframes[j];
				array[0].AddKey(keyframeSerializable._time, keyframeSerializable._xPos1);
				array2[0].AddKey(keyframeSerializable._time, keyframeSerializable._yPos1);
				array3[0].AddKey(keyframeSerializable._time, keyframeSerializable._zPos1);
				array4[0].AddKey(keyframeSerializable._time, keyframeSerializable._xRot1);
				array5[0].AddKey(keyframeSerializable._time, keyframeSerializable._yRot1);
				array6[0].AddKey(keyframeSerializable._time, keyframeSerializable._zRot1);
				array[1].AddKey(keyframeSerializable._time, keyframeSerializable._xPos2);
				array2[1].AddKey(keyframeSerializable._time, keyframeSerializable._yPos2);
				array3[1].AddKey(keyframeSerializable._time, keyframeSerializable._zPos2);
				array4[1].AddKey(keyframeSerializable._time, keyframeSerializable._xRot2);
				array5[1].AddKey(keyframeSerializable._time, keyframeSerializable._yRot2);
				array6[1].AddKey(keyframeSerializable._time, keyframeSerializable._zRot2);
				array[2].AddKey(keyframeSerializable._time, keyframeSerializable._xPos3);
				array2[2].AddKey(keyframeSerializable._time, keyframeSerializable._yPos3);
				array3[2].AddKey(keyframeSerializable._time, keyframeSerializable._zPos3);
				array4[2].AddKey(keyframeSerializable._time, keyframeSerializable._xRot3);
				array5[2].AddKey(keyframeSerializable._time, keyframeSerializable._yRot3);
				array6[2].AddKey(keyframeSerializable._time, keyframeSerializable._zRot3);
			}
			string[] array7 = new string[]
			{
				"HandControllers/LeftSaber",
				"HandControllers/RightSaber",
				"MainCamera"
			};
			for (int k = 0; k < 3; k++)
			{
				animationClip.SetCurve(array7[k], typeof(Transform), "localPosition.x", array[k]);
				animationClip.SetCurve(array7[k], typeof(Transform), "localPosition.y", array2[k]);
				animationClip.SetCurve(array7[k], typeof(Transform), "localPosition.z", array3[k]);
				animationClip.SetCurve(array7[k], typeof(Transform), "localRotation.x", array4[k]);
				animationClip.SetCurve(array7[k], typeof(Transform), "localRotation.y", array5[k]);
				animationClip.SetCurve(array7[k], typeof(Transform), "localRotation.z", array6[k]);
			}
			Debug.Log("Performance loaded from file " + recordingfilePath);
			return animationClip;
		}
		Debug.LogWarning("Loading performance file failed (" + recordingfilePath + ")");
		return null;
	}

	// Token: 0x0400148B RID: 5259
	[SerializeField]
	[NullAllowed]
	private TextAsset _recordingTextAsset;

	// Token: 0x0400148C RID: 5260
	[SerializeField]
	private string _recordingFileName = "VRControllersRecording.dat";

	// Token: 0x0400148D RID: 5261
	[SerializeField]
	private VRControllersRecorder.Mode _mode = VRControllersRecorder.Mode.Off;

	// Token: 0x0400148E RID: 5262
	[Space]
	[SerializeField]
	private bool _dontMoveHead;

	// Token: 0x0400148F RID: 5263
	[SerializeField]
	private bool _changeToNonVRCamera;

	// Token: 0x04001490 RID: 5264
	[SerializeField]
	private bool _adjustSabersPositionBasedOnHeadPosition;

	// Token: 0x04001491 RID: 5265
	[SerializeField]
	private Vector3 _headRotationOffset;

	// Token: 0x04001492 RID: 5266
	[SerializeField]
	private Vector3 _headPositionOffset;

	// Token: 0x04001493 RID: 5267
	[SerializeField]
	private float _headSmooth = 8f;

	// Token: 0x04001494 RID: 5268
	[SerializeField]
	private float _cameraFOV = 65f;

	// Token: 0x04001495 RID: 5269
	[SerializeField]
	private float _controllersTimeOffset;

	// Token: 0x04001496 RID: 5270
	[SerializeField]
	private float _controllersSmooth = 8f;

	// Token: 0x04001497 RID: 5271
	[Space]
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private VRController _controller0;

	// Token: 0x04001498 RID: 5272
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private VRController _controller1;

	// Token: 0x04001499 RID: 5273
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private Transform _headTransform;

	// Token: 0x0400149A RID: 5274
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private Camera _camera;

	// Token: 0x0400149B RID: 5275
	[Space]
	[SerializeField]
	private Camera _recorderCamera;

	// Token: 0x0400149C RID: 5276
	[SerializeField]
	private Transform _spawnRotationTransform;

	// Token: 0x0400149D RID: 5277
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x0400149E RID: 5278
	private List<VRControllersRecorder.Keyframe> _keyframes;

	// Token: 0x0400149F RID: 5279
	private int _keyframeIndex;

	// Token: 0x02000441 RID: 1089
	private sealed class TypeSerializationBinder : SerializationBinder
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x0000FB85 File Offset: 0x0000DD85
		public override Type BindToType(string assemblyName, string typeName)
		{
			return Type.GetType(typeName);
		}
	}

	// Token: 0x02000442 RID: 1090
	[Serializable]
	private class SavedData
	{
		// Token: 0x040014A0 RID: 5280
		public VRControllersRecorder.SavedData.KeyframeSerializable[] _keyframes;

		// Token: 0x02000443 RID: 1091
		[Serializable]
		public class KeyframeSerializable
		{
			// Token: 0x040014A1 RID: 5281
			public float _xPos1;

			// Token: 0x040014A2 RID: 5282
			public float _yPos1;

			// Token: 0x040014A3 RID: 5283
			public float _zPos1;

			// Token: 0x040014A4 RID: 5284
			public float _xPos2;

			// Token: 0x040014A5 RID: 5285
			public float _yPos2;

			// Token: 0x040014A6 RID: 5286
			public float _zPos2;

			// Token: 0x040014A7 RID: 5287
			public float _xPos3;

			// Token: 0x040014A8 RID: 5288
			public float _yPos3;

			// Token: 0x040014A9 RID: 5289
			public float _zPos3;

			// Token: 0x040014AA RID: 5290
			public float _xRot1;

			// Token: 0x040014AB RID: 5291
			public float _yRot1;

			// Token: 0x040014AC RID: 5292
			public float _zRot1;

			// Token: 0x040014AD RID: 5293
			public float _wRot1;

			// Token: 0x040014AE RID: 5294
			public float _xRot2;

			// Token: 0x040014AF RID: 5295
			public float _yRot2;

			// Token: 0x040014B0 RID: 5296
			public float _zRot2;

			// Token: 0x040014B1 RID: 5297
			public float _wRot2;

			// Token: 0x040014B2 RID: 5298
			public float _xRot3;

			// Token: 0x040014B3 RID: 5299
			public float _yRot3;

			// Token: 0x040014B4 RID: 5300
			public float _zRot3;

			// Token: 0x040014B5 RID: 5301
			public float _wRot3;

			// Token: 0x040014B6 RID: 5302
			public float _time;
		}
	}

	// Token: 0x02000444 RID: 1092
	public enum Mode
	{
		// Token: 0x040014B8 RID: 5304
		Record,
		// Token: 0x040014B9 RID: 5305
		Playback,
		// Token: 0x040014BA RID: 5306
		Off
	}

	// Token: 0x02000445 RID: 1093
	private class Keyframe
	{
		// Token: 0x040014BB RID: 5307
		public Vector3 _pos1;

		// Token: 0x040014BC RID: 5308
		public Vector3 _pos2;

		// Token: 0x040014BD RID: 5309
		public Vector3 _pos3;

		// Token: 0x040014BE RID: 5310
		public Quaternion _rot1;

		// Token: 0x040014BF RID: 5311
		public Quaternion _rot2;

		// Token: 0x040014C0 RID: 5312
		public Quaternion _rot3;

		// Token: 0x040014C1 RID: 5313
		public float _time;
	}
}
