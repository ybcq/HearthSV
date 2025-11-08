using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySelector : MonoBehaviour
{
	private PlaySelector()
	{
	}

	public static PlaySelector Instance
	{
		get
		{
			return PlaySelector._instance;
		}
	}

	public void ShowDecks()
	{
		this.Controllers.ForEach(delegate(PlayDeckController c)
		{
			c.DestroyController();
		});
		this.Controllers.Clear();
		for (int i = 0; i < DeckManager.Instance.SavedDecks.Count; i++)
		{
			SavedDeck deck = DeckManager.Instance.SavedDecks[i];
			int num = i % 3 - 1;
			int num2 = i / 3 - 1;
			Vector3 position = new Vector3((float)num * 2.4f, (float)(-(float)num2) * 2.4f, 0f);
			PlayDeckController item = PlayDeckController.Create(this.DeckParent, deck, position);
			this.Controllers.Add(item);
		}
	}

	public void FocusDeck(PlayDeckController focusedController)
	{
		this.FocusedDeck = focusedController;
		foreach (PlayDeckController playDeckController in this.Controllers)
		{
			if (playDeckController != focusedController)
			{
				playDeckController.UnFocus();
			}
			else
			{
				focusedController.Focus();
			}
		}
		HeroClass @class = focusedController.Deck.Class;
		string enumName = @class.GetEnumName();
		this.HeroRenderer.sprite = Resources.Load<Sprite>(string.Concat(new string[]
		{
			"Sprites/Heroes/",
			enumName,
			"/",
			ClassManager.Heroes[@class].Name,
			"_Portrait_Menu"
		}));
		this.HeroPowerRenderer.sprite = Resources.Load<Sprite>(string.Concat(new string[]
		{
			"Sprites/HeroPowers/",
			enumName,
			"/",
			ClassManager.HeroPowers[@class].Name,
			"_Token"
		}));
		this.DeckNameController.UpdateText(focusedController.Deck.Name);
	}

	public void RemoveFocus()
	{
		this.FocusedDeck = null;
		this.HeroRenderer.sprite = Resources.Load<Sprite>("Sprites/Heroes/None/None_Portrait_Menu");
		this.HeroPowerRenderer.sprite = Resources.Load<Sprite>("Sprites/None/HeroPower/Token");
		this.DeckNameController.UpdateText("None");
	}

	public void Play()
	{
		SavedDeck deck = this.FocusedDeck.Deck;
		MenuManager.Instance.Deck = deck;
		if (deck.ToGameDeck().Count == 30 || MenuManager.Instance.DeveloperCardToggle.isOn)
		{
			this.IsLoading = true;
			this.StartSpin();
			CameraManager.Instance.FadeToBlur(2f);
			MenuManager.Instance.DisableAllMenus();
			base.StartCoroutine(this.PlayCoroutine());
		}
		else
		{
			SoundManager.Instance.Play("DeckBuilder_Card_Invalid");
		}
	}

	private IEnumerator PlayCoroutine()
	{
		yield return new WaitForSeconds(1f);
		ResourcesManager.Instance.WarmAssets();
		yield return new WaitForSeconds(1f);
		yield return SceneManager.LoadSceneAsync("GameBoard", LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameBoard"));
		MapManager.Instance.CurrentTableTop.SetActive(false);
		while (!ResourcesManager.Instance.IsAssetWarmFinished())
		{
			yield return null;
		}
		yield return this.StopSpin();
		yield return MenuManager.Instance.FadeToBlack();
		CameraManager.Instance.DisableBlur();
		CameraManager.Instance.Reposition(new Vector3(800f, 2000f, 600f), new Vector3(90f, 0f, 0f), 35f);
		MapManager.Instance.CurrentTableTop.SetActive(true);
		yield return MenuManager.Instance.FadeToNormal();
		SceneManager.UnloadSceneAsync("PlaySelector");
		ActionQueue.Add(new Func<IEnumerator>(GameManager.Instance.Mulligan));
		yield break;
	}

	private void Awake()
	{
		PlaySelector._instance = this;
		this.Animator = this.FindOpponentGameObject.GetComponent<Animator>();
		this.DeckNameController = TextController.CreateBuilderText("DeckName", this.Selector, new Vector3(4.5f, 3.15f, 0f), TextAnchor.MiddleCenter, 20, 1005);
		this.ShowDecks();
		if (this.Controllers.Count > 0)
		{
			this.FocusDeck(this.Controllers[0]);
		}
		else
		{
			this.RemoveFocus();
		}
	}

	public void StartSpin()
	{
		this.FindOpponentGameObject.SetActive(true);
		this.Animator.SetTrigger("Spin");
		SoundManager.Instance.Play("Spinner_WindUp");
		SoundManager.Instance.Play("Spinner_Start");
		this.AudioLoopSource = SoundManager.Instance.PlayOnLoop("Spinner_Loop", 0.25f);
	}

	public IEnumerator StopSpin()
	{
		UnityEngine.Object.Destroy(this.AudioLoopSource);
		this.Animator.SetTrigger("Stop");
		SoundManager.Instance.Play("Spinner_End");
		MenuManager.Instance.SurrenderButton.interactable = true;
		MenuManager.Instance.SetDeveloperIngameOptions(true);
		yield return new WaitForSeconds(2f);
		for (float f = 0f; f < 0.5f; f += 0.01f)
		{
			this.FindOpponentGameObject.transform.localScale = Util.InverseCubicLerp(Vector3.one * 0.5f, Vector3.zero, f / 0.5f);
			yield return null;
		}
		this.FindOpponentGameObject.transform.localScale = Vector3.zero;
		yield break;
	}

	private static PlaySelector _instance;

	public GameObject DeckParent;

	public GameObject Selector;

	public SpriteRenderer HeroRenderer;

	public SpriteRenderer HeroPowerRenderer;

	public PlayDeckController FocusedDeck;

	private TextController DeckNameController;

	private List<PlayDeckController> Controllers = new List<PlayDeckController>();

	private const float DISTANCE = 2.4f;

	public bool IsLoading;

	public GameObject FindOpponentGameObject;

	private Animator Animator;

	private AudioSource AudioLoopSource;

	private string SpinnerFinalMessage = "Mighty AI";

	private List<string> SpinnerMessages = new List<string>
	{
		"Test",
		"Kappa",
		"Keepo",
		"PogChamp"
	};
}
