using System;
using UnityEngine;

public class PreviewCardController : BaseController
{
	public static PreviewCardController Create(BaseCard card)
	{
		GameObject gameObject = new GameObject(card.Name);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(3.5f, 5.75f, 1f);
		boxCollider.isTrigger = true;
		Animator animator = gameObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/CardAnimator");
		PreviewCardController previewCardController = gameObject.AddComponent<PreviewCardController>();
		previewCardController.Card = card;
		previewCardController.Collider = boxCollider;
		previewCardController.Initialize();
		return previewCardController;
	}

	public override void Initialize()
	{
		this.CostController = NumberController.Create("Cost_Controller", base.gameObject, new Vector3(-1.375f, 2.15f, -0.01f), 9999, 0.45f);
		this.AttackController = NumberController.Create("Attack_Controller", base.gameObject, new Vector3(-1.35f, -2.15f, -0.01f), 9999, 0.45f);
		this.AttributeController = NumberController.Create("Attribute_Controller", base.gameObject, new Vector3(1.5f, -2.15f, -0.01f), 9999, 0.45f);
		if (this.Card.Rarity == CardRarity.Legendary)
		{
			this.CardRenderer = base.CreateMesh("Card_Mesh", ShaderMode.Normal, new Vector3(0f, 0.075f, 0f), Vector3.zero, new Vector3(4f, 5.75f, 1f), 9998);
		}
		else
		{
			this.CardRenderer = base.CreateMesh("Card_Mesh", ShaderMode.Normal, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 9998);
		}
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		this.AttackController.Remove();
		this.AttributeController.Remove();
		this.CostController.Remove();
		UnityEngine.Object.Destroy(this.CardRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.CardRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/" + this.Card.Class.GetEnumName() + "/Cards/" + this.Card.GetTypeName()));
	}

	public override void UpdateNumbers()
	{
		this.CostController.UpdateNumber(this.Card.CurrentCost, Util.GetInverseNumberColor(this.Card.CurrentCost, this.Card.BaseCost));
		CardType cardType = this.Card.GetCardType();
		if (cardType != CardType.Minion)
		{
			if (cardType == CardType.Weapon)
			{
				WeaponCard weaponCard = this.Card.As<WeaponCard>();
				this.AttackController.UpdateNumber(weaponCard.CurrentAttack, Util.GetCharacterNumberColor(weaponCard.CurrentAttack, weaponCard.BaseAttack));
				this.AttributeController.UpdateNumber(weaponCard.MaxDurability, Util.GetCharacterNumberColor(weaponCard.MaxDurability, weaponCard.BaseDurability));
			}
		}
		else
		{
			MinionCard minionCard = this.Card.As<MinionCard>();
			this.AttackController.UpdateNumber(minionCard.CurrentAttack, Util.GetCharacterNumberColor(minionCard.CurrentAttack, minionCard.BaseAttack));
			this.AttributeController.UpdateNumber(minionCard.CurrentHealth, Util.GetCharacterNumberColor(minionCard.CurrentHealth, minionCard.BaseHealth));
		}
	}

	public BaseCard Card;

	private MeshRenderer CardRenderer;

	private NumberController CostController;

	private NumberController AttackController;

	private NumberController AttributeController;
}
