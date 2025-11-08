using System;
using System.Collections;
using UnityEngine;

public class TurnButtonController : MonoBehaviour
{
	public void Enable()
	{
		this.IsEnabled = true;
		ActionQueue.Add(new Func<IEnumerator>(this.RotateUpAnimation));
	}

	private void Awake()
	{
		this.Animator = base.GetComponent<Animator>();
		this.ButtonMaterial = base.GetComponent<MeshRenderer>().material;
		base.transform.position = this.BasePosition;
	}

	private void OnMouseEnter()
	{
		this.IsHovering = true;
	}

	private void OnMouseExit()
	{
		this.IsHovering = false;
	}

	private void OnMouseDown()
	{
		if (this.IsEnabled)
		{
			this.Animator.SetTrigger("MoveDown");
			SoundManager.Instance.Play("Game_TurnButton_Down");
		}
	}

	private void OnMouseUp()
	{
		if (this.IsEnabled && !GameManager.Instance.IsMulliganing)
		{
			if (this.IsHovering)
			{
				if (!this.IsAnimating)
				{
					this.IsEnabled = false;
					this.AnimateRotateDown();
					ActionQueue.Add(new Func<IEnumerator>(GameManager.Instance.TurnEnd));
				}
			}
			else
			{
				this.Animator.SetTrigger("MoveUp");
				SoundManager.Instance.Play(ResourcesManager.Sounds["Game_TurnButton_Up"]);
			}
		}
	}

	private void AnimateRotateDown()
	{
		base.StartCoroutine(this.RotateDownAnimation());
	}

	private IEnumerator RotateDownAnimation()
	{
		this.IsAnimating = true;
		this.Animator.SetTrigger("MoveUp");
		SoundManager.Instance.Play(ResourcesManager.Sounds["Game_TurnButton_Up"]);
		yield return new WaitForSeconds(0.25f);
		this.Animator.SetTrigger("RotateDown");
		SoundManager.Instance.Play("Game_TurnButton_End");
		yield return new WaitForSeconds(0.25f);
		this.IsAnimating = false;
		yield break;
	}

	private void AnimateRotateUp()
	{
		base.StartCoroutine(this.RotateUpAnimation());
	}

	private IEnumerator RotateUpAnimation()
	{
		this.IsAnimating = true;
		SoundManager.Instance.Play("Game_TurnButton_Start");
		this.Animator.SetTrigger("RotateUp");
		yield return new WaitForSeconds(0.25f);
		this.IsAnimating = false;
		yield break;
	}

	private bool IsEnabled = true;

	private bool IsHovering;

	private readonly Vector3 BasePosition = new Vector3(1475f, 45f, 627.5f);

	private Material ButtonMaterial;

	private readonly Vector2 yellowMaterial = new Vector2(0f, 0f);

	private readonly Vector2 greenMaterial = new Vector2(0f, 0.5f);

	private readonly Vector2 greyMaterial = new Vector2(0.5f, 0f);

	private Animator Animator;

	private bool IsAnimating;
}
