using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
	public static NumberController Create(string name, GameObject parent, Vector3 position, int order, float size)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.ChangeParentAt(parent.transform, position);
		NumberController numberController = gameObject.AddComponent<NumberController>();
		numberController.MainPosition = position;
		numberController.Order = order;
		numberController.Size = size;
		return numberController;
	}

	public void UpdateNumber(int wholeNumber, string color)
	{
		this.DestroyRenderers();
		char[] array = wholeNumber.ToString().ToCharArray();
		int i = 0;
		while (i < array.Length)
		{
			string text = array[i].ToString();
			Vector3 position = new Vector3((float)i * (this.Size + 0.05f), 0f, 0f);
			if (text == null)
			{
				goto IL_9B;
			}
			if (!(text == "-") && !(text == "+"))
			{
				goto IL_9B;
			}
			SpriteRenderer item = this.CreateNumberRendererAt(text, color, position);
			this.Renderers.Add(item);
			IL_D2:
			i++;
			continue;
			IL_9B:
			int number = int.Parse(array[i].ToString());
			SpriteRenderer item2 = this.CreateNumberRendererAt(number, color, position);
			this.Renderers.Add(item2);
			goto IL_D2;
		}
		base.transform.localPosition = this.MainPosition - new Vector3((float)(array.Length - 1) * (this.Size + 0.05f) / 2f, 0f, 0f);
	}

	public void Remove()
	{
		this.DestroyRenderers();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void DestroyRenderers()
	{
		foreach (SpriteRenderer spriteRenderer in this.Renderers)
		{
			UnityEngine.Object.Destroy(spriteRenderer.gameObject);
		}
		this.Renderers.Clear();
	}

	public void SetEnabled(bool status)
	{
		foreach (SpriteRenderer spriteRenderer in this.Renderers)
		{
			spriteRenderer.enabled = status;
		}
	}

	public void SetRenderingOrder(int order)
	{
		this.Order = order;
		foreach (SpriteRenderer spriteRenderer in this.Renderers)
		{
			spriteRenderer.sortingOrder = order;
		}
	}

	private SpriteRenderer CreateNumberRendererAt(int number, string color, Vector3 position)
	{
		GameObject gameObject = new GameObject("NumberRenderer_" + number);
		gameObject.transform.ChangeParentAt(base.transform, position);
		gameObject.transform.localScale = Vector3.one * this.Size;
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = this.Order;
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = ResourcesManager.Numbers[color][number];
		return spriteRenderer;
	}

	private SpriteRenderer CreateNumberRendererAt(string symbol, string color, Vector3 position)
	{
		GameObject gameObject = new GameObject("NumberRenderer_" + symbol);
		gameObject.transform.ChangeParentAt(base.transform, position);
		gameObject.transform.localScale = Vector3.one * this.Size;
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = this.Order;
		spriteRenderer.enabled = true;
		if (symbol != null)
		{
			if (!(symbol == "-"))
			{
				if (symbol == "+")
				{
					spriteRenderer.sprite = ResourcesManager.Numbers[color][11];
				}
			}
			else
			{
				spriteRenderer.sprite = ResourcesManager.Numbers[color][10];
			}
		}
		return spriteRenderer;
	}

	private Vector3 MainPosition;

	private int Order;

	private float Size;

	private List<SpriteRenderer> Renderers = new List<SpriteRenderer>();
}
