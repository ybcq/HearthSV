using System;
using System.Collections;

public class TrialofAncientKeepers : SpellCard
{
	public TrialofAncientKeepers()
	{
		this.Name = "龙之启示";
		this.Description = "Your Hero'TurnMana Add 1. If your Hero'TurnMana > 7, Draw a card.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		Minion minion = (Minion)target;
		if (this.Player.TurnMana < 10)
		{
			Player player = this.Player;
			int turnMana = player.TurnMana;
			player.TurnMana = turnMana + 1;
			yield return turnMana;
		}
		if (this.Player.TurnMana >= 7)
		{
			yield return this.Player.Draw(null);
		}
		yield break;
	}
}
