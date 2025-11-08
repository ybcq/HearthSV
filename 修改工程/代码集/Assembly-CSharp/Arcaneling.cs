using System;
using System.Collections;

public class Arcaneling : MinionCard
{
	public Arcaneling()
	{
		this.Name = "奥术元素";
		this.Description = "Whenever you spend mana, deal 1 damage to a random enemy.";
		this.Class = HeroClass.Neutral;
		this.Rarity = CardRarity.Rare;
		this.MinionType = MinionType.General;
		this.BaseCost = 2;
		this.BaseAttack = 2;
		this.BaseHealth = 2;
		this.ManaSpentSubscription = this.Mechanics.OnManaSpent.Add(new Func<ManaSpentEvent, IEnumerator>(this.OnManaSpent));
		base.InitializeMinion();
	}

	public IEnumerator OnManaSpent(ManaSpentEvent evt)
	{
		if (evt.Player == this.Player && evt.ManaAmount > 0)
		{
			this.Minion.Controller.As<MinionController>().AnimateTriggerFlash();
			Character randomTarget = RNG.RandomItemFrom<Character>(this.Player.Enemy.GetAllCharacters());
			yield return randomTarget.Damage(null, 1);
			yield return randomTarget.CheckDeath();
		}
		yield break;
	}

	public IDisposable ManaSpentSubscription;
}
