using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelector : MonoBehaviour
{
	private DeckSelector()
	{
	}

	public static DeckSelector Instance
	{
		get
		{
			return DeckSelector._instance;
		}
	}

	public void ShowDecks()
	{
		this.Controllers.ForEach(delegate(DeckController c)
		{
			c.DestroyController();
		});
		this.Controllers.Clear();
		for (int i = 0; i < DeckManager.Instance.SavedDecks.Count; i++)
		{
			SavedDeck deck = DeckManager.Instance.SavedDecks[i];
			int num = i % 3 - 1;
			int num2 = i / 3 - 1;
			Vector3 position = new Vector3((float)num * 2.4f, (float)(-(float)num2) * 2.4f, 0f);
			DeckController item = DeckController.Create(this.DeckParent, deck, position);
			this.Controllers.Add(item);
		}
		this.ButtonCreate.Enabled = (this.Controllers.Count != 9);
	}

	public void FocusDeck(SavedDeck deck)
	{
		foreach (DeckController deckController in this.Controllers)
		{
			if (deckController.Deck == deck)
			{
				this.FocusDeck(deckController);
				break;
			}
		}
	}

	public void FocusDeck(DeckController focusedController)
	{
		this.FocusedDeck = focusedController;
		foreach (DeckController deckController in this.Controllers)
		{
			if (deckController != focusedController)
			{
				deckController.UnFocus();
			}
			else
			{
				focusedController.Focus();
			}
		}
		HeroClass @class = focusedController.Deck.Class;
		string enumName = @class.GetEnumName();
		this.HeroRenderer.sprite = Resources.Load<Sprite>(string.Concat(new string[]
		{
			"Sprites/Heroes/",
			enumName,
			"/",
			ClassManager.Heroes[@class].Name,
			"_Portrait_Menu"
		}));
		this.HeroPowerRenderer.sprite = Resources.Load<Sprite>(string.Concat(new string[]
		{
			"Sprites/HeroPowers/",
			enumName,
			"/",
			ClassManager.HeroPowers[@class].Name,
			"_Token"
		}));
		this.DeckNameController.UpdateText(focusedController.Deck.Name);
		this.ButtonEdit.Enabled = true;
		this.ButtonDelete.Enabled = true;
	}

	public void RemoveFocus()
	{
		this.FocusedDeck = null;
		this.HeroRenderer.sprite = Resources.Load<Sprite>("Sprites/None/Hero/None_Portrait_Menu");
		this.HeroPowerRenderer.sprite = Resources.Load<Sprite>("Sprites/None/HeroPower/Token");
		this.DeckNameController.UpdateText("None");
		this.ButtonEdit.Enabled = false;
		this.ButtonDelete.Enabled = false;
	}

	private void Awake()
	{
		DeckSelector._instance = this;
		this.DeckNameController = TextController.CreateBuilderText("DeckName", DeckBuilder.Instance.Selector.gameObject, new Vector3(4.5f, 3.15f, 0f), TextAnchor.MiddleCenter, 20, 1005);
		this.ShowDecks();
		if (this.Controllers.Count > 0)
		{
			this.FocusDeck(this.Controllers[0]);
		}
		else
		{
			this.RemoveFocus();
		}
	}

	private static DeckSelector _instance;

	public GameObject DeckParent;

	public SpriteRenderer HeroRenderer;

	public SpriteRenderer HeroPowerRenderer;

	public ButtonBase ButtonCreate;

	public ButtonBase ButtonDelete;

	public ButtonBase ButtonEdit;

	public DeckController FocusedDeck;

	private TextController DeckNameController;

	private List<DeckController> Controllers = new List<DeckController>();

	private const float DISTANCE = 2.4f;
}
