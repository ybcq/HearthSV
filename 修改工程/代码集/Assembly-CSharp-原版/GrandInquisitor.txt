using System;
using System.Collections;

public class GrandInquisitor : MinionCard
{
	public GrandInquisitor()
	{
		this.Name = "Grand Inquisitor";
		this.Description = "Battlecry: Draw 2 cards. Discard any spell drawn this way.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 5;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield break;
	}

	public IEnumerator GrandInquisitorDraw(BaseCard card)
	{
		if (card is SpellCard)
		{
			yield return card.Discard();
		}
		yield break;
	}
}
