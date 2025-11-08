using System;
using System.Collections;
using UnityEngine;

public class HoverWeaponController : BaseController
{
	public static HoverWeaponController Create(WeaponCard card, Transform parent)
	{
		GameObject gameObject = new GameObject(card.Name + "_HoverController");
		if (card.Player.IsEnemy)
		{
			gameObject.transform.ChangeParentAt(parent, HoverWeaponController.PREVIEW_BOTTOM);
		}
		else
		{
			gameObject.transform.ChangeParentAt(parent, HoverWeaponController.PREVIEW_TOP);
		}
		GameObject gameObject2 = new GameObject(card.Name + "_Root");
		gameObject2.transform.ChangeParent(gameObject.transform);
		HoverWeaponController hoverWeaponController = gameObject.AddComponent<HoverWeaponController>();
		hoverWeaponController.Card = card;
		hoverWeaponController.Child = gameObject2;
		hoverWeaponController.Initialize();
		return hoverWeaponController;
	}

	public override void Initialize()
	{
		this.CostController = NumberController.Create("Cost_Controller", this.Child, new Vector3(-1.375f, 2.15f, -0.01f), 43, 0.45f);
		this.AttackController = NumberController.Create("Attack_Controller", this.Child, new Vector3(-1.35f, -2.15f, -0.01f), 43, 0.45f);
		this.HealthController = NumberController.Create("Attribute_Controller", this.Child, new Vector3(1.5f, -2.15f, -0.01f), 43, 0.45f);
		this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Culled, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 42);
		this.Child.SetActive(false);
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		base.StopAllCoroutines();
		this.AttackController.Remove();
		this.HealthController.Remove();
		this.CostController.Remove();
		UnityEngine.Object.Destroy(this.CardRenderer);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.CardRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/" + this.Card.Class.GetEnumName() + "/Cards/" + this.Card.GetTypeName()));
	}

	public override void UpdateNumbers()
	{
		this.CostController.UpdateNumber(this.Card.BaseCost, "White");
		this.AttackController.UpdateNumber(this.Card.BaseAttack, "White");
		this.HealthController.UpdateNumber(this.Card.BaseDurability, "White");
	}

	private void Update()
	{
		if (this.IsHovering && Time.timeSinceLevelLoad - this.HoverTime > 0.5f && !this.IsShown)
		{
			this.Child.SetActive(true);
			this.IsShown = true;
			this.AnimateZoom();
		}
	}

	public void OnMouseEnter()
	{
		this.HoverTime = Time.timeSinceLevelLoad;
		this.IsHovering = true;
	}

	public void OnMouseExit()
	{
		this.IsHovering = false;
		this.IsShown = false;
		this.Child.SetActive(false);
	}

	public void AnimateZoom()
	{
		base.StartCoroutine(this.ZoomAnimation());
	}

	private IEnumerator ZoomAnimation()
	{
		for (float i = 0f; i <= 1.36f; i += 0.27f)
		{
			base.transform.localScale = Vector3.one * i;
			yield return 0;
		}
		yield break;
	}

	public WeaponCard Card;

	private MeshRenderer CardRenderer;

	private NumberController CostController;

	private NumberController AttackController;

	private NumberController HealthController;

	public static readonly Vector3 PREVIEW_TOP = new Vector3(0f, 4f, -1f);

	public static readonly Vector3 PREVIEW_BOTTOM = new Vector3(0f, -5f, -1f);

	public const float DELAY = 0.5f;

	public const float SCALE = 1.35f;

	public const float STEP = 0.27f;

	private bool IsHovering;

	private float HoverTime;

	private bool IsShown;
}
