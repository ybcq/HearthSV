using System;
using System.Collections;
using UnityEngine;

public class TreacherousMercenary : MinionCard
{
	public TreacherousMercenary()
	{
		this.Name = "复仇军领袖";
		this.Description = "Whenever an opponent creature attacks, this creature gets + 1 / + 1.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.Biol;
		this.BaseCost = 3;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.BattlecryType = BattlecryType.NoTarget;
		this.Mechanics.OnMinionAttacked.Add(new Func<MinionAttackedEvent, IEnumerator>(this.OnMinionAttacked));
		base.InitializeMinion();
	}

	public IEnumerator OnMinionAttacked(MinionAttackedEvent evt)
	{
		if (evt.Minion.IsEnemyOf(this.Player.Hero) && evt.Minion.Card.MinionType == MinionType.Biol)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			yield return new WaitForSeconds(0.25f);
			base.AddAttackModifier(new Func<int, int>(this.ApplyAttackModifier));
			base.AddHealthModifier(new Func<int, int>(this.ApplyAttackModifier));
			this.CurrentHealth++;
		}
		yield break;
	}

	public int ApplyAttackModifier(int attack)
	{
		return attack + 1;
	}
}
