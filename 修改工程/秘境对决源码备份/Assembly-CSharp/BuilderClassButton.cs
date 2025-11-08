using System;
using UnityEngine;

public class BuilderClassButton : MonoBehaviour
{
	private void Awake()
	{
		this.Class = (HeroClass)Enum.Parse(typeof(HeroClass), base.name);
	}

	private void OnMouseUp()
	{
		if (this.Active && this.Class != DeckBuilder.Instance.CurrentPageClass)
		{
			DeckBuilder.Instance.EnlargeOn(this.Class);
			DeckBuilder.Instance.ShowPage(this.Class, 0);
			SoundManager.Instance.Play("DeckBuilder_ClassTab_Click", 0.1f);
		}
	}

	public void SetActive(bool active)
	{
		this.Active = active;
		this.ColorRenderer.enabled = active;
		this.GreyRenderer.enabled = !active;
	}

	public HeroClass Class;

	public SpriteRenderer ColorRenderer;

	public SpriteRenderer GreyRenderer;

	private bool Active;
}
