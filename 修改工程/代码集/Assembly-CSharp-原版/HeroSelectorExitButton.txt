using System;
using UnityEngine;

public class HeroSelectorExitButton : MonoBehaviour
{
	private void Awake()
	{
		this.TextController = TextController.CreateBuilderText("ExitText", base.gameObject, new Vector3(0f, 0.02f, 0f), TextAnchor.MiddleCenter, 30, 155);
		this.TextController.transform.localScale = new Vector3(0.12f, 0.1f, 0.1f);
		this.TextController.UpdateText("Exit");
	}

	private void OnMouseDown()
	{
		DeckBuilder.Instance.AnimateHeroSelectorToSelector();
	}

	private TextController TextController;
}
