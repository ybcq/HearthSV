using System;
using System.Collections;
using System.Collections.Generic;

public class Immolation : SpellCard
{
	public Immolation()
	{
		this.Name = "Immolation";
		this.Description = "Give a friendly minion \"At the end of your turn, deal 1 damage to all enemies.\"";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Common;
		this.TargetType = TargetType.FriendlyMinions;
		this.BaseCost = 3;
		base.InitializeSpell();
	}

	public override bool CanCast()
	{
		return this.Player.Minions.TargeteablesBySpellOf(this.Player).Count > 0;
	}

	public override IEnumerator Cast(Character target)
	{
		Minion targetMinion = (Minion)target;
		targetMinion.Mechanics.OnTurnEnd.Add((TurnEvent x) => this.OnTurnEnd(x, targetMinion));
		yield break;
	}

	public IEnumerator OnTurnEnd(TurnEvent evt, Minion self)
	{
		if (evt.Player == this.Player)
		{
			self.IsStealth = false;
			List<Character> availableTargets = this.Player.Enemy.GetAllCharacters();
			foreach (Character enemy in availableTargets)
			{
				yield return enemy.Damage(null, 1);
			}
			foreach (Character enemy2 in availableTargets)
			{
				yield return enemy2.CheckDeath();
			}
		}
		yield break;
	}
}
