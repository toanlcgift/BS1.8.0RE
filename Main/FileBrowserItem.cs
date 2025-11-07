using System;

// Token: 0x020000C9 RID: 201
public class FileBrowserItem
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060002D3 RID: 723 RVA: 0x00003D29 File Offset: 0x00001F29
	// (set) Token: 0x060002D4 RID: 724 RVA: 0x00003D31 File Offset: 0x00001F31
	public string displayName { get; private set; }

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003D3A File Offset: 0x00001F3A
	// (set) Token: 0x060002D6 RID: 726 RVA: 0x00003D42 File Offset: 0x00001F42
	public string fullPath { get; private set; }

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060002D7 RID: 727 RVA: 0x00003D4B File Offset: 0x00001F4B
	// (set) Token: 0x060002D8 RID: 728 RVA: 0x00003D53 File Offset: 0x00001F53
	public bool isDirectory { get; private set; }

	// Token: 0x060002D9 RID: 729 RVA: 0x00003D5C File Offset: 0x00001F5C
	public FileBrowserItem(string displayName, string fullPath, bool isDirectory)
	{
		this.displayName = displayName;
		this.fullPath = fullPath;
		this.isDirectory = isDirectory;
	}
}
