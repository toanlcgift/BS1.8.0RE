using System;
using System.Collections.Generic;

// Token: 0x020003A7 RID: 935
public class MissionNodesHelper
{
	// Token: 0x06001110 RID: 4368 RVA: 0x00041AE8 File Offset: 0x0003FCE8
	public static HashSet<MissionNode> GetAllNodesFromRoot(MissionNode root)
	{
		HashSet<MissionNode> hashSet = new HashSet<MissionNode>();
		MissionNodesHelper.VisitAllTree(root, hashSet);
		return hashSet;
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00041B04 File Offset: 0x0003FD04
	private static void VisitAllTree(MissionNode node, HashSet<MissionNode> visitedNodes)
	{
		if (visitedNodes.Contains(node))
		{
			return;
		}
		visitedNodes.Add(node);
		MissionNode[] childNodes = node.childNodes;
		for (int i = 0; i < childNodes.Length; i++)
		{
			MissionNodesHelper.VisitAllTree(childNodes[i], visitedNodes);
		}
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x0000CF1B File Offset: 0x0000B11B
	public static bool CycleDetection(MissionNode node)
	{
		return MissionNodesHelper.CycleDetection(node, 0, new Dictionary<MissionNode, int>());
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x00041B44 File Offset: 0x0003FD44
	private static bool CycleDetection(MissionNode node, int layer, Dictionary<MissionNode, int> layers)
	{
		if (layers.ContainsKey(node))
		{
			return layer > layers[node];
		}
		layers[node] = layer;
		MissionNode[] childNodes = node.childNodes;
		for (int i = 0; i < childNodes.Length; i++)
		{
			if (MissionNodesHelper.CycleDetection(childNodes[i], layer + 1, layers))
			{
				return true;
			}
		}
		layers.Remove(node);
		return false;
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0000CF29 File Offset: 0x0000B129
	public static bool FinalNodeIsFinal(MissionNode finalNode, MissionNode rootNode)
	{
		return MissionNodesHelper.FinalNodeIsFinal(finalNode, rootNode, new HashSet<MissionNode>());
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00041BA0 File Offset: 0x0003FDA0
	private static bool FinalNodeIsFinal(MissionNode finalNode, MissionNode node, HashSet<MissionNode> visitedNodes)
	{
		if (visitedNodes.Contains(node))
		{
			return true;
		}
		if (finalNode.position.y < node.position.y)
		{
			return false;
		}
		visitedNodes.Add(node);
		foreach (MissionNode node2 in node.childNodes)
		{
			if (!MissionNodesHelper.FinalNodeIsFinal(finalNode, node2, visitedNodes))
			{
				return false;
			}
		}
		return true;
	}
}
