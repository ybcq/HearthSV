using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CardController : BaseController
{
	public static CardController Create(BaseCard card, bool isEnemy)
	{
		GameObject gameObject = new GameObject(card.Name);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(3.5f, 5.75f, 1f);
		boxCollider.isTrigger = true;
		Animator animator = gameObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/CardAnimator");
		GameObject gameObject2 = new GameObject("Root");
		gameObject2.transform.ChangeParent(gameObject.transform);
		CardController cardController = gameObject.AddComponent<CardController>();
		cardController.Card = card;
		cardController.Collider = boxCollider;
		cardController.GlowType = cardController.GetGlowType();
		cardController.Animator = animator;
		cardController.IsEnemy = isEnemy;
		cardController.Child = gameObject2;
		cardController.Initialize();
		return cardController;
	}

	public override void Initialize()
	{
		if (this.IsEnemy)
		{
			this.CardBackRenderer = base.CreateChildMesh("CardBack_Mesh", ShaderMode.Normal, new Vector3(0f, 0f, 0.2f), new Vector3(0f, 180f, 0f), new Vector3(3.5f, 5f, 1f), 39);
			if (this.Card is MinionCard && this.Card.Rarity == CardRarity.Legendary)
			{
				this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Normal, new Vector3(0f, 0.075f, 0f), Vector3.zero, new Vector3(-4f, 5.75f, 1f), 42);
			}
			else
			{
				this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Normal, Vector3.zero, Vector3.zero, new Vector3(-4f, 5.5f, 1f), 42);
			}
			this.CardRenderer.enabled = false;
			this.CostController = NumberController.Create("Cost_Controller", this.Child, new Vector3(1.375f, 2.15f, -0.01f), 43, 0.45f);
			this.AttackController = NumberController.Create("Attack_Controller", this.Child, new Vector3(1.35f, -2.15f, -0.01f), 43, 0.45f);
			this.AttributeController = NumberController.Create("Attribute_Controller", this.Child, new Vector3(-1.5f, -2.15f, -0.01f), 43, 0.45f);
			this.CostController.transform.localScale = new Vector3(-1f, 1f, 1f);
			this.AttackController.transform.localScale = new Vector3(-1f, 1f, 1f);
			this.AttributeController.transform.localScale = new Vector3(-1f, 1f, 1f);
			this.CostController.enabled = false;
			this.AttackController.enabled = false;
			this.AttributeController.enabled = false;
		}
		else
		{
			this.CostController = NumberController.Create("Cost_Controller", this.Child, new Vector3(-1.375f, 2.15f, -0.01f), 43, 0.45f);
			this.AttackController = NumberController.Create("Attack_Controller", this.Child, new Vector3(-1.35f, -2.15f, -0.01f), 43, 0.45f);
			this.AttributeController = NumberController.Create("Attribute_Controller", this.Child, new Vector3(1.5f, -2.15f, -0.01f), 43, 0.45f);
			if (this.Card is MinionCard && this.Card.Rarity == CardRarity.Legendary)
			{
				this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Normal, new Vector3(0f, 0.075f, 0f), Vector3.zero, new Vector3(4f, 5.75f, 1f), 42);
			}
			else
			{
				this.CardRenderer = base.CreateChildMesh("Card_Mesh", ShaderMode.Normal, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 42);
			}
			this.CardBackRenderer = base.CreateChildMesh("CardBack_Mesh", ShaderMode.Culled, new Vector3(0f, 0f, 0.2f), new Vector3(0f, 180f, 0f), new Vector3(4f, 5.5f, 1f), 39);
			CardType cardType = this.Card.GetCardType();
			if (cardType != CardType.Weapon)
			{
				if (cardType != CardType.Spell)
				{
					if (cardType == CardType.Minion)
					{
						if (this.Card.Rarity == CardRarity.Legendary)
						{
							this.EchoRenderer = base.CreateChildSprite("Echo_Sprite", Vector3.one * 1.4f, new Vector3(0.15f, 0f, 0f), 42);
							this.ComboGlowRenderer = base.CreateChildSprite("ComboGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0.15f, 0.1f), 41);
							this.GreenGlowRenderer = base.CreateChildSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0.15f, 0.1f), 40);
						}
						else
						{
							this.EchoRenderer = base.CreateChildSprite("Echo_Sprite", Vector3.one * 1.4f, new Vector3(0.15f, 0f, 0f), 42);
							this.ComboGlowRenderer = base.CreateChildSprite("ComboGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0f, 0.1f), 41);
							this.GreenGlowRenderer = base.CreateChildSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.07f, 0f, 0.1f), 40);
						}
					}
				}
				else
				{
					this.EchoRenderer = base.CreateChildSprite("Echo_Sprite", Vector3.one * 1.4f, new Vector3(0.1f, 0f, 0f), 42);
					this.ComboGlowRenderer = base.CreateChildSprite("ComboGlow_Sprite", Vector3.one * 2.5f, new Vector3(0f, 0f, 0.1f), 41);
					this.GreenGlowRenderer = base.CreateChildSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0f, 0f, 0.1f), 40);
				}
			}
			else
			{
				this.EchoRenderer = base.CreateChildSprite("Echo_Sprite", Vector3.one * 1.4f, new Vector3(0.1f, 0f, 0f), 42);
				this.ComboGlowRenderer = base.CreateChildSprite("ComboGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.0375f, 0.025f, 0.1f), 41);
				this.GreenGlowRenderer = base.CreateChildSprite("GreenGlow_Sprite", Vector3.one * 2.5f, new Vector3(0.0375f, 0.025f, 0.1f), 40);
			}
		}
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		if (!this.IsEnemy)
		{
			this.AttackController.Remove();
			this.AttributeController.Remove();
			this.CostController.Remove();
			UnityEngine.Object.Destroy(this.CardRenderer);
			UnityEngine.Object.Destroy(this.GreenGlowRenderer);
			UnityEngine.Object.Destroy(this.ComboGlowRenderer);
		}
		UnityEngine.Object.Destroy(this.Animator);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		if (this.IsRevealed || !this.IsEnemy)
		{
			this.CardRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/" + this.Card.Class.GetEnumName() + "/Cards/" + this.Card.GetTypeName()));
		}
		if (!this.IsEnemy)
		{
			this.EchoRenderer.sprite = ResourcesManager.Glows["Card_" + this.GlowType + "_Echo"];
			this.GreenGlowRenderer.sprite = ResourcesManager.Glows["Card_" + this.GlowType + "_GreenGlow"];
		}
		this.CardBackRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/General/Card_Back_Classic"));
	}

	public override void UpdateNumbers()
	{
		if (!this.IsEnemy || this.IsRevealed)
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
	}

	public void SetRenderingOrder(int order)
	{
		if (!this.IsEnemy)
		{
			this.EchoRenderer.sortingOrder = order + 4;
			this.CostController.SetRenderingOrder(order + 3);
			this.AttackController.SetRenderingOrder(order + 3);
			this.AttributeController.SetRenderingOrder(order + 3);
			this.CardRenderer.sortingOrder = order + 2;
			this.ComboGlowRenderer.sortingOrder = order + 1;
			this.GreenGlowRenderer.sortingOrder = order;
		}
	}

	public void SetShadowCasting(bool shadowCast)
	{
		if (this.CardRenderer != null)
		{
			this.CardRenderer.shadowCastingMode = ((!shadowCast) ? ShadowCastingMode.Off : ShadowCastingMode.On);
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

	public void Reveal()
	{
		this.IsRevealed = true;
		this.CardRenderer.enabled = true;
		this.CostController.enabled = true;
		this.AttackController.enabled = true;
		this.AttributeController.enabled = true;
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	private void Update()
	{
		float maxDistanceDelta = this.Speed * Time.deltaTime;
		Vector3 target = new Vector3(this.TargetPosition.x, 16f, -1f);
		ControllerStatus status = this.Status;
		if (status != ControllerStatus.Inactive)
		{
			if (status != ControllerStatus.Dragging)
			{
				if (status == ControllerStatus.Targeting)
				{
					this.Child.transform.ChangeParent(base.transform);
					this.SetShadowCasting(true);
					this.SetRenderingOrder(500);
					base.transform.localScale = Vector3.one;
					base.transform.localEulerAngles = Vector3.zero;
					base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, target, maxDistanceDelta);
				}
			}
			else
			{
				this.Child.transform.ChangeParent(base.transform);
				this.SetShadowCasting(true);
				this.SetRenderingOrder(500);
				base.transform.localScale = Vector3.one;
				base.transform.localEulerAngles = Vector3.zero;
				base.transform.position = Util.GetWorldMousePosition();
				base.transform.localPosition += Vector3.back;
				if (this.Card.GetCardType() == CardType.Minion)
				{
					Vector3 worldMousePosition = Util.GetWorldMousePosition();
					if (this.Card.Player.BoardController.SelfBoardContainsPoint(worldMousePosition))
					{
						int boardPosition = this.Card.Player.BoardController.GetBoardPosition(worldMousePosition);
						this.Card.Player.BoardController.PreDropPosition = boardPosition;
						this.Card.Player.BoardController.UpdateBoard();
					}
				}
				else
				{
					this.Card.Player.BoardController.UpdateBoard();
				}
			}
		}
		else
		{
			this.SetShadowCasting(false);
			if (this.IsHovering && base.transform.localPosition.x == this.TargetPosition.x && !InterfaceManager.Instance.IsTargeting && !InterfaceManager.Instance.IsDragging)
			{
				this.StopAnimating();
				this.SetRenderingOrder(500);
				this.Child.transform.ChangeParentAt(base.transform.parent, new Vector3(base.transform.localPosition.x, 16f, 0f));
				this.Child.transform.localScale = Vector3.one * 2.15f;
			}
			else
			{
				this.Child.transform.ChangeParent(base.transform);
				this.SetRenderingOrder(this.TargetRenderingOrder);
				base.transform.localScale = Vector3.one * 0.85f;
				base.transform.localEulerAngles = this.TargetRotation;
				base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.TargetPosition, maxDistanceDelta);
			}
			this.Card.Player.BoardController.UpdateBoard();
		}
	}

	private void OnMouseEnter()
	{
		if (!this.IsEnemy && !GameManager.Instance.IsGameEnded)
		{
			this.IsHovering = true;
		}
	}

	private void OnMouseExit()
	{
		if (!this.IsEnemy && !GameManager.Instance.IsGameEnded)
		{
			this.IsHovering = false;
		}
	}

	private void OnMouseDown()
	{
		if (!this.IsEnemy && !GameManager.Instance.IsGameEnded && this.Status != ControllerStatus.Animating)
		{
			if (!this.Card.Player.IsCurrent())
			{
				this.Status = ControllerStatus.Dragging;
				InterfaceManager.Instance.IsDragging = true;
				return;
			}
			CardType cardType = this.Card.GetCardType();
			if (cardType != CardType.Minion)
			{
				if (cardType != CardType.Weapon)
				{
					if (cardType == CardType.Spell)
					{
						if (this.Card.As<SpellCard>().TargetType == TargetType.NoTarget || this.Card.Player.AvailableMana < this.Card.CurrentCost)
						{
							this.Status = ControllerStatus.Dragging;
							InterfaceManager.Instance.IsDragging = true;
						}
						else
						{
							this.Status = ControllerStatus.Targeting;
							InterfaceManager.Instance.EnableArrowAt(this, this.Child.transform.position - new Vector3(0f, 450f, 0f));
							InterfaceManager.Instance.EnlightenTargetsOf(this.Card.As<SpellCard>());
							InterfaceManager.Instance.CanTarget = new Func<Character, bool>(this.Card.As<SpellCard>().CanTarget);
						}
					}
				}
				else if (this.Card.BattlecryType == BattlecryType.NoTarget || this.Card.BattlecryType == BattlecryType.None)
				{
					this.Status = ControllerStatus.Dragging;
					InterfaceManager.Instance.IsDragging = true;
				}
				else if (this.Card.CanBattlecry())
				{
					this.Status = ControllerStatus.Targeting;
					InterfaceManager.Instance.EnableArrowAt(this, base.transform.position - new Vector3(0f, 450f, 0f));
					InterfaceManager.Instance.CanTarget = new Func<Character, bool>(this.Card.CanBattlecryTarget);
				}
				else
				{
					this.Status = ControllerStatus.Dragging;
					InterfaceManager.Instance.IsDragging = true;
				}
			}
			else
			{
				this.Status = ControllerStatus.Dragging;
				InterfaceManager.Instance.IsDragging = true;
			}
		}
	}

	private void OnMouseUp()
	{
		if (!this.IsEnemy && !GameManager.Instance.IsGameEnded && this.Status != ControllerStatus.Animating)
		{
			this.Status = ControllerStatus.Inactive;
			InterfaceManager.Instance.DisableArrow();
			InterfaceManager.Instance.DarkenAllTargets();
			InterfaceManager.Instance.IsDragging = false;
			this.Card.Player.BoardController.PreDropPosition = -1;
			if (this.Card.Player.IsCurrent() && GameManager.Instance.CanPlayCards && this.Card.Player.AvailableMana >= this.Card.CurrentCost)
			{
				Vector3 worldMousePosition = Util.GetWorldMousePosition();
				CardType cardType = this.Card.GetCardType();
				if (cardType != CardType.Minion)
				{
					if (cardType != CardType.Weapon)
					{
						if (cardType == CardType.Spell)
						{
							if (!this.Card.Player.CanPlaySpells)
							{
								return;
							}
							SpellCard spellCard = this.Card.As<SpellCard>();
							if (spellCard.CanCast())
							{
								if (spellCard.TargetType == TargetType.NoTarget)
								{
									if (spellCard.Player.BoardController.AllBoardContainsPoint(worldMousePosition))
									{
										this.Status = ControllerStatus.Animating;
										ActionQueue.Add(() => spellCard.PlayOn(null));
									}
								}
								else
								{
									Character target = Util.GetCharacterAtMouse();
									if (spellCard.CanTarget(target))
									{
										this.Status = ControllerStatus.Animating;
										ActionQueue.Add(() => spellCard.PlayOn(target));
									}
								}
							}
						}
					}
					else
					{
						if (!this.Card.Player.CanPlayWeapons)
						{
							return;
						}
						WeaponCard weaponCard = this.Card.As<WeaponCard>();
						if (weaponCard.BattlecryType == BattlecryType.NoTarget || weaponCard.BattlecryType == BattlecryType.None)
						{
							if (weaponCard.Player.BoardController.AllBoardContainsPoint(worldMousePosition))
							{
								this.Status = ControllerStatus.Animating;
								ActionQueue.Add(() => weaponCard.Play(null));
							}
						}
						else if (weaponCard.CanBattlecry())
						{
							Character target = Util.GetCharacterAtMouse();
							if (weaponCard.CanBattlecryTarget(target))
							{
								this.Status = ControllerStatus.Animating;
								ActionQueue.Add(() => weaponCard.Play(target));
							}
						}
						else if (weaponCard.Player.BoardController.AllBoardContainsPoint(worldMousePosition))
						{
							this.Status = ControllerStatus.Animating;
							ActionQueue.Add(() => weaponCard.Play(null));
						}
					}
				}
				else
				{
					if (!this.Card.Player.CanPlayMinions)
					{
						return;
					}
					MinionCard minionCard = this.Card.As<MinionCard>();
					if (minionCard.Player.BoardController.SelfBoardContainsPoint(worldMousePosition) && minionCard.Player.Minions.Count < 7)
					{
						this.Status = ControllerStatus.Animating;
						int boardPosition = minionCard.Player.BoardController.GetBoardPosition(worldMousePosition);
						ActionQueue.Add(() => this.PreDropAnimation(boardPosition));
						ActionQueue.AddVoid(delegate
						{
							minionCard.Minion = minionCard.Player.AddMinionToBoard(minionCard, boardPosition);
						});
						ActionQueue.AddVoid(delegate
						{
							minionCard.Player.RemoveCardFromHand(minionCard);
						});
						if (minionCard.BattlecryType == BattlecryType.None || minionCard.BattlecryType == BattlecryType.NoTarget)
						{
							ActionQueue.Add(() => minionCard.PlayOn(null));
						}
						else if (minionCard.CanBattlecry())
						{
							ActionQueue.AddVoid(delegate
							{
								InterfaceManager.Instance.ListenToTarget(minionCard.Minion, new Func<Character, IEnumerator>(minionCard.PlayOn), new Func<Character, bool>(minionCard.CanBattlecryTarget), new Action<Minion>(minionCard.Player.RemoveMinionFromBoard));
							});
						}
						else
						{
							ActionQueue.Add(() => minionCard.PlayOn(null));
						}
					}
				}
			}
		}
	}

	public void StartAnimating()
	{
		this.Status = ControllerStatus.Animating;
		this.Animator.enabled = true;
	}

	public void StopAnimating()
	{
		this.Animator.enabled = false;
		this.Status = ControllerStatus.Inactive;
	}

	public void AnimateDraw()
	{
		base.StartCoroutine(this.DrawAnimation());
	}

	public IEnumerator DrawAnimation()
	{
		this.StartAnimating();
		if (this.Card.Player.IsEnemy)
		{
			SoundManager.Instance.Play("Game_Draw_Card");
			this.Animator.SetTrigger("DrawEnemy");
			yield return new WaitForSeconds(0.35f);
		}
		else
		{
			this.Animator.SetTrigger("DrawSelf");
			SoundManager.Instance.Play("Game_Draw_Card_Hand");
			yield return new WaitForSeconds(1.85f);
		}
		this.StopAnimating();
		yield break;
	}

	public void AnimateDrawDiscard()
	{
		base.StartCoroutine(this.DrawDiscardAnimation());
	}

	public IEnumerator DrawDiscardAnimation()
	{
		this.StartAnimating();
		this.Animator.SetTrigger("DrawSelf");
		yield return new WaitForSeconds(2f);
		this.Animator.enabled = false;
		for (float i = 2.5f; i > 0f; i -= 0.1f)
		{
			base.transform.localScale = Vector3.one * i;
			yield return null;
		}
		this.DestroyController();
		yield break;
	}

	public IEnumerator PreDropAnimation(int position)
	{
		this.Status = ControllerStatus.Animating;
		this.EchoRenderer.enabled = true;
		this.GreenGlowRenderer.enabled = false;
		base.transform.SetParent(this.Card.Player.BoardController.transform);
		base.transform.localEulerAngles = Vector3.zero;
		Vector3 startPosition = base.transform.localPosition;
		Vector3 targetPosition = this.Card.Player.BoardController.GetTargetPosition(position) + new Vector3(-1.5f, 0f, -2f);
		this.Card.Player.BoardController.PreDropPosition = position;
		Vector3 startScale = base.transform.localScale;
		Vector3 targetScale = Vector3.one;
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < 1f)
		{
			float elapsed = Time.timeSinceLevelLoad - startTime;
			base.transform.localPosition = Util.InverseCubicLerp(startPosition, targetPosition, elapsed);
			base.transform.localScale = Util.InverseCubicLerp(startScale, targetScale, elapsed);
			yield return null;
		}
		base.transform.localPosition = targetPosition;
		this.Card.Player.BoardController.PreDropPosition = -1;
		yield break;
	}

	public BaseCard Card;

	public int TargetRenderingOrder;

	public Vector3 TargetPosition = Vector3.zero;

	public Vector3 TargetRotation = Vector3.zero;

	private MeshRenderer CardRenderer;

	private MeshRenderer CardBackRenderer;

	private SpriteRenderer ComboGlowRenderer;

	private SpriteRenderer EchoRenderer;

	private NumberController CostController;

	private NumberController AttackController;

	private NumberController AttributeController;

	private string GlowType;

	private bool IsEnemy;

	public bool IsRevealed;

	public float Speed = 100f;

	private ControllerStatus Status;

	private bool IsHovering;

	private Animator Animator;
}
