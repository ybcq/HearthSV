using System;
using System.Collections;

public class Darkness : SpellCard
{
	public Darkness()
	{
		this.Name = "Darkness";
		this.Description = "Draw a card for each friendly Evasive character, then give your minions Evasion.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Rare;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 2;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		foreach (Character characer in this.Player.GetAllCharacters())
		{
			if (characer.IsEvasive)
			{
				yield return this.Player.Draw(null);
			}
		}
		foreach (Minion minion in this.Player.Minions)
		{
			if (!minion.IsEvasive)
			{
				minion.SetEvasion(true);
			}
		}
		yield break;
	}
}
