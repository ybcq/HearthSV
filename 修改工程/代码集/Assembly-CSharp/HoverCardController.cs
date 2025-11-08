using System;
using System.Collections;
using UnityEngine;

public class HoverCardController : BaseController
{
	public static HoverCardController Create(MinionCard card, Transform parent)
	{
		GameObject gameObject = new GameObject(card.Name + "_HoverController");
		gameObject.transform.ChangeParent(parent);
		GameObject gameObject2 = new GameObject(card.Name + "_Root");
		gameObject2.transform.ChangeParent(gameObject.transform);
		HoverCardController hoverCardController = gameObject.AddComponent<HoverCardController>();
		hoverCardController.Card = card;
		hoverCardController.Child = gameObject2;
		hoverCardController.Initialize();
		return hoverCardController;
	}

	public override void Initialize()
	{
		this.CostController = NumberController.Create("Cost_Controller", this.Child, new Vector3(-1.375f, 2.15f, -0.01f), 43, 0.45f);
		this.AttackController = NumberController.Create("Attack_Controller", this.Child, new Vector3(-1.35f, -2.15f, -0.01f), 43, 0.45f);
		this.HealthController = NumberController.Create("Attribute_Controller", this.Child, new Vector3(1.5f, -2.15f, -0.01f), 43, 0.45f);
		if (this.Card.Rarity == CardRarity.Legendary)
		{
			this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Culled, new Vector3(0f, 0.075f, 0f), Vector3.zero, new Vector3(4f, 5.75f, 1f), 42);
		}
		else
		{
			this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Culled, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 42);
		}
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
		this.HealthController.UpdateNumber(this.Card.BaseHealth, "White");
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
		if (this.Card.Minion.GetPosition() < 5)
		{
			base.transform.localPosition = this.RIGHT_PREVIEW;
		}
		else
		{
			base.transform.localPosition = this.LEFT_PREVIEW;
		}
		for (float i = 0f; i < 1.51f; i += 0.3f)
		{
			base.transform.localScale = Vector3.one * i;
			yield return 0;
		}
		yield break;
	}

	public MinionCard Card;

	private MeshRenderer CardRenderer;

	private NumberController CostController;

	private NumberController AttackController;

	private NumberController HealthController;

	public readonly Vector3 RIGHT_PREVIEW = new Vector3(4f, 0f, -1f);

	public readonly Vector3 LEFT_PREVIEW = new Vector3(-4.5f, 0f, -1f);

	public const float DELAY = 0.5f;

	public const float SCALE = 1.5f;

	public const float STEP = 0.3f;

	private bool IsHovering;

	private float HoverTime;

	private bool IsShown;
}
