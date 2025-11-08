using System;
using System.Collections;
using UnityEngine;

public class HeroController : global::CharacterController
{
	public static HeroController Create(Hero hero, bool isEnemy)
	{
		GameObject gameObject = new GameObject("Hero_Controller");
		gameObject.transform.ChangeParent(hero.Player.transform);
		Animator animator = gameObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/HeroAnimator");
		animator.enabled = false;
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.center = new Vector3(0f, 0.75f, 0f);
		boxCollider.size = new Vector3(4f, 4f, 0.1f);
		boxCollider.isTrigger = true;
		HeroController heroController = gameObject.AddComponent<HeroController>();
		heroController.Hero = hero;
		heroController.Collider = boxCollider;
		heroController.Animator = animator;
		heroController.IsEnemy = isEnemy;
		heroController.Initialize();
		return heroController;
	}

	public override void Initialize()
	{
		this.AttackController = NumberController.Create("Attack_Controller", base.gameObject, new Vector3(-1.4f, -0.9f, -0.01f), 36, 0.45f);
		this.HealthController = NumberController.Create("Health_Controller", base.gameObject, new Vector3(1.45f, -0.9f, -0.01f), 36, 0.45f);
		this.ArmorController = NumberController.Create("Armor_Controller", base.gameObject, new Vector3(1.5f, 0.55f, -0.01f), 36, 0.45f);
		this.AttackRenderer = base.CreateSprite("Attack_Sprite", Vector3.one * 0.5f, new Vector3(-1.5f, -0.75f, -0.01f), 35);
		this.HealthRenderer = base.CreateSprite("Health_Sprite", Vector3.one * 0.55f, new Vector3(1.5f, -0.75f, -0.01f), 35);
		this.ArmorRenderer = base.CreateSprite("Armor_Sprite", Vector3.one * 0.45f, new Vector3(1.5f, 0.55f, -0.01f), 35);
		this.EvasionRenderer = Util.Instantiate(EffectsManager.Instance.HeroEvasionPrefab, base.transform);
		this.FreezeRenderer = Util.Instantiate(EffectsManager.Instance.HeroFreezePrefab, base.transform);
		this.ImmuneRenderer = Util.Instantiate(EffectsManager.Instance.HeroImmunePrefab, base.transform);
		this.StealthRenderer = Util.Instantiate(EffectsManager.Instance.HeroStealthPrefab, base.transform);
		this.SpellshieldRenderer = Util.Instantiate(EffectsManager.Instance.HeroSpellshieldPrefab, base.transform);
		this.PresenceRenderer = Util.Instantiate(EffectsManager.Instance.PresencePrefab, base.transform);
		this.HeroRenderer = base.CreateMesh("Hero_Mesh", ShaderMode.Normal, Vector3.zero, Vector3.zero, new Vector3(4f, 5.5f, 1f), 33);
		this.WhiteGlowRenderer = base.CreateSprite("WhiteGlow_Sprite", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0.01f), 32);
		this.GreenGlowRenderer = base.CreateSprite("GreenGlow_Sprite", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0.01f), 31);
		this.RedGlowRenderer = base.CreateSprite("RedGlow_Sprite", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0.01f), 30);
		this.HeroRenderer.enabled = true;
		this.HealthRenderer.enabled = true;
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		this.AttackController.Remove();
		this.HealthController.Remove();
		this.ArmorController.Remove();
		UnityEngine.Object.Destroy(this.HeroRenderer);
		UnityEngine.Object.Destroy(this.AttackRenderer);
		UnityEngine.Object.Destroy(this.HealthRenderer);
		UnityEngine.Object.Destroy(this.GreenGlowRenderer);
		UnityEngine.Object.Destroy(this.RedGlowRenderer);
		UnityEngine.Object.Destroy(this.WhiteGlowRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		this.HeroRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(string.Concat(new string[]
		{
			"Sprites/Heroes/",
			this.Hero.Class.GetEnumName(),
			"/",
			this.Hero.GetTypeName(),
			"_Portrait_Ingame"
		})));
		this.AttackRenderer.sprite = ResourcesManager.Attributes["Attack"];
		this.HealthRenderer.sprite = ResourcesManager.Attributes["Health"];
		this.ArmorRenderer.sprite = ResourcesManager.Attributes["Armor"];
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows["Hero_Portrait_WhiteGlow"];
		this.GreenGlowRenderer.sprite = ResourcesManager.Glows["Hero_Portrait_GreenGlow"];
		this.RedGlowRenderer.sprite = ResourcesManager.Glows["Hero_Portrait_RedGlow"];
		this.EvasionRenderer.SetActive(this.Hero.IsEvasive);
		this.FreezeRenderer.SetActive(this.Hero.IsFrozen);
		this.ImmuneRenderer.SetActive(this.Hero.IsImmune);
		this.StealthRenderer.SetActive(this.Hero.IsStealth);
		this.SpellshieldRenderer.SetActive(this.Hero.HasSpellshield);
	}

	public override void UpdateNumbers()
	{
		if (this.Hero.CurrentAttack > 0)
		{
			this.AttackController.UpdateNumber(this.Hero.CurrentAttack, "White");
			this.AttackRenderer.enabled = true;
			this.AttackController.SetEnabled(true);
		}
		else
		{
			this.AttackRenderer.enabled = false;
			this.AttackController.SetEnabled(false);
		}
		if (this.Hero.CurrentArmor > 0)
		{
			this.ArmorController.UpdateNumber(this.Hero.CurrentArmor, "White");
			this.ArmorRenderer.enabled = true;
			this.ArmorController.SetEnabled(true);
		}
		else
		{
			this.ArmorRenderer.enabled = false;
			this.ArmorController.SetEnabled(false);
		}
		if (this.Hero.CurrentHealth == this.Hero.MaxHealth)
		{
			this.HealthController.UpdateNumber(this.Hero.CurrentHealth, "White");
		}
		else
		{
			this.HealthController.UpdateNumber(this.Hero.CurrentHealth, "Red");
		}
	}

	public void SetPresence(Presence presence)
	{
		this.PresenceRenderer.SetActive(true);
		if (presence != Presence.Blood)
		{
			if (presence != Presence.Frost)
			{
				if (presence == Presence.Unholy)
				{
					this.PresenceRenderer.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 0f, 0.75f);
					this.HeroRenderer.GetComponent<Renderer>().material.color = new Color(0.5f, 1f, 0.5f, 1f);
				}
			}
			else
			{
				this.PresenceRenderer.GetComponent<SpriteRenderer>().color = new Color(0f, 0.5f, 1f, 0.6f);
				this.HeroRenderer.GetComponent<Renderer>().material.color = new Color(0.25f, 0.5f, 1f, 1f);
			}
		}
		else
		{
			this.PresenceRenderer.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.75f);
			this.HeroRenderer.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0.5f, 1f);
		}
	}

	public override Character GetCharacter()
	{
		return this.Hero;
	}

	private void Update()
	{
		if (!this.IsAnimating && !this.IsPressing)
		{
			base.transform.localPosition = Vector3.zero;
		}
	}

	private void OnMouseEnter()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(true);
			InterfaceManager.Instance.OnHoverStart(this);
		}
	}

	private void OnMouseExit()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(false);
			InterfaceManager.Instance.OnHoverStop();
		}
	}

	private void OnMouseDown()
	{
		if (!this.IsEnemy && this.Hero.Player.IsCurrent() && !GameManager.Instance.IsGameEnded && this.Hero.CanAttack() && !this.IsAnimating)
		{
			this.IsPressing = true;
			this.AnimateLevitateWait();
			InterfaceManager.Instance.EnableArrow(this);
			InterfaceManager.Instance.EnlightenTargetsOf(this.Hero);
			SoundManager.Instance.PlayHeroSound(this.Hero, "Attack", 0.25f);
		}
	}

	private void OnMouseUp()
	{
		if (!this.IsEnemy && this.Hero.Player.IsCurrent() && !GameManager.Instance.IsGameEnded)
		{
			InterfaceManager.Instance.DisableArrow();
			if (this.IsPressing)
			{
				this.IsPressing = false;
				if (GameManager.Instance.IsTurnOf(this.Hero))
				{
					InterfaceManager.Instance.DarkenAllTargets();
					Character target = Util.GetCharacterAtMouse();
					if (target != null && this.Hero.CanAttackTo(target))
					{
						if (this.Hero.HasWindfury)
						{
							if (this.Hero.CurrentTurnAttacks < 2)
							{
								base.SetGreenRenderer(true);
							}
							else
							{
								base.SetGreenRenderer(false);
							}
						}
						else
						{
							base.SetGreenRenderer(false);
						}
						ActionQueue.Add(() => this.Hero.Attack(target));
						return;
					}
				}
				this.AnimateDelevitate();
			}
		}
	}

	public void StartAnimating()
	{
		this.Animator.enabled = true;
		this.IsAnimating = true;
	}

	public void StopAnimating()
	{
		this.Animator.enabled = false;
		this.IsAnimating = false;
	}

	public override void AnimateLevitateWait()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.LevitateWaitAnimation());
	}

	public override IEnumerator LevitateWaitAnimation()
	{
		this.StartAnimating();
		if (this.Hero.Player.IsEnemy)
		{
			this.Animator.SetTrigger("LevitateEnemy");
		}
		else
		{
			this.Animator.SetTrigger("LevitateSelf");
		}
		SoundManager.Instance.Play("Game_Hero_Attack_Start");
		yield return new WaitForSeconds(1.5f);
		this.Animator.enabled = false;
		yield break;
	}

	public override void AnimateDelevitate()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.DelevitateAnimation());
	}

	public override IEnumerator DelevitateAnimation()
	{
		this.StartAnimating();
		if (this.Hero.Player.IsEnemy)
		{
			this.Animator.SetTrigger("DelevitateEnemy");
		}
		else
		{
			this.Animator.SetTrigger("DelevitateSelf");
		}
		SoundManager.Instance.Play("Game_Hero_Attack_End");
		yield return new WaitForSeconds(0.5f);
		this.StopAnimating();
		yield break;
	}

	public override void AnimateAttack(Character target, int enemyDamage, int selfDamage)
	{
		base.StartCoroutine(this.AttackAnimation(target, enemyDamage, selfDamage));
	}

	public override IEnumerator AttackAnimation(Character target, int enemyDamage, int selfDamage)
	{
		this.IsAnimating = true;
		AnimatorStateInfo animState = this.Animator.GetCurrentAnimatorStateInfo(0);
		AnimatorClipInfo animClip = this.Animator.GetCurrentAnimatorClipInfo(0)[0];
		float remainingTime = animClip.clip.length - animClip.clip.length * animState.normalizedTime;
		yield return new WaitForSeconds(remainingTime);
		Vector3 startingPosition = base.transform.position;
		Vector3 targetPosition = target.Controller.transform.position;
		Vector3 endPosition = startingPosition - new Vector3(0f, 350f, 0f);
		Vector3 directionVector = targetPosition - startingPosition;
		Vector3 returnDirectionVector = targetPosition - endPosition;
		float attackStartTime = Time.timeSinceLevelLoad;
		float attackElapsedTime = 0f;
		float attackDuration = 0.1f;
		while (attackElapsedTime <= attackDuration)
		{
			attackElapsedTime = Time.timeSinceLevelLoad - attackStartTime;
			float normalizedIteration = Mathf.Clamp01(attackElapsedTime / attackDuration);
			base.transform.localEulerAngles = new Vector3(35f * normalizedIteration, 0f, 0f);
			base.transform.position = startingPosition + directionVector * normalizedIteration;
			yield return null;
		}
		if (selfDamage > 0)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(this, selfDamage);
		}
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, enemyDamage);
		SoundManager.Instance.PlayImpactSound(this.Hero.CurrentAttack);
		this.UpdateNumbers();
		this.UpdateSprites();
		target.Controller.UpdateNumbers();
		target.Controller.UpdateSprites();
		float returnStartTime = Time.timeSinceLevelLoad;
		float returnElapsedTime = 0f;
		float returnDuration = 0.5f;
		while (returnElapsedTime <= returnDuration)
		{
			returnElapsedTime = Time.timeSinceLevelLoad - returnStartTime;
			float normalizedIteration2 = Util.InverseCubicLerp(1f, 0f, returnElapsedTime / returnDuration);
			base.transform.localEulerAngles = new Vector3(35f * normalizedIteration2, 0f, 0f);
			base.transform.position = endPosition + returnDirectionVector * normalizedIteration2;
			yield return null;
		}
		SoundManager.Instance.Play("Game_Hero_Attack_End");
		this.IsAnimating = false;
		yield break;
	}

	public override void AnimateDestroy()
	{
		base.StartCoroutine(this.DestroyAnimation());
	}

	public IEnumerator AnimateReplaceFromCenter()
	{
		this.StartAnimating();
		this.Animator.SetTrigger("ReplaceFromCenter");
		yield return new WaitForSeconds(1.35f);
		SoundManager.Instance.Play("Game_Hero_Attack_End");
		yield return new WaitForSeconds(0.1f);
		this.StopAnimating();
		yield break;
	}

	public override IEnumerator DestroyAnimation()
	{
		yield break;
	}

	public Hero Hero;

	private MeshRenderer HeroRenderer;

	private GameObject EvasionRenderer;

	private GameObject FreezeRenderer;

	private GameObject ImmuneRenderer;

	private GameObject SpellshieldRenderer;

	private GameObject StealthRenderer;

	private GameObject PresenceRenderer;

	private SpriteRenderer AttackRenderer;

	private SpriteRenderer HealthRenderer;

	private SpriteRenderer ArmorRenderer;

	private NumberController AttackController;

	private NumberController HealthController;

	private NumberController ArmorController;

	private bool IsEnemy;

	private bool IsPressing;

	private Animator Animator;

	public bool IsAnimating;
}
