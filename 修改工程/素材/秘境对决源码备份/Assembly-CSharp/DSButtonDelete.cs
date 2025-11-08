using System;
using UnityEngine;

public class DSButtonDelete : MonoBehaviour
{
	public void OnClick()
	{
		if (DeckSelector.Instance.FocusedDeck != null)
		{
			MenuManager.Instance.DeckBuilderConfirmMenu.SetActive(true);
			SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
		}
	}
}
