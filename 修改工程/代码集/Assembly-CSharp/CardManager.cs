using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	private CardManager()
	{
	}

	public static CardManager Instance
	{
		get
		{
			return CardManager._instance;
		}
	}

	private void Awake()
	{
		if (CardManager._instance == null)
		{
			CardManager._instance = this;
			Dictionary<HeroClass, List<BaseCard>> dictionary = new Dictionary<HeroClass, List<BaseCard>>();
			Dictionary<HeroClass, List<BaseCard>> dictionary2 = new Dictionary<HeroClass, List<BaseCard>>();
			IEnumerator enumerator = Enum.GetValues(typeof(HeroClass)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					HeroClass key = (HeroClass)obj;
					dictionary.Add(key, new List<BaseCard>());
					dictionary2.Add(key, new List<BaseCard>());
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			IEnumerable<Type> enumerable = Util.FindDerivedTypesOf(typeof(BaseCard));
			foreach (Type type in enumerable)
			{
				if (!type.IsAbstract)
				{
					BaseCard baseCard = Activator.CreateInstance(type) as BaseCard;
					if (baseCard.Collectible)
					{
						this.AllCards.Add(baseCard);
						dictionary[baseCard.Class].Add(baseCard);
					}
					else
					{
						this.UncollectibleCards.Add(baseCard);
						dictionary2[baseCard.Class].Add(baseCard);
					}
				}
			}
			foreach (KeyValuePair<HeroClass, List<BaseCard>> keyValuePair in dictionary)
			{
				this.ClassCards.Add(keyValuePair.Key, (from x in keyValuePair.Value
				orderby x.BaseCost
				select x).ToList<BaseCard>());
			}
			foreach (KeyValuePair<HeroClass, List<BaseCard>> keyValuePair2 in dictionary2)
			{
				this.UncollectibleClassCards.Add(keyValuePair2.Key, (from x in keyValuePair2.Value
				orderby x.BaseCost
				select x).ToList<BaseCard>());
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void DoRecount()
	{
		string text = string.Empty;
		text += " ----- Card Recount -----\n";
		text = text + "\n Total Cards : " + (this.AllCards.Count + this.UncollectibleCards.Count);
		text = text + "\n Collectible : " + this.AllCards.Count;
		text = text + "\n Uncollectible : " + this.UncollectibleCards.Count;
		text += "\n\n ------------------------ \n\n";
		foreach (KeyValuePair<HeroClass, List<BaseCard>> keyValuePair in this.ClassCards)
		{
			if (keyValuePair.Value.Count > 0)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					keyValuePair.Key.GetEnumName(),
					" : ",
					keyValuePair.Value.Count,
					" (",
					this.UncollectibleClassCards[keyValuePair.Key].Count,
					" uncollectible) \n"
				});
			}
		}
		MonoBehaviour.print(text + "\n ------------------------ \n\n");
	}

	private void DoWebJSON()
	{
		string text = "{";
		text += "\"Neutral\": [";
		text = this.AddClassCards(text, HeroClass.Neutral);
		text += "],";
		text += "\"DeathKnight\": [";
		text = this.AddClassCards(text, HeroClass.DeathKnight);
		text += "],";
		text += "\"Monk\": [";
		text = this.AddClassCards(text, HeroClass.Monk);
		text += "],";
		text += "\"DemonHunter\": [";
		text = this.AddClassCards(text, HeroClass.DemonHunter);
		text += "]";
		text += "}";
		File.WriteAllText(Application.persistentDataPath + "/search.json", text);
	}

	private string AddClassCards(string json, HeroClass heroClass)
	{
		foreach (BaseCard baseCard in this.ClassCards[heroClass])
		{
			string text = json;
			json = string.Concat(new object[]
			{
				text,
				"{ \"Name\": \"",
				baseCard.Name,
				"\", \"Description\": \"",
				baseCard.Description.Replace("\"", "\\\""),
				"\", \"Type\": \"",
				baseCard.GetType().BaseType.Name.Replace("Card", string.Empty),
				"\", \"Class\": \"",
				baseCard.Class.GetEnumName(),
				"\", \"Rarity\": \"",
				baseCard.Rarity.GetEnumName(),
				"\", \"Image\": \"\", \"Cost\": ",
				baseCard.BaseCost,
				", "
			});
			if (baseCard is MinionCard)
			{
				MinionCard minionCard = (MinionCard)baseCard;
				text = json;
				json = string.Concat(new object[]
				{
					text,
					"\"Attack\": \"",
					minionCard.BaseAttack,
					"\", \"Health\": \"",
					minionCard.BaseHealth,
					"\", \"Race\": \"",
					minionCard.MinionType.GetEnumName(),
					"\", \"Mechanics\": ["
				});
				if (minionCard.HasCharge)
				{
					json += "\"Charge\", ";
				}
				if (minionCard.HasCleave)
				{
					json += "\"Cleave\", ";
				}
				if (minionCard.HasDivineShield)
				{
					json += "\"Divine Shield\", ";
				}
				if (minionCard.IsEvasive)
				{
					json += "\"Evasion\", ";
				}
				if (minionCard.HasFreeze)
				{
					json += "\"Freeze\", ";
				}
				if (minionCard.IsImmune)
				{
					json += "\"Immune\", ";
				}
				if (minionCard.IsInaccurate)
				{
					json += "\"Inaccurate\", ";
				}
				if (minionCard.HasPoison)
				{
					json += "\"Poison\", ";
				}
				if (minionCard.HasSpellshield)
				{
					json += "\"Spellshield\", ";
				}
				if (minionCard.IsStealth)
				{
					json += "\"Stealth\", ";
				}
				if (minionCard.HasTaunt)
				{
					json += "\"Taunt\", ";
				}
				if (minionCard.HasWindfury)
				{
					json += "\"Windfury\", ";
				}
				if (minionCard.Mechanics.HasBattlecry())
				{
					json += "\"Battlecry\", ";
				}
				if (minionCard.Mechanics.HasDeathrattle())
				{
					json += "\"Deathrattle\", ";
				}
				if (minionCard.Mechanics.HasEnrage())
				{
					json += "\"Enrage\", ";
				}
				if (minionCard.Mechanics.HasInspire())
				{
					json += "\"Inspire\", ";
				}
				if (minionCard.Mechanics.HasMeditate())
				{
					json += "\"Meditate\", ";
				}
				if (minionCard.CardAura != null || minionCard.MinionAura != null || minionCard.HeroPowerAura != null || minionCard.HeroAura != null)
				{
					json += "\"Aura\", ";
				}
			}
			else if (baseCard is WeaponCard)
			{
				WeaponCard weaponCard = (WeaponCard)baseCard;
				text = json;
				json = string.Concat(new object[]
				{
					text,
					"\"Attack\": \"",
					weaponCard.BaseAttack,
					"\", \"Durability\": \"",
					weaponCard.BaseDurability,
					"\", \"Mechanics\": ["
				});
				if (weaponCard.IsInaccurate)
				{
					json += "\"Inaccurate\", ";
				}
				if (weaponCard.HasWindfury)
				{
					json += "\"Windfury\", ";
				}
				if (weaponCard.Mechanics.HasBattlecry())
				{
					json += "\"Battlecry\", ";
				}
				if (weaponCard.Mechanics.HasDeathrattle())
				{
					json += "\"Deathrattle\", ";
				}
				if (weaponCard.CardAura != null || weaponCard.MinionAura != null || weaponCard.HeroPowerAura != null || weaponCard.HeroAura != null)
				{
					json += "\"Aura\", ";
				}
			}
			else
			{
				json += "\"Mechanics\": [";
			}
			if (baseCard.HasHeld)
			{
				json += "\"Held\", ";
			}
			if (baseCard.Combo)
			{
				json += "\"Combo\", ";
			}
			if (baseCard.Overload > 0)
			{
				json += "\"Overload\", ";
			}
			if (json.EndsWith(", "))
			{
				json = json.Substring(0, json.Length - 2);
			}
			json += "] },";
		}
		json = json.Substring(0, json.Length - 1);
		return json;
	}

	private static CardManager _instance;

	public List<BaseCard> AllCards = new List<BaseCard>();

	public List<BaseCard> UncollectibleCards = new List<BaseCard>();

	public Dictionary<HeroClass, List<BaseCard>> ClassCards = new Dictionary<HeroClass, List<BaseCard>>();

	public Dictionary<HeroClass, List<BaseCard>> UncollectibleClassCards = new Dictionary<HeroClass, List<BaseCard>>();
}
