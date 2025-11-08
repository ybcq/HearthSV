using System;
using System.Collections;

public class TreacherousMercenary : MinionCard
{
	public TreacherousMercenary()
	{
		this.Name = "Treacherous Mercenary";
		this.Description = "Charge. Battlecry: Deal 3 damage to your hero.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 3;
		this.BaseAttack = 4;
		this.BaseHealth = 2;
		this.HasCharge = true;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character character)
	{
		yield return this.Player.Hero.Damage(null, 3);
		yield return this.Player.Hero.CheckDeath();
		yield break;
	}
}
