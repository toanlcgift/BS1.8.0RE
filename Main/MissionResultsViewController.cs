using System;
using System.Collections;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020003F4 RID: 1012
public class MissionResultsViewController : ViewController
{
	// Token: 0x140000AB RID: 171
	// (add) Token: 0x060012F2 RID: 4850 RVA: 0x00046CB8 File Offset: 0x00044EB8
	// (remove) Token: 0x060012F3 RID: 4851 RVA: 0x00046CF0 File Offset: 0x00044EF0
	public event Action<MissionResultsViewController> continueButtonPressedEvent;

	// Token: 0x140000AC RID: 172
	// (add) Token: 0x060012F4 RID: 4852 RVA: 0x00046D28 File Offset: 0x00044F28
	// (remove) Token: 0x060012F5 RID: 4853 RVA: 0x00046D60 File Offset: 0x00044F60
	public event Action<MissionResultsViewController> retryButtonPressedEvent;

	// Token: 0x060012F6 RID: 4854 RVA: 0x0000E4EB File Offset: 0x0000C6EB
	public void Init(MissionNode missionNode, MissionCompletionResults missionCompletionResults)
	{
		this._missionNode = missionNode;
		this._missionCompletionResults = missionCompletionResults;
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x00046D98 File Offset: 0x00044F98
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._continueButton, new Action(this.ContinueButtonPressed));
			base.buttonBinder.AddBinding(this._retryButton, new Action(this.RetryButtonPressed));
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.SetDataToUI();
			if (this._missionCompletionResults.IsMissionComplete)
			{
				this._startFireworksAfterDelayCoroutine = base.StartCoroutine(this.StartFireworksAfterDelay(1.95f));
				this._songPreviewPlayer.CrossfadeTo(this._levelClearedAudioClip, 0f, this._levelClearedAudioClip.length, 1f);
			}
		}
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x0000E4FB File Offset: 0x0000C6FB
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (this._startFireworksAfterDelayCoroutine != null)
		{
			base.StopCoroutine(this._startFireworksAfterDelayCoroutine);
			this._startFireworksAfterDelayCoroutine = null;
		}
		this._fireworksController.enabled = false;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x0000E524 File Offset: 0x0000C724
	private IEnumerator StartFireworksAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		this._fireworksController.enabled = true;
		yield break;
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x00046E38 File Offset: 0x00045038
	private void SetDataToUI()
	{
		bool levelCleared = this._missionCompletionResults.levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared;
		MissionObjectiveResult[] missionObjectiveResults = this._missionCompletionResults.missionObjectiveResults;
		MissionObjective[] missionObjectives = this._missionNode.missionData.missionObjectives;
		this._resultObjectiveListItemList.SetData(missionObjectives.Length + 1, delegate(int idx, ResultObjectiveListItem objectiveListItem)
		{
			if (idx == 0)
			{
				objectiveListItem.title = Localization.Get("CAMPAIGN_FINISH_LEVEL");
				objectiveListItem.conditionText = "";
				objectiveListItem.valueText = "";
				objectiveListItem.icon = (levelCleared ? this._successIcon : this._failIcon);
				objectiveListItem.iconGlow = (levelCleared ? this._successIconGlow : this._failIconGlow);
				objectiveListItem.iconColor = (levelCleared ? this._successColor : this._failColor);
				objectiveListItem.hideValueText = true;
				objectiveListItem.hideConditionText = true;
				return;
			}
			MissionObjective missionObjective = missionObjectives[idx - 1];
			MissionObjectiveResult missionObjectiveResult = null;
			foreach (MissionObjectiveResult missionObjectiveResult2 in missionObjectiveResults)
			{
				if (missionObjectiveResult2.missionObjective == missionObjective)
				{
					missionObjectiveResult = missionObjectiveResult2;
					break;
				}
			}
			ObjectiveValueFormatterSO objectiveValueFormater = missionObjective.type.objectiveValueFormater;
			if (missionObjective.type.noConditionValue)
			{
				objectiveListItem.title = missionObjective.type.objectiveNameLocalized.Replace(" ", "\n");
				objectiveListItem.hideValueText = true;
				objectiveListItem.hideConditionText = true;
			}
			else
			{
				objectiveListItem.hideValueText = false;
				objectiveListItem.hideConditionText = false;
				objectiveListItem.title = missionObjective.type.objectiveNameLocalized;
				objectiveListItem.conditionText = string.Format("{0} {1}", missionObjective.referenceValueComparisonType.Name(), objectiveValueFormater.FormatValue(missionObjective.referenceValue));
			}
			objectiveListItem.valueText = objectiveValueFormater.FormatValue(missionObjectiveResult.value);
			objectiveListItem.icon = (missionObjectiveResult.cleared ? this._successIcon : this._failIcon);
			objectiveListItem.iconGlow = (missionObjectiveResult.cleared ? this._successIconGlow : this._failIconGlow);
			objectiveListItem.iconColor = (missionObjectiveResult.cleared ? this._successColor : this._failColor);
		});
		this._missionNameText.text = Localization.Get("CAMPAIGN_MISSION") + " " + this._missionNode.formattedMissionNodeName;
		this._songNameText.text = this._missionNode.missionData.level.songName;
		if (this._missionCompletionResults.IsMissionComplete)
		{
			this._resultText.text = Localization.Get("CAMPAIGN_MISSION_COMPLETE");
			this._resultText.color = this._successColor;
			this._retryButton.gameObject.SetActive(false);
			return;
		}
		this._resultText.text = Localization.Get("CAMPAIGN_MISSION_FAILED");
		this._resultText.color = this._failColor;
		this._retryButton.gameObject.SetActive(true);
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x0000E53A File Offset: 0x0000C73A
	private void ContinueButtonPressed()
	{
		Action<MissionResultsViewController> action = this.continueButtonPressedEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x0000E54D File Offset: 0x0000C74D
	private void RetryButtonPressed()
	{
		Action<MissionResultsViewController> action = this.retryButtonPressedEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x040012A9 RID: 4777
	[SerializeField]
	private TextMeshProUGUI _resultText;

	// Token: 0x040012AA RID: 4778
	[SerializeField]
	private TextMeshProUGUI _missionNameText;

	// Token: 0x040012AB RID: 4779
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x040012AC RID: 4780
	[SerializeField]
	private Sprite _successIcon;

	// Token: 0x040012AD RID: 4781
	[SerializeField]
	private Sprite _successIconGlow;

	// Token: 0x040012AE RID: 4782
	[SerializeField]
	private Color _successColor;

	// Token: 0x040012AF RID: 4783
	[SerializeField]
	private Sprite _failIcon;

	// Token: 0x040012B0 RID: 4784
	[SerializeField]
	private Sprite _failIconGlow;

	// Token: 0x040012B1 RID: 4785
	[SerializeField]
	private Color _failColor;

	// Token: 0x040012B2 RID: 4786
	[SerializeField]
	private ResultObjectiveListItemsList _resultObjectiveListItemList;

	// Token: 0x040012B3 RID: 4787
	[SerializeField]
	private Button _continueButton;

	// Token: 0x040012B4 RID: 4788
	[SerializeField]
	private Button _retryButton;

	// Token: 0x040012B5 RID: 4789
	[Space]
	[SerializeField]
	private AudioClip _levelClearedAudioClip;

	// Token: 0x040012B6 RID: 4790
	[Inject]
	private FireworksController _fireworksController;

	// Token: 0x040012B7 RID: 4791
	[Inject]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x040012BA RID: 4794
	private MissionNode _missionNode;

	// Token: 0x040012BB RID: 4795
	private MissionCompletionResults _missionCompletionResults;

	// Token: 0x040012BC RID: 4796
	private Coroutine _startFireworksAfterDelayCoroutine;
}
