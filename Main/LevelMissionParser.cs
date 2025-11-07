using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class LevelMissionParser
{
	// Token: 0x060003B4 RID: 948 RVA: 0x0000444D File Offset: 0x0000264D
	public LevelMissionParser()
	{
		this._functions = new Dictionary<string, LevelMissionParser.ParserFunction>(10);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00004462 File Offset: 0x00002662
	public void AddFunction(string name, LevelMissionParser.ParserFunction function)
	{
		this._functions[name] = function;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00004471 File Offset: 0x00002671
	public bool Parse(string s)
	{
		return this.Parse(s, 0, s.Length);
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00020060 File Offset: 0x0001E260
	private bool Parse(string s, int start, int length)
	{
		if (length == 0 || start + length > s.Length)
		{
			Debug.LogError("Bad mission string format.");
			return false;
		}
		if (s[start] == '(')
		{
			if (s[start + length - 1] != ')' || length < 2)
			{
				Debug.LogError("Bad mission string format.");
				return false;
			}
			return this.Parse(s, start + 1, length - 2);
		}
		else
		{
			int num = 0;
			for (int i = start; i < start + length; i++)
			{
				if (s[i] == ')')
				{
					num--;
				}
				else if (s[i] == '(')
				{
					num++;
				}
				if (num < 0)
				{
					Debug.LogError("Bad mission string format.");
					return false;
				}
				if (num == 0)
				{
					if (s[i] == '&')
					{
						return this.Parse(s, start, i - start) & this.Parse(s, i + 1, length - i + start - 1);
					}
					if (s[i] == '|')
					{
						return this.Parse(s, start, i - start) | this.Parse(s, i + 1, length - i + start - 1);
					}
				}
			}
			if (s[start] == '!')
			{
				return !this.Parse(s, start + 1, length - 1);
			}
			return this.ParseFunction(s, start, length);
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00020180 File Offset: 0x0001E380
	private bool ParseFunction(string s, int start, int length)
	{
		if (length < 3 || start + length > s.Length)
		{
			Debug.LogError("Bad mission string format.");
			return false;
		}
		string key = "";
		int num = 0;
		for (int i = start; i < start + length; i++)
		{
			if (s[i] == '(')
			{
				key = s.Substring(start, i - start);
				num = i + 1;
			}
		}
		if (num < start + 2 || num + 1 > start + length || s[start + length - 1] != ')')
		{
			Debug.LogError("Bad mission string format.");
			return false;
		}
		float[] array = new float[5];
		int num2 = 0;
		int num3 = num;
		for (int j = num; j < start + length - 1; j++)
		{
			if (s[j] == ',')
			{
				if (num2 + 1 > 5)
				{
					Debug.LogError("Bad mission string format.");
					return false;
				}
				array[num2] = float.Parse(s.Substring(num3, j - num3 + 1));
				num3 = j + 1;
				num2++;
			}
		}
		if (num2 < 5 && start + length - num3 > 1)
		{
			array[num2] = float.Parse(s.Substring(num3, start + length - num3 - 1));
			num2++;
		}
		LevelMissionParser.ParserFunction parserFunction = this._functions[key];
		return parserFunction == null || parserFunction(array, num2);
	}

	// Token: 0x04000423 RID: 1059
	private Dictionary<string, LevelMissionParser.ParserFunction> _functions;

	// Token: 0x020000F8 RID: 248
	// (Invoke) Token: 0x060003BA RID: 954
	public delegate bool ParserFunction(float[] functionParams, int paramCount);
}
