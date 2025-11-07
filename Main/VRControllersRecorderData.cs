using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Token: 0x02000446 RID: 1094
public class VRControllersRecorderData
{
	// Token: 0x060014E3 RID: 5347 RVA: 0x0000FB95 File Offset: 0x0000DD95
	public VRControllersRecorderData(VRControllersRecorderData.NodeInfo[] nodesInfo)
	{
		this.nodesInfo = nodesInfo;
		this._keyframes = new List<VRControllersRecorderData.Keyframe>();
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x0000FBAF File Offset: 0x0000DDAF
	public void AddKeyFrame(VRControllersRecorderData.PositionAndRotation[] positionsAndRotations, float time)
	{
		this._keyframes.Add(new VRControllersRecorderData.Keyframe(positionsAndRotations, time));
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x0004D104 File Offset: 0x0004B304
	public VRControllersRecorderData.PositionAndRotation GetPositionAndRotation(int frameIdx, XRNode nodeType, int nodeIdx)
	{
		for (int i = 0; i < this.nodesInfo.Length; i++)
		{
			VRControllersRecorderData.NodeInfo nodeInfo = this.nodesInfo[i];
			if (nodeInfo.nodeType == nodeType && nodeInfo.nodeIdx == nodeIdx)
			{
				return this._keyframes[frameIdx].positionsAndRotations[i];
			}
		}
		return default(VRControllersRecorderData.PositionAndRotation);
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x0004D160 File Offset: 0x0004B360
	public VRControllersRecorderData.PositionAndRotation GetLerpedPositionAndRotation(int frameIdx, float t, XRNode nodeType, int nodeIdx)
	{
		VRControllersRecorderData.PositionAndRotation positionAndRotation = this.GetPositionAndRotation(frameIdx, nodeType, nodeIdx);
		VRControllersRecorderData.PositionAndRotation positionAndRotation2 = this.GetPositionAndRotation(frameIdx + 1, nodeType, nodeIdx);
		return VRControllersRecorderData.PositionAndRotation.Lerp(positionAndRotation, positionAndRotation2, t);
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x0000FBC3 File Offset: 0x0000DDC3
	public float GetFrameTime(int frameIdx)
	{
		return this._keyframes[frameIdx].time;
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0000FBD6 File Offset: 0x0000DDD6
	public int numberOfKeyframes
	{
		get
		{
			return this._keyframes.Count;
		}
	}

	// Token: 0x040014C2 RID: 5314
	public readonly VRControllersRecorderData.NodeInfo[] nodesInfo;

	// Token: 0x040014C3 RID: 5315
	private readonly List<VRControllersRecorderData.Keyframe> _keyframes;

	// Token: 0x02000447 RID: 1095
	public struct PositionAndRotation
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		public PositionAndRotation(Vector3 pos, Quaternion rot)
		{
			this.pos = pos;
			this.rot = rot;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0000FBF3 File Offset: 0x0000DDF3
		public static VRControllersRecorderData.PositionAndRotation Lerp(VRControllersRecorderData.PositionAndRotation a, VRControllersRecorderData.PositionAndRotation b, float t)
		{
			return new VRControllersRecorderData.PositionAndRotation(Vector3.Lerp(a.pos, b.pos, t), Quaternion.Slerp(a.rot, b.rot, t));
		}

		// Token: 0x040014C4 RID: 5316
		public readonly Vector3 pos;

		// Token: 0x040014C5 RID: 5317
		public readonly Quaternion rot;
	}

	// Token: 0x02000448 RID: 1096
	public class Keyframe
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x0000FC1E File Offset: 0x0000DE1E
		public Keyframe(VRControllersRecorderData.PositionAndRotation[] positionAndRotations, float time)
		{
			this.positionsAndRotations = positionAndRotations;
			this.time = time;
		}

		// Token: 0x040014C6 RID: 5318
		public readonly VRControllersRecorderData.PositionAndRotation[] positionsAndRotations;

		// Token: 0x040014C7 RID: 5319
		public readonly float time;
	}

	// Token: 0x02000449 RID: 1097
	public class NodeInfo
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public NodeInfo(XRNode nodeType, int nodeIdx)
		{
			this.nodeType = nodeType;
			this.nodeIdx = nodeIdx;
		}

		// Token: 0x040014C8 RID: 5320
		public readonly XRNode nodeType;

		// Token: 0x040014C9 RID: 5321
		public readonly int nodeIdx;
	}
}
