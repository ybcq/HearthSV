using System;
using UnityEngine;

public class MeditateController : BaseController
{
	public static MeditateController Create(Player player)
	{
		GameObject gameObject = new GameObject("Meditate_Controller");
		gameObject.transform.ChangeParentAt(player.transform, new Vector3(0.05f, -1f, 0f));
		SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
		sphereCollider.radius = 0.3f;
		sphereCollider.isTrigger = true;
		MeditateController meditateController = gameObject.AddComponent<MeditateController>();
		meditateController.Player = player;
		meditateController.Collider = sphereCollider;
		meditateController.Initialize();
		return meditateController;
	}

	public override void Initialize()
	{
		this.QuantityController = NumberController.Create("Quantity_Controller", base.gameObject, Vector3.zero, 39, 0.45f);
		this.MeditateRenderer = base.CreateSprite("Meditate", Vector3.one * 2f, Vector3.zero, 38);
		this.GreenGlowRenderer = base.CreateSprite("GreenGlow", Vector3.one * 1.25f, Vector3.zero, 37);
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		UnityEngine.Object.Destroy(this.QuantityController);
		UnityEngine.Object.Destroy(this.MeditateRenderer);
		UnityEngine.Object.Destroy(this.GreenGlowRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.MeditateRenderer.sprite = ResourcesManager.Effects["Meditate"];
		this.GreenGlowRenderer.sprite = ResourcesManager.Glows["Meditate_GreenGlow"];
		bool enabled = this.Player.Meditations.Count > 0;
		this.MeditateRenderer.enabled = enabled;
		this.GreenGlowRenderer.enabled = enabled;
	}

	public override void UpdateNumbers()
	{
	}

	public Player Player;

	private SpriteRenderer MeditateRenderer;

	private NumberController QuantityController;
}
