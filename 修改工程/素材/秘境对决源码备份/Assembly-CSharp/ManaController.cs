using System;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
	public static ManaController Create(Player player, Vector3 manaPosition, bool displayCrystals)
	{
		GameObject gameObject = new GameObject("Mana_Controller");
		gameObject.transform.ChangeParentAt(player.transform, manaPosition);
		gameObject.transform.localScale = Vector3.one * 0.75f;
		ManaController manaController = gameObject.AddComponent<ManaController>();
		manaController.Player = player;
		manaController.DisplayCrystals = displayCrystals;
		manaController.CountController = TextController.CreateGameText("Count", gameObject, Vector3.zero, TextAnchor.MiddleCenter, 100, 0);
		manaController.UpdateSprites();
		manaController.UpdateNumbers();
		return manaController;
	}

	public void DestroyController()
	{
		this.DestroyRenderers();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void DestroyRenderers()
	{
		foreach (SpriteRenderer obj in this.Crystals)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.Crystals.Clear();
	}

	public void UpdateSprites()
	{
		if (this.DisplayCrystals)
		{
			int num = 0;
			this.DestroyRenderers();
			int num2 = 0;
			while (num2 < this.Player.AvailableMana && num2 < 10)
			{
				SpriteRenderer item = this.CreateManaRenderer("Available", new Vector3((float)num + 2f, -0.1f, 0f));
				this.Crystals.Add(item);
				num++;
				num2++;
			}
			int num3 = 0;
			while (num3 < this.Player.TurnMana - this.Player.CurrentOverloadedMana - this.Player.AvailableMana && num3 < 10)
			{
				SpriteRenderer item2 = this.CreateManaRenderer("Used", new Vector3((float)num + 2f, -0.1f, 0f));
				this.Crystals.Add(item2);
				num++;
				num3++;
			}
			int num4 = 0;
			while (num4 < this.Player.CurrentOverloadedMana && num4 < 10)
			{
				SpriteRenderer item3 = this.CreateManaRenderer("Overloaded", new Vector3((float)num + 2f, -0.1f, 0f));
				this.Crystals.Add(item3);
				num++;
				num4++;
			}
		}
	}

	public void UpdateNumbers()
	{
		this.CountController.UpdateText(this.Player.AvailableMana + "/" + this.Player.TurnMana);
	}

	public void UpdateAll()
	{
		this.UpdateNumbers();
		this.UpdateSprites();
	}

	private SpriteRenderer CreateManaRenderer(string manaType, Vector3 position)
	{
		GameObject gameObject = new GameObject("ManaCrystal_" + manaType + "_Sprite");
		gameObject.transform.ChangeParentAt(base.transform, position);
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
		spriteRenderer.sprite = ResourcesManager.Crystals[manaType];
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = 0;
		spriteRenderer.enabled = true;
		return spriteRenderer;
	}

	public Player Player;

	private TextController CountController;

	private List<SpriteRenderer> Crystals = new List<SpriteRenderer>();

	private bool DisplayCrystals;
}
