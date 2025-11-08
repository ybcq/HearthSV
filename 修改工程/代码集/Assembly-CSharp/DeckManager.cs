using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
	private DeckManager()
	{
	}

	public static DeckManager Instance
	{
		get
		{
			return DeckManager._instance;
		}
	}

	private void Awake()
	{
		if (DeckManager._instance == null)
		{
			DeckManager._instance = this;
			this.DecklistPath = Application.persistentDataPath + "/decklist.json";
			if (File.Exists(this.DecklistPath))
			{
				string json = File.ReadAllText(this.DecklistPath);
				try
				{
					SavedDeck[] array = JsonHelper.FromJson<SavedDeck>(json);
					if (array != null)
					{
						this.SavedDecks = array.ToList<SavedDeck>();
						foreach (SavedDeck deck in this.SavedDecks)
						{
							this.ValidateDeck(deck);
						}
						this.SaveDecklist();
					}
					else
					{
						this.SavedDecks = new List<SavedDeck>();
					}
					return;
				}
				catch (Exception arg)
				{
					Debugger.Log("ERROR PARSING DECKLIST -> " + arg);
					this.SavedDecks = new List<SavedDeck>();
					File.Create(this.DecklistPath).Close();
					return;
				}
			}
			this.SavedDecks = new List<SavedDeck>();
			File.Create(this.DecklistPath).Close();
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public SavedDeck GetDeck(string name)
	{
		return this.SavedDecks.FirstOrDefault((SavedDeck d) => d.Name == name);
	}

	public void AddDeck(SavedDeck deck)
	{
		this.SavedDecks.Add(deck);
		this.SaveDecklist();
	}

	public void RemoveDeck(SavedDeck deck)
	{
		this.SavedDecks.Remove(deck);
		this.SaveDecklist();
	}

	public void SaveDecklist()
	{
		string value = JsonHelper.ToJson<SavedDeck>(this.SavedDecks.ToArray());
		StreamWriter streamWriter = new StreamWriter(File.Create(Application.persistentDataPath + "/decklist.json"));
		streamWriter.Write(value);
		streamWriter.Close();
	}

	private void ValidateDeck(SavedDeck deck)
	{
		foreach (SavedCard savedCard in deck.SavedCards.ToList<SavedCard>())
		{
			BaseCard baseCard = savedCard.ToGameCard();
			if (baseCard == null || !baseCard.Collectible)
			{
				deck.SavedCards.Remove(savedCard);
			}
		}
	}

	public SavedDeck GetRandomAIDeck()
	{
		return RNG.RandomItemFrom<SavedDeck>(this.AIDecks);
	}

	private static DeckManager _instance;

	public List<SavedDeck> SavedDecks;

	public List<SavedDeck> AIDecks = new List<SavedDeck>
	{
		new SavedDeck("AI DK Deck", HeroClass.DeathKnight)
		{
			SavedCards = new List<SavedCard>
			{
				new SavedCard("KoboldTunneler", 2),
				new SavedCard("HiredThug", 1),
				new SavedCard("ElwynnForestBear", 2),
				new SavedCard("AllWillServe", 2),
				new SavedCard("FrostmaneTroll", 2),
				new SavedCard("IllFatedSquire", 2),
				new SavedCard("Arcaneling", 2),
				new SavedCard("VengefulSpirit", 2),
				new SavedCard("RockjawBonepicker", 2),
				new SavedCard("BonyConstruct", 1),
				new SavedCard("BladeOfLostSouls", 1),
				new SavedCard("ScourgeNecromancer", 2),
				new SavedCard("ShatteringElemental", 2),
				new SavedCard("SuntouchedWarrior", 1),
				new SavedCard("CorpseExplosion", 1),
				new SavedCard("UnholyRuneblade", 2),
				new SavedCard("DeathGrip", 1),
				new SavedCard("TeronGorefiend", 1),
				new SavedCard("ArmyoftheDead", 1)
			}
		},
		new SavedDeck("AI Monk Deck", HeroClass.Monk)
		{
			SavedCards = new List<SavedCard>
			{
				new SavedCard("Ascension", 2),
				new SavedCard("Jab", 1),
				new SavedCard("GiftoftheSerpent", 2),
				new SavedCard("BrawlingStance", 1),
				new SavedCard("FrostmaneTroll", 1),
				new SavedCard("AspiringStudent", 2),
				new SavedCard("RockjawBonepicker", 1),
				new SavedCard("DrunkenBrewmaster", 2),
				new SavedCard("PatientMistweaver", 2),
				new SavedCard("SuntouchedWarrior", 1),
				new SavedCard("SpinningFireBlossom", 2),
				new SavedCard("ChiWave", 2),
				new SavedCard("LilisWaterDragon", 1),
				new SavedCard("SpinningCraneKick", 2),
				new SavedCard("DragonTurtle", 2),
				new SavedCard("Ozumat", 1),
				new SavedCard("ZenMaster", 2),
				new SavedCard("StormEarthandFire", 2),
				new SavedCard("TaranZhu", 1)
			}
		}
	};

	private string DecklistPath;
}
