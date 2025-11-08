using System;
using System.Collections;

public class GoblinMerchant : MinionCard
{
	public GoblinMerchant()
	{
		this.Name = "光辉营志愿军";
		this.Description = "Warcry: Gain 3 armor.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.Hero.CurrentArmor += 3;
		yield break;
	}
}
