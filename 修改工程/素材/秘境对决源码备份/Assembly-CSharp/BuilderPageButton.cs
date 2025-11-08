using System;
using UnityEngine;

public class BuilderPageButton : MonoBehaviour
{
	private void OnMouseUp()
	{
		HeroClass currentPageClass = DeckBuilder.Instance.CurrentPageClass;
		int currentPageNumber = DeckBuilder.Instance.CurrentPageNumber;
		PageButtonType buttonType = this.ButtonType;
		if (buttonType != PageButtonType.Previous)
		{
			if (buttonType == PageButtonType.Next)
			{
				int count = CardManager.Instance.ClassCards[currentPageClass].Count;
				int num = (currentPageNumber + 1) * 8;
				if (num < count)
				{
					DeckBuilder.Instance.ShowPage(currentPageClass, currentPageNumber + 1);
					SoundManager.Instance.Play("DeckBuilder_Page_Forward", 0.1f);
				}
				else if (currentPageClass != HeroClass.Neutral)
				{
					DeckBuilder.Instance.ShowPage(HeroClass.Neutral, 0);
					DeckBuilder.Instance.EnlargeOn(HeroClass.Neutral);
					SoundManager.Instance.Play("DeckBuilder_Page_Forward", 0.1f);
				}
			}
		}
		else if (currentPageNumber > 0)
		{
			DeckBuilder.Instance.ShowPage(currentPageClass, currentPageNumber - 1);
			SoundManager.Instance.Play("DeckBuilder_Page_Back", 0.1f);
		}
		else if (currentPageClass == HeroClass.Neutral)
		{
			HeroClass @class = DeckBuilder.Instance.CurrentDeck.Class;
			int count2 = CardManager.Instance.ClassCards[@class].Count;
			int page = (count2 - 1) / 8;
			DeckBuilder.Instance.ShowPage(@class, page);
			DeckBuilder.Instance.EnlargeOn(@class);
			SoundManager.Instance.Play("DeckBuilder_Page_Back", 0.1f);
		}
	}

	private void OnMouseEnter()
	{
		PageButtonType buttonType = this.ButtonType;
		if (buttonType != PageButtonType.Previous)
		{
			if (buttonType == PageButtonType.Next)
			{
				CursorManager.Instance.SetCursor(CursorStatus.Right);
			}
		}
		else
		{
			CursorManager.Instance.SetCursor(CursorStatus.Left);
		}
	}

	private void OnMouseExit()
	{
		CursorManager.Instance.SetCursor(CursorStatus.Normal);
	}

	public PageButtonType ButtonType;
}
