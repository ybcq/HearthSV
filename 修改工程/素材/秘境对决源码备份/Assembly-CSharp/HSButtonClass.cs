using System;
using UnityEngine;

public class HSButtonClass : MonoBehaviour
{
	public void OnClick()
	{
		DeckBuilder.Instance.SetupFor(this.Class);
		DeckBuilder.Instance.AnimateHeroSelectorToBuilder();
	}

	public HeroClass Class;
}
