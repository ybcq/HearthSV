using System;
using System.Collections;
using UnityEngine;

public class EyeBeam : SpellCard
{
	public EyeBeam()
	{
		this.Name = "Eye Beam";
		this.Description = "Deal 7 damage to the first centermost enemy character.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		if (this.Player.Enemy.Minions.Count % 2 == 1)
		{
			int middlePosition = (int)Mathf.Floor((float)this.Player.Enemy.Minions.Count / 2f);
			yield return this.Player.Enemy.Minions[middlePosition].Damage(null, 7 + this.Player.GetSpellPower());
			yield return this.Player.Enemy.Minions[middlePosition].CheckDeath();
		}
		else
		{
			yield return this.Player.Enemy.Hero.Damage(null, 7 + this.Player.GetSpellPower());
			yield return this.Player.Enemy.Hero.CheckDeath();
		}
		yield break;
	}
}
