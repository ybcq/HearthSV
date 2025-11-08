using System;
using UnityEngine;

public static class Debugger
{
	public static void Log(string text)
	{
		string str = string.Format("{0:HH:mm:ss}", DateTime.Now);
		MonoBehaviour.print("[" + str + "] " + text);
	}

	public static void LogCard(BaseCard card, string action)
	{
		Debugger.Log(card.Name + " " + action);
	}

	public static void LogMinion(Minion minion, string action)
	{
		Debugger.Log(minion.Card.Name + " " + action);
	}

	public static void LogPlayer(Player player, string action)
	{
		Debugger.Log(player.Hero.GetName() + " " + action);
	}

	public static void LogHero(Hero hero, string action)
	{
		Debugger.Log(hero.GetName() + " " + action);
	}

	public static void LogWeapon(Weapon weapon, string action)
	{
		Debugger.Log(weapon.Card.Name + " " + action);
	}

	public static void LogCharacter(Character character, string action)
	{
		Debugger.Log(character.GetName() + " " + action);
	}
}
