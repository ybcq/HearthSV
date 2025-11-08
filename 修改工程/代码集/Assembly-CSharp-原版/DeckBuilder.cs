using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
	private DeckBuilder()
	{
	}

	public static DeckBuilder Instance
	{
		get
		{
			return DeckBuilder._instance;
		}
	}

	public void ShowPage(HeroClass heroClass, int page)
	{
		this.RemovePage();
		this.TitleRenderer.sprite = Resources.Load<Sprite>("Sprites/DeckBuilder/Title_" + heroClass.GetEnumName());
		this.CurrentPage = new GameObject(heroClass.GetEnumName() + "_Page" + page);
		this.CurrentPage.transform.parent = this.PageParent.transform;
		this.CurrentPage.transform.localPosition = new Vector3(-5.4f, 0.1f, 1.75f);
		this.CurrentPage.transform.localEulerAngles = Vector3.right * 90f;
		int num = 8 * page;
		int count = CardManager.Instance.ClassCards[heroClass].Count;
		if (num < count)
		{
			List<BaseCard> list = CardManager.Instance.ClassCards[heroClass];
			int num2 = 0;
			while (num2 < 8 && num2 + num < count)
			{
				BaseCard card = list[num2 + num];
				Vector3 position = new Vector3((float)(num2 % 4), (float)(num2 / 4) * -1.6f, 0f) * 2.25f;
				BuilderCardController builderCardController = BuilderCardController.Create(card);
				builderCardController.transform.ChangeParentAt(this.CurrentPage.transform, position);
				builderCardController.transform.localScale = new Vector3(0.55f, 0.55f, 1f);
				this.CurrentCards[num2] = builderCardController;
				num2++;
			}
		}
		this.CurrentPageNumber = page;
		this.CurrentPageClass = heroClass;
		this.UpdateAll();
	}

	public void RemovePage()
	{
		if (this.CurrentPage != null)
		{
			UnityEngine.Object.Destroy(this.CurrentPage);
			for (int i = 0; i < 8; i++)
			{
				BuilderCardController x = this.CurrentCards[i];
				if (x != null)
				{
					this.CurrentCards[i].DestroyController();
					this.CurrentCards[i] = null;
				}
			}
		}
	}

	public void AddSelectedCard(BaseCard card)
	{
		if (this.GetCardCount() < 30)
		{
			if (!card.CanAddToDeck(this.CurrentDeck))
			{
				SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
				return;
			}
			bool flag = this.SelectedCards.Any((BuilderSelectedCardController x) => x.Card.Name == card.Name);
			if (card.Rarity == CardRarity.Legendary)
			{
				if (!flag)
				{
					BuilderSelectedCardController item = BuilderSelectedCardController.Create(card, SelectedCardType.Legendary);
					this.SelectedCards.Add(item);
					this.CurrentDeck.SavedCards.Add(new SavedCard(card.GetTypeName(), 1));
					SoundManager.Instance.Play("DeckBuilder_Card_Add", 0.1f);
				}
				else
				{
					SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
				}
			}
			else if (!flag)
			{
				BuilderSelectedCardController item2 = BuilderSelectedCardController.Create(card, SelectedCardType.Normal);
				this.SelectedCards.Add(item2);
				this.CurrentDeck.SavedCards.Add(new SavedCard(card.GetTypeName(), 1));
				SoundManager.Instance.Play("DeckBuilder_Card_Add", 0.1f);
			}
			else
			{
				BuilderSelectedCardController builderSelectedCardController = this.SelectedCards.First((BuilderSelectedCardController x) => x.Card.Name == card.Name);
				SelectedCardType type = builderSelectedCardController.Type;
				if (type != SelectedCardType.Normal)
				{
					if (type == SelectedCardType.Double)
					{
						SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
					}
				}
				else
				{
					this.CurrentDeck.SavedCards.Find((SavedCard c) => c.Name == card.GetTypeName()).Quantity = 2;
					builderSelectedCardController.Type = SelectedCardType.Double;
					SoundManager.Instance.Play("DeckBuilder_Card_Add", 0.1f);
				}
			}
			this.UpdateAll();
		}
	}

	private void AddController(SavedCard savedCard)
	{
		BaseCard baseCard = savedCard.ToGameCard();
		BuilderSelectedCardController item;
		if (baseCard.Rarity == CardRarity.Legendary)
		{
			item = BuilderSelectedCardController.Create(baseCard, SelectedCardType.Legendary);
		}
		else if (savedCard.Quantity == 1)
		{
			item = BuilderSelectedCardController.Create(baseCard, SelectedCardType.Normal);
		}
		else
		{
			item = BuilderSelectedCardController.Create(baseCard, SelectedCardType.Double);
		}
		this.SelectedCards.Add(item);
		this.UpdateOrder();
		this.UpdateCounter();
	}

	public void RemoveSelectedCard(BuilderSelectedCardController controller)
	{
		if (this.SelectedCards.Contains(controller))
		{
			SavedCard savedCard = this.CurrentDeck.SavedCards.Find((SavedCard c) => c.Name == controller.Card.GetTypeName());
			if (controller.Type == SelectedCardType.Double)
			{
				savedCard.Quantity = 1;
				controller.Type = SelectedCardType.Normal;
				controller.UpdateSprites();
			}
			else
			{
				this.CurrentDeck.SavedCards.Remove(savedCard);
				controller.DestroyController();
				this.SelectedCards.Remove(controller);
			}
		}
		SoundManager.Instance.Play("DeckBuilder_Card_Remove", 0.1f);
		this.UpdateAll();
	}

	public void CreateNewDeck(HeroClass heroClass)
	{
		this.CurrentDeck = new SavedDeck("New Deck", heroClass);
		MenuManager.Instance.DeckBuilderNameField.text = "New Deck";
	}

	public void UnloadDeck()
	{
		if (this.CurrentDeck != null)
		{
			this.CurrentDeck.Name = MenuManager.Instance.DeckBuilderNameField.text;
			DeckManager.Instance.SaveDecklist();
			this.CurrentDeck = null;
		}
	}

	private int GetCardCount()
	{
		int num = 0;
		foreach (BuilderSelectedCardController builderSelectedCardController in this.SelectedCards)
		{
			if (builderSelectedCardController.Type == SelectedCardType.Double)
			{
				num++;
			}
			num++;
		}
		return num;
	}

	public void UpdateAll()
	{
		foreach (BuilderCardController builderCardController in this.CurrentCards)
		{
			if (builderCardController != null)
			{
				builderCardController.UpdateSprites();
				builderCardController.UpdateNumbers();
			}
		}
		foreach (BuilderSelectedCardController builderSelectedCardController in this.SelectedCards)
		{
			builderSelectedCardController.UpdateSprites();
			builderSelectedCardController.UpdateNumbers();
		}
		this.UpdateOrder();
		this.UpdateCounter();
	}

	public void UpdateOrder()
	{
		this.SelectedCards = (from x in this.SelectedCards
		orderby x.Card.BaseCost
		select x).ToList<BuilderSelectedCardController>();
		for (int i = 0; i < this.SelectedCards.Count; i++)
		{
			this.SelectedCards[i].transform.localPosition = new Vector3(0f, -0.4f * (float)i, 0f);
		}
	}

	public void UpdateCounter()
	{
		int cardCount = this.GetCardCount();
		this.CardCounterController.UpdateText(cardCount + "/30");
	}

	public void SetupFor(HeroClass heroClass)
	{
		this.EnlargeOn(heroClass);
		this.FocusOn(heroClass);
		this.SelectedCards.ForEach(delegate(BuilderSelectedCardController c)
		{
			c.DestroyController();
		});
		this.SelectedCards.Clear();
		this.ShowPage(heroClass, 0);
		this.UpdateAll();
		MenuManager.Instance.DeckBuilderNameField.gameObject.SetActive(true);
		MenuManager.Instance.DeckBuilderNameField.text = "New Deck";
		this.CurrentDeck = new SavedDeck("New Deck", heroClass);
		DeckManager.Instance.AddDeck(this.CurrentDeck);
	}

	public void SetupFor(SavedDeck savedDeck)
	{
		this.EnlargeOn(savedDeck.Class);
		this.FocusOn(savedDeck.Class);
		this.ShowPage(savedDeck.Class, 0);
		this.SelectedCards.ForEach(delegate(BuilderSelectedCardController c)
		{
			c.DestroyController();
		});
		this.SelectedCards.Clear();
		MenuManager.Instance.DeckBuilderNameField.gameObject.SetActive(true);
		MenuManager.Instance.DeckBuilderNameField.text = savedDeck.Name;
		base.StartCoroutine("SetupCoroutine", savedDeck);
		this.CurrentDeck = savedDeck;
	}

	private IEnumerator SetupCoroutine(SavedDeck savedDeck)
	{
		foreach (SavedCard card in savedDeck.SavedCards)
		{
			this.AddController(card);
			yield return null;
		}
		this.UpdateAll();
		yield break;
	}

	public void EnlargeOn(HeroClass heroClass)
	{
		foreach (BuilderClassButton builderClassButton in UnityEngine.Object.FindObjectsOfType<BuilderClassButton>())
		{
			if (builderClassButton.Class == heroClass)
			{
				builderClassButton.transform.localScale = Vector3.one * 1.5f;
			}
			else
			{
				builderClassButton.transform.localScale = Vector3.one;
			}
		}
	}

	public void FocusOn(HeroClass heroClass)
	{
		foreach (BuilderClassButton builderClassButton in UnityEngine.Object.FindObjectsOfType<BuilderClassButton>())
		{
			bool active = builderClassButton.Class == heroClass || builderClassButton.Class == HeroClass.Neutral;
			builderClassButton.SetActive(active);
		}
	}

	private void Awake()
	{
		DeckBuilder._instance = this;
	}

	private void Start()
	{
		this.CardCounterController = TextController.CreateBuilderText("CardCounter", this.Scenery, new Vector3(4.6f, -4.575f, 0f), TextAnchor.MiddleCenter, 30, 150);
		this.UpdateCounter();
	}

	public void AnimateSelectorToBuilder()
	{
		if (!this.IsAnimating)
		{
			base.StartCoroutine(this.SelectorToBuilderAnimation());
		}
	}

	private IEnumerator SelectorToBuilderAnimation()
	{
		this.IsAnimating = true;
		this.SceneryAnimator.SetTrigger("DeckSelectorOut");
		SoundManager.Instance.Play("DeckBuilder_Scene_MoveUp", 0.1f);
		BuilderScrollerController.Instance.ResetScrollerPosition();
		yield return new WaitForSeconds(0.5f);
		this.IsAnimating = false;
		yield break;
	}

	public void AnimateSelectorToHeroSelector()
	{
		if (!this.IsAnimating)
		{
			base.StartCoroutine(this.SelectorToHeroSelectorAnimation());
		}
	}

	private IEnumerator SelectorToHeroSelectorAnimation()
	{
		this.IsAnimating = true;
		this.SceneryAnimator.SetTrigger("HeroSelectorIn");
		SoundManager.Instance.Play("DeckBuilder_Scene_MoveDown", 0.1f);
		yield return new WaitForSeconds(0.5f);
		this.IsAnimating = false;
		yield break;
	}

	public void AnimateHeroSelectorToBuilder()
	{
		if (!this.IsAnimating)
		{
			base.StartCoroutine(this.HeroSelectorToBuilderAnimation());
		}
	}

	private IEnumerator HeroSelectorToBuilderAnimation()
	{
		this.IsAnimating = true;
		this.SceneryAnimator.SetTrigger("HeroSelectorToBuilder");
		SoundManager.Instance.Play("DeckBuilder_Scene_MoveUp", 0.1f);
		BuilderScrollerController.Instance.ResetScrollerPosition();
		yield return new WaitForSeconds(0.5f);
		this.IsAnimating = false;
		yield break;
	}

	public void AnimateHeroSelectorToSelector()
	{
		if (!this.IsAnimating)
		{
			base.StartCoroutine(this.HeroSelectorToSelectorAnimation());
		}
	}

	private IEnumerator HeroSelectorToSelectorAnimation()
	{
		this.IsAnimating = true;
		this.SceneryAnimator.SetTrigger("HeroSelectorOut");
		SoundManager.Instance.Play("DeckBuilder_Scene_MoveUp", 0.1f);
		DeckSelector.Instance.ShowDecks();
		yield return new WaitForSeconds(0.5f);
		this.IsAnimating = false;
		yield break;
	}

	public void AnimateBuilderToSelector()
	{
		if (!this.IsAnimating)
		{
			base.StartCoroutine(this.BuilderToSelectorAnimation());
		}
	}

	private IEnumerator BuilderToSelectorAnimation()
	{
		this.IsAnimating = true;
		this.SceneryAnimator.SetTrigger("DeckSelectorIn");
		SoundManager.Instance.Play("DeckBuilder_Scene_MoveDown", 0.1f);
		DeckSelector.Instance.ShowDecks();
		yield return new WaitForSeconds(0.5f);
		this.IsAnimating = false;
		yield break;
	}

	private static DeckBuilder _instance;

	public GameObject Scenery;

	public GameObject Background;

	public GameObject Builder;

	public GameObject Selector;

	public GameObject HeroSelector;

	public TextController CardCounterController;

	public SpriteRenderer TitleRenderer;

	public GameObject PageParent;

	public GameObject CurrentPage;

	public BuilderCardController[] CurrentCards = new BuilderCardController[8];

	public HeroClass CurrentPageClass;

	public int CurrentPageNumber;

	public GameObject SelectionParent;

	public List<BuilderSelectedCardController> SelectedCards = new List<BuilderSelectedCardController>();

	public SavedDeck CurrentDeck;

	private const float BUILDERCARD_DISTANCE = 2.25f;

	private const float SELECTEDCARD_DISTANCE = -0.4f;

	public Animator SceneryAnimator;

	private bool IsAnimating;
}
