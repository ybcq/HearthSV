using System;
using System.Collections;
using UnityEngine;

public class MinionController : global::CharacterController
{
	public static MinionController Create(BoardController parentBoard, Minion minion)
	{
		GameObject gameObject = new GameObject(minion.Card.Name + "_Controller");
		gameObject.transform.ChangeParent(parentBoard.transform);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(2.5f, 3.5f, 0.5f);
		boxCollider.isTrigger = true;
		GameObject gameObject2 = new GameObject("Root");
		gameObject2.transform.ChangeParent(gameObject.transform);
		Animator animator = gameObject2.AddComponent<Animator>();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/MinionAnimator");
		animator.enabled = false;
		MinionController minionController = gameObject.AddComponent<MinionController>();
		minionController.Minion = minion;
		minionController.Child = gameObject2;
		minionController.Collider = boxCollider;
		minionController.Animator = animator;
		minionController.HoverController = HoverCardController.Create(minion.Card, gameObject.transform);
		minionController.Initialize();
		return minionController;
	}

	public override void Initialize()
	{
		this.AttackController = NumberController.Create("Attack_Controller", this.Child, new Vector3(-0.75f, -0.95f, -0.04f), 15, 0.35f);
		this.HealthController = NumberController.Create("Health_Controller", this.Child, new Vector3(0.825f, -0.95f, -0.04f), 15, 0.35f);
		this.DeathrattleRenderer = Util.Instantiate(EffectsManager.Instance.MinionDeathrattlePrefab, this.Child.transform);
		this.DivineShieldRenderer = Util.Instantiate(EffectsManager.Instance.MinionDivineShieldPrefab, this.Child.transform);
		this.EvasionRenderer = Util.Instantiate(EffectsManager.Instance.MinionEvasionPrefab, this.Child.transform);
		this.FreezeRenderer = Util.Instantiate(EffectsManager.Instance.MinionFreezePrefab, this.Child.transform);
		this.ImmuneRenderer = Util.Instantiate(EffectsManager.Instance.MinionImmunePrefab, this.Child.transform);
		this.InspireRenderer = Util.Instantiate(EffectsManager.Instance.MinionInspirePrefab, this.Child.transform);
		this.PoisonRenderer = Util.Instantiate(EffectsManager.Instance.MinionPoisonPrefab, this.Child.transform);
		this.SilenceRenderer = Util.Instantiate(EffectsManager.Instance.MinionSilencePrefab, this.Child.transform);
		this.SpellshieldRenderer = Util.Instantiate(EffectsManager.Instance.MinionSpellshieldPrefab, this.Child.transform);
		this.StealthRenderer = Util.Instantiate(EffectsManager.Instance.MinionStealthPrefab, this.Child.transform);
		this.TriggerRenderer = Util.Instantiate(EffectsManager.Instance.MinionTriggerPrefab, this.Child.transform);
		this.TriggerFlashRenderer = Util.Instantiate(EffectsManager.Instance.MinionTriggerFlashPrefab, this.Child.transform);
		this.TriggerFlashRenderer.SetActive(false);
		this.WindfuryRenderer = base.CreateChildSprite("Windfury_Sprite", Vector3.one, new Vector3(0f, 0f, -0.03f), 14);
		this.EnragedRenderer = base.CreateChildSprite("Enraged_Sprite", Vector3.one, new Vector3(0f, 0f, 0f), 14);
		this.TokenRenderer = base.CreateChildMesh("Token_Mesh", ShaderMode.Normal, new Vector3(0f, -0.15f, -0.02f), Vector3.zero, new Vector3(4f, 5f, 4f), 14);
		this.MinionRenderer = base.CreateChildMesh("Minion_Mesh", ShaderMode.Culled, Vector3.zero, Vector3.zero, Vector3.one * 4f, 13);
		this.WhiteGlowRenderer = base.CreateChildSprite("WhiteGlow_Sprite", Vector3.one, new Vector3(0f, -0.15f, 0.02f), 12);
		this.GreenGlowRenderer = base.CreateChildSprite("GreenGlow_Sprite", Vector3.one, new Vector3(0f, -0.15f, 0.04f), 11);
		this.RedGlowRenderer = base.CreateChildSprite("RedGlow_Sprite", Vector3.one, new Vector3(0f, -0.15f, 0.04f), 10);
		this.UpdateSprites();
		this.UpdateNumbers();
	}

