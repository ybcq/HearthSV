using System;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
	public static TextController CreateBuilderText(string name, GameObject parent, Vector3 position, TextAnchor anchor, int size, int order)
	{
		GameObject gameObject = new GameObject(name + "_Controller");
		gameObject.transform.ChangeParentAt(parent.transform, position);
		gameObject.transform.localScale = Vector3.one * 0.1f;
		TextController textController = gameObject.AddComponent<TextController>();
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				GameObject gameObject2 = new GameObject("TextMesh_" + i);
				gameObject2.transform.ChangeParentAt(gameObject.transform, new Vector3((float)i * 0.1f, (float)j * 0.1f, 0f));
				TextMesh textMesh = gameObject2.AddComponent<TextMesh>();
				textMesh.alignment = TextAlignment.Center;
				textMesh.anchor = anchor;
				textMesh.font = ResourcesManager.Font;
				textMesh.fontSize = size;
				textMesh.color = Color.black;
				Renderer component = textMesh.GetComponent<Renderer>();
				component.material = ResourcesManager.FontMaterial;
				component.sortingLayerName = "Game";
				component.sortingOrder = order;
				textController.Meshes.Add(textMesh);
			}
		}
		textController.Meshes[4].color = Color.white;
		textController.Meshes[4].GetComponent<Renderer>().sortingOrder = order + 1;
		return textController;
	}

	public static TextController CreateGameText(string name, GameObject parent, Vector3 position, TextAnchor anchor, int size, int order)
	{
		GameObject gameObject = new GameObject(name + "_Controller");
		gameObject.transform.ChangeParentAt(parent.transform, position);
		gameObject.transform.localScale = Vector3.one * 0.1f;
		TextController textController = gameObject.AddComponent<TextController>();
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				GameObject gameObject2 = new GameObject("TextMesh_" + i);
				gameObject2.transform.ChangeParentAt(gameObject.transform, new Vector3((float)i * 0.35f, (float)j * 0.35f, 0f));
				TextMesh textMesh = gameObject2.AddComponent<TextMesh>();
				textMesh.alignment = TextAlignment.Center;
				textMesh.anchor = anchor;
				textMesh.font = ResourcesManager.Font;
				textMesh.fontSize = size;
				textMesh.color = Color.black;
				Renderer component = textMesh.GetComponent<Renderer>();
				component.material = ResourcesManager.FontMaterial;
				component.sortingLayerName = "Game";
				component.sortingOrder = order;
				textController.Meshes.Add(textMesh);
			}
		}
		textController.Meshes[4].color = Color.white;
		textController.Meshes[4].GetComponent<Renderer>().sortingOrder = order + 1;
		return textController;
	}

	public void UpdateText(string text)
	{
		foreach (TextMesh textMesh in this.Meshes)
		{
			textMesh.text = text;
		}
	}

	public void UpdateColor(Color color)
	{
		this.Meshes[4].color = color;
	}

	public void UpdateOrder(int order)
	{
		foreach (TextMesh textMesh in this.Meshes)
		{
			textMesh.GetComponent<Renderer>().sortingOrder = order;
		}
	}

	public void UpdateSize(int size)
	{
		foreach (TextMesh textMesh in this.Meshes)
		{
			textMesh.fontSize = size;
		}
	}

	public List<TextMesh> Meshes = new List<TextMesh>();

	private const float OUTLINE_DISTANCE = 0.1f;
}
