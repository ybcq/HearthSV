using System;
using System.Collections;
using System.Linq;

public class Immolation : SpellCard
{
	public Immolation()
	{
		this.Name = "暗夜中的兽群";
		this.Description = "Summon 2 jungle bats to the battlefield, and inflict an equal amount of damage to the enemy's entourage as the number of jungle bats on your battlefield.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.EnemyMinions;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Enemy.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		HighWarlordNajentus ImmolationCard = new HighWarlordNajentus();
		yield return this.Player.SummonMinion(ImmolationCard);
		if (ImmolationCard.Minion != null)
		{
			ImmolationCard.Minion.Mechanics.RemoveAll();
		}
		HighWarlordNajentus ImmolationCard2 = new HighWarlordNajentus();
		yield return this.Player.SummonMinion(ImmolationCard2);
		if (ImmolationCard2.Minion != null)
		{
			ImmolationCard2.Minion.Mechanics.RemoveAll();
		}
		int num = 0;
		foreach (Minion minion in (from m in this.Player.Minions
		where m.Card is HighWarlordNajentus
		select m).ToList<Minion>())
		{
			num++;
		}
		InterfaceManager.Instance.SpawnDamageSplatOn(target.Controller, num + this.Player.GetSpellPower());
		yield return target.Damage(null, num + this.Player.GetSpellPower());
		yield return target.CheckDeath();
		yield break;
	}
}
