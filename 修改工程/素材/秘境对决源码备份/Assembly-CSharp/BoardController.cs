using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
	public static BoardController Create(Player player, Vector3 boardCenter, bool inverted)
	{
		GameObject gameObject = new GameObject("Board_Controller");
		gameObject.transform.ChangeParentAt(player.transform, boardCenter);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(27f, 5f, 0.1f);
		BoardController boardController = gameObject.AddComponent<BoardController>();
		boardController.Player = player;
		boardController.Collider = boxCollider;
		boardController.IsInverted = inverted;
		boardController.UpdateBoard();
		return boardController;
	}

	public void AddMinion(Minion minion, int position)
	{
		MinionController minionController = (MinionController)minion.Controller;
		if (position <= this.MinionControllers.Count)
		{
			this.MinionControllers.Insert(position, minionController);
		}
		else
		{
			this.MinionControllers.Add(minionController);
		}
		minionController.transform.localPosition = this.GetTargetPosition(position);
		this.UpdateBoard();
	}

	public void RemoveMinion(Minion minion)
	{
		this.MinionControllers.Remove(minion.Controller.As<MinionController>());
		this.UpdateBoard();
	}

	public void UpdateBoard()
	{
		if (this.MinionControllers.Count > 0)
		{
			for (int i = 0; i < this.MinionControllers.Count; i++)
			{
				this.MinionControllers[i].TargetPosition = this.GetTargetPosition(i);
				this.MinionControllers[i].BoardPosition = i;
			}
		}
	}

	public Vector3 GetTargetPosition(int index)
	{
		float num = ((float)this.MinionControllers.Count / 2f - 0.5f) * -3f;
		float num2 = (float)index * 3f + num;
		if (this.PreDropPosition != -1)
		{
			if (index < this.PreDropPosition)
			{
				num2 -= 1.5f;
			}
			else
			{
				num2 += 1.5f;
			}
		}
		return new Vector3(num2, 0f, 0f);
	}

	public int GetBoardPosition(Vector3 point)
	{
		float num = base.transform.InverseTransformPoint(point).x + 13.5f;
		if (this.MinionControllers.Count == 0)
		{
			return 0;
		}
		if (this.MinionControllers.Count == 1)
		{
			return Convert.ToInt32(num > 13.5f);
		}
		float num2 = (27f - 3f * (float)(this.MinionControllers.Count - 1)) / 2f;
		if (num > num2)
		{
			num -= num2;
			for (int i = 1; i < this.MinionControllers.Count; i++)
			{
				if (num < 3f)
				{
					return i;
				}
				num -= 3f;
			}
			return this.MinionControllers.Count;
		}
		return 0;
	}

	public int GetPositionOf(MinionController controller)
	{
		if (this.MinionControllers.Contains(controller))
		{
			return this.MinionControllers.FindIndex((MinionController c) => c == controller);
		}
		return -1;
	}

	public bool SelfBoardContainsPoint(Vector3 point)
	{
		Vector3 vector = base.transform.InverseTransformPoint(point);
		return vector.x < 13.5f && vector.x > -13.5f && vector.y < 12f && vector.y > -5f;
	}

	public bool AllBoardContainsPoint(Vector3 point)
	{
		Vector3 vector = base.transform.InverseTransformPoint(point);
		if (vector.x < 25f && vector.x > -25f)
		{
			if (this.IsInverted)
			{
				if (vector.y > -25f && vector.y < 2f)
				{
					return true;
				}
			}
			else if (vector.y < 25f && vector.y > -2f)
			{
				return true;
			}
		}
		return false;
	}

	public Player Player;

	public int PreDropPosition = -1;

	private List<MinionController> MinionControllers = new List<MinionController>();

	private BoxCollider Collider;

	private bool IsInverted;

	private const float DISTANCE = 3f;
}
