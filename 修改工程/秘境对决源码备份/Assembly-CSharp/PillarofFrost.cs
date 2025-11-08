using System;
using System.Collections;
using System.Collections.Generic;

public class PillarofFrost : SpellCard
{
	public PillarofFrost()
	{
		this.Name = "末日狂欢";
		this.Description = "Destroy all minions, then Discards some Cards from your Deck.";
		this.Class = HeroClass.DeathKnight;
		this.Rarity = CardRarity.Basic;
		this.TargetType = TargetType.NoTarget;
		this.BaseCost = 6;
		base.InitializeSpell();
	}

	public override IEnumerator Cast(Character target)
	{
		int num = this.Player.Minions.Count + this.Player.Enemy.Minions.Count;
		foreach (Minion minion in GameManager.Instance.GetAllMinions())
		{
			if (minion.Card.MinionType != MinionType.Totem)
			{
				yield return minion.Destroy();
			}
		}
		List<Minion>.Enumerator enumerator = default(List<Minion>.Enumerator);
		int num2;
		for (int i = 1; i <= num; i = num2 + 1)
		{
			yield return this.Player.Draw(new Func<BaseCard, IEnumerator>(this.PillarofFrostDra));
			num2 = i;
		}
		yield break;
		yield break;
	}

	public IEnumerator PillarofFrostDra(BaseCard card)
	{
		yield return card.Discard();
		yield break;
	}
}
