using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CryoftheAbyss : WeaponCard
{
	public CryoftheAbyss()
	{
		this.Name = "Cry of the Abyss";
		this.Description = "Deathrattle: Put a random Naga from your hand into the battlefield.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.BaseCost = 6;
		this.BaseAttack = 5;
		this.BaseDurability = 2;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeWeapon();
	}

	public IEnumerator Deathrattle(Minion minion)
	{
		if (this.Player.Minions.Count < 7)
		{
			List<MinionCard> nagasInHand = (from c in this.Player.Hand.OfType<MinionCard>()
			where c.MinionType == MinionType.Naga
			select c).ToList<MinionCard>();
			if (nagasInHand.Count > 0)
			{
				MinionCard randomNaga = RNG.RandomItemFrom<MinionCard>(nagasInHand);
				this.Player.RemoveCardFromHand(randomNaga);
				yield return this.Player.SummonMinion(randomNaga);
			}
		}
		yield break;
	}
}
