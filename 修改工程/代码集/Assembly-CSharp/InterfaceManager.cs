using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
	private InterfaceManager()
	{
	}

	public static InterfaceManager Instance
	{
		get
		{
			return InterfaceManager._instance;
		}
	}

	private SpriteRenderer CreateChildSprite(string sprite, int order)
	{
		GameObject gameObject = new GameObject(sprite.Substring(sprite.LastIndexOf("/", StringComparison.InvariantCulture) + 1));
		gameObject.transform.ChangeParent(base.transform);
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = order;
		spriteRenderer.sprite = Resources.Load<Sprite>(sprite);
		spriteRenderer.enabled = false;
		return spriteRenderer;
	}

	private void Update()
	{
		if (this.IsListening && Input.GetMouseButtonDown(0))
		{
			Character target = Util.GetCharacterAtMouse();
			if (target != this.Minion)
			{
				if (this.Condition(target))
				{
					ActionQueue.Add(() => this.DoAction(target));
				}
				else if (this.CancelAction != null)
				{
					this.CancelAction(this.Minion);
				}
				this.IsListening = false;
				this.DisableArrow();
			}
			else
			{
				this.EnableArrow(this.Minion.Player.Hero.Controller);
			}
		}
	}

	public void ListenToTarget(Minion minion, Func<Character, IEnumerator> doAction, Func<Character, bool> condition, Action<Minion> cancelAction)
	{
		this.IsListening = true;
		this.Minion = minion;
		this.DoAction = doAction;
		this.Condition = condition;
		this.CancelAction = cancelAction;
		this.EnableArrow(minion.Player.Hero.Controller);
		this.CanTarget = new Func<Character, bool>(minion.Card.CanBattlecryTarget);
	}

	public void EnableArrowAt(BaseController controller, Vector3 position)
	{
		this.worldOriginPosition = position;
		this.originController = controller;
		this.IsTargeting = true;
		this.arrowRenderer.enabled = true;
		this.bodyRenderer.enabled = true;
	}

	public void EnableArrow(BaseController controller)
	{
		this.EnableArrowAt(controller, controller.transform.position);
	}

	public void DisableArrow()
	{
		this.IsTargeting = false;
		this.CanTarget = null;
		this.arrowRenderer.enabled = false;
		this.bodyRenderer.enabled = false;
		this.circleRenderer.enabled = false;
	}

	public void OnHoverStart(global::CharacterController controller)
	{
		if (this.IsTargeting)
		{
			if (this.CanTarget != null)
			{
				if (this.CanTarget(controller.GetCharacter()))
				{
					this.circleRenderer.enabled = true;
				}
			}
			else
			{
				this.circleRenderer.enabled = true;
			}
		}
	}

	public void OnHoverStop()
	{
		this.circleRenderer.enabled = false;
	}

	public void ShowMulliganSelection(params BaseCard[] cards)
	{
		GameManager.Instance.SelfPlayer.IsSelectingCard = true;
		this.SelectionType = SelectionType.Mulligan;
		base.StartCoroutine(this.CreateSelectionCards(new Action<SelectionCardController>(this.AnimateMulliganDraw), 0.25f, -12.5f, cards));
	}

	public void ShowCardSelection(Func<BaseCard, IEnumerator> action, params BaseCard[] cards)
	{
		GameManager.Instance.SelfPlayer.IsSelectingCard = true;
		this.ChoiceAction = action;
		this.SelectionType = SelectionType.SelectOne;
		base.StartCoroutine(this.CreateSelectionCards(new Action<SelectionCardController>(this.AnimateZoom), 0f, -17.5f, cards));
	}

	public void ShowEnemySelection(int quantity)
	{
	}

	private IEnumerator CreateSelectionCards(Action<SelectionCardController> animation, float delay, float depth, params BaseCard[] cards)
	{
		this.SelectionControllers.Clear();
		this.IsAnimatingMulligan = true;
		for (int i = 0; i < cards.Length; i++)
		{
			float inverse = (float)i - ((float)cards.Length - 0.5f);
			float x = inverse * 4.5f + 4.5f * ((float)cards.Length / 2f);
			SelectionCardController controller = SelectionCardController.Create(cards[i], new Vector3(x, 7.5f, depth), i);
			controller.transform.ChangeParentAt(GameManager.Instance.SelfPlayer.transform, new Vector3(x, 7.5f, depth));
			this.SelectionControllers.Add(controller);
			animation(controller);
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}
		}
		yield return new WaitForSeconds(0.5f);
		this.IsAnimatingMulligan = false;
		this.MulliganButton.SetActive(this.SelectionType == SelectionType.Mulligan);
		this.ShowButton.SetActive(this.SelectionType == SelectionType.SelectOne);
		this.SelectionControllers.ForEach(delegate(SelectionCardController c)
		{
			c.SetGreenRenderer(true);
		});
		yield break;
	}

	private void AnimateMulliganDraw(SelectionCardController controller)
	{
		base.StartCoroutine("MulliganDrawAnimation", controller);
	}

	private IEnumerator MulliganDrawAnimation(SelectionCardController controller)
	{
		float startTime = Time.timeSinceLevelLoad;
		SoundManager.Instance.Play("Game_Draw_Card");
		while (Time.timeSinceLevelLoad - startTime < 0.75f)
		{
			float normalizedValue = (Time.timeSinceLevelLoad - startTime) / 0.75f;
			controller.transform.localPosition = Vector3.Slerp(this.DeckPosition, controller.TargetPosition, normalizedValue);
			controller.transform.localEulerAngles = Vector3.Slerp(this.DeckRotation, Vector3.zero, normalizedValue);
			yield return 0;
		}
		controller.transform.localPosition = controller.TargetPosition;
		controller.transform.localEulerAngles = Vector3.zero;
		yield break;
	}

	private void AnimateMulliganUndraw(SelectionCardController controller)
	{
		base.StartCoroutine("MulliganUndrawAnimation", controller);
	}

	private IEnumerator MulliganUndrawAnimation(SelectionCardController controller)
	{
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < 1f)
		{
			float normalizedValue = (Time.timeSinceLevelLoad - startTime) / 1f;
			controller.transform.localPosition = Vector3.Slerp(controller.TargetPosition, this.DeckPosition, normalizedValue);
			controller.transform.localEulerAngles = Vector3.Slerp(Vector3.zero, this.DeckRotation, normalizedValue);
			yield return 0;
		}
		controller.transform.localPosition = this.DeckPosition;
		controller.transform.localEulerAngles = this.DeckRotation;
		yield break;
	}

	private void AnimateZoom(SelectionCardController controller)
	{
		base.StartCoroutine("ZoomAnimation", controller);
	}

	private IEnumerator ZoomAnimation(SelectionCardController controller)
	{
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < 0.25f)
		{
			float normalizedValue = (Time.timeSinceLevelLoad - startTime) / 0.25f;
			controller.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, normalizedValue);
			yield return 0;
		}
		yield break;
	}

	public IEnumerator PickSelection(SelectionCardController selectedController)
	{
		GameManager.Instance.SelfPlayer.IsSelectingCard = false;
		SelectionType selectionType = this.SelectionType;
		if (selectionType != SelectionType.Mulligan)
		{
			if (selectionType == SelectionType.SelectOne)
			{
				this.MulliganButton.SetActive(false);
				this.ShowButton.SetActive(false);
				yield return this.ChoiceAction(selectedController.Card);
				foreach (SelectionCardController selectionCardController in this.SelectionControllers)
				{
					selectionCardController.DestroyController();
				}
			}
		}
		else
		{
			selectedController.ToggleCross();
		}
		yield break;
	}

	public void MulliganButton_Click()
	{
		base.StartCoroutine(this.MulliganEndAnimation());
	}

	private IEnumerator MulliganEndAnimation()
	{
		Debugger.Log("Mulligan phase end");
		this.IsAnimatingMulligan = true;
		this.MulliganButton.SetActive(false);
		this.ShowButton.SetActive(false);
		this.SelectionControllers.ForEach(delegate(SelectionCardController c)
		{
			c.SetGreenRenderer(false);
		});
		List<BaseCard> mulliganCards = (from c in this.SelectionControllers
		select c.Card).ToList<BaseCard>();
		List<SelectionCardController> discardedControllers = (from c in this.SelectionControllers
		where c.IsDiscarded
		select c).ToList<SelectionCardController>();
		int totalCount = this.SelectionControllers.Count;
		int discardedCount = discardedControllers.Count;
		if (discardedCount > 0)
		{
			List<int> indexControllers = new List<int>();
			foreach (SelectionCardController controller in discardedControllers)
			{
				indexControllers.Add(this.SelectionControllers.IndexOf(controller));
				this.AnimateMulliganUndraw(controller);
				yield return new WaitForSeconds(0.5f);
			}
			discardedControllers.Reverse();
			indexControllers.Reverse();
			yield return new WaitForSeconds(1f);
			List<BaseCard> drawableCards = (from c in GameManager.Instance.SelfPlayer.Deck
			where !mulliganCards.Contains(c)
			select c).ToList<BaseCard>();
			List<BaseCard> newCards = drawableCards.Take(discardedControllers.Count).ToList<BaseCard>();
			discardedControllers.ForEach(delegate(SelectionCardController c)
			{
				this.SelectionControllers.Remove(c);
			});
			discardedControllers.ForEach(delegate(SelectionCardController c)
			{
				c.DestroyController();
			});
			for (int i = discardedCount - 1; i >= 0; i--)
			{
				float inverse = (float)indexControllers[i] - ((float)totalCount - 0.5f);
				float x = inverse * 4.5f + 4.5f * ((float)totalCount / 2f);
				SelectionCardController controller2 = SelectionCardController.Create(newCards[i], new Vector3(x, 7.5f, -12.5f), indexControllers[i]);
				controller2.transform.ChangeParent(GameManager.Instance.SelfPlayer.transform);
				this.SelectionControllers.Insert(indexControllers[i], controller2);
				this.AnimateMulliganDraw(controller2);
				yield return new WaitForSeconds(0.5f);
			}
			yield return new WaitForSeconds(1.25f);
		}
		foreach (SelectionCardController selectionCardController in this.SelectionControllers)
		{
			GameManager.Instance.SelfPlayer.MulliganDrawFromDeck(selectionCardController.Card, selectionCardController);
			selectionCardController.DestroyController();
		}
		yield return new WaitForSeconds(0.5f);
		yield return this.BlackOverlayFade();
		foreach (BaseCard baseCard in GameManager.Instance.SelfPlayer.Hand)
		{
			baseCard.Controller.Speed = 100f;
		}
		yield return GameManager.Instance.CurrentPlayer.AddCardToHand(new Coin());
		ActionQueue.Add(new Func<IEnumerator>(GameManager.Instance.TurnEnd));
		GameManager.Instance.IsMulliganing = false;
		this.IsAnimatingMulligan = false;
		yield break;
	}

	public void ShowButton_Click()
	{
	}

	public IEnumerator BlackOverlayFade()
	{
		for (float i = 0.8f; i > 0f; i -= 0.0075f)
		{
			this.BlackOverlay.color = new Color(1f, 1f, 1f, i);
			yield return null;
		}
		this.BlackOverlay.enabled = false;
		yield break;
	}

	private void Awake()
	{
		InterfaceManager._instance = this;
	}

	private void Start()
	{
		this.turnRenderer = this.CreateChildSprite("Sprites/General/YourTurn", 1003);
		this.arrowRenderer = this.CreateChildSprite("Sprites/UI/Arrow", 1002);
		this.bodyRenderer = this.CreateChildSprite("Sprites/UI/ArrowBody", 1001);
		this.circleRenderer = this.CreateChildSprite("Sprites/UI/ArrowCircle", 1000);
		this.turnRenderer.transform.localPosition = new Vector3(10f, 1f, 8f);
		this.turnRenderer.transform.localEulerAngles = Vector3.right * 90f;
	}

	private void LateUpdate()
	{
		if (this.IsTargeting)
		{
			Vector3 worldMousePosition = Util.GetWorldMousePosition();
			Vector3 a = worldMousePosition - this.worldOriginPosition;
			float y = Mathf.Atan2(a.x, a.z) * 57.29578f;
			Vector3 localEulerAngles = new Vector3(90f, y, 0f);
			this.arrowRenderer.transform.position = worldMousePosition;
			this.arrowRenderer.transform.localEulerAngles = localEulerAngles;
			this.arrowRenderer.transform.localPosition += new Vector3(0f, 0.5f, 0f);
			this.circleRenderer.transform.position = worldMousePosition;
			this.circleRenderer.transform.localEulerAngles = Vector3.right * 90f;
			this.circleRenderer.transform.localPosition += new Vector3(0f, 0.5f, 0f);
			Vector3 vector = this.worldOriginPosition + a / 2f;
			float y2 = 0.003375f * a.magnitude;
			this.bodyRenderer.transform.position = new Vector3(vector.x, 100f, vector.z);
			this.bodyRenderer.transform.localEulerAngles = localEulerAngles;
			this.bodyRenderer.transform.localScale = new Vector3(1f, y2, 1f);
		}
	}

	public void SpawnTurnSprite()
	{
		if (this.TurnFadeCoroutine != null)
		{
			base.StopCoroutine(this.TurnFadeCoroutine);
		}
		this.TurnFadeCoroutine = this.TurnSpriteFade();
		base.StartCoroutine(this.TurnFadeCoroutine);
	}

	private IEnumerator TurnSpriteFade()
	{
		this.turnRenderer.enabled = true;
		this.turnRenderer.transform.localScale = Vector3.one * 1.5f;
		yield return new WaitForSeconds(1f);
		for (float i = 1.5f; i > 0f; i -= 0.1f)
		{
			this.turnRenderer.transform.localScale = Vector3.one * i;
			yield return null;
		}
		this.turnRenderer.enabled = false;
		yield break;
	}

	public void EnlightenTargetsOf(Character character)
	{
		Player enemy = character.Player.Enemy;
		if (enemy.HasTauntMinions())
		{
			foreach (Minion minion in enemy.Minions)
			{
				if (minion.HasTaunt && !minion.IsStealth)
				{
					minion.Controller.SetRedRenderer(true);
				}
			}
		}
		else
		{
			enemy.Hero.Controller.SetRedRenderer(true);
			foreach (Minion minion2 in enemy.Minions)
			{
				if (!minion2.IsStealth)
				{
					minion2.Controller.SetRedRenderer(true);
				}
			}
		}
	}

	public void EnlightenTargetsOf(SpellCard spell)
	{
		foreach (Character character in GameManager.Instance.GetAllCharacters())
		{
			if (spell.CanTarget(character))
			{
				character.Controller.SetRedRenderer(true);
			}
		}
	}

	public void DarkenAllTargets()
	{
		foreach (Character character in GameManager.Instance.GetAllCharacters())
		{
			character.Controller.SetRedRenderer(false);
		}
	}

	public IEnumerator ShowEnemyCard(BaseCard card)
	{
		PreviewCardController cardController = PreviewCardController.Create(card);
		cardController.transform.ChangeParent(GameManager.Instance.SelfPlayer.HandController.transform);
		yield return this.ShowCardAnimation(cardController, "ShowEnemy");
		yield break;
	}

	public IEnumerator ShowFriendlyCard(BaseCard card)
	{
		PreviewCardController cardController = PreviewCardController.Create(card);
		cardController.transform.ChangeParent(GameManager.Instance.SelfPlayer.HandController.transform);
		yield return this.ShowCardAnimation(cardController, "ShowFriendly");
		yield break;
	}

	public IEnumerator ShowNeutralCard(BaseCard card)
	{
		PreviewCardController cardController = PreviewCardController.Create(card);
		cardController.transform.ChangeParent(GameManager.Instance.SelfPlayer.HandController.transform);
		yield return this.ShowCardAnimation(cardController, "ShowNeutral");
		yield break;
	}

	private IEnumerator ShowCardAnimation(PreviewCardController controller, string animation)
	{
		controller.GetComponent<Animator>().SetTrigger(animation);
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(controller.gameObject, 1f);
		yield break;
	}

	public void SpawnDamageSplatOn(global::CharacterController controller, int damage)
	{
		string typeName = controller.GetTypeName();
		if (typeName != null)
		{
			if (!(typeName == "MinionController"))
			{
				if (typeName == "HeroController")
				{
					this.SpawnDamageSplatOn(controller.As<HeroController>(), damage);
				}
			}
			else
			{
				this.SpawnDamageSplatOn(controller.As<MinionController>(), damage);
			}
		}
	}

	public void SpawnDamageSplatOn(MinionController controller, int damage)
	{
		GameObject gameObject = new GameObject("DamageSplat");
		gameObject.transform.ChangeParent(controller.transform);
		gameObject.transform.localScale = Vector3.one * 0.75f;
		this.StartDamageSplat(gameObject, damage);
	}

	public void SpawnDamageSplatOn(HeroController controller, int damage)
	{
		GameObject gameObject = new GameObject("DamageSplat");
		gameObject.transform.ChangeParentAt(controller.transform, new Vector3(0f, 0.5f, 0f));
		gameObject.transform.localScale = Vector3.one;
		this.StartDamageSplat(gameObject, damage);
	}

	private void StartDamageSplat(GameObject childObject, int damage)
	{
		SpriteRenderer spriteRenderer = childObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = ResourcesManager.Splats["Damage"];
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = 600;
		TextController textController = TextController.CreateGameText("Text", childObject, new Vector3(0f, 0.25f, 0f), TextAnchor.MiddleCenter, 150, 650);
		textController.UpdateText("-" + damage);
		base.StartCoroutine(this.FadeSplatAnimation(spriteRenderer, textController));
	}

	public void SpawnHealSplatOn(global::CharacterController controller, int heal)
	{
		if (controller != null)
		{
			string typeName = controller.GetTypeName();
			if (typeName != null)
			{
				if (!(typeName == "MinionController"))
				{
					if (typeName == "HeroController")
					{
						this.SpawnHealSplatOn(controller.As<HeroController>(), heal);
					}
				}
				else
				{
					this.SpawnHealSplatOn(controller.As<MinionController>(), heal);
				}
			}
		}
		else
		{
			Debugger.Log("Couldn't display Heal splat because controller was null");
		}
	}

	private void SpawnHealSplatOn(MinionController controller, int heal)
	{
		GameObject gameObject = new GameObject("HealSplat");
		gameObject.transform.ChangeParent(controller.transform);
		gameObject.transform.localScale = Vector3.one * 2f;
		this.StartHealSplat(gameObject, heal);
	}

	private void SpawnHealSplatOn(HeroController controller, int heal)
	{
		GameObject gameObject = new GameObject("HealSplat");
		gameObject.transform.ChangeParentAt(controller.transform, new Vector3(0f, 0.5f, 0f));
		gameObject.transform.localScale = Vector3.one * 3f;
		this.StartHealSplat(gameObject, heal);
	}

	private void StartHealSplat(GameObject childObject, int damage)
	{
		SpriteRenderer spriteRenderer = childObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = ResourcesManager.Splats["Heal"];
		spriteRenderer.sortingLayerName = "Game";
		spriteRenderer.sortingOrder = 600;
		TextController textController = TextController.CreateGameText("Text", childObject, new Vector3(0f, 0.1f, 0f), TextAnchor.MiddleCenter, 125, 650);
		textController.transform.localScale = Vector3.one * 0.05f;
		textController.UpdateText("+" + damage);
		base.StartCoroutine(this.FadeSplatAnimation(spriteRenderer, textController));
	}

	private IEnumerator FadeSplatAnimation(SpriteRenderer splatRenderer, TextController textController)
	{
		yield return new WaitForSeconds(1.5f);
		Color alphaLoss = new Color(0f, 0f, 0f, 0.01f);
		for (int i = 0; i < 100; i++)
		{
			if (splatRenderer != null)
			{
				splatRenderer.material.color -= alphaLoss;
				foreach (TextMesh textMesh in textController.Meshes)
				{
					textMesh.color -= alphaLoss;
				}
				yield return 0;
			}
		}
		if (splatRenderer != null)
		{
			UnityEngine.Object.Destroy(splatRenderer.gameObject);
		}
		yield break;
	}

	public void SpawnEndGameSprite(Player player)
	{
		if (this.EndGameCoroutine != null)
		{
			base.StopCoroutine(this.EndGameCoroutine);
		}
		if (this.EndGameLoopAudio != null)
		{
			UnityEngine.Object.Destroy(this.EndGameLoopAudio);
			this.EndGameLoopAudio = null;
		}
		this.EndGameCoroutine = this.EndGameSprite(player);
		base.StartCoroutine(this.EndGameCoroutine);
	}

	private IEnumerator EndGameSprite(Player player)
	{
		SpriteRenderer endGameRenderer;
		if (player.IsSelf())
		{
			SoundManager.Instance.Play("Game_Defeat_Start");
			this.EndGameLoopAudio = SoundManager.Instance.PlayOnLoop("Game_Defeat_Thunder_Loop", 0.25f);
			endGameRenderer = this.CreateChildSprite("Sprites/UI/DefeatScreen", 1011);
		}
		else
		{
			SoundManager.Instance.Play("Game_Victory_Start");
			SoundManager.Instance.Play("Game_Victory_Jingle");
			SoundManager.Instance.Play("Game_Victory_Fireworks_Start");
			this.EndGameLoopAudio = SoundManager.Instance.PlayOnLoop("Game_Victory_Fireworks_Loop", 0.25f);
			endGameRenderer = this.CreateChildSprite("Sprites/UI/VictoryScreen", 1011);
		}
		string className = GameManager.Instance.SelfPlayer.Hero.Class.GetEnumName();
		SpriteRenderer endGameHeroRenderer = this.CreateChildSprite(string.Concat(new string[]
		{
			"Sprites/Heroes/",
			className,
			"/",
			GameManager.Instance.SelfPlayer.Hero.GetTypeName(),
			"_Portrait_Ingame"
		}), 1010);
		endGameRenderer.transform.localPosition = new Vector3(10f, 1f, 8f);
		endGameRenderer.transform.localEulerAngles = Vector3.right * 90f;
		endGameRenderer.transform.localScale = Vector3.one * 1.5f;
		endGameRenderer.gameObject.layer = LayerMask.NameToLayer("Ignore Effects");
		endGameRenderer.enabled = true;
		endGameHeroRenderer.transform.localPosition = new Vector3(10.158f, 1f, 8.928f);
		endGameHeroRenderer.transform.localEulerAngles = Vector3.right * 90f;
		endGameHeroRenderer.transform.localScale = Vector3.one * 1.3f;
		endGameHeroRenderer.gameObject.layer = LayerMask.NameToLayer("Ignore Effects");
		endGameHeroRenderer.enabled = true;
		yield return new WaitForSeconds(2f);
		base.StartCoroutine(this.WaitForClick());
		yield break;
	}

	private IEnumerator WaitForClick()
	{
		bool hasExited = false;
		while (!hasExited)
		{
			if (Input.GetMouseButtonDown(0))
			{
				hasExited = true;
				UnityEngine.Object.Destroy(this.EndGameLoopAudio);
				this.EndGameLoopAudio = null;
				MenuManager.Instance.Exit();
			}
			else
			{
				yield return null;
			}
		}
		yield break;
	}

	private static InterfaceManager _instance;

	public bool IsTargeting;

	public bool IsDragging;

	public bool IsListening;

	public Func<Character, bool> CanTarget;

	private Minion Minion;

	private Func<Character, IEnumerator> DoAction;

	private Func<Character, bool> Condition;

	private Action<Minion> CancelAction;

	private BaseController originController;

	private Vector3 worldOriginPosition = Vector3.zero;

	private SpriteRenderer turnRenderer;

	private SpriteRenderer arrowRenderer;

	private SpriteRenderer circleRenderer;

	private SpriteRenderer bodyRenderer;

	public GameObject ShowButton;

	public GameObject MulliganButton;

	public SpriteRenderer BlackOverlay;

	private Func<BaseCard, IEnumerator> ChoiceAction;

	private SelectionType SelectionType;

	private List<SelectionCardController> SelectionControllers = new List<SelectionCardController>();

	private const float SELECTION_DISTANCE = 4.5f;

	private readonly Vector3 DeckPosition = new Vector3(15f, 4.5f, 0f);

	private readonly Vector3 DeckRotation = new Vector3(0f, -90f, 90f);

	public bool IsAnimatingMulligan;

	private IEnumerator TurnFadeCoroutine;

	private IEnumerator EndGameCoroutine;

	private AudioSource EndGameLoopAudio;
}
