using System;
using System.Collections;

public class SoulFragment : MinionCard
{
	public SoulFragment()
	{
		this.Name = "利维坦";
		this.Description = "Can't attack until you being powerful.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Dragon;
		this.BaseCost = 4;
		this.BaseAttack = 6;
		this.BaseHealth = 5;
		this.CantAttack = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.OnTurnStart.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnStart));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnStart(TurnEvent evt)
	{
		if (this.Player.TurnMana > 8)
		{
			yield return this.CantAttack = false;
			yield break;
		}
		yield break;
	}

	public IEnumerator Battlecry(Character target)
	{
		if (this.Player.TurnMana == 10)
		{
			yield return this.CantAttack = false;
		}
		yield break;
	}
}
