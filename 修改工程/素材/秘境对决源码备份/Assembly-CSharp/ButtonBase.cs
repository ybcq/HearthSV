using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class ButtonBase : MonoBehaviour
{
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.CreateTextController();
		this.TextController.UpdateText(this.Text);
		this.prevEnableState = this.Enabled;
		this.OnEnabledChanged();
	}

	protected abstract void CreateTextController();

	private void Update()
	{
		if (this.prevEnableState != this.Enabled)
		{
			this.prevEnableState = this.Enabled;
			this.OnEnabledChanged();
		}
	}

	private void OnEnabledChanged()
	{
		if (this.Enabled)
		{
			this.spriteRenderer.color = new Color(1f, 1f, 1f);
		}
		else
		{
			this.spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f);
		}
	}

	private void OnMouseOver()
	{
		if (this.Enabled)
		{
			this.spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f);
		}
	}

	private void OnMouseExit()
	{
		if (this.Enabled)
		{
			this.spriteRenderer.color = new Color(1f, 1f, 1f);
		}
	}

	private void OnMouseDown()
	{
		if (this.Enabled && this.OnClick != null)
		{
			this.OnClick.Invoke();
		}
	}

	public string Text;

	public bool Enabled;

	public UnityEvent OnClick;

	private bool prevEnableState;

	private SpriteRenderer spriteRenderer;

	protected TextController TextController;
}
