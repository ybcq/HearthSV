using System;
using System.Collections;

public class GrandInquisitor : MinionCard
{
	public GrandInquisitor()
	{
		this.Name = "暗裔破袭者";
		this.Description = "Warcry: The target player puts the top two cards of his library into the graveyard.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 3;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.AllCharacters;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		yield return target.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield return target.Player.Draw(new Func<BaseCard, IEnumerator>(this.GrandInquisitorDraw));
		yield break;
	}

	public IEnumerator GrandInquisitorDraw(BaseCard card)
	{
		yield return card.Discard();
		yield break;
	}

	public override bool CanBattlecryTarget(Character target)
	{
		return target != null && target.IsHero();
	}
}
