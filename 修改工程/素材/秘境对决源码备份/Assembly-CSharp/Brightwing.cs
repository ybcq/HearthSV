using System;

public class Brightwing : MinionCard
{
	public Brightwing()
	{
		this.Name = "恶瘴冥灵";
		this.Description = "The cost is increased to the lowest number of hands in both parties.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 4;
		this.BaseAttack = 8;
		this.BaseHealth = 7;
		this.IsStealth = true;
		this.BattlecryType = BattlecryType.NoTarget;
		base.AddCostModifier(new Func<int, int>(this.HandNumberModifier));
		base.InitializeMinion();
	}

	public int HandNumberModifier(int cost)
	{
		if (this.Player.Hand.Count > this.Player.Enemy.Hand.Count)
		{
			return cost + this.Player.Enemy.Hand.Count;
		}
		return cost + this.Player.Hand.Count;
	}
}
