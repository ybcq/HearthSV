using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	private MenuManager()
	{
	}

	public static MenuManager Instance
	{
		get
		{
			return MenuManager._instance;
		}
	}

	private void Awake()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;
		if (MenuManager._instance == null)
		{
			MenuManager._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.transform.parent.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(this.Canvas);
		}
		else
		{
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
			UnityEngine.Object.Destroy(this.Canvas);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.ToggleGameMenu();
		}
	}

	public void SurrenderButtonClick()
	{
		if (GameManager.Instance != null && !GameManager.Instance.IsGameEnded)
		{
			this.ToggleGameMenu();
			GameManager.Instance.EndGame(GameManager.Instance.SelfPlayer);
		}
	}

	public void MenuButtonClick()
	{
		SoundManager.Instance.Play("Menu_Button_Click", 0.1f);
	}

	public void ConfirmDelete()
	{
		DeckManager.Instance.RemoveDeck(DeckSelector.Instance.FocusedDeck.Deck);
		DeckSelector.Instance.RemoveFocus();
		DeckSelector.Instance.ShowDecks();
		SoundManager.Instance.Play("DeckBuilder_Card_Remove", 0.1f);
		this.DeckBuilderConfirmMenu.SetActive(false);
	}

	public void CancelDelete()
	{
		this.DeckBuilderConfirmMenu.SetActive(false);
	}

	public void MenuButtonHover()
	{
		SoundManager.Instance.Play("Menu_Button_Hover", 0.05f);
	}

	public void ToggleGameMenu()
	{
		this.DeveloperMenu.SetActive(false);
		this.OptionsMenu.SetActive(false);
		this.GameMenu.SetActive(!this.GameMenu.activeSelf);
		if (this.GameMenu.activeSelf)
		{
			SoundManager.Instance.Play("GameMenu_Open", 0.1f);
		}
		else
		{
			SoundManager.Instance.Play("GameMenu_Close", 0.1f);
		}
	}

	public void OpenOptionsMenu()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			this.ToggleGameMenu();
			this.DeveloperMenu.SetActive(true);
		}
		else
		{
			this.ToggleGameMenu();
			this.OptionsMenu.SetActive(true);
		}
	}

	public void DisableAllMenus()
	{
		this.MainMenu.SetActive(false);
		this.GameMenu.SetActive(false);
		this.DeveloperMenu.SetActive(false);
		this.OptionsMenu.SetActive(false);
		this.DeckBuilderNameField.gameObject.SetActive(false);
	}

	private void ExitGame()
	{
		this.CanvasAnimator.SetTrigger("CloseTable");
		this.MainMenu.SetActive(true);
		this.GameMenu.SetActive(false);
		this.OptionsMenu.SetActive(false);
		this.SurrenderButton.interactable = false;
		this.SetDeveloperIngameOptions(false);
		base.StartCoroutine(this.BackgroundUnloadGame());
	}

	private IEnumerator BackgroundUnloadGame()
	{
		yield return new WaitForSeconds(1f);
		CameraManager.Instance.Reposition(new Vector3(0f, 6.4f, 0f), new Vector3(90f, 0f, 0f), 80f);
		CameraManager.Instance.DisableGray();
		CameraManager.Instance.DisableBlur();
		SceneManager.UnloadScene("GameBoard");
		yield break;
	}

	public void LoadPlaySelector()
	{
		if (!this.IsLoading)
		{
			SoundManager.Instance.PlayDelayed("Menu_Load", 0.1f, 0.75f);
			this.CanvasAnimator.SetTrigger("OpenTable");
			this.DeveloperMenu.SetActive(false);
			base.StartCoroutine(this.BackgroundLoadPlaySelector());
		}
	}

	private IEnumerator BackgroundLoadPlaySelector()
	{
		this.IsLoading = true;
		yield return SceneManager.LoadSceneAsync("PlaySelector", LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlaySelector"));
		yield return new WaitForSeconds(1f);
		this.IsLoading = false;
		yield break;
	}

	public void ExitPlaySelector()
	{
		if (!this.IsLoading)
		{
			this.GameMenu.SetActive(false);
			this.CanvasAnimator.SetTrigger("CloseTable");
			base.StartCoroutine(this.BackgroundUnloadPlaySelector());
		}
	}

	private IEnumerator BackgroundUnloadPlaySelector()
	{
		this.IsLoading = true;
		yield return new WaitForSeconds(0.15f);
		SoundManager.Instance.Play("Menu_Return", 0.1f);
		yield return new WaitForSeconds(0.85f);
		SceneManager.UnloadScene("PlaySelector");
		this.IsLoading = false;
		yield break;
	}

	public void LoadDeckBuilder()
	{
		if (!this.IsLoading)
		{
			SoundManager.Instance.PlayDelayed("Menu_Load", 0.1f, 0.75f);
			this.CanvasAnimator.SetTrigger("OpenTable");
			this.DeveloperMenu.SetActive(false);
			base.StartCoroutine(this.BackgroundLoadDeckBuilder());
		}
	}

	private IEnumerator BackgroundLoadDeckBuilder()
	{
		this.IsLoading = true;
		yield return SceneManager.LoadSceneAsync("DeckBuilder", LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("DeckBuilder"));
		yield return new WaitForSeconds(1f);
		this.IsLoading = false;
		yield break;
	}

	public void ExitDeckBuilder()
	{
		if (!this.IsLoading)
		{
			this.GameMenu.SetActive(false);
			this.DeckBuilderNameField.gameObject.SetActive(false);
			this.CanvasAnimator.SetTrigger("CloseTable");
			DeckManager.Instance.SaveDecklist();
			base.StartCoroutine(this.BackgroundUnloadDeckBuilder());
		}
	}

	private IEnumerator BackgroundUnloadDeckBuilder()
	{
		this.IsLoading = true;
		yield return new WaitForSeconds(0.15f);
		SoundManager.Instance.Play("Menu_Return", 0.1f);
		yield return new WaitForSeconds(0.85f);
		SceneManager.UnloadScene("DeckBuilder");
		this.IsLoading = false;
		yield break;
	}

	public void Exit()
	{
		string name = SceneManager.GetActiveScene().name;
		if (name != null)
		{
			if (!(name == "MainMenu"))
			{
				if (!(name == "DeckBuilder"))
				{
					if (!(name == "PlaySelector"))
					{
						if (name == "GameBoard")
						{
							this.ExitGame();
						}
					}
					else
					{
						this.ExitPlaySelector();
					}
				}
				else
				{
					this.ExitDeckBuilder();
				}
			}
			else
			{
				this.ExitApplication();
			}
		}
	}

	private void ExitApplication()
	{
		Application.Quit();
	}

	public IEnumerator FadeToBlack()
	{
		for (float f = 0f; f < 1f; f += 0.02f)
		{
			this.BlackOverlay.color = new Color(0f, 0f, 0f, f);
			yield return null;
		}
		this.BlackOverlay.color = new Color(0f, 0f, 0f, 1f);
		yield break;
	}

	public IEnumerator FadeToNormal()
	{
		for (float f = 1f; f > 0f; f -= 0.01f)
		{
			this.BlackOverlay.color = new Color(0f, 0f, 0f, f);
			yield return null;
		}
		this.BlackOverlay.color = new Color(0f, 0f, 0f, 0f);
		yield break;
	}

	public void SetDeveloperIngameOptions(bool ingame)
	{
		this.DeveloperMenu.GetComponentsInChildren<InputField>().ToList<InputField>().ForEach(delegate(InputField i)
		{
			i.interactable = ingame;
		});
		this.DeveloperMenu.GetComponentsInChildren<Button>().ToList<Button>().ForEach(delegate(Button i)
		{
			i.interactable = ingame;
		});
		this.DeveloperMenu.GetComponentsInChildren<Toggle>().ToList<Toggle>().ForEach(delegate(Toggle i)
		{
			i.interactable = !ingame;
		});
	}

	public void DeveloperUpdatePlayerStats()
	{
		GameManager.Instance.SelfPlayer.Hero.CurrentHealth = int.Parse(this.DeveloperHealthField.text);
		GameManager.Instance.SelfPlayer.AvailableMana = int.Parse(this.DeveloperCurrentManaField.text);
		GameManager.Instance.SelfPlayer.TurnMana = int.Parse(this.DeveloperTurnManaField.text);
		GameManager.Instance.GameUpdate();
	}

	public void DeveloperDrawCard()
	{
		ActionQueue.Add(() => GameManager.Instance.SelfPlayer.Draw(null));
	}

	public void DeveloperForceWin()
	{
		this.DeveloperMenu.SetActive(false);
		GameManager.Instance.EndGame(GameManager.Instance.EnemyPlayer);
	}

	public bool AllMenusClosed()
	{
		return !this.GameMenu.activeSelf && !this.OptionsMenu.activeSelf && !this.DeveloperMenu.activeSelf;
	}

	private static MenuManager _instance;

	public SavedDeck Deck;

	public GameObject Canvas;

	public GameObject MainMenu;

	public GameObject GameMenu;

	public Button SurrenderButton;

	public GameObject OptionsMenu;

	public GameObject DeveloperMenu;

	public Toggle DeveloperCardToggle;

	public Toggle DeveloperViewEnemyHand;

	public InputField DeveloperHealthField;

	public InputField DeveloperCurrentManaField;

	public InputField DeveloperTurnManaField;

	public InputField DeckBuilderNameField;

	public GameObject DeckBuilderConfirmMenu;

	public Animator CanvasAnimator;

	public SpriteRenderer BlackOverlay;

	private bool IsLoading;
}
