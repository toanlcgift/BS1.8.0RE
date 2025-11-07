using System;
using System.Collections.Generic;

// Token: 0x020000D4 RID: 212
[Serializable]
public class LeaderboardUploadData
{
	// Token: 0x06000326 RID: 806 RVA: 0x0001ED10 File Offset: 0x0001CF10
	public LeaderboardUploadData(string playerName, string playerId, string score, string leaderboardId, string songName, string songSubName, string authorName, string bpm, string difficulty, string infoHash, List<string> modifiers)
	{
		this.playerName = playerName;
		this.playerId = playerId;
		this.score = score;
		this.leaderboardId = leaderboardId;
		this.songName = songName;
		this.songSubName = songSubName;
		this.authorName = authorName;
		this.bpm = bpm;
		this.difficulty = difficulty;
		this.infoHash = infoHash;
		this.modifiers = modifiers;
	}

	// Token: 0x04000392 RID: 914
	public string playerName;

	// Token: 0x04000393 RID: 915
	public string playerId;

	// Token: 0x04000394 RID: 916
	public string score;

	// Token: 0x04000395 RID: 917
	public string leaderboardId;

	// Token: 0x04000396 RID: 918
	public string songName;

	// Token: 0x04000397 RID: 919
	public string songSubName;

	// Token: 0x04000398 RID: 920
	public string authorName;

	// Token: 0x04000399 RID: 921
	public string bpm;

	// Token: 0x0400039A RID: 922
	public string difficulty;

	// Token: 0x0400039B RID: 923
	public string infoHash;

	// Token: 0x0400039C RID: 924
	public List<string> modifiers;
}
