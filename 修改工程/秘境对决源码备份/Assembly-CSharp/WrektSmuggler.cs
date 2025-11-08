using System;
using System.Collections;

public class WrektSmuggler : MinionCard
{
	public WrektSmuggler()
	{
		this.Name = "唤尸掘墓人";
		this.Description = "The Curtain Call: Summon 1 zombie to the battlefield.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Undead;
		this.BaseCost = 5;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		HighWarlordNajentus WrektSmugglerCard = new HighWarlordNajentus
		{
			BaseCost = 2,
			BaseAttack = 2,
			BaseHealth = 2,
			CurrentHealth = 2
		};
		yield return self.Player.SummonMinion(WrektSmugglerCard);
		if (WrektSmugglerCard.Minion != null)
		{
			WrektSmugglerCard.Minion.Mechanics.RemoveAll();
		}
		yield break;
	}
}
