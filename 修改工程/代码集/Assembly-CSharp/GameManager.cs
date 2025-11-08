using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private GameManager()
	{
	}

	public static GameManager Instance
	{
		get
		{
			return GameManager._instance;
		}
	}

	private void Start()
	{
		GameManager._instance = this;
		this.IsGameEnded = false;
		EventManager.Instance.Reset();
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;
		SavedDeck deck = MenuManager.Instance.Deck;
		PlayerParameters parameters = new PlayerParameters
		{
			IsEnemy = false,
			PlayerPosition = this.BOTTOM_CENTER,
			HandPosition = this.BOTTOM_HAND,
			ManaPosition = this.BOTTOM_MANA,
			DisplayCrystals = true,
			BoardPosition = this.BOTTOM_BOARD,
			Hero = ClassManager.Heroes[deck.Class],
			Deck = deck.ToGameDeck()
		};
		this.SelfPlayer = Player.Create(parameters);
		SavedDeck randomAIDeck = DeckManager.Instance.GetRandomAIDeck();
		PlayerParameters parameters2 = new PlayerParameters
		{
			IsEnemy = true,
			PlayerPosition = this.TOP_CENTER,
			HandPosition = this.TOP_HAND,
			ManaPosition = this.TOP_MANA,
			DisplayCrystals = false,
			BoardPosition = this.TOP_BOARD,
			Hero = ClassManager.Heroes[randomAIDeck.Class],
			Deck = randomAIDeck.ToGameDeck()
		};
		this.EnemyPlayer = Player.Create(parameters2);
		this.AI = this.EnemyPlayer.gameObject.AddComponent<SimpleAI>();
		this.AI.Player = this.EnemyPlayer;
		this.SelfPlayer.Enemy = this.EnemyPlayer;
		this.EnemyPlayer.Enemy = this.SelfPlayer;
		this.SelfPlayer.Deck.Shuffle<BaseCard>();
		this.EnemyPlayer.Deck.Shuffle<BaseCard>();
		this.CurrentPlayer = RNG.RandomChoice<Player>(this.SelfPlayer, this.EnemyPlayer);
	}

	public IEnumerator Mulligan()
	{
		Debugger.Log("Mulligan phase start");
		if (this.CurrentPlayer == this.EnemyPlayer)
		{
			this.SelfPlayer.Mulligan(this.SelfPlayer.Deck.Take(3).ToArray<BaseCard>());
			yield return this.EnemyPlayer.InstantDraw(4, null);
		}
		else
		{
			yield return this.EnemyPlayer.InstantDraw(3, null);
			this.SelfPlayer.Mulligan(this.SelfPlayer.Deck.Take(4).ToArray<BaseCard>());
		}
		yield break;
	}

	public IEnumerator TurnStart()
	{
		Debugger.Log("Turn start");
		this.CanPlayCards = false;
		if (this.TotalTurns == 0)
		{
			yield return EventManager.Instance.OnGameStart();
		}
		this.TotalTurns++;
		this.CurrentTurnDeadMinions = 0;
		this.CurrentTurnPlayedSpells = 0;
		this.CurrentPlayer = this.CurrentPlayer.Enemy;
		this.CurrentPlayer.AddEmptyMana(1);
		this.CurrentPlayer.UsedMana = 0;
		this.CurrentPlayer.CurrentOverloadedMana = this.CurrentPlayer.NextOverloadedMana;
		this.CurrentPlayer.NextOverloadedMana = 0;
		this.CurrentPlayer.RefillMana();
		yield return EventManager.Instance.OnTurnStart(this.CurrentPlayer);
		if (this.CurrentPlayer.Meditations.Count > 0)
		{
			SoundManager.Instance.Play("Game_Secret_Trigger");
			foreach (BaseCard card in this.CurrentPlayer.Meditations)
			{
				if (this.CurrentPlayer.IsSelf())
				{
					yield return InterfaceManager.Instance.ShowFriendlyCard(card);
				}
				else
				{
					yield return InterfaceManager.Instance.ShowEnemyCard(card);
				}
				yield return card.Mechanics.Meditate.Fire(this.CurrentPlayer);
			}
			this.CurrentPlayer.Meditations.Clear();
			this.CurrentPlayer.MeditateController.UpdateSprites();
			this.CurrentPlayer.MeditateController.UpdateNumbers();
		}
		foreach (Minion minion in this.CurrentPlayer.Minions)
		{
			minion.CurrentTurnAttacks = 0;
		}
		this.CurrentPlayer.Hero.CurrentTurnAttacks = 0;
		this.CurrentPlayer.Hero.HeroPower.CurrentUses = 0;
		if (this.CurrentPlayer.IsSelf())
		{
			InterfaceManager.Instance.SpawnTurnSprite();
			SoundManager.Instance.Play("Game_YourTurn");
			this.TurnButton.Enable();
		}
		else
		{
			this.AI.DoTurn();
		}
		if (this.CurrentPlayer.HasWeapon())
		{
			SoundManager.Instance.Play("Game_Weapon_Unsheathe");
		}
		if (this.CurrentPlayer.Hero.HeroPower.Controller.IsDown)
		{
			this.CurrentPlayer.Hero.HeroPower.Controller.AnimateRotateUp();
		}
		yield return this.CurrentPlayer.Draw(null);
		this.CanPlayCards = true;
		ActionQueue.AddVoid(new Action(this.GameUpdate));
		yield break;
	}

	public IEnumerator TurnEnd()
	{
		Debugger.Log("Turn end");
		yield return EventManager.Instance.OnTurnEnd(this.CurrentPlayer);
		if (this.CurrentPlayer.HasWeapon())
		{
			SoundManager.Instance.Play("Game_Weapon_Sheathe");
		}
		foreach (Minion minion in this.CurrentPlayer.Minions)
		{
			minion.IsSleeping = false;
			if (minion.IsFrozen && minion.CurrentTurnAttacks == 0)
			{
				minion.IsFrozen = false;
			}
		}
		if (this.CurrentPlayer.Hero.IsFrozen && this.CurrentPlayer.Hero.CurrentTurnAttacks == 0)
		{
			this.CurrentPlayer.Hero.IsFrozen = false;
		}
		this.CurrentPlayer.ResetVisuals();
		yield return this.TurnStart();
		yield break;
	}

	public void GameUpdate()
	{
		AuraManager.Instance.UpdateAuras();
		this.SelfPlayer.UpdateVisuals();
		this.EnemyPlayer.UpdateVisuals();
	}

	public void EndGame(Player loser)
	{
		this.IsGameEnded = true;
		CameraManager.Instance.FadeToGray(1f);
		CameraManager.Instance.FadeToBlur(1f);
		InterfaceManager.Instance.SpawnEndGameSprite(loser);
		this.AI.Stop();
		ActionQueue.StopAll();
	}

	public List<Character> GetAllCharacters()
	{
		return this.SelfPlayer.GetAllCharacters().Concat(this.EnemyPlayer.GetAllCharacters()).ToList<Character>();
	}

	public List<Minion> GetAllMinions()
	{
		return this.EnemyPlayer.Minions.Concat(this.SelfPlayer.Minions).ToList<Minion>();
	}

	public List<BaseCard> GetAllCards()
	{
		return this.GetAllHandCards().Concat(this.GetAllDeckCards()).ToList<BaseCard>();
	}

	public List<BaseCard> GetAllHandCards()
	{
		return this.EnemyPlayer.Hand.Concat(this.SelfPlayer.Hand).ToList<BaseCard>();
	}

	public List<BaseCard> GetAllDeckCards()
	{
		return this.EnemyPlayer.Deck.Concat(this.SelfPlayer.Deck).ToList<BaseCard>();
	}

	public bool IsTurnOf(Character character)
	{
		return character.Player == this.CurrentPlayer;
	}

	private static GameManager _instance;

	private readonly Vector3 BOTTOM_CENTER = new Vector3(798f, 60f, 230f);

	private readonly Vector3 BOTTOM_HAND = new Vector3(-0.5f, -9.6f, -8f);

	private readonly Vector3 BOTTOM_MANA = new Vector3(6.65f, -3.2f, 0f);

	private readonly Vector3 BOTTOM_BOARD = new Vector3(0f, 5.5f, 0.5f);

	private readonly Vector3 TOP_CENTER = new Vector3(800f, 60f, 935f);

	private readonly Vector3 TOP_HAND = new Vector3(0f, 11f, -8f);

	private readonly Vector3 TOP_MANA = new Vector3(6f, 3.7f, 0f);

	private readonly Vector3 TOP_BOARD = new Vector3(0f, -4f, 0.5f);

	public Player EnemyPlayer;

	public Player SelfPlayer;

	public Player CurrentPlayer;

	public SimpleAI AI;

	public TurnButtonController TurnButton;

	public Shader EndGameShader;

	public int CurrentTurnDeadMinions;

	public int CurrentTurnPlayedSpells;

	public int TotalTurns;

	public bool CanPlayCards;

	public bool IsMulliganing = true;

	public bool IsGameEnded;
}
