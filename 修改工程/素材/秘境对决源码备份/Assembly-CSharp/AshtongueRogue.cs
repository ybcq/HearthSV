using System;
using System.Collections;
using System.Linq;

public class AshtongueRogue : MinionCard
{
	public AshtongueRogue()
	{
		this.Name = "死灵骑士";
		this.Description = "Guard. Call of Duty: A random guardian effect is given to yourself.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.HasTaunt = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		Minion minion = RNG.RandomItemFrom<Minion>((from m in this.Player.Minions
		where m.IsAlive() && m.Card.MinionType != MinionType.Totem
		select m).ToList<Minion>());
		if (minion != null)
		{
			yield return minion.HasTaunt = true;
		}
		yield break;
	}
}
