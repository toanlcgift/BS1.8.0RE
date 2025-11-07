using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A1 RID: 929
public class MissionNodeConnection : MonoBehaviour
{
	// Token: 0x17000388 RID: 904
	// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0000CCF1 File Offset: 0x0000AEF1
	public MissionNodeVisualController parentMissionNode
	{
		get
		{
			return this._parentMissionNode;
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0000CCF9 File Offset: 0x0000AEF9
	public MissionNodeVisualController childMissionNode
	{
		get
		{
			return this._childMissionNode;
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0000CD01 File Offset: 0x0000AF01
	public bool isActive
	{
		get
		{
			return this._isActive;
		}
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0000CD09 File Offset: 0x0000AF09
	public void Setup(MissionNodeVisualController parentMissionNode, MissionNodeVisualController childMissionNode)
	{
		this._parentMissionNode = parentMissionNode;
		this._childMissionNode = childMissionNode;
		this.UpdateConnectionRectTransform();
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x00041744 File Offset: 0x0003F944
	public void UpdateConnectionRectTransform()
	{
		if (this._parentMissionNodePosition == this._parentMissionNode.missionNode.position && this._childMissionNodePosition == this._childMissionNode.missionNode.position)
		{
			return;
		}
		this._parentMissionNodePosition = this._parentMissionNode.missionNode.position;
		this._childMissionNodePosition = this._childMissionNode.missionNode.position;
		Vector2 vector = this._childMissionNodePosition - this._parentMissionNodePosition;
		float z = 57.29578f * Mathf.Atan2(vector.y, vector.x);
		this._rectTransform.localPosition = Vector3.zero;
		this._rectTransform.anchoredPosition = (this._parentMissionNodePosition + this._childMissionNodePosition) / 2f;
		this._rectTransform.rotation = Quaternion.Euler(0f, 0f, z);
		this._rectTransform.localScale = Vector3.one;
		this._rectTransform.sizeDelta = new Vector2(vector.magnitude - this._childMissionNode.missionNode.radius - this._separator * 2f, this._width);
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00041880 File Offset: 0x0003FA80
	public void SetActive(bool animated)
	{
		this._isActive = true;
		if (animated && base.gameObject.activeInHierarchy)
		{
			this._animator.enabled = true;
			this._animator.SetBool("MissionConnectionEnabled", true);
			return;
		}
		this._image.color = Color.white;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0000CD1F File Offset: 0x0000AF1F
	public void MissionConnectionEnabledDidFinish()
	{
		this._animator.enabled = false;
	}

	// Token: 0x040010F0 RID: 4336
	[SerializeField]
	private float _separator = 2f;

	// Token: 0x040010F1 RID: 4337
	[SerializeField]
	private float _width = 0.8f;

	// Token: 0x040010F2 RID: 4338
	[SerializeField]
	private RectTransform _rectTransform;

	// Token: 0x040010F3 RID: 4339
	[SerializeField]
	private Image _image;

	// Token: 0x040010F4 RID: 4340
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private MissionNodeVisualController _parentMissionNode;

	// Token: 0x040010F5 RID: 4341
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private MissionNodeVisualController _childMissionNode;

	// Token: 0x040010F6 RID: 4342
	[SerializeField]
	private Animator _animator;

	// Token: 0x040010F7 RID: 4343
	private Vector2 _parentMissionNodePosition;

	// Token: 0x040010F8 RID: 4344
	private Vector2 _childMissionNodePosition;

	// Token: 0x040010F9 RID: 4345
	private bool _isActive;
}
