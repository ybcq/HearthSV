using System;
using UnityEngine;

public class ButtonCircle : ButtonBase
{
	protected override void CreateTextController()
	{
		this.TextController = TextController.CreateBuilderText("Text_" + this.Text, base.gameObject, Vector3.zero, TextAnchor.MiddleCenter, 50, base.GetComponent<SpriteRenderer>().sortingOrder + 1);
	}
}
