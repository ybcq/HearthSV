using System;
using System.Collections;
using System.Collections.Generic;

public class RiverpawOutrunner : MinionCard
{
	public RiverpawOutrunner()
	{
		this.Name = "萌芽花寄生";
		this.Description = "Battlecry: The cost of creature cards with cost ≥ 7 in your hand is reduced 2 this round.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 3;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		using (List<BaseCard>.Enumerator enumerator = this.Player.Hand.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BaseCard baseCard = enumerator.Current;
				if (baseCard.BaseCost >= 7)
				{
					baseCard.AddCostModifier(new Func<int, int>(this.RiverpawOutrunnerModifier));
				}
			}
			yield break;
		}
		yield break;
	}

	public int RiverpawOutrunnerModifier(int mana)
	{
		return mana - 2;
	}

	public IEnumerator OnTurnEnd(TurnEvent turnEvent)
	{
		if (this.RiverpawOutrunnerHasBattlecry == 1)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			foreach (BaseCard baseCard in this.Player.Hand)
			{
				baseCard.RemoveCostModifier(new Func<int, int>(this.RiverpawOutrunnerModifier));
			}
			this.RiverpawOutrunnerHasBattlecry = 0;
		}
		yield break;
	}

	public int RiverpawOutrunnerHasBattlecry;
}
