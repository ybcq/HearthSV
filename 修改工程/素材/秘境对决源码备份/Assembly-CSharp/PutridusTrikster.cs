using System;
using System.Collections;
using System.Linq;

public class PutridusTrikster : MinionCard
{
	public PutridusTrikster()
	{
		this.Name = "墓生食尸鬼";
		this.Description = "Warcry: For every creature card in your graveyard, you get +1/+1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Common;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 1;
		this.BaseHealth = 1;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.Battlecry.Add(new Func<Character, IEnumerator>(this.Battlecry));
		base.InitializeMinion();
	}

	public IEnumerator Battlecry(Character target)
	{
		this.destroyedFragments = this.Player.DeadMinions.Count((MinionCard m) => m.MinionType == MinionType.Biol);
		base.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		base.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + this.destroyedFragments;
	}

	public int destroyedFragments;
}
