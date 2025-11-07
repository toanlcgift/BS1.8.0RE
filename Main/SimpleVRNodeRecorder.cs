using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x0200043C RID: 1084
public class SimpleVRNodeRecorder : MonoBehaviour
{
	// Token: 0x060014BE RID: 5310 RVA: 0x0004BE3C File Offset: 0x0004A03C
	protected void Awake()
	{
		if (this._saveFilename == "")
		{
			this._saveFilename = "VRControllersRecording.dat";
		}
		this._keyframes = new List<SimpleVRNodeRecorder.SavedData.NodeKeyframe>();
		if (this._mode == SimpleVRNodeRecorder.RecordMode.Playback)
		{
			this.Load();
			return;
		}
		if (this._mode == SimpleVRNodeRecorder.RecordMode.Record)
		{
			Debug.LogWarning("Recording performance.");
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0000FA5A File Offset: 0x0000DC5A
	protected void OnDestroy()
	{
		if (this._mode == SimpleVRNodeRecorder.RecordMode.Record)
		{
			this.Save();
		}
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x0004BE94 File Offset: 0x0004A094
	private void RecordNewKeyFrame()
	{
		Vector3 pos;
		Quaternion rot;
		this._vrPlatformHelper.GetNodePose(this._node, 0, out pos, out rot);
		SimpleVRNodeRecorder.SavedData.NodeKeyframe item = new SimpleVRNodeRecorder.SavedData.NodeKeyframe(pos, rot, this._songTime.value);
		this._keyframes.Add(item);
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x0004BED8 File Offset: 0x0004A0D8
	protected void Update()
	{
		if (this._mode == SimpleVRNodeRecorder.RecordMode.Record)
		{
			this.RecordNewKeyFrame();
		}
		if (this._mode != SimpleVRNodeRecorder.RecordMode.Playback || this._keyframes.Count < 2)
		{
			return;
		}
		float value = this._songTime.value;
		while (this._keyframeIndex < this._keyframes.Count - 2 && this._keyframes[this._keyframeIndex + 1].time < value)
		{
			this._keyframeIndex++;
		}
		SimpleVRNodeRecorder.SavedData.NodeKeyframe nodeKeyframe = this._keyframes[this._keyframeIndex];
		SimpleVRNodeRecorder.SavedData.NodeKeyframe nodeKeyframe2 = this._keyframes[this._keyframeIndex + 1];
		float t = (value - nodeKeyframe.time) / Mathf.Max(1E-06f, nodeKeyframe2.time - nodeKeyframe.time);
		Vector3 pos = nodeKeyframe.pos;
		Vector3 pos2 = nodeKeyframe2.pos;
		Quaternion rot = nodeKeyframe.rot;
		Quaternion rot2 = nodeKeyframe2.rot;
		if (this._smooth < 0f)
		{
			this._playbackTransform.localPosition = Vector3.Lerp(pos, pos2, t);
			this._playbackTransform.localRotation = Quaternion.Slerp(rot, rot2, t);
			return;
		}
		Vector3 vector = Vector3.Lerp(this._prevPos, Vector3.Lerp(pos, pos2, t), Time.deltaTime * this._smooth);
		Quaternion quaternion = Quaternion.Slerp(this._prevRot, Quaternion.Slerp(rot, rot2, t), Time.deltaTime * this._smooth);
		Vector3 eulerAngles = quaternion.eulerAngles;
		eulerAngles.z = 0f;
		quaternion.eulerAngles = eulerAngles;
		this._playbackTransform.localPosition = vector;
		this._playbackTransform.localRotation = quaternion;
		this._playbackTransform.localPosition += this._playbackTransform.forward * this._forwardOffset;
		this._prevPos = vector;
		this._prevRot = quaternion;
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x0004C0B0 File Offset: 0x0004A2B0
	private void Save()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Open(this._saveFilename, FileMode.OpenOrCreate);
		binaryFormatter.Serialize(fileStream, new SimpleVRNodeRecorder.SavedData
		{
			keyframes = this._keyframes.ToArray()
		});
		fileStream.Close();
		Debug.Log("Performance saved to file " + this._saveFilename);
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x0004C108 File Offset: 0x0004A308
	private void Load()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = null;
		SimpleVRNodeRecorder.SavedData savedData = null;
		try
		{
			fileStream = File.Open(this._saveFilename, FileMode.Open);
			savedData = (SimpleVRNodeRecorder.SavedData)binaryFormatter.Deserialize(fileStream);
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
			this._keyframes = new List<SimpleVRNodeRecorder.SavedData.NodeKeyframe>(savedData.keyframes);
			Debug.Log("Performance loaded from file " + this._saveFilename);
			return;
		}
		Debug.Log("Loading performance file failed (" + this._saveFilename + ")");
		base.enabled = false;
	}

	// Token: 0x04001472 RID: 5234
	[SerializeField]
	private FloatSO _songTime;

	// Token: 0x04001473 RID: 5235
	[SerializeField]
	private string _saveFilename = "VRControllersRecording.dat";

	// Token: 0x04001474 RID: 5236
	[SerializeField]
	private SimpleVRNodeRecorder.RecordMode _mode = SimpleVRNodeRecorder.RecordMode.Off;

	// Token: 0x04001475 RID: 5237
	[SerializeField]
	private XRNode _node;

	// Token: 0x04001476 RID: 5238
	[SerializeField]
	private Transform _playbackTransform;

	// Token: 0x04001477 RID: 5239
	[SerializeField]
	private float _smooth = 4f;

	// Token: 0x04001478 RID: 5240
	[SerializeField]
	private float _forwardOffset;

	// Token: 0x04001479 RID: 5241
	[Inject]
	private VRPlatformHelper _vrPlatformHelper;

	// Token: 0x0400147A RID: 5242
	private List<SimpleVRNodeRecorder.SavedData.NodeKeyframe> _keyframes;

	// Token: 0x0400147B RID: 5243
	private int _keyframeIndex;

	// Token: 0x0400147C RID: 5244
	private Vector3 _prevPos;

	// Token: 0x0400147D RID: 5245
	private Quaternion _prevRot;

	// Token: 0x0200043D RID: 1085
	[Serializable]
	private class SavedData
	{
		// Token: 0x0400147E RID: 5246
		public SimpleVRNodeRecorder.SavedData.NodeKeyframe[] keyframes;

		// Token: 0x0200043E RID: 1086
		[Serializable]
		public class NodeKeyframe
		{
			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0000FA8F File Offset: 0x0000DC8F
			public Vector3 pos
			{
				get
				{
					return new Vector3(this.posX, this.posY, this.posZ);
				}
			}

			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
			public Quaternion rot
			{
				get
				{
					return new Quaternion(this.rotX, this.rotY, this.rotZ, this.rotW);
				}
			}

			// Token: 0x060014C8 RID: 5320 RVA: 0x0004C1B4 File Offset: 0x0004A3B4
			public NodeKeyframe(Vector3 pos, Quaternion rot, float time)
			{
				this.posX = pos.x;
				this.posY = pos.y;
				this.posZ = pos.z;
				this.rotX = rot.x;
				this.rotY = rot.y;
				this.rotZ = rot.z;
				this.rotW = rot.w;
				this.time = time;
			}

			// Token: 0x0400147F RID: 5247
			public float posX;

			// Token: 0x04001480 RID: 5248
			public float posY;

			// Token: 0x04001481 RID: 5249
			public float posZ;

			// Token: 0x04001482 RID: 5250
			public float rotX;

			// Token: 0x04001483 RID: 5251
			public float rotY;

			// Token: 0x04001484 RID: 5252
			public float rotZ;

			// Token: 0x04001485 RID: 5253
			public float rotW;

			// Token: 0x04001486 RID: 5254
			public float time;
		}
	}

	// Token: 0x0200043F RID: 1087
	private enum RecordMode
	{
		// Token: 0x04001488 RID: 5256
		Record,
		// Token: 0x04001489 RID: 5257
		Playback,
		// Token: 0x0400148A RID: 5258
		Off
	}
}
