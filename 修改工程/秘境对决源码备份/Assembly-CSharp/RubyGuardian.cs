using System;
using System.Collections;

public class RubyGuardian : MinionCard
{
	public RubyGuardian()
	{
		this.Name = "地下街劫掠者";
		this.Description = "Battlecry: Deal 2 damage to your enemy hero and restore 2 Health to your hero.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 3;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		InterfaceManager.Instance.SpawnDamageSplatOn(this.Player.Enemy.Hero.Controller, 2);
		yield return this.Player.Enemy.Hero.Damage(null, 2);
		yield return this.Player.Enemy.Hero.CheckDeath();
		yield return this.Player.Hero.Heal(2);
		yield return this.Player.Hero.CheckDeath();
		yield break;
	}
}
