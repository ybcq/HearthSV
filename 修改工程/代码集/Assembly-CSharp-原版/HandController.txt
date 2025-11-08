using System;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
	public static HandController Create(Player player, Vector3 handPosition, bool inverted)
	{
		GameObject gameObject = new GameObject("Hand_Controller");
		gameObject.transform.ChangeParentAt(player.transform, handPosition);
		gameObject.transform.localScale = Vector3.one * 0.75f;
		if (inverted)
		{
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		}
		HandController handController = gameObject.AddComponent<HandController>();
		handController.Player = player;
		return handController;
	}

	public void SetAsParentOf(CardController cardController)
	{
		cardController.transform.parent = base.transform;
		cardController.transform.Reset();
	}

	public void Add(CardController cardController)
	{
		this.Controllers.Add(cardController);
		this.SetAsParentOf(cardController);
		this.MoveCards();
	}

	public void Remove(CardController cardController)
	{
		if (this.Controllers.Contains(cardController))
		{
			this.Controllers.Remove(cardController);
			cardController.DestroyController();
		}
		this.MoveCards();
	}

	private void MoveCards()
	{
		if (this.Controllers.Count > 0)
		{
			float num = 20f - (float)this.Controllers.Count * 1.5f;
			float num2 = num * (float)(this.Controllers.Count - 1) / 2f;
			for (int i = 0; i < this.Controllers.Count; i++)
			{
				CardController cardController = this.Controllers[i];
				float num3 = num * (float)i - num2;
				float f = num3 * 0.0174532924f;
				float x = Mathf.Sin(f) * 11f;
				float y = Mathf.Cos(f) * 11f;
				cardController.TargetPosition = new Vector3(x, y, -0.01f * (float)i);
				cardController.TargetRotation = new Vector3(0f, 0f, -num3);
				cardController.TargetRenderingOrder = 200 + 10 * i;
			}
		}
	}

	public Player Player;

	public List<CardController> Controllers = new List<CardController>();

	private const float DISTANCE = 11f;
}