	public override void DestroyController()
	{
		this.AttackController.Remove();
		this.HealthController.Remove();
		UnityEngine.Object.Destroy(this.TokenRenderer);
		UnityEngine.Object.Destroy(this.MinionRenderer);
		UnityEngine.Object.Destroy(this.WhiteGlowRenderer);
		UnityEngine.Object.Destroy(this.GreenGlowRenderer);
		UnityEngine.Object.Destroy(this.RedGlowRenderer);
		base.StopAllCoroutines();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void UpdateSprites()
	{
		string tokenPath = this.GetTokenPath();
		string glowPath = this.GetGlowPath();
		this.WindfuryRenderer.sprite = ResourcesManager.Effects["Windfury"];
		this.EnragedRenderer.sprite = ResourcesManager.Effects["Enraged"];
		this.TokenRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/General/" + tokenPath));
		this.MinionRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/" + this.Minion.Card.Class.GetEnumName() + "/Minions/" + this.Minion.Card.GetTypeName()));
		this.WhiteGlowRenderer.sprite = ResourcesManager.Glows[glowPath + "WhiteGlow"];
		this.GreenGlowRenderer.sprite = ResourcesManager.Glows[glowPath + "GreenGlow"];
		this.RedGlowRenderer.sprite = ResourcesManager.Glows[glowPath + "RedGlow"];
		this.DeathrattleRenderer.SetActive(this.Minion.Mechanics.HasDeathrattle());
		this.DivineShieldRenderer.SetActive(this.Minion.HasDivineShield);
		this.EvasionRenderer.SetActive(this.Minion.IsEvasive);
		this.FreezeRenderer.SetActive(this.Minion.IsFrozen);
		this.ImmuneRenderer.SetActive(this.Minion.IsImmune);
		this.InspireRenderer.SetActive(this.Minion.Mechanics.HasInspire());
		this.PoisonRenderer.SetActive(this.Minion.HasPoison);
		this.SilenceRenderer.SetActive(this.Minion.IsSilenced);
		this.SpellshieldRenderer.SetActive(this.Minion.HasSpellshield);
		this.StealthRenderer.SetActive(this.Minion.IsStealth);
		this.TriggerRenderer.SetActive(this.Minion.Mechanics.HasTrigger());
		this.WindfuryRenderer.enabled = this.Minion.HasWindfury;
		this.EnragedRenderer.enabled = (this.Minion.Mechanics.HasEnrage() && this.Minion.GetMissingHealth() > 0);
	}

	public override void UpdateNumbers()
	{
		this.AttackController.UpdateNumber(this.Minion.CurrentAttack, Util.GetAttackNumberColor(this.Minion.CurrentAttack, this.Minion.BaseAttack));
		this.HealthController.UpdateNumber(this.Minion.CurrentHealth, Util.GetCharacterNumberColor(this.Minion.CurrentHealth, this.Minion.BaseHealth, this.Minion.MaxHealth));
	}

	private string GetTokenPath()
	{
		string str = "Minion_";
		if (this.Minion.Card.Rarity == CardRarity.Legendary)
		{
			str += "Legendary";
		}
		else
		{
			str += "Normal";
		}
		if (this.Minion.HasTaunt)
		{
			str += "Taunt";
		}
		return str + "Token";
	}

	private string GetGlowPath()
	{
		string str = "Minion_";
		if (this.Minion.Card.Rarity == CardRarity.Legendary)
		{
			str += "Legendary";
		}
		else
		{
			str += "Normal";
		}
		if (this.Minion.HasTaunt)
		{
			str += "Taunt";
		}
		return str + "_";
	}

	public override Character GetCharacter()
	{
		return this.Minion;
	}

	public void ChangeRenderingOrder(int value)
	{
		foreach (Renderer renderer in base.transform.GetComponents<Renderer>())
		{
			renderer.sortingOrder += value;
		}
	}

	private void Update()
	{
		if (!this.IsAnimating && !this.IsPressing)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.TargetPosition, 0.5f);
		}
	}

	private void OnMouseEnter()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(true);
			InterfaceManager.Instance.OnHoverStart(this);
			this.HoverController.OnMouseEnter();
		}
	}

	private void OnMouseExit()
	{
		if (!GameManager.Instance.IsGameEnded)
		{
			base.SetWhiteRenderer(false);
			InterfaceManager.Instance.OnHoverStop();
			this.HoverController.OnMouseExit();
		}
	}

	private void OnMouseDown()
	{
		if (this.Minion.Player.IsSelf() && this.Minion.Player.IsCurrent() && !GameManager.Instance.IsGameEnded && this.Minion.CanAttack())
		{
			this.IsPressing = true;
			this.AnimateLevitateWait();
			InterfaceManager.Instance.EnableArrow(this);
			InterfaceManager.Instance.EnlightenTargetsOf(this.Minion);
			SoundManager.Instance.PlayMinionSound(this.Minion.Card, "Attack", 0.5f);
		}
	}

	private void OnMouseUp()
	{
		if (this.Minion.Player.IsSelf() && this.Minion.Player.IsCurrent() && !GameManager.Instance.IsGameEnded)
		{
			if (!InterfaceManager.Instance.IsListening)
			{
				InterfaceManager.Instance.DisableArrow();
			}
			if (this.IsPressing)
			{
				this.IsPressing = false;
				if (GameManager.Instance.IsTurnOf(this.Minion) && this.Minion.CanAttack())
				{
					InterfaceManager.Instance.DarkenAllTargets();
					Character target = Util.GetCharacterAtMouse();
					if (target != null && this.Minion.CanAttackTo(target))
					{
						if (this.Minion.HasWindfury)
						{
							if (this.Minion.CurrentTurnAttacks + 1 < 2)
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
						ActionQueue.Add(() => this.Minion.Attack(target));
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
		base.StartCoroutine(this.LevitateWaitAnimation());
	}

	public override IEnumerator LevitateWaitAnimation()
	{
		this.StartAnimating();
		this.Animator.SetTrigger("Levitate");
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	public override void AnimateDelevitate()
	{
		base.StartCoroutine(this.DelevitateAnimation());
	}

	public override IEnumerator DelevitateAnimation()
	{
		this.StartAnimating();
		this.Animator.SetTrigger("Delevitate");
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
		this.Animator.enabled = false;
		this.IsAnimating = true;
		MonoBehaviour.print("Animating " + this.Minion.Card.Name + " attack to " + target.GetName());
		Vector3 startingPosition = base.transform.position;
		Vector3 targetPosition = target.Controller.transform.position + new Vector3(0f, startingPosition.y - target.Controller.transform.position.y, 0f) + new Vector3(0f, 50f, 0f);
		Vector3 endPosition = base.transform.parent.TransformPoint(this.TargetPosition);
		Vector3 directionVector = targetPosition - startingPosition;
		Vector3 returnDirectionVector = targetPosition - endPosition;
		float attackStartTime = Time.timeSinceLevelLoad;
		float attackElapsedTime = 0f;
		float attackDuration = 0.2f;
		while (attackElapsedTime <= attackDuration)
		{
			attackElapsedTime = Time.timeSinceLevelLoad - attackStartTime;
			float normalizedIteration = Mathf.Clamp01(attackElapsedTime / attackDuration);
			base.transform.position = startingPosition + directionVector * normalizedIteration;
			yield return null;
		}
		if (selfDamage > 0)
		{
			InterfaceManager.Instance.SpawnDamageSplatOn(this, selfDamage);
		}
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, enemyDamage);
		SoundManager.Instance.PlayImpactSound(this.Minion.CurrentAttack);
		this.UpdateNumbers();
		this.UpdateSprites();
		target.Controller.UpdateNumbers();
		target.Controller.UpdateSprites();
		float returnStartTime = Time.timeSinceLevelLoad;
		float returnElapsedTime = 0f;
		float returnDuration = 0.25f;
		while (returnElapsedTime <= returnDuration)
		{
			returnElapsedTime = Time.timeSinceLevelLoad - returnStartTime;
			float normalizedIteration2 = Mathf.Clamp01(1f - returnElapsedTime / returnDuration);
			base.transform.position = endPosition + returnDirectionVector * normalizedIteration2;
			this.Child.transform.localPosition = Vector3.back * normalizedIteration2;
			yield return null;
		}
		this.IsAnimating = false;
		yield break;
	}

	public override void AnimateDestroy()
	{
		base.StartCoroutine(this.DestroyAnimation());
	}

	public override IEnumerator DestroyAnimation()
	{
		this.StartAnimating();
		this.Animator.SetTrigger("Destroy");
		base.Invoke("PlayDeathSound", 0.5f);
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < 1f)
		{
			if (this.MinionRenderer == null)
			{
				yield break;
			}
			float elapsedTime = Time.timeSinceLevelLoad - startTime;
			float color = 0.2f + Mathf.Lerp(0.8f, 0f, elapsedTime);
			this.MinionRenderer.material.color = new Color(color, color, color, 1f);
			yield return null;
		}
		this.StopAnimating();
		yield break;
	}

	private void PlayDeathSound()
	{
		SoundManager.Instance.Play("Game_Minion_Death");
	}

	public void AnimateTriggerFlash()
	{
		base.StartCoroutine(this.TriggerFlashAnimation());
	}

	public IEnumerator TriggerFlashAnimation()
	{
		this.TriggerFlashRenderer.SetActive(true);
		SoundManager.Instance.Play("Game_Mechanic_Trigger");
		yield return new WaitForSeconds(1.5f);
		this.TriggerFlashRenderer.SetActive(false);
		yield break;
	}

	public IEnumerator AnimateHeroReplace()
	{
		this.IsAnimating = true;
		this.Animator.enabled = false;
		Vector3 startingPos = base.transform.localPosition;
		Vector3 endPos = new Vector3(0f, 2.5f, -10f);
		float startTime = Time.timeSinceLevelLoad;
		float levitateDuration = 1.25f;
		while (Time.timeSinceLevelLoad - startTime < levitateDuration)
		{
			float elapsedTime = Time.timeSinceLevelLoad - startTime;
			base.transform.localPosition = Util.InverseCubicLerp(startingPos, endPos, elapsedTime / levitateDuration);
			yield return null;
		}
		base.transform.localPosition = endPos;
		startTime = Time.timeSinceLevelLoad;
		float rotateLeftDuration = 0.25f;
		Vector3 originRotation = Vector3.zero;
		Vector3 endRotation = new Vector3(0f, -30f, 0f);
		while (Time.timeSinceLevelLoad - startTime < rotateLeftDuration)
		{
			float elapsedTime2 = Time.timeSinceLevelLoad - startTime;
			base.transform.localEulerAngles = Util.InverseCubicLerp(originRotation, endRotation, elapsedTime2 / rotateLeftDuration);
			yield return null;
		}
		startTime = Time.timeSinceLevelLoad;
		float rotateRightDuration = 0.25f;
		originRotation = new Vector3(0f, -30f, 0f);
		endRotation = new Vector3(0f, 90f, 0f);
		while (Time.timeSinceLevelLoad - startTime < rotateRightDuration)
		{
			float elapsedTime3 = Time.timeSinceLevelLoad - startTime;
			base.transform.localEulerAngles = Vector3.Lerp(originRotation, endRotation, elapsedTime3 / rotateRightDuration);
			yield return null;
		}
		yield break;
	}

	public Minion Minion;

	public Vector3 TargetPosition;

	public int BoardPosition;

	public HoverCardController HoverController;

	private MeshRenderer TokenRenderer;

	private MeshRenderer MinionRenderer;

	private GameObject DeathrattleRenderer;

	private GameObject DivineShieldRenderer;

	private GameObject EvasionRenderer;

	private GameObject FreezeRenderer;

	private GameObject ImmuneRenderer;

	private GameObject InspireRenderer;

	private GameObject PoisonRenderer;

	private GameObject SilenceRenderer;

	private GameObject SpellshieldRenderer;

	private GameObject StealthRenderer;

	private GameObject TriggerRenderer;

	private GameObject TriggerFlashRenderer;

	private SpriteRenderer WindfuryRenderer;

	private SpriteRenderer EnragedRenderer;

	private NumberController AttackController;

	private NumberController HealthController;

	private bool IsPressing;

	private Animator Animator;

	private bool IsAnimating;
}
