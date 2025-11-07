using System;
using System.IO;
using SFB;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class NativeFileDialogs
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600002A RID: 42 RVA: 0x000022A1 File Offset: 0x000004A1
	private static string lastUsedDirectory
	{
		get
		{
			return PlayerPrefs.GetString("NativeFileDialogs.LastUsedFilePath", null);
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00014EE0 File Offset: 0x000130E0
	private static void SetLastUsedDirectory(string path)
	{
		string text = null;
		if (text != null)
		{
			try
			{
				text = Path.GetDirectoryName(path);
			}
			catch
			{
			}
		}
		PlayerPrefs.SetString("NativeFileDialogs.LastUsedFilePath", text);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00014F1C File Offset: 0x0001311C
	public static string SaveFileDialog(string title, string defaultName, string extension, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = NativeFileDialogs.lastUsedDirectory;
		}
		string text = StandaloneFileBrowser.SaveFilePanel(title, initialDirectory, defaultName, extension);
		if (text == null || text == "")
		{
			return null;
		}
		NativeFileDialogs.SetLastUsedDirectory(text);
		return text;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00014F58 File Offset: 0x00013158
	public static string[] OpenFileDialogMultiselect(string title, string extension, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = NativeFileDialogs.lastUsedDirectory;
		}
		string[] array = StandaloneFileBrowser.OpenFilePanel(title, initialDirectory, extension, true);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		NativeFileDialogs.SetLastUsedDirectory(array[0]);
		return array;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00014F8C File Offset: 0x0001318C
	public static string OpenFileDialog(string title, string extension, string initialDirectory)
	{
		ExtensionFilter[] array;
		if (!string.IsNullOrEmpty(extension))
		{
			(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[]
			{
				extension
			});
		}
		else
		{
			array = null;
		}
		ExtensionFilter[] extensions = array;
		return NativeFileDialogs.OpenFileDialog(title, extensions, initialDirectory);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00014FD0 File Offset: 0x000131D0
	public static string OpenFileDialog(string title, ExtensionFilter[] extensions, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = NativeFileDialogs.lastUsedDirectory;
		}
		string[] array = StandaloneFileBrowser.OpenFilePanel(title, initialDirectory, extensions, false);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		NativeFileDialogs.SetLastUsedDirectory(array[0]);
		return array[0];
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00015008 File Offset: 0x00013208
	public static string OpenDirectoryDialog(string title, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = NativeFileDialogs.lastUsedDirectory;
		}
		string[] array = StandaloneFileBrowser.OpenFolderPanel(title, initialDirectory, false);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		NativeFileDialogs.SetLastUsedDirectory(array[0]);
		return array[0];
	}

	// Token: 0x0400002C RID: 44
	private const string kLastUsedDirectoryPathKey = "NativeFileDialogs.LastUsedFilePath";
}
