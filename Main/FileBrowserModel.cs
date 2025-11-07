using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x020000C7 RID: 199
public static class FileBrowserModel
{
	// Token: 0x060002CD RID: 717 RVA: 0x0001E528 File Offset: 0x0001C728
	public static void GetContentOfDirectory(string direcotryPath, string[] extensions, Action<FileBrowserItem[]> callback)
	{
		FileBrowserItem[] items = null;
		Action job = delegate()
		{
			items = FileBrowserModel.GetContentOfDirectory(direcotryPath, extensions);
		};
		Action finishCallback = delegate()
		{
			callback(items);
		};
		new HMTask(job, finishCallback).Run();
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0001E57C File Offset: 0x0001C77C
	private static FileBrowserItem[] GetContentOfDirectory(string directoryPath, string[] extensions)
	{
		List<FileBrowserItem> list = new List<FileBrowserItem>();
		string path = directoryPath + "\\..";
		if (Path.GetFullPath(path) != Path.GetFullPath(directoryPath))
		{
			list.Add(new FileBrowserItem("..", Path.GetFullPath(path), true));
		}
		if (!FileBrowserModel.CanOpenDirectory(directoryPath))
		{
			return list.ToArray();
		}
		directoryPath = Path.GetFullPath(directoryPath);
		foreach (string path2 in Directory.GetDirectories(directoryPath))
		{
			list.Add(new FileBrowserItem(Path.GetFileName(path2), Path.GetFullPath(path2), true));
		}
		foreach (string text in Directory.GetFiles(directoryPath))
		{
			foreach (string value in extensions)
			{
				if (text.EndsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(new FileBrowserItem(Path.GetFileName(text), Path.GetFullPath(text), false));
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001E674 File Offset: 0x0001C874
	private static bool CanOpenDirectory(string path)
	{
		bool result;
		try
		{
			Directory.GetDirectories(path);
			result = true;
		}
		catch
		{
			result = false;
		}
		return result;
	}
}
