using System;
using System.Linq;
using UnityEngine;

public class BuilderCardController : BaseController
{
	public static BuilderCardController Create(BaseCard card)
	{
		GameObject gameObject = new GameObject(card.Name);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(3.5f, 5.5f, 1f);
		boxCollider.isTrigger = true;
		BuilderCardController builderCardController = gameObject.AddComponent<BuilderCardController>();
		builderCardController.Card = card;
		builderCardController.Collider = boxCollider;
		builderCardController.Initialize();
		return builderCardController;
	}

	public override void Initialize()
	{
		this.CostController = NumberController.Create("Cost_Controller", base.gameObject, new Vector3(-1.375f, 2.15f, -0.01f), 8, 0.5f);
		this.AttackController = NumberController.Create("Attack_Controller", base.gameObject, new Vector3(-1.35f, -2.15f, -0.01f), 8, 0.5f);
		this.AttributeController = NumberController.Create("Attribute_Controller", base.gameObject, new Vector3(1.5f, -2.15f, -0.01f), 8, 0.5f);
		if (this.Card is MinionCard && this.Card.Rarity == CardRarity.Legendary)
		{
			this.CardRenderer = base.CreateMesh("Card_Mesh", ShaderMode.Transparent, new Vector3(0f, 0.075f, 0f), Vector3.zero, new Vector3(4f, 5.75f, 1f), 7);
		}
		else
		{
			this.CardRenderer = base.CreateMesh("Card_Mesh", ShaderMode.Transparent, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 7);
		}
		this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 2.5f, new Vector3(0f, 0.1f, 0f), 6);
		this.LimitRenderer = base.CreateMesh("Limit_Mesh", ShaderMode.Transparent, new Vector3(0.05f, -0.16f, 0f), Vector3.zero, new Vector3(4f, 6.5f, 1f), 5);
		this.LimitRenderer.enabled = false;
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
		this.LimitRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/DeckBuilder/Border_" + this.Card.GetCardType().GetEnumName() + "_Limit"));
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows["Card_" + this.GetGlowType() + "_WhiteGlow"];
		if (this.Card.Rarity == CardRarity.Legendary)
		{
			this.LimitRenderer.enabled = DeckBuilder.Instance.SelectedCards.Any((BuilderSelectedCardController x) => x.Card.Name == this.Card.Name);
		}
		else
		{
			BuilderSelectedCardController builderSelectedCardController = DeckBuilder.Instance.SelectedCards.FirstOrDefault((BuilderSelectedCardController x) => x.Card.Name == this.Card.Name);
			if (builderSelectedCardController != null)
			{
				this.LimitRenderer.enabled = (builderSelectedCardController.Type == SelectedCardType.Double);
			}
			else
			{
				this.LimitRenderer.enabled = false;
			}
		}
	}

	public override void UpdateNumbers()
	{
		this.CostController.UpdateNumber(this.Card.BaseCost, "White");
		CardType cardType = this.Card.GetCardType();
		if (cardType != CardType.Minion)
		{
			if (cardType == CardType.Weapon)
			{
				WeaponCard weaponCard = this.Card.As<WeaponCard>();
				this.AttackController.UpdateNumber(weaponCard.BaseAttack, "White");
				this.AttributeController.UpdateNumber(weaponCard.BaseDurability, "White");
			}
		}
		else
		{
			MinionCard minionCard = this.Card.As<MinionCard>();
			this.AttackController.UpdateNumber(minionCard.BaseAttack, "White");
			this.AttributeController.UpdateNumber(minionCard.BaseHealth, "White");
		}
	}

	private string GetGlowType()
	{
		string name = this.Card.GetType().BaseType.Name;
		if (name != null)
		{
			if (!(name == "MinionCard"))
			{
				if (name == "SpellCard")
				{
					return "Spell";
				}
				if (name == "WeaponCard")
				{
					return "Weapon";
				}
			}
			else
			{
				if (this.Card.As<MinionCard>().Rarity == CardRarity.Legendary)
				{
					return "LegendaryMinion";
				}
				return "Minion";
			}
		}
		return "Normal";
	}

	private void OnMouseEnter()
	{
		base.SetWhiteRenderer(true);
		SoundManager.Instance.Play("DeckBuilder_Card_Hover", 0.1f);
	}

	private void OnMouseExit()
	{
		base.SetWhiteRenderer(false);
	}

	private void OnMouseUp()
	{
		if (MenuManager.Instance.AllMenusClosed())
		{
			DeckBuilder.Instance.AddSelectedCard(this.Card);
			this.UpdateSprites();
			this.UpdateNumbers();
		}
	}

	public BaseCard Card;

	private MeshRenderer CardRenderer;

	private MeshRenderer LimitRenderer;

	private NumberController CostController;

	private NumberController AttackController;

	private NumberController AttributeController;
}
