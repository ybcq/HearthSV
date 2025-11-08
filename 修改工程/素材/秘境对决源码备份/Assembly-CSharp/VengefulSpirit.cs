using System;
using System.Collections;

public class VengefulSpirit : MinionCard
{
	public VengefulSpirit()
	{
		this.Name = "亚夏巫觐";
		this.Description = "嘲讽，亡语：将你牌库顶三张牌置入坟场.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 1;
		this.BaseHealth = 4;
		this.HasTaunt = true;
		this.Mechanics.Deathrattle.Add(new Func<Minion, IEnumerator>(this.Deathrattle));
		base.InitializeMinion();
	}

	public IEnumerator Deathrattle(Minion self)
	{
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.VengefulSpiritDraw));
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.VengefulSpiritDraw));
		yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.VengefulSpiritDraw));
		yield break;
	}

	public IEnumerator VengefulSpiritDraw(BaseCard card)
	{
		yield return card.Discard();
		yield break;
	}
}
