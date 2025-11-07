using System;
using NetEase.Docker;

namespace NetEase
{
	// Token: 0x020004C2 RID: 1218
	public static class DockerWrap
	{
		// Token: 0x06001639 RID: 5689 RVA: 0x0001070A File Offset: 0x0000E90A
		public static void SetInitializeCallback(Action<InitializeData> callback)
		{
			VivaDocker.SetInitializeCallback(callback);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00010712 File Offset: 0x0000E912
		public static void Initialize()
		{
			VivaDocker.Initialize();
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00010719 File Offset: 0x0000E919
		public static void SetLoginCallback(Action<LoginData> callback)
		{
			VivaDocker.SetLoginCallback(callback);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00010721 File Offset: 0x0000E921
		public static void Login()
		{
			VivaDocker.Login();
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00010728 File Offset: 0x0000E928
		public static void SetLogoutCallback(Action<LogoutData> callback)
		{
			VivaDocker.SetLogoutCallback(callback);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00010730 File Offset: 0x0000E930
		public static void Logout()
		{
			VivaDocker.Logout();
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00010737 File Offset: 0x0000E937
		public static void UploadScore(UploadScoreData scoreData)
		{
			VivaDocker.UploadScore(scoreData);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0001073F File Offset: 0x0000E93F
		public static bool WillProvideHighscore()
		{
			return VivaDocker.WillProvideHighscore();
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00010746 File Offset: 0x0000E946
		public static void SetHighscoreReceivedCallback(Action<ReceivedHighscoreData> callbackAction)
		{
			VivaDocker.SetHighscoreReceivedCallback(callbackAction);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0001074E File Offset: 0x0000E94E
		public static void RequestHighscoreList(RequestHighscoreData highscoreRequest)
		{
			VivaDocker.RequestHighscoreList(highscoreRequest);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00010756 File Offset: 0x0000E956
		public static void UpdateLoop()
		{
			VivaDocker.UpdateLoop();
		}
	}
}
