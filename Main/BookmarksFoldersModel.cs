using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class BookmarksFoldersModel : PersistentScriptableObject
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600023C RID: 572 RVA: 0x0001B840 File Offset: 0x00019A40
	public FileBrowserItem[] bookmarksFolders
	{
		get
		{
			if (this._bookmarksFolders == null)
			{
				List<FileBrowserItem> list = new List<FileBrowserItem>();
				foreach (string text in Directory.GetLogicalDrives())
				{
					list.Add(new FileBrowserItem(text, text, true));
				}
				Environment.SpecialFolder[] array = new Environment.SpecialFolder[4];
				RuntimeHelpers.InitializeArray(array, PrivateImplementationDetails.FieldHandler);
				IEnumerable<string> collection = from specialFolder in array
				select Environment.GetFolderPath(specialFolder);
				List<string> list2 = new List<string>(this.myFolders);
				list2.AddRange(collection);
				list2.Add(Application.dataPath);
				foreach (string text2 in list2)
				{
					if (Directory.Exists(text2))
					{
						list.Add(new FileBrowserItem(new DirectoryInfo(text2).Name, text2, true));
					}
				}
				this._bookmarksFolders = list.ToArray();
			}
			return this._bookmarksFolders;
		}
	}

	// Token: 0x04000265 RID: 613
	[SerializeField]
	private string[] myFolders;

	// Token: 0x04000266 RID: 614
	private FileBrowserItem[] _bookmarksFolders;
}
