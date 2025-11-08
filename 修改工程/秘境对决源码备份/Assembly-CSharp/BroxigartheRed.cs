using System;
using System.Collections;

public class BroxigartheRed : MinionCard
{
	public BroxigartheRed()
	{
		this.Name = "风语冥想师";
		this.Description = "Warcry: Draw a card.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.Draw(null);
		yield break;
	}
}
