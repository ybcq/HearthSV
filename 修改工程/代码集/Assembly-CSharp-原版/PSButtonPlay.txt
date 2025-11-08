using System;
using UnityEngine;

public class PSButtonPlay : MonoBehaviour
{
	public void OnClick()
	{
		if (!PlaySelector.Instance.IsLoading)
		{
			SavedDeck deck = PlaySelector.Instance.FocusedDeck.Deck;
			if (deck != null)
			{
				PlaySelector.Instance.Play();
			}
			else
			{
				SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
			}
		}
	}
}
