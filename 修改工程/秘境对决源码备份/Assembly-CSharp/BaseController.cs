using System;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
	public virtual void Initialize()
	{
	}

	public virtual void DestroyController()
	{
	}

	public virtual void UpdateSprites()
	{
	}

	public virtual void UpdateNumbers()
	{
	}

	protected SpriteRenderer CreateSprite(string name, Vector3 scale, Vector3 position, int order)
	{
		return this.CreateSprite(name, scale, position, order, base.transform);
	}

	protected SpriteRenderer CreateChildSprite(string name, Vector3 scale, Vector3 position, int order)
	{
		return this.CreateSprite(name, scale, position, order, this.Child.transform);
	}

	private SpriteRenderer CreateSprite(string name, Vector3 scale, Vector3 position, int order, Transform parent)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.ChangeParentAt(parent, position);
		gameObject.transform.localScale = scale;
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = order;
		spriteRenderer.enabled = false;
		if (name.Contains("Echo"))
		{
			spriteRenderer.material = Resources.Load<Material>("Materials/Echo");
		}
		else
		{
			spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
		}
		return spriteRenderer;
	}

	protected MeshRenderer CreateMesh(string name, ShaderMode shader, Vector3 position, Vector3 rotation, Vector3 scale, int order)
	{
		return this.CreateMesh(name, shader, position, rotation, scale, order, base.transform);
	}

	protected MeshRenderer CreateChildMesh(string name, ShaderMode shader, Vector3 position, Vector3 rotation, Vector3 scale, int order)
	{
		return this.CreateMesh(name, shader, position, rotation, scale, order, this.Child.transform);
	}

	private MeshRenderer CreateMesh(string name, ShaderMode shader, Vector3 position, Vector3 rotation, Vector3 scale, int order, Transform parent)
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		gameObject.name = name;
		gameObject.transform.ChangeParentAt(parent, position);
		gameObject.transform.localEulerAngles = rotation;
		gameObject.transform.localScale = scale;
		UnityEngine.Object.Destroy(gameObject.GetComponent<MeshCollider>());
		MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
		component.material = new Material(ResourcesManager.Shaders[shader]);
		component.material.renderQueue = 3000;
		component.sortingLayerName = "Game";
		component.sortingOrder = order;
		component.enabled = true;
		return component;
	}

	public void SetGreenRenderer(bool status)
	{
		if (this.GreenGlowRenderer != null)
		{
			this.GreenGlowRenderer.enabled = status;
		}
	}

	public void SetWhiteRenderer(bool status)
	{
		if (this.WhiteGlowRenderer != null)
		{
			this.WhiteGlowRenderer.enabled = status;
		}
	}

	public void SetRedRenderer(bool status)
	{
		if (this.RedGlowRenderer != null)
		{
			this.RedGlowRenderer.enabled = status;
		}
	}

	public SpriteRenderer GreenGlowRenderer;

	public SpriteRenderer WhiteGlowRenderer;

	public SpriteRenderer RedGlowRenderer;

	public GameObject Child;

	public Collider Collider;
}
