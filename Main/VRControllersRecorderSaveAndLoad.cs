using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public static class VRControllersRecorderSaveAndLoad
{
	// Token: 0x060014ED RID: 5357 RVA: 0x0004D18C File Offset: 0x0004B38C
	private static VRControllersRecorderSaveData LoadSaveDataFromFile(string filePath)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		VRControllersRecorderSaveData result = null;
		FileStream fileStream = null;
		try
		{
			fileStream = File.Open(filePath, FileMode.Open);
			result = (VRControllersRecorderSaveData)binaryFormatter.Deserialize(fileStream);
		}
		catch
		{
			result = null;
		}
		finally
		{
			if (fileStream != null)
			{
				fileStream.Close();
			}
		}
		return result;
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x0004D1E8 File Offset: 0x0004B3E8
	private static VRControllersRecorderSaveData LoadSaveDataFromTextAsset(TextAsset textAsset)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		Stream serializationStream = new MemoryStream(textAsset.bytes);
		return binaryFormatter.Deserialize(serializationStream) as VRControllersRecorderSaveData;
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x0004D214 File Offset: 0x0004B414
	public static void LoadFromFile(string filePath, VRControllersRecorderData data)
	{
		VRControllersRecorderSaveData vrcontrollersRecorderSaveData = VRControllersRecorderSaveAndLoad.LoadSaveDataFromFile(filePath);
		if (vrcontrollersRecorderSaveData == null)
		{
			Debug.LogWarning("Loading performance file failed (" + filePath + ").");
		}
		for (int i = 0; i < vrcontrollersRecorderSaveData.keyframes.Length; i++)
		{
			VRControllersRecorderData.PositionAndRotation[] array = new VRControllersRecorderData.PositionAndRotation[data.nodesInfo.Length];
			VRControllersRecorderSaveData.Keyframe keyframe = vrcontrollersRecorderSaveData.keyframes[i];
			for (int j = 0; j < vrcontrollersRecorderSaveData.nodesInfo.Length; j++)
			{
				VRControllersRecorderSaveData.NodeInfo nodeInfo = vrcontrollersRecorderSaveData.nodesInfo[j];
				for (int k = 0; k < data.nodesInfo.Length; k++)
				{
					VRControllersRecorderData.NodeInfo nodeInfo2 = data.nodesInfo[k];
					if (nodeInfo2.nodeIdx == nodeInfo.nodeIdx && nodeInfo2.nodeType == nodeInfo.nodeType)
					{
						VRControllersRecorderSaveData.PositionAndRotation positionAndRotation = keyframe.positionsAndRotations[j];
						array[k] = new VRControllersRecorderData.PositionAndRotation(new Vector3(positionAndRotation.posX, positionAndRotation.posY, positionAndRotation.posZ), new Quaternion(positionAndRotation.rotX, positionAndRotation.rotY, positionAndRotation.rotZ, positionAndRotation.rotW));
						break;
					}
				}
			}
			data.AddKeyFrame(array, keyframe.time);
		}
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x0004D344 File Offset: 0x0004B544
	public static void SaveToFile(string filePath, VRControllersRecorderData data)
	{
		VRControllersRecorderSaveData vrcontrollersRecorderSaveData = new VRControllersRecorderSaveData();
		List<VRControllersRecorderSaveData.NodeInfo> list = new List<VRControllersRecorderSaveData.NodeInfo>(data.nodesInfo.Length);
		foreach (VRControllersRecorderData.NodeInfo nodeInfo in data.nodesInfo)
		{
			list.Add(new VRControllersRecorderSaveData.NodeInfo
			{
				nodeType = nodeInfo.nodeType,
				nodeIdx = nodeInfo.nodeIdx
			});
		}
		vrcontrollersRecorderSaveData.nodesInfo = list.ToArray();
		List<VRControllersRecorderSaveData.Keyframe> list2 = new List<VRControllersRecorderSaveData.Keyframe>(data.numberOfKeyframes);
		for (int j = 0; j < data.numberOfKeyframes; j++)
		{
			VRControllersRecorderSaveData.Keyframe keyframe = new VRControllersRecorderSaveData.Keyframe();
			keyframe.time = data.GetFrameTime(j);
			keyframe.positionsAndRotations = new VRControllersRecorderSaveData.PositionAndRotation[data.nodesInfo.Length];
			for (int k = 0; k < data.nodesInfo.Length; k++)
			{
				VRControllersRecorderData.NodeInfo nodeInfo2 = data.nodesInfo[k];
				VRControllersRecorderData.PositionAndRotation positionAndRotation = data.GetPositionAndRotation(j, nodeInfo2.nodeType, nodeInfo2.nodeIdx);
				VRControllersRecorderSaveData.PositionAndRotation positionAndRotation2 = new VRControllersRecorderSaveData.PositionAndRotation();
				positionAndRotation2.posX = positionAndRotation.pos.x;
				positionAndRotation2.posY = positionAndRotation.pos.y;
				positionAndRotation2.posZ = positionAndRotation.pos.z;
				positionAndRotation2.rotX = positionAndRotation.rot.x;
				positionAndRotation2.rotY = positionAndRotation.rot.y;
				positionAndRotation2.rotZ = positionAndRotation.rot.z;
				positionAndRotation2.rotW = positionAndRotation.rot.w;
				keyframe.positionsAndRotations[k] = positionAndRotation2;
			}
			list2.Add(keyframe);
		}
		vrcontrollersRecorderSaveData.keyframes = list2.ToArray();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate);
		binaryFormatter.Serialize(fileStream, vrcontrollersRecorderSaveData);
		fileStream.Close();
	}
}
