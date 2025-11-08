using System;
using System.Collections;

public class SigilofSilence : SpellCard
{
	public SigilofSilence()
	{
		this.Name = "Sigil of Silence";
		this.Description = "Give your opponent a Silence card with Held: You can't use your Hero Power or cast other spells.";
		this.Class = HeroClass.DemonHunter;
		this.Rarity = CardRarity.Epic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 4;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		yield return this.Player.Enemy.AddCardToHand(new Silence());
		yield break;
	}
}
