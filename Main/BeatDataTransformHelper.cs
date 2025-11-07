using System;

// Token: 0x0200008F RID: 143
public class BeatDataTransformHelper
{
	// Token: 0x06000232 RID: 562 RVA: 0x0001B7CC File Offset: 0x000199CC
	public static BeatmapData CreateTransformedBeatmapData(BeatmapData beatmapData, GameplayModifiers gameplayModifiers, PracticeSettings practiceSettings, PlayerSpecificSettings playerSpecificSettings)
	{
		BeatmapData beatmapData2 = beatmapData;
		if (playerSpecificSettings.leftHanded)
		{
			beatmapData2 = BeatDataMirrorTransform.CreateTransformedData(beatmapData2);
		}
		GameplayModifiers.EnabledObstacleType enabledObstacleType = gameplayModifiers.enabledObstacleType;
		if (gameplayModifiers.demoNoObstacles)
		{
			enabledObstacleType = GameplayModifiers.EnabledObstacleType.NoObstacles;
		}
		if (enabledObstacleType != GameplayModifiers.EnabledObstacleType.All || gameplayModifiers.noBombs)
		{
			beatmapData2 = BeatmapDataObstaclesAndBombsTransform.CreateTransformedData(beatmapData2, enabledObstacleType, gameplayModifiers.noBombs);
		}
		if (gameplayModifiers.noArrows)
		{
			beatmapData2 = BeatmapDataNoArrowsTransform.CreateTransformedData(beatmapData2, false);
		}
		if (playerSpecificSettings.staticLights)
		{
			beatmapData2 = BeatmapDataStaticLightsTransform.CreateTransformedData(beatmapData2);
		}
		if (beatmapData2 == beatmapData)
		{
			beatmapData2 = beatmapData.GetCopy();
		}
		return beatmapData2;
	}
}
