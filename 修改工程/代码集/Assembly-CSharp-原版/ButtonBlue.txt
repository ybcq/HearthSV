using System;
using UnityEngine;

public class ButtonBlue : ButtonBase
{
	protected override void CreateTextController()
	{
		this.TextController = TextController.CreateBuilderText("Text_" + this.Text, base.gameObject, Vector3.zero, TextAnchor.MiddleCenter, 22, base.GetComponent<SpriteRenderer>().sortingOrder + 1);
	}
}
