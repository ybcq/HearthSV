using System;
using System.Collections;

public class FallenrootHellcaller : MinionCard
{
	public FallenrootHellcaller()
	{
		this.Name = "来去自如的潜伏者";
		this.Description = "At the end of your turn, switch sides.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Basic;
		this.MinionType = MinionType.General;
		this.BaseCost = 5;
		this.BaseAttack = 5;
		this.BaseHealth = 6;
		this.Mechanics.OnTurnEnd.Add(new Func<TurnEvent, IEnumerator>(this.OnTurnEnd));
		base.InitializeMinion();
	}

	private IEnumerator OnTurnEnd(TurnEvent evt)
	{
		if (evt.Player == this.Player)
		{
			FallenrootHellcaller minionCard = new FallenrootHellcaller
			{
				BaseAttack = this.Minion.BaseAttack,
				BaseHealth = this.Minion.BaseHealth,
				CurrentHealth = this.Minion.CurrentHealth,
				CantAttack = this.Minion.CantAttack,
				CantAttackTaunt = this.Minion.CantAttackTaunt,
				HasFreeze = this.Minion.HasFreeze,
				HasTaunt = this.Minion.HasTaunt,
				HasCharge = this.Minion.HasCharge,
				HasPoison = this.Minion.HasPoison,
				HasWindfury = this.Minion.HasWindfury,
				HasDivineShield = this.Minion.HasDivineShield,
				HasSpellshield = this.Minion.HasSpellshield,
				IsEvasive = this.Minion.IsEvasive,
				IsInaccurate = this.Minion.IsInaccurate,
				IsStealth = this.Minion.IsStealth,
				SpellPower = this.Minion.SpellPower,
				IsImmune = this.Minion.IsImmune
			};
			yield return this.Player.Enemy.SummonMinion(minionCard);
			this.Minion.Mechanics.RemoveAll();
			yield return this.Minion.Destroy();
		}
		yield break;
	}
}
