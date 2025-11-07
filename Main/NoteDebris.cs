using System;
using UnityEngine;
using Zenject;

// Token: 0x0200024A RID: 586
[RequireComponent(typeof(Rigidbody))]
public class NoteDebris : MonoBehaviour
{
	// Token: 0x1400002F RID: 47
	// (add) Token: 0x060009A5 RID: 2469 RVA: 0x0002DEEC File Offset: 0x0002C0EC
	// (remove) Token: 0x060009A6 RID: 2470 RVA: 0x0002DF24 File Offset: 0x0002C124
	public event Action<NoteDebris> didFinishEvent;

	// Token: 0x060009A7 RID: 2471 RVA: 0x0000792C File Offset: 0x00005B2C
	protected void Awake()
	{
		if (NoteDebris._meshVertices == null)
		{
			NoteDebris._meshVertices = this._centroidComputationMesh.vertices;
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0002DF5C File Offset: 0x0002C15C
	protected void Update()
	{
		if (this._elapsedTime < this._lifeTime)
		{
			this._materialPropertyBlockController.materialPropertyBlock.SetFloat(NoteDebris._cutoutPropertyID, this._cutoutCurve.Evaluate(this._elapsedTime / this._lifeTime));
			this._materialPropertyBlockController.ApplyChanges();
			this._elapsedTime += Time.deltaTime;
			return;
		}
		Action<NoteDebris> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
	public void Init(NoteType noteType, Transform initTransform, Vector3 cutPoint, Vector3 cutNormal, Vector3 force, Vector3 torque, float lifeTime)
	{
		Vector3 vector = initTransform.InverseTransformPoint(cutPoint);
		Vector3 vector2 = initTransform.InverseTransformVector(cutNormal);
		float magnitude = vector.magnitude;
		if (magnitude > this._maxCutPointCenterDistance * this._maxCutPointCenterDistance)
		{
			vector = this._maxCutPointCenterDistance * vector / Mathf.Sqrt(magnitude);
		}
		Vector4 vector3 = vector2;
		vector3.w = -Vector3.Dot(vector2, vector);
		float num = Mathf.Sqrt(Vector3.Dot(vector3, vector3));
		Vector3 vector4 = Vector3.zero;
		int num2 = NoteDebris._meshVertices.Length;
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector5 = NoteDebris._meshVertices[i];
			float num3 = Vector3.Dot(vector3, vector5) + vector3.w;
			if (num3 < 0f)
			{
				float d = num3 / num;
				Vector3 a = vector5 - (Vector3)vector3 * d;
				vector4 += a / (float)num2;
			}
			else
			{
				vector4 += vector5 / (float)num2;
			}
		}
		base.transform.SetPositionAndRotation(initTransform.TransformPoint(vector4), initTransform.rotation);
		base.transform.localScale = initTransform.localScale;
		this._meshTransform.localPosition = -vector4;
		this._rigidbody.velocity = Vector3.zero;
		this._rigidbody.angularVelocity = Vector3.zero;
		this._rigidbody.AddForce(force, ForceMode.VelocityChange);
		this._rigidbody.AddTorque(torque, ForceMode.VelocityChange);
		Color value = this._colorManager.ColorForNoteType(noteType);
		MaterialPropertyBlock materialPropertyBlock = this._materialPropertyBlockController.materialPropertyBlock;
		materialPropertyBlock.Clear();
		materialPropertyBlock.SetColor(NoteDebris._colorID, value);
		materialPropertyBlock.SetVector(NoteDebris._cutPlaneID, vector3);
		materialPropertyBlock.SetVector(NoteDebris._cutoutTexOffsetID, UnityEngine.Random.insideUnitSphere);
		materialPropertyBlock.SetFloat(NoteDebris._cutoutPropertyID, 0f);
		this._materialPropertyBlockController.ApplyChanges();
		this._lifeTime = lifeTime;
		this._elapsedTime = 0f;
	}

	// Token: 0x040009D5 RID: 2517
	[SerializeField]
	private Transform _meshTransform;

	// Token: 0x040009D6 RID: 2518
	[SerializeField]
	private Rigidbody _rigidbody;

	// Token: 0x040009D7 RID: 2519
	[SerializeField]
	private MaterialPropertyBlockController _materialPropertyBlockController;

	// Token: 0x040009D8 RID: 2520
	[Space]
	[SerializeField]
	private AnimationCurve _cutoutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x040009D9 RID: 2521
	[SerializeField]
	private float _maxCutPointCenterDistance = 0.2f;

	// Token: 0x040009DA RID: 2522
	[Space]
	[SerializeField]
	private Mesh _centroidComputationMesh;

	// Token: 0x040009DB RID: 2523
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x040009DD RID: 2525
	private float _elapsedTime;

	// Token: 0x040009DE RID: 2526
	private float _lifeTime;

	// Token: 0x040009DF RID: 2527
	[DoesNotRequireDomainReloadInit]
	private static readonly int _cutoutPropertyID = Shader.PropertyToID("_Cutout");

	// Token: 0x040009E0 RID: 2528
	[DoesNotRequireDomainReloadInit]
	private static readonly int _colorID = Shader.PropertyToID("_Color");

	// Token: 0x040009E1 RID: 2529
	[DoesNotRequireDomainReloadInit]
	private static readonly int _cutPlaneID = Shader.PropertyToID("_CutPlane");

	// Token: 0x040009E2 RID: 2530
	[DoesNotRequireDomainReloadInit]
	private static readonly int _cutoutTexOffsetID = Shader.PropertyToID("_CutoutTexOffset");

	// Token: 0x040009E3 RID: 2531
	[DoesNotRequireDomainReloadInit]
	private static Vector3[] _meshVertices;

	// Token: 0x0200024B RID: 587
	public class Pool : MemoryPoolWithActiveItems<NoteDebris>
	{
	}
}
