using System;
using UnityEngine;

public class ButtonBig : ButtonBase
{
	protected override void CreateTextController()
	{
		this.TextController = TextController.CreateBuilderText("Text_" + this.Text, base.gameObject, new Vector3(0f, 0.03f, 0f), TextAnchor.MiddleCenter, 30, base.GetComponent<SpriteRenderer>().sortingOrder + 1);
	}
}
