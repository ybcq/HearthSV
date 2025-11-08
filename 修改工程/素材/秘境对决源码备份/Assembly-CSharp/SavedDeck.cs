using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedDeck
{
	public SavedDeck(string name, HeroClass heroClass)
	{
		this.Name = name;
		this.Class = heroClass;
	}

	public SavedDeck(string jsonString)
	{
		JsonUtility.FromJsonOverwrite(jsonString, this);
	}

	public List<BaseCard> ToGameDeck()
	{
		List<BaseCard> list = new List<BaseCard>();
		foreach (SavedCard savedCard in this.SavedCards)
		{
			for (int i = 0; i < savedCard.Quantity; i++)
			{
				list.Add(savedCard.ToGameCard());
			}
		}
		return list;
	}

	public string Name;

	public HeroClass Class;

	public List<SavedCard> SavedCards = new List<SavedCard>();
}
