using System;
using UnityEngine;

public class DeckController : BaseController
{
	public static DeckController Create(GameObject parent, SavedDeck deck, Vector3 position)
	{
		GameObject gameObject = new GameObject("Deck_" + deck.Name);
		gameObject.transform.ChangeParentAt(parent.transform, position);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(2.4f, 1.15f, 0.1f);
		boxCollider.isTrigger = true;
		DeckController deckController = gameObject.AddComponent<DeckController>();
		deckController.Deck = deck;
		deckController.Collider = boxCollider;
		deckController.Initialize();
		return deckController;
	}

	public override void Initialize()
	{
		this.NameController = TextController.CreateBuilderText("Name_Controller", base.gameObject, new Vector3(-1f, -0.235f, 0f), TextAnchor.MiddleLeft, 20, 1015);
		this.BlueGlowRenderer = base.CreateSprite("BlueGlow_Sprite", new Vector3(0.67f, 0.87f, 0.1f), Vector3.zero, 1015);
		this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", new Vector3(0.67f, 0.87f, 0.1f), Vector3.zero, 1014);
		this.ShadeRenderer = base.CreateSprite("Shade_Sprite", Vector3.one, new Vector3(-0.26f, -0.25f, 0f), 1013);
		this.HeroRenderer = base.CreateSprite("Hero_Sprite", new Vector3(1.05f, 1.3f, 1f), Vector3.zero, 1012);
		this.TokenRenderer = base.CreateSprite("Token_Sprite", new Vector3(1f, 1.2f, 1f), Vector3.zero, 1011);
		this.BannerRenderer = base.CreateSprite("Banner_Sprite", Vector3.one, new Vector3(0f, -0.275f, 0f), 1010);
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		UnityEngine.Object.Destroy(this.BannerRenderer);
		UnityEngine.Object.Destroy(this.TokenRenderer);
		UnityEngine.Object.Destroy(this.HeroRenderer);
		UnityEngine.Object.Destroy(this.ShadeRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.BannerRenderer.sprite = ResourcesManager.Decks["Banner_" + this.Deck.Class.GetEnumName()];
		this.TokenRenderer.sprite = ResourcesManager.Decks["BannerHero_Token"];
		this.HeroRenderer.sprite = ResourcesManager.Decks["BannerHero_" + this.Deck.Class.GetEnumName()];
		this.ShadeRenderer.sprite = ResourcesManager.Decks["BannerShade"];
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows["Banner_WhiteGlow"];
		this.BlueGlowRenderer.sprite = ResourcesManager.Glows["Banner_BlueGlow"];
		this.BannerRenderer.enabled = true;
		this.TokenRenderer.enabled = true;
		this.HeroRenderer.enabled = true;
		this.ShadeRenderer.enabled = true;
	}

	public override void UpdateNumbers()
	{
		this.NameController.UpdateText(this.Deck.Name);
	}

	public void Focus()
	{
		this.BlueGlowRenderer.enabled = true;
	}

	public void UnFocus()
	{
		this.BlueGlowRenderer.enabled = false;
	}

	private void OnMouseDown()
	{
		if (MenuManager.Instance.AllMenusClosed() && DeckSelector.Instance.FocusedDeck != this)
		{
			DeckSelector.Instance.FocusDeck(this);
			SoundManager.Instance.Play("Menu_Button_Click", 0.1f);
		}
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

	public SavedDeck Deck;

	private SpriteRenderer BannerRenderer;

	private SpriteRenderer TokenRenderer;

	private SpriteRenderer HeroRenderer;

	private SpriteRenderer ShadeRenderer;

	private SpriteRenderer BlueGlowRenderer;

	private TextController NameController;
}
