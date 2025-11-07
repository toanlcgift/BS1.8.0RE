using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class NoteCutInfo
{
	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000649 RID: 1609 RVA: 0x00005A99 File Offset: 0x00003C99
	// (set) Token: 0x0600064A RID: 1610 RVA: 0x00005AA1 File Offset: 0x00003CA1
	public bool speedOK { get; private set; }

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x0600064B RID: 1611 RVA: 0x00005AAA File Offset: 0x00003CAA
	// (set) Token: 0x0600064C RID: 1612 RVA: 0x00005AB2 File Offset: 0x00003CB2
	public bool directionOK { get; private set; }

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x0600064D RID: 1613 RVA: 0x00005ABB File Offset: 0x00003CBB
	// (set) Token: 0x0600064E RID: 1614 RVA: 0x00005AC3 File Offset: 0x00003CC3
	public bool saberTypeOK { get; private set; }

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x0600064F RID: 1615 RVA: 0x00005ACC File Offset: 0x00003CCC
	// (set) Token: 0x06000650 RID: 1616 RVA: 0x00005AD4 File Offset: 0x00003CD4
	public bool wasCutTooSoon { get; private set; }

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000651 RID: 1617 RVA: 0x00005ADD File Offset: 0x00003CDD
	// (set) Token: 0x06000652 RID: 1618 RVA: 0x00005AE5 File Offset: 0x00003CE5
	public float saberSpeed { get; private set; }

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000653 RID: 1619 RVA: 0x00005AEE File Offset: 0x00003CEE
	// (set) Token: 0x06000654 RID: 1620 RVA: 0x00005AF6 File Offset: 0x00003CF6
	public Vector3 saberDir { get; private set; }

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000655 RID: 1621 RVA: 0x00005AFF File Offset: 0x00003CFF
	// (set) Token: 0x06000656 RID: 1622 RVA: 0x00005B07 File Offset: 0x00003D07
	public SaberType saberType { get; private set; }

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000657 RID: 1623 RVA: 0x00005B10 File Offset: 0x00003D10
	// (set) Token: 0x06000658 RID: 1624 RVA: 0x00005B18 File Offset: 0x00003D18
	public float timeDeviation { get; private set; }

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000659 RID: 1625 RVA: 0x00005B21 File Offset: 0x00003D21
	// (set) Token: 0x0600065A RID: 1626 RVA: 0x00005B29 File Offset: 0x00003D29
	public float cutDirDeviation { get; private set; }

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x0600065B RID: 1627 RVA: 0x00005B32 File Offset: 0x00003D32
	// (set) Token: 0x0600065C RID: 1628 RVA: 0x00005B3A File Offset: 0x00003D3A
	public Vector3 cutPoint { get; private set; }

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x0600065D RID: 1629 RVA: 0x00005B43 File Offset: 0x00003D43
	// (set) Token: 0x0600065E RID: 1630 RVA: 0x00005B4B File Offset: 0x00003D4B
	public Vector3 cutNormal { get; private set; }

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x0600065F RID: 1631 RVA: 0x00005B54 File Offset: 0x00003D54
	// (set) Token: 0x06000660 RID: 1632 RVA: 0x00005B5C File Offset: 0x00003D5C
	public float cutDistanceToCenter { get; private set; }

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000661 RID: 1633 RVA: 0x00005B65 File Offset: 0x00003D65
	// (set) Token: 0x06000662 RID: 1634 RVA: 0x00005B6D File Offset: 0x00003D6D
	public SaberSwingRatingCounter swingRatingCounter { get; private set; }

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000663 RID: 1635 RVA: 0x00005B76 File Offset: 0x00003D76
	public bool allIsOK
	{
		get
		{
			return this.speedOK && this.directionOK && this.saberTypeOK && !this.wasCutTooSoon;
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06000664 RID: 1636 RVA: 0x00005B9B File Offset: 0x00003D9B
	public bool allExceptSaberTypeIsOK
	{
		get
		{
			return this.speedOK && this.directionOK && !this.wasCutTooSoon;
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000665 RID: 1637 RVA: 0x00005BB8 File Offset: 0x00003DB8
	public string FailText
	{
		get
		{
			if (this.wasCutTooSoon)
			{
				return "TOO SOON!";
			}
			if (!this.saberTypeOK)
			{
				return "WRONG\nCOLOR!";
			}
			if (!this.speedOK)
			{
				return "CUT\nHARDER!";
			}
			if (!this.directionOK)
			{
				return "WRONG\nDIRECTION!";
			}
			return "";
		}
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00024B98 File Offset: 0x00022D98
	public NoteCutInfo(bool speedOK, bool directionOK, bool saberTypeOK, bool wasCutTooSoon, float saberSpeed, Vector3 saberDir, SaberType saberType, float timeDeviation, float cutDirDeviation, Vector3 cutCenter, Vector3 cutNormal, SaberSwingRatingCounter swingRatingCounter, float cutDistanceToCenter)
	{
		this.speedOK = speedOK;
		this.directionOK = directionOK;
		this.saberTypeOK = saberTypeOK;
		this.wasCutTooSoon = wasCutTooSoon;
		this.saberSpeed = saberSpeed;
		this.saberDir = saberDir;
		this.saberType = saberType;
		this.cutPoint = cutCenter;
		this.cutNormal = cutNormal;
		this.timeDeviation = timeDeviation;
		this.cutDirDeviation = cutDirDeviation;
		this.swingRatingCounter = swingRatingCounter;
		this.cutDistanceToCenter = cutDistanceToCenter;
	}
}
