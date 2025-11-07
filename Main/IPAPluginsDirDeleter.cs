using System;
using System.IO;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class IPAPluginsDirDeleter : MonoBehaviour
{
	// Token: 0x06000E5B RID: 3675 RVA: 0x0003AC70 File Offset: 0x00038E70
	protected void Awake()
	{
		string text = "Unknown";
		if (File.Exists("BeatSaberVersion.txt"))
		{
			try
			{
				text = File.ReadAllText("BeatSaberVersion.txt");
			}
			catch
			{
			}
		}
		try
		{
			if (!string.Equals(text, Application.version, StringComparison.OrdinalIgnoreCase))
			{
				string text2 = "Plugins";
				if (Directory.Exists(text2))
				{
					string text3 = "Incompatible Plugins";
					if (!Directory.Exists(text3))
					{
						Directory.CreateDirectory(text3);
					}
					string text4 = Path.Combine(text3, "Plugins v" + text);
					text4 = FileHelpers.GetUniqueDirectoryNameByAppendingNumber(text4);
					Directory.Move(text2, text4);
				}
			}
		}
		catch
		{
		}
		text = Application.version;
		try
		{
			File.WriteAllText("BeatSaberVersion.txt", text);
		}
		catch
		{
		}
	}

	// Token: 0x04000EB8 RID: 3768
	private const string kVersionFilename = "BeatSaberVersion.txt";
}
