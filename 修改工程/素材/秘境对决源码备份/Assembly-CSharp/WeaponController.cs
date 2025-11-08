using System;
using System.Collections;
using UnityEngine;

public class WeaponController : BaseController
{
	public static WeaponController Create(Player player, Weapon weapon)
	{
		GameObject gameObject = new GameObject("Weapon_Controller");
		gameObject.transform.ChangeParentAt(player.transform, new Vector3(-4f, 0.5f, 0f));
		gameObject.transform.localScale = Vector3.one * 1.3f;
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(2f, 2f, 1f);
		boxCollider.isTrigger = true;
		WeaponController weaponController = gameObject.AddComponent<WeaponController>();
		weaponController.Weapon = weapon;
		weaponController.Collider = boxCollider;
		weaponController.HoverController = HoverWeaponController.Create(weapon.Card, gameObject.transform);
		weaponController.Initialize();
		return weaponController;
	}

	public override void Initialize()
	{
		this.AttackController = NumberController.Create("Attack_Controller", base.gameObject, new Vector3(-0.725f, -0.675f, 0f), 15, 0.35f);
		this.DurabilityController = NumberController.Create("Durability_Controller", base.gameObject, new Vector3(0.725f, -0.675f, 0f), 15, 0.35f);
		this.DeathrattleRenderer = Util.Instantiate(EffectsManager.Instance.WeaponDeathrattlePrefab, base.transform);
		this.TriggerRenderer = Util.Instantiate(EffectsManager.Instance.WeaponTriggerPrefab, base.transform);
		this.TriggerFlashRenderer = Util.Instantiate(EffectsManager.Instance.WeaponTriggerFlashPrefab, base.transform);
		this.TriggerFlashRenderer.SetActive(false);
		this.OpenTokenRenderer = base.CreateSprite("OpenToken_Sprite", Vector3.one, Vector3.zero, 14);
		this.ClosedTokenRenderer = base.CreateSprite("ClosedToken_Sprite", Vector3.one, Vector3.zero, 14);
		this.WeaponRenderer = base.CreateSprite("Weapon_Sprite", Vector3.one, Vector3.zero, 13);
		this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 1.5f, Vector3.zero, 12);
		this.GreenGlowRenderer = base.CreateSprite("GreenGlow_Sprite", Vector3.one * 1.5f, Vector3.zero, 11);
		this.RedGlowRenderer = base.CreateSprite("RedGlow_Sprite", Vector3.one * 1.5f, Vector3.zero, 10);
		this.OpenTokenRenderer.enabled = true;
		this.WeaponRenderer.enabled = true;
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		this.AttackController.Remove();
		this.DurabilityController.Remove();
		UnityEngine.Object.Destroy(this.OpenTokenRenderer);
		UnityEngine.Object.Destroy(this.ClosedTokenRenderer);
		UnityEngine.Object.Destroy(this.WeaponRenderer);
		UnityEngine.Object.Destroy(this.WhiteGlowRenderer);
		UnityEngine.Object.Destroy(this.GreenGlowRenderer);
		UnityEngine.Object.Destroy(this.RedGlowRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.OpenTokenRenderer.sprite = ResourcesManager.Tokens["Weapon_Open"];
		this.ClosedTokenRenderer.sprite = ResourcesManager.Tokens["Weapon_Closed"];
		this.WeaponRenderer.sprite = Resources.Load<Sprite>("Sprites/" + this.Weapon.Card.Class.GetEnumName() + "/Weapons/" + this.Weapon.Card.GetTypeName());
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows["Weapon_WhiteGlow"];
		this.GreenGlowRenderer.sprite = ResourcesManager.Glows["Weapon_GreenGlow"];
		this.RedGlowRenderer.sprite = ResourcesManager.Glows["Weapon_RedGlow"];
		this.DeathrattleRenderer.SetActive(this.Weapon.Card.Mechanics.HasDeathrattle());
		this.TriggerRenderer.SetActive(this.Weapon.Card.Mechanics.HasTrigger());
	}

	public override void UpdateNumbers()
	{
		this.AttackController.UpdateNumber(this.Weapon.CurrentAttack, Util.GetCharacterNumberColor(this.Weapon.CurrentAttack, this.Weapon.BaseAttack));
		this.DurabilityController.UpdateNumber(this.Weapon.CurrentDurability, Util.GetCharacterNumberColor(this.Weapon.CurrentDurability, this.Weapon.BaseDurability, this.Weapon.MaxDurability));
	}

	private void OnMouseEnter()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(true);
			this.HoverController.OnMouseEnter();
		}
	}

	private void OnMouseExit()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(false);
			this.HoverController.OnMouseExit();
		}
	}

	public void AnimateTriggerFlash()
	{
		base.StartCoroutine(this.TriggerFlashAnimation());
	}

	public IEnumerator TriggerFlashAnimation()
	{
		this.TriggerFlashRenderer.SetActive(true);
		SoundManager.Instance.Play("Game_Mechanic_Trigger");
		yield return new WaitForSeconds(1.5f);
		this.TriggerFlashRenderer.SetActive(false);
		yield break;
	}

	public Weapon Weapon;

	public SpriteRenderer WeaponRenderer;

	public SpriteRenderer OpenTokenRenderer;

	public SpriteRenderer ClosedTokenRenderer;

	private GameObject DeathrattleRenderer;

	private GameObject TriggerRenderer;

	private GameObject TriggerFlashRenderer;

	public NumberController AttackController;

	public NumberController DurabilityController;

	private HoverWeaponController HoverController;
}
