using System;
using UnityEngine;

public class SelectionCardController : BaseController
{
	public static SelectionCardController Create(BaseCard card, Vector3 targetPosition, int order)
	{
		GameObject gameObject = new GameObject(card.Name);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(3.5f, 5.75f, 1f);
		boxCollider.isTrigger = true;
		SelectionCardController selectionCardController = gameObject.AddComponent<SelectionCardController>();
		selectionCardController.Card = card;
		selectionCardController.Collider = boxCollider;
		selectionCardController.GlowType = selectionCardController.GetGlowType();
		selectionCardController.TargetPosition = targetPosition;
		selectionCardController.OrderOffset = order * 10;
		selectionCardController.Initialize();
		return selectionCardController;
	}

	public override void Initialize()
	{
		this.CostController = NumberController.Create("Cost_Controller", base.gameObject, new Vector3(-1.375f, 2.15f, -0.01f), 9999 + this.OrderOffset, 0.45f);
		this.AttackController = NumberController.Create("Attack_Controller", base.gameObject, new Vector3(-1.35f, -2.15f, -0.01f), 9999 + this.OrderOffset, 0.45f);
		this.AttributeController = NumberController.Create("Attribute_Controller", base.gameObject, new Vector3(1.5f, -2.15f, -0.01f), 9999 + this.OrderOffset, 0.45f);
		if (this.Card is MinionCard && this.Card.Rarity == CardRarity.Legendary)
		{
			this.CardRenderer = base.CreateMesh("Card_Mesh", ShaderMode.Normal, new Vector3(0f, 0.075f, 0f), Vector3.zero, new Vector3(4f, 5.75f, 1f), 9998 + this.OrderOffset);
		}
		else
		{
			this.CardRenderer = base.CreateMesh("Card_Mesh", ShaderMode.Normal, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 9998 + this.OrderOffset);
		}
		this.CrossRenderer = base.CreateSprite("Cross_Sprite", Vector3.one * 1.75f, new Vector3(-0.25f, 0f, 0f), 9999 + this.OrderOffset);
		this.CrossRenderer.transform.localEulerAngles = new Vector3(0f, 180f, 90f);
		CardType cardType = this.Card.GetCardType();
		if (cardType != CardType.Weapon)
		{
			if (cardType != CardType.Spell)
			{
				if (cardType == CardType.Minion)
				{
					if (this.Card.Rarity == CardRarity.Legendary)
					{
						this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0.15f, 0.1f), 9997 + this.OrderOffset);
						this.GreenGlowRenderer = base.CreateSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0.15f, 0.1f), 9996 + this.OrderOffset);
					}
					else
					{
						this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0f, 0.1f), 9997 + this.OrderOffset);
						this.GreenGlowRenderer = base.CreateSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0f, 0.1f), 9996 + this.OrderOffset);
					}
				}
			}
			else
			{
				this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 2.5f, new Vector3(0f, 0f, 0.1f), 9997 + this.OrderOffset);
				this.GreenGlowRenderer = base.CreateSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0f, 0f, 0.1f), 9996 + this.OrderOffset);
			}
		}
		else
		{
			this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.0375f, 0.025f, 0.1f), 9997 + this.OrderOffset);
			this.GreenGlowRenderer = base.CreateSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.0375f, 0.025f, 0.1f), 9996 + this.OrderOffset);
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
		UnityEngine.Object.Destroy(this.WhiteGlowRenderer);
		UnityEngine.Object.Destroy(this.GreenGlowRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.CardRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/" + this.Card.Class.GetEnumName() + "/Cards/" + this.Card.GetTypeName()));
		this.CrossRenderer.sprite = ResourcesManager.Decks["Cross"];
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows["Card_" + this.GlowType + "_WhiteGlow"];
		this.GreenGlowRenderer.sprite = ResourcesManager.Glows["Card_" + this.GlowType + "_GreenGlow"];
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
				return "Weapon";
			}
		}
		return "Normal";
	}

	public void ToggleCross()
	{
		this.CrossRenderer.enabled = !this.IsDiscarded;
		base.SetGreenRenderer(this.IsDiscarded);
		this.IsDiscarded = !this.IsDiscarded;
	}

	private void OnMouseEnter()
	{
		this.IsHovering = true;
		if (!InterfaceManager.Instance.IsAnimatingMulligan)
		{
			base.SetWhiteRenderer(true);
		}
	}

	private void OnMouseExit()
	{
		this.IsHovering = false;
		base.SetWhiteRenderer(false);
	}

	private void OnMouseUp()
	{
		if (this.IsHovering && !InterfaceManager.Instance.IsAnimatingMulligan)
		{
			base.StartCoroutine(InterfaceManager.Instance.PickSelection(this));
		}
	}

	public BaseCard Card;

	public Vector3 TargetPosition;

	public bool IsDiscarded;

	private MeshRenderer CardRenderer;

	private SpriteRenderer CrossRenderer;

	private NumberController CostController;

	private NumberController AttackController;

	private NumberController AttributeController;

	private string GlowType;

	private int OrderOffset;

	private bool IsHovering;
}
