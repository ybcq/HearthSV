using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DunMoroghWendigo : MinionCard
{
	public DunMoroghWendigo()
	{
		this.Name = "Ñª·«ÅÚÊÖ";
		this.Description = "Battlecry: Give your other Pirates +3 Attack.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.Pirate;
		this.BaseCost = 4;
		this.BaseAttack = 4;
		this.BaseHealth = 3;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public override bool CanBattlecry()
	{
		return GameManager.Instance.GetAllMinions().Any((Minion m) => m.Card.MinionType == MinionType.Pirate && m.Player == this.Player);
	}

	public IEnumerator Battlecry(Character target)
	{
		using (List<Minion>.Enumerator enumerator = GameManager.Instance.GetAllMinions().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Minion minion = enumerator.Current;
				if (minion.Card.MinionType == MinionType.Pirate && minion.IsFriendlyOf(this.Player.Hero))
				{
					minion.AddAttackModifier(new Func<int, int>(this.DunMoroghWendigoModifier));
				}
			}
			yield break;
		}
		yield break;
	}

	public int DunMoroghWendigoModifier(int attack)
	{
		return attack + 3;
	}
}
