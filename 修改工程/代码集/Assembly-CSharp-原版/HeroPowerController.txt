using System;
using System.Collections;
using UnityEngine;

public class HeroPowerController : BaseController
{
	public static HeroPowerController Create(BaseHeroPower heroPower, bool isEnemy)
	{
		GameObject gameObject = new GameObject("HeroPower_Controller");
		gameObject.transform.ChangeParentAt(heroPower.Hero.Player.transform, new Vector3(4f, 0.5f, 0f));
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(3f, 3f, 0.1f);
		boxCollider.isTrigger = true;
		GameObject gameObject2 = new GameObject("Root");
		gameObject2.transform.ChangeParent(gameObject.transform);
		Animator animator = gameObject2.AddComponent<Animator>();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/HeroPowerAnimator");
		HeroPowerController heroPowerController = gameObject.AddComponent<HeroPowerController>();
		heroPowerController.HeroPower = heroPower;
		heroPowerController.Collider = boxCollider;
		heroPowerController.Child = gameObject2;
		heroPowerController.Animator = animator;
		heroPowerController.IsEnemy = isEnemy;
		heroPowerController.HoverController = HoverHeroPowerController.Create(heroPower, gameObject.transform);
		heroPowerController.Initialize();
		return heroPowerController;
	}

	public override void Initialize()
	{
		this.CostController = NumberController.Create("Cost_Controller", this.Child.gameObject, new Vector3(-0.02f, 1.15f, -0.06f), 20, 0.35f);
		this.FrontTokenRenderer = base.CreateChildMesh("FrontToken_Mesh", ShaderMode.Culled, new Vector3(0f, 0f, -0.05f), Vector3.zero, new Vector3(3f, 3.25f, 1f), 19);
		this.BackTokenRenderer = base.CreateChildMesh("BackToken_Mesh", ShaderMode.Culled, new Vector3(0f, 0f, 0.05f), new Vector3(0f, 180f, 0f), new Vector3(3f, 3.25f, 1f), 19);
		this.HeroPowerRenderer = base.CreateChildMesh("HeroPower_Mesh", ShaderMode.Culled, new Vector3(0f, 0f, -0.04f), Vector3.zero, new Vector3(3f, 3.25f, 1f), 18);
		this.WhiteGlowRenderer = base.CreateChildSprite("WhiteGlow_Sprite", Vector3.one * 2f, new Vector3(0f, -0.05f, 0f), 17);
		this.GreenGlowRenderer = base.CreateChildSprite("GreenGlow_Sprite", Vector3.one * 2f, new Vector3(0f, -0.05f, 0f), 16);
		this.RedGlowRenderer = base.CreateChildSprite("RedGlow_Sprite", Vector3.one * 2f, new Vector3(0f, -0.05f, 0f), 15);
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		this.CostController.Remove();
		UnityEngine.Object.Destroy(this.FrontTokenRenderer);
		UnityEngine.Object.Destroy(this.BackTokenRenderer);
		UnityEngine.Object.Destroy(this.HeroPowerRenderer);
		UnityEngine.Object.Destroy(this.WhiteGlowRenderer);
		UnityEngine.Object.Destroy(this.GreenGlowRenderer);
		UnityEngine.Object.Destroy(this.RedGlowRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.FrontTokenRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/General/HeroPower_FrontToken"));
		this.BackTokenRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/General/HeroPower_BackToken"));
		this.HeroPowerRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(string.Concat(new string[]
		{
			"Sprites/HeroPowers/",
			this.HeroPower.Class.GetEnumName(),
			"/",
			this.HeroPower.GetTypeName(),
			"_Token"
		})));
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows["Hero_Power_WhiteGlow"];
		this.GreenGlowRenderer.sprite = ResourcesManager.Glows["Hero_Power_GreenGlow"];
		this.RedGlowRenderer.sprite = ResourcesManager.Glows["Hero_Power_RedGlow"];
	}

	public override void UpdateNumbers()
	{
		this.CostController.UpdateNumber(this.HeroPower.CurrentCost, Util.GetInverseNumberColor(this.HeroPower.CurrentCost, this.HeroPower.BaseCost));
	}

	private void OnMouseEnter()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(true);
			if (!this.IsEnemy)
			{
				this.IsHovering = true;
			}
			this.HoverController.OnMouseEnter();
		}
	}

	private void OnMouseExit()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(false);
			if (!this.IsEnemy)
			{
				this.IsHovering = false;
			}
			this.HoverController.OnMouseExit();
		}
	}

	private void OnMouseDown()
	{
		if (!this.IsEnemy && !GameManager.Instance.IsGameEnded && this.HeroPower.Hero.Player.IsCurrent() && this.HeroPower.IsAvailable() && this.HeroPower.TargetType != TargetType.NoTarget)
		{
			InterfaceManager.Instance.CanTarget = new Func<Character, bool>(this.HeroPower.CanTarget);
			InterfaceManager.Instance.EnableArrow(this);
		}
	}

	private void OnMouseUp()
	{
		if (!this.IsEnemy && !GameManager.Instance.IsGameEnded)
		{
			InterfaceManager.Instance.DisableArrow();
			if (!this.IsQueued && this.HeroPower.Hero.Player.IsCurrent() && this.HeroPower.IsAvailable() && this.HeroPower.CanUse())
			{
				if (this.HeroPower.TargetType == TargetType.NoTarget)
				{
					if (this.IsHovering)
					{
						this.IsQueued = true;
						ActionQueue.Add(new Func<IEnumerator>(this.RotateDownAnimation));
						ActionQueue.Add(() => this.HeroPower.Hero.Player.UseHeroPower(null));
					}
				}
				else
				{
					Character target = Util.GetCharacterAtMouse();
					if (target != null && this.HeroPower.CanTarget(target))
					{
						this.IsQueued = true;
						ActionQueue.Add(new Func<IEnumerator>(this.RotateDownAnimation));
						ActionQueue.Add(() => this.HeroPower.Hero.Player.UseHeroPower(target));
					}
				}
			}
		}
	}

	public void AnimateRotateDown()
	{
		base.StartCoroutine(this.RotateDownAnimation());
	}

	public IEnumerator RotateDownAnimation()
	{
		this.IsDown = true;
		this.Animator.SetTrigger("RotateDown");
		SoundManager.Instance.Play("Game_HeroPower_Flip_Off");
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	public void AnimateRotateUp()
	{
		base.StartCoroutine(this.RotateUpAnimation());
	}

	public IEnumerator RotateUpAnimation()
	{
		this.IsDown = false;
		this.Animator.SetTrigger("RotateUp");
		SoundManager.Instance.Play("Game_HeroPower_Flip_On");
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	public IEnumerator ReplaceAnimation()
	{
		this.Animator.SetTrigger("Replace");
		yield return new WaitForSeconds(1.5f);
		yield break;
	}

	public BaseHeroPower HeroPower;

	public bool IsEnemy;

	private MeshRenderer HeroPowerRenderer;

	private MeshRenderer FrontTokenRenderer;

	private MeshRenderer BackTokenRenderer;

	private NumberController CostController;

	public HoverHeroPowerController HoverController;

	public bool IsQueued;

	private bool IsHovering;

	private Animator Animator;

	public bool IsDown;
}
