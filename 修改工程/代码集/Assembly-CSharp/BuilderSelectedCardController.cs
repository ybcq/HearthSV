using System;
using UnityEngine;

public class BuilderSelectedCardController : BaseController
{
	public static BuilderSelectedCardController Create(BaseCard card, SelectedCardType type)
	{
		GameObject gameObject = new GameObject(card.Name);
		gameObject.transform.ChangeParent(DeckBuilder.Instance.SelectionParent.transform);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(2.5f, 0.3f, 0.1f);
		boxCollider.isTrigger = true;
		BuilderSelectedCardController builderSelectedCardController = gameObject.AddComponent<BuilderSelectedCardController>();
		builderSelectedCardController.Card = card;
		builderSelectedCardController.Type = type;
		int size = (card.Name.Length > 20) ? 12 : 15;
		builderSelectedCardController.NameController = TextController.CreateBuilderText("Name", gameObject, new Vector3(-0.8f, 0.02f, 0f), TextAnchor.MiddleLeft, size, 3);
		builderSelectedCardController.CostController = TextController.CreateBuilderText("Cost", gameObject, new Vector3(-1.04f, 0.02f, 0f), TextAnchor.MiddleCenter, 30, 3);
		builderSelectedCardController.TokenRenderer = builderSelectedCardController.CreateSprite("Token_Sprite", Vector3.one, Vector3.zero, 2);
		builderSelectedCardController.CardRenderer = builderSelectedCardController.CreateSprite("Card_Sprite", new Vector3(0.45f, 0.5f, 1f), new Vector3(0.6f, 0f, 0f), 1);
		builderSelectedCardController.CardRenderer.enabled = true;
		builderSelectedCardController.TokenRenderer.enabled = true;
		builderSelectedCardController.UpdateSprites();
		builderSelectedCardController.UpdateNumbers();
		return builderSelectedCardController;
	}

	public override void DestroyController()
	{
		UnityEngine.Object.Destroy(this.CardRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		Texture2D texture = Resources.Load<Texture>("Sprites/" + this.Card.Class.GetEnumName() + "/Cards/" + this.Card.GetTypeName()) as Texture2D;
		Sprite sprite = Sprite.Create(texture, new Rect(140f, 350f, 260f, 50f), new Vector2(0.5f, 0.5f));
		this.CardRenderer.sprite = sprite;
		this.TokenRenderer.sprite = Resources.Load<Sprite>("Sprites/DeckBuilder/Card_" + this.Type.GetEnumName());
	}

	public override void UpdateNumbers()
	{
		this.NameController.UpdateText(this.Card.Name);
		this.CostController.UpdateText(this.Card.BaseCost.ToString());
	}

	private void OnMouseUp()
	{
		DeckBuilder.Instance.RemoveSelectedCard(this);
	}

	public BaseCard Card;

	private SpriteRenderer CardRenderer;

	private SpriteRenderer TokenRenderer;

	private TextController NameController;

	private TextController CostController;

	public SelectedCardType Type;
}
