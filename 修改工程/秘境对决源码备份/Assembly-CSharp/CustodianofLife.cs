using System;
using System.Collections;

public class CustodianofLife : MinionCard
{
	public CustodianofLife()
	{
		this.Name = "好运波葛";
		this.Description = "If you control an artifact when you enter the battlefield, draw a card.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.HasWeapon();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.Player.Draw(null);
		yield break;
	}
}
