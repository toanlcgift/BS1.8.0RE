using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AE RID: 686
public class MissionObjectiveGameUIView : MonoBehaviour
{
	// Token: 0x06000B93 RID: 2963 RVA: 0x00034AA8 File Offset: 0x00032CA8
	public void SetMissionObjectiveChecker(MissionObjectiveChecker missionObjectiveChecker)
	{
		this._missionObjectiveChecker = missionObjectiveChecker;
		this._missionObjectiveChecker.statusDidChangeEvent += this.HandleMissionObjectiveStatusDidChange;
		this._missionObjectiveChecker.checkedValueDidChangeEvent += this.HandleMissionObjectiveCheckedValueDidChange;
		MissionObjective missionObjective = missionObjectiveChecker.missionObjective;
		if (missionObjective.type.noConditionValue)
		{
			this._conditionText.gameObject.SetActive(false);
			this._valueText.gameObject.SetActive(false);
			this._nameText.text = missionObjective.type.objectiveNameLocalized.Replace(" ", "\n");
		}
		else
		{
			this._conditionText.gameObject.SetActive(true);
			this._valueText.gameObject.SetActive(true);
			this._nameText.text = missionObjective.type.objectiveNameLocalized;
			this._conditionText.text = string.Format("{0} {1}", missionObjective.referenceValueComparisonType.Name(), missionObjective.type.objectiveValueFormater.FormatValue(missionObjective.referenceValue));
		}
		this.RefreshIcon();
		this.RefreshValue();
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0000923D File Offset: 0x0000743D
	private void HandleMissionObjectiveStatusDidChange(MissionObjectiveChecker missionObjectiveChecker)
	{
		this.RefreshIcon();
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00009245 File Offset: 0x00007445
	private void HandleMissionObjectiveCheckedValueDidChange(MissionObjectiveChecker missionObjectiveChecker)
	{
		this.RefreshValue();
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00034BC4 File Offset: 0x00032DC4
	private void RefreshIcon()
	{
		switch (this._missionObjectiveChecker.status)
		{
		case MissionObjectiveChecker.Status.NotClearedYet:
			this._resultIcon.sprite = this._notClearedIcon;
			this._resultIcon.color = this._nonFinalIconColor;
			return;
		case MissionObjectiveChecker.Status.NotFailedYet:
			this._resultIcon.sprite = this._notFailedIcon;
			this._resultIcon.color = this._nonFinalIconColor;
			return;
		case MissionObjectiveChecker.Status.Cleared:
			this._resultIcon.sprite = this._clearedIcon;
			this._resultIcon.color = this._finalClearIconColor;
			this._clearedPS.Emit(this._numberOfParticles);
			return;
		case MissionObjectiveChecker.Status.Failed:
			this._resultIcon.sprite = this._failedIcon;
			this._resultIcon.color = this._finalFailIconColor;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00034C94 File Offset: 0x00032E94
	private void RefreshValue()
	{
		string text = this._missionObjectiveChecker.missionObjectiveType.objectiveValueFormater.FormatValue(this._missionObjectiveChecker.checkedValue);
		this._valueText.text = text;
	}

	// Token: 0x04000C3E RID: 3134
	[SerializeField]
	private Sprite _notFailedIcon;

	// Token: 0x04000C3F RID: 3135
	[SerializeField]
	private Sprite _failedIcon;

	// Token: 0x04000C40 RID: 3136
	[SerializeField]
	private Sprite _notClearedIcon;

	// Token: 0x04000C41 RID: 3137
	[SerializeField]
	private Sprite _clearedIcon;

	// Token: 0x04000C42 RID: 3138
	[SerializeField]
	private Image _resultIcon;

	// Token: 0x04000C43 RID: 3139
	[SerializeField]
	private Color _finalClearIconColor;

	// Token: 0x04000C44 RID: 3140
	[SerializeField]
	private Color _finalFailIconColor;

	// Token: 0x04000C45 RID: 3141
	[SerializeField]
	private Color _nonFinalIconColor;

	// Token: 0x04000C46 RID: 3142
	[SerializeField]
	private ParticleSystem _clearedPS;

	// Token: 0x04000C47 RID: 3143
	[SerializeField]
	private int _numberOfParticles = 70;

	// Token: 0x04000C48 RID: 3144
	[Space]
	[SerializeField]
	private TextMeshProUGUI _nameText;

	// Token: 0x04000C49 RID: 3145
	[SerializeField]
	private TextMeshProUGUI _valueText;

	// Token: 0x04000C4A RID: 3146
	[SerializeField]
	private TextMeshProUGUI _conditionText;

	// Token: 0x04000C4B RID: 3147
	private MissionObjectiveChecker _missionObjectiveChecker;
}
