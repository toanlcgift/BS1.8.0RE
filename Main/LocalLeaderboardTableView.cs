using System;
using System.Collections.Generic;

// Token: 0x02000425 RID: 1061
public class LocalLeaderboardTableView : LeaderboardTableView
{
	// Token: 0x06001443 RID: 5187 RVA: 0x0004A60C File Offset: 0x0004880C
	public void SetScores(List<LocalLeaderboardsModel.ScoreData> scores, int specialScorePos, int maxNumberOfCells)
	{
		List<LeaderboardTableView.ScoreData> list = new List<LeaderboardTableView.ScoreData>();
		if (scores != null)
		{
			int num = 0;
			while (num < scores.Count && num < maxNumberOfCells)
			{
				LocalLeaderboardsModel.ScoreData scoreData = scores[num];
				list.Add(new LeaderboardTableView.ScoreData(scoreData._score, scoreData._playerName, num + 1, scoreData._fullCombo));
				num++;
			}
		}
		for (int i = (scores != null) ? scores.Count : 0; i < maxNumberOfCells; i++)
		{
			list.Add(new LeaderboardTableView.ScoreData(-1, "", i + 1, false));
		}
		base.SetScores(list, specialScorePos);
	}
}
