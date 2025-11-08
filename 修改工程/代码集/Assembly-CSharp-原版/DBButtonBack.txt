using System;
using UnityEngine;

public class DBButtonBack : MonoBehaviour
{
	public void OnClick()
	{
		SavedDeck currentDeck = DeckBuilder.Instance.CurrentDeck;
		DeckBuilder.Instance.UnloadDeck();
		MenuManager.Instance.DeckBuilderNameField.gameObject.SetActive(false);
		DeckBuilder.Instance.AnimateBuilderToSelector();
		DeckSelector.Instance.FocusDeck(currentDeck);
		DeckManager.Instance.SaveDecklist();
	}
}
