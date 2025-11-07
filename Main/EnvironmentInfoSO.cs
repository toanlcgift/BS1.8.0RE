using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
[CreateAssetMenu(fileName = "EnvironmentInfoSO", menuName = "BS/Others/EnvironmentInfoSO")]
public class EnvironmentInfoSO : PersistentScriptableObject
{
	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060002C0 RID: 704 RVA: 0x00003CC5 File Offset: 0x00001EC5
	public SceneInfo sceneInfo
	{
		get
		{
			return this._sceneInfo;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060002C1 RID: 705 RVA: 0x00003CCD File Offset: 0x00001ECD
	public string environmentName
	{
		get
		{
			return this._environmentName;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060002C2 RID: 706 RVA: 0x00003CD5 File Offset: 0x00001ED5
	public ColorSchemeSO colorScheme
	{
		get
		{
			return this._colorScheme;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x00003CDD File Offset: 0x00001EDD
	public string serializedName
	{
		get
		{
			return this._serializedName;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003CE5 File Offset: 0x00001EE5
	public EnvironmentTypeSO environmentType
	{
		get
		{
			return this._environmentType;
		}
	}

	// Token: 0x0400034E RID: 846
	[SerializeField]
	private string _environmentName;

	// Token: 0x0400034F RID: 847
	[SerializeField]
	private ColorSchemeSO _colorScheme;

	// Token: 0x04000350 RID: 848
	[SerializeField]
	private SceneInfo _sceneInfo;

	// Token: 0x04000351 RID: 849
	[SerializeField]
	private string _serializedName;

	// Token: 0x04000352 RID: 850
	[SerializeField]
	private EnvironmentTypeSO _environmentType;
}
