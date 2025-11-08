using System;
using UnityEngine;

public class DSButtonEdit : MonoBehaviour
{
	public void OnClick()
	{
		SavedDeck deck = DeckSelector.Instance.FocusedDeck.Deck;
		if (deck != null)
		{
			DeckBuilder.Instance.SetupFor(deck);
			DeckBuilder.Instance.AnimateSelectorToBuilder();
		}
		else
		{
			SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
		}
	}
}
