using System;
using UnityEngine;

public class DSButtonCreate : MonoBehaviour
{
	public void OnClick()
	{
		if (DeckManager.Instance.SavedDecks.Count < 9)
		{
			DeckBuilder.Instance.AnimateSelectorToHeroSelector();
		}
		else
		{
			SoundManager.Instance.Play("DeckBuilder_Card_Invalid", 0.1f);
		}
	}
}
