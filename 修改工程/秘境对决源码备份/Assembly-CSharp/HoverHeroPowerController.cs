using System;
using System.Collections;
using UnityEngine;

public class HoverHeroPowerController : BaseController
{
	public static HoverHeroPowerController Create(BaseHeroPower card, Transform parent)
	{
		GameObject gameObject = new GameObject(card.Name + "_HoverController");
		gameObject.transform.ChangeParentAt(parent, HoverHeroPowerController.PREVIEW_BELOW);
		GameObject gameObject2 = new GameObject(card.Name + "_Root");
		gameObject2.transform.ChangeParent(gameObject.transform);
		HoverHeroPowerController hoverHeroPowerController = gameObject.AddComponent<HoverHeroPowerController>();
		hoverHeroPowerController.HeroPower = card;
		hoverHeroPowerController.Child = gameObject2;
		hoverHeroPowerController.Initialize();
		return hoverHeroPowerController;
	}

	public override void Initialize()
	{
		if (this.HeroPower.Hero.Player.IsEnemy)
		{
			base.transform.localPosition = HoverHeroPowerController.PREVIEW_BELOW;
		}
		else
		{
			base.transform.localPosition = HoverHeroPowerController.PREVIEW_ABOVE;
		}
		this.CostController = NumberController.Create("Cost_Controller", this.Child, new Vector3(0f, 2.25f, -0.01f), 43, 0.45f);
		this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Culled, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 42);
		this.Child.SetActive(false);
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		base.StopAllCoroutines();
		this.CostController.Remove();
		UnityEngine.Object.Destroy(this.CardRenderer);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.CardRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(string.Concat(new string[]
		{
			"Sprites/HeroPowers/",
			this.HeroPower.Class.GetEnumName(),
			"/",
			this.HeroPower.GetTypeName(),
			"_Card"
		})));
	}

	public override void UpdateNumbers()
	{
		this.CostController.UpdateNumber(this.HeroPower.CurrentCost, "White");
	}

	public void SetRenderingOrder(int order)
	{
		this.CostController.SetRenderingOrder(order + 3);
		this.CardRenderer.sortingOrder = order + 2;
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
		for (float i = 0f; i <= 1.66f; i += 0.33f)
		{
			base.transform.localScale = Vector3.one * i;
			yield return 0;
		}
		yield break;
	}

	public BaseHeroPower HeroPower;

	private MeshRenderer CardRenderer;

	private NumberController CostController;

	public static readonly Vector3 PREVIEW_BELOW = new Vector3(0f, -6f, -1f);

	public static readonly Vector3 PREVIEW_ABOVE = new Vector3(0f, 6f, -1f);

	public const float DELAY = 0.5f;

	public const float SCALE = 1.65f;

	public const float STEP = 0.33f;

	private bool IsHovering;

	private float HoverTime;

	private bool IsShown;
}
