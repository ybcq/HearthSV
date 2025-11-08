using System;
using System.Collections;
using System.Linq;

public class ExhumedLich : MinionCard
{
	public ExhumedLich()
	{
		this.Name = "Exhumed Lich";
		this.Description = "Battlecry: Destroy a friendly Undead and gain Mana Crystals equal to its Health this turn only.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 7;
		this.BaseAttack = 9;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.FriendlyMinions;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return this.Player.Minions.Any((Minion m) => m.Card.MinionType == MinionType.Undead);
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target.IsFriendlyOf(this.Player.Hero) && target.As<Minion>().Card.MinionType == MinionType.Undead;
	}

	public IEnumerator Battlecry(Character target)
	{
		Minion targetMinion = (Minion)target;
		int currentHealth = targetMinion.CurrentHealth;
		yield return targetMinion.Destroy();
		this.Player.AddTurnMana(currentHealth);
		yield break;
	}
}
