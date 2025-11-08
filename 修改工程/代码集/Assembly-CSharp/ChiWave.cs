using System;
using System.Collections;

public class ChiWave : SpellCard
{
	public ChiWave()
	{
		this.Name = "奇波";
		this.Description = "Restore 5 Health. Deal 5 damage randomly split amongst enemies";
		this.Class = HeroClass.Monk;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.AllCharacters;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return target.Heal(5);
		int damage = 5 + this.Player.GetSpellPower();
		int num;
		for (int i = 0; i < damage; i = num + 1)
		{
			Character randomEnemy = RNG.RandomItemFrom<Character>(this.Player.Enemy.GetAllCharacters());
			InterfaceManager.Instance.SpawnDamageSplatOn(randomEnemy.Controller, 1);
			yield return randomEnemy.Damage(null, 1);
			yield return randomEnemy.CheckDeath();
			randomEnemy = null;
			num = i;
		}
		yield break;
	}
}
